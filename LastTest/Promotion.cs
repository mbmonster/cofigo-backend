//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LastTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Promotion
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<int> IDStore { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Last { get; set; }
        public Nullable<int> IDMenu { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual Menu Menu1 { get; set; }
    }
}
