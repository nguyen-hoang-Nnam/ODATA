using Ass2.DAL.Repository.IRepository;
using Ass2.Model.Data;
using Ass2.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.DAL.Repository
{
    public class FootballClubRepository : GenericRepository<FootballClub>, IFootballClubRepository
    {
        private readonly EnglishPremierLeague2024DbContext _context;

        public FootballClubRepository(EnglishPremierLeague2024DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
