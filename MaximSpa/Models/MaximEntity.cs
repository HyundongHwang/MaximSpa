using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaximSpa.Models
{
    public class MaximEntity : TableEntity
    {
        public MaximEntity()
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = "PartitionKey";
        }

        public string Name { get; set; }
        public string Content { get; set; }
    }
}