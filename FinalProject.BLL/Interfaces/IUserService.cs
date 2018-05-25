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
        ClientProfileDto GetUser(string userId);
        List<ClientProfileDto> GetAllUsers();
        void UpdateUser(ClientProfileDto clientProfileDto);
        void AddPost(PostDto postDto);
        void AddMessage(MessageDto messageDto);
        List<MessageDto> GetMessages(string u1Id, string u2Id);
    }
}
