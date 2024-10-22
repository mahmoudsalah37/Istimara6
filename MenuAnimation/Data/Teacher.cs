namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            AstmaraAs = new HashSet<AstmaraA>();
            AstmaraBs = new HashSet<AstmaraB>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        public int? IdWorkHours { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? idSection { get; set; }

        public int? TotalHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AstmaraA> AstmaraAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AstmaraB> AstmaraBs { get; set; }

        public virtual Section Section { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }

        public virtual WorkHour WorkHour { get; set; }
    }
}
