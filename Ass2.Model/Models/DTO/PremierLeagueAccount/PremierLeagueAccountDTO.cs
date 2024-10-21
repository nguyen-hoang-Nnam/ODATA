using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Model.Models.DTO.PremierLeagueAccount
{
    public class PremierLeagueAccountDTO
    {
        public int AccId { get; set; }

        public string Password { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public string Description { get; set; } = null!;

        public int? Role { get; set; }
    }
}
