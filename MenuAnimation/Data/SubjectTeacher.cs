namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubjectTeacher
    {
        public int Id { get; set; }

        public int? IdBranch { get; set; }

        public int? IdSubject { get; set; }

        public int? IdTeacher { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
