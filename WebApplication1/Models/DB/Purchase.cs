//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long UserID { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Status { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}