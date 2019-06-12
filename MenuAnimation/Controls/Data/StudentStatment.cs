namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentStatment
    {
        public int Id { get; set; }

        public int? IdLevel { get; set; }

        public int? IdBranch { get; set; }

        public int? IdSubject { get; set; }

        public int? NumberOfStudent { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Level Level { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
