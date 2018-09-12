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
    /// Interaction logic for CreateClientPage.xaml
    /// </summary>
    public partial class CreateCustomerPage : Page
    {
        ModelView.CustomerModelView _clientModelView;
        public CreateCustomerPage()
        {
            InitializeComponent();
            DataContext = _clientModelView = new ModelView.CustomerModelView();
        }

        private void btnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            _clientModelView.CreateCustomer();
        }
    }
}
