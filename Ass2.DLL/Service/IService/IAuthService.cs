using Ass2.Model.Models.DTO.Auth;
using Ass2.Model.Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.BLL.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(AccountLoginDTO loginDTO);
    }
}
