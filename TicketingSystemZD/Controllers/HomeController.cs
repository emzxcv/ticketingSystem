using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using TicketingSystemZD.Models;

namespace TicketingSystemZD.Controllers
{
    public class HomeController : Controller
	{
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var req = (HttpWebRequest)WebRequest.Create("https://emxqm.zendesk.com/api/v2/tickets.json");
            req.Credentials = new NetworkCredential("emilyha17@gmail.com", "C0ldrock");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //System.Console.WriteLine(resp.StatusCode);
            StreamReader responseReader = new StreamReader(resp.GetResponseStream());
            var json = responseReader.ReadToEnd();

			dynamic dynJson = JsonConvert.DeserializeObject(json);
			foreach (var item in dynJson.tickets)
			{
				System.Console.WriteLine("id:{0} \ndesc:{1} \nstatus:{2} \n" +
										 "priority:{3}\n", item.id, item.description,
					item.status, item.priority);
			}

            List<Ticket> ticketsList = new List<Ticket>();
            foreach ( var item in dynJson.tickets){
                ticketsList.Add(new Ticket{
					id = item.id,
					description = item.description,
					status = item.status,
					priority = item.priority,
					type = item.type
                });
            }

            var pager = new Pager(ticketsList.Count(), page);

       
			var viewModel = new IndexViewModel
            {
                Items = ticketsList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),		
                         Pager = pager
			};
			return View(viewModel);
        }
    }
}



