using System.Windows;
using System.Windows.Controls;
using Stolovayaa.ViewModels;


namespace Stolovayaa.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            // Создаем ViewModel без authService для теста
            _viewModel = new LoginViewModel(null);  // ← ПЕРЕДАЕМ null
            DataContext = _viewModel;

            // Привязка паролей
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}