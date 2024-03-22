using BarayeAzadi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarayeAzadi.Application.Common.Interfaces;
using BarayeAzadi.Infrastructure.Data;

namespace BarayeAzadi.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IContactusRepository Contactus { get; private set; }
        public IApplicationUserRepository User { get; private set; }

        

        readonly private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Contactus = new ContactusRepository(db);
            User = new ApplicationUserRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
