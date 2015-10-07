using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrwellApi.Controllers
{
    public class PeopleController : ApiController
    {
        // GET people?contains=[term]
        public IEnumerable<string> Get(string contains = "")
        {
            List<string> people = new List<string>
            {
                "Stefan",
                "Tim",
                "Erik",
                "Cezar",
                "Adam"
            };

            if (string.IsNullOrEmpty(contains))
            {
                return people;
            }

            return people.Where(p => p.ToUpper().Contains(contains.ToUpper()));
        }
    }
}
