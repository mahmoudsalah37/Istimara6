namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentStatement")]
    public partial class StudentStatement
    {
        public int ID { get; set; }

        public int? id_level { get; set; }

        public int? id_branch { get; set; }

        public int? id_subject { get; set; }

        public int? numberOfStudents { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Level Level { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
