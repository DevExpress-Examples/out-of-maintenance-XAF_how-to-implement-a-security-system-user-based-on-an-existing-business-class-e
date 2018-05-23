using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects {
    [DefaultClassOptions, ImageName("BO_Task")]
    public class EmployeeTask : Task {
        public EmployeeTask() : base() { }
        
        public virtual Employee Owner { get; set; }
    } 
}
