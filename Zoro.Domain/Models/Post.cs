using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Common;
using Zoro.Domain.ApplicationEnums;

namespace Zoro.Domain.Models
{
    public class Post: BaseModel
    {
       
            [Display(Name = "Brand")]
            public Guid BrandId { get; set; }

            [ValidateNever]
            [ForeignKey("BrandId")]
            public Brand Brand { get; set; }

            [Display(Name = "Vehicle Type")]
            public Guid VehicleTypeId { get; set; }

            [ValidateNever]
            [ForeignKey("VehicleTypeId")]
            public VechicleTypes VechicleTypes { get; set; }

            public string Name { get; set; }

            [Display(Name = "Select Categories")]
            public BycycleCategories BycycleCategories { get; set; }

            [Display(Name = "Select BycycleTypes")]
            public BycycleTypes BycycleTypes { get; set; }


            [Display(Name = "Base Price")]
            public double PriceFrom { get; set; }

            [Display(Name = "Top-End Price")]
            public double PriceTo { get; set; }


            [Range(1, 5, ErrorMessage = "Rating Should be from 1 to 5 only")]
            public int Ratings { get; set; }

            [Display(Name = "Upload Vehicle Image")]
            public string VehicleImage { get; set; }
        }

    
}
