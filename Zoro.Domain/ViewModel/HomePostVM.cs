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
    public class HomePostVM
    {
        public List<Post> Posts { get; set; }

        public string? searchBox { get; set; } = string.Empty;

        public Guid? BrandId { get; set; }

        public Guid? vehicleTypesId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> BrandList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleTypeList { get; set; }


    }
}
