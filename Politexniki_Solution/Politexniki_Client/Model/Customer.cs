using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model
{
    public class Customer
    {
        #region Properties

        public string CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FatherFullName { get; set; }

        public string MotherFullName { get; set; }

        public string Birthday { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Telephone { get; set; }

        public string Id { get; set; }

        public string ResidencePlace { get; set; }

        public string Address { get; set; }

        public int Number { get; set; }

        public string PostCode { get; set; }

        public string SocialNumber { get; set; }

        public string TaxPlace { get; set; }

        #endregion

        #region Constructor

        public Customer()
        {}

        #endregion
    }
}
