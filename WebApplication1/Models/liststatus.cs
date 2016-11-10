using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class liststatus
        
    {

        [Display(Name = "User Role")]
        public long ID { get; set; }
        public IEnumerable<SelectListItem> Description { get; set; }


    }
}