using Ass2.DAL.Repository.IRepository;
using Ass2.Model.Data;
using Ass2.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.DAL.Repository
{
    public class PremierLeagueAccountRepository : GenericRepository<PremierLeagueAccount>, IPremierLeagueAccountRepository
    {
        private readonly EnglishPremierLeague2024DbContext _context;

        public PremierLeagueAccountRepository(EnglishPremierLeague2024DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PremierLeagueAccount> GetByEmailAsync(string email)
        {
            return await _context.PremierLeagueAccounts
                .FirstOrDefaultAsync(u => u.EmailAddress == email);
        }

    }
}
