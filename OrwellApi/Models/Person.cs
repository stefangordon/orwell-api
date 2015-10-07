using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace OrwellApi.Models
{

    public class Person : TableEntity
    {
        public readonly string PartitionName = "person";

        public string Alias { get; set; }

        public string DisplayName { get; set; }

        public bool EmailNotify { get; set; }

        public Person(string alias, string DisplayName)
        {
            this.PartitionKey = PartitionName;
            this.RowKey = alias;
        }

        public Person() { }
    }
}