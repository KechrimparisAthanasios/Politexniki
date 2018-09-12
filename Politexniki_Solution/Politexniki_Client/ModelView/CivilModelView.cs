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

        Model.CivilEngineer civil;
        private bool _isStored;
        /// <summary>
        /// Save in the Model and store to the SQLiteDB
        /// </summary>
        /// <returns></returns>
        public bool SaveCivilEngineer()
        {
            civil = new Model.CivilEngineer();
            try
            {
                civil.CivilFirstName = CivilFirstName;
                civil.CivilLastName = CivilLastName;
                civil.Speciality = Speciality;
                civil.NumberTEE = NumberTEE;
                civil.CivilAFM = CivilAFM;
                civil.CivilDOY = CivilDOY;
                civil.CivilTele = CivilTele;
                civil.CivilNumberID = CivilNumberID;
                civil.Nomos = Nomos;
                civil.CivilMunicipality = CivilMunicipality;
                civil.PlaceOfHouse = PlaceOfHouse;
                civil.CivilAddress = CivilAddress;
                civil.CivilNumber = CivilNumber;
                civil.CivilPostCode = CivilPostCode;

               bool isStored = SQLite.SQLiteHandling.Instance.InsertCivilEngineer(civil);
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
                CivilObservable = SQLite.SQLiteHandling.Instance.ReturnCivilEngineer();
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
            }
        }

        private ObservableCollection<CivilModelView> listOfCivil;
        public void GetCivilById(int civilNumberId)
        {
            try
            {
                listOfCivil = new ObservableCollection<CivilModelView>();
                listOfCivil = SQLite.SQLiteHandling.Instance.ReturnCivilById(civilNumberId);
                //To display the selected Civil
                CivilObservable = listOfCivil;
                //To display the info of the selected civil
                CivilEditObservable = listOfCivil;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
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
                listOfCivil = new ObservableCollection<CivilModelView>();
                listOfCivil = SQLite.SQLiteHandling.Instance.ReturnCivilById(civilNumberId);
                //To display the selected Civil
                CivilObservable = listOfCivil;
                //To display the info of the selected civil
                CivilViewObservable = listOfCivil;
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
            }
        }

        public void DeleteCivil(string civilId)
        {
            try
            {
                bool isClientDeleted = SQLite.SQLiteHandling.Instance.DeleteCivil(civilId);
                if (isClientDeleted)
                {
                    listOfCivil = new ObservableCollection<CivilModelView>();
                    listOfCivil = SQLite.SQLiteHandling.Instance.ReturnCivilEngineer();
                    CivilObservable = listOfCivil;
                    MainWindowModel.Instance.MessageStatus = "Ο μηχανικός διαγράφηκε.";
                }
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
            }
        }

        /// <summary>
        /// Save in the Model and store to the SQLiteDB
        /// </summary>
        /// <returns></returns>
        public bool UpdateCivilEngineer(int civilId)
        {
            civil = new Model.CivilEngineer();
            try
            {
               var newListOfCivil =  CivilEditObservable.Where(x => x.CivilId == civilId).ToList();
                foreach (var item in newListOfCivil)
                {
                    civil.CivilId = civilId;
                    civil.CivilFirstName = item.CivilFirstName;
                    civil.CivilLastName = item.CivilLastName;
                    civil.Speciality = item.Speciality;
                    civil.NumberTEE = item.NumberTEE;
                    civil.CivilAFM = item.CivilAFM;
                    civil.CivilDOY = item.CivilDOY;
                    civil.CivilTele = item.CivilTele;
                    civil.CivilNumberID = item.CivilNumberID;
                    civil.Nomos = item.Nomos;
                    civil.CivilMunicipality = item.CivilMunicipality;
                    civil.PlaceOfHouse = item.PlaceOfHouse;
                    civil.CivilAddress = item.CivilAddress;
                    civil.CivilNumber = item.CivilNumber;
                    civil.CivilPostCode = item.CivilPostCode;
                    MainWindowModel.Instance.MessageStatus = SQLite.SQLiteHandling.Instance.UpdateCivil(civil);
                }
                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
            }
            catch (Exception e)
            {
                _isStored = false;
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
            return _isStored;
        }

        public void ExportAllCivilsInPDF()
        {
            PDFHandler.PdfHandling pdfHandling = new PDFHandler.PdfHandling();
            try
            {
                CivilObservable = SQLite.SQLiteHandling.Instance.ReturnInfoOfAllCivilEngineer();

                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = pdfHandling.ExportAllCivilsInPDF(CivilObservable);
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
        }

        public void ExportSelectedCivilInfoInPDF(int civilNumberId)
        {
            PDFHandler.PdfHandling pdfHandling = new PDFHandler.PdfHandling();
            try
            {
                listOfCivil = new ObservableCollection<CivilModelView>();
                listOfCivil = SQLite.SQLiteHandling.Instance.ReturnCivilById(civilNumberId);

                MainWindowModel.Instance.IsSuccessVisible = Visibility.Visible;
                MainWindowModel.Instance.MessageStatus = pdfHandling.ExportSelectedCivilInPDF(listOfCivil);
            }
            catch (Exception e)
            {
                MainWindowModel.Instance.MessageStatus = e.Message;
                MainWindowModel.Instance.IsFailVisible = Visibility.Visible;
            }
        }

        #endregion

        #region INotify Event

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
