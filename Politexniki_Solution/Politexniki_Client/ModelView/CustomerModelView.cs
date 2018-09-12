using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Politexniki_Client.Model;

namespace Politexniki_Client.ModelView
{
    public  class CustomerModelView : INotifyPropertyChanged
    {

        #region Properties

        private string _customerID;
        public string CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; NotifyPropertyChanged("CustomerID"); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; NotifyPropertyChanged("FirstName"); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; NotifyPropertyChanged("LastName"); }
        }

        private string _fatherFullName;
        public string FatherFullName
        {
            get { return _fatherFullName; }
            set { _fatherFullName = value; NotifyPropertyChanged("FatherFullName"); }
        }

        private string _motherFullName;
        public string MotherFullName
        {
            get { return _motherFullName; }
            set { _motherFullName = value; NotifyPropertyChanged("MotherFullName"); }
        }

        private string _birthday;
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; NotifyPropertyChanged("Birthday"); }
        }

        private string _placeOfBirth;
        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set { _placeOfBirth = value; NotifyPropertyChanged("PlaceOfBirth"); }
        }

        private string _telephone;
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; NotifyPropertyChanged("Telephone"); }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }

        private string _residencePlace;
        public string ResidencePlace
        {
            get { return _residencePlace; }
            set { _residencePlace = value; NotifyPropertyChanged("ResidencePlace"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged("Address"); }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }

        private string _postCode;
        public string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; NotifyPropertyChanged("PostCode"); }
        }

        private string _socialNumber;
        public string SocialNumber
        {
            get { return _socialNumber; }
            set { _socialNumber = value; NotifyPropertyChanged("SocialNumber"); }
        }

        private string _taxPlace;
        public string TaxPlace
        {
            get { return _taxPlace; }
            set { _taxPlace = value; NotifyPropertyChanged("TaxPlace"); }
        }

        private ObservableCollection<CustomerModelView> _customerObservable;
        public ObservableCollection<CustomerModelView> CustomerObservable
        {
            get { return _customerObservable; }
            set { _customerObservable = value; NotifyPropertyChanged("CustomerObservable"); }
        }

        private ObservableCollection<CustomerModelView> _customerEditObservable;
        public ObservableCollection<CustomerModelView> CustomerEditObservable
        {
            get { return _customerEditObservable; }
            set { _customerEditObservable = value; NotifyPropertyChanged("CustomerEditObservable"); }
        }

        private ObservableCollection<CustomerModelView> _customerViewObservable;
        public ObservableCollection<CustomerModelView> CustomerViewObservable
        {
            get { return _customerViewObservable; }
            set { _customerViewObservable = value; NotifyPropertyChanged("CustomerViewObservable"); }
        }

        #endregion

        #region Methods

        Customer customer;
        private bool _isCustomerStored;
        /// <summary>
        /// Create the new customer in the Model and
        /// store in the SQLite Database
        /// </summary>
        /// <returns></returns>
        public bool CreateCustomer()
        {
            try
            {
                customer = new Customer();
                customer.CustomerID = CustomerID;
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.FatherFullName = FatherFullName;
                customer.MotherFullName = MotherFullName;
                customer.Birthday = Birthday;
                customer.PlaceOfBirth = PlaceOfBirth;
                customer.Telephone = Telephone;
                customer.Id = Id;
                customer.ResidencePlace = ResidencePlace;
                customer.Address = Address;
                customer.Number = Number;
                customer.PostCode = PostCode;
                customer.SocialNumber = SocialNumber;
                customer.TaxPlace = TaxPlace;

               _isCustomerStored =  SQLite.SQLiteHandling.Instance.StoreCustomer(customer);
                if (!_isCustomerStored)
                {
                    MainWindowModel.Instance.MessageStatus = "Δεν ήταν δυνατή η ολοκλήρωση αποθήκευσης. Δοκίμασε ξανά.";
                    MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                }
                else
                {
                    MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                    MainWindowModel.Instance.MessageStatus = "Ο πελάτης δημιουργήθηκε!!!";
                    CustomerID = "";
                    FirstName = "";
                    LastName = "";
                    FatherFullName = "";
                    MotherFullName = "";
                    Birthday = "";
                    PlaceOfBirth = "";
                    Telephone = "";
                    Id = "";
                    ResidencePlace = "";
                    Address = "";
                    Number = 0;
                    PostCode = "";
                    SocialNumber = "";
                    TaxPlace = "";
                }
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
            return _isCustomerStored;
        }

        public void GetCustomers()
        {
            CustomerObservable = SQLite.SQLiteHandling.Instance.ReturnCustomer();
        }

        private ObservableCollection<CustomerModelView> _listOfCustomers;
        public void GetCustomerByIdToEdit(string customerId)
        {
            _listOfCustomers = new ObservableCollection<CustomerModelView>();
            _listOfCustomers = SQLite.SQLiteHandling.Instance.ReturnCustomerById(customerId);
            CustomerObservable = _listOfCustomers;
            CustomerEditObservable = _listOfCustomers;
            //Clear the view collection
            CustomerViewObservable = null;
        }

        public void GetCustomerByIdToView(string customerId)
        {
            _listOfCustomers = new ObservableCollection<CustomerModelView>();
            _listOfCustomers = SQLite.SQLiteHandling.Instance.ReturnCustomerById(customerId);
            CustomerObservable = _listOfCustomers;
            CustomerViewObservable = _listOfCustomers;
            //Clear the edit Collection
            CustomerEditObservable = null;
        }

        public void DeleteSelectedCustomer(string selectedCustomerId)
        {
            try
            {
                string status = SQLite.SQLiteHandling.Instance.DeleteSelectedCustomer(selectedCustomerId);
                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = status;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = e.Message;
            }
        }

        public void EditCustomer(string customerId)
        {
            customer = new Customer();
            try
            {
                var editedCustomer = CustomerEditObservable.Where(x => x.CustomerID == customerId).ToList();
                foreach (var editedCust in editedCustomer)
                {
                    customer.CustomerID = editedCust.CustomerID;
                    customer.FirstName = editedCust.FirstName;
                    customer.LastName = editedCust.LastName;
                    customer.FatherFullName = editedCust.FatherFullName;
                    customer.MotherFullName = editedCust.MotherFullName;
                    customer.Birthday = editedCust.Birthday;
                    customer.PlaceOfBirth = editedCust.PlaceOfBirth;
                    customer.Telephone = editedCust.Telephone;
                    customer.Id = editedCust.Id;
                    customer.ResidencePlace = editedCust.ResidencePlace;
                    customer.Address = editedCust.Address;
                    customer.Number = editedCust.Number;
                    customer.PostCode = editedCust.PostCode;
                    customer.SocialNumber = editedCust.SocialNumber;
                    customer.TaxPlace = editedCust.TaxPlace;
                    MainWindowModel.Instance.MessageStatus = SQLite.SQLiteHandling.Instance.UpdateCustomer(customer);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ExportAllCustomersPDF()
        {

        }

        public void ExportSelectedCustomerPDF(string customerId)
        {

        }

        #endregion

        #region INotifyEvent

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
