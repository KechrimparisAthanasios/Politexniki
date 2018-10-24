using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Politexniki_Client.Model.Customers;

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

        private Customer _customer;
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
                _customer = new Customer
                {
                    CustomerID = CustomerID,
                    FirstName = FirstName,
                    LastName = LastName,
                    FatherFullName = FatherFullName,
                    MotherFullName = MotherFullName,
                    Birthday = Birthday,
                    PlaceOfBirth = PlaceOfBirth,
                    Telephone = Telephone,
                    Id = Id,
                    ResidencePlace = ResidencePlace,
                    Address = Address,
                    Number = Number,
                    PostCode = PostCode,
                    SocialNumber = SocialNumber,
                    TaxPlace = TaxPlace
                };

                _isCustomerStored =  SQLite.SqLiteHandling.Instance.StoreCustomer(_customer);
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
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView","CreateCustomer()",e.Message,DateTime.Now);
            }
            return _isCustomerStored;
        }

        public void GetCustomers()
        {
            try
            {
                CustomerObservable = SQLite.SqLiteHandling.Instance.ReturnCustomer();
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "GetCustomers", e.Message, DateTime.Now);
            }
            
        }

        private ObservableCollection<CustomerModelView> _listOfCustomers;
        public void GetCustomerByIdToEdit(string customerId)
        {
            try
            {
                _listOfCustomers = new ObservableCollection<CustomerModelView>();
                _listOfCustomers = SQLite.SqLiteHandling.Instance.ReturnCustomerById(customerId);
                CustomerObservable = _listOfCustomers;
                CustomerEditObservable = _listOfCustomers;
                //Clear the view collection
                CustomerViewObservable = null;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "GetCustomerByIdToEdit", e.Message, DateTime.Now);
            }
            
        }

        public void GetCustomerByIdToView(string customerId)
        {
            try
            {
                _listOfCustomers = new ObservableCollection<CustomerModelView>();
                _listOfCustomers = SQLite.SqLiteHandling.Instance.ReturnCustomerById(customerId);
                CustomerObservable = _listOfCustomers;
                CustomerViewObservable = _listOfCustomers;
                //Clear the edit Collection
                CustomerEditObservable = null;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "GetCustomerByIdToView", e.Message, DateTime.Now);
            }
           
        }

        public void DeleteSelectedCustomer(string selectedCustomerId)
        {
            try
            {
                string status = SQLite.SqLiteHandling.Instance.DeleteSelectedCustomer(selectedCustomerId);
                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = status;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "DeleteSelectedCustomer()", e.Message, DateTime.Now);
            }
        }

        public void EditCustomer(string customerId)
        {
            _customer = new Customer();
            try
            {
                var editedCustomer = CustomerEditObservable.Where(x => x.CustomerID == customerId).ToList();
                foreach (var editedCust in editedCustomer)
                {
                    _customer.CustomerID = editedCust.CustomerID;
                    _customer.FirstName = editedCust.FirstName;
                    _customer.LastName = editedCust.LastName;
                    _customer.FatherFullName = editedCust.FatherFullName;
                    _customer.MotherFullName = editedCust.MotherFullName;
                    _customer.Birthday = editedCust.Birthday;
                    _customer.PlaceOfBirth = editedCust.PlaceOfBirth;
                    _customer.Telephone = editedCust.Telephone;
                    _customer.Id = editedCust.Id;
                    _customer.ResidencePlace = editedCust.ResidencePlace;
                    _customer.Address = editedCust.Address;
                    _customer.Number = editedCust.Number;
                    _customer.PostCode = editedCust.PostCode;
                    _customer.SocialNumber = editedCust.SocialNumber;
                    _customer.TaxPlace = editedCust.TaxPlace;
                    MainWindowModel.Instance.MessageStatus = SQLite.SqLiteHandling.Instance.UpdateCustomer(_customer);
                }
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "EditCustomer()", e.Message, DateTime.Now);
            }
        }

        public void ExportAllCustomersPdf()
        {

        }

        public void ExportSelectedCustomerPdf(string customerId)
        {
            try
            {
                _listOfCustomers = new ObservableCollection<CustomerModelView>();
                _listOfCustomers = SQLite.SqLiteHandling.Instance.ReturnCustomerById(customerId);
                PDFHandler.PdfHandling pDfHandler = new Politexniki_Client.PDFHandler.PdfHandling();
                MainWindowModel.Instance.MessageStatus = pDfHandler.ExportSelectedCustomerInPdf(_listOfCustomers);
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CustomerModelView", "ExportSelectedCustomerPdf()", e.Message, DateTime.Now);
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
