using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace OrwellApi.Models
{
    public class Project : TableEntity
    {
        public readonly string PartitionName = "project";

        [IgnoreProperty]
        [JsonProperty("name")]
        public string ProjectName
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }
     
        public Project(string projectName)
        {
            this.PartitionKey = PartitionName;
            this.RowKey = projectName;
        }

        public Project() { }

    }
                
}