using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkShopNET5.Model.Store
{
    public class GetAPICheckEmployeeNowByITAccount_Result
    {
        public string EmailCMU { get; set; }
        public string FirstNameTha { get; set; }
        public string MiddleNameTha { get; set; }
        public string LastNameTha { get; set; }
        public string FirstNameEng { get; set; }
        public string MiddleNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string PicEmpPath { get; set; }
        public System.Guid EmployeeID { get; set; }
        public string PositionName { get; set; }
    }
}
