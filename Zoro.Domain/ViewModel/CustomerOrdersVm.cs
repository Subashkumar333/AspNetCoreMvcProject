using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Domain.ViewModel
{
    public class CustomerOrdersVm
    {
        public OrderProducts orderProducts { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Orderstatus { get; set; }



    }
}
