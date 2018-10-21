using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Model.CivilEngineers
{
    public class CivilEngineer
    {
        #region Properties
        public int CivilId { get; set; }
        public string CivilFirstName { get; set; }
        public string CivilLastName { get; set; }
        public string Speciality { get; set; }
        public string NumberTEE { get; set; }
        public string CivilAFM { get; set; }
        public string CivilDOY { get; set; }
        public string CivilTele { get; set; }
        public string CivilNumberID { get; set; }
        public string Nomos { get; set; }
        public string CivilMunicipality { get; set; }
        public string PlaceOfHouse { get; set; }
        public string CivilAddress { get; set; }
        public string CivilNumber { get; set; }
        public string CivilPostCode { get; set; }
        #endregion

        #region Constructor

        public CivilEngineer()
        {

        }

        public CivilEngineer(string civilFirstName, string civilLastName, string speciality, string numberTee,
                           string civilAfm, string civilDoy, string civilTele, string civilNumberId,
                           string nomos, string civilMunicipality, string placeOfHouse, string civilAddress, string civilNumber,
                           string civilPostCode)
        {
            this.CivilFirstName = civilFirstName;
            this.CivilLastName = civilLastName;
            this.Speciality = speciality;
            this.NumberTEE = numberTee;
            this.CivilAFM = civilAfm;
            this.CivilDOY = civilDoy;
            this.CivilTele = civilTele;
            this.CivilNumberID = civilNumberId;
            this.Nomos = nomos;
            this.CivilMunicipality = civilMunicipality;
            this.PlaceOfHouse = placeOfHouse;
            this.CivilAddress = civilAddress;
            this.CivilNumber = civilNumber;
            this.CivilPostCode = civilPostCode;
        }
        #endregion
    }
}
