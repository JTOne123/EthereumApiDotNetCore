﻿using Lykke.Service.EthereumCore.BusinessModels;
using Lykke.Service.EthereumCore.Core.Exceptions;
using Lykke.Service.EthereumCore.Models;
using Lykke.Service.EthereumCore.Models.Indexer;
using Lykke.Service.EthereumCore.Models.Models;
using Lykke.Service.EthereumCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Lykke.Service.EthereumCore.Services.PrivateWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.EthereumCore;

namespace Lykke.Service.EthereumCore.Controllers
{
    [Route("api/internalMessages")]
    [Produces("application/json")]
    public class InternalMessageController : Controller
    {
        private IEthereumIndexerService _ethereumIndexerService;

        public InternalMessageController(IEthereumIndexerService ethereumIndexerService)
        {
            _ethereumIndexerService = ethereumIndexerService;
        }

        [HttpPost("txHash/{transactionHash}")]
        [ProducesResponseType(typeof(FilteredInternalMessagessResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [ProducesResponseType(typeof(ApiException), 500)]
        public async Task<IActionResult> GetInternalMessages([FromRoute] string transactionHash)
        {
            if (!ModelState.IsValid)
            {
                throw new ClientSideException(ExceptionType.WrongParams, JsonConvert.SerializeObject(ModelState.Errors()));
            }

            IEnumerable<InternalMessageModel> messages = await _ethereumIndexerService.GetInternalMessagesForTransactionAsync(transactionHash);
            IEnumerable<Models.Indexer.InternalMessageResponse> result = messages.Select(message =>
                MapInternalMessageModelToResponse(message));

            return Ok(new FilteredInternalMessagessResponse()
            {
                Messages = result
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilteredInternalMessagessResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [ProducesResponseType(typeof(ApiException), 500)]
        public async Task<IActionResult> GetInternalMessages([FromBody]AddressTransactions addressTransactions)
        {
            if (!ModelState.IsValid)
            {
                throw new ClientSideException(ExceptionType.WrongParams, JsonConvert.SerializeObject(ModelState.Errors()));
            }

            Lykke.Service.EthereumCore.BusinessModels.AddressTransaction request = new Lykke.Service.EthereumCore.BusinessModels.AddressTransaction()
            {
                Address = addressTransactions.Address,
                Count = addressTransactions.Count,
                Start = addressTransactions.Start,
            };

            IEnumerable<InternalMessageModel> messages = await _ethereumIndexerService.GetInternalMessagesHistory(request);
            IEnumerable<Models.Indexer.InternalMessageResponse> result = messages.Select(message =>
                MapInternalMessageModelToResponse(message));

            return Ok(new FilteredInternalMessagessResponse()
            {
                Messages = result
            });
        }

        private static InternalMessageResponse MapInternalMessageModelToResponse(InternalMessageModel message)
        {
            return new InternalMessageResponse()
            {
                BlockNumber = message.BlockNumber,
                Depth = message.Depth,
                FromAddress = message.FromAddress,
                MessageIndex = message.MessageIndex,
                ToAddress = message.ToAddress,
                TransactionHash = message.TransactionHash,
                Type = message.Type,
                Value = message.Value.ToString(),
                BlockTimestamp = message.BlockTimestamp,
                BlockTimeUtc = message.BlockTimeUtc
            };
        }
    }
}
