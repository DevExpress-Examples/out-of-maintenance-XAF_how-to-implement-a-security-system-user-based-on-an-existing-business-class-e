Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.EF
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy
Imports ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects

Namespace ImplementCustomUserBasedOnExistingClassEF.Module.DatabaseUpdate
    ' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim adminEmployeeRole As EmployeeRole = ObjectSpace.FindObject(Of EmployeeRole)(New BinaryOperator("Name", "AdminRole"))
            If adminEmployeeRole Is Nothing Then
                adminEmployeeRole = ObjectSpace.CreateObject(Of EmployeeRole)()
                adminEmployeeRole.Name = "AdminRole"
                adminEmployeeRole.IsAdministrative = True
            End If
            Dim adminEmployee As Employee = ObjectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", "Admin"))
            If adminEmployee Is Nothing Then
                adminEmployee = ObjectSpace.CreateObject(Of Employee)()
                adminEmployee.UserName = "Admin"
                adminEmployee.SetPassword("")
                adminEmployee.EmployeeRoles.Add(adminEmployeeRole)
            End If
            ObjectSpace.CommitChanges()
        End Sub
        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        End Sub
    End Class
End Namespace
