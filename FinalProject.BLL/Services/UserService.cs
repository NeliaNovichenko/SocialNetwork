using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FinalProject.BLL.DTO;
using FinalProject.BLL.Infrastructure;
using FinalProject.BLL.Interfaces;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Interfaces;
using Microsoft.AspNet.Identity;


namespace FinalProject.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; }

        public UserService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientProfile, ClientProfileDto>();
                cfg.CreateMap<ClientProfileDto, ClientProfile>();
                cfg.CreateMap<Post, PostDto>();
                cfg.CreateMap<MessageDto, Message>();
                cfg.CreateMap<Message, MessageDto>();
            });
        }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(RegistrationModelDto userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, FirstName = userDto.FirstName, LastName = userDto.LastName };
                Database.ClientProfiles.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration completed successfully", "");
            }
            else
            {
                return new OperationDetails(false, "User with such login exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(RegistrationModelDto userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(RegistrationModelDto adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public ClientProfileDto GetUser(string userId)
        {
            var clientProfile = Database.ClientProfiles.GetAll().DefaultIfEmpty().First(c => c.Id == userId);
            var userDto = Mapper.Map<ClientProfile, ClientProfileDto>(clientProfile);

            return userDto;
        }

        public List<ClientProfileDto> GetAllUsers()
        {
            var clientProfiles = Database.UserManager.Users.Select(u => u.ClientProfile).ToList();
            var users = Mapper.Map<IEnumerable<ClientProfile>, List<ClientProfileDto>>(clientProfiles);

            return users;
        }

        public void UpdateUser(ClientProfileDto clientProfileDto)
        {
            var cp = Database.ClientProfiles.Find(c => c.Id == clientProfileDto.Id).DefaultIfEmpty().First();

            if (cp == null)
                return;

            cp.FirstName = clientProfileDto.FirstName;
            cp.LastName = clientProfileDto.LastName;
            cp.DateOfBirth = clientProfileDto.DateOfBirth;
            cp.PhoneNumber = clientProfileDto.PhoneNumber;
            cp.Gender = clientProfileDto.Gender;
            foreach (var postDto in clientProfileDto.UserPosts)
            {
                var post = new Post()
                {
                    Text = postDto.Text,
                    PostDate = postDto.PostDate,
                    ApplicationUser = Database.UserManager.FindById(postDto.ApplicationUserId)
                };
                cp.UserPosts.Add(post);
            }

            //cp.UserPosts = Mapper.Map<IEnumerable<PostDto>, IEnumerable<Post>>(clientProfileDto.UserPosts).ToList();
            foreach (var frientDto in clientProfileDto.Friends)
            {
                var cpFriend = Database.ClientProfiles.Find(c => c.Id == frientDto.Id).DefaultIfEmpty().First();
                cp.Friends.Add(cpFriend);
            }

            Database.ClientProfiles.Update(cp);
            Database.Save();
        }
        public void AddPost(PostDto postDto)
        {
            var user = Database.UserManager.Users.DefaultIfEmpty().First(u => u.Id == postDto.ApplicationUserId);
            var post = new Post()
            {
                //PostId = postDto.PostId,
                PostDate = postDto.PostDate,
                Text = postDto.Text,
                ApplicationUser = user
            };
            user.ClientProfile.UserPosts.Add(post);
            Database.ClientProfiles.Update(user.ClientProfile);
        }
        public void AddMessage(MessageDto messageDto)
        {
            var message = Mapper.Map<MessageDto, Message>(messageDto);
            Database.Messages.Create(message);
        }
        public List<MessageDto> GetMessages(string u1Id, string u2Id)
        {
            var messages = Database.Messages.Find(m => m.Users.Find(u => u.Id == u1Id) != null &&
                                                       m.Users.Find(u => u.Id == u2Id) != null);
            var messageDtos = Mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
            return messageDtos.ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
