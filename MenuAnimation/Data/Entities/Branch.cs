using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    class Branch: BaseEntity
    {
        [ForeignKey("Section")]
        public int? IdSection { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<StudentStatment> StudentStatments{ get; set; }
        public virtual ICollection<SubjectTeacher>  SubjectTeachers { get; set; }

    }
}
