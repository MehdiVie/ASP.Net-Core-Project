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
    public class StatementRepository : Repository<Statement>, IStatementRepository
    {
        private readonly ApplicationDbContext _db;
        public StatementRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Statement entity)
        {
            _db.Update(entity);
        }

    }
}
