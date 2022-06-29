Public Class Retention

    Dim PathToMonitor
    Dim DaysToKeep As Integer
    Dim TimeOfDayToDeleteFiles


    Protected Overrides Sub OnStart(ByVal args() As String)


        Dim Timer1 As New System.Timers.Timer



        'Dim PathToMonitor As String
        'Dim DaysToKeep As Integer
        'Dim ExtentionToMonitor As String

        PathToMonitor = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Retention", "PathToMonitor", Nothing)
        DaysToKeep = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Retention", "DaysToKeep", Nothing)
        TimeOfDayToDeleteFiles = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Retention", "TimeOfDayToDeleteFiles", Nothing)

        If PathToMonitor = "" Then PathToMonitor = "C:\ProgramData\Gallagher\Backup\"
        If DaysToKeep = Nothing Then DaysToKeep = 30
        If TimeOfDayToDeleteFiles = Nothing Then TimeOfDayToDeleteFiles = "12:34"


        AddHandler Timer1.Elapsed, AddressOf Timer1_Elapsed

        With Timer1
            .Interval = 1000
            .Enabled = True
        End With


        Timer1.Start()

        WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Version 1.0.2 Started")
        WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Using ..." & PathToMonitor & " As path to monitor")
        WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Using ..." & DaysToKeep & " Days To Keep ")
        WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Using ..." & TimeOfDayToDeleteFiles & " As time of day to delete files")


        'T_O_D_T_D_F = Convert.ToDateTime(TimeOfDayToDeleteFiles)

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Stopped")
    End Sub


    Private Sub WriteLog(ByVal strMessage As String)

        Dim strPath As String, file As System.IO.StreamWriter
        strPath = AppDomain.CurrentDomain.BaseDirectory & "\Retention.log"
        file = New System.IO.StreamWriter(strPath, True)
        file.WriteLine(strMessage)
        file.Close()

    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As EventArgs)

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(PathToMonitor) ' Go through each file in the directory and add is to the lost box

            Dim FileCreationTime = FileDateTime(foundFile)                                                  ' Get the creation date of each file in the Directory
            Dim DateFileCreated As Date = FileDateTime(foundFile).Date                                      'What date was the file created on
            Dim CurrentTimeNow As Date = Now                                                                'What time is it right now   
            Dim DaysOld = DateDiff(DateInterval.Day, DateFileCreated, CurrentTimeNow)                       'How many days diffrents between today and the file creation date

            Dim TheCurrentTime = Now.ToString("HH:mm")                                                      ' format the current time 
            'WriteLog(Me.ServiceName & "Checking Directory")

            Dim TOD As String = CType(TimeOfDayToDeleteFiles, String)

            'WriteLog(TOD & " AND " & TheCurrentTime)

            If TheCurrentTime = TOD Then                                                                'Delete Files at this time of day 

                'WriteLog("Reached Right Time Of Day")

                Dim INTDaysOld As Integer = Convert.ToInt32(DaysOld)                                        'Convert day into integer so we can compare it with a number

                'WriteLog(foundFile & " is " & INTDaysOld)

                If INTDaysOld > DaysToKeep Then                                                                        'If older then this many days 

                    WriteLog(DateString & " " & TimeString & " " & Me.ServiceName & " Deleted File " & foundFile & " From " & PathToMonitor & " Because it was " & INTDaysOld & " Days Old !")
                    My.Computer.FileSystem.DeleteFile(foundFile)                                            'Delete the file


                End If

            End If

        Next
    End Sub
End Class
