using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DB;

namespace WebApplication1.Models
{
    public class adminlistview
       
    {
        public IEnumerable<User> User { get; set; }
        public IEnumerable<Purchase> Purchase { get; set; }
    }
}