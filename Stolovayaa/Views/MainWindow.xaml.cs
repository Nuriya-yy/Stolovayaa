using System.Windows;
using Stolovayaa.Views;

namespace Stolovayaa.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LogoutButton_Clickkk(object sender, RoutedEventArgs e)
        {
            this.Close();
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
