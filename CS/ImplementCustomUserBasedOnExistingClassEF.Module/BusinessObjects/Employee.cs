using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Employee : Person, ISecurityUser, IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser, ISecurityUserWithRoles, IPermissionPolicyUser, ICanInitialize {
        public Employee() : base() {
            IsActive = true;
            EmployeeRoles = new List<EmployeeRole>();
        }

        public virtual IList<EmployeeTask> OwnTasks { get; set; }

        #region ISecurityUser Members
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
         "The login with the entered user name was already registered within the system.")]
        public string UserName { get; set; }
        public bool IsActive { get; set; }  
        #endregion

        #region IAuthenticationStandartUser Members
        public bool ChangePasswordOnFirstLogon { get; set; }
        [Browsable(false), SecurityBrowsable]
        public string StoredPassword { get; set; }
        bool IAuthenticationStandardUser.ComparePassword(string password) {
            PasswordCryptographer passwordCryptographer = new PasswordCryptographer();
            return passwordCryptographer.AreEqual(StoredPassword, password);
        }
        public void SetPassword(string password) {
            PasswordCryptographer passwordCryptographer = new PasswordCryptographer();
            StoredPassword = passwordCryptographer.GenerateSaltedPassword(password);
        }
        #endregion

        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles {
            get {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach(EmployeeRole role in EmployeeRoles) {
                    result.Add(role);
                }
                return result;
            }
        }  
        #endregion
        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save, TargetCriteria = "IsActive", CustomMessageTemplate = "An active employee must have at least one role assigned")]
        public virtual IList<EmployeeRole> EmployeeRoles { get; set; }

        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles {
            get { return EmployeeRoles.OfType<IPermissionPolicyRole>(); }  
        } 
        #endregion

        #region ICanInitialize Members
        void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security) {
            EmployeeRole newUserRole = (EmployeeRole)objectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", security.NewUserRoleName));
            if(newUserRole == null) {
                newUserRole = objectSpace.CreateObject<EmployeeRole>(); 
                newUserRole.Name = security.NewUserRoleName; 
                newUserRole.IsAdministrative = true; 
                newUserRole.Employees.Add(this);
            }  
        } 
        #endregion
    } 
}
