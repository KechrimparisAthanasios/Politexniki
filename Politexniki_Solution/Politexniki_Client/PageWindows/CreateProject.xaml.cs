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
        ModelView.ProjectView _projectModelView;
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
                for (int i = 0; i < ElementNumber; i++)
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
                ownerInfoPanel.Children.Remove(StackPanelowner[ElementNumber - 1]);
                if (ElementNumber == 0)
                {
                    ElementNumber = 0;
                }
                else
                {
                    ElementNumber = ElementNumber - 1;
                }
            }
            catch (Exception ex)
            {
                MainWindowModel.Instance.MessageStatus = ex.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
        }

        private int ElementNumber = 0;
        StackPanel[] StackPanelowner = new StackPanel[1000];
        StackPanel StackPanellbl;
        StackPanel StackPanelstxt;

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
                StackPanelowner[ElementNumber] = new StackPanel();
                StackPanelowner[ElementNumber].Orientation = Orientation.Horizontal;

                StackPanellbl = new StackPanel();
                StackPanelstxt = new StackPanel();
                lblFullName[ElementNumber] = new Label
                {
                    Content = "Ονοματεπώνυμο",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblAFM[ElementNumber] = new Label
                {
                    Content = "ΑΦΜ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblADT[ElementNumber] = new Label
                {
                    Content = "Α.Δ.Τ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblDOY[ElementNumber] = new Label
                {
                    Content = "Δ.Ο.Υ",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };

                lblPercentage[ElementNumber] = new Label
                {
                    Content = "Έιδος και ποσοστό",
                    Height = 25,
                    FontWeight = FontWeights.Bold
                };



                OwnerFullNameTextBox[ElementNumber] = new TextBox
                {
                    Name = "FullNametxt" + ElementNumber
                    //Binding myBinding = new Binding("OwnerFullName");
                    //textBox.SetBinding(TextBox.TextProperty, myBinding);
                };
                Label lbl = new Label();
                OwnerAFMTextBox[ElementNumber] = new TextBox
                {
                    Name = "AFMtxt" + ElementNumber
                };
                Label lbl1 = new Label();
                OwnerADTTextBox[ElementNumber] = new TextBox
                {
                    Name = "ADTtxt" + ElementNumber
                };
                Label lbl2 = new Label();
                OwnerDOYTextBox[ElementNumber] = new TextBox
                {
                    Name = "DOYtxt" + ElementNumber
                };
                Label lbl3 = new Label();
                OwnerPercentageTextBox[ElementNumber] = new TextBox
                {
                    Name = "Percentagetxt" + ElementNumber
                };
                Label lbl4 = new Label();

                Label lbl5 = new Label();
                Label lbl6 = new Label();
                Label lbl7 = new Label();
                Label lbl8 = new Label();
                StackPanellbl.Children.Add(lblFullName[ElementNumber]);
                StackPanellbl.Children.Add(lbl5);
                StackPanellbl.Children.Add(lblAFM[ElementNumber]);
                StackPanellbl.Children.Add(lbl6);
                StackPanellbl.Children.Add(lblADT[ElementNumber]);
                StackPanellbl.Children.Add(lbl7);
                StackPanellbl.Children.Add(lblDOY[ElementNumber]);
                StackPanellbl.Children.Add(lbl8);
                StackPanellbl.Children.Add(lblPercentage[ElementNumber]);

                StackPanelstxt.Children.Add(OwnerFullNameTextBox[ElementNumber]);
                StackPanelstxt.Children.Add(lbl);
                StackPanelstxt.Children.Add(OwnerAFMTextBox[ElementNumber]);
                StackPanelstxt.Children.Add(lbl1);
                StackPanelstxt.Children.Add(OwnerADTTextBox[ElementNumber]);
                StackPanelstxt.Children.Add(lbl2);
                StackPanelstxt.Children.Add(OwnerDOYTextBox[ElementNumber]);
                StackPanelstxt.Children.Add(lbl3);
                StackPanelstxt.Children.Add(OwnerPercentageTextBox[ElementNumber]);
                StackPanelstxt.Children.Add(lbl4);

                StackPanelowner[ElementNumber].Children.Add(StackPanellbl);
                StackPanelowner[ElementNumber].Children.Add(StackPanelstxt);

                ownerInfoPanel.Children.Add(StackPanelowner[ElementNumber]);

                ElementNumber++;
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
