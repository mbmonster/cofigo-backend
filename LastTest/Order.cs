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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public string ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> IDShip { get; set; }
        public string IDCustomer { get; set; }
        public string Status { get; set; }
        public string SDT { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longtitude { get; set; }
        public Nullable<double> Total { get; set; }
    
        public virtual Ship Ship { get; set; }
        public virtual User User { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
