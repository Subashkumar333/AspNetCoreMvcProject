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
    public class PostVM
    {
        public Post Post {  get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> BrandList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleTypeList { get; set; }

        public IEnumerable<SelectListItem> BycycleCategories { get; set; }


        public IEnumerable<SelectListItem> BycycleTypes { get; set; }


    }
}
