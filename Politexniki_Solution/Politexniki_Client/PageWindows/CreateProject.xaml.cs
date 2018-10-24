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
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Page
    {
        readonly ModelView.ProjectView _projectModelView;
        public CreateProject()
        {
            InitializeComponent();
            DataContext = _projectModelView = new ModelView.ProjectView();
            //Initialize the owner textBoxes
            CreateOwnerView();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.Project.ProjectOwner projectOwner = new Model.Project.ProjectOwner();
                for (int i = 0; i < _elementNumber; i++)
                {
                    projectOwner.FullName = OwnerFullNameTextBox[i].Text;
                    projectOwner.DOY = OwnerDOYTextBox[i].Text;
                    projectOwner.SocialNumber = OwnerADTTextBox[i].Text;
                    projectOwner.Percantage = OwnerPercentageTextBox[i].Text;
                    projectOwner.AFM = OwnerAFMTextBox[i].Text;
                    _projectModelView.ListOfProjectOwnersObservable.Add(projectOwner);
                    //_projectModelView.OwnerFullName = ProjectNameTextBox[i].Text;
                }

                _projectModelView.CreateProject();
            }
            catch (Exception w)
            {
                MainWindowModel.Instance.MessageStatus = w.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CreateProject","saveBtn",w.Message,DateTime.Now);
            }

        }

        #region UI Helper

        private void addOwner_Click(object sender, RoutedEventArgs e)
        {
            CreateOwnerView();
        }

        private void removeOwnerbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ownerInfoPanel.Children.Remove(_stackPanelowner[_elementNumber - 1]);
                if (_elementNumber == 0)
                {
                    _elementNumber = 0;
                }
                else
                {
                    _elementNumber = _elementNumber - 1;
                }
            }
            catch (Exception ex)
            {
                MainWindowModel.Instance.MessageStatus = ex.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
        }

        private int _elementNumber = 0;
        private readonly StackPanel[] _stackPanelowner = new StackPanel[1000];
        StackPanel _stackPanellbl;
        StackPanel _stackPanelstxt;

        Label[] lblFullName = new Label[1000];
        Label[] lblAFM = new Label[1000];
        Label[] lblADT = new Label[1000];
        Label[] lblDOY = new Label[1000];
        Label[] lblPercentage = new Label[1000];


        TextBox[] OwnerFullNameTextBox = new TextBox[1000];
        TextBox[] OwnerAFMTextBox = new TextBox[1000];
        TextBox[] OwnerADTTextBox = new TextBox[1000];
        TextBox[] OwnerDOYTextBox = new TextBox[1000];
        TextBox[] OwnerPercentageTextBox = new TextBox[1000];

        public void CreateOwnerView()
        {
            try
            {
                _stackPanelowner[_elementNumber] = new StackPanel();
                _stackPanelowner[_elementNumber].Orientation = Orientation.Horizontal;

                _stackPanellbl = new StackPanel();
                _stackPanelstxt = new StackPanel();
                lblFullName[_elementNumber] = new Label
                {
                    Content = "Ονοματεπώνυμο",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblAFM[_elementNumber] = new Label
                {
                    Content = "ΑΦΜ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblADT[_elementNumber] = new Label
                {
                    Content = "Α.Δ.Τ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblDOY[_elementNumber] = new Label
                {
                    Content = "Δ.Ο.Υ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblPercentage[_elementNumber] = new Label
                {
                    Content = "Έιδος και ποσοστό",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };



                OwnerFullNameTextBox[_elementNumber] = new TextBox
                {
                    Name = "FullNametxt" + _elementNumber
                    //Binding myBinding = new Binding("OwnerFullName");
                    //textBox.SetBinding(TextBox.TextProperty, myBinding);
                };
                Label lbl = new Label();
                OwnerAFMTextBox[_elementNumber] = new TextBox
                {
                    Name = "AFMtxt" + _elementNumber
                };
                Label lbl1 = new Label();
                OwnerADTTextBox[_elementNumber] = new TextBox
                {
                    Name = "ADTtxt" + _elementNumber
                };
                Label lbl2 = new Label();
                OwnerDOYTextBox[_elementNumber] = new TextBox
                {
                    Name = "DOYtxt" + _elementNumber
                };
                Label lbl3 = new Label();
                OwnerPercentageTextBox[_elementNumber] = new TextBox
                {
                    Name = "Percentagetxt" + _elementNumber
                };
                Label lbl4 = new Label();

                Label lbl5 = new Label();
                Label lbl6 = new Label();
                Label lbl7 = new Label();
                Label lbl8 = new Label();
                _stackPanellbl.Children.Add(lblFullName[_elementNumber]);
                _stackPanellbl.Children.Add(lbl5);
                _stackPanellbl.Children.Add(lblAFM[_elementNumber]);
                _stackPanellbl.Children.Add(lbl6);
                _stackPanellbl.Children.Add(lblADT[_elementNumber]);
                _stackPanellbl.Children.Add(lbl7);
                _stackPanellbl.Children.Add(lblDOY[_elementNumber]);
                _stackPanellbl.Children.Add(lbl8);
                _stackPanellbl.Children.Add(lblPercentage[_elementNumber]);

                _stackPanelstxt.Children.Add(OwnerFullNameTextBox[_elementNumber]);
                _stackPanelstxt.Children.Add(lbl);
                _stackPanelstxt.Children.Add(OwnerAFMTextBox[_elementNumber]);
                _stackPanelstxt.Children.Add(lbl1);
                _stackPanelstxt.Children.Add(OwnerADTTextBox[_elementNumber]);
                _stackPanelstxt.Children.Add(lbl2);
                _stackPanelstxt.Children.Add(OwnerDOYTextBox[_elementNumber]);
                _stackPanelstxt.Children.Add(lbl3);
                _stackPanelstxt.Children.Add(OwnerPercentageTextBox[_elementNumber]);
                _stackPanelstxt.Children.Add(lbl4);

                _stackPanelowner[_elementNumber].Children.Add(_stackPanellbl);
                _stackPanelowner[_elementNumber].Children.Add(_stackPanelstxt);

                ownerInfoPanel.Children.Add(_stackPanelowner[_elementNumber]);

                _elementNumber++;
            }
            catch (Exception ex)
            {
                MainWindowModel.Instance.MessageStatus = ex.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
        }


        #endregion

        
    }
}
