﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserTransferWallet
    {
        string UserAddress { get; }
        string TransferContractAddress { get; }
        DateTime UpdateDate { get; }
        BigInteger LastBalance { get; set; }
    }

    public class UserTransferWallet : IUserTransferWallet
    {
        public string UserAddress { get; set; }
        public string TransferContractAddress { get; set; }
        public DateTime UpdateDate { get; set; }
        public BigInteger LastBalance { get; set; }
    }

    public interface IUserTransferWalletRepository
    {
        Task SaveAsync(IUserTransferWallet wallet);
        Task ReplaceAsync(IUserTransferWallet wallet);
        Task<IUserTransferWallet> GetUserContractAsync(string userAddress, string transferContractAddress);
    }
}