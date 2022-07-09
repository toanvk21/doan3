namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentNew
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "ntext")]
        public string Date { get; set; }

        public int News { get; set; }

        public virtual News News1 { get; set; }
    }
}
