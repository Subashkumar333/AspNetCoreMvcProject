using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Common;

namespace Zoro.Domain.Models
{
    public class Brand:BaseModel
    {
       

        [Required]
        public string Name { get; set; }


        public int PublishYear { get; set; }


        public string BrandLogo { get; set; }



    }
}
