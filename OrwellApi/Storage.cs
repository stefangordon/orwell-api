using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using OrwellApi.Models;

namespace OrwellApi
{
    public static class Storage
    {
        private static CloudStorageAccount _storageAccount;
        private static CloudTableClient _tableClient;   

        public static void Initialize(string storageConnectionString)
        {
            // Get storage account
            _storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            // Create the clients
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public static void UpdateWeek(Week week)
        {
            // Create the CloudTable object that represents the table.
            CloudTable table = _tableClient.GetTableReference("week");
            table.CreateIfNotExists();

            // Create the TableOperation
            TableOperation insertOperation = TableOperation.InsertOrReplace(week);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        public static Week GetWeek(YearWeek yearWeek, string person)
        {
            // Create the CloudTable object that represents the table.
            CloudTable table = _tableClient.GetTableReference("week");
            table.CreateIfNotExists();

            var week = from result in table.CreateQuery<Week>()
                       where result.PartitionKey == string.Format("{0}_{1}", yearWeek.Year, yearWeek.Week)
                       && result.RowKey == person
                       select result;

            return week.FirstOrDefault();
        }

        public static IEnumerable<Week> GetWeekAll(YearWeek yearWeek)
        {
            // Create the CloudTable object that represents the table.
            CloudTable table = _tableClient.GetTableReference("week");
            table.CreateIfNotExists();

            var weeks = from result in table.CreateQuery<Week>()
                       where result.PartitionKey == string.Format("{0}_{1}", yearWeek.Year, yearWeek.Week)
                       select result;

            return weeks;
        }

        public static void AddProject(Project project)
        {
            // Create the CloudTable object that represents the table.
            CloudTable table = _tableClient.GetTableReference("projects");
            table.CreateIfNotExists();

            // Create the TableOperation
            TableOperation insertOperation = TableOperation.InsertOrMerge(project);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        public static IEnumerable<string> GetProjects()
        {
            // Create the CloudTable object that represents the table.
            CloudTable table = _tableClient.GetTableReference("projects");
            table.CreateIfNotExists();

            IEnumerable<string> projects = from result in table.CreateQuery<Week>()
                                           select result.RowKey;

            return projects;
        }
    }
}