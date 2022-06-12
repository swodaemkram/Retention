Public Class Retention


    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim Timer1 As New System.Timers.Timer

        AddHandler Timer1.Elapsed, AddressOf Timer1_Elapsed

        With Timer1
            .Interval = 1000
            .Enabled = True
        End With


        Timer1.Start()
        WriteLog(Me.ServiceName & " Started")

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        WriteLog(Me.ServiceName & " Stopped")
    End Sub


    Private Sub WriteLog(ByVal strMessage As String)

        Dim strPath As String, file As System.IO.StreamWriter
        strPath = AppDomain.CurrentDomain.BaseDirectory & "\Retention.log"
        file = New System.IO.StreamWriter(strPath, True)
        file.WriteLine(strMessage)
        file.Close()

    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As EventArgs)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("C:\ProgramData\Gallagher\Backup\") ' Go through each file in the directory and add is to the lost box

            Dim FileCreationTime = FileDateTime(foundFile)                                                  ' Get the creation date of each file in the Directory
            Dim DateFileCreated As Date = FileDateTime(foundFile).Date                                      'What date was the file created on
            Dim CurrentTimeNow As Date = Now                                                                'What time is it right now   
            Dim DaysOld = DateDiff(DateInterval.Day, DateFileCreated, CurrentTimeNow)                       'How many days diffrents between today and the file creation date

            Dim TheCurrentTime = Now.ToString("HH:mm")                                                      ' format the current time 
            'WriteLog(Me.ServiceName & "Checking Directory")
            If TheCurrentTime = "12:00" Then                                                                'Delete Files at this time of day 
                Dim INTDaysOld As Integer = Convert.ToInt32(DaysOld)                                        'Convert day into integer so we can compare it with a number

                If DaysOld > 30 Then                                                                        'If older then this many days 

                    My.Computer.FileSystem.DeleteFile(foundFile)                                            'Delete the file

                End If

            End If

        Next
    End Sub
End Class
