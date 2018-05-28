using System.ComponentModel.DataAnnotations;

namespace Bolierplate.Repository.Tables
{
    public partial class TestsDetail
    {
        public int Id { get; set; }

        public int TestsId { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public virtual Test Test { get; set; }
    }
}
