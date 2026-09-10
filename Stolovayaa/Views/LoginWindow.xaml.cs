using System.Windows;
using Stolovayaa.Model;
using Stolovayaa.Repositories;
using Stolovayaa.Servise;
using Stolovayaa.ViewModels;

namespace Stolovayaa.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            var context = new Dining_roomEntities1();
            var userRepo = new UserRepository(context);
            var authService = new AuthService(userRepo);
            _viewModel = new LoginViewModel(authService);
            DataContext = _viewModel;

            PasswordBox.PasswordChanged += (s, e) =>
            {
                _viewModel.Password = PasswordBox.Password;
            };

            RegisterPasswordBox.PasswordChanged += (s, e) =>
            {
                _viewModel.RegisterPassword = RegisterPasswordBox.Password;
            };

            RegisterConfirmPasswordBox.PasswordChanged += (s, e) =>
            {
                _viewModel.RegisterConfirmPassword = RegisterConfirmPasswordBox.Password;
            };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}