﻿using EthereumApi.Models.Models.Airlines;
using Lykke.Service.EthereumCore.Attributes;
using Lykke.Service.EthereumCore.Core.Airlines;
using Lykke.Service.EthereumCore.Core.Exceptions;
using Lykke.Service.EthereumCore.Models;
using Lykke.Service.EthereumCore.Utils;
using Lykke.Service.EthereumCoreSelfHosted.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Lykke.Service.EthereumCore.Core;

namespace Lykke.Service.EthereumCore.Controllers.Airlines
{
    [ApiKeyAuthorize] //Configure keys in settings
    [Route("api/airlines/erc20deposits")]
    [Produces("application/json")]
    public class AirlinesErc20DepositContractsController : Controller
    {
        private readonly IAirlinesErc20DepositContractService _contractService;

        public AirlinesErc20DepositContractsController(
            [KeyFilter(Constants.AirLinesKey)]IAirlinesErc20DepositContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [ProducesResponseType(typeof(void), 401)]
        [ProducesResponseType(typeof(ApiException), 500)]
        public async Task<IActionResult> CreateDepositContractAsync([FromQuery][Required] string userAddress)
        {
            if (!ModelState.IsValid)
            {
                throw new ClientSideException(ExceptionType.WrongParams, JsonConvert.SerializeObject(ModelState.Errors()));
            }

            var contractAddress = await _contractService.AssignContractAsync(userAddress);

            return Ok(new RegisterResponse
            {
                Contract = contractAddress
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [ProducesResponseType(typeof(void), 401)]
        [ProducesResponseType(typeof(ApiException), 500)]
        public async Task<IActionResult> GetDepositContractAsync([FromQuery][Required] string userAddress)
        {
            if (!ModelState.IsValid)
            {
                throw new ClientSideException(ExceptionType.WrongParams, JsonConvert.SerializeObject(ModelState.Errors()));
            }

            var contractAddress = await _contractService.GetContractAddressAsync(userAddress);

            return Ok(new RegisterResponse
            {
                Contract = contractAddress
            });
        }

        //TODO: Contract response
        [HttpPost("transfer")]
        [ProducesResponseType(typeof(OperationIdResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [ProducesResponseType(typeof(void), 401)]
        [ProducesResponseType(typeof(ApiException), 500)]
        public async Task<IActionResult> TransferAsync([FromBody] AirlinesTransferFromDepositRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new ClientSideException(ExceptionType.WrongParams, JsonConvert.SerializeObject(ModelState.Errors()));
            }

            string opId = await _contractService.RecievePaymentFromDepositContractAsync(request.DepositContractAddress?.ToLower(),
                request.TokenAddress?.ToLower(),
                request.DestinationAddress?.ToLower(),
                request.TokenAmount);

            return Ok(new OperationIdResponse()
            {
                OperationId = opId
            });
        }
    }
}