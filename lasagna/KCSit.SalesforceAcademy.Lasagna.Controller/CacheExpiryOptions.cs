using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Controller
{
    public class CacheExpiryOptions : MemoryCacheEntryOptions
    {

        public CacheExpiryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            Priority = CacheItemPriority.High;
            SlidingExpiration = TimeSpan.FromMinutes(2);
        }




    }
}
