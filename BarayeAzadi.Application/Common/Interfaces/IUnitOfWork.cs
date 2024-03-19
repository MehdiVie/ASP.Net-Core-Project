using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarayeAzadi.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IContactusRepository Contactus { get; }

        void Save();
    }
}
