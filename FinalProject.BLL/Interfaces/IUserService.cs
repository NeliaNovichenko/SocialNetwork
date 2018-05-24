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
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(RegistrationModelDto userDto);
        Task<ClaimsIdentity> Authenticate(RegistrationModelDto userDto);
        Task SetInitialData(RegistrationModelDto adminDto, List<string> roles);
        UserDto GetUser(string userId);
        List<UserDto> GetAllUsers();
        void UpdateUser(UserDto userDto);
        void AddPost(PostDto postDto);
        //ChatDto GetOrCreateChat(string u1Id, string u2Id);
        //void UpdateChat(ChatDto chatDto);
        //void CreateChat(ChatDto chatDto);
    }
}
