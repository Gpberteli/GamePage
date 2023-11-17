using AutoMapper;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;

namespace JogoRpg.Services.Services
{
    public class UserService : BaseService<UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserWithCharacters(long userId)
        {
            var userEntity = await _userRepository.GetUserWithCharacters(userId);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public override async Task<UserDTO> Remove(long userId)
        {
            var userDto = await _userRepository.Get(userId);
            if (userDto != null)
            {
                return _mapper.Map<UserDTO>(await _userRepository.Remove(userDto));
            }
            return null;
        }
    }
}