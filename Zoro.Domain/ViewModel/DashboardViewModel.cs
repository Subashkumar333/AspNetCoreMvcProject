using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Domain.ViewModel
{
    public class DashboardViewModel
    {
        public int TotalOrders { get; set; }
       
        public int TotalPendingOrders { get; set; }

        public int TotalReadyToPickOrders { get; set; }


        public int TotalCompletedOrder { get; set; }

        public double TotalAmount{ get; set; }

        public int TotalFeedBack { get; set; }

        public int TotalCustomers { get; set; }  
    }
}
