using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Politexniki_Client.ModelView
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        #region Singleton

        private static readonly MainWindowModel instance = new MainWindowModel();
        static MainWindowModel(){}

        private MainWindowModel()
        {
            SQLite.SqLiteHandling.Instance.InitSqLite();
            InitUnderline();
        }

        public static MainWindowModel Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Properties

        private string _messageStatus;
        public string MessageStatus
        {
            get { return _messageStatus; }
            set { _messageStatus = value; NotifyPropertyChanged("MessageStatus"); }
        }

        private Visibility _isLogoVisible;
        public Visibility IsLogoVisible
        {
            get { return _isLogoVisible; }
            set { _isLogoVisible = value; NotifyPropertyChanged("IsLogoVisible"); }
        }

        private Visibility _isSuccessVisible;
        public Visibility IsSuccessVisible
        {
            get { return _isSuccessVisible; }
            set { _isSuccessVisible = value; NotifyPropertyChanged("IsSuccessVisible"); }
        }

        private Visibility _isFailVisible;
        public Visibility IsFailVisible
        {
            get { return _isFailVisible; }
            set { _isFailVisible = value; NotifyPropertyChanged("IsFailVisible"); }
        }

        #region UnderLine Properties

        private Visibility _avatarUnderline;
        public Visibility AvatarUnderline
        {
            get { return _avatarUnderline; }
            set { _avatarUnderline = value; NotifyPropertyChanged("AvatarUnderline"); }
        }

        private Visibility _settingUnderline;
        public Visibility SettingUnderline
        {
            get { return _settingUnderline; }
            set { _settingUnderline = value; NotifyPropertyChanged("SettingUnderline"); }
        }

        private Visibility _homeUnderline;
        public Visibility HomeUnderline
        {
            get { return _homeUnderline; }
            set { _homeUnderline = value; NotifyPropertyChanged("HomeUnderline"); }
        }

        private Visibility _createCivilUnderline;
        public Visibility CreateCivilUnderline
        {
            get { return _createCivilUnderline; }
            set { _createCivilUnderline = value; NotifyPropertyChanged("CreateCivilUnderline"); }
        }

        private Visibility _showCivilUnderline;
        public Visibility ShowCivilUnderline
        {
            get { return _showCivilUnderline; }
            set { _showCivilUnderline = value; NotifyPropertyChanged("ShowCivilUnderline"); }
        }

        private Visibility _createCustomerUnderline;
        public Visibility CreateCustomerUnderline
        {
            get { return _createCustomerUnderline; }
            set { _createCustomerUnderline = value; NotifyPropertyChanged("CreateCustomerUnderline"); }
        }

        private Visibility _showCustomerUnderline;
        public Visibility ShowCustomerUnderline
        {
            get { return _showCustomerUnderline; }
            set { _showCustomerUnderline = value; NotifyPropertyChanged("ShowCustomerUnderline"); }
        }

        private Visibility _createProjectUnderline;
        public Visibility CreateProjectUnderline
        {
            get { return _createProjectUnderline; }
            set { _createProjectUnderline = value; NotifyPropertyChanged("CreateProjectUnderline"); }
        }

        private Visibility _showProjectUnderline;
        public Visibility ShowProjectUnderline
        {
            get { return _showProjectUnderline; }
            set { _showProjectUnderline = value; NotifyPropertyChanged("ShowProjectUnderline"); }
        }

        private Visibility _createApplicationUnderline;
        public Visibility CreateApplicationUnderline
        {
            get { return _createApplicationUnderline; }
            set { _createApplicationUnderline = value; NotifyPropertyChanged("CreateApplicationUnderline"); }
        }

        #endregion

        #endregion

        #region Methods

        public void ClearContent()
        {
            //IsLogoVisible = Visibility.Collapsed;
            MessageStatus = "";
            IsSuccessVisible = Visibility.Collapsed;
            IsFailVisible = Visibility.Collapsed;
        }

        public void HideLogo()
        {
            IsLogoVisible = Visibility.Collapsed;
        }

        public void DisplayHome()
        {
            MessageStatus = "";
            IsLogoVisible = Visibility.Visible;
        }

        /// <summary>
        /// Collapsed the underline color of each button in the Main Window
        /// </summary>
        public void InitUnderline()
        {
            SettingUnderline = Visibility.Collapsed;
            AvatarUnderline = Visibility.Collapsed;
            HomeUnderline = Visibility.Collapsed;
            CreateCivilUnderline = Visibility.Collapsed;
            ShowCivilUnderline = Visibility.Collapsed;
            CreateCustomerUnderline = Visibility.Collapsed;
            ShowCustomerUnderline = Visibility.Collapsed;
            CreateProjectUnderline = Visibility.Collapsed;
            ShowProjectUnderline = Visibility.Collapsed;
            CreateApplicationUnderline = Visibility.Collapsed;
        }

        #endregion

        #region INotify Event

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
