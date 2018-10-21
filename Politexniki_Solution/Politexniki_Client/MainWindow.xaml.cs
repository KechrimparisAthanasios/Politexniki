using Politexniki_Client.ModelView;
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

namespace Politexniki_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ModelView.MainWindowModel.Instance;
            MainWindowModel.Instance.ClearContent();
        }

        #endregion

        #region Home

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            //string s = @"C:\Users\akech\source\repos\Politexniki\Politexniki_Client\Icons\Politexniki_logo.jpg";
            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(s);
            //bitmap.EndInit();
            //imgMainLogo.Source = bitmap;

            //imgMainLogo.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("../../Icons/Politexniki_logo.jpg", UriKind.Relative));
            MainWindowModel.Instance.DisplayHome();
            Main.Content = null;
            
        }

        private void homeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.HomeUnderline = Visibility.Visible;
        }

        private void homeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.HomeUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Create Civil

        private void btnCreateCivil_Click(object sender, RoutedEventArgs e)
        {
            MainWindowModel.Instance.ClearContent();
            MainWindowModel.Instance.HideLogo();
            Main.Content = new PageWindows.CreateCivil();
        }

        private void btnCreateCivil_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateCivilUnderline = Visibility.Visible;
        }

        private void btnCreateCivil_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateCivilUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Show Civil

        private void btnShowCivil_Click(object sender, RoutedEventArgs e)
        {
            MainWindowModel.Instance.ClearContent();
            MainWindowModel.Instance.HideLogo();
            Main.Content = new PageWindows.ShowCivil();
        }

        private void btnShowCivil_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowCivilUnderline = Visibility.Visible;
        }

        private void btnShowCivil_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowCivilUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Create Customer

        private void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainWindowModel.Instance.ClearContent();
            MainWindowModel.Instance.HideLogo();
            Main.Content = new PageWindows.CreateCustomerPage();
        }

        private void btnCreateCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateCustomerUnderline = Visibility.Visible;
        }

        private void btnCreateCustomer_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateCustomerUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Show Customer

        private void btnShowCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainWindowModel.Instance.ClearContent();
            MainWindowModel.Instance.HideLogo();
            Main.Content = new PageWindows.ShowCustomer();
        }

        private void btnShowCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowCustomerUnderline = Visibility.Visible;
        }

        private void btnShowCustomer_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowCustomerUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Create Project

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            MainWindowModel.Instance.ClearContent();
            MainWindowModel.Instance.HideLogo();
            Main.Content = new PageWindows.CreateProject();
        }

        private void btnCreateProject_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateProjectUnderline = Visibility.Visible;
        }

        private void btnCreateProject_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateProjectUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Show Projects

        private void btnShowProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowProject_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowProjectUnderline = Visibility.Visible;
        }

        private void btnShowProject_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.ShowProjectUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Create Application

        private void btnCreateApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateApplication_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateApplicationUnderline = Visibility.Visible;
        }

        private void btnCreateApplication_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.CreateApplicationUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Setting

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //avatarShowlbl.Visibility = Visibility.Visible;
            MainWindowModel.Instance.SettingUnderline = Visibility.Visible;
        }

        private void settingBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //avatarShowlbl.Visibility = Visibility.Collapsed;
            MainWindowModel.Instance.SettingUnderline = Visibility.Collapsed;
        }

        #endregion

        #region Avatar

        private void btnAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void avatarBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.AvatarUnderline = Visibility.Visible;
        }

        private void avatarBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindowModel.Instance.AvatarUnderline = Visibility.Hidden;
        }















        #endregion

    }
}



//bool isForMinimize = true;
//private void maximize_Click(object sender, RoutedEventArgs e)
//{
//    if (isForMinimize)
//    {
//        this.WindowState = WindowState.Normal;
//        isForMinimize = false;
//    }
//    else
//    {
//        this.WindowState = WindowState.Maximized;
//        isForMinimize = true;
//    } 
//}