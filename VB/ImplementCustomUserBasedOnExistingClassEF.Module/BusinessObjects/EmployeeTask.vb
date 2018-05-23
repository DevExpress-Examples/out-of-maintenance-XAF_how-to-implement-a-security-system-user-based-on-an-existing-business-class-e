Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ImplementCustomUserBasedOnExistingClassEF.Module.BusinessObjects
    <DefaultClassOptions, ImageName("BO_Task")> _
    Public Class EmployeeTask
        Inherits Task

        Public Sub New()
            MyBase.New()
        End Sub

        Public Overridable Property Owner() As Employee
    End Class
End Namespace
