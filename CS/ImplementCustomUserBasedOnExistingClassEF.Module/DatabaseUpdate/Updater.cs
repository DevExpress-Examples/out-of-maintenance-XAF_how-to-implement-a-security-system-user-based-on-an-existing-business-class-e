using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects;

namespace ImplementCustomUserBasedOnExistingClassEF.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            EmployeeRole adminEmployeeRole = ObjectSpace.FindObject<EmployeeRole>(
         new BinaryOperator("Name", "AdminRole"));
            if(adminEmployeeRole == null) {
                adminEmployeeRole = ObjectSpace.CreateObject<EmployeeRole>();
                adminEmployeeRole.Name = "AdminRole";
                adminEmployeeRole.IsAdministrative = true;
            }
            Employee adminEmployee = ObjectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", "Admin"));
            if(adminEmployee == null) {
                adminEmployee = ObjectSpace.CreateObject<Employee>();
                adminEmployee.UserName = "Admin";
                adminEmployee.SetPassword("");
                adminEmployee.EmployeeRoles.Add(adminEmployeeRole);
            }
            ObjectSpace.CommitChanges();  
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
