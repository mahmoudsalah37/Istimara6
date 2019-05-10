using Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    class User: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public String Password { get; set; }
    }
}
