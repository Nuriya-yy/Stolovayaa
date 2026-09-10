using System;
using System.Windows;
using System.Windows.Input;
using Stolovayaa.Commands;
using Stolovayaa.DTOs;
using Stolovayaa.Model;
using Stolovayaa.Repositories;
using Stolovayaa.Servise;
using Stolovayaa.Views;

namespace Stolovayaa.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private string _login;
        private string _password;
        private string _registerLogin;
        private string _registerPassword;
        private string _registerConfirmPassword;
        private string _registerFirstName;
        private string _registerLastName;
        private bool _isLoginMode = true;
        private string _errorMessage;

        public LoginViewModel(IAuthService authService = null)
        {
            _authService = authService;

            LoginCommand = new RelayCommand
                (_ => ExecuteLogin(), _ => CanExecuteLogin());
            RegisterCommand = new RelayCommand
                (_ => ExecuteRegister(), _ => CanExecuteRegister());
            SwitchModeCommand = new RelayCommand
                (_ => SwitchMode());
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string RegisterLogin
        {
            get => _registerLogin;
            set => SetProperty(ref _registerLogin, value);
        }

        public string RegisterPassword
        {
            get => _registerPassword;
            set => SetProperty(ref _registerPassword, value);
        }

        public string RegisterConfirmPassword
        {
            get => _registerConfirmPassword;
            set => SetProperty(ref _registerConfirmPassword, value);
        }

        public string RegisterFirstName
        {
            get => _registerFirstName;
            set => SetProperty(ref _registerFirstName, value);
        }

        public string RegisterLastName
        {
            get => _registerLastName;
            set => SetProperty(ref _registerLastName, value);
        }

        public bool IsLoginMode
        {
            get => _isLoginMode;
            set => SetProperty(ref _isLoginMode, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand SwitchModeCommand { get; }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin()
        {
            if (_authService == null)
            {
                MessageBox.Show("Тестовый режим: Вход выполнен!", "Успех");
                return;
            }

            try
            {
                ErrorMessage = string.Empty;
                var loginDto = new LoginDto { Login = Login, Password = Password };
                var user = _authService.Login(loginDto);

                MessageBox.Show($"Добро пожаловать, {user.FullName}!", "Успех");

                if (user.RoleName == "Учитель")
                {
                    OpenOrderWindow();
                }
                else if (user.RoleName == "Работник")
                {
                    OpenKitchenWindow();
                }
                else if (user.RoleName == "Админ")
                {
                    OpenAdminWindow();
                }
                else
                {
                    MessageBox.Show($"Неизвестная роль: '{user.RoleName}'", "Ошибка");
                    return;
                }

                foreach (Window w in Application.Current.Windows)
                {
                    if (w is LoginWindow)
                    {
                        w.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(RegisterLogin) &&
                   !string.IsNullOrWhiteSpace(RegisterPassword) &&
                   !string.IsNullOrWhiteSpace(RegisterConfirmPassword) &&
                   !string.IsNullOrWhiteSpace(RegisterFirstName) &&
                   !string.IsNullOrWhiteSpace(RegisterLastName);
        }

        private void ExecuteRegister()
        {
            if (_authService == null)
            {
                MessageBox.Show("Тестовый режим: Регистрация выполнена!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                IsLoginMode = true;
                return;
            }

            try
            {
                ErrorMessage = string.Empty;
                var registerDto = new RegisterDto
                {
                    Login = RegisterLogin,
                    Password = RegisterPassword,
                    ConfirmPassword = RegisterConfirmPassword,
                    FirstName = RegisterFirstName,
                    LastName = RegisterLastName
                };

                _authService.Register(registerDto);

                MessageBox.Show("Регистрация успешна!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);

                IsLoginMode = true;
                RegisterLogin = string.Empty;
                RegisterPassword = string.Empty;
                RegisterConfirmPassword = string.Empty;
                RegisterFirstName = string.Empty;
                RegisterLastName = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void SwitchMode()
        {
            IsLoginMode = !IsLoginMode;
            ErrorMessage = string.Empty;

            if (IsLoginMode)
            {
                RegisterLogin = string.Empty;
                RegisterPassword = string.Empty;
                RegisterConfirmPassword = string.Empty;
                RegisterFirstName = string.Empty;
                RegisterLastName = string.Empty;
            }
            else
            {
                Login = string.Empty;
                Password = string.Empty;
            }
        }

        private void OpenOrderWindow()
        {
            var context = new Dining_roomEntities1();
            var schoolboyRepo = new SchoolboyRepository(context);
            var menuRepo = new DaylyMenuRepository(context);
            var orderRepo = new OrderRepository(context);

            var orderService = new OrderService(schoolboyRepo, menuRepo, orderRepo);
            var vm = new OrderViewModel(orderService);

            var window = new OrderWindow();
            window.DataContext = vm;
            window.Show();
        }

        private void OpenKitchenWindow()
        {
            var window = new KitchenWindow();
            window.Show();
        }

        private void OpenAdminWindow()
        {
            var window = new AdminWindow();
            window.Show();
        }
    }
}