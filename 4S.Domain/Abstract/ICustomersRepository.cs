using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4S.Domain.Entities;

namespace _4S.Domain.Abstract
{
    //提供数据实体的接口（存储库模式）
    public interface ICustomersRepository
    {
        IEnumerable<Customer> Customers { get; }
        IEnumerable<maintainOrder> maintainOrders { get; }
    }
}
