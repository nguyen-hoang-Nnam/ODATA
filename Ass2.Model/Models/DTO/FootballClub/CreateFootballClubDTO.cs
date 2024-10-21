using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Model.Models.DTO.FootballClub
{
    public class CreateFootballClubDTO
    {
        public string ClubName { get; set; } = null!;

        public string ClubShortDescription { get; set; } = null!;

        public string SoccerPracticeField { get; set; } = null!;

        public string Mascos { get; set; } = null!;
    }
}
