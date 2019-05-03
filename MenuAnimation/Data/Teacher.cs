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
            Subjects_Teachers = new HashSet<Subjects_Teachers>();
        }

        public int ID { get; set; }

        public string name { get; set; }

        public string nickname { get; set; }

        public int? ID_WorkHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subjects_Teachers> Subjects_Teachers { get; set; }

        public virtual WorkHour WorkHour { get; set; }
    }
}
