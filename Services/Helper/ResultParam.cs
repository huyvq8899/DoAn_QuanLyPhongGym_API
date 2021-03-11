using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementServices.Helper
{
    public class ResultParam
    {
        public UserViewModel User { get; set; }
        public bool Result { get; set; }
        public string FileName { get; set; }
        public int UId { get; set; }
        public string HinhAnh { get; set; }
        public string Files { get; set; }
        public string FileError { get; set; }
    }
}
