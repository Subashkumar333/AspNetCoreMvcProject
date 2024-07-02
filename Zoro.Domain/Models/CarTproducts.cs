using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Common;

namespace Zoro.Domain.Models
{
    public class CarTproducts:BaseModel
    {
        

        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string Image { get; set; }


    }
}
