using Ass2.BLL.Service.IService;
using Ass2.Model.Models.DTO.FootballPlayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Ass2.API.Controllers
{
    [Route("odata/[controller]")]
    //[ApiController]
    public class FootballPlayerController : ODataController
    {
        private readonly IFootballPlayerService _playerService;

        public FootballPlayerController(IFootballPlayerService playerService)
        {
            _playerService = playerService;
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<FootballPlayerDTO> GetFootballPlayers()
        {
            return _playerService.GetAllPlayersAsQueryable();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlayers(string? searchTerm)
        {
            var players = await _playerService.GetAllPlayersWithClubAsync(searchTerm ?? string.Empty);
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(string id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost("create")]
        //[Authorize(Roles = "System Admin")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreateFootballPlayerDTO playerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _playerService.AddPlayerAsync(playerDTO);
            return response.IsSucceed ? CreatedAtAction(nameof(GetPlayerById), new { id = playerDTO.FullName }, response)
                                       : BadRequest(response.Message);
        }

        [HttpPut("update/{id}")]
        //[Authorize(Roles = "System Admin")]
        public async Task<IActionResult> UpdatePlayer(string id, [FromBody] UpdateFootballPlayerDTO playerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _playerService.UpdatePlayerAsync(id, playerDTO);
            if (response.IsSucceed)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "System Admin")]
        public async Task<IActionResult> DeletePlayer(string id)
        {
            var response = await _playerService.DeletePlayerAsync(id);
            if (response.IsSucceed)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
