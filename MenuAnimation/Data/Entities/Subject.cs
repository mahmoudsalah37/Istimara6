using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    class Subject: BaseEntity
    {
        [MaxLength(100)]
        public string Code { get; set; }
        public int? Academic { get; set; }
        public int? Virtual { get; set; }
        public int? Exprement { get; set; }
        public int? TotalHours { get; set; }
        public virtual ICollection<StudentStatment> StudentStatments { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }

    }
}
