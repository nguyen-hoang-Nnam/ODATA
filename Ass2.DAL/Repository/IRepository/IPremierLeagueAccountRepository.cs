using Ass2.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.DAL.Repository.IRepository
{
    public interface IPremierLeagueAccountRepository : IGenericRepository<PremierLeagueAccount>
    {
        Task<PremierLeagueAccount> GetByEmailAsync(string email);
    }
}
