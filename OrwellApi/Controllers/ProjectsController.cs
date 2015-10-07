using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrwellApi.Controllers
{
    public class ProjectsController : ApiController
    {
        IEnumerable<string> ProjectCache;
        DateTime CacheInvalidate;

        // GET projects?contains=[term]
        public IEnumerable<string> Get(string contains = "")
        {
            if (CacheInvalidate == null || DateTime.Now > CacheInvalidate)
            {
                // Update Cache
                ProjectCache = Storage.GetProjects();
                CacheInvalidate = DateTime.Now.AddSeconds(30);
            }

            //List<string> projects = new List<string>
            //{
            //    "Down and Out in Paris and London",
            //    "Burmese Days",
            //    "A Clergyman's Daughter",
            //    "Keep the Aspidistra Flying",
            //    "The Road to Wigan Pier",
            //    "Homage to Catalonia",
            //    "Coming Up for Air",
            //    "Animal Farm",
            //    "Nineteen Eighty-Four"
            //};

            if (string.IsNullOrEmpty(contains))
            {
                return ProjectCache;
            }

            return ProjectCache.Where(p => p.ToUpper().Contains(contains.ToUpper()));
        }

    }
}
