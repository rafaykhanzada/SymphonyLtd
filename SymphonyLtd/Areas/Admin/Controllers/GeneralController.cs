using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SymphonyLtd.Models;
using Newtonsoft.Json;
using SymphonyLtd.Security;

namespace SymphonyLtd.Areas.Admin.Controllers
{
    [FormAuthentication(RoleId = "1")]

    public class GeneralController : Controller
    {
        private SymphonyDBEntities db = new SymphonyDBEntities();

        // GET: Admin/General
        [HttpPost]        
        public string GetStateByCountryID(int id)
        {
            List<State> Data = db.tblStates.Where(x => x.Country_FK == id).Select(o => new State
            {
               StateID=o.StateID,
               StateName=o.StateName,
               CountryID=o.Country_FK,
               City=o.tblCities.Select(x=>new CityByState
               { 
               CityID=x.CityID,
               CityName=x.CityName,
               }).ToList(),
            }).ToList();
            return JsonConvert.SerializeObject(Data);
        } 
        [HttpPost]
        public string GetCitiesByCountryID(int id)
        {
            List<City> Data = db.tblCities.Where(x => x.State_FK == id).Select(o => new City{
            CityID=o.CityID,
            CityName=o.CityName,
            StateID=o.State_FK,
            CountryID=o.tblState.Country_FK            
            }).ToList();
            return JsonConvert.SerializeObject(Data);
        }      
    }
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? StateID { get; set; }
        public int? CountryID { get; set; }
    }
    public class CityByState
    {
        public int CityID { get; set; }
        public string CityName { get; set; }     
    }
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public List<CityByState> City { get; set; }
        public int? CountryID { get; set; }
    }
}
