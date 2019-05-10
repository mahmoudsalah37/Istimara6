using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    class Section : BaseIdEntity
    {
        public string TypeOfSection { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }

    }
}
