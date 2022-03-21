using FitABit.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepository:Repository,IApplicationDbRepository
    {
        public ApplicationDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
