using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects {
	public class ImplementCustomUserBasedOnExistingClassEFDbContext : DbContext {
		public ImplementCustomUserBasedOnExistingClassEFDbContext(String connectionString)
			: base(connectionString) {
		}
		public ImplementCustomUserBasedOnExistingClassEFDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
	}
}