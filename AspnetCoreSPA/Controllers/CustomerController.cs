using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration Config;
        private readonly string CustomerDataPath;

        public CustomerController(IConfiguration configuration)
        {
            this.Config = configuration;
            CustomerDataPath = Environment.CurrentDirectory + this.Config["CustomerDataPath"];

           
        }

        public IEnumerable<Customer> Get()
        {
            List<Customer> customers = new List<Customer>();
            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(CustomerDataPath);

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                
                if (!string.IsNullOrEmpty(row))
                {
                    customers.Add(new Customer
                    {
                        First_name = row.Split(',')[0],
                        Last_name = row.Split(',')[1],
                        Email = row.Split(',')[9],
                        Phone1 = row.Split(',')[7]

                    });
                }

                
            }


           // customers.RemoveAt(0);
            return customers;


        }


        //http://localhost:5000/api/Customer/Searchtxt?qtxt=first_name

        [HttpGet("[action]")]
        public IEnumerable<Customer> Searchtxt([FromQuery] string qtxt)
        {

            qtxt = qtxt != null ? qtxt : string.Empty;
            return Get().Where(x => (x.First_name != null && x.First_name.ToLower().Contains(qtxt.ToLower()))
                            || (x.Last_name != null && x.Last_name.ToLower().Contains(qtxt.ToLower()))
                            || (x.Email != null && x.Email.ToLower().Contains(qtxt.ToLower()))
                            || (x.Phone1 != null && x.Phone1.ToLower().Contains(qtxt.ToLower())));
        }

    }
}