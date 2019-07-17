using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using _4S.Domain.Entities;

namespace _4S.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
