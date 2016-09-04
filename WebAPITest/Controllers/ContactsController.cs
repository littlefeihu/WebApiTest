using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    public class ContactsController : ApiController
    {
    
        [Route("api/contacts")]
        public void Post()
        {
            var formatters = new MediaTypeFormatter[] {  new XmlMediaTypeFormatter()};
            Contact contact = this.Request.Content.ReadAsAsync<Contact>(formatters).Result;
            Console.WriteLine(contact.Name);
        }
    }
}
