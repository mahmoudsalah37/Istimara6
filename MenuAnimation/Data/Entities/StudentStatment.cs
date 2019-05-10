using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    class StudentStatment: BaseIdEntity
    {
        [ForeignKey("Level")]
        public int? IdLevel { get; set; }
        [ForeignKey("Branch")]
        public int? IdBranch { get; set; }
        [ForeignKey("Subject")]
        public int? IdSubject { get; set; }
        public int? NumberOfStudent { get; set; }
        public  virtual Branch Branch{ get; set; }
        public virtual Level Level { get; set; }
        public virtual Subject Subject { get; set; }




    }
}
