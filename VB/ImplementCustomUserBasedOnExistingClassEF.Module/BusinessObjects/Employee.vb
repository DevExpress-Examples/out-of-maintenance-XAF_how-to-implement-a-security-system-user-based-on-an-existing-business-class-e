Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Base.Security
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.Validation
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text

Namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects
    <DefaultClassOptions> _
    Public Class Employee
        Inherits Person
        Implements ISecurityUser, IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser, ISecurityUserWithRoles, IPermissionPolicyUser, ICanInitialize

        Public Sub New()
            MyBase.New()
            IsActive = True
            EmployeeRoles = New List(Of EmployeeRole)()
        End Sub

        Public Overridable Property OwnTasks() As IList(Of EmployeeTask)

        #Region "ISecurityUser Members"
        <RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save), RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save, "The login with the entered user name was already registered within the system.")> _
        Public Property UserName() As String Implements IAuthenticationActiveDirectoryUser.UserName
        Public Property IsActive() As Boolean
        Private ReadOnly Property UserNameInternal() As String Implements ISecurityUser.UserName, IAuthenticationStandardUser.UserName
            Get
                Return UserName
            End Get
        End Property
        Private ReadOnly Property IsActiveInternal() As Boolean Implements ISecurityUser.IsActive
            Get
                Return IsActive
            End Get
        End Property
#End Region

        #Region "IAuthenticationStandartUser Members"
        Public Property ChangePasswordOnFirstLogon() As Boolean Implements IAuthenticationStandardUser.ChangePasswordOnFirstLogon
        <Browsable(False), SecurityBrowsable> _
        Public Property StoredPassword() As String
        Private Function IAuthenticationStandardUser_ComparePassword(ByVal password As String) As Boolean Implements IAuthenticationStandardUser.ComparePassword
            Dim passwordCryptographer As New PasswordCryptographer()
            Return passwordCryptographer.AreEqual(StoredPassword, password)
        End Function
        Public Sub SetPassword(ByVal password As String) Implements IAuthenticationStandardUser.SetPassword
            Dim passwordCryptographer As New PasswordCryptographer()
            StoredPassword = passwordCryptographer.GenerateSaltedPassword(password)
        End Sub
        #End Region

        #Region "ISecurityUserWithRoles Members"
        Private ReadOnly Property ISecurityUserWithRoles_Roles() As IList(Of ISecurityRole) Implements ISecurityUserWithRoles.Roles
            Get
                Dim result As IList(Of ISecurityRole) = New List(Of ISecurityRole)()
                For Each role As EmployeeRole In EmployeeRoles
                    result.Add(role)
                Next role
                Return result
            End Get
        End Property
        #End Region
        <RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save, TargetCriteria := "IsActive", CustomMessageTemplate := "An active employee must have at least one role assigned")> _
        Public Overridable Property EmployeeRoles() As IList(Of EmployeeRole)

        #Region "IPermissionPolicyUser Members"
        Private ReadOnly Property IPermissionPolicyUser_Roles() As IEnumerable(Of IPermissionPolicyRole) Implements IPermissionPolicyUser.Roles
            Get
                Return EmployeeRoles.OfType(Of IPermissionPolicyRole)()
            End Get
        End Property
        #End Region

        #Region "ICanInitialize Members"
        Private Sub ICanInitialize_Initialize(ByVal objectSpace As IObjectSpace, ByVal security As SecurityStrategyComplex) Implements ICanInitialize.Initialize
            Dim newUserRole As EmployeeRole = CType(objectSpace.FindObject(Of EmployeeRole)(New BinaryOperator("Name", security.NewUserRoleName)), EmployeeRole)
            If newUserRole Is Nothing Then
                newUserRole = objectSpace.CreateObject(Of EmployeeRole)()
                newUserRole.Name = security.NewUserRoleName
                newUserRole.IsAdministrative = True
                newUserRole.Employees.Add(Me)
            End If
        End Sub
        #End Region
    End Class
End Namespace
