using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model.Project
{
    public class ProjectPlace
    {
        #region Properties

        public string City { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string OT { get; set; }
        public string Municipallity { get; set; }

        #endregion

        #region Constructor

        public ProjectPlace(){}

        #endregion
    }
}
