using System.Collections.Generic;

namespace ManagementServices.Helper
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string Keyword { get; set; }
        public string SortValue { get; set; }
        public string SortKey { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string KeywordCol { get; set; }
        public string ColName { get; set; }
        public string SelectedUser { get; set; }
        public string userId { get; set; }
        public string projectId { get; set; }
        public string roomId { get; set; }
        public string groupId { get; set; }

        public bool? fromMe { get; set; }
        public bool? toMe { get; set; }
        public bool? iSpy { get; set; }
        public int? LoaiTieuChi { get; set; }
        public int? LoaiCanhBao { get; set; }
        public int? TrangThaiDoanhNghiep { get; set; }
        public List<Filter> Filter { get; set; }
        public string listCreateById { get; set; }
        public string listProcessId { get; set; }
        public string type { get; set; }
    }

    public class Filter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
