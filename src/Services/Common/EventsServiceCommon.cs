﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using EthereumSamuraiApiCaller;
using EthereumSamuraiApiCaller.Models;
using Lykke.Job.EthereumCore.Contracts.Enums.LykkePay;
using Lykke.Job.EthereumCore.Contracts.Events.LykkePay;
using Lykke.Service.EthereumCore.Core;
using Lykke.Service.EthereumCore.Core.Common;
using Lykke.Service.EthereumCore.Core.LykkePay;
using Lykke.Service.EthereumCore.Core.Repositories;
using Lykke.Service.EthereumCore.Core.Services;
using Lykke.Service.EthereumCore.Core.Settings;
using Lykke.Service.EthereumCore.Services.PrivateWallet;
using Lykke.Service.RabbitMQ;
using Newtonsoft.Json.Linq;

namespace Lykke.Service.EthereumCore.Services.Common
{
    public abstract class EventsServiceCommon : ILykkePayEventsService
    {
        protected abstract string HotWalletMarker { get; }

        private readonly IBlockSyncedByHashRepository _blockSyncedRepository;
        private readonly IEthereumSamuraiAPI _indexerApi;
        private readonly IErc20DepositContractLocatorService _depositContractService;
        private readonly IEthereumIndexerService _ethereumIndexerService;
        private readonly IRabbitQueuePublisher _rabbitQueuePublisher;

        public EventsServiceCommon(
            IBlockSyncedByHashRepository blockSyncedRepository,
            IEthereumSamuraiAPI indexerApi,
            IEthereumIndexerService ethereumIndexerService,
            IRabbitQueuePublisher rabbitQueuePublisher,
            IErc20DepositContractLocatorService depositContractService)
        {
            _blockSyncedRepository = blockSyncedRepository;
            _indexerApi = indexerApi;
            _depositContractService = depositContractService;
            _ethereumIndexerService = ethereumIndexerService;
            _rabbitQueuePublisher = rabbitQueuePublisher;
        }

        public async Task<(BigInteger? amount, string blockHash, ulong blockNumber)> IndexCashinEventsForErc20TransactionHashAsync(string transactionHash)
        {
            BigInteger result = 0;
            var transaction = await _ethereumIndexerService.GetTransactionAsync(transactionHash);

            if (transaction == null)
                return (null, null, 0);

            if (transaction.ErcTransfer != null)
            {
                //only one transfer could appear in deposit transaction
                foreach (var item in transaction.ErcTransfer)
                {
                    if (!await _depositContractService.ContainsAsync(item.From?.ToLower()))
                    {
                        continue;
                    }

                    BigInteger.TryParse(item.Value, out result);
                }
            }

            return (result, transaction.Transaction?.BlockHash, transaction.Transaction?.BlockNumber ?? 0);
        }

        public async Task IndexCashinEventsForErc20Deposits()
        {
            var indexerStatusResponse = await _indexerApi.ApiIsAliveGetWithHttpMessagesAsync();
            if (indexerStatusResponse.Response.IsSuccessStatusCode)
            {
                var responseContent = await indexerStatusResponse.Response.Content.ReadAsStringAsync();
                var indexerStatus = JObject.Parse(responseContent);
                var lastIndexedBlock = BigInteger.Parse(indexerStatus["blockchainTip"].Value<string>());
                var lastSyncedBlock = await GetLastSyncedBlockNumber(HotWalletMarker);

                while (lastSyncedBlock <= lastIndexedBlock)
                {
                    //Get all transfers from block
                    var transfersResponse = await _indexerApi.ApiErc20TransferHistoryGetErc20TransfersPostAsync
                    (
                        new GetErc20TransferHistoryRequest
                        {
                            BlockNumber = (long)lastSyncedBlock,
                        }
                    );

                    switch (transfersResponse)
                    {
                        case IEnumerable<Erc20TransferHistoryResponse> transfers:
                            foreach (var transfer in transfers)
                            {
                                // Ignore transfers from not deposit contract addresses
                                if (!await _depositContractService.ContainsAsync(transfer.To))
                                {
                                    continue;
                                }

                                var id = $"Detected_{Guid.NewGuid()}";
                                await _rabbitQueuePublisher.PublshEvent(new TransferEvent(id,
                                    transfer.TransactionHash,
                                    transfer.TransferAmount,
                                    transfer.Contract,
                                    transfer.FromProperty,
                                    transfer.To,
                                    transfer.BlockHash,
                                    (ulong)transfer.BlockNumber,
                                    SenderType.Customer,
                                    EventType.Detected
                                    ));
                            }

                            break;
                        case ApiException exception:
                            throw new Exception($"Ethereum indexer responded with error: {exception.Error.Message}");
                        default:
                            throw new Exception($"Ethereum indexer returned unexpected response");
                    }

                    var blockCurrent = (await _indexerApi.ApiBlockNumberByBlockNumberGetAsync((long)lastSyncedBlock)) as BlockResponse;

                    if (blockCurrent == null)
                        return;

                    var parentBlock = await _blockSyncedRepository.GetByPartitionAndHashAsync(HotWalletMarker, blockCurrent.ParentHash);
                    if (parentBlock == null)
                    {
                        lastSyncedBlock--;
                        await _blockSyncedRepository.DeleteByPartitionAndHashAsync(HotWalletMarker, blockCurrent.ParentHash);
                        continue;
                    }

                    await _blockSyncedRepository.InsertAsync(new BlockSyncedByHash()
                    {
                        BlockNumber = lastSyncedBlock.ToString(),
                        Partition = HotWalletMarker,
                        BlockHash = blockCurrent.BlockHash
                    });

                    lastSyncedBlock++;
                }
            }
            else
            {
                throw new Exception("Can not obtain ethereum indexer status.");
            }
        }

        private async Task<BigInteger> GetLastSyncedBlockNumber(string coinAdapterAddress)
        {
            string lastSyncedBlockNumber = (await _blockSyncedRepository.GetLastSyncedAsync(coinAdapterAddress))?.BlockNumber;
            BigInteger lastSynced;
            BigInteger.TryParse(lastSyncedBlockNumber, out lastSynced);

            return lastSynced;
        }
    }
}