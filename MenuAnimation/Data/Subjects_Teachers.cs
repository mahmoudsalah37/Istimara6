namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subjects_Teachers
    {
        public int ID { get; set; }

        public int? id_branch { get; set; }

        public int? id_subject { get; set; }

        public int? id_teacher { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
