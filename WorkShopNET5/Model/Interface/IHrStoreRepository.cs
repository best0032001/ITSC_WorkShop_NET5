using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopNET5.Model.Store;

namespace WorkShopNET5.Model.Interface
{
    public interface IHrStoreRepository
    {
        Task<List<GetAPICheckEmployeeNowByITAccount_Result>> GetAPICheckEmployeeNowByITAccount_Result(String ITAccount);
    }
}
