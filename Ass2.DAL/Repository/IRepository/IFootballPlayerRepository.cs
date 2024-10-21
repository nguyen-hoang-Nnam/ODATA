﻿using Ass2.Model.Models;
using Ass2.Model.Models.DTO.FootballPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.DAL.Repository.IRepository
{
    public interface IFootballPlayerRepository : IGenericRepository<FootballPlayer>
    {
        Task<List<FootballPlayerDTO>> GetAllPlayersWithClubAsync(string searchTerm);
        IQueryable<FootballPlayer> GetAll();
        Task<FootballPlayer> GetById(string id);
    }
}