Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects
    <ImageName("BO_Role")> _
    Public Class EmployeeRole
        Inherits PermissionPolicyRoleBase
        Implements IPermissionPolicyRoleWithUsers

        Public Sub New()
            MyBase.New()
        End Sub
        Public Overridable Property Employees() As IList(Of Employee)
        Private ReadOnly Property IPermissionPolicyRoleWithUsers_Users() As IEnumerable(Of IPermissionPolicyUser) Implements IPermissionPolicyRoleWithUsers.Users
            Get
                Return Employees.OfType(Of IPermissionPolicyUser)()
            End Get
        End Property
    End Class
End Namespace
