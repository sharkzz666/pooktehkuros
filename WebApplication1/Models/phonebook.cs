using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.DB;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class phonebook
    {

        [Required(ErrorMessage ="please fill up")]
        [DisplayName("Nama")]
        public string nama { get; set; }
        
        [Required(ErrorMessage = "Please fill ip")]
        [DisplayName("No Tel")]
        public string notel { get; set; }

        [Required(ErrorMessage = "please key in valid address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
        ErrorMessage = "Email Format is wrong")]
        public string email { get; set; }


    }
}