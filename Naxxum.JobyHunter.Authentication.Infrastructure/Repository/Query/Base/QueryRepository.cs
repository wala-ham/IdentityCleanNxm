using Microsoft.Extensions.Configuration;
using Naxxum.JobyHunter.Authentication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Infrastructure.Repository.Query.Base
{
    // Generic Query repository class
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
