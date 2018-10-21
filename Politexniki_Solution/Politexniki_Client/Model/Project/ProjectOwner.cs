using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model.Project
{
    public class ProjectOwner
    {
        #region Properties
        
        public string FullName { get; set; }

        public string DOY { get; set; }

        public string SocialNumber { get; set; }

        public string Percantage { get; set; }

        public string AFM { get; set; }

        #endregion

        #region Constructor

        public ProjectOwner(){}

        #endregion
    }
}
