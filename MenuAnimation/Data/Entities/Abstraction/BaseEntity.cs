using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Data.Entities.Abstraction
{
    class BaseEntity:BaseIdEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
