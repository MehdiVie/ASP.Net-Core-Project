using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarayeAzadi.Domain.Entities;

namespace BarayeAzadi.Application.Services.Interface
{
    public interface IContactusService
    {
        IEnumerable<Contactus> GetAllContactuss();
        Contactus GetContactusById(int id);
        void CreateContactus(Contactus contactus);
        bool DeleteContactus(Contactus contactus);
    }
}
