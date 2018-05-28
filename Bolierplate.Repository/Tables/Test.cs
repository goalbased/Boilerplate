using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bolierplate.Repository.Tables
{
    public partial class Test
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateTime { get; set; }

        public virtual ICollection<TestsDetail> TestsDetails { get; set; }
    }
}
