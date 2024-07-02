using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Domain.ViewModel
{
    public class FeedBackVM
    {
        public List<ContactusData> ContactUsDataList { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public DateTime MessageDate { get; set; }

        public string CustomerMessage { get; set; }
    }
}
