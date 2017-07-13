using System;
using System.Web.Helpers;

namespace TicketingSystemZD.Models
{
    public class Ticket
    {
		public int id { get; set; }
        public string type { get; set; }
		public string description { get; set; }
		public string priority { get; set; }
        public string status { get; set; }
    }



}
