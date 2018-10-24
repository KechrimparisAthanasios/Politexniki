using Politexniki_Client.SystemMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using Politexniki_Client.Model.CivilEngineers;

namespace Politexniki_Client.ModelView
{
    public class CivilModelView : INotifyPropertyChanged
    {
        #region Properties

        private int _civilId;
        public int CivilId
        {
            get { return _civilId; }
            set { _civilId = value; NotifyPropertyChanged("CivilId"); }
        }

        private string _civilFirstName;
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        public string CivilFirstName
        {
            get { return _civilFirstName; }
            set { _civilFirstName = value; NotifyPropertyChanged("CivilFirstName"); }
        }

        private string _civilLastName;
        public string CivilLastName
        {
            get { return _civilLastName; }
            set { _civilLastName = value; NotifyPropertyChanged("CivilLastName"); }
        }

        private string _speciality;
        public string Speciality
        {
            get { return _speciality; }
            set { _speciality = value; NotifyPropertyChanged("Speciality"); }
        }

        private string _numberTee;
        public string NumberTEE
        {
            get { return _numberTee; }
            set { _numberTee = value; NotifyPropertyChanged("NumberTEE"); }
        }

        private string _civilAfm;
        public string CivilAFM
        {
            get { return _civilAfm; }
            set { _civilAfm = value; NotifyPropertyChanged("CivilAFM"); }
        }

        private string _civilDoy;
        public string CivilDOY
        {
            get { return _civilDoy; }
            set { _civilDoy = value; NotifyPropertyChanged("CivilDOY"); }
        }

        private string _civilTele;
        public string CivilTele
        {
            get { return _civilTele; }
            set { _civilTele = value; NotifyPropertyChanged("CivilTele"); }
        }

        private string _civilNumberId;
        public string CivilNumberID
        {
            get { return _civilNumberId; }
            set { _civilNumberId = value; NotifyPropertyChanged("CivilNumberID"); }
        }

        private string _nomos;
        public string Nomos
        {
            get { return _nomos; }
            set { _nomos = value; NotifyPropertyChanged("Nomos"); }
        }

        private string _civilMunicipality;
        public string CivilMunicipality
        {
            get { return _civilMunicipality; }
            set { _civilMunicipality = value; NotifyPropertyChanged("CivilMunicipality"); }
        }

        private string _placeOfHouse;
        public string PlaceOfHouse
        {
            get { return _placeOfHouse; }
            set { _placeOfHouse = value; NotifyPropertyChanged("PlaceOfHouse"); }
        }

        private string _civilAddress;
        public string CivilAddress
        {
            get { return _civilAddress; }
            set { _civilAddress = value; NotifyPropertyChanged("CivilAddress"); }
        }

        private string _civilNumber;
        public string CivilNumber
        {
            get { return _civilNumber; }
            set { _civilNumber = value; NotifyPropertyChanged("CivilNumber"); }
        }

        private string _civilPostCode;
        public string CivilPostCode
        {
            get { return _civilPostCode; }
            set { _civilPostCode = value; NotifyPropertyChanged("CivilPostCode"); }
        }

        private ObservableCollection<CivilModelView> _civilObservable;
        public ObservableCollection<CivilModelView> CivilObservable
        {
            get { return _civilObservable; }
            set { _civilObservable = value; NotifyPropertyChanged("CivilObservable"); }
        }

        private ObservableCollection<CivilModelView> _civilEditObservable;
        public ObservableCollection<CivilModelView> CivilEditObservable
        {
            get { return _civilEditObservable; }
            set { _civilEditObservable = value; NotifyPropertyChanged("CivilEditObservable"); }
        }

        private ObservableCollection<CivilModelView> _civilViewObservable;
        public ObservableCollection<CivilModelView> CivilViewObservable
        {
            get { return _civilViewObservable; }
            set { _civilViewObservable = value; NotifyPropertyChanged("CivilViewObservable"); }
        }

        private Visibility _isUpdateBtnVisible;
        public Visibility IsUpdateBtnVisible
        {
            get { return _isUpdateBtnVisible; }
            set { _isUpdateBtnVisible = value; NotifyPropertyChanged("IsUpdateBtnVisible"); }
        }

        private Visibility _noContentCivil;
        public Visibility NoContentCivil
        {
            get { return _noContentCivil; }
            set { _noContentCivil = value; NotifyPropertyChanged("NoContentCivil"); }
        }

        #endregion

        #region Methods

        private CivilEngineer _civil;
        private bool _isStored;
        /// <summary>
        /// Save in the Model and store to the SQLiteDB
        /// </summary>
        /// <returns></returns>
        public bool SaveCivilEngineer()
        {
            _civil = new CivilEngineer();
            try
            {
                _civil.CivilFirstName = CivilFirstName;
                _civil.CivilLastName = CivilLastName;
                _civil.Speciality = Speciality;
                _civil.NumberTEE = NumberTEE;
                _civil.CivilAFM = CivilAFM;
                _civil.CivilDOY = CivilDOY;
                _civil.CivilTele = CivilTele;
                _civil.CivilNumberID = CivilNumberID;
                _civil.Nomos = Nomos;
                _civil.CivilMunicipality = CivilMunicipality;
                _civil.PlaceOfHouse = PlaceOfHouse;
                _civil.CivilAddress = CivilAddress;
                _civil.CivilNumber = CivilNumber;
                _civil.CivilPostCode = CivilPostCode;

               bool isStored = SQLite.SqLiteHandling.Instance.InsertCivilEngineer(_civil);
                if (isStored)
                {
                    //ClearValues
                    CivilFirstName = " ";
                    CivilLastName = " ";
                    Speciality = " ";
                    NumberTEE = " ";
                    CivilAFM = " ";
                    CivilDOY = " ";
                    CivilTele = " ";
                    CivilNumberID = " ";
                    Nomos = " ";
                    CivilMunicipality = " ";
                    PlaceOfHouse = " ";
                    CivilAddress = " ";
                    CivilNumber = " ";
                    CivilPostCode = " ";
                    MainWindowModel.Instance.MessageStatus = "Ο μηχανικός δημιουργήθηκε.";
                    MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                _isStored = false;
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "SaveCivilEngineer", e.Message, DateTime.Now);
            }
            return _isStored;
        }

        /// <summary>
        /// Diplay them into boxes
        /// </summary>
        public void GetCivilEngineer()
        {
            try
            {
                CivilObservable = SQLite.SqLiteHandling.Instance.ReturnCivilEngineer();
                if (CivilObservable.Count == 0 )
                {
                    NoContentCivil = Visibility.Visible;
                }
                else
                {
                    NoContentCivil = Visibility.Collapsed;
                }
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "GetCivilEngineer", e.Message, DateTime.Now);
            }
        }

        private ObservableCollection<CivilModelView> _listOfCivil;
        public void GetCivilById(int civilNumberId)
        {
            try
            {
                _listOfCivil = new ObservableCollection<CivilModelView>();
                _listOfCivil = SQLite.SqLiteHandling.Instance.ReturnCivilById(civilNumberId);
                //To display the selected Civil
                CivilObservable = _listOfCivil;
                //To display the info of the selected civil
                CivilEditObservable = _listOfCivil;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "GetCivilById", e.Message, DateTime.Now);
            }
        }

        /// <summary>
        /// Display all the info of the selected Civil
        /// </summary>
        /// <param name="civilNumberId"></param>
        public void GetCivilViewById(int civilNumberId)
        {
            try
            {
                _listOfCivil = new ObservableCollection<CivilModelView>();
                _listOfCivil = SQLite.SqLiteHandling.Instance.ReturnCivilById(civilNumberId);
                //To display the selected Civil
                CivilObservable = _listOfCivil;
                //To display the info of the selected civil
                CivilViewObservable = _listOfCivil;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "GetCivilViewById", e.Message, DateTime.Now);
            }
        }

        /// <summary>
        /// Delete the selected civil from the SQLite
        /// </summary>
        /// <param name="civilId"></param>
        public void DeleteCivil(string civilId)
        {
            try
            {
                bool isClientDeleted = SQLite.SqLiteHandling.Instance.DeleteCivil(civilId);
                if (isClientDeleted)
                {
                    _listOfCivil = new ObservableCollection<CivilModelView>();
                    _listOfCivil = SQLite.SqLiteHandling.Instance.ReturnCivilEngineer();
                    CivilObservable = _listOfCivil;
                    MainWindowModel.Instance.MessageStatus = "Ο μηχανικός διαγράφηκε.";
                }
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "DeleteCivil", e.Message, DateTime.Now);
            }
        }

        /// <summary>
        /// Save in the Model and store to the SQLiteDB
        /// </summary>
        /// <returns></returns>
        public bool UpdateCivilEngineer(int civilId)
        {
            _civil = new CivilEngineer();
            try
            {
               var newListOfCivil =  CivilEditObservable.Where(x => x.CivilId == civilId).ToList();
                foreach (var item in newListOfCivil)
                {
                    _civil.CivilId = civilId;
                    _civil.CivilFirstName = item.CivilFirstName;
                    _civil.CivilLastName = item.CivilLastName;
                    _civil.Speciality = item.Speciality;
                    _civil.NumberTEE = item.NumberTEE;
                    _civil.CivilAFM = item.CivilAFM;
                    _civil.CivilDOY = item.CivilDOY;
                    _civil.CivilTele = item.CivilTele;
                    _civil.CivilNumberID = item.CivilNumberID;
                    _civil.Nomos = item.Nomos;
                    _civil.CivilMunicipality = item.CivilMunicipality;
                    _civil.PlaceOfHouse = item.PlaceOfHouse;
                    _civil.CivilAddress = item.CivilAddress;
                    _civil.CivilNumber = item.CivilNumber;
                    _civil.CivilPostCode = item.CivilPostCode;
                    MainWindowModel.Instance.MessageStatus = SQLite.SqLiteHandling.Instance.UpdateCivil(_civil);
                }
                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
            }
            catch (Exception e)
            {
                _isStored = false;
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "UpdateCivilEngineer", e.Message, DateTime.Now);
            }
            return _isStored;
        }

        /// <summary>
        /// Export all Civil engineers into a PDF FILE
        /// </summary>
        public void ExportAllCivilsInPdf()
        {
            PDFHandler.PdfHandling pdfHandling = new PDFHandler.PdfHandling();
            try
            {
                CivilObservable = SQLite.SqLiteHandling.Instance.ReturnInfoOfAllCivilEngineer();

                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = pdfHandling.ExportAllCivilsInPdf(CivilObservable);
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "ExportAllCivilsInPdf", e.Message, DateTime.Now);
            }
        }

        /// <summary>
        /// Export the selected Civil engineer into a PDF File
        /// </summary>
        /// <param name="civilNumberId"></param>
        public void ExportSelectedCivilInfoInPdf(int civilNumberId)
        {
            PDFHandler.PdfHandling pdfHandling = new PDFHandler.PdfHandling();
            try
            {
                _listOfCivil = new ObservableCollection<CivilModelView>();
                _listOfCivil = SQLite.SqLiteHandling.Instance.ReturnCivilById(civilNumberId);

                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = pdfHandling.ExportSelectedCivilInPdf(_listOfCivil);
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
                Log_Handler.LogHandling.Instance.StoreLog("CivilModelView", "ExportSelectedCivilInfoInPdf", e.Message, DateTime.Now);
            }
        }

        #endregion

        #region INotify Event

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
