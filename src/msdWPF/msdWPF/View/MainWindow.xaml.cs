using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using msdWPF.Model;
using msdWPF.View;
using msdWPF.ViewModel;

namespace msdWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

        }

        public void closeLoginWindow()
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _viewModel = FindResource("mainWindowViewModel") as MainWindowViewModel;
                _viewModel.setLoginModel(new LoginModel());
            }
        }

        private void LoginLogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Password = "";
            _viewModel.LoginLogoutProcess();
            this.UsernameTextBox.Focus();
        }

        private void LoginButton_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.LoginProcess(UsernameTextBox.Text, PasswordTextBox.Password);
            this.StudentPanel.FirstNameBox.Focus();
        }

        private void CancelButton_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelLoginProcess();
        }

        private void PasswordTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginButton_onClick(null, null);
            }
        }
    }
}
