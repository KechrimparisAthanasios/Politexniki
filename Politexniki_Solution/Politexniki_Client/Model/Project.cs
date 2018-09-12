using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model
{
    public class Project
    {
        #region Properties

        public string ProjectName { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public string OT { get; set; }

        #endregion

        #region Constructor

        public Project()
        {

        }

        #endregion
    }
}
