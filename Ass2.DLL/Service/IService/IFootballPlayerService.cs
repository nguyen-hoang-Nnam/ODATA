using Ass2.Model.Models.DTO.FootballPlayer;
using Ass2.Model.Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.BLL.Service.IService
{
    public interface IFootballPlayerService
    {
        Task<IEnumerable<FootballPlayerDTO>> GetAllPlayersAsync();
        Task<FootballPlayerDTO> GetPlayerByIdAsync(string id);
        Task<ResponseDTO> AddPlayerAsync(CreateFootballPlayerDTO playerDTO);
        Task<ResponseDTO> UpdatePlayerAsync(string id, UpdateFootballPlayerDTO playerDTO);
        Task<ResponseDTO> DeletePlayerAsync(string id);
        Task<List<FootballPlayerDTO>> GetAllPlayersWithClubAsync(string searchTerm);
        IQueryable<FootballPlayerDTO> GetAllPlayersAsQueryable();
    }
}
