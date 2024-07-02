using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Domain.Common
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }


        public DateTime CreateOn { get; set; }


        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }



    }
}
