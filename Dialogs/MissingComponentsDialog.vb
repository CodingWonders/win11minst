Imports System.Windows.Forms
Imports System.IO
Imports System.IO.Compression
Imports System.Net

Public Class MissingComponentsDialog
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Dim WebSite As String
    Dim TargetFile As String
    Dim TargetPath As String = ".\prog_bin"
    Dim CheckedCount As Integer = 0
    Dim CheckCount As Integer = 3
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click
        MainForm.Notify.Visible = False
        End
    End Sub

    Private Sub closeBox_MouseEnter(sender As Object, e As EventArgs) Handles closeBox.MouseEnter
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub closeBox_MouseLeave(sender As Object, e As EventArgs) Handles closeBox.MouseLeave
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        closeToolTip.SetToolTip(closeBox, "Exit")
    End Sub

    Private Sub closeBox_MouseDown(sender As Object, e As MouseEventArgs) Handles closeBox.MouseDown
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox_down)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark_down)
        End If
    End Sub

    Private Sub closeBox_MouseUp(sender As Object, e As MouseEventArgs) Handles closeBox.MouseUp
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub MissingComponentsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.FlatStyle = FlatStyle.System
        Button2.FlatStyle = FlatStyle.System
        Button3.FlatStyle = FlatStyle.System
        Button4.FlatStyle = FlatStyle.System
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.FromArgb(243, 243, 243)
            ForeColor = Color.Black
            closeBox.Image = New Bitmap(My.Resources.closebox)
            CheckPic1.Image = New Bitmap(My.Resources.check)
            CheckPic2.Image = New Bitmap(My.Resources.check)
            CheckPic3.Image = New Bitmap(My.Resources.check)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(32, 32, 32)
            ForeColor = Color.White
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
            CheckPic1.Image = New Bitmap(My.Resources.check_dark)
            CheckPic2.Image = New Bitmap(My.Resources.check_dark)
            CheckPic3.Image = New Bitmap(My.Resources.check_dark)
        End If
        If Not Directory.Exists(".\prog_bin") Then
            Directory.CreateDirectory(".\prog_bin")
        End If
        If Environment.Is64BitOperatingSystem = True Then
            If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
                CheckPic1.Visible = False
                Button2.Visible = True
            Else
                CheckPic1.Visible = True
                Button2.Visible = False
                CheckedCount = CheckedCount + 1
            End If
            If Not File.Exists("\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Deployment Tools\amd64\Oscdimg\oscdimg.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
                CheckedCount = CheckedCount + 1
            End If
        Else
            If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
                CheckPic1.Visible = False
                Button2.Visible = True
            Else
                CheckPic1.Visible = True
                Button2.Visible = False
                CheckedCount = CheckedCount + 1
            End If
            If Not File.Exists("\Program Files\Windows Kits\10\Assessment and Deployment Kit\Deployment Tools\x86\Oscdimg\oscdimg.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
                CheckedCount = CheckedCount + 1
            End If
        End If
        If File.Exists("\Windows\system32\dism.exe") Then
            CheckPic2.Visible = True
            CheckedCount = CheckedCount + 1
        Else
            CheckPic2.Visible = False
        End If
        If CheckedCount = 3 Then
            Label10.Visible = True
            Button1.Visible = True
        End If
    End Sub

    Private Sub MissingComponentsDialog_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    Private Sub MissingComponentsDialog_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    Private Sub MissingComponentsDialog_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WebSite = "http://www.sevenforums.com/attachments/general-discussion/32382d1256189124-make-bootable-iso-student-d-l-oscdimg.zip"
        TargetFile = "oscdimg.zip"
        Using OSCDIMG As New WebClient()
            OSCDIMG.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36 Edg/83.0.478.45")
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            OSCDIMG.DownloadFile(WebSite, TargetPath & "\" & TargetFile)
        End Using
        Label11.Visible = True
        Process.Start(".\prog_bin\oscdimg.zip")
        Do Until File.Exists(".\prog_bin\oscdimg.exe")
            If Not File.Exists(".\prog_bin\oscdimg.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
            End If
        Loop
        Label11.Visible = False
        If Not File.Exists(".\prog_bin\oscdimg.exe") Then
            Button3.Visible = True
            CheckPic3.Visible = False
        Else
            Button3.Visible = False
            CheckPic3.Visible = True
        End If
        File.Delete(".\oscdimg.zip")
        CheckedCount = CheckedCount + 1
        '' Code nicely taken from someone (which took it from someone else?)
        '' Removed "IO." because System.IO was imported
        'Dim sc As New Shell32.Shell()

        'Dim output As Shell32.Folder = sc.NameSpace(".\prog_bin")
        'Dim input As Shell32.Folder = sc.NameSpace(".\prog_bin\oscdimg.zip")
        'output.CopyHere(input.Items, 4)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CheckedCount = 0
        If Environment.Is64BitOperatingSystem = True Then
            If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
                CheckPic1.Visible = False
                Button2.Visible = True
            Else
                CheckPic1.Visible = True
                Button2.Visible = False
                CheckedCount = CheckedCount + 1
            End If
            If Not File.Exists("\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Deployment Tools\amd64\Oscdimg\oscdimg.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
                CheckedCount = CheckedCount + 1
            End If
        Else
            If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
                CheckPic1.Visible = False
                Button2.Visible = True
            Else
                CheckPic1.Visible = True
                Button2.Visible = False
                CheckedCount = CheckedCount + 1
            End If
            If Not File.Exists("\Program Files\Windows Kits\10\Assessment and Deployment Kit\Deployment Tools\x86\Oscdimg\oscdimg.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
            End If
        End If
        If File.Exists("\Windows\system32\dism.exe") Then
            CheckPic2.Visible = True
        Else
            CheckPic2.Visible = False
        End If
        If CheckedCount = 3 Then
            Label10.Visible = True
            Button1.Visible = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Environment.Is64BitOperatingSystem = True Then
            WebSite = "https://www.7-zip.org/a/7z2107-x64.exe"
            TargetFile = "7z2107-x64.exe"
        Else
            WebSite = "https://www.7-zip.org/a/7z2107.exe"
            TargetFile = "7z2107.exe"
        End If
        Using SevenZipDownload As New WebClient()
            SevenZipDownload.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36 Edg/83.0.478.45")
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            SevenZipDownload.DownloadFile(WebSite, TargetPath & "\" & TargetFile)
        End Using
        If File.Exists(".\prog_bin\7z2107-x64.exe") Then
            Process.Start(".\prog_bin\7z2107-64.exe")
        ElseIf File.Exists(".\prog_bin\7z2107.exe") Then
            Process.Start(".\prog_bin\7z2107.exe")
        End If
        Do Until File.Exists("\Program Files\7-Zip\7z.exe")
            If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
                Button3.Visible = True
                CheckPic3.Visible = False
            Else
                Button3.Visible = False
                CheckPic3.Visible = True
            End If
        Loop
        If Not File.Exists("\Program Files\7-Zip\7z.exe") Then
            Button3.Visible = True
            CheckPic3.Visible = False
        Else
            Button3.Visible = False
            CheckPic3.Visible = True
        End If
        CheckedCount = CheckedCount + 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
