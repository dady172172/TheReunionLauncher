Imports System.IO
Imports System
Imports System.IO.Compression
Imports Ionic
Imports System.Net
Imports System.ComponentModel
Imports Ionic.Zip

Public Class Download
    Dim setInstall As String
    Dim setType As String
    Dim DLModel As String = "https://dl.dropboxusercontent.com/s/tkwvra1s2wicf6x/ModelPatches.zip?dl=0"
    Dim DLlogin As String = "https://dl.dropboxusercontent.com/s/thhuhfdvp0rwrmc/patch-enUS-4.MPQ?dl=0"
    Dim DLwater As String = "https://dl.dropboxusercontent.com/s/o2rct1xwc7ij48k/patch-W.MPQ?dl=0"
    Dim WD As String = My.Settings.workingdir
    Dim FileModel As String = WD & "\Data\ModelPatches.zip"
    Dim FileLogin As String
    Dim FileWater As String = WD & "\Data\patch-W.MPQ"
    Dim FileModelA As String = WD & "\Data\patch-A.MPQ"
    Dim FileModelB As String = WD & "\Data\patch-B.MPQ"
    Dim FileModelC As String = WD & "\Data\patch-C.MPQ"
    Dim FileModelD As String = WD & "\Data\patch-D.MPQ"
    Dim FileDataDir As String = WD & "\Data"
    Dim AddonsDir As String = WD & "\Interface\AddOns\"
    Dim WithEvents Modelwc As New WebClient
    Dim WithEvents Loginwc As New WebClient
    Dim WithEvents Waterwc As New WebClient
    Dim WithEvents addonwc As New WebClient
    Dim DownloadCanceled As Boolean
    Private Sub Download_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Hide()
        Button1.Show()
        Button1.Text = "Start"
        Label3.Text = ""
        Label4.Text = ""
        ProgressBar1.Value = 0
        GetSettings()
        If setInstall = "uninstall" Then Me.Text = "Uninstall Patch" Else Me.Text = "Download Patch"
        If setType = "model" And setInstall = "Download" Then Label1.Text = "Downloading Model Patches" : Label2.Text = "Click start to begin downloading Model Patches. After download they will be installed."
        If setType = "model" And setInstall = "Uninstall" Then Label1.Text = "Removing Model Patches" : Label2.Text = "Click start to remove Model Patches"
        If setType = "login" And setInstall = "Download" Then Label1.Text = "Downloading Login Screen Patch" : Label2.Text = "Click start to download the Login Screen patch"
        If setType = "login" And setInstall = "Uninstall" Then Label1.Text = "Removing Login Screen Patch" : Label2.Text = "Click start to remove Login Screen patch"
        If setType = "water" And setInstall = "Download" Then Label1.Text = "Downloading Water Patch" : Label2.Text = "Click start to download the Water patch"
        If setType = "water" And setInstall = "Uninstall" Then Label1.Text = "Removing Water Patch" : Label2.Text = "Click start to remove the Water patch"
        If setType = "addons" Then Label1.Text = "Deleting All Addons" : Label2.Text = "Click start to delete all the addons in the addons directory. This will not remove any of the default addons World of Warcraft needs."
        If setType = "backup" Then Label1.Text = "Backup Addons Folder" : Label2.Text = "Click start to backup your addons folder. When finished you can find addons.zip in the interface folder. If there is an addons.zip already in that folder it will be deleted."
        If setType = "installAddon" Then Label1.Text = "Download " & setInstall : Label2.Text = "click start to download and extract the zip file to the addons directory. When finished extracting we will delete the zip file."


        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Download_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ProgressBar1.Value = 0
        DownloadCanceled = False
    End Sub

    Private Sub GetSettings()
        setInstall = My.Settings.installuninstall
        setType = My.Settings.patch
        If My.Settings.ENUS = True Then FileLogin = WD & "\Data\enUS\patch-enUS-4.MPQ" Else FileLogin = WD & "\Data\enGB\patch=enGB-4.MPQ"
    End Sub
    '' remove patches
    Private Sub RemoveModel()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        TimerWorking.Start()
        If File.Exists(FileModel) Then File.Delete(FileModel)
        WhatAmIWorkingOn = "mb"
        BackgroundWorker2.RunWorkerAsync()
        File.Delete(FileModelA)
        WhatAmIWorkingOn = "mb"
        ProgressBar1.Value = 25
        File.Delete(FileModelB)
        WhatAmIWorkingOn = "mc"
        ProgressBar1.Value = 50
        File.Delete(FileModelC)
        WhatAmIWorkingOn = "md"
        ProgressBar1.Value = 75
        File.Delete(FileModelD)
        BackgroundWorker2.CancelAsync()
        Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100
        MsgBox("Models Patches have been deleted!")
        Me.Close()

    End Sub

    Private Sub RemoveLogin()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        TimerWorking.Start()
        BackgroundWorker2.RunWorkerAsync()
        File.Delete(FileLogin)
        BackgroundWorker2.CancelAsync()
        Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100
        MsgBox("Login Screen patch has been deleted!")
        Me.Close()
    End Sub

    Private Sub RemoveWater()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        TimerWorking.Start()
        BackgroundWorker2.RunWorkerAsync()
        File.Delete(FileWater)
        BackgroundWorker2.CancelAsync()
        Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100
        MsgBox("Water Patch has been deleted!")
        Me.Close()
    End Sub



    '' download patches
    Private Sub DownloadModel()
        ProgressBar1.Show()
        TimerWorking.Start()
        Button1.Text = "Cancel"
        If File.Exists(FileModel) Then File.Delete(FileModel)

        Modelwc.DownloadFileAsync(New Uri(DLModel), FileModel)
    End Sub

    Private Sub DownloadLogin()
        ProgressBar1.Show()
        TimerWorking.Start()
        Button1.Text = "Cancel"
        Loginwc.DownloadFileAsync(New Uri(DLlogin), FileLogin)
    End Sub

    Private Sub DownloadWater()
        ProgressBar1.Show()
        TimerWorking.Start()
        Button1.Text = "Cancel"
        Waterwc.DownloadFileAsync(New Uri(DLwater), FileWater)
    End Sub

    ' Hide stuff
    Private Sub HideStuff()
        ProgressBar1.Hide()
    End Sub

    'show stuff
    Private Sub ShowStuff()
        ProgressBar1.Show()
        ProgressBar1.Value = 0
    End Sub
    ' zip files needed to unzip
    Dim WithEvents Zip1 As Zip.ZipFile

    Private Sub ZipFiles()

        Dim ZipToUnpack As String = FileModel
        Dim TargetDir As String = FileDataDir
        ProgressBar1.Value = 0
        'BackgroundWorker2.RunWorkerAsync()

        Using Zip1 = ZipFile.Read(ZipToUnpack)

            Dim e As ZipEntry
            ' here, we extract every entry, but we could extract    
            ' based on entry name, size, date, etc.   
            For Each e In Zip1
                If File.Exists(FileModelD) Then WhatAmIWorkingOn = "md" Else If File.Exists(FileModelC) Then WhatAmIWorkingOn = "mc" Else If File.Exists(FileModelB) Then _
                    WhatAmIWorkingOn = "mb" Else If File.Exists(FileModelA) Then WhatAmIWorkingOn = "ma"
                e.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
        WhatAmIWorkingOn = "md"
        Threading.Thread.Sleep(200)
        If File.Exists(ZipToUnpack) Then My.Computer.FileSystem.DeleteFile(ZipToUnpack, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        Threading.Thread.Sleep(200)
        If File.Exists(ZipToUnpack) Then File.Delete(ZipToUnpack)
        ProgressBar1.Value = 100
        TimerWorking.Stop()
        'BackgroundWorker2.CancelAsync()
        Threading.Thread.Sleep(100)
        MsgBox("Done Downloading and installing model patches.")
        Me.Close()




    End Sub
    ' start button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Cancel" Then Cancel_OnClick() : Exit Sub
        If setInstall = "Download" Then
            If setType = "model" Then DownloadModel()
            If setType = "login" Then DownloadLogin()
            If setType = "water" Then DownloadWater()
        ElseIf setInstall = "Uninstall" Then
            If setType = "model" Then RemoveModel()
            If setType = "login" Then RemoveLogin()
            If setType = "water" Then RemoveWater()
        ElseIf setType = "addons" Then
            DeleteAddons()
        ElseIf setType = "backup" Then
            BackupAddons()
        ElseIf setType = "installAddon" Then
            InstallAddon()
        Else
            MsgBox("You shouldn't see this message box, if you do please contact Keathunsar on discord and let him know you've seen this message. ERROR = download.vb1", MsgBoxStyle.OkOnly, "ERROR!!!")
        End If
    End Sub
    Dim WhatAmIWorkingOn As String
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If WhatAmIWorkingOn = "ma" Then
            ProgressBar1.Value = 10
            If ProgressBar1.Value = 25 Then
                ProgressBar1.Value = ProgressBar1.Value - 5
            End If
        End If

        If WhatAmIWorkingOn = "mb" Then
            If ProgressBar1.Value < 25 Then ProgressBar1.Value = 25
            If ProgressBar1.Value = 50 Then ProgressBar1.Value = ProgressBar1.Value - 5
        End If

        If WhatAmIWorkingOn = "mc" Then
            If ProgressBar1.Value < 50 Then ProgressBar1.Value = 50
            If ProgressBar1.Value = 75 Then ProgressBar1.Value = ProgressBar1.Value - 5
        End If


        If WhatAmIWorkingOn = "md" Then
            If ProgressBar1.Value < 75 Then ProgressBar1.Value = 75
            If ProgressBar1.Value = 100 Then ProgressBar1.Value = ProgressBar1.Value - 5
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        Label4.Text = ProgressBar1.Value.ToString & "%"

    End Sub

    Private Sub Download_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        TRRMainForm.TRRMainForm_Load(My.Application.OpenForms, EventArgs.Empty)
        If TimerWorking.Enabled Then TimerWorking.Stop()
        Button1.Text = "Start"
    End Sub

    Private Sub Modelwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Modelwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
    End Sub

    Private Sub Modelwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Modelwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        If DownloadCanceled = True Then
            If File.Exists(WD & "\Interface\AddOns.zip") Then My.Computer.FileSystem.DeleteFile(WD & "\Interface\AddOns.zip")
            System.Threading.Thread.Sleep(200)

            Exit Sub
        End If
        ZipFiles()

    End Sub

    Private Sub Loginwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Loginwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
    End Sub

    Private Sub Loginwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Loginwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        TimerWorking.Stop()
        MsgBox("Login patch has been downloaded. Ready to play.", MsgBoxStyle.OkOnly, "Done!")
        Me.Close()
    End Sub

    Private Sub Waterwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Waterwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
    End Sub

    Private Sub Waterwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Waterwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        TimerWorking.Stop()
        MsgBox("Water patch has been downloaded. Ready to play.", MsgBoxStyle.OkOnly, "Done!")
        Me.Close()
    End Sub

    Private Sub Zip1_ExtractProgress(sender As Object, e As ExtractProgressEventArgs) Handles Zip1.ExtractProgress
        Dim percent As Integer = e.TotalBytesToTransfer / e.BytesTransferred * 100
        ProgressBar1.Value = percent
        Label4.Text = percent & "%"
        If DownloadCanceled = True Then e.Cancel = True
    End Sub

    Private Sub Zip1_ReadProgress(sender As Object, e As ReadProgressEventArgs) Handles Zip1.ReadProgress
        Dim percent As Integer = e.TotalBytesToTransfer / e.BytesTransferred * 100
        ProgressBar1.Value = percent
        Label4.Text = percent & "%"
        If DownloadCanceled = True Then e.Cancel = True
    End Sub

    '' delete addons
    Private Sub DeleteAddons()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        TimerWorking.Start()
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.WorkerReportsProgress = True
        BackgroundWorker1.RunWorkerAsync()



    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim dInfo As New System.IO.DirectoryInfo(AddonsDir)

        For Each dir As System.IO.DirectoryInfo In dInfo.GetDirectories()
            Dim dir1 As String = AddonsDir & dir.Name

            If Not dir1 Like AddonsDir & "Blizzard_*" Then
                My.Computer.FileSystem.DeleteDirectory(dir1, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)

                If dir.Name Like "!*" Then BackgroundWorker1.ReportProgress(4)
                If dir.Name Like "A*" Then BackgroundWorker1.ReportProgress(8)
                If dir.Name Like "B*" Then BackgroundWorker1.ReportProgress(12)
                If dir.Name Like "C*" Then BackgroundWorker1.ReportProgress(16)
                If dir.Name Like "D*" Then BackgroundWorker1.ReportProgress(20)
                If dir.Name Like "E*" Then BackgroundWorker1.ReportProgress(24)
                If dir.Name Like "F*" Then BackgroundWorker1.ReportProgress(28)
                If dir.Name Like "G*" Then BackgroundWorker1.ReportProgress(32)
                If dir.Name Like "H*" Then BackgroundWorker1.ReportProgress(36)
                If dir.Name Like "I*" Then BackgroundWorker1.ReportProgress(40)
                If dir.Name Like "J*" Then BackgroundWorker1.ReportProgress(44)
                If dir.Name Like "K*" Then BackgroundWorker1.ReportProgress(48)
                If dir.Name Like "L*" Then BackgroundWorker1.ReportProgress(50)
                If dir.Name Like "M*" Then BackgroundWorker1.ReportProgress(54)
                If dir.Name Like "N*" Then BackgroundWorker1.ReportProgress(58)
                If dir.Name Like "O*" Then BackgroundWorker1.ReportProgress(62)
                If dir.Name Like "P*" Then BackgroundWorker1.ReportProgress(66)
                If dir.Name Like "Q*" Then BackgroundWorker1.ReportProgress(70)
                If dir.Name Like "R*" Then BackgroundWorker1.ReportProgress(74)
                If dir.Name Like "S*" Then BackgroundWorker1.ReportProgress(75)
                If dir.Name Like "T*" Then BackgroundWorker1.ReportProgress(79)
                If dir.Name Like "U*" Then BackgroundWorker1.ReportProgress(83)
                If dir.Name Like "V*" Then BackgroundWorker1.ReportProgress(87)
                If dir.Name Like "W*" Then BackgroundWorker1.ReportProgress(92)
                If dir.Name Like "X*" Then BackgroundWorker1.ReportProgress(96)
                If dir.Name Like "Y*" Then BackgroundWorker1.ReportProgress(98)
                If dir.Name Like "Z*" Then BackgroundWorker1.ReportProgress(99)
            End If

        Next
        BackgroundWorker1.ReportProgress(100)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        If BackgroundWorker1.CancellationPending = True Then Me.Close() : Exit Sub

        Me.Close()
    End Sub

    '' cancel button click
    Private Sub Cancel_OnClick()
        If BackgroundWorker1.IsBusy Then BackgroundWorker1.CancelAsync()
        If BackgroundWorker2.IsBusy Then BackgroundWorker2.CancelAsync()
        If Modelwc.IsBusy Then Modelwc.CancelAsync()
        If Loginwc.IsBusy Then Loginwc.CancelAsync()
        If Waterwc.IsBusy Then Waterwc.CancelAsync()
        If DownloadCanceled = False Then DownloadCanceled = True
        If TimerWorking.Enabled Then TimerWorking.Stop()
        If Timer1.Enabled Then Timer1.Stop()


        If setType = "model" Then
            If File.Exists(FileModel) Then File.Delete(FileModel)
            If File.Exists(FileModelA) Then File.Delete(FileModelA)
            If File.Exists(FileModelB) Then File.Delete(FileModelB)
            If File.Exists(FileModelC) Then File.Delete(FileModelC)
            If File.Exists(FileModelD) Then File.Delete(FileModelD)
            System.Threading.Thread.Sleep(200)
            If File.Exists(FileModel) Then My.Computer.FileSystem.DeleteFile(FileModel, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            If File.Exists(FileModelA) Then My.Computer.FileSystem.DeleteFile(FileModelA, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            If File.Exists(FileModelB) Then My.Computer.FileSystem.DeleteFile(FileModelB, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            If File.Exists(FileModelC) Then My.Computer.FileSystem.DeleteFile(FileModelC, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            If File.Exists(FileModelD) Then My.Computer.FileSystem.DeleteFile(FileModelD, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            System.Threading.Thread.Sleep(200)
        End If

        If setType = "water" Then
            If File.Exists(FileWater) Then File.Delete(FileWater)
            Threading.Thread.Sleep(200)
            If File.Exists(FileWater) Then My.Computer.FileSystem.DeleteFile(FileWater, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If

        If setType = "login" Then
            If File.Exists(FileLogin) Then File.Delete(FileLogin)
            Threading.Thread.Sleep(200)
            If File.Exists(FileLogin) Then My.Computer.FileSystem.DeleteFile(FileLogin, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If
        If setType = "installAddon" Then
            Threading.Thread.Sleep(100)
            If File.Exists(WD & "\Interface\AddOns\TMPAddon.zip") Then File.Delete(WD & "\Interface\AddOns\TMPAddon.zip")
        End If

        MsgBox("Cancel Complete.")
        Me.Close()

    End Sub

    Dim timertext As String
    Private Sub TimerWorking_Tick(sender As Object, e As EventArgs) Handles TimerWorking.Tick
        If Label3.Text = "" Then
            Label3.Text = "Working."
        ElseIf Label3.Text = "Working." Then
            Label3.Text = "Working.."
        ElseIf Label3.Text = "Working.." Then
            Label3.Text = "Working..."
        ElseIf Label3.Text = "Working..." Then
            Label3.Text = "Working...."
        ElseIf Label3.Text = "Working...." Then
            Label3.Text = "Working."
        End If

    End Sub

    '' backup addons
    Private Sub BackupAddons()
        Dim zipdir2 As String = WD & "\Interface\AddOns.zip"

        ProgressBar1.Show()
        Me.Refresh()
        Button1.Text = "Cancel"
        BackgroundWorker2.RunWorkerAsync()
        WhatAmIWorkingOn = "ma"
        If File.Exists(zipdir2) Then My.Computer.FileSystem.DeleteFile(zipdir2, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        System.Threading.Thread.Sleep(200)
        If File.Exists(zipdir2) Then File.Delete(zipdir2)
        System.Threading.Thread.Sleep(200)
        WhatAmIWorkingOn = "mb"
        System.Threading.Thread.Sleep(200)
        Using Zip2 As ZipFile = New ZipFile()
            WhatAmIWorkingOn = "mb"
            Zip2.AddDirectory(AddonsDir)
            WhatAmIWorkingOn = "mc"
            Zip2.Save(zipdir2)

        End Using
        System.Threading.Thread.Sleep(200)
        WhatAmIWorkingOn = "md"


        WhatAmIWorkingOn = "md"
        BackgroundWorker2.CancelAsync()
        Threading.Thread.Sleep(200)
        ProgressBar1.Value = 100
        Label4.Text = "100%"
        MsgBox("Done! Zipped Addons folder to " & WD & "\Interface")
        Me.Close()
    End Sub

    '' install addons
    Private Sub InstallAddon()
        Dim DLLink As String = ""
        Dim addonfile As String = WD & "\Interface\AddOns\TMPAddon.zip"
        If File.Exists(addonfile) Then File.Delete(addonfile)



        For i = 0 To Addons.AddonNamesList.Count - 1
            If setInstall = Addons.AddonNamesList(i) Then
                DLLink = Addons.UrlsList(i)
            End If
        Next
        ProgressBar1.Show()
        ProgressBar1.Value = 0
        Button1.Text = "Cancel"
        addonwc.DownloadFileAsync(New Uri(DLLink), addonfile)




        'ProgressBar1.Show()
        'Button1.Text = "Cancel"
        'BackgroundWorker2.RunWorkerAsync()
        'WhatAmIWorkingOn = "ma"
        'Using zip1 = ZipFile.Read(setInstall)
        '    Dim e As ZipEntry
        '    ' here, we extract every entry, but we could extract    
        '    ' based on entry name, size, date, etc. 
        '    WhatAmIWorkingOn = "mb"
        '    For Each e In zip1
        '        e.Extract(AddonsDir, ExtractExistingFileAction.OverwriteSilently)
        '    Next
        '    WhatAmIWorkingOn = "mc"
        'End Using
        'WhatAmIWorkingOn = "md"

        'BackgroundWorker2.CancelAsync()
        'Threading.Thread.Sleep(100)
        'Label4.Text = "100%"
        'ProgressBar1.Value = 100

        'MsgBox("Done")
        'TRRMainForm.AddonListBoxSetup()
        'Me.Close()
    End Sub


    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Do Until ProgressBar1.Value = 100
            If WhatAmIWorkingOn = "ma" Then
                ProgressBar1.Value = 10
                If ProgressBar1.Value = 25 Then
                    ProgressBar1.Value = ProgressBar1.Value - 5
                End If
            End If

            If WhatAmIWorkingOn = "mb" Then
                If ProgressBar1.Value < 25 Then ProgressBar1.Value = 25
                If ProgressBar1.Value = 50 Then ProgressBar1.Value = ProgressBar1.Value - 5
            End If

            If WhatAmIWorkingOn = "mc" Then
                If ProgressBar1.Value < 50 Then ProgressBar1.Value = 50
                If ProgressBar1.Value = 75 Then ProgressBar1.Value = ProgressBar1.Value - 5
            End If


            If WhatAmIWorkingOn = "md" Then
                If ProgressBar1.Value < 75 Then ProgressBar1.Value = 75
                If ProgressBar1.Value = 99 Then ProgressBar1.Value = ProgressBar1.Value - 5
            End If
            Threading.Thread.Sleep(100)
            If ProgressBar1.Value < 100 Then
                ProgressBar1.Value = ProgressBar1.Value + 1
            End If

            Label4.Text = ProgressBar1.Value.ToString & "%"
        Loop
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Label4.Text = "100%"
        ProgressBar1.Value = 100
    End Sub

    Private Sub Zip1_ZipError(sender As Object, e As ZipErrorEventArgs) Handles Zip1.ZipError

    End Sub

    Private Sub addonwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles addonwc.DownloadProgressChanged
        Label4.Text = e.ProgressPercentage & "%"
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub addonwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles addonwc.DownloadFileCompleted
        ' unzip addon
        Label4.Text = "100%"
        UnzipAddon()

    End Sub


    Dim WithEvents UnzipAddons As Zip.ZipFile = New ZipFile
    Private Sub UnzipAddon()
        Dim addonfile As String = WD & "\Interface\AddOns\TMPAddon.zip"
        Dim TargetDir As String = WD & "\Interface\AddOns"

        Label1.Text = "Extracting zip file"
        Label2.Text = ""
        Label4.Text = "0%"
        ProgressBar1.Value = 0
        ProgressBar1.Value = 10



        'Using 
        UnzipAddons = ZipFile.Read(addonfile)
        UnzipAddons.ExtractAll(TargetDir, ExtractExistingFileAction.OverwriteSilently)

        UnzipAddons.Dispose()
        'Dim e As ZipEntry
        '' here, we extract every entry, but we could extract    
        '' based on entry name, size, date, etc.   
        'For Each e In UnzipAddons
        '    e.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently)
        'Next
        'End Using
        ProgressBar1.Value = 100
        Label4.Text = "100%"
        System.Threading.Thread.Sleep(100)
        MsgBox("Done Downloading and extracting " & setInstall & ".")

        Addons.PopulateInstalledAddonList()

        If File.Exists(addonfile) Then File.Delete(addonfile)
        Me.Close()



    End Sub

    Private Sub UnzipAddons_ExtractProgress(sender As Object, e As ExtractProgressEventArgs) Handles UnzipAddons.ExtractProgress
        Dim bytes As Integer = CInt(e.BytesTransferred)
        Dim total As Integer = CInt(e.TotalBytesToTransfer)


        If bytes > 1 And total > 1 Then

            Dim num As Long = (bytes / total) * 100
            ProgressBar1.Value = num
            Label4.Text = num & "%"
        End If

    End Sub

    'Private Sub UnzipAddons_ReadProgress(sender As Object, e As ReadProgressEventArgs) Handles UnzipAddons.ReadProgress
    '    Dim num As Integer = e.BytesTransferred * 100 / e.TotalBytesToTransfer
    '    ProgressBar1.Value = num
    '    Label4.Text = num & "%"
    'End Sub
End Class
