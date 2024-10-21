using Ass2.BLL.Service.IService;
using Ass2.DAL.Repository.IRepository;
using Ass2.Model.Models.DTO.FootballPlayer;
using Ass2.Model.Models.DTO.Response;
using Ass2.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ass2.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Ass2.Model.Data;

namespace Ass2.BLL.Service
{
    public class FootballPlayerService : IFootballPlayerService
    {
        private readonly IFootballPlayerRepository _playerRepository;
        private readonly EnglishPremierLeague2024DbContext _context;

        public FootballPlayerService(IFootballPlayerRepository playerRepository, EnglishPremierLeague2024DbContext context)
        {
            _playerRepository = playerRepository;
            _context = context;
        }

        public async Task<IEnumerable<FootballPlayerDTO>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(p => new FootballPlayerDTO
            {
                FootballPlayerId = p.FootballPlayerId,
                FullName = p.FullName,
                Achievements = p.Achievements,
                Birthday = p.Birthday,
                PlayerExperiences = p.PlayerExperiences,
                Nomination = p.Nomination,
                ClubName = p.FootballClub.ClubName
            }).ToList();
        }

        public IQueryable<FootballPlayerDTO> GetAllPlayersAsQueryable()
        {
            var players = _playerRepository.GetAll();
            return players.Select(p => new FootballPlayerDTO
            {
                FootballPlayerId = p.FootballPlayerId,
                FullName = p.FullName,
                Achievements = p.Achievements,
                Birthday = p.Birthday,
                PlayerExperiences = p.PlayerExperiences,
                Nomination = p.Nomination,
                ClubName = p.FootballClub.ClubName,
            }).AsQueryable();
        }

        public async Task<FootballPlayerDTO> GetPlayerByIdAsync(string id)
        {
            var player = await _playerRepository.GetById(id);
            if (player == null) return null;

            return new FootballPlayerDTO
            {
                FootballPlayerId = player.FootballPlayerId,
                FullName = player.FullName,
                Achievements = player.Achievements,
                Birthday = player.Birthday,
                PlayerExperiences = player.PlayerExperiences,
                Nomination = player.Nomination,
                ClubName = player.FootballClub.ClubName
            };
        }

        public async Task<ResponseDTO> AddPlayerAsync(CreateFootballPlayerDTO playerDTO)
        {
            var newPlayerId = await GenerateFootballPlayerIdAsync();

            var player = new FootballPlayer
            {
                FootballPlayerId = newPlayerId,
                FullName = playerDTO.FullName,
                Achievements = playerDTO.Achievements,
                Birthday = playerDTO.Birthday,
                PlayerExperiences = playerDTO.PlayerExperiences,
                Nomination = playerDTO.Nomination,
                FootballClubId = playerDTO.FootballClubId
            };

            await _playerRepository.AddAsync(player);
            await _playerRepository.SaveChangesAsync();

            return new ResponseDTO { IsSucceed = true, Message = "Player added successfully.", Data = true };
        }

        public async Task<ResponseDTO> UpdatePlayerAsync(string id, UpdateFootballPlayerDTO playerDTO)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return new ResponseDTO { IsSucceed = false, Message = "Player not found." };
            }

            player.FullName = playerDTO.FullName;
            player.Achievements = playerDTO.Achievements;
            player.Birthday = playerDTO.Birthday;
            player.PlayerExperiences = playerDTO.PlayerExperiences;
            player.Nomination = playerDTO.Nomination;
            player.FootballClubId = playerDTO.FootballClubId;

            _playerRepository.Update(player);
            await _playerRepository.SaveChangesAsync();

            return new ResponseDTO { IsSucceed = true, Message = "Player updated successfully.", Data = true };
        }

        public async Task<ResponseDTO> DeletePlayerAsync(string id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return new ResponseDTO { IsSucceed = false, Message = "Player not found." };
            }

            _playerRepository.Delete(player);
            await _playerRepository.SaveChangesAsync();

            return new ResponseDTO { IsSucceed = true, Message = "Player deleted successfully.", Data = true };
        }

        public async Task<List<FootballPlayerDTO>> GetAllPlayersWithClubAsync(string searchTerm)
        {
            return await _playerRepository.GetAllPlayersWithClubAsync(searchTerm);
        }

        

        private async Task<string> GenerateFootballPlayerIdAsync()
        {
            // Retrieve the latest player ID
            var lastPlayer = await _context.FootballPlayers
                .OrderByDescending(p => p.FootballPlayerId)
                .FirstOrDefaultAsync();

            string newId;

            if (lastPlayer != null && lastPlayer.FootballPlayerId.StartsWith("PL"))
            {
                // Extract the numeric part and increment it
                var numberPart = int.Parse(lastPlayer.FootballPlayerId.Substring(2));
                newId = $"PL{(numberPart + 1).ToString("D5")}"; // Increment the number
            }
            else
            {
                // If no players found, start from PL00960
                newId = "PL00960";
            }

            return newId;
        }

    }
}
