using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Abstraction
{
    class BaseIdEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
