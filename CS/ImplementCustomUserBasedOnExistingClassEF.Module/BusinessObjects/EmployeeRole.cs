using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects {
    [ImageName("BO_Role")]
    public class EmployeeRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers {
        public EmployeeRole()
            : base() {
        }
        public virtual IList<Employee> Employees { get; set; }
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users {
            get { return Employees.OfType<IPermissionPolicyUser>(); }
        }
    }  
}
