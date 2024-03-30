using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarayeAzadi.Domain.Entities;

namespace BarayeAzadi.Application.Services.Interface
{
    public interface IStatementService
    {
        IEnumerable<Statement> GetAllStatement();
        Statement GetStatementById(int id);
        void CreateStatement(Statement statement);
        void UpdateStatement(Statement statement);
        bool DeleteStatement(int statementId);
    }
}
