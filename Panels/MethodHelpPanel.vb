Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class MethodHelpPanel

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub MethodHelpPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            GroupBox1.ForeColor = Color.Black
            ComboBox1.BackColor = BackColor
            ComboBox1.ForeColor = ForeColor
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            GroupBox1.ForeColor = Color.White
            ComboBox1.BackColor = BackColor
            ComboBox1.ForeColor = ForeColor
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Method help"
            Label2.Text = "Please select the method that best suits your needs."
            Label3.Text = "Selected method:"
            Label4.Text = "Select a method in the drop down menu above and you will see the details here."
            Label5.Text = "WIMR (Description: WIM-Replace)"
            Label6.Text = "Move WIM/ESD files from the Windows 11 installer to the Windows 10 installer."
            Label7.Text = "Procedure:"
            Label8.Text = "Advantages:"
            Label9.Text = "Disadvantages:"
            Label10.Text = "Time taken*:"
            Label11.Text = "1. Delete " & Quote & "install.wim/esd" & Quote & " from the Windows 10 installer" & CrLf & "2. Move " & Quote & "install.wim/esd" & Quote & " from the Windows 11 installer to the custom installer"
            Label12.Text = "This method is simple, but effective, and it works all the time"
            Label13.Text = "The upgrade capability will not be present, because the installer files are from Windows 10"
            Label16.Text = "This method is inefficient and may rarely work"
            Label17.Text = "This method takes the shortest time"
            Label18.Text = "1. Delete DLL files from the Windows 11 installer" & CrLf & "2. Move DLL files from the Windows 10 installer"
            Label19.Text = "Procedure:"
            Label20.Text = "Advantages:"
            Label21.Text = "Disadvantages:"
            Label22.Text = "* The time may vary on different hardware. Tests conducted on a SATA SSD with source installers on external HDD"
            Label23.Text = "** The time may vary on the options specified on Advanced options"
            Label24.Text = "Time taken*:"
            Label25.Text = "Move DLL files from the Windows 10 installer to the Windows 11 installer."
            Label26.Text = "DLLR (Description: DLL-Replace)"
            Label28.Text = "This method may not work on virtual machines. You also need administrative privileges"
            Label29.Text = "There is no need to specify a Windows 10 installer, and is configurable"
            Label30.Text = "1. Move " & Quote & "boot.wim" & Quote & " from the Windows 11 installer" & CrLf & "2. Mount the image and perform registry tweaks" & CrLf & "3. Commit and unmount the image, and move it back to the installer"
            Label31.Text = "Procedure:"
            Label32.Text = "Advantages:"
            Label33.Text = "Disadvantages:"
            Label34.Text = "Time taken*:"
            Label35.Text = "Perform registry tweaks to the Windows 11 installer"
            Label36.Text = "REGTWEAK (Description: REGistry-TWEAK)"
            GroupBox1.Text = "Method details"
            OK_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Ayuda de métodos"
            Label2.Text = "Por favor, seleccione el método que más le sirva."
            Label3.Text = "Método seleccionado:"
            Label4.Text = "Seleccione un método en el menú desplegable arriba y verá los detalles aquí."
            Label5.Text = "WIMR (Descripción: WIM-Replace)"
            Label6.Text = "Mover archivos WIM/ESD del instalador de Windows 11 al instalador de Windows 10."
            Label7.Text = "Procedimiento:"
            Label8.Text = "Ventajas:"
            Label9.Text = "Inconvenientes:"
            Label10.Text = "Tiempo tardado*:"
            Label11.Text = "1. Eliminar " & Quote & "install.wim/esd" & Quote & " del instalador de Windows 10" & CrLf & "2. Mover " & Quote & "install.wim/esd" & Quote & " del instalador de Windows 11 al instalador modificado"
            Label12.Text = "Este método es simple, pero efectivo, y siempre funciona"
            Label13.Text = "La abilidad de actualizar no estará presente, debido a que los archivos del instalador son de Windows 10"
            Label16.Text = "Este método no es efectivo y raramente funciona"
            Label17.Text = "Este método tarda menos"
            Label18.Text = "1. Eliminar archivos DLL del instalador de Windows 11" & CrLf & "2. Mover archivos DLL del instalador de Windows 10"
            Label19.Text = "Procedimiento:"
            Label20.Text = "Ventajas:"
            Label21.Text = "Inconvenientes:"
            Label22.Text = "* El tiempo podría variar en hardware diferente. Las pruebas fueron realizadas en una SSD SATA, con los instaladores de origen en un disco duro externo"
            Label23.Text = "** El tiempo podría variar dependiendo de las opciones especificadas en Opciones avanzadas"
            Label24.Text = "Tiempo tardado*:"
            Label25.Text = "Mover archivos DLL del instalador de Windows 10 al instalador de Windows 11."
            Label26.Text = "DLLR (Descripción: DLL-Replace)"
            Label28.Text = "Este método podría no funcionar en máquinas virtuales. También necesita privilegios de administrador"
            Label29.Text = "No se necesita especificar un instalador de Windows 10, y es configurable"
            Label30.Text = "1. Mover " & Quote & "boot.wim" & Quote & " del instalador de Windows 11" & CrLf & "2. Montar la imagen y realizar cambios al registro" & CrLf & "3. Guardar y desmontar la imagen, y moverla de vuelta al instalador"
            Label31.Text = "Procedimiento:"
            Label32.Text = "Ventajas:"
            Label33.Text = "Inconvenientes:"
            Label34.Text = "Tiempo tardado*:"
            Label35.Text = "Realizar cambios al registro del instalador de Windows 11"
            Label36.Text = "REGTWEAK (Descripción: REGistry-TWEAK)"
            GroupBox1.Text = "Detalles de método"
            OK_Button.Text = "Aceptar"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Aide aux méthodes"
            Label2.Text = "Veuillez choisir la méthode qui correspond le mieux à vos besoins."
            Label3.Text = "Méthode choisie :"
            Label4.Text = "Sélectionnez une méthode dans le menu déroulant ci-dessus et vous verrez les détails ici."
            Label5.Text = "WIMR (Description : WIM-Replace)"
            Label6.Text = "Déplacer les fichiers WIM/ESD de l'installateur Windows 11 vers l'installateur Windows 10."
            Label7.Text = "Procédure :"
            Label8.Text = "Avantages :"
            Label9.Text = "Inconvénients :"
            Label10.Text = "Temps pris* :"
            Label11.Text = "1. Supprimer " & Quote & "install.wim/esd" & Quote & " de l'installateur de Windows 10" & CrLf & "2. Déplacer " & Quote & "install.wim/esd" & Quote & " de l'installateur de Windows 11 à l'installateur personnalisé"
            Label12.Text = "Cette méthode est simple, mais efficace, et elle fonctionne tout le temps."
            Label13.Text = "La capacité de mise à niveau ne sera pas présente, car les fichiers de l'installateur proviennent de Windows 10."
            Label16.Text = "Cette méthode est inefficace et peut rarement fonctionner."
            Label17.Text = "Cette méthode prend le temps le plus court"
            Label18.Text = "1. Supprimer les fichiers DLL de l'installateur de Windows 11" & CrLf & "2. Déplacer les fichiers DLL de l'installateur de Windows 10"
            Label19.Text = "Procédure :"
            Label20.Text = "Avantages :"
            Label21.Text = "Inconvénients :"
            Label22.Text = "* Le temps peut varier selon le matériel utilisé. Tests effectués sur un SSD SATA avec des installateurs de sources sur un disque dur externe."
            Label23.Text = "** La durée peut varier en fonction des options spécifiées dans les options avancées."
            Label24.Text = "Temps pris* :"
            Label25.Text = "Déplacer les fichiers DLL de l'installateur de Windows 10 vers l'installateur de Windows 11."
            Label26.Text = "DLLR (Description : DLL-Replace)"
            Label28.Text = "Cette méthode peut ne pas fonctionner sur les machines virtuelles. Vous devez également disposer de privilèges administratifs"
            Label29.Text = "Il n'est pas nécessaire de spécifier un installateur Windows 10, et il est configurable."
            Label30.Text = "1. Déplacer " & Quote & "boot.wim" & Quote & " de l'installateur de Windows 11" & CrLf & "2. Monter l'image et effectuer les modifications du registre" & CrLf & "3. Valider et démonter l'image, et la déplacer vers l'installateur."
            Label31.Text = "Procédure :"
            Label32.Text = "Avantages :"
            Label33.Text = "Inconvénients :"
            Label34.Text = "Temps pris* :"
            Label35.Text = "Effectuer des modifications de registre pour l'installateur de Windows 11"
            Label36.Text = "REGTWEAK (Description : REGistry-TWEAK)"
            GroupBox1.Text = "Détail de la méthode"
            OK_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Method help"
                Label2.Text = "Please select the method that best suits your needs."
                Label3.Text = "Selected method:"
                Label4.Text = "Select a method in the drop down menu above and you will see the details here."
                Label5.Text = "WIMR (Description: WIM-Replace)"
                Label6.Text = "Move WIM/ESD files from the Windows 11 installer to the Windows 10 installer."
                Label7.Text = "Procedure:"
                Label8.Text = "Advantages:"
                Label9.Text = "Disadvantages:"
                Label10.Text = "Time taken*:"
                Label11.Text = "1. Delete " & Quote & "install.wim/esd" & Quote & " from the Windows 10 installer" & CrLf & "2. Moves " & Quote & "install.wim/esd" & Quote & " from the Windows 11 installer to the custom installer"""
                Label12.Text = "This method is simple, but effective, and it works all the time"
                Label13.Text = "The upgrade capability will not be present, because the installer files are from Windows 10"
                Label16.Text = "This method is inefficient and may rarely work"
                Label17.Text = "This method takes the shortest time"
                Label18.Text = "1. Delete DLL files from the Windows 11 installer" & CrLf & "2. Move DLL files from the Windows 10 installer"
                Label19.Text = "Procedure:"
                Label20.Text = "Advantages:"
                Label21.Text = "Disadvantages:"
                Label22.Text = "* The time may vary on different hardware. Tests conducted on a SATA SSD with source installers on external HDD"
                Label23.Text = "** The time may vary on the options specified on Advanced options"
                Label24.Text = "Time taken*:"
                Label25.Text = "Move DLL files from the Windows 10 installer to the Windows 11 installer."
                Label26.Text = "DLLR (Description: DLL-Replace)"
                Label28.Text = "This method may not work on virtual machines. You also need administrative privileges"
                Label29.Text = "There is no need to specify a Windows 10 installer, and is configurable"
                Label30.Text = "1. Move " & Quote & "boot.wim" & Quote & " from the Windows 11 installer" & CrLf & "2. Mount the image and perform registry tweaks" & CrLf & "3. Commit and unmount the image, and move it back to the installer"
                Label31.Text = "Procedure:"
                Label32.Text = "Advantages:"
                Label33.Text = "Disadvantages:"
                Label34.Text = "Time taken*:"
                Label35.Text = "Perform registry tweaks to the Windows 11 installer"
                Label36.Text = "REGTWEAK (Description: REGistry-TWEAK)"
                GroupBox1.Text = "Method details"
                OK_Button.Text = "OK"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Ayuda de métodos"
                Label2.Text = "Por favor, seleccione el método que más le sirva."
                Label3.Text = "Método seleccionado:"
                Label4.Text = "Seleccione un método en el menú desplegable arriba y verá los detalles aquí."
                Label5.Text = "WIMR (Descripción: WIM-Replace)"
                Label6.Text = "Mover archivos WIM/ESD del instalador de Windows 11 al instalador de Windows 10."
                Label7.Text = "Procedimiento:"
                Label8.Text = "Ventajas:"
                Label9.Text = "Inconvenientes:"
                Label10.Text = "Tiempo tardado*:"
                Label11.Text = "1. Eliminar " & Quote & "install.wim/esd" & Quote & " del instalador de Windows 10" & CrLf & "2. Mover " & Quote & "install.wim/esd" & Quote & " del instalador de Windows 11 al instalador modificado"
                Label12.Text = "Este método es simple, pero efectivo, y siempre funciona"
                Label13.Text = "La abilidad de actualizar no estará presente, debido a que los archivos del instalador son de Windows 10"
                Label16.Text = "Este método no es efectivo y raramente funciona"
                Label17.Text = "Este método tarda menos"
                Label18.Text = "1. Eliminar archivos DLL del instalador de Windows 11" & CrLf & "2. Mover archivos DLL del instalador de Windows 10"
                Label19.Text = "Procedimiento:"
                Label20.Text = "Ventajas:"
                Label21.Text = "Inconvenientes:"
                Label22.Text = "* El tiempo podría variar en hardware diferente. Las pruebas fueron realizadas en una SSD SATA, con los instaladores de origen en un disco duro externo"
                Label23.Text = "** El tiempo podría variar dependiendo de las opciones especificadas en Opciones avanzadas"
                Label24.Text = "Tiempo tardado*:"
                Label25.Text = "Mover archivos DLL del instalador de Windows 10 al instalador de Windows 11."
                Label26.Text = "DLLR (Descripción: DLL-Replace)"
                Label28.Text = "Este método podría no funcionar en máquinas virtuales. También necesita privilegios de administrador"
                Label29.Text = "No se necesita especificar un instalador de Windows 10, y es configurable"
                Label30.Text = "1. Mover " & Quote & "boot.wim" & Quote & " del instalador de Windows 11" & CrLf & "2. Montar la imagen y realizar cambios al registro" & CrLf & "3. Guardar y desmontar la imagen, y moverla de vuelta al instalador"
                Label31.Text = "Procedimiento:"
                Label32.Text = "Ventajas:"
                Label33.Text = "Inconvenientes:"
                Label34.Text = "Tiempo tardado*:"
                Label35.Text = "Realizar cambios al registro del instalador de Windows 11"
                Label36.Text = "REGTWEAK (Descripción: REGistry-TWEAK)"
                GroupBox1.Text = "Detalles de método"
                OK_Button.Text = "Aceptar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Aide aux méthodes"
                Label2.Text = "Veuillez choisir la méthode qui correspond le mieux à vos besoins."
                Label3.Text = "Méthode choisie :"
                Label4.Text = "Sélectionnez une méthode dans le menu déroulant ci-dessus et vous verrez les détails ici."
                Label5.Text = "WIMR (Description : WIM-Replace)"
                Label6.Text = "Déplacer les fichiers WIM/ESD de l'installateur Windows 11 vers l'installateur Windows 10."
                Label7.Text = "Procédure :"
                Label8.Text = "Avantages :"
                Label9.Text = "Inconvénients :"
                Label10.Text = "Temps pris* :"
                Label11.Text = "1. Supprimer " & Quote & "install.wim/esd" & Quote & " de l'installateur de Windows 10" & CrLf & "2. Déplacer " & Quote & "install.wim/esd" & Quote & " de l'installateur de Windows 11 à l'installateur personnalisé"
                Label12.Text = "Cette méthode est simple, mais efficace, et elle fonctionne tout le temps."
                Label13.Text = "La capacité de mise à niveau ne sera pas présente, car les fichiers de l'installateur proviennent de Windows 10."
                Label16.Text = "Cette méthode est inefficace et peut rarement fonctionner."
                Label17.Text = "Cette méthode prend le temps le plus court"
                Label18.Text = "1. Supprimer les fichiers DLL de l'installateur de Windows 11" & CrLf & "2. Déplacer les fichiers DLL de l'installateur de Windows 10"
                Label19.Text = "Procédure :"
                Label20.Text = "Avantages :"
                Label21.Text = "Inconvénients :"
                Label22.Text = "* Le temps peut varier selon le matériel utilisé. Tests effectués sur un SSD SATA avec des installateurs de sources sur un disque dur externe."
                Label23.Text = "** La durée peut varier en fonction des options spécifiées dans les options avancées."
                Label24.Text = "Temps pris* :"
                Label25.Text = "Déplacer les fichiers DLL de l'installateur de Windows 10 vers l'installateur de Windows 11."
                Label26.Text = "DLLR (Description : DLL-Replace)"
                Label28.Text = "Cette méthode peut ne pas fonctionner sur les machines virtuelles. Vous devez également disposer de privilèges administratifs"
                Label29.Text = "Il n'est pas nécessaire de spécifier un installateur Windows 10, et il est configurable."
                Label30.Text = "1. Déplacer " & Quote & "boot.wim" & Quote & " de l'installateur de Windows 11" & CrLf & "2. Monter l'image et effectuer les modifications du registre" & CrLf & "3. Valider et démonter l'image, et la déplacer vers l'installateur."
                Label31.Text = "Procédure :"
                Label32.Text = "Avantages :"
                Label33.Text = "Inconvénients :"
                Label34.Text = "Temps pris* :"
                Label35.Text = "Effectuer des modifications de registre pour l'installateur de Windows 11"
                Label36.Text = "REGTWEAK (Description : REGistry-TWEAK)"
                GroupBox1.Text = "Détail de la méthode"
                OK_Button.Text = "OK"
            End If
        End If
        Text = Label1.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "WIMR" Then
            NISPanel.Visible = False
            WIMRPanel.Visible = True
            DLLRPanel.Visible = False
            REGTWEAKPanel.Visible = False
        ElseIf ComboBox1.SelectedItem = "DLLR" Then
            NISPanel.Visible = False
            WIMRPanel.Visible = False
            DLLRPanel.Visible = True
            REGTWEAKPanel.Visible = False
        ElseIf ComboBox1.SelectedItem = "REGTWEAK" Then
            NISPanel.Visible = False
            WIMRPanel.Visible = False
            DLLRPanel.Visible = False
            REGTWEAKPanel.Visible = True
        Else
            NISPanel.Visible = True
            WIMRPanel.Visible = False
            DLLRPanel.Visible = False
            REGTWEAKPanel.Visible = False
        End If
    End Sub
End Class
