﻿Imports System.ComponentModel
Imports System.Configuration.Install

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Sub Retention_AfterInstall(sender As Object, e As InstallEventArgs) Handles Retention.AfterInstall

    End Sub
End Class
