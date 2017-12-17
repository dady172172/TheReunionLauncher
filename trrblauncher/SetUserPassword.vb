Imports System.ComponentModel

Public Class SetUserPassword
    Dim usernames As String()
    Dim passwords As String()

    Private Sub SetUserPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateUsernamesPasswords()
        If Not My.Settings.UserName = "" Then
            If My.Settings.UserName = "Off" Then TextBox1.Text = "" : TextBox2.Text = "" Else TextBox1.Text = My.Settings.UserName : TextBox2.Text = My.Settings.Password
        Else
            TextBox1.Text = "" : TextBox2.Text = ""
        End If

    End Sub

    Private Sub SetUserPassword_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        TRRMainForm.TRRMainForm_Load(sender:=Nothing, e:=Nothing)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            Dim msgre As MsgBoxResult = MsgBox("Are you sure you want to quit?", MsgBoxStyle.YesNoCancel, "Quit?")
            If msgre = MsgBoxResult.Cancel Then
                Exit Sub
            ElseIf msgre = MsgBoxResult.No Then
                Exit Sub
            ElseIf msgre = MsgBoxResult.Yes Then
                My.Settings.AutoLogin = False
                My.Settings.Save()
                Me.Close()
            Else
                Me.Close()
            End If
        ElseIf Not TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("Password field is empty.", MsgBoxStyle.OkOnly, "Need Password")
        ElseIf TextBox1.Text = "" And Not TextBox2.Text = "" Then
            MsgBox("Username field is empty.", MsgBoxStyle.OkOnly, "Need Username")
        ElseIf Not TextBox1.Text = "" And Not TextBox2.Text = "" Then
            If Not My.Settings.Usernames = "" Then
                For i = 0 To usernames.Length - 1
                    If usernames(i) = TextBox1.Text Then
                        passwords(i) = TextBox2.Text
                        Dim passwordsstring As String = ""
                        For ii = 0 To passwords.Length - 1
                            If passwordsstring = "" Then
                                passwordsstring = passwords(ii)
                            Else
                                passwordsstring = passwordsstring & "," & passwords(ii)
                            End If

                        Next
                        My.Settings.Passwords = passwordsstring

                        My.Settings.UserName = TextBox1.Text
                        My.Settings.Password = TextBox2.Text
                        My.Settings.Save()
                        PopulateUsernamesPasswords()
                        Exit Sub
                    End If
                Next
            End If
            If My.Settings.Usernames = "" Then My.Settings.Usernames = TextBox1.Text Else My.Settings.Usernames = My.Settings.Usernames & "," & TextBox1.Text
            If My.Settings.Passwords = "" Then My.Settings.Passwords = TextBox2.Text Else My.Settings.Passwords = My.Settings.Passwords & "," & TextBox2.Text
            My.Settings.UserName = TextBox1.Text
            My.Settings.Password = TextBox2.Text
            My.Settings.Save()
            PopulateUsernamesPasswords()
        End If
    End Sub

    Private Sub PopulateUsernamesPasswords()
        ListBox1.Items.Clear()
        Dim username As String = My.Settings.Usernames
        Dim password As String = My.Settings.Passwords
        If Not username = "" Then
            If username Like "*,*" Then usernames = username.Split(New Char() {","c}) Else usernames = username.Split(New Char() {","c})
        End If
        If Not password = "" Then
            If password Like "*,*" Then passwords = password.Split(New Char() {","c}) Else passwords = password.Split(New Char() {","c})
        End If
        If Not username = "" Then
            For i = 0 To usernames.Length - 1
                ListBox1.Items.Add(usernames(i))
            Next
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Not usernames(0) = "" Then
            For i = 0 To usernames.Length - 1
                If usernames(i) = ListBox1.SelectedItem Then
                    TextBox1.Text = usernames(i)
                    TextBox2.Text = passwords(i)
                    My.Settings.UserName = TextBox1.Text
                    My.Settings.Password = TextBox2.Text
                    My.Settings.Save()
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not usernames(0) = "" Then
            Dim NewUsername As String = ""
            Dim NewPassword As String = ""
            For i = 0 To usernames.Length - 1
                If Not usernames(i) = ListBox1.SelectedItem Then
                    If NewUsername = "" Then
                        NewUsername = usernames(i)
                        NewPassword = passwords(i)
                    Else
                        NewUsername = NewUsername & "," & usernames(i)
                        NewPassword = NewPassword & "," & passwords(i)
                    End If
                End If
            Next
            My.Settings.Usernames = NewUsername
            My.Settings.Passwords = NewPassword
            My.Settings.Save()
            PopulateUsernamesPasswords()
            If ListBox1.Items.Count > 0 Then
                ListBox1.SelectedIndex = 0
            Else
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If

        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Button1.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class