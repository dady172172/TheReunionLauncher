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
    Dim rez As String
    Dim folder As String

    Public Sub TRRMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        patchInfoTXT = Nothing
        If My.Settings.workingdir = "" Then
            MsgBox("Unable to locate wow.exe. Please set directory to wow.exe.", MsgBoxStyle.OkOnly, "No Directory Set")
            My.Settings.workingdir = "wow"
            My.Settings.Save()
        End If
        SetDims()
        If My.Settings.CheckboxCheckForWowEXE = True Then CheckBox1.CheckState = CheckState.Checked Else CheckBox1.CheckState = CheckState.Unchecked
        If My.Settings.CheckboxSetrealmlist = True Then CheckBox2.CheckState = CheckState.Checked Else CheckBox2.CheckState = CheckState.Unchecked
        If My.Settings.raidbuttonWinMin = True Then RadioButton1.Checked = True Else RadioButton2.Checked = True
        If IO.File.Exists(patchAdir) And IO.File.Exists(patchBdir) And IO.File.Exists(patchCdir) And IO.File.Exists(patchDdir) Then
            Label1.Text = "Installed"
            Label1.ForeColor = Color.LawnGreen
            LinkLabel1.Text = "Delete"
            ToolTip1.SetToolTip(LinkLabel1, "Delete Patch")
        Else
            Label1.Text = "Not Installed"
            Label1.ForeColor = Color.Red
            LinkLabel1.Text = "Download"
            ToolTip1.SetToolTip(LinkLabel1, "Download Patch")
        End If

        If IO.File.Exists(patchWdir) Then
            Label5.Text = "Installed"
            Label5.ForeColor = Color.LawnGreen
            LinkLabel2.Text = "Delete"
            ToolTip1.SetToolTip(LinkLabel2, "Delete Patch")
        Else
            Label5.Text = "Not Installed"
            Label5.ForeColor = Color.Red
            LinkLabel2.Text = "Download"
            ToolTip1.SetToolTip(LinkLabel2, "Download Patch")
        End If

        If IO.File.Exists(patchENUSdir) Or IO.File.Exists(patchENGBdir) Then
            Label3.Text = "Installed"
            Label3.ForeColor = Color.LawnGreen
            LinkLabel3.Text = "Delete"
            ToolTip1.SetToolTip(LinkLabel3, "Delete Patch")
        Else
            Label3.Text = "Not Installed"
            Label3.ForeColor = Color.Red
            LinkLabel3.Text = "Download"
            ToolTip1.SetToolTip(LinkLabel3, "Download Patch")
        End If
        TextBoxInstallDirectory.Text = curdirdim
        CenterPanels()
        PopulateUsernameDropdown()

    End Sub

    Private Sub TRRMainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If ToolStripButton6.Text = "Off" Then My.Settings.AutoLogin = False Else My.Settings.AutoLogin = True
    End Sub

    Private Sub CenterPanels()
        If PanelBackground1.Visible = True Then PanelBackground1.Location = New Point(0, 25) : PanelBackground1.Size = New Size(Width, Height)
        If Panel1.Visible = True Then Panel1.Location = New Point((PanelBackground1.Width - Panel1.Width) \ 2, ((PanelBackground1.Height - Panel1.Height) \ 2) - 30)

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Process.Start("https://reunion-reborn.com/")

    End Sub

    Private Sub TRRMainForm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        CenterPanels()

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Process.Start("https://discordapp.com/invite/ZfpmRVp")

    End Sub

    Dim FBD1 As New FolderBrowserDialog
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FBD1 As New FolderBrowserDialog With {
            .Description = "Select the folder with Wow.exe in it. Example - C:/World of Warcraft/"
        }
        Dim FBDR1Result As DialogResult = FBD1.ShowDialog
        If FBDR1Result = DialogResult.Cancel Then
            Exit Sub
        ElseIf FBDR1Result = DialogResult.Abort Then
            Exit Sub
        ElseIf FBDR1Result = DialogResult.OK Then
            If File.Exists(FBD1.SelectedPath & "/Wow.exe") Then
                My.Settings.workingdir = FBD1.SelectedPath
                My.Settings.Save()
                MsgBox("Saved Path. You may now download patches or play.")
                TRRMainForm_Load(sender:=Nothing, e:=EventArgs.Empty)
            Else
                MsgBox("Unable to locate Wow.exe in that directory. Please try again.")
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
        If FindLineInWTF("SET accountName*") = True Then
            My.Settings.RememberAccountName = True
            My.Settings.Save()
        Else
            My.Settings.RememberAccountName = False
            My.Settings.Save()
        End If

    End Sub

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
                MsgBox("Wow.exe doesn't exist in the folder selected.", MsgBoxStyle.OkOnly, "Unable to locate Wow.exe")
            End If
        End If
        If My.Settings.CheckboxSetrealmlist = True Then
            If IO.File.Exists(remlistFile) Then
                My.Computer.FileSystem.WriteAllText(remlistFile, "set realmlist login.reunion-reborn.com", False)
            End If
        End If
        FindUsernameWTF()
        System.Threading.Thread.Sleep(200)
        SetDims()
        System.Threading.Thread.Sleep(200)
        If My.Settings.AutoLogin = True Then
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
        If RadioButton1.Checked = True Then
            Me.WindowState = FormWindowState.Minimized
        End If
        If Not IO.File.Exists(curdirdim & "\Reunion.exe") Then
            IO.File.WriteAllBytes(curdirdim & "\Reunion.exe", My.Resources.Reunion)
        End If
        WowProcess.StartInfo.FileName = curdirdim & "\Reunion.exe"
        WowProcess.Start()
        If My.Settings.AutoLogin = True Then
            Timer3.Start()
        Else
            If RadioButton1.Checked = True Then
                Me.WindowState = FormWindowState.Minimized
            ElseIf RadioButton2.Checked Then
                Me.Close()
            End If
        End If

    End Sub

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

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.raidbuttonWinMin = True
        Else
            My.Settings.raidbuttonWinMin = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.raidbuttonWinMin = False
        Else
            My.Settings.raidbuttonWinMin = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ButtonStart()
    End Sub

    Private Sub TRRMainForm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            MsgBox(path)
        Next
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

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        TRRMainForm_Load(sender:=Nothing, e:=EventArgs.Empty)
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
            If RadioButton1.Checked Then
                Me.WindowState = FormWindowState.Minimized
            ElseIf RadioButton2.Checked Then
                Me.Close()
            End If
        Else

        End If

    End Sub

    Private Sub SetScreenRez()
        If Not File.Exists(curdirdim & "\WTF\Config.wtf") Then
            MsgBox("Unable to file config.wtf file.")
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

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If File.Exists("addons.xml") Then File.Delete("addons.xml")
        Dim xmlwc As New WebClient
        xmlwc.DownloadFile("https://reunion-reborn.com/downloads/launcher/xml/addons.xml", "addons.xml")
        Addons.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripButton6_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripButton6.DropDownItemClicked
        Dim Itemname As String = e.ClickedItem.Text
        If Itemname = "Add/Edit" Then
            SetUserPassword.ShowDialog()
            Exit Sub
        ElseIf Itemname = "Off" Then
            My.Settings.AutoLogin = False
            My.Settings.Save()
            ToolStripButton6.Text = Itemname
            Exit Sub
        End If
        ToolStripButton6.Text = Itemname
        My.Settings.AutoLogin = True
        My.Settings.UserName = Itemname
        My.Settings.Password = GetPasswordByUser(Itemname)

    End Sub

    Private Sub PopulateUsernameDropdown()
        Dim userlist As String()
        ToolStripButton6.DropDownItems.Clear()
        If Not My.Settings.Usernames = "" Then
            userlist = My.Settings.Usernames.Split(New Char() {","c})
        End If
        ToolStripButton6.DropDownItems.Add("Add/Edit")
        ToolStripButton6.DropDownItems.Add("Off")
        If Not My.Settings.Usernames = "" Then
            For Each item As String In userlist
                ToolStripButton6.DropDownItems.Add(item)
            Next
        ElseIf Not My.Settings.UserName = "" Then
            ToolStripButton6.DropDownItems.Add(My.Settings.UserName)
        End If

        If My.Settings.AutoLogin = False Then
            ToolStripButton6.Text = "Off"
            Exit Sub
        End If
        If Not My.Settings.UserName = "" Then
            ToolStripButton6.Text = My.Settings.UserName
        End If

    End Sub

    Private Function GetPasswordByUser(ByVal Username As String) As String
        If Username = "" Then
            Return ""
            Exit Function
        End If
        Dim userlist As String()
        Dim Passwordlist As String() = My.Settings.Passwords.Split(New Char() {","c})
        If Not My.Settings.Usernames = "" Then
            userlist = My.Settings.Usernames.Split(New Char() {","c})
        End If
        For i = 0 To userlist.Length - 1
            If userlist(i) = Username Then
                Return Passwordlist(i)
                Exit Function
            End If
        Next

    End Function

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If LinkLabel1.Text = "Delete" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "model"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If LinkLabel2.Text = "Delete" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "water"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        If LinkLabel3.Text = "Delete" Then My.Settings.installuninstall = "Uninstall" Else My.Settings.installuninstall = "Download"
        My.Settings.patch = "login"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        If IO.Directory.Exists(curdirdim & "\Cache") Then My.Computer.FileSystem.DeleteDirectory(curdirdim & "\Cache", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing) : MsgBox("Deleted Cache!!", MsgBoxStyle.OkOnly, "Cache Deleted")
    End Sub

End Class
