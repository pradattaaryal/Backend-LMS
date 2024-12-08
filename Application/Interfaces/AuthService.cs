using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        public AuthService(IAuthRepository _authRepository, IMapper _mapper)
        {
            authRepository = _authRepository;
            mapper = _mapper;
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            var UserIntity = mapper.Map<User>(user);
            return await authRepository.CreateUserrepo(UserIntity);
        }

        public async Task<UserDto> GetUserByName(string username)
        {
            var UserIntity=await authRepository.GetUserByNamerepo(username);
            return mapper.Map<UserDto>(UserIntity);
        }
    }
}
