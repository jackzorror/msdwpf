using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using msdWPF.Annotations;
using msdWPF.Model;

namespace msdWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private LoginModel _loginModel;

        /// <summary>
        /// 
        /// </summary>
        public String LoginButtonLabel
        {
            get { return Login ? "Logout" : "Login";}
        }

        public string Username { get; set; }
        
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Login
        {
            get { return _login; }
            set { _login = value; NotifyPropertyChanged(""); }
        }
        private bool _login;

        public string ShowMainTabControl { get { return Login ? "Visible" : "Hidden"; } }

        public string LoginErrorMessage { get; set; }

        public bool LoginPopupIsOpen { get; set; }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// PropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        internal void LoginLogoutProcess()
        {
            if (_login)
            {
                _login = false;
            }
            else
            {
                Username = "";
                Password = "";
                LoginErrorMessage = "";
                LoginPopupIsOpen = true;
            }
            NotifyPropertyChanged("");
        }

        internal void CancelLoginProcess()
        {
            Username = "";
            Password = "";
            LoginErrorMessage = "";
            _login = false;
            LoginPopupIsOpen = false;
            NotifyPropertyChanged("");
        }

        public void LoginProcess(string Username, string Password)
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                LoginErrorMessage = "Please provide username/password;";
                NotifyPropertyChanged("");
                return;
            } else if (!_loginModel.Login(Username, Password))
            {
                LoginErrorMessage = "Invalid username/password! Please try again;";
                NotifyPropertyChanged("");
                return;
            }

            _login = true;
            LoginPopupIsOpen = false;
            NotifyPropertyChanged("");
        }

        internal void setLoginModel(LoginModel model)
        {
            _loginModel = model;
        }

    }
}
