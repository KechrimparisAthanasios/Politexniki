using Politexniki_Client.Model;
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

namespace Politexniki_Client.PageWindows
{
    /// <summary>
    /// Interaction logic for ShowCivil.xaml
    /// </summary>
    public partial class ShowCivil : Page
    {
        ModelView.CivilModelView _civilModelView;
        public ShowCivil()
        {
            InitializeComponent();
            DataContext = _civilModelView = new ModelView.CivilModelView();
            _civilModelView.GetCivilEngineer();
            _civilModelView.IsUpdateBtnVisible = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var civilEnfineer = ((Button)sender);
                var civilId = civilEnfineer.Tag;
                _civilModelView.CivilObservable = null;
                //Clear View list of Civil
                _civilModelView.CivilViewObservable = null;
                _civilModelView.GetCivilById(int.Parse(civilId.ToString()));
                //_civilModelView.HideViewButton();
                _civilModelView.IsUpdateBtnVisible = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MainWindowModel.Instance.MessageStatus = exc.Message; 
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteWarning = MessageBox.Show("Να διαγραφεί ο μηχανικός;", "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                if (deleteWarning == MessageBoxResult.OK)
                {
                    var civilID = ((Button)sender).Tag;
                    _civilModelView.DeleteCivil(civilID.ToString());
                    _civilModelView.CivilEditObservable = null;
                }
            }
            catch (Exception exc)
            {
                MainWindowModel.Instance.MessageStatus = exc.Message;
            }
        }

        private void editSavebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var civilID = ((Button)sender).Tag;
                _civilModelView.UpdateCivilEngineer(int.Parse(civilID.ToString()));
            }
            catch (Exception exc)
            {
                MainWindowModel.Instance.MessageStatus = exc.Message;
            }
        }

        private void viewBtn_Click(object sender, RoutedEventArgs e)
        {
            var civilEnfineer = ((Button)sender);
            var civilId = civilEnfineer.Tag;
            _civilModelView.CivilObservable = null;
            //Clear the Edit List of the Civils
            _civilModelView.CivilEditObservable = null;
            _civilModelView.GetCivilViewById(int.Parse(civilId.ToString()));
            _civilModelView.IsUpdateBtnVisible = Visibility.Collapsed;
            //_civilModelView.HideViewButton();
        }

        private void pdfExportBtn_Click(object sender, RoutedEventArgs e)
        {
            _civilModelView.ExportAllCivilsInPdf();
        }

        private void civilPdfExportbtn_Click(object sender, RoutedEventArgs e)
        {
            var civilEnginnerButtonId = ((Button)sender);
            var civilId = civilEnginnerButtonId.Tag;

            _civilModelView.ExportSelectedCivilInfoInPdf(int.Parse(civilId.ToString()));
        }
    }
}
