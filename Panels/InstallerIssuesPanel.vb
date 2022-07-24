Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class InstallerIssuesPanel

    Public Field1IsWrong As Boolean
    Public Field2IsWrong As Boolean
    Public Field3IsWrong As Boolean
    Public Field4IsWrong As Boolean
    Public Field5IsWrong As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        If MainForm.TextBox3.ForeColor = Color.Crimson And Field3IsWrong = False Then
            MainForm.TextBox3.ForeColor = MainForm.ForeColor
        End If
        If MainForm.TextBox4.ForeColor = Color.Crimson And Field4IsWrong = False Then
            MainForm.TextBox4.ForeColor = MainForm.ForeColor
        End If
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://docs.microsoft.com/" & MainForm.RegionalCode & "/windows/win32/fileio/naming-a-file#naming-conventions")
    End Sub

    Private Sub InstallerIssuesPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GatherIssues()
        If Field1IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic1.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic1.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix1.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic1.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic1.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix1.Visible = False
        End If
        If Field2IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic2.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic2.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix2.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic2.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic2.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix2.Visible = False
        End If
        If Field3IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic3.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic3.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix3.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic3.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic3.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix3.Visible = False
        End If
        If Field4IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic4.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic4.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix4.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic4.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic4.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix4.Visible = False
        End If
        If Field5IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic5.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic5.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix5.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic5.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic5.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix5.Visible = False
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
            IssueFix1.LinkColor = Color.FromArgb(1, 92, 186)
            IssueFix2.LinkColor = Color.FromArgb(1, 92, 186)
            IssueFix3.LinkColor = Color.FromArgb(1, 92, 186)
            IssueFix4.LinkColor = Color.FromArgb(1, 92, 186)
            IssueFix5.LinkColor = Color.FromArgb(1, 92, 186)
            GroupBox1.ForeColor = Color.Black
            GroupBox2.ForeColor = Color.Black
            InvalidTextFieldPic.Image = New Bitmap(My.Resources.invalidtext_light)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
            IssueFix1.LinkColor = Color.FromArgb(76, 194, 255)
            IssueFix2.LinkColor = Color.FromArgb(76, 194, 255)
            IssueFix3.LinkColor = Color.FromArgb(76, 194, 255)
            IssueFix4.LinkColor = Color.FromArgb(76, 194, 255)
            IssueFix5.LinkColor = Color.FromArgb(76, 194, 255)
            GroupBox1.ForeColor = Color.White
            GroupBox2.ForeColor = Color.White
            InvalidTextFieldPic.Image = New Bitmap(My.Resources.invalidtext_dark)
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Installer creation issues"
            Label2.Text = "You cannot create the installer unless you fix these issues:"
            If Field1IsWrong Then
                Issue1.Text = "The Windows 11 installer does not exist"
            Else
                Issue1.Text = "The Windows 11 installer exists"
            End If
            If Field2IsWrong Then
                Issue2.Text = "The Windows 10 installer does not exist"
            Else
                Issue2.Text = "The Windows 10 installer exists"
            End If
            If Field3IsWrong Then
                Issue3.Text = "There are invalid characters on the target installer name"
            Else
                Issue3.Text = "There are no invalid characters on the target installer name"
            End If
            If Field4IsWrong Then
                Issue4.Text = "There are invalid characters on the target installer path"
            Else
                Issue4.Text = "There are no invalid characters on the target installer path"
            End If
            If Field5IsWrong Then
                Issue5.Text = "The Windows 11 and Windows 10 installers are the same"
            Else
                Issue5.Text = "The Windows 11 and Windows 10 installers are different"
            End If
            Label7.Text = "Reserved names and characters: CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
            Label8.Text = "To see whether an issue is encountered, invalid text boxes are shown in red."
            LinkLabel1.Text = "If you don't know what reserved names are, please read Microsoft's documentation"
            LinkLabel1.LinkArea = New LinkArea(50, 30)
            GroupBox1.Text = "Windows reserved character list"
            GroupBox2.Text = "More information"
            IssueFix1.Text = "Fix"
            IssueFix2.Text = "Fix"
            IssueFix3.Text = "Fix"
            IssueFix4.Text = "Fix"
            IssueFix5.Text = "Fix"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Problemas de creación del instalador"
            Label2.Text = "No puede crear el instalador a menos que corrija estos problemas"
            If Field1IsWrong Then
                Issue1.Text = "El instalador de Windows 11 no existe"
            Else
                Issue1.Text = "El instalador de Windows 11 existe"
            End If
            If Field2IsWrong Then
                Issue2.Text = "El instalador de Windows 10 no existe"
            Else
                Issue2.Text = "El instalador de Windows 10 existe"
            End If
            If Field3IsWrong Then
                Issue3.Text = "Hay caracteres inválidos en el nombre del instalador de destino"
            Else
                Issue3.Text = "No hay caracteres inválidos en el nombre del instalador de destino"
            End If
            If Field4IsWrong Then
                Issue4.Text = "Hay caracteres inválidos en la ruta del instalador de destino"
            Else
                Issue4.Text = "No hay caracteres inválidos en la ruta del instalador de destino"
            End If
            If Field5IsWrong Then
                Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son iguales"
            Else
                Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son diferentes"
            End If
            Label7.Text = "Nombres y caracteres reservados: CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
            Label8.Text = "Para ver si se ha encontrado un error, los cuadros de texto inválidos se muestran en rojo."
            LinkLabel1.Text = "Si no sabe qué son los nombres reservados, consulte la documentación de Microsoft"
            LinkLabel1.LinkArea = New LinkArea(43, 38)
            GroupBox1.Text = "Lista de caracteres reservados de Windows"
            GroupBox2.Text = "Más información"
            IssueFix1.Text = "Corregir"
            IssueFix2.Text = "Corregir"
            IssueFix3.Text = "Corregir"
            IssueFix4.Text = "Corregir"
            IssueFix5.Text = "Corregir"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Problèmes de création de l'installateur"
            Label2.Text = "Vous ne pouvez pas créer l'installateur si vous ne résolvez pas ces problèmes :"
            If Field1IsWrong Then
                Issue1.Text = "L'installateur de Windows 11 n'existe pas"
            Else
                Issue1.Text = "L'installateur de Windows 11 existe"
            End If
            If Field2IsWrong Then
                Issue2.Text = "L'installateur de Windows 10 n'existe pas"
            Else
                Issue2.Text = "L'installateur de Windows 10 existe"
            End If
            If Field3IsWrong Then
                Issue3.Text = "Il y a des caractères invalides dans le nom de l'installateur cible"
            Else
                Issue3.Text = "Il n'y a pas de caractères invalides dans le nom de l'installateur cible"
            End If
            If Field4IsWrong Then
                Issue4.Text = "Il y a des caractères non valides dans le chemin de l'installateur cible"
            Else
                Issue4.Text = "Il n'y a pas de caractères non valides dans le chemin de l'installateur cible"
            End If
            If Field5IsWrong Then
                Issue5.Text = "Les installateurs de Windows 11 et de Windows 10 sont les mêmes"
            Else
                Issue5.Text = "Les installateurs de Windows 11 et Windows 10 sont différents"
            End If
            Label7.Text = "Noms et caractères réservés : CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
            Label8.Text = "Pour voir si un problème est rencontré, les zones de texte invalides sont affichées en rouge."
            LinkLabel1.Text = "Si vous ne savez pas ce que sont les noms réservés, veuillez lire la documentation de Microsoft"
            LinkLabel1.LinkArea = New LinkArea(61, 34)
            GroupBox1.Text = "Liste des caractères réservés de Windows"
            GroupBox2.Text = "Information supplémentaire"
            IssueFix1.Text = "Corriger"
            IssueFix2.Text = "Corriger"
            IssueFix3.Text = "Corriger"
            IssueFix4.Text = "Corriger"
            IssueFix5.Text = "Corriger"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Installer creation issues"
                Label2.Text = "You cannot create the installer unless you fix these issues:"
                If Field1IsWrong Then
                    Issue1.Text = "The Windows 11 installer does not exist"
                Else
                    Issue1.Text = "The Windows 11 installer exists"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "The Windows 10 installer does not exist"
                Else
                    Issue2.Text = "The Windows 10 installer exists"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "There are invalid characters on the target installer name"
                Else
                    Issue3.Text = "There are no invalid characters on the target installer name"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "There are invalid characters on the target installer path"
                Else
                    Issue4.Text = "There are no invalid characters on the target installer path"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "The Windows 11 and Windows 10 installers are the same"
                Else
                    Issue5.Text = "The Windows 11 and Windows 10 installers are different"
                End If
                Label7.Text = "Reserved names and characters: CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
                Label8.Text = "To see whether an issue is encountered, invalid text boxes are shown in red."
                LinkLabel1.Text = "If you don't know what reserved names are, please read Microsoft's documentation"
                LinkLabel1.LinkArea = New LinkArea(50, 30)
                GroupBox1.Text = "Windows reserved character list"
                GroupBox2.Text = "More information"
                IssueFix1.Text = "Fix"
                IssueFix2.Text = "Fix"
                IssueFix3.Text = "Fix"
                IssueFix4.Text = "Fix"
                IssueFix5.Text = "Fix"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Problemas de creación del instalador"
                Label2.Text = "No puede crear el instalador a menos que corrija estos problemas"
                If Field1IsWrong Then
                    Issue1.Text = "El instalador de Windows 11 no existe"
                Else
                    Issue1.Text = "El instalador de Windows 11 existe"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "El instalador de Windows 10 no existe"
                Else
                    Issue2.Text = "El instalador de Windows 10 existe"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "Hay caracteres inválidos en el nombre del instalador de destino"
                Else
                    Issue3.Text = "No hay caracteres inválidos en el nombre del instalador de destino"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "Hay caracteres inválidos en la ruta del instalador de destino"
                Else
                    Issue4.Text = "No hay caracteres inválidos en la ruta del instalador de destino"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son iguales"
                Else
                    Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son diferentes"
                End If
                Label7.Text = "Nombres y caracteres reservados: CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
                Label8.Text = "Para ver si se ha encontrado un error, los cuadros de texto inválidos se muestran en rojo."
                LinkLabel1.Text = "Si no sabe qué son los nombres reservados, consulte la documentación de Microsoft"
                LinkLabel1.LinkArea = New LinkArea(43, 38)
                GroupBox1.Text = "Lista de caracteres reservados de Windows"
                GroupBox2.Text = "Más información"
                IssueFix1.Text = "Corregir"
                IssueFix2.Text = "Corregir"
                IssueFix3.Text = "Corregir"
                IssueFix4.Text = "Corregir"
                IssueFix5.Text = "Corregir"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Problèmes de création de l'installateur"
                Label2.Text = "Vous ne pouvez pas créer l'installateur si vous ne résolvez pas ces problèmes :"
                If Field1IsWrong Then
                    Issue1.Text = "L'installateur de Windows 11 n'existe pas"
                Else
                    Issue1.Text = "L'installateur de Windows 11 existe"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "L'installateur de Windows 10 n'existe pas"
                Else
                    Issue2.Text = "L'installateur de Windows 10 existe"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "Il y a des caractères invalides dans le nom de l'installateur cible"
                Else
                    Issue3.Text = "Il n'y a pas de caractères invalides dans le nom de l'installateur cible"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "Il y a des caractères non valides dans le chemin de l'installateur cible"
                Else
                    Issue4.Text = "Il n'y a pas de caractères non valides dans le chemin de l'installateur cible"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "Les installateurs de Windows 11 et de Windows 10 sont les mêmes"
                Else
                    Issue5.Text = "Les installateurs de Windows 11 et Windows 10 sont différents"
                End If
                Label7.Text = "Noms et caractères réservés : CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, " & Quote & ", *, /, \, |, ?"
                Label8.Text = "Pour voir si un problème est rencontré, les zones de texte invalides sont affichées en rouge."
                LinkLabel1.Text = "Si vous ne savez pas ce que sont les noms réservés, veuillez lire la documentation de Microsoft"
                LinkLabel1.LinkArea = New LinkArea(61, 34)
                GroupBox1.Text = "Liste des caractères réservés de Windows"
                GroupBox2.Text = "Information supplémentaire"
                IssueFix1.Text = "Corriger"
                IssueFix2.Text = "Corriger"
                IssueFix3.Text = "Corriger"
                IssueFix4.Text = "Corriger"
                IssueFix5.Text = "Corriger"
            End If
        End If
        Text = Label1.Text
        Beep()
    End Sub

    Sub GatherIssues()
        If MainForm.TextBox1.ForeColor = Color.Crimson Then
            Field1IsWrong = True
        Else
            Field1IsWrong = False
        End If
        If MainForm.TextBox2.ForeColor = Color.Crimson Then
            Field2IsWrong = True
        Else
            Field2IsWrong = False
        End If
        If MainForm.TextBox3.ForeColor = Color.Crimson Then
            Field3IsWrong = True
        Else
            Field3IsWrong = False
        End If
        If MainForm.TextBox4.ForeColor = Color.Crimson Then
            Field4IsWrong = True
        Else
            Field4IsWrong = False
        End If
        If MainForm.TextBox1.Text = MainForm.TextBox2.Text Then
            Field5IsWrong = True
        Else
            Field5IsWrong = False
        End If
    End Sub

    Sub FixIssue1()
        MainForm.Button2.PerformClick()
        CheckFixedIssues()
    End Sub

    Sub FixIssue2()
        MainForm.Button4.PerformClick()
        CheckFixedIssues()
    End Sub

    Sub FixIssue3()
        MainForm.Button3.PerformClick()
        CheckFixedIssues()
    End Sub

    Sub FixIssue4()
        MainForm.Button5.PerformClick()
        CheckFixedIssues()
    End Sub

    Sub FixIssue5()
        OK_Button.PerformClick()
        MainForm.TextBox1.SelectAll()
    End Sub

    Private Sub IssueFix1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IssueFix1.LinkClicked
        FixIssue1()
    End Sub

    Private Sub IssueFix2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IssueFix2.LinkClicked
        FixIssue2()
    End Sub

    Private Sub IssueFix3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IssueFix3.LinkClicked
        FixIssue3()
    End Sub

    Private Sub IssueFix4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IssueFix4.LinkClicked
        FixIssue4()
    End Sub

    Private Sub IssueFix5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IssueFix5.LinkClicked
        FixIssue5()
    End Sub

    Sub CheckFixedIssues()
        If MainForm.TextBox1.ForeColor = Color.Crimson Then
            Field1IsWrong = True
        Else
            Field1IsWrong = False
        End If
        If MainForm.TextBox2.ForeColor = Color.Crimson Then
            Field2IsWrong = True
        Else
            Field2IsWrong = False
        End If
        If MainForm.TextBox3.ForeColor = Color.Crimson Then
            Field3IsWrong = True
        Else
            Field3IsWrong = False
        End If
        If MainForm.TextBox4.ForeColor = Color.Crimson Then
            Field4IsWrong = True
        Else
            Field4IsWrong = False
        End If
        If MainForm.TextBox1.Text = MainForm.TextBox2.Text Then
            Field5IsWrong = True
        Else
            Field5IsWrong = False
        End If
        If Field1IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic1.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic1.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix1.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic1.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic1.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix1.Visible = False
        End If
        If Field2IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic2.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic2.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix2.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic2.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic2.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix2.Visible = False
        End If
        If Field3IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic3.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic3.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix3.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic3.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic3.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix3.Visible = False
        End If
        If Field4IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic4.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic4.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix4.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic4.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic4.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix4.Visible = False
        End If
        If Field5IsWrong Then
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic5.Image = New Bitmap(My.Resources.cross_light)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic5.Image = New Bitmap(My.Resources.cross_dark)
            End If
            IssueFix5.Visible = True
        Else
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                IssuePic5.Image = New Bitmap(My.Resources.check)
            ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
                IssuePic5.Image = New Bitmap(My.Resources.check_dark)
            End If
            IssueFix5.Visible = False
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            If Field1IsWrong Then
                Issue1.Text = "The Windows 11 installer does not exist"
            Else
                Issue1.Text = "The Windows 11 installer exists"
            End If
            If Field2IsWrong Then
                Issue2.Text = "The Windows 10 installer does not exist"
            Else
                Issue2.Text = "The Windows 10 installer exists"
            End If
            If Field3IsWrong Then
                Issue3.Text = "There are invalid characters on the target installer name"
            Else
                Issue3.Text = "There are no invalid characters on the target installer name"
            End If
            If Field4IsWrong Then
                Issue4.Text = "There are invalid characters on the target installer path"
            Else
                Issue4.Text = "There are no invalid characters on the target installer path"
            End If
            If Field5IsWrong Then
                Issue5.Text = "The Windows 11 and Windows 10 installers are the same"
            Else
                Issue5.Text = "The Windows 11 and Windows 10 installers are different"
            End If
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            If Field1IsWrong Then
                Issue1.Text = "El instalador de Windows 11 no existe"
            Else
                Issue1.Text = "El instalador de Windows 11 existe"
            End If
            If Field2IsWrong Then
                Issue2.Text = "El instalador de Windows 10 no existe"
            Else
                Issue2.Text = "El instalador de Windows 10 existe"
            End If
            If Field3IsWrong Then
                Issue3.Text = "Hay caracteres inválidos en el nombre del instalador de destino"
            Else
                Issue3.Text = "No hay caracteres inválidos en el nombre del instalador de destino"
            End If
            If Field4IsWrong Then
                Issue4.Text = "Hay caracteres inválidos en la ruta del instalador de destino"
            Else
                Issue4.Text = "No hay caracteres inválidos en la ruta del instalador de destino"
            End If
            If Field5IsWrong Then
                Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son iguales"
            Else
                Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son diferentes"
            End If
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            If Field1IsWrong Then
                Issue1.Text = "L'installateur de Windows 11 n'existe pas"
            Else
                Issue1.Text = "L'installateur de Windows 11 existe"
            End If
            If Field2IsWrong Then
                Issue2.Text = "L'installateur de Windows 10 n'existe pas"
            Else
                Issue2.Text = "L'installateur de Windows 10 existe"
            End If
            If Field3IsWrong Then
                Issue3.Text = "Il y a des caractères invalides dans le nom de l'installateur cible"
            Else
                Issue3.Text = "Il n'y a pas de caractères invalides dans le nom de l'installateur cible"
            End If
            If Field4IsWrong Then
                Issue4.Text = "Il y a des caractères non valides dans le chemin de l'installateur cible"
            Else
                Issue4.Text = "Il n'y a pas de caractères non valides dans le chemin de l'installateur cible"
            End If
            If Field5IsWrong Then
                Issue5.Text = "Les installateurs de Windows 11 et de Windows 10 sont les mêmes"
            Else
                Issue5.Text = "Les installateurs de Windows 11 et Windows 10 sont différents"
            End If
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                If Field1IsWrong Then
                    Issue1.Text = "The Windows 11 installer does not exist"
                Else
                    Issue1.Text = "The Windows 11 installer exists"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "The Windows 10 installer does not exist"
                Else
                    Issue2.Text = "The Windows 10 installer exists"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "There are invalid characters on the target installer name"
                Else
                    Issue3.Text = "There are no invalid characters on the target installer name"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "There are invalid characters on the target installer path"
                Else
                    Issue4.Text = "There are no invalid characters on the target installer path"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "The Windows 11 and Windows 10 installers are the same"
                Else
                    Issue5.Text = "The Windows 11 and Windows 10 installers are different"
                End If
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                If Field1IsWrong Then
                    Issue1.Text = "El instalador de Windows 11 no existe"
                Else
                    Issue1.Text = "El instalador de Windows 11 existe"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "El instalador de Windows 10 no existe"
                Else
                    Issue2.Text = "El instalador de Windows 10 existe"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "Hay caracteres inválidos en el nombre del instalador de destino"
                Else
                    Issue3.Text = "No hay caracteres inválidos en el nombre del instalador de destino"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "Hay caracteres inválidos en la ruta del instalador de destino"
                Else
                    Issue4.Text = "No hay caracteres inválidos en la ruta del instalador de destino"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son iguales"
                Else
                    Issue5.Text = "Los instaladores de Windows 11 y Windows 10 son diferentes"
                End If
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                If Field1IsWrong Then
                    Issue1.Text = "L'installateur de Windows 11 n'existe pas"
                Else
                    Issue1.Text = "L'installateur de Windows 11 existe"
                End If
                If Field2IsWrong Then
                    Issue2.Text = "L'installateur de Windows 10 n'existe pas"
                Else
                    Issue2.Text = "L'installateur de Windows 10 existe"
                End If
                If Field3IsWrong Then
                    Issue3.Text = "Il y a des caractères invalides dans le nom de l'installateur cible"
                Else
                    Issue3.Text = "Il n'y a pas de caractères invalides dans le nom de l'installateur cible"
                End If
                If Field4IsWrong Then
                    Issue4.Text = "Il y a des caractères non valides dans le chemin de l'installateur cible"
                Else
                    Issue4.Text = "Il n'y a pas de caractères non valides dans le chemin de l'installateur cible"
                End If
                If Field5IsWrong Then
                    Issue5.Text = "Les installateurs de Windows 11 et de Windows 10 sont les mêmes"
                Else
                    Issue5.Text = "Les installateurs de Windows 11 et Windows 10 sont différents"
                End If
            End If
        End If
        If Field1IsWrong = False And Field2IsWrong = False And Field3IsWrong = False And Field4IsWrong = False And Field5IsWrong = False Then
            OK_Button.PerformClick()
        End If
    End Sub
End Class
