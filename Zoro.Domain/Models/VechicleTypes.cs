using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Common;

namespace Zoro.Domain.Models
{
    public class VechicleTypes:BaseModel
    {
        [Required]
        public string Name { get; set; } 





    }
}
