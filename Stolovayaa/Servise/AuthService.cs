using System;
using Stolovayaa.DTOs;
using Stolovayaa.Model;
using Stolovayaa.Repositories;

namespace Stolovayaa.Servise
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;// UserRepository может иметь тип IUserRepository, потому что он РЕАЛИЗУЕТ интерфейс IUserRepository. см на вкладке UserRepository
        private UserDto _currentUser;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsLoggedIn => _currentUser != null;//кто-то залогичен?
        public UserDto CurrentUser => _currentUser;//кто залогичен?

        public UserDto Login(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Login))
                throw new ArgumentException("Введите логин");

            if (string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Введите пароль");

            var user = _userRepository.GetUserByLoginAndPassword(
                loginDto.Login.Trim(),
                loginDto.Password.Trim());

            if (user == null)
                throw new UnauthorizedAccessException("Неверный логин или пароль");
            var userWithRole = _userRepository.GetUserWithRole(user.ID);

            _currentUser = new UserDto
            {
                Id = userWithRole.ID,
                Login = userWithRole.Login,
                FirstName = userWithRole.FirstName,
                LastName = userWithRole.LastName,
                RoleName = userWithRole.Roles?.Name ?? "Пользователь"
            };

            return _currentUser;
        }

        public void Register(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Login))
                throw new ArgumentException("Введите логин");

            if (string.IsNullOrWhiteSpace(registerDto.Password))
                throw new ArgumentException("Введите пароль");

            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new ArgumentException("Пароли не совпадают");

            if (registerDto.Password.Length < 4)
                throw new ArgumentException("Пароль должен содержать минимум 4 символа");

            if (string.IsNullOrWhiteSpace(registerDto.FirstName))
                throw new ArgumentException("Введите имя");

            if (string.IsNullOrWhiteSpace(registerDto.LastName))
                throw new ArgumentException("Введите фамилию");

            if (_userRepository.IsLoginExists(registerDto.Login.Trim()))
                throw new InvalidOperationException($"Пользователь с логином '{registerDto.Login}' уже существует");

            var newUser = new Users
            {
                Login = registerDto.Login.Trim(),
                Password = registerDto.Password.Trim(),
                FirstName = registerDto.FirstName.Trim(),
                LastName = registerDto.LastName.Trim(),
                RoleId = registerDto.RoleId
            };

            _userRepository.Add(newUser);
            _userRepository.SaveChanges();
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}