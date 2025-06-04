Imports System.IO
Imports System.Security.AccessControl
Public Class Form1
    Dim txtCommand1, txtCommand2, txtCommand3, txtCommand4, txtCommand5, txtCommand6, txtCommand7, retText As String
    Dim ComboBox1SelectedIndex As Int16
    Dim RemoveGroups As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCommand1 = ""
        txtCommand2 = ""
        txtCommand3 = ""
        txtCommand4 = ""
        txtCommand5 = ""
        txtCommand6 = ""
        Load_USB_Drive()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | icacls " & ComboBox1.Text & "\* /T /Q /C /RESET"
        txtCommand2 = ""
        Button1.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | TAKEOWN /F " & ComboBox1.Text & "\ /R /A"
        txtCommand2 = ""
        Button2.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | icacls " & ComboBox1.Text & "\ /setowner " & "Everyone" & " /T /C"
        txtCommand2 = ""
        Button3.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "attrib -r -a -s -h " & ComboBox1.Text & "\*  /s /d /l"
        txtCommand2 = ""
        Button4.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If Button5.Text = "+" Then
            Me.Width = 575
            Me.Height = 455
            Button5.Text = "-"
        Else
            Button5.Text = "-"
            Me.Width = 575
            Me.Height = 260
            Button5.Text = "+"
        End If

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | TAKEOWN " & " /F " & ComboBox1.Text & "\ /R /A /SKIPSL"
        txtCommand2 = "ECHO Y | icacls " & ComboBox1.Text & " /grant Everyone:(OI)(CI)F"
        Button6.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | icacls " & ComboBox1.Text & " /grant:r Everyone:(OI)(CI)RX /T"
        txtCommand2 = "ECHO Y | icacls " & ComboBox1.Text & " /deny Everyone:(OI)(CI)(WD,AD,WA,WEA,DE,DC)"
        Button7.Text = "Wait.."
        BackgroundWorker1.RunWorkerAsync() 'do magic

    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ComboBox1.Items.Clear()
        Load_USB_Drive()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ComboBox1SelectedIndex = ComboBox1.SelectedIndex

        txtCommand1 = "ECHO Y | icacls " & ComboBox1.Text & "\ /setowner " & "Everyone" & " /T /C"
        txtCommand2 = ""
        txtCommand3 = "icacls " & ComboBox1.Text & "\ /remove:g *S-1-3-0 /T /Q /C"
        txtCommand4 = "icacls " & ComboBox1.Text & "\ /remove:g *S-1-5-32-544 /T /Q /C"
        txtCommand5 = "icacls " & ComboBox1.Text & "\ /remove:g *S-1-5-11 /T /Q /C"
        txtCommand6 = "icacls " & ComboBox1.Text & "\ /remove:g *S-1-5-18 /T /Q /C"
        txtCommand7 = "icacls " & ComboBox1.Text & "\ /remove:g *S-1-5-32-545 /T /Q /C"
        Button9.Text = "Wait.."
        RemoveGroups = True

        BackgroundWorker1.RunWorkerAsync() 'do magic
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        MsgBox("Al-Qaisar Technologies Developing Team: " & vbCrLf & "Coding: Sadiq Al-Mohandis techwizard " & vbCrLf & "Testing: Mohammed Ahmed DarkTek", MsgBoxStyle.Information, "QaisaTek Advanced Drives and Permission")
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        ComboBox1.Items.Clear()
        Load_USB_Drive()

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        lstFolderSecurity.Items.Clear()
        Check_Permissions()

    End Sub
    Private Sub Check_Permissions()
        On Error Resume Next

        Dim ds As DirectorySecurity = Directory.GetAccessControl(ComboBox1.Text)
        If ds IsNot Nothing Then
            For Each rule As FileSystemAccessRule In ds.GetAccessRules(True, True, GetType(Security.Principal.NTAccount))
                If Not IsNumeric(rule.FileSystemRights.ToString) Then
                    lstFolderSecurity.Items.Add($"{rule.IdentityReference.Value} : {rule.FileSystemRights} : {rule.AccessControlType}")
                End If

            Next
        End If

    End Sub
    Private Sub Load_USB_Drive()
        For Each Drive In My.Computer.FileSystem.Drives
            If CheckBox1.Checked = True Then
                If Drive.DriveType = IO.DriveType.Fixed Or Drive.DriveType = IO.DriveType.Removable Then
                    'ComboBox1.Items.Add(Drive.Name)
                    ComboBox1.Items.Add(Drive.Name.TrimEnd("\"c))
                End If
            Else
                If Drive.DriveType = IO.DriveType.Removable Then

                    'ComboBox1.Items.Add(Drive.Name)
                    ComboBox1.Items.Add(Drive.Name.TrimEnd("\"c))

                End If
            End If
        Next
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If

    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.CreateNoWindow = True  '<---- if you want to not create a window
        StartInfo.UseShellExecute = False 'required to redirect

        myprocess.StartInfo = StartInfo
        myprocess.Start()
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine(txtCommand1) 'the command you wish to run.....
        SW.WriteLine(txtCommand2) 'the command you wish to run.....
        If RemoveGroups Then
            SW.WriteLine(txtCommand3) 'the command you wish to run.....
            SW.WriteLine(txtCommand4) 'the command you wish to run.....
            SW.WriteLine(txtCommand5) 'the command you wish to run.....
            SW.WriteLine(txtCommand6) 'the command you wish to run.....
            SW.WriteLine(txtCommand7) 'the command you wish to run.....
        End If
        SW.WriteLine("exit") 'exits command prompt window
        retText = SR.ReadToEnd.ToString

        SW.Close()
        SR.Close()
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MsgBox("The Operation Compeleted Successfully!", MsgBoxStyle.OkOnly & MsgBoxStyle.Information, "QaisarTek Drive Permission 2024")

        TextBox1.Text = retText
        Button1.Text = "Reset Permission"
        Button2.Text = "Take Ownership"
        Button3.Text = "Set Owner to Everyone"
        Button4.Text = "Reset Hidden and System Files"
        Button6.Text = "Unlock Permission"
        Button7.Text = "Lock Permission"
        Button9.Text = "Remove Groups And Users"
        RemoveGroups = False

        ComboBox1.Items.Clear()
        Load_USB_Drive()

        lstFolderSecurity.Text = ""
        ComboBox1.SelectedIndex = 0
        ComboBox1.SelectedIndex = ComboBox1SelectedIndex

    End Sub

End Class
