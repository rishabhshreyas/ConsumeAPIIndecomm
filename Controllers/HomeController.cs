using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeAPI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            
            HttpClient client = new HttpClient();
            List<PropertiesJSONViewModel> _result = new List<PropertiesJSONViewModel>();
            var value = await client.GetStringAsync(new Uri("https://samplerspubcontent.blob.core.windows.net/public/properties.json"));
            if (!string.IsNullOrEmpty(value))
            {
                var res = JsonConvert.DeserializeObject<Root>(value);
                foreach (var properties in res.properties)
                {
                    _result.Add(new PropertiesJSONViewModel()
                    {
                        Addresses = new Address()
                        {
                            address1 = properties.address.address1,
                            address2 = properties.address.address2,
                            city = properties.address.city,
                            country = properties.address.country,
                            county = properties.address.county,
                            district = properties.address.district,
                            state = properties.address.state,
                            zip = properties.address.zip,
                            zipPlus4 = properties.address.zipPlus4,
                        },
                        listPrice = properties.financial?.listPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US")),
                        monthlyRent = properties.financial?.monthlyRent.ToString("C", CultureInfo.CreateSpecificCulture("en-US")),
                        yearBuilt = properties.physical?.yearBuilt,
                        GrossYield = Convert.ToString(Math.Round(Convert.ToDouble(properties.financial?.listPrice == 0 ? 0 : (((properties.financial?.monthlyRent * 12) / properties.financial?.listPrice) * 100)),2)) + "%"
                    });
                }
            }



            return View(new PropertiesViewModel() { Properties = _result });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string rowJson)
        {
            var data = JsonConvert.DeserializeObject<PropertiesJSONViewModel>(rowJson);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertDataintoTable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ListPrice", SqlDbType.VarChar).Value = data.listPrice;
                    cmd.Parameters.Add("@MonthlyRent", SqlDbType.VarChar).Value = data.monthlyRent;
                    cmd.Parameters.Add("@YearBuilt", SqlDbType.VarChar).Value = data.yearBuilt;
                    cmd.Parameters.Add("@GrossYield", SqlDbType.VarChar).Value = data.GrossYield;
                    cmd.Parameters.Add("@address1", SqlDbType.VarChar).Value = data.Addresses.address1;
                    cmd.Parameters.Add("@address2", SqlDbType.VarChar).Value = data.Addresses.address2;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = data.Addresses.city;
                    cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = data.Addresses.country;
                    cmd.Parameters.Add("@county", SqlDbType.VarChar).Value = data.Addresses.county;
                    cmd.Parameters.Add("@district", SqlDbType.VarChar).Value = data.Addresses.district;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = data.Addresses.state;
                    cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = data.Addresses.zip;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("index");
        }
    }
}