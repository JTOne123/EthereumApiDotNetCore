﻿using System.Threading.Tasks;
using Core.Repositories;
using Core.Timers;
using Nethereum.Web3;
using Services;
using Common.Log;
using Core.Settings;
using System.Numerics;
using System;

namespace EthereumJobs.Job
{
    public class TransferContractUserAssignmentJob : TimerPeriod
    {
        //10 minutes
        private const int TimerPeriodSeconds = 60;
        private const int AlertNotChangedBalanceCount = 3;

        private readonly ILog _logger;
        private readonly ITransferContractUserAssignmentQueueService _transferContractUserAssignmentQueueService;
        private readonly IBaseSettings _settings;
        private readonly ICoinRepository _coinRepository;

        public TransferContractUserAssignmentJob(IBaseSettings settings,
            ILog logger,
            ITransferContractUserAssignmentQueueService transferContractUserAssignmentQueueService,
            ICoinRepository coinRepository
            ) :
            base("MonitoringTransferContracts", TimerPeriodSeconds * 1000, logger)
        {
            _settings = settings;
            _logger = logger;
            _transferContractUserAssignmentQueueService = transferContractUserAssignmentQueueService;
            _coinRepository = coinRepository;
        }

        public override async Task Execute()
        {
            try
            {
                while (await _transferContractUserAssignmentQueueService.Count() != 0 && Working)
                {
                    var assignment = await _transferContractUserAssignmentQueueService.GetContract();

                    var web3 = new Web3(_settings.EthereumUrl);

                    ICoin coinAdapter = await _coinRepository.GetCoinByAddress(assignment.CoinAdapterAddress);
                    if (coinAdapter == null)
                    {
                        continue;
                    }

                    string coinAbi;
                    if (coinAdapter.ContainsEth)
                    {
                        coinAbi = _settings.EthAdapterContract.Abi;
                    }
                    else
                    {
                        coinAbi = _settings.TokenAdapterContract.Abi;
                    }

                    await web3.Personal.UnlockAccount.SendRequestAsync(_settings.EthereumMainAccount,
                       _settings.EthereumMainAccountPassword, 120);

                    var contract = web3.Eth.GetContract(coinAbi, assignment.CoinAdapterAddress);
                    var function = contract.GetFunction("setTransferAddressUser");
                    //function setTransferAddressUser(address userAddress, address transferAddress) onlyowner{
                    string transaction =
                        await function.SendTransactionAsync(_settings.EthereumMainAccount,
                        assignment.UserAddress, assignment.TransferContractAddress);
                }
            }
            catch (Exception ex)
            {
                await _logger.WriteErrorAsync("EthereumJob", "TransferContractUserAssignmentJob", "", ex);
            }
        }
    }
}
