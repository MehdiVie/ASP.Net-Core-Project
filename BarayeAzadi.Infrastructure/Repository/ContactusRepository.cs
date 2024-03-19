using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BarayeAzadi.Application.Common.Interfaces;
using BarayeAzadi.Domain.Entities;
using BarayeAzadi.Infrastructure.Data;

namespace BarayeAzadi.Infrastructure.Repository
{
    public class ContactusRepository : Repository<Contactus>, IContactusRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactusRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        

    }
}
