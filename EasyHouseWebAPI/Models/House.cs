using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyHouseWebAPI.Models
{
    public class House
    {
        public int HomeId { get;  set; }
        public string  Address { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string ZipCode { get;  set; }
        public string ContactName { get;  set; }
        public string ContactPhone { get; set; }
        public string FrontViewPhoto { get; set; }
        public decimal Price { get;  set; }
    }
}
