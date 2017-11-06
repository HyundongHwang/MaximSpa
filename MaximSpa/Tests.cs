using MaximSpa.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MaximSpa
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void cleanup_maxim()
        {
            var dao = new MaximSpaDAO();
            dao.MaximTable.DeleteIfExists();
        }

        [TestMethod]
        public void insert_maxim()
        {
            var dao = new MaximSpaDAO();

            for (int i = 0; i < 10; i++)
            {
                var newMaxim = new MaximEntity();
                newMaxim.Name = "hello";
                newMaxim.Content = "world";
                var insertOperation = TableOperation.InsertOrReplace(newMaxim);
                dao.MaximTable.Execute(insertOperation);
            }

            var query = new TableQuery<MaximEntity>();
            var maximList = dao.MaximTable.ExecuteQuery(query).ToList();




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