using Ass2.DAL.Repository.IRepository;
using Ass2.Model.Data;
using Ass2.Model.Models;
using Ass2.Model.Models.DTO.FootballPlayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.DAL.Repository
{
    public class FootballPlayerRepository : GenericRepository<FootballPlayer>, IFootballPlayerRepository
    {
        private readonly EnglishPremierLeague2024DbContext _context;

        public FootballPlayerRepository(EnglishPremierLeague2024DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FootballPlayerDTO>> GetAllPlayersWithClubAsync(string searchTerm)
        {
            return await _context.FootballPlayers
                .Include(p => p.FootballClub)
                .Where(p => p.Achievements.Contains(searchTerm) ||
                             p.Nomination.Contains(searchTerm))
                .Select(p => new FootballPlayerDTO
                {
                    FootballPlayerId = p.FootballPlayerId,
                    FullName = p.FullName,
                    Achievements = p.Achievements,
                    Birthday = p.Birthday,
                    PlayerExperiences = p.PlayerExperiences,
                    Nomination = p.Nomination,
                    ClubName = p.FootballClub.ClubName
                })
                .ToListAsync();
        }

        public IQueryable<FootballPlayer> GetAll()
        {
            return _context.FootballPlayers.AsQueryable();
        }

        public async Task<FootballPlayer> GetById(string id)
        {
            return await _context.FootballPlayers
                .Include(p => p.FootballClub)
                .FirstOrDefaultAsync(p => p.FootballPlayerId == id);
        }

    }
}
