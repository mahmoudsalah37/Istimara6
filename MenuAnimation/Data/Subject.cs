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
            AstmaraAs = new HashSet<AstmaraA>();
            StudentStatments = new HashSet<StudentStatment>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        public int? Academic { get; set; }

        public int? Virtual { get; set; }

        public int? Exprement { get; set; }

        public int? TotalHours { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AstmaraA> AstmaraAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStatment> StudentStatments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
