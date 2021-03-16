using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopNET5.Model.Interface;
using WorkShopNET5.Model.Store;

namespace WorkShopNET5.Model.Repository
{
    public class HrStoreRepository : IHrStoreRepository
    {
        private StoreMISPortalDBContext _storeMISPortalDBContext;
        public HrStoreRepository(StoreMISPortalDBContext StoreMISPortalDBContext)
        {
            _storeMISPortalDBContext = StoreMISPortalDBContext;
        }

        public async Task<List<GetAPICheckEmployeeNowByITAccount_Result>> GetAPICheckEmployeeNowByITAccount_Result(string ITAccount)
        {
            string query = "exec GetAPICheckEmployeeNowByITAccount @ITAccount";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ITAccount", ITAccount);
            return _storeMISPortalDBContext.GetAPICheckEmployeeNowByITAccount_Result.FromSqlRaw<GetAPICheckEmployeeNowByITAccount_Result>(query, param).ToList();
           
        }
    }
}
