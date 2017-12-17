Imports System.ComponentModel
Imports System.IO
Imports System.Xml
Public Class Addons

    Dim WorkingDir As String = My.Settings.workingdir
    Dim AddonsDir As String = WorkingDir & "\Interface\AddOns\"
    Public AddonNamesList As New List(Of String)()
    Public UrlsList As New List(Of String)()
    Public DiscriptionsList As New List(Of String)()
    Public typesList As New List(Of String)()
    Public AddonsToDeleteList As New List(Of String)()

    Public Sub Addons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateXMLDims()
        PopulateTypeDropdown()
        PopulateInstalledAddonList()

    End Sub

    Private Sub Addons_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        PopulateListbox()

    End Sub

    Public Sub PopulateInstalledAddonList()
        If Directory.Exists(AddonsDir) Then
            ListBox2.Items.Clear()
            If AddonsDir = Nothing Then Exit Sub
            Dim dInfo As New System.IO.DirectoryInfo(AddonsDir)
            For Each dir As System.IO.DirectoryInfo In dInfo.GetDirectories()
                If Not dir.Name Like "Blizzard_*" Then ListBox2.Items.Add(dir.Name)
            Next
        End If

    End Sub

    Public Sub PopulateXMLDims()
        Dim xmldoc As New XmlDocument()
        Dim xmlnode As XmlNodeList
        Dim i As Integer
        Dim fs As New FileStream("addons.xml", FileMode.Open, FileAccess.Read)
        xmldoc.Load(fs)
        xmlnode = xmldoc.GetElementsByTagName("Addons")
        Dim an1 As String = ""
        Dim aurl1 As String = ""
        Dim ad1 As String = ""
        Dim at1 As String = ""
        Dim testArray(5) As String
        For i = 0 To xmlnode.Count - 1
            Dim an As String
            Dim aurl As String
            Dim ad As String
            Dim at As String
            With xmlnode(i).ChildNodes
                an = .Item(0).InnerText.Trim()
                aurl = .Item(1).InnerText.Trim()
                ad = .Item(2).InnerText.Trim()
                at = .Item(3).InnerText.Trim()
                AddonNamesList.Add(an)
                UrlsList.Add(aurl)
                DiscriptionsList.Add(ad)
                typesList.Add(at)
            End With
        Next

    End Sub

    Public Sub PopulateListbox()
        ListBox1.Items.Clear()
        For i = 0 To AddonNamesList.Count - 1
            ListBox1.Items.Add(AddonNamesList(i))
        Next
    End Sub

    Private Sub Addons_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        trrblauncher.TRRMainForm.Show()

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        For i As Integer = 0 To AddonNamesList.Count - 1
            If AddonNamesList(i) = ListBox1.SelectedItem Then

                RichTextBox1.Clear()
                RichTextBox1.Text = DiscriptionsList(i)
            End If
        Next

    End Sub

    Private Sub PopulateTypeDropdown()
        ToolStripButton1.DropDownItems.Clear()
        ToolStripButton1.DropDownItems.Add("ALL")
        Dim buttonlist As String
        For i As Integer = 0 To typesList.Count - 1
            If buttonlist = "" Then buttonlist = "1"
            If Not buttonlist = typesList(i) Then
                ToolStripButton1.DropDownItems.Add(typesList(i))
            End If
            buttonlist = typesList(i)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SelectedItem As String = ListBox1.SelectedItem
        If Not SelectedItem = "" Then
            My.Settings.patch = "installAddon"
            My.Settings.installuninstall = SelectedItem
            My.Settings.Save()
            Download.ShowDialog()
        End If
    End Sub

    Private Sub ToolStripButton1_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripButton1.DropDownItemClicked
        ToolStripButton1.Image = My.Resources.DarkBackground
        Dim clickedtext As String = e.ClickedItem.Text
        ToolStripButton1.Text = clickedtext
        ListBox1.Items.Clear()
        If clickedtext = "ALL" Then
            PopulateListbox()
        Else
            For i = 0 To typesList.Count - 1
                If typesList(i) = clickedtext Then
                    ListBox1.Items.Add(AddonNamesList(i))
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddonsToDeleteList.Clear()
        For Each item1 As String In ListBox2.SelectedItems
            AddonsToDeleteList.Add(item1)
        Next
        My.Settings.installuninstall = String.Join(", ", AddonsToDeleteList.ToArray())
        My.Settings.patch = "DeleteAddons"
        My.Settings.Save()
        Download.ShowDialog()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Settings.patch = "backup"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        AddonsToDeleteList.Clear()
        For Each item1 As String In ListBox2.Items
            AddonsToDeleteList.Add(item1)
        Next
        My.Settings.installuninstall = String.Join(", ", AddonsToDeleteList.ToArray())
        My.Settings.patch = "DeleteAddons"
        My.Settings.Save()
        Download.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim addonsDir As String = WorkingDir & "\Interface\AddOns"
        If IO.Directory.Exists(addonsDir) Then Process.Start(WorkingDir & "\Interface\AddOns") Else MsgBox("Unable to locate the addons folder!!", MsgBoxStyle.OkOnly, "Addons Folder???")
    End Sub
End Class