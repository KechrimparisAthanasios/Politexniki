using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model.Project
{
    public class Project
    {
        #region Properties

        public string ProjectName { get; set; }

        public ProjectPlace ProjectPlace { get; set; }

        public ObservableCollection<ProjectOwner> ListOfProjectOwners { get; set; }

        #endregion

        #region Constructor

        public Project()
        {

        }

        #endregion
    }
}
