﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure;
using Core.Log;
using Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Repositories
{

	public class CoinTransactionEntity : TableEntity, ICoinTransaction
	{
		public static string GeneratePartitionKey()
		{
			return "Transactions";
		}
		public string TransactionHash => RowKey;
		public int ConfirmationLevel { get; set; }
		public bool Error { get; set; }

		public static CoinTransactionEntity Create(ICoinTransaction transaction)
		{
			return new CoinTransactionEntity
			{
				RowKey = transaction.TransactionHash,
				PartitionKey = GeneratePartitionKey(),
				ConfirmationLevel = 0,
				Error = transaction.Error
			};
		}

	}


	public class CoinTransactionRepository : ICoinTransactionRepository
	{
		private readonly INoSQLTableStorage<CoinTransactionEntity> _table;		

		public CoinTransactionRepository(INoSQLTableStorage<CoinTransactionEntity> table)
		{
			_table = table;			
		}

		public async Task AddAsync(ICoinTransaction transaction)
		{
			var entity = CoinTransactionEntity.Create(transaction);
			await _table.InsertAsync(entity);
		}

		public async Task InsertOrReplaceAsync(ICoinTransaction transaction)
		{
			var entity = CoinTransactionEntity.Create(transaction);

			await _table.InsertOrReplaceAsync(entity);
		}

		public async Task<ICoinTransaction> GetTransaction(string transactionHash)
		{
			return await _table.GetDataAsync(CoinTransactionEntity.GeneratePartitionKey(), transactionHash);
		}
	}
}