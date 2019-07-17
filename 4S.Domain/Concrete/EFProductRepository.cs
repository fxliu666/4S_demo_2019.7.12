using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4S.Domain.Abstract;
using _4S.Domain.Entities;

namespace _4S.Domain.Concrete
{
    public class EFProductRepository:ICustomersRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Customer> Customers
        {
            get { return context.Customers; }
        }
    }
}
