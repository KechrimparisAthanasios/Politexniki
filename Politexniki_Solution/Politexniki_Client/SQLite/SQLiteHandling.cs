using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Politexniki_Client.Model.Customers;
using Politexniki_Client.Model.CivilEngineers;
using Politexniki_Client.Model.Project;

namespace Politexniki_Client.SQLite
{
    public class SQLiteHandling
    {
        #region Singleton
        private static readonly SQLiteHandling instance = new SQLiteHandling();
        static SQLiteHandling(){}

        private SQLiteHandling(){}

        public static SQLiteHandling Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Database Handling

        private readonly string _SQLiteCredential = @"C:\Politexniki\PolitexnikiDatabase\PolitexnikiDataBase.sqlite";
        private SQLiteConnection _sqliteConnection;
        private SQLiteCommand _sqliteCommand;
        private readonly string _lockSQLite = "";

        public void InitSQLite()
        {
            CreateDirectory();
            CreateSQLiteDb();
            CreateCivilTable();
            CreateCustomerTable();
        }

        public void CreateDirectory()
        {
            try
            {
                var folderPath = @"C:\Politexniki\PolitexnikiDatabase";
                var exists = System.IO.Directory.Exists(folderPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(folderPath);
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }
        }

        private bool _isDbCreated;
        public bool CreateSQLiteDb()
        {
            try
            {
                if (File.Exists(_SQLiteCredential))
                {
                    _isDbCreated = true;
                }
                else
                {
                    SQLiteConnection.CreateFile(_SQLiteCredential);
                    _isDbCreated = true;
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }

            return _isDbCreated;
        }

        #endregion

        #region Civil Handling

        /// <summary>
        /// Create the table of the Civil
        /// </summary>
        public void CreateCivilTable()
        {
            lock (_lockSQLite)
            {
                _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                try
                {
                    _sqliteConnection.OpenAsync();
                    string createCivilTableQuery =
                        "Create Table if not exists CivilEngineerTable " +
                        "(" +
                        "CivilId INT, " +
                        "CivilFirstName VARCHAR(20000), " +
                        "CivilLastName NVARCHAR(20000), " +
                        "CivilSpeciality NVARCHAR(20000)," +
                        "NumberTEE NVARCHAR(20000)," +
                        "CivilAFM  NVARCHAR(20000)," +
                        "CivilDOY  NVARCHAR(20000)," +
                        "CivilTele NVARCHAR(20000)," +
                        "CivilNumberID  NVARCHAR(20000)," +
                        "Nomos  NVARCHAR(20000)," +
                        "CivilMunicipality  NVARCHAR(20000)," +
                        "PlaceOfHouse  NVARCHAR(20000)," +
                        "CivilAddress  NVARCHAR(20000)," +
                        "CivilNumber  NVARCHAR(20000)," +
                        "CivilPostCode  NVARCHAR(20000))";
                    _sqliteCommand = new SQLiteCommand(createCivilTableQuery, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                    _sqliteConnection.Close();
                }
                catch (Exception e)
                {
                    string _messageStatus = e.Message;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
            }
        }

        private bool _isCivilStored;
        /// <summary>
        /// Create a new Civil engineer
        /// </summary>
        /// <param name="civilEngineer"></param>
        /// <returns></returns>
        public bool InsertCivilEngineer(CivilEngineer civilEngineer)
        {
            lock (_lockSQLite)
            {
                try
                {
                    int civilId = GetLastCivilId();
                    int newCivilId = civilId + 1;
                    
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();

                    string insertData = "Insert into  CivilEngineerTable(" +
                        "CivilId," +
                        "CivilFirstName," +
                        "CivilLastName," +
                        "CivilSpeciality," +
                        "NumberTEE," +
                        "CivilAFM," +
                        "CivilDOY," +
                        "CivilTele," +
                        "CivilNumberID," +
                        "Nomos," +
                        "CivilMunicipality," +
                        "PlaceOfHouse," +
                        "CivilAddress," +
                        "CivilNumber," +
                        "CivilPostCode)" +
                        "values('" +
                        newCivilId + "','" +
                        civilEngineer.CivilFirstName + "','" +
                        civilEngineer.CivilLastName + "','" +
                        civilEngineer.Speciality + "','" +
                        civilEngineer.NumberTEE + "','" +
                        civilEngineer.CivilAFM + "','" +
                        civilEngineer.CivilDOY + "','" +
                        civilEngineer.CivilTele + "','" +
                        civilEngineer.CivilNumberID + "','" +
                        civilEngineer.Nomos + "','" +
                        civilEngineer.CivilMunicipality + "','" +
                        civilEngineer.PlaceOfHouse + "','" +
                        civilEngineer.CivilAddress + "','" +
                        civilEngineer.CivilNumber + "','" +
                        civilEngineer.CivilPostCode + "')";

                    _sqliteCommand = new SQLiteCommand(insertData, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();

                    _isCivilStored = true;
                }
                catch (Exception exce)
                {
                    string _messageStatus = "SQLiteError: " + exce.Message;
                    _isCivilStored = false;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _isCivilStored;
            }
        }

        private string _messageStatus;
        /// <summary>
        /// Update the info of the Civil
        /// </summary>
        /// <param name="civilEngineer"></param>
        /// <returns></returns>
        public string UpdateCivil(CivilEngineer civilEngineer)
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();

                    string updateQuery = "Update CivilEngineerTable Set " +
                            "CivilId = " + civilEngineer.CivilId + "," +
                            " CivilFirstName = '" + civilEngineer.CivilFirstName + "'," +
                            " CivilLastName = '" + civilEngineer.CivilLastName + "'," +
                            " CivilSpeciality = '" + civilEngineer.Speciality + "'," +
                            " NumberTEE = '" + civilEngineer.NumberTEE + "'," +
                            " CivilAFM = '" + civilEngineer.CivilAFM + "'," +
                            " CivilDOY = '" + civilEngineer.CivilDOY + "'," +
                            " CivilTele = '" + civilEngineer.CivilTele + "'," +
                            " CivilNumberID = '" + civilEngineer.CivilNumberID + "'," +
                            " Nomos = '" + civilEngineer.Nomos + "'," +
                            " CivilMunicipality = '" + civilEngineer.CivilMunicipality + "'," +
                            " PlaceOfHouse = '" + civilEngineer.PlaceOfHouse + "'," +
                            " CivilAddress = '" + civilEngineer.CivilAddress + "'," +
                            " CivilNumber = '" + civilEngineer.CivilNumber + "'," +
                            " CivilPostCode = '" + civilEngineer.CivilPostCode + "' where CivilId=" + civilEngineer.CivilId;

                    _sqliteCommand = new SQLiteCommand(updateQuery, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();

                    _messageStatus = "";
                }
                catch (Exception exce)
                {
                    string _messageStatus = "SQLiteError: " + exce.Message;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _messageStatus;
            }
        }

        private int _lastId;
        /// <summary>
        /// Get the last id of the Civil in order to add the next id in the new Civil
        /// </summary>
        /// <returns></returns>
        public int GetLastCivilId()
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");

                    _sqliteConnection.OpenAsync();

                    string countQuery = "SELECT RowID FROM CivilEngineerTable ORDER BY RowID Desc LIMIT 1;";
                    SQLiteCommand command = new SQLiteCommand(countQuery, _sqliteConnection);

                    _lastId = Convert.ToInt32(command.ExecuteScalar());

                    _sqliteConnection.Close();
                }
                catch (Exception e)
                {
                    _sqliteConnection.Close();
                    //We send the exception as -1
                    _lastId = -1;
                }
                return _lastId;
            }
        }

        private ObservableCollection<ModelView.CivilModelView> _listOfCivilEngineer;
        private ModelView.CivilModelView _civilEngineer;
        /// <summary>
        /// Return the Civil to display them
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModelView.CivilModelView> ReturnCivilEngineer()
        {
            lock (_lockSQLite)
            {
                _listOfCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();

                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string selectCivilEngineer = @"Select CivilFirstName,CivilLastName,CivilId from CivilEngineerTable";

                    _sqliteCommand = new SQLiteCommand(selectCivilEngineer, _sqliteConnection);
                    SQLiteDataReader reader = _sqliteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        _civilEngineer = new ModelView.CivilModelView
                        {
                            CivilFirstName = reader["CivilFirstName"].ToString(),
                            CivilLastName = reader["CivilLastName"].ToString(),
                            CivilId = int.Parse(reader["CivilId"].ToString())
                        };
                        _listOfCivilEngineer.Add(_civilEngineer);
                    }
                }
                catch (Exception e)
                {
                    _listOfCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();
                }
                finally
                {
                    _sqliteConnection.Close();
                }

                return _listOfCivilEngineer;
            }
        }

        /// <summary>
        /// Return all the Civils to display them into PDF
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModelView.CivilModelView> ReturnInfoOfAllCivilEngineer()
        {
            lock (_lockSQLite)
            {
                _listOfCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();

                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string selectCivilEngineer = @"Select CivilFirstName,CivilLastName,CivilId,CivilSpeciality,CivilTele from CivilEngineerTable";

                    _sqliteCommand = new SQLiteCommand(selectCivilEngineer, _sqliteConnection);
                    SQLiteDataReader reader = _sqliteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        _civilEngineer = new ModelView.CivilModelView
                        {
                            CivilFirstName = reader["CivilFirstName"].ToString(),
                            CivilLastName = reader["CivilLastName"].ToString(),
                            CivilId = int.Parse(reader["CivilId"].ToString()),
                            Speciality = reader["CivilSpeciality"].ToString(),
                            CivilTele = reader["CivilTele"].ToString()
                        };
                        _listOfCivilEngineer.Add(_civilEngineer);
                    }
                }
                catch (Exception e)
                {
                    _listOfCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();
                }
                finally
                {
                    _sqliteConnection.Close();
                }

                return _listOfCivilEngineer;
            }
        }

        private ObservableCollection<ModelView.CivilModelView> _listOfOneCivilEngineer;
        private ModelView.CivilModelView _onecivilEngineer;
        /// <summary>
        /// Retutn the selected Civil by Id
        /// </summary>
        /// <param name="civilId"></param>
        /// <returns></returns>
        public ObservableCollection<ModelView.CivilModelView> ReturnCivilById(int civilId)
        {
            lock (_lockSQLite)
            {
                _listOfOneCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();

                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string selectCivilEngineer = @"Select * from CivilEngineerTable where CivilId ='" + civilId + "'";

                    _sqliteCommand = new SQLiteCommand(selectCivilEngineer, _sqliteConnection);
                    SQLiteDataReader reader = _sqliteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        _onecivilEngineer = new ModelView.CivilModelView
                        {
                            CivilId = int.Parse(reader["CivilId"].ToString()),
                            CivilFirstName = reader["CivilFirstName"].ToString(),
                            CivilLastName = reader["CivilLastName"].ToString(),
                            Speciality = reader["CivilSpeciality"].ToString(),
                            NumberTEE = reader["NumberTEE"].ToString(),
                            CivilAFM = reader["CivilAFM"].ToString(),
                            CivilDOY = reader["CivilDOY"].ToString(),
                            CivilTele = reader["CivilTele"].ToString(),
                            CivilNumberID = reader["CivilNumberID"].ToString(),
                            Nomos = reader["Nomos"].ToString(),
                            CivilMunicipality = reader["CivilMunicipality"].ToString(),
                            PlaceOfHouse = reader["PlaceOfHouse"].ToString(),
                            CivilAddress = reader["CivilAddress"].ToString(),
                            CivilNumber = reader["CivilNumber"].ToString(),
                            CivilPostCode = reader["CivilPostCode"].ToString()
                        };
                        _listOfOneCivilEngineer.Add(_onecivilEngineer);
                    }
                }
                catch (Exception e)
                {
                    _listOfOneCivilEngineer = new ObservableCollection<ModelView.CivilModelView>();
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _listOfOneCivilEngineer;
            }
        }

        private bool _isCivilDeleted;
        /// <summary>
        /// Delete the selected Civil by Id
        /// </summary>
        /// <param name="civilID"></param>
        /// <returns></returns>
        public bool DeleteCivil(string civilID)
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string deleteCivilEngineer = @"Delete from CivilEngineerTable where CivilId ='" + civilID + "'";
                    _sqliteCommand = new SQLiteCommand(deleteCivilEngineer, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                    _isCivilDeleted = true;
                }
                catch (Exception e)
                {
                    _isCivilDeleted = false;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _isCivilDeleted;
            }
        }

        #endregion

        #region Customer Handling

        /// <summary>
        /// Create the table of the Customer in the SQLite db
        /// </summary>
        public void CreateCustomerTable()
        {
            lock (_lockSQLite)
            {
                _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                try
                {
                    _sqliteConnection.OpenAsync();
                    string createTable =
                        "Create Table if not exists CustomerTable(" +
                        "CustomerId VARCHAR(200)," +
                        "FirstName VARCHAR(20000), " +
                        "LastName NVARCHAR(20000), " +
                        "FatherFullName NVARCHAR(20000)," +
                        "MotherFullName NVARCHAR(20000)," +
                        "Birthday NVARCHAR(20000)," +
                        "PlaceOfBirth NVARCHAR(20000)," +
                        "Telephone NVARCHAR(20000)," +
                        "ID NVARCHAR(20000)," +
                        "ResidencePlace  NVARCHAR(20000)," +
                        "Address NVARCHAR(20000)," +
                        "Number INT," +
                        "PostCode  NVARCHAR(20000)," +
                        "SocialNumber  NVARCHAR(20000)," +
                        "TaxPlace NVARCHAR(20000))";

                    _sqliteCommand = new SQLiteCommand(createTable, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string _messageStatus = e.Message;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
            }
        }

        private bool _isCustomerStored;
        /// <summary>
        /// Stores the new Customer in the SQLite
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>If the new customer stored</returns>
        public bool StoreCustomer(Customer customer)
        {
            lock (_lockSQLite)
            {
                try
                {
                    int customerId = GetLastCustomerId();
                    int newCustomerId = customerId + 1;

                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();

                    string insertCustomerQuery = "Insert into CustomerTable(" +
                        "CustomerId," +
                        "FirstName," +
                        "LastName," +
                        "FatherFullName," +
                        "MotherFullName," +
                        "Birthday," +
                        "PlaceOfBirth," +
                        "Telephone," +
                        "ID," +
                        "ResidencePlace," +
                        "Address," +
                        "Number," +
                        "PostCode," +
                        "SocialNumber," +
                        "TaxPlace)" +
                        "values('" +
                        newCustomerId + "','" +
                        customer.FirstName + "','" +
                        customer.LastName + "','" +
                        customer.FatherFullName + "','" +
                        customer.MotherFullName + "','" +
                        customer.Birthday + "','" +
                        customer.PlaceOfBirth + "','" +
                        customer.Telephone + "','" +
                        customer.Id + "','" +
                        customer.ResidencePlace + "','" +
                        customer.Address + "','" +
                        customer.Number + "','" +
                        customer.PostCode + "','" +
                        customer.SocialNumber + "','" +
                        customer.TaxPlace + "')";

                    _sqliteCommand = new SQLiteCommand(insertCustomerQuery, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                    _isCustomerStored = true;
                }
                catch (Exception exce)
                {
                    string _messageStatus = "SQLiteError: " + exce.Message;
                    _isCustomerStored = false;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _isCustomerStored;
            }
        }

        private ObservableCollection<ModelView.CustomerModelView> _listOfCustomers;
        private ModelView.CustomerModelView _customerModelView;
        /// <summary>
        /// Getting the info to display in the panel of the customers 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModelView.CustomerModelView> ReturnCustomer()
        {
            lock (_lockSQLite)
            {
                try
                {
                    _listOfCustomers = new ObservableCollection<ModelView.CustomerModelView>();
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();

                    string selectCustomerQuery = @"Select FirstName,LastName,CustomerId from CustomerTable";

                    _sqliteCommand = new SQLiteCommand(selectCustomerQuery, _sqliteConnection);
                    SQLiteDataReader reader = _sqliteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        _customerModelView = new ModelView.CustomerModelView
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            CustomerID = reader["CustomerId"].ToString()
                        };
                        _listOfCustomers.Add(_customerModelView);
                    }
                }
                catch (Exception E)
                {
                    string s = E.ToString();
                    _listOfCustomers = new ObservableCollection<ModelView.CustomerModelView>();
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _listOfCustomers;
            }
        }

        private int _lastCustomerId;
        /// <summary>
        /// Get the last Customer id in order to 
        /// add the id in the new customer
        /// </summary>
        /// <returns></returns>
        public int GetLastCustomerId()
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");

                    _sqliteConnection.OpenAsync();

                    string countQuery = "SELECT RowID FROM CustomerTable ORDER BY RowID Desc LIMIT 1;";
                    SQLiteCommand command = new SQLiteCommand(countQuery, _sqliteConnection);

                    _lastCustomerId = Convert.ToInt32(command.ExecuteScalar());

                    _sqliteConnection.Close();
                }
                catch (Exception e)
                {
                    _sqliteConnection.Close();
                    //We send the exception as -1
                    _lastCustomerId = -1;
                }
                return _lastCustomerId;
            }
        }

        private ObservableCollection<ModelView.CustomerModelView> _listOfOneCustomer;
        private ModelView.CustomerModelView _oneCustomer;
        /// <summary>
        /// Return the selected Customer by Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public ObservableCollection<ModelView.CustomerModelView> ReturnCustomerById(string customerId)
        {
            lock (_lockSQLite)
            {
                _listOfOneCustomer = new ObservableCollection<ModelView.CustomerModelView>();

                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string selectClient = @"Select * from CustomerTable where CustomerId ='" + customerId + "'";

                    _sqliteCommand = new SQLiteCommand(selectClient, _sqliteConnection);
                    SQLiteDataReader reader = _sqliteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        _oneCustomer = new ModelView.CustomerModelView
                        {
                            CustomerID = reader["CustomerId"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            FatherFullName = reader["FatherFullName"].ToString(),
                            MotherFullName = reader["MotherFullName"].ToString(),
                            Birthday = reader["Birthday"].ToString(),
                            PlaceOfBirth = reader["PlaceOfBirth"].ToString(),
                            Telephone = reader["Telephone"].ToString(),
                            Id = reader["ID"].ToString(),
                            ResidencePlace = reader["ResidencePlace"].ToString(),
                            Address = reader["Address"].ToString(),
                            Number = int.Parse(reader["Number"].ToString()),
                            PostCode = reader["PostCode"].ToString(),
                            SocialNumber = reader["SocialNumber"].ToString(),
                            TaxPlace = reader["TaxPlace"].ToString()
                        };
                        _listOfOneCustomer.Add(_oneCustomer);
                    }
                }
                catch (Exception e)
                {
                    _listOfOneCustomer = new ObservableCollection<ModelView.CustomerModelView>();
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _listOfOneCustomer;
            }
        }

        private string _isCustomerDeletedStatus;
        /// <summary>
        /// Delete the selected Customer from SQLite
        /// </summary>
        /// <param name="selectedCustomerId"></param>
        /// <returns></returns>
        internal string DeleteSelectedCustomer(string selectedCustomerId)
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();
                    string deleteCustomer = @"Delete from CustomerTable where CustomerId ='" + selectedCustomerId + "'";
                    _sqliteCommand = new SQLiteCommand(deleteCustomer, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                    _isCustomerDeletedStatus = "Ο πελάτης διαγράφηκε.";
                }
                catch (Exception e)
                {
                    _isCustomerDeletedStatus = e.Message;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _isCustomerDeletedStatus;
            }
        }

        /// <summary>
        /// Update the info of the Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public string UpdateCustomer(Customer customer)
        {
            lock (_lockSQLite)
            {
                try
                {
                    _sqliteConnection = new SQLiteConnection("Data Source=" + _SQLiteCredential + ";Version=3;");
                    _sqliteConnection.OpenAsync();

                    string updateQuery = "Update CustomerTable Set " +
                            "CustomerId = " + customer.CustomerID + "," +
                            "FirstName = '" + customer.FirstName + "'," +
                            "LastName = '" + customer.LastName + "'," +
                            "FatherFullName = '" + customer.FatherFullName + "'," +
                            "MotherFullName = '" + customer.MotherFullName + "'," +
                            "Birthday = '" + customer.Birthday + "'," +
                            "PlaceOfBirth = '" + customer.PlaceOfBirth + "'," +
                            "Telephone = '" + customer.Telephone + "'," +
                            "ID = '" + customer.Id + "'," +
                            "ResidencePlace = '" + customer.ResidencePlace + "'," +
                            "Address = '" + customer.Address + "'," +
                            "Number = '" + customer.Number + "'," +
                            "PostCode = '" + customer.PostCode + "'," +
                            "SocialNumber = '" + customer.SocialNumber + "'," +
                            "TaxPlace = '" + customer.TaxPlace + "' where CustomerId=" + customer.CustomerID;

                    _sqliteCommand = new SQLiteCommand(updateQuery, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();

                    _messageStatus = "";
                }
                catch (Exception exce)
                {
                    string _messageStatus = "SQLiteError: " + exce.Message;
                }
                finally
                {
                    _sqliteConnection.Close();
                }
                return _messageStatus;
            }
        }

        #endregion

        #region Handle Project

        private readonly string _sqLiteCredentialProject = @"C:\Politexniki\PolitexnikiDatabase";

        public bool CreateSqLiteDbProject(string projectName)
        {
            try
            {
                if (File.Exists(_sqLiteCredentialProject + @"\" + projectName + ".sqlite"))
                {
                    _isDbCreated = true;
                }
                else
                {
                    SQLiteConnection.CreateFile(_sqLiteCredentialProject + @"\" + projectName + ".sqlite");
                    _isDbCreated = true;
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }

            return _isDbCreated;
        }

        private bool _isProjectTableCreated;
        private string _buildQuery;
        public bool CreateProjectTable(Project project)
        {
            lock (_lockSQLite)
            {
                _sqliteConnection = new SQLiteConnection("Data Source=" + _sqLiteCredentialProject + @"\" + project.ProjectName + ".sqlite;Version=3;");
                try
                {
                    _sqliteConnection.OpenAsync();
                    _buildQuery = "Create Table if not exists ProjectTable(ProjectId VARCHAR(200),";
                    for (int i = 0; i < project.ListOfProjectOwners.Count; i++)
                    {
                        foreach (ProjectOwner projectOwner in project.ListOfProjectOwners)
                        {
                            _buildQuery += "'" + nameof(projectOwner.FullName) + i + "'";
                            _buildQuery += " " + "TEXT";
                            _buildQuery += ",";
                            _buildQuery += "'" + nameof(projectOwner.DOY) + i + "'";
                            _buildQuery += " " + "TEXT";
                            _buildQuery += ",";
                            _buildQuery += "'" + nameof(projectOwner.SocialNumber) + i + "'";
                            _buildQuery += " " + "TEXT";
                            _buildQuery += ",";
                            _buildQuery += "'" + nameof(projectOwner.Percantage) + i + "'";
                            _buildQuery += " " + "TEXT";
                            _buildQuery += ",";
                            _buildQuery += "'" + nameof(projectOwner.AFM) + i + "'";
                            _buildQuery += " " + "TEXT";
                            if (project.ListOfProjectOwners.Count < i)
                            {
                                _buildQuery += ",";
                            }
                        }
                    }

                    _buildQuery += ");";
                    _sqliteCommand = new SQLiteCommand(_buildQuery, _sqliteConnection);
                    _sqliteCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    _isProjectTableCreated = false;
                }
                finally
                {
                    _sqliteConnection.Close();
                }

                return _isProjectTableCreated;
            }
        }

        #endregion

    }
}
