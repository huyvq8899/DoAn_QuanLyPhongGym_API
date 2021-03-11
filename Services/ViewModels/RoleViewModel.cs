using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            this.Status = true;
        }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool? Status { get; set; }
        public List<FunctionViewModel> ListFunction { get; set; }
        public string ParentRoleId { get; set; }
        public RoleViewModel ParentRoleViewModel { get; set; }
        public List<RoleViewModel> children { get; set; }
        //thêm
        public string Key { get; set; }
        public bool isLeaf { get; set; }
        public bool expanded { get; set; }
        public string Title { get; set; }
    }
}
