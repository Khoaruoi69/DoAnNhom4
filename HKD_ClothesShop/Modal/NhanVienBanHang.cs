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
    
    public partial class NhanVienBanHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVienBanHang()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }
    
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public byte[] AnhThe { get; set; }
        public string GioiTinh { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}