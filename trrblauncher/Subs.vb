Public Class Settings
    Dim CheckBox1 As CheckBox = trrblauncher.TRRMainForm.CheckBox1
    Dim CheckBox2 As CheckBox = trrblauncher.TRRMainForm.CheckBox2
    Dim CheckBox4 As CheckBox = trrblauncher.TRRMainForm.CheckBox4
    Dim RadioButton1 As RadioButton = trrblauncher.TRRMainForm.RadioButton1
    Dim RadioButton2 As RadioButton = trrblauncher.TRRMainForm.RadioButton2
    Public Sub load()

        If My.Settings.workingdir = "" Then
            MsgBox("You have not selected the path to wow.exe.. This can be set in the settings. If the launcher is in the same directory as wow.exe. You can ignore this message. ", MsgBoxStyle.OkOnly, "No Directory Selected")
            My.Settings.workingdir = "wow"
            My.Settings.Save()
            If My.Settings.CheckboxCheckForWowEXE = True Then
                CheckBox1.CheckState = CheckState.Checked
            Else
                CheckBox1.CheckState = CheckState.Unchecked
            End If
            If My.Settings.CheckboxSetrealmlist = True Then
                CheckBox2.CheckState = CheckState.Checked
            Else
                CheckBox2.CheckState = CheckState.Unchecked
            End If
            If My.Settings.AutoLogin = True Then
                CheckBox4.CheckState = CheckState.Checked
            Else
                CheckBox4.CheckState = CheckState.Unchecked
            End If

            If My.Settings.raidbuttonWinMin = True Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

        End If
    End Sub

    Public Sub save()


    End Sub



End Class
