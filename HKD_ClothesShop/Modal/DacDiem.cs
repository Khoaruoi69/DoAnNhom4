//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HKD_ClothesShop.Modal
{
    using System;
    using System.Collections.Generic;
    
    public partial class DacDiem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DacDiem()
        {
            this.DacDiem_SanPham = new HashSet<DacDiem_SanPham>();
        }
    
        public string Size { get; set; }
        public string Color { get; set; }
        public string MoTa { get; set; }
        public bool Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DacDiem_SanPham> DacDiem_SanPham { get; set; }
    }
}
