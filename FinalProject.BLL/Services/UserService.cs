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
    //С помощью объекта IUnitOfWork сервис будет взаимодействовать с базой данных.
    public class UserService : IUserService
    {
        public UserService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientProfile, UserDto>();
                cfg.CreateMap<UserDto, ClientProfile>();
                cfg.CreateMap<Post, PostDto>();
                //cfg.CreateMap<ChatDto, Chat>();
                //cfg.CreateMap<MessageDto, Message>();
            });

            //Mapper.Initialize(cfg => cfg.CreateMap<CreateUserViewModel, User>()
            //    .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
            //    .ForMember("Email", opt => opt.MapFrom(src => src.Login)));
        }

        IUnitOfWork Database { get; set; }

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
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
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
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
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

        public UserDto GetUser(string userId)
        {
            var clientProfile = Database.ClientProfiles.GetAll().DefaultIfEmpty().First(c => c.Id == userId);
            var userDto = Mapper.Map<ClientProfile, UserDto>(clientProfile);

            return userDto;
        }
        public List<UserDto> GetAllUsers()
        {
            var clientProfiles = Database.UserManager.Users.Select(u => u.ClientProfile).ToList();
            var users = Mapper.Map<IEnumerable<ClientProfile>, List<UserDto>>(clientProfiles);

            return users;
        }

        public void UpdateUser(UserDto userDto)
        {
            var clientProfile = Mapper.Map<UserDto, ClientProfile>(userDto);
            var cp = Database.ClientProfiles.Find(c => c.Id == userDto.Id).DefaultIfEmpty().First();

            if (cp == null)
                return;

            cp.FirstName = userDto.FirstName;
            cp.LastName = userDto.LastName;
            cp.DateOfBirth = userDto.DateOfBirth;
            cp.PhoneNumber = userDto.PhoneNumber;
            cp.Gender = userDto.Gender;
            cp.UserPosts = Mapper.Map<IEnumerable<PostDto>, IEnumerable<Post>>(userDto.UserPosts).ToList();
            cp.Friends = Mapper.Map<IEnumerable<UserDto>, IEnumerable<ClientProfile>>(userDto.Friends).ToList();

            Database.ClientProfiles.Update(cp);
            Database.Save();
        }
        public void AddPost(PostDto postDto)
        {
            var user = Database.UserManager.Users.DefaultIfEmpty().First(u => u.Id == postDto.ApplicationUserId);
            var post = new Post()
            {
                PostId = postDto.PostId,
                PostDate = postDto.PostDate,
                Text = postDto.Text,
                ApplicationUser = user
            };
            user.ClientProfile.UserPosts.Add(post);
            Database.ClientProfiles.Update(user.ClientProfile);
        }

        //public void CreateChat(ChatDto chatDto)
        //{
        //    var chat = Mapper.Map<ChatDto, Chat>(chatDto);

        //    Database.Chats.Create(chat);
        //}
        //public void UpdateChat(ChatDto chatDto)
        //{
        //    var chat = Mapper.Map<ChatDto, Chat>(chatDto);

        //    Database.Chats.Update(chat);
        //}

        //public ChatDto GetOrCreateChat(string u1Id, string u2Id)
        //{
        //    Chat chat = null;
        //    try
        //    {
        //        chat = Database.Chats.GetAll()
        //                       .DefaultIfEmpty(new Chat()).First(c => c.ChatUserIds.Exists(u => u == u1Id) && c.ChatUserIds.Exists(u => u == u2Id));

        //    }
        //    catch (Exception e)
        //    {
        //        chat = new Chat();
        //        chat.ChatUserIds.Add(u1Id);
        //        chat.ChatUserIds.Add(u2Id);
        //        Database.Chats.Create(chat);
        //        Database.Save();
        //    }

        //    var chatDto = Mapper.Map<Chat, ChatDto>(chat);

        //    return chatDto;
        //}

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
