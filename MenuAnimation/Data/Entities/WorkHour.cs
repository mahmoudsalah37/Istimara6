using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    class WorkHour: BaseIdEntity
    {
        public double GPA { get; set; }

        public int Quorum { get; set; }

        public bool AcademicOrVirtual { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }




    }
}
