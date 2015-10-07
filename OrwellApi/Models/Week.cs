using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace OrwellApi.Models
{
    public class Week : TableEntity
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("week")]
        public int WeekNumber { get; set; }

        [JsonProperty("weekEndDate")]
        public DateTime WeekEndDate { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [IgnoreProperty]
        [JsonProperty("entries")]
        public IEnumerable<ProjectEntry> Entries { get; set; }

        [JsonIgnore]
        public string EntriesJson
        {
            get
            {
                return JsonConvert.SerializeObject(Entries);
            }
            set
            {
                Entries = JsonConvert.DeserializeObject<IEnumerable<ProjectEntry>>(value);
            }
        }

        public Week(int year, int weekNumber, string alias) : 
            this(new YearWeek { Year = year, Week = weekNumber }, alias) { }

        public Week(YearWeek yearWeek, string alias)
        {
            this.PartitionKey = yearWeek.ToString();
            this.RowKey = alias;
            this.Year = yearWeek.Year;
            this.WeekNumber = yearWeek.Week;
            this.WeekEndDate = DateUtilities.DateOfWeek(yearWeek);
            this.Alias = alias;
        }

        public Week() { }

    }

    public class ProjectEntry
    {
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("timeAllocation")]
        public int TimeAllocation { get; set; }

        [JsonProperty("health")]
        public int Health { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

    }
}