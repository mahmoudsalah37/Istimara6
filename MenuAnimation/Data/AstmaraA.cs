namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AstmaraA")]
    public partial class AstmaraA
    {
        public int Id { get; set; }

        public int? IdBranch { get; set; }

        public int? IdSubject { get; set; }

        public int? IdTeacher { get; set; }

        public int? IdLevel { get; set; }

        public int? NumberOfSections { get; set; }

        public int? TotalPaper { get; set; }

        public int? TotalVirtual { get; set; }

        public int? TotalExperment { get; set; }

        public int? TotalSuperVision { get; set; }

        public int? NumOfPaper { get; set; }

        public int? NumberOfVirtual { get; set; }

        public int? NumberOfExprement { get; set; }

        public int? NumberOfSuperVision { get; set; }

        public int? NumOfStudent { get; set; }

        public int? TotalOfHour { get; set; }

        public int? SumOfSubject { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Level Level { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
