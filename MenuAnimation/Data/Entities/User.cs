using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    class User: BaseEntity
    {
        public String Password { get; set; }
    }
}
