using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaximSpa.Assets.Strings;


namespace MaximSpa.Models
{
    public class MaximSpaDAO
    {
        public CloudTable MaximTable { get; private set; }

        public MaximSpaDAO()
        {
            var storageAccount = CloudStorageAccount.Parse(MaximSecureKeysStrings.azure_storage_connection_string);
            var tableClient = storageAccount.CreateCloudTableClient();
            this.MaximTable = tableClient.GetTableReference("maxim");
            var res = this.MaximTable.CreateIfNotExists();



            //var query = new TableQuery<AzureTableLogEntity>();
            //var maxRowKey = table.ExecuteQuery(query).Max(item => long.Parse(item.RowKey));



            //var log = new AzureTableLogEntity();
            //log.PartitionKey = tmpLog.Level;
            //log.LogLevel = tmpLog.Level;
            //log.Log = tmpLog.Log;
            //var insertOperation = TableOperation.InsertOrReplace(log);
            //table.Execute(insertOperation);
        }
    }
}