using DLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Services.Helper
{
    public static class GetIndex
    {

        public static int GetLastSTT(this Datacontext context, string table_name)
        {
            int val = 0;

            try
            {
                var param = new SqlParameter("@TableName", table_name);
                var xxx = context.RetrieveOrderRecords.FromSql("dbo.usp_GetLastOrder @TableName", param).FirstOrDefault();
                if (xxx == null) return 0;
                val = xxx.STT;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                val = 0;
            }

            return val;
        }
    }
}
