using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrwellApi.Models
{
    public class YearWeek
    {
        public int Year { get; set; }
        public int Week { get; set; }

        public override string ToString()
        {
            return string.Format("{0}_{1}", Year, Week);
        }
    }
}