namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            StudentStatements = new HashSet<StudentStatement>();
            Subjects_Teachers = new HashSet<Subjects_Teachers>();
        }

        public int ID { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public int? paper { get; set; }

        [Column("virtual")]
        public int? _virtual { get; set; }

        public int? total_hours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStatement> StudentStatements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subjects_Teachers> Subjects_Teachers { get; set; }
    }
}
