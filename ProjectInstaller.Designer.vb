<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Retention1 = New System.ServiceProcess.ServiceProcessInstaller()
        Me.Retention = New System.ServiceProcess.ServiceInstaller()
        '
        'Retention1
        '
        Me.Retention1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.Retention1.Password = Nothing
        Me.Retention1.Username = Nothing
        '
        'Retention
        '
        Me.Retention.ServiceName = "Retention"
        Me.Retention.ServicesDependedOn = New String() {"Will delete files in the ""C:\ProgramData\Gallagher\Backup "" Directory Automaticly" &
            " that ", "are 30 days old and older."}
        Me.Retention.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.Retention1, Me.Retention})

    End Sub

    Friend WithEvents Retention1 As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents Retention As ServiceProcess.ServiceInstaller
End Class
