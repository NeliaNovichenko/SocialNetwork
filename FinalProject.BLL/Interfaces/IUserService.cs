using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BLL.DTO;
using FinalProject.BLL.Infrastructure;

namespace FinalProject.BLL.Interfaces
{
    //Через объекты данного интерфейса уровень представления будет взаимодействовать с уровнем доступа к данным. 
    //Здесь определены только три метода: Create (создание пользователей), Authenticate (аутентификация пользователей)
    //и SetInitialData (установка начальных данных в БД - админа и списка ролей).
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(ApplicationUserDTO userDto);
        Task<ClaimsIdentity> Authenticate(ApplicationUserDTO userDto);
        Task SetInitialData(ApplicationUserDTO adminDto, List<string> roles);
    }
}
