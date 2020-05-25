Imports System.IO
Imports System.Text
Imports System.Security.Principal
Public Class FormBase

    Dim versionName As String, infection As Integer
    Dim identity = WindowsIdentity.GetCurrent()
    Dim program = New WindowsPrincipal(identity)
    Dim isAdmin As Boolean = program.IsInRole(WindowsBuiltInRole.Administrator)

    Private Sub LaunchCheck() Handles BtnCheck.Click
        Dim sessionUserName As String = Environment.UserName
        For Each Dir As String In System.IO.Directory.GetDirectories("C:\Users\" & sessionUserName & "\AppData\Roaming\Discord")
            Dim dirInfo As New System.IO.DirectoryInfo(Dir)
            If Strings.Left(dirInfo.Name, 1) = "0" Or
                Strings.Left(dirInfo.Name, 1) = "1" Or
                Strings.Left(dirInfo.Name, 1) = "2" Or
                Strings.Left(dirInfo.Name, 1) = "3" Or
                Strings.Left(dirInfo.Name, 1) = "4" Or
                Strings.Left(dirInfo.Name, 1) = "5" Or
                Strings.Left(dirInfo.Name, 1) = "6" Or
                Strings.Left(dirInfo.Name, 1) = "7" Or
                Strings.Left(dirInfo.Name, 1) = "9" Or
                Strings.Left(dirInfo.Name, 1) = "8" Then
                versionName = dirInfo.Name
            End If
        Next

        Using sr As StreamReader = File.OpenText("C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_modules\index.js")
            If sr.ReadLine() <> "module.exports = require('./discord_modules.node');" Then
                infection = 1
            Else
                infection = 0
            End If
        End Using

        Using sr As StreamReader = File.OpenText("C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_desktop_core\index.js")
            If sr.ReadLine() <> "module.exports = require('./core.asar');" Then
                If infection = 1 Then
                    infection = 3
                Else
                    infection = 2
                End If
            Else
                infection = 0
            End If
        End Using

        Select Case infection
            Case 1
                MsgBox("The file 'C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_modules\index.js' have been infected." &
                       Chr(10) & Chr(10) & "To avoid stealing of your accounts, passwords, copyboard and so on... Please reinstall Discord (desktop version)")

            Case 2
                MsgBox("The file 'C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_desktop_core\index.js' have been infected." &
                       Chr(10) & Chr(10) & "To avoid stealing of your accounts, passwords, copyboard and so on... Please reinstall Discord (desktop version)")

            Case 3
                MsgBox("Both files 'C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_desktop_core\index.js' et 'C:\Users\" & sessionUserName & "\AppData\Roaming\Discord\" & versionName & "\modules\discord_modules\index.js' have been infected." &
                       Chr(10) & Chr(10) & "To avoid stealing of your accounts, passwords, copyboard and so on... Please reinstall Discord (desktop version)")
            Case 0
                MsgBox("Your Discord installation have not been compromised, no need to reinstall it." & Chr(10) & "Please stay aware of the links you click on and the servers you may join.")
        End Select

    End Sub

End Class
