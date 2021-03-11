using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helper
{
    public class ParamTask
    {
        public string UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderBy { get; set; } // 0 ngày tạo, 1 deadline
        public string OrderByCreateDate { get; set; } // 0 ngày tạo, 1 deadline
        public string OrderByTotalTask { get; set; } // 0 ngày tạo, 1 deadline

        public string ProjectId { get; set; }
        public string SearchKey { get; set; }

        public string colAssignValue { get; set; }
        public string colResultValue { get; set; }
        public string colProjectValue { get; set; }
        public string colGroupValue { get; set; }
    }
}
