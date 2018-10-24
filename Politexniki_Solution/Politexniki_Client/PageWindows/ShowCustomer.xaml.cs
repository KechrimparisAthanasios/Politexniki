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
    /// Interaction logic for ShowClient.xaml
    /// </summary>
    public partial class ShowCustomer : Page
    {
        readonly ModelView.CustomerModelView _customerModelView;
        public ShowCustomer()
        {
            InitializeComponent();
            DataContext = _customerModelView = new ModelView.CustomerModelView();
            _customerModelView.GetCustomers();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteWarning = MessageBox.Show("Να διαγραφεί ο πελάτης;", "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                if (deleteWarning == MessageBoxResult.OK)
                {
                    var selectedCustomer = ((Button)sender);
                    string selectedCustomerId = selectedCustomer.Tag.ToString();
                    _customerModelView.DeleteSelectedCustomer(selectedCustomerId);
                    _customerModelView.GetCustomers();
                }
            }
            catch (Exception exce)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exce.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer","DeleteBtn_Click",exce.Message,DateTime.Now);
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = ((Button)sender);

                var customerId = customer.Tag;
                _customerModelView.CustomerObservable = null;
                _customerModelView.GetCustomerByIdToEdit(customerId.ToString());
            }
            catch (Exception exception)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exception.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer", "editBtn_Click", exception.Message, DateTime.Now);
            }
        }

        private void viewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = ((Button)sender);

                var customerId = customer.Tag;
                _customerModelView.CustomerObservable = null;
                _customerModelView.GetCustomerByIdToView(customerId.ToString());
            }
            catch (Exception exception)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exception.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer", "viewBtnBtn_Click", exception.Message, DateTime.Now);
            }
        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCustomer = ((Button)sender);
                var customerId = selectedCustomer.Tag;
                _customerModelView.EditCustomer(customerId.ToString());
            }
            catch (Exception exception)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exception.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer", "btnEditClient_Click", exception.Message, DateTime.Now);
            }
        }

        private void exportCustomerPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _customerModelView.ExportAllCustomersPdf();
            }
            catch (Exception exception)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exception.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer", "exportCustomerPDF_Click", exception.Message, DateTime.Now);
            }
        }

        private void exportSelectedCustomerPDFbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCustomer = ((Button)sender);
                var customerId = selectedCustomer.Tag;
                _customerModelView.ExportSelectedCustomerPdf(customerId.ToString());
            }
            catch (Exception exception)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = exception.Message;
                Log_Handler.LogHandling.Instance.StoreLog("ShowCustomer", "exportSelectedCustomerPDFbtn_Click", exception.Message, DateTime.Now);
            }
        }
    }
}
