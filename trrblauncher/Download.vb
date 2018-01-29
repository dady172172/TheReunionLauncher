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
    Dim WithEvents Addonwc As New WebClient
    Dim DownloadCanceled As Boolean
    Dim WithEvents Zip1 As Zip.ZipFile
    Dim WhatAmIWorkingOn As String
    Dim WithEvents ZipUpAddons As New ZipFile
    Dim WithEvents UnzipAddons As Zip.ZipFile = New ZipFile

    Private Sub Download_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Hide()
        Button1.Show()
        Button1.Text = "Start"
        Label3.Text = ""
        Label4.Text = ""
        ProgressBar1.Value = 0
        GetSettings()
        If setType = "model" And setInstall = "Download" Then Label1.Text = "Download Model Patches" : Label2.Text = "Click start to download Model patches." : Me.Text = "Download"
        If setType = "model" And setInstall = "Uninstall" Then Label1.Text = "Delete Model Patches" : Label2.Text = "Click start to delete Model patches" : Me.Text = "Delete"
        If setType = "login" And setInstall = "Download" Then Label1.Text = "Download Loginscreen Patch" : Label2.Text = "Click start to download the Loginscreen patch" : Me.Text = "Download"
        If setType = "login" And setInstall = "Uninstall" Then Label1.Text = "Delete Loginscreen Patch" : Label2.Text = "Click start to delete Loginscreen patch" : Me.Text = "Delete"
        If setType = "water" And setInstall = "Download" Then Label1.Text = "Download Water Patch" : Label2.Text = "Click start to download the Water patch" : Me.Text = "Download"
        If setType = "water" And setInstall = "Uninstall" Then Label1.Text = "Delete Water Patch" : Label2.Text = "Click start to delete the Water patch" : Me.Text = "Delete"
        If setType = "backup" Then Label1.Text = "Backup Addons Folder" : Label2.Text = "Click start to backup your addons folder. When finished you can find AddOns.zip in the interface folder. If Addons.zip already exists it will be removed." : Me.Text = "Backup"
        If setType = "installAddon" Then Label1.Text = "Download " : Label2.Text = "Click Start to download addon. " & vbNewLine & setInstall : Me.Text = "Download"
        If setType = "DeleteAddons" Then Label1.Text = "Delete Addons" : Label2.Text = "Click start to delete:" & vbNewLine & setInstall : Me.Text = "Delete"
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

    Private Sub RemoveModel()
        Button1.Text = "Cancel" : ProgressBar1.Show() : If File.Exists(FileModel) Then File.Delete(FileModel)
        WhatAmIWorkingOn = "mb" : File.Delete(FileModelA) : WhatAmIWorkingOn = "mb"
        ProgressBar1.Value = 25 : File.Delete(FileModelB) : WhatAmIWorkingOn = "mc"
        ProgressBar1.Value = 50 : File.Delete(FileModelC) : WhatAmIWorkingOn = "md"
        ProgressBar1.Value = 75 : File.Delete(FileModelD) : Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100 : MsgBox("Model patches have been removed!") : Close()

    End Sub

    Private Sub RemoveLogin()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        File.Delete(FileLogin)
        Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100
        MsgBox("Loginscreen patch has been removed!")
        Me.Close()
    End Sub

    Private Sub RemoveWater()
        Button1.Text = "Cancel"
        ProgressBar1.Show()
        File.Delete(FileWater)
        Threading.Thread.Sleep(100)
        ProgressBar1.Value = 100
        MsgBox("Water patch has been removed!")
        Me.Close()
    End Sub

    Private Sub DownloadModel()
        ProgressBar1.Show()
        Label2.Text = "Downloading patches. Please Wait."
        Button1.Text = "Cancel"
        If File.Exists(FileModel) Then File.Delete(FileModel)
        Modelwc.DownloadFileAsync(New Uri(DLModel), FileModel)

    End Sub

    Private Sub DownloadLogin()
        ProgressBar1.Show()
        Button1.Text = "Cancel"
        Loginwc.DownloadFileAsync(New Uri(DLlogin), FileLogin)

    End Sub

    Private Sub DownloadWater()
        ProgressBar1.Show()
        Button1.Text = "Cancel"
        Waterwc.DownloadFileAsync(New Uri(DLwater), FileWater)

    End Sub

    Private Sub ZipFiles()
        Label2.Text = "Extracting zip file. Please wait."
        Dim ZipToUnpack As String = FileModel
        Dim TargetDir As String = FileDataDir
        ProgressBar1.Value = 0
        Zip1 = ZipFile.Read(ZipToUnpack)
        Zip1.ExtractAll(TargetDir, ExtractExistingFileAction.OverwriteSilently)
        Zip1.Dispose()
        Threading.Thread.Sleep(200)
        If File.Exists(ZipToUnpack) Then File.Delete(ZipToUnpack)
        ProgressBar1.Value = 100
        Threading.Thread.Sleep(100)
        MsgBox("Model patches have been installed.")
        Me.Close()

    End Sub

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
        ElseIf setType = "DeleteAddons" Then
            DeleteAddons()
        ElseIf setType = "backup" Then
            BackupAddons()
        ElseIf setType = "installAddon" Then
            InstallAddon()
        Else
            MsgBox("You shouldn't see this message box. If you do please contact keathunsar on discord and let him know you have seen this message." & vbNewLine & " ERROR = download.vb1", MsgBoxStyle.OkOnly, "ERROR!!!")
        End If
    End Sub

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
        Button1.Text = "Start"
    End Sub

    Private Sub Modelwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Modelwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
        Dim rec As Integer = e.BytesReceived / 1000000
        Dim trec As Integer = e.TotalBytesToReceive / 1000000
        Label3.Text = rec & "/" & trec & "MB"
    End Sub

    Private Sub Modelwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Modelwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        If e.Cancelled Then
            If File.Exists(WD & "\Interface\AddOns.zip") Then My.Computer.FileSystem.DeleteFile(WD & "\Interface\AddOns.zip")
            System.Threading.Thread.Sleep(200)
            Exit Sub
        End If
        Label3.Text = ""
        ZipFiles()

    End Sub

    Private Sub Loginwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Loginwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
        Dim rec As Integer = e.BytesReceived / 1000000
        Dim trec As Integer = e.TotalBytesToReceive / 1000000
        Label3.Text = rec & "/" & trec & "MB"
    End Sub

    Private Sub Loginwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Loginwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        Label3.Text = ""
        MsgBox("Loginscreen patch has been installed.", MsgBoxStyle.OkOnly, "Done!")
        Me.Close()
    End Sub

    Private Sub Waterwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Waterwc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label4.Text = e.ProgressPercentage & "%"
        Dim rec As Integer = e.BytesReceived / 1000000
        Dim trec As Integer = e.TotalBytesToReceive / 1000000
        Label3.Text = rec & "/" & trec & "MB"
    End Sub

    Private Sub Waterwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Waterwc.DownloadFileCompleted
        ProgressBar1.Value = 100
        Label3.Text = ""
        MsgBox("Water patch has been installed.", MsgBoxStyle.OkOnly, "Done!")
        Me.Close()
    End Sub

    Private Sub Zip1_ExtractProgress(sender As Object, e As ExtractProgressEventArgs) Handles Zip1.ExtractProgress
        Dim bytes As Integer = CInt(e.BytesTransferred)
        Dim total As Integer = CInt(e.TotalBytesToTransfer)
        If bytes > 1 And total > 1 Then
            Dim num As Long = (bytes / total) * 100
            ProgressBar1.Value = num
            Label4.Text = num & "%"
            Label3.Text = bytes & "/" & total
        End If

        If DownloadCanceled = True Then e.Cancel = True
    End Sub

    Private Sub DeleteAddons()
        Button1.Text = "Cancel"
        Label2.Text = "Deleting Addons" & String.Join(", ", Addons.AddonsToDeleteList.ToArray())
        ProgressBar1.Show()
        ProgressBar1.Value = 0
        Label4.Text = "0%"
        Dim addonpath As String = WD & "\Interface\AddOns\"
        Dim howmany As String = Addons.AddonsToDeleteList.Count
        Dim percent As String = ""
        For Each item As String In Addons.AddonsToDeleteList
            Label3.Text = "Deleting " & item
            For i = 0 To Addons.AddonsToDeleteList.Count - 1
                If item = Addons.AddonsToDeleteList(i) Then
                    percent = (i + 1) / howmany * 100
                End If
                ProgressBar1.Value = percent
                Label4.Text = percent & "%"
            Next
            If Directory.Exists(addonpath & item) Then
                Directory.Delete(addonpath & item, True)
            Else
                MsgBox("Unable to locate the directory named " & item)
                Me.Close()
            End If
        Next
        System.Threading.Thread.Sleep(100)
        Dim couldnotdelete As New List(Of String)()
        For Each item1 As String In Addons.AddonsToDeleteList
            If Directory.Exists(addonpath & item1) Then
                couldnotdelete.Add(item1)
            End If
        Next
        If couldnotdelete.Count > 1 Then
            Dim couldnotdelete1 As String = ""
            For Each item2 As String In couldnotdelete
                If couldnotdelete1 = "" Then
                    couldnotdelete1 = item2
                Else
                    couldnotdelete1 = couldnotdelete1 & ", " & item2
                End If
            Next
            MsgBox("Unable to delete " & couldnotdelete1)
        Else
            ProgressBar1.Value = 100
            Label4.Text = "100%"
            Label3.Text = ""
            MsgBox("Deleted addons " & vbNewLine & String.Join(", ", Addons.AddonsToDeleteList.ToArray()))
            Addons.PopulateInstalledAddonList()
            Me.Close()
        End If


    End Sub

    Private Sub Cancel_OnClick()

        If Modelwc.IsBusy Then Modelwc.CancelAsync()
        If Loginwc.IsBusy Then Loginwc.CancelAsync()
        If Waterwc.IsBusy Then Waterwc.CancelAsync()
        If DownloadCanceled = False Then DownloadCanceled = True
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
            If Addonwc.IsBusy Then Addonwc.CancelAsync()
            Threading.Thread.Sleep(100)
            If File.Exists(WD & "\Interface\AddOns\TMPAddon.zip") Then File.Delete(WD & "\Interface\AddOns\TMPAddon.zip")
        End If
        MsgBox("Canceling Complete.")
        Me.Close()

    End Sub

    Private Sub BackupAddons()
        Dim Directorytozip As String = WD & "\Interface\AddOns"
        Dim ZipfileName As String = WD & "\Interface\AddOns.zip"
        ProgressBar1.Show()
        ProgressBar1.Value = 0
        Label4.Text = "0%"
        Button1.Text = "Cancel"
        Me.Refresh()
        Threading.Thread.Sleep(100)
        If File.Exists(ZipfileName) Then
            File.Delete(ZipfileName)
        End If
        ZipUpAddons.Name = ZipfileName
        ZipUpAddons.AddDirectory(Directorytozip, "AddOns")
        ZipUpAddons.Save()
        ZipUpAddons.Dispose()
        Threading.Thread.Sleep(200)
        ProgressBar1.Value = 100
        Label4.Text = "100%"
        MsgBox("Backup complete. You can find it at " & ZipfileName)
        Me.Close()
    End Sub

    Private Sub ZipUpAddons_AddProgress(sender As Object, e As AddProgressEventArgs) Handles ZipUpAddons.AddProgress
        Dim t As Integer = CInt(e.BytesTransferred)
        Dim tt As Integer = CInt(e.TotalBytesToTransfer)
        Dim percent As Integer = 0
        If t > 1 And tt > 1 Then
            percent = tt / t * 100
            Label3.Text = t & "/" & tt
        End If
        ProgressBar1.Value = percent
        Label4.Text = percent & "%"

    End Sub

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
        Addonwc.DownloadFileAsync(New Uri(DLLink), addonfile)

    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Label4.Text = "100%"
        ProgressBar1.Value = 100
    End Sub

    Private Sub Addonwc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Addonwc.DownloadProgressChanged
        Label4.Text = e.ProgressPercentage & "%"
        ProgressBar1.Value = e.ProgressPercentage
        Dim rec As Integer = e.BytesReceived / 1000000
        Dim trec As Integer = e.TotalBytesToReceive / 1000000
        Label3.Text = rec & "/" & trec & "MB"
    End Sub

    Private Sub Addonwc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Addonwc.DownloadFileCompleted
        If e.Cancelled Then Exit Sub
        Label4.Text = "100%"
        Label3.Text = ""
        System.Threading.Thread.Sleep(100)
        UnzipAddon()

    End Sub

    Private Sub UnzipAddon()
        Dim addonfile As String = WD & "\Interface\AddOns\TMPAddon.zip"
        Dim TargetDir As String = WD & "\Interface\AddOns"
        Label1.Text = "Extracting zip file"
        Label2.Text = ""
        Label4.Text = "0%"
        ProgressBar1.Value = 0
        ProgressBar1.Value = 10
        UnzipAddons = ZipFile.Read(addonfile)
        UnzipAddons.ExtractAll(TargetDir, ExtractExistingFileAction.OverwriteSilently)
        UnzipAddons.Dispose()
        ProgressBar1.Value = 100
        Label4.Text = "100%"
        System.Threading.Thread.Sleep(100)
        MsgBox("Done Downloading " & setInstall & ".")
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
            Label3.Text = bytes & "/" & total
        End If
        If DownloadCanceled = True Then e.Cancel = True

    End Sub

End Class
