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
    /// Interaction logic for CreateCivil.xaml
    /// </summary>
    public partial class CreateCivil : Page
    {
        private ModelView.CivilModelView _CivilModelView;
        public CreateCivil()
        {
            InitializeComponent();
            DataContext = _CivilModelView = new ModelView.CivilModelView();
        }

        private void createbtn_Click(object sender, RoutedEventArgs e)
        {
            bool result = _CivilModelView.SaveCivilEngineer();
        }
    }
}
