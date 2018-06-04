Imports System
Imports System.Data
Imports System.Linq
Imports System.Data.Entity
Imports System.Data.Common
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports System.ComponentModel
Imports DevExpress.ExpressApp.EF.Updating
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy

Namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects
    Public Class ImplementCustomUserBasedOnExistingClassEFDbContext
        Inherits DbContext

        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub
        Public Sub New(ByVal connection As DbConnection)
            MyBase.New(connection, False)
        End Sub
        Public Property ModulesInfo() As DbSet(Of ModuleInfo)
        Public Property Roles() As DbSet(Of PermissionPolicyRole)
        Public Property TypePermissionObjects() As DbSet(Of PermissionPolicyTypePermissionObject)
        Public Property Users() As DbSet(Of PermissionPolicyUser)
        Public Property ModelDifferences() As DbSet(Of ModelDifference)
        Public Property ModelDifferenceAspects() As DbSet(Of ModelDifferenceAspect)
        Public Property Employees() As DbSet(Of Employee)
        Public Property EmployeeRoles() As DbSet(Of EmployeeRole)
        Public Property EmployeeTasks() As DbSet(Of EmployeeTask)
    End Class
End Namespace