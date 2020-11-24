using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.DomainObjects.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
