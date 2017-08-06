using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskAppBackEnd.Data.Entities;
using AskAppBackEnd.Data;
using System.Data.Entity;
using System.Security.Cryptography;
using AskAppBackEnd.CrossCutting;

namespace AskAppBackEnd.Services
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        IQueryable<UserFriend> GetUserFriends();
        IQueryable<UserLocation> GetUserLocations();
        IQueryable<UserPreference> GetUserPreferences();
        IQueryable<UserNotification> GetUserNotifications();
        IQueryable<UserResponse> GetUserResponses();

        Task<User> GetUserByEmailAsync(string username);
        Task<ResetTicket> CreateResetTicketAsync(Guid id);
        Task<ResetTicket> GetResetTicketIfTokenValidAsync(string token);
        Task MarkResetTicketAsUsed(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IUserLocationRepository _userLocationRepository;
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly IUserResponsRepository _userReponseRepository;
        private readonly IUserNotificationRepository _userNotificationRepository;
        private readonly IResetTicketRepository _resetTicketRepository;

        public UserService(IUserRepository userRepository, IUserFriendRepository userFriendRepository,
            IUserLocationRepository userLocationRepository, IUserPreferenceRepository userPreferenceRepository,
            IUserResponsRepository userReponseRepository, IUserNotificationRepository userNotificationRepository,
            IResetTicketRepository resetTicketRepository)
        {
            _userRepository = userRepository;
            _userFriendRepository = userFriendRepository;
            _userLocationRepository = userLocationRepository;
            _userPreferenceRepository = userPreferenceRepository;
            _userReponseRepository = userReponseRepository;
            _userNotificationRepository = userNotificationRepository;
            _resetTicketRepository = resetTicketRepository;
        }
        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        public IQueryable<UserFriend> GetUserFriends()
        {
            return _userFriendRepository.GetAll();
        }
        public IQueryable<UserLocation> GetUserLocations()
        {
            return _userLocationRepository.GetAll();
        }
        public IQueryable<UserPreference> GetUserPreferences()
        {
            return _userPreferenceRepository.GetAll();
        }
        public IQueryable<UserResponse> GetUserResponses()
        {
            return _userReponseRepository.GetAll();
        }
        public IQueryable<UserNotification> GetUserNotifications()
        {
            return _userNotificationRepository.GetAll();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ResetTicket> CreateResetTicketAsync(Guid userId)
        {
            string token = HashUtility.GetToken(10);
            string tokenHash = HashUtility.GetSHA1Hash(token);
            var ticket = new ResetTicket()
            {
                Id = Guid.NewGuid(),
                ExpiredDate = DateTime.Now.AddDays(1),
                TokenHash = tokenHash,
                TokenUsed = false,
                UserId = userId
            };

            _resetTicketRepository.Insert(ticket);
            await _resetTicketRepository.SaveAsync();

            //return token, not takenHash
            ticket.TokenHash = token;
            return ticket;
        }

        public async Task<ResetTicket> GetResetTicketIfTokenValidAsync(string token)
        {
            string tokenHash = HashUtility.GetSHA1Hash(token);
            var resetTicket = await _resetTicketRepository.GetAll().FirstOrDefaultAsync(t => t.TokenHash == tokenHash);

            if ((resetTicket != null) && (DateTime.Now <= resetTicket.ExpiredDate) && !resetTicket.TokenUsed)
                return resetTicket;
            else
                return null;
        }

        public async Task MarkResetTicketAsUsed(Guid id)
        {
            var ticket = _resetTicketRepository.GetById(id);
            ticket.TokenUsed = true;

            await _resetTicketRepository.SaveAsync();
        }
    }
}
