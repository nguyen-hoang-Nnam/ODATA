using Ass2.BLL.Service.IService;
using Ass2.DAL.Repository.IRepository;
using Ass2.Model.Helper;
using Ass2.Model.Models.DTO.Auth;
using Ass2.Model.Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.BLL.Service
{
    public class AuthService : IAuthService
    {
        private readonly IPremierLeagueAccountRepository _premierLeagueAccountRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IPremierLeagueAccountRepository premierLeagueAccountRepository, JwtHelper jwtHelper)
        {
            _premierLeagueAccountRepository = premierLeagueAccountRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<ResponseDTO> LoginAsync(AccountLoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.EmailAddress) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return new ResponseDTO { IsSucceed = false, Message = "Email and password are required." };
            }

            var account = await _premierLeagueAccountRepository.GetByEmailAsync(loginDTO.EmailAddress);
            if (account == null || !VerifyPassword(loginDTO.Password, account.Password))
            {
                return new ResponseDTO { IsSucceed = false, Message = "Invalid email or password." };
            }

            var token = _jwtHelper.GenerateJwtToken(account);
            return new ResponseDTO { IsSucceed = true, Message = "Login successful.", Data = token };
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }
    }
}
