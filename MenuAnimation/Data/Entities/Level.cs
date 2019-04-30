using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    class Level: BaseEntity
    {
        public virtual ICollection<StudentStatment> StudentStatments { get; set; }

    }
}
