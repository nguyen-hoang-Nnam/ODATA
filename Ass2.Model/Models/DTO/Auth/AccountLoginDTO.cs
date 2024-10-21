using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Model.Models.DTO.Auth
{
    public class AccountLoginDTO
    {
        public string Password { get; set; } = null!;

        public string? EmailAddress { get; set; }
    }
}
