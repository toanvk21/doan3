namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckOut")]
    public partial class CheckOut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckOut()
        {
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string Date { get; set; }

        [Column(TypeName = "ntext")]
        public string FirstName { get; set; }

        [Column(TypeName = "ntext")]
        public string LastName { get; set; }

        [Column(TypeName = "ntext")]
        public string Email { get; set; }

        [Column(TypeName = "ntext")]
        public string Address { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
