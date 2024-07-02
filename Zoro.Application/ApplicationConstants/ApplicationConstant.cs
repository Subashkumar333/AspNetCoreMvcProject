using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Application.ApplicationConstants
{
    public class ApplicationConstant
    {
       
    }

    public static class CommanMessage
    {
        public static string RecordCreated = "Record Created SuccessFully";
        public static string RecordUpdated = "Record Updated SuccessFully";
        public static string RecordDeleted = "Record Deleted SuccessFully";


    }

    public static class CustomRole
    {
        public const string MasterAdmin = "MASTERADMIN";
        public const string Admin = "ADMIN";
        public const string Customer= "CUSTOMER";


    }

}
