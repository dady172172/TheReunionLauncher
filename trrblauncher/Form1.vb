Option Explicit On
Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Security.Principal
Imports Ionic
Imports System.Runtime.InteropServices
Imports System.Text

Public Class TRRMainForm
    Dim DesktopDir As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Dim curdirdim As String = ""
    Dim enUSdir As String = ""
    Dim enGBdir As String = ""
    Dim patchENUSdir As String = ""
    Dim patchENGBdir As String = ""
    Dim remlistFile As String = ""
    Dim patchAdir As String
    Dim patchBdir As String
    Dim patchCdir As String
    Dim patchDdir As String
    Dim patchWdir As String
    Dim patchENdir As String
    Dim patchInfoTXT As String
    Dim addonsdir As String
    Dim WowProcess As New Process
    Dim UserName As String
    Dim PassWord As String
    Dim buttonClose As Boolean
    Dim rez As String
    Dim folder As String



    Public Sub TRRMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Not File.Exists("DotNetZip.dll") Or Not File.Exists("DotNetZip.xml") Or Not File.Exists("TheReunionReborn.exe.config") Then
        '    Loading.ShowDialog()
        'End If
        '' play button size

        'ToolStripSplitButton1.Size
        'ToolStripSplitButton1.Height = 41


        '' reset settings for testing
        'If My.Settings.testing = True Then
        '    My.Settings.Reset()
        '    My.Settings.testing = False
        '    My.Settings.Save()
        'End If






        'SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        patchInfoTXT = Nothing
        '----------- check settings and set checkboxes at load
        If My.Settings.workingdir = "" Then
            MsgBox("You have not selected the path to wow.exe.. This can be set in the settings. If the launcher is in the same directory as wow.exe. You can ignore this message. ", MsgBoxStyle.OkOnly, "No Directory Selected")
            My.Settings.workingdir = "wow"
            My.Settings.Save()
        End If
        SetDims()
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

        If IO.File.Exists(patchAdir) And IO.File.Exists(patchBdir) And IO.File.Exists(patchCdir) And IO.File.Exists(patchDdir) Then
            Label1.Text = "Installed"
            Button8.Text = "Uninstall"
        Else
            Label1.Text = "Not Installed"
            Button8.Text = "Download"
        End If

        If IO.File.Exists(patchWdir) Then
            Label5.Text = "Installed"
            Button9.Text = "Uninstall"
        Else
            Label5.Text = "Not Installed"
            Button9.Text = "Download"
        End If

        If IO.File.Exists(patchENUSdir) Or IO.File.Exists(patchENGBdir) Then
            Label3.Text = "Installed"
            Button10.Text = "Uninstall"
        Else
            Label3.Text = "Not Installed"
            Button10.Text = "Download"
        End If
        TextBoxInstallDirectory.Text = curdirdim
        CenterPanels()
        AddonListBoxSetup()

        'For Each setting As String In My.Settings.Properties

        '    If setting IsNot "workingdir" And setting IsNot "CheckboxCheckForWowEXE" And setting IsNot "CheckboxSetrealmlist" And
        '        setting IsNot "raidbuttonWinMin" And setting IsNot "installuninstall" And setting IsNot "patch" And setting IsNot "ENUS" _
        '        And setting IsNot "AutoLogin" And setting IsNot "UserName" And setting IsNot "Password" And setting IsNot "RememberAccountName" _
        '        And setting IsNot "refreshCache" And setting IsNot "testing" Then

        '    End If

        'Next

    End Sub


    Private Sub TRRMainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'ToolStripSplitButton1.Select()


    End Sub

    ''find all model patches


    '' when resizing form centers panels and refreshes form
    Private Sub TRRMainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        CenterPanels()
        'Refresh()
    End Sub
    '' centers the panels
    Private Sub CenterPanels()
        'If Panel1.Visible = True Then Panel1.Location = New Point((Width - Panel1.Width) \ 2, (Height - Panel1.Height) \ 2)
        If PanelBackground1.Visible = True Then
            PanelBackground1.Location = New Point(0, 25)
            PanelBackground1.Size = New Size(Width, Height)
        End If
        If Panel1.Visible = True Then Panel1.Location = New Point((PanelBackground1.Width - Panel1.Width) \ 2, ((PanelBackground1.Height - Panel1.Height) \ 2) - 30)

    End Sub
    '' website button
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Process.Start("https://www.reunion-reborn.com/")
    End Sub
    '' resize end
    Private Sub TRRMainForm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        CenterPanels()
        'Refresh()
    End Sub
    '' discord button
    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Process.Start("https://discordapp.com/invite/ZfpmRVp")
    End Sub
    '' select directory button
    Dim FBD1 As New FolderBrowserDialog
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'MsgBox("Make sure you select the main folder with wow.exe.", MsgBoxStyle.OkOnly, "Select Wow.exe!!")
        Dim FBD1 As New FolderBrowserDialog With {
            .Description = "Select the main folder with Wow.exe in it. Example - C:/World of Warcraft/"
        }

        Dim FBDR1Result As DialogResult = FBD1.ShowDialog
        'FolderBrowserDialog1.ShowDialog()
        If FBDR1Result = DialogResult.Cancel Then

            Exit Sub
        ElseIf FBDR1Result = DialogResult.Abort Then
            Exit Sub
        ElseIf FBDR1Result = DialogResult.OK Then
            If File.Exists(FBD1.SelectedPath & "/Wow.exe") Then
                My.Settings.workingdir = FBD1.SelectedPath
                My.Settings.Save()
                MsgBox("Saved Path. You may now download the patches or play.")
                TRRMainForm_Load(sender:=Nothing, e:=EventArgs.Empty)
            Else
                MsgBox("Could not find Wow.exe in that directory. Please try again.")
            End If
        End If
    End Sub

    Public Sub SetDims()
        If My.Settings.workingdir = "wow" Then
            curdirdim = CurDir()
        Else
            curdirdim = My.Settings.workingdir
        End If
        TextBoxInstallDirectory.Text = curdirdim
        If IO.Directory.Exists(curdirdim & "\Data\enUS") Then
            My.Settings.ENUS = True
            My.Settings.Save()
            enUSdir = curdirdim & "\Data\enUS"
            If IO.File.Exists(enUSdir & "\realmlist.wtf") Then remlistFile = enUSdir & "\realmlist.wtf"
            patchENUSdir = enUSdir & "\patch-enUS-4.MPQ"

        End If
        If IO.Directory.Exists(curdirdim & "\Data\enGB") Then
            My.Settings.ENUS = False
            My.Settings.Save()
            enGBdir = curdirdim & "\Data\enGB"
            If IO.File.Exists(enGBdir & "\realmlist.wtf") Then remlistFile = enGBdir & "\realmlist.wtf"
            patchENGBdir = enGBdir & "\patch-enGB-4.MPQ"

        End If
        Dim tempDataDir As String = curdirdim & "\Data"
        If IO.File.Exists(tempDataDir & "\patch-A.MPQ") Then patchAdir = tempDataDir & "\patch-A.MPQ"
        If IO.File.Exists(tempDataDir & "\patch-B.MPQ") Then patchBdir = tempDataDir & "\patch-B.MPQ"
        If IO.File.Exists(tempDataDir & "\patch-C.MPQ") Then patchCdir = tempDataDir & "\patch-C.MPQ"
        If IO.File.Exists(tempDataDir & "\patch-D.MPQ") Then patchDdir = tempDataDir & "\patch-D.MPQ"
        If IO.File.Exists(tempDataDir & "\patch-W.MPQ") Then patchWdir = tempDataDir & "\patch-W.MPQ"
        If Not patchENUSdir = Nothing Then patchENdir = patchENUSdir Else If Not patchENGBdir = Nothing Then patchENdir = patchENGBdir
        If Directory.Exists(curdirdim & "\Interface\AddOns") Then addonsdir = curdirdim & "\Interface\AddOns"
        If Not My.Settings.UserName = "" Then UserName = My.Settings.UserName
        If Not My.Settings.Password = "" Then PassWord = My.Settings.Password
        '' look for username in wtf file
        If FindLineInWTF("SET accountName*") = True Then
            My.Settings.RememberAccountName = True
            My.Settings.Save()
        Else
            My.Settings.RememberAccountName = False
            My.Settings.Save()
        End If



    End Sub

    '' if username exists set it to curent username in auto login else do nothing
    Private Sub FindUsernameWTF()
        Dim wtffile As String = curdirdim & "\WTF\Config.wtf"

        Dim setAccountNameString As String = "SET accountName " & Chr(34) & UserName & Chr(34)
        If FindLineInWTF("SET accountName*") = True Then
            Dim lines() As String = File.ReadAllLines(curdirdim & "\WTF\Config.wtf")
            For i As Integer = 0 To lines.Length - 1
                If lines(i) Like "SET accountName*" Then
                    If Not lines(i) = setAccountNameString Then lines(i) = setAccountNameString

                End If
            Next
            File.WriteAllLines(wtffile, lines)
        Else

        End If








        'If File.Exists(curdirdim & "\WTF\config.wtf") Then
        '    Dim lines() As String = File.ReadAllLines(curdirdim & "\WTF\Config.wtf")
        '    For i As Integer = 0 To lines.Length - 1
        '        If lines(i) Like "SET accountName*" Then


        '            Dim linesChar As String = lines(i)
        '            linesChar = linesChar.Remove(0, 15)
        '            linesChar = linesChar.Trim()
        '            Dim chartotrim As Char() = {Chr(34)}
        '            linesChar = linesChar.Trim(chartotrim)
        '            '' found name at this point
        '            If Not linesChar = "" Then
        '                My.Settings.RememberAccountName = True
        '                My.Settings.Save()
        '            End If
        '            If Not lines(i) = "SET accountName " & Chr(34) & UserName & Chr(34) Then
        '                lines(i) = setAccountNameString

        '            End If
        '            foundaccountname = True
        '        End If
        '    Next
        '    If foundaccountname = False Then
        '        My.Settings.RememberAccountName = False
        '        My.Settings.Save()
        '    End If
        '    File.WriteAllLines(curdirdim & "\WTF\Config.wtf", lines)
        '    TRRMainForm_Load(sender:=Nothing, e:=Nothing)
        'End If

    End Sub

    Private Sub SetUsernameWTF()
        Dim wtffile As String = curdirdim & "\WTF\Config.wtf"
        Dim setAccountNameString() As String = {"SET accountName " & Chr(34) & UserName & Chr(34)}
        File.AppendAllLines(wtffile, setAccountNameString)

    End Sub



    Private Function FindLineInWTF(ByVal line As String)
        Dim wtfdir As String = curdirdim & "\WTF\Config.wtf"
        If File.Exists(wtfdir) Then
            Dim lines() As String = File.ReadAllLines(curdirdim & "\WTF\Config.wtf")
            For i As Integer = 0 To lines.Length - 1
                If lines(i) Like line Then
                    Return True
                    Exit Function
                End If
            Next
            Return False
            Exit Function
        End If
        Return False


    End Function

    Private Sub ReplaceLineInWTF(ByVal line As String, ByVal TEXT As String)
        Dim wtfdir As String = curdirdim & "\WTF\Config.wtf"
        Dim setstring As String = "SET accountName " & Chr(34) & UserName & Chr(34)

        If File.Exists(wtfdir) Then
            Dim lines() As String = File.ReadAllLines(wtfdir)
            For i As Integer = 0 To lines.Length - 1
                If lines(i) Like line Then
                    lines(i) = setstring
                End If
            Next
            File.WriteAllLines(wtfdir, lines)
        End If




    End Sub



    '' play button
    Private Sub ButtonStart()
        If My.Settings.refreshCache = True Then
            If Directory.Exists(curdirdim & "\Cache") Then
                My.Computer.FileSystem.DeleteDirectory(curdirdim & "/Cache", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                My.Settings.refreshCache = False
                My.Settings.Save()
            End If
        Else

        End If

        If My.Settings.CheckboxCheckForWowEXE = True Then
            If Not IO.File.Exists(curdirdim & "\Wow.exe") Then
            MsgBox("Wow.exe doesn't exist in the folder selected. Please go to the settings and set the correct directory or change the settings to look for wow.exe.", MsgBoxStyle.OkOnly, "Can't find Wow.exe")
            End If
        End If
        If My.Settings.CheckboxSetrealmlist = True Then
            If IO.File.Exists(remlistFile) Then
                My.Computer.FileSystem.WriteAllText(remlistFile, "set realmlist login.reunion-reborn.com", False)
            End If
        End If

        ''if checkbox 4 and no user pass set and set accountname in wtf else setusername
        FindUsernameWTF()
        System.Threading.Thread.Sleep(200)
        SetDims()
        System.Threading.Thread.Sleep(200)
        If CheckBox4.Checked = True Then
            If My.Settings.UserName = "" Or My.Settings.UserName = Nothing Or My.Settings.Password = "" Or My.Settings.Password = Nothing Then
                SetUserPassword.ShowDialog()
                Exit Sub
            Else
                UserName = My.Settings.UserName
                PassWord = My.Settings.Password
                If My.Settings.RememberAccountName = True Then
                    ReplaceLineInWTF("SET accountName*", "SET accountName " & Chr(34) & UserName & Chr(34))
                Else
                    SetUsernameWTF()
                End If


            End If
        End If

        '' copy reunion.exe if not existing
        If Not IO.File.Exists(curdirdim & "\Reunion.exe") Then
            IO.File.WriteAllBytes(curdirdim & "\Reunion.exe", My.Resources.Reunion)
        End If
        '' set exe to start
        WowProcess.StartInfo.FileName = curdirdim & "\Reunion.exe"
        '' start exe
        WowProcess.Start()
        '' get raidbutton and set
        If My.Settings.raidbuttonWinMin = True Then
            Me.WindowState = FormWindowState.Minimized
        Else
            buttonClose = True
        End If



        If CheckBox4.Checked Then
            Timer3.Start()
        Else
            If buttonClose = True Then
                Me.Close()
            End If
        End If



        'Dim p As Process() = Process.GetProcessesByName("World of Warcraft")

        'Do Until p.Length = 0 And p(0).WaitForInputIdle = True And p(0).Responding = True
        '    Threading.Thread.Sleep(4000)
        'Loop

        'Do Until WowProcess.WaitForInputIdle = True And WowProcess.Responding = True
        '    Threading.Thread.Sleep(4000)
        'Loop



        'Threading.Thread.Sleep(800)
        'SendKeys.Send("keathunsar")
        'Threading.Thread.Sleep(100)
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("ilovejj")
        'Threading.Thread.Sleep(100)
        'SendKeys.Send("{ENTER}")



        'System.Threading.Thread.Sleep(4000)
        'SendKeys.Send("keathunsar")
        'Threading.Thread.Sleep(100)
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("ilovejj")
        'Threading.Thread.Sleep(100)
        'SendKeys.Send("{ENTER}")





    End Sub
    '' saving check box states
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            My.Settings.CheckboxCheckForWowEXE = True
            My.Settings.Save()
        Else
            My.Settings.CheckboxCheckForWowEXE = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            My.Settings.CheckboxSetrealmlist = True
            My.Settings.Save()
        Else
            My.Settings.CheckboxSetrealmlist = False
            My.Settings.Save()
        End If
    End Sub

    ' ------------Delete cache
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IO.Directory.Exists(curdirdim & "\Cache") Then My.Computer.FileSystem.DeleteDirectory(curdirdim & "\Cache", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing) : MsgBox("Deleted Cache!!", MsgBoxStyle.OkOnly, "Cache Deleted")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Addons Form here
        My.Settings.patch = "addons"
        My.Settings.installuninstall = ""
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    '' addons button get more addons
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Addons.Show()
        Me.Hide()
        'Process.Start("https://legacy-wow.com/wotlk-addons/")
    End Sub


    '' radio button minimize
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.raidbuttonWinMin = True
        Else
            My.Settings.raidbuttonWinMin = False
        End If
        My.Settings.Save()
    End Sub
    '' radio button close
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.raidbuttonWinMin = False
        Else
            My.Settings.raidbuttonWinMin = True
        End If
        My.Settings.Save()
    End Sub
    '' addons folder button
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim addonsDir As String = curdirdim & "\Interface\AddOns"
        If IO.Directory.Exists(addonsDir) Then Process.Start(curdirdim & "\Interface\AddOns") Else MsgBox("Could not find the addons folder!!", MsgBoxStyle.OkOnly, "Addons Folder???")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Button8.Text = "Uninstall" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "model"
        My.Settings.Save()
        Download.ShowDialog()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Button9.Text = "Uninstall" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "water"
        My.Settings.Save()
        Download.ShowDialog()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Button10.Text = "Uninstall" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "login"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ButtonStart()
    End Sub


    '' populate addons for listbox
    Public Sub AddonListBoxSetup()
        If ListBox1.Items.Count >= 1 Then ListBox1.Items.Clear()
        If addonsdir = Nothing Then Exit Sub
        Dim dInfo As New System.IO.DirectoryInfo(addonsdir)

        For Each dir As System.IO.DirectoryInfo In dInfo.GetDirectories()
            If Not dir.Name Like "Blizzard_*" Then
                ListBox1.Items.Add(dir.Name)
            End If
        Next
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        My.Settings.patch = "backup"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub TRRMainForm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            MsgBox(path)
        Next
    End Sub

    Private Sub TRRMainForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp

    End Sub

    Private Sub Button11_DragEnter(sender As Object, e As DragEventArgs)
        MsgBox(sender.ToString)
        MsgBox(e.ToString)
        MsgBox(e.Data.ToString)
        MsgBox(e.Effect.ToString)
    End Sub

    Private Sub PanelBackground1_DragDrop(sender As Object, e As DragEventArgs) Handles PanelBackground1.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            MsgBox(path)
        Next
    End Sub

    Private Sub TRRMainForm_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim deldir As String = curdirdim & "\Interface\AddOns\" & ListBox1.SelectedItem
        If MsgBox("Are you sure you Want to delete " & ListBox1.SelectedItem, MsgBoxStyle.YesNo, "Confirm Delete!!") = MsgBoxResult.Yes Then
            Directory.Delete(deldir, True)
            ListBox1.Items.Clear()
            AddonListBoxSetup()
        Else

        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs)
        'install addon button




        Dim ZipFileDir As String = ""
        Dim ofd1 As New OpenFileDialog With {
            .InitialDirectory = DesktopDir,
            .Title = "Select addon .zip to install",
            .Filter = "zip files|*.zip"
        }
        Dim ofd1Result As DialogResult = ofd1.ShowDialog
        If ofd1Result = DialogResult.Cancel Then
            Exit Sub
        ElseIf DialogResult.OK Then
            ZipFileDir = ofd1.FileName
            My.Settings.installuninstall = ZipFileDir
            My.Settings.patch = "installAddon"
            My.Settings.Save()
            Download.ShowDialog()
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        TRRMainForm_Load(sender:=Nothing, e:=EventArgs.Empty)
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            My.Settings.AutoLogin = True
        Else
            My.Settings.AutoLogin = False
        End If
        My.Settings.Save()
        If My.Settings.UserName = "" Or My.Settings.Password = "" Then
            SetUserPassword.ShowDialog()
        End If

    End Sub



    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim p As Process() = Process.GetProcessesByName("World of Warcraft")
        If p.Length >= 0 Then
            Do Until WowProcess.WaitForInputIdle = True
                Threading.Thread.Sleep(1000)
            Loop
            Do Until WowProcess.Responding = True
                Threading.Thread.Sleep(1000)
            Loop

            AppActivate(WowProcess.Id)
            Threading.Thread.Sleep(800)
            AppActivate(WowProcess.Id)
            Threading.Thread.Sleep(50)
            AppActivate(WowProcess.Id)
            SendKeys.Send(PassWord)
            SendKeys.Send("{ENTER}")
            System.Threading.Thread.Sleep(1000)

            Timer3.Stop()
            If buttonClose = True Then
                Me.Close()
            End If
        Else


        End If


    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        SetUserPassword.ShowDialog()
    End Sub

    Private Sub SetScreenRez()
        If Not File.Exists(curdirdim & "\WTF\Config.wtf") Then
            MsgBox("Could not file config.wtf file.")
            Exit Sub
        End If

        Dim lines() As String = File.ReadAllLines(curdirdim & "\WTF\Config.wtf")
        For i As Integer = 0 To lines.Length - 1
            If lines(i) Like "SET gxResolution*" Then
                lines(i) = "SET gxResolution " & Chr(34) & rez & Chr(34)
            End If
        Next
        File.WriteAllLines(curdirdim & "\WTF\Config.wtf", lines)

    End Sub

    Private Sub EightBySixMenu_Click(sender As Object, e As EventArgs) Handles EightBySixMenu.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "800x600"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub TwelveEightyByEight_Click(sender As Object, e As EventArgs) Handles TwelveEightyByEight.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "1280x800"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub TwelveEightyByNineSixty_Click(sender As Object, e As EventArgs) Handles TwelveEightyByNineSixty.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "1280x960"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub NinteenTwontyByTenEighty_Click(sender As Object, e As EventArgs) Handles NinteenTwontyByTenEighty.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "1920x1080"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        SetScreenRez()
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        ButtonStart()
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton1.ButtonClick
        ButtonStart()
    End Sub

    Private Sub ConsoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsoleToolStripMenuItem.Click
        WowProcess.StartInfo.Arguments = "-console"
        ButtonStart()
    End Sub

    Private Sub FullscreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullscreenToolStripMenuItem.Click
        WowProcess.StartInfo.Arguments = "-fullscreen"
        ButtonStart()
    End Sub

    Private Sub TwelveEightyBySevinTwonty_Click(sender As Object, e As EventArgs) Handles twelveEightyBySevinTwonty.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "1280x720"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub SeventeenSixtyEightByNineNineTwo_Click(sender As Object, e As EventArgs) Handles seventeenSixtyEightByNineNineTwo.Click
        CheckForIllegalCrossThreadCalls = False
        rez = "1768x992"
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Public Function AdminUser() As Boolean

        Dim blnAdmin As Boolean
        Try
            Dim wiUser As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim wpPrincipal As New WindowsPrincipal(wiUser)
            blnAdmin = wpPrincipal.IsInRole(WindowsBuiltInRole.Administrator)
        Catch uaex As UnauthorizedAccessException
            blnAdmin = False
        Catch ex As Exception
            blnAdmin = False
        End Try
        Return blnAdmin

    End Function
End Class
