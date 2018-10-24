using Politexniki_Client.Model.Project;
using Politexniki_Client.PageWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Politexniki_Client.ModelView
{
    class ProjectView
    {
        #region Properties

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; NotifyPropertyChanged("ProjectName"); }
        }

        #region Project Place Properties

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; NotifyPropertyChanged("City"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged("Address"); }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set { _area = value; NotifyPropertyChanged("Area"); }
        }

        private string _ot;
        public string OT
        {
            get { return _ot; }
            set { _ot = value; NotifyPropertyChanged("OT"); }
        }

        private string _municipallity;
        public string Municipallity
        {
            get { return _municipallity; }
            set { _municipallity = value; NotifyPropertyChanged("Municipallity"); }
        }

        #endregion

        #region Project Owner Properties

        public ObservableCollection<ProjectOwner> ListOfProjectOwnersObservable { get; set; }
        //private string _ownerFullName;
        //public string OwnerFullName
        //{
        //    get { return _ownerFullName; }
        //    set { _ownerFullName = value; NotifyPropertyChanged("OwnerFullName"); }
        //}

        #endregion

        #endregion

        #region Constructor

        public ProjectView()
        {
            ListOfProjectOwnersObservable = new ObservableCollection<ProjectOwner>();
        }

        #endregion

        #region Methods

        public void CreateProject()
        {
            try
            {
                Project project = new Project
                {
                    ProjectName = ProjectName,
                    ListOfProjectOwners = ListOfProjectOwnersObservable
                };

                SQLite.SqLiteHandling.Instance.CreateSqLiteDbProject(ProjectName);
                SQLite.SqLiteHandling.Instance.CreateProjectTable(project);
            }
            catch (Exception w)
            {
                MainWindowModel.Instance.MessageStatus = w.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("ProjectView", "CreateProject",w.Message,DateTime.Now);
            }
        }

        #endregion

        #region INotifyEvent

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
