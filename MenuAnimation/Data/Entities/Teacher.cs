using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    class Teacher: BaseEntity
    {
        public String NickName { get; set; }
        [ForeignKey("WorkHour")]
        public int IdWorkHours { get; set; }
        public virtual WorkHour WorkHour{ get; set; }
        public virtual ICollection<SubjectTeacher>SubjectTeachers { get; set; }

    }
}
