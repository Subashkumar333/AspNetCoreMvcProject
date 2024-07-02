﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Domain.ViewModel
{
    public class CompletedOrderVM
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string OrderStatus { get; set; }
        public string PhoneNumber { get; set; }    
        public double TotalPrice { get; set; }
        public DateTime CreateOn { get; set; }       
        public DateTime ModifiedOn { get; set; }
    }
}
