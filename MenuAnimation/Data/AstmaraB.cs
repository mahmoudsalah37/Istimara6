namespace Astmara6.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AstmaraB")]
    public partial class AstmaraB
    {
        public int Id { get; set; }

        public int? IdDoctor { get; set; }

        public int? IdWorkHours { get; set; }

        public string Subject { get; set; }

        public int? IdLevel { get; set; }

        public int? Paper { get; set; }

        public int? Virtial { get; set; }

        public int? Experment { get; set; }

        public int? Sum { get; set; }

        public int? Total { get; set; }

        public virtual Level Level { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual WorkHour WorkHour { get; set; }
    }
}
