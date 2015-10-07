using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrwellApi.Models;

namespace OrwellApi.Controllers
{
    public class WeekController : ApiController
    {
        [Route("week/{year}/{week}/{user}")]
        public Week Get(int year, int week, string user)
        {
            if (user.ToUpper().Equals("ALL"))
            {
                var weeks = Storage.GetWeekAll(new YearWeek { Year = year, Week = week });
                var entries = weeks.SelectMany(w => w.Entries);
                var projects = entries.Select(e => e.ProjectName).Distinct();

                var summaryWeek = new Week(year, week, user);
                var summaryEntries = new List<ProjectEntry>();

                foreach (string project in projects)
                {
                    ProjectEntry newEntry = new ProjectEntry
                    {
                        ProjectName = project,
                        TimeAllocation = (entries.Where(e => e.ProjectName.Equals(project)).Sum(e => e.TimeAllocation)) / weeks.Count(),
                    };

                    summaryEntries.Add(newEntry);
                }

                summaryWeek.Entries = summaryEntries;
                return summaryWeek;
            }
            else
            {
                var retrieved = Storage.GetWeek(new YearWeek { Year = year, Week = week }, user);

                if (retrieved != null)
                {
                    return retrieved;
                }

                return new Week(year, week, user);
            }
        }

        [Route("week/{date}/{user}")]
        public Week Get(DateTime date, string user)
        {
            var yearWeek = DateUtilities.YearWeekFromDate(date);
            return Get(yearWeek.Year, yearWeek.Week, user);
        }

        [HttpGet]
        [Route("week/current/{user}")]
        public Week GetCurrent(string user)
        {
            var yearWeek = DateUtilities.YearWeekFromDate(DateTime.Now);
            return Get(yearWeek.Year, yearWeek.Week, user);
        }

        [HttpGet]
        [Route("week/previous/{user}")]
        public Week GetPrevious(string user)
        {
            var yearWeek = DateUtilities.YearWeekFromDate(DateTime.Now.AddDays(-7));
            return Get(yearWeek.Year, yearWeek.Week, user);
        }

        [HttpPut]
        [Route("week/{date}/{user}")]
        public void Put(DateTime date, string user, [FromBody]Week value)
        {
            if (value.Entries != null)
            {
                var projects = value.Entries.Select(e => e.ProjectName).Distinct();

                foreach (string project in projects)
                {
                    Storage.AddProject(new Project(project));
                }
            }

            var entry = new Week(date.Year, DateUtilities.NumberOfWeek(date), user)
            {             
                Entries = value.Entries
            };

            Storage.UpdateWeek(entry);
        }

        [HttpPut]
        [Route("week/{year}/{week}/{user}")]
        public void Put(int year, int week, string user, [FromBody]Week value)
        {
            Put(DateUtilities.DateOfWeek(year, week), user, value);
        }

        [HttpPut]
        [Route("week/current/{user}")]
        public void PutCurrent(string user, [FromBody]Week value)
        {
            var yearWeek = DateUtilities.YearWeekFromDate(DateTime.Now);
            Put(yearWeek.Year, yearWeek.Week, user, value);
        }

        [HttpPut]
        [Route("week/previous/{user}")]
        public void PutPrevious(string user, [FromBody]Week value)
        {
            var yearWeek = DateUtilities.YearWeekFromDate(DateTime.Now.AddDays(-7));
            Put(yearWeek.Year, yearWeek.Week, user, value);
        }

    }
}
