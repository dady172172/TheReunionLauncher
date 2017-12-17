<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Addons
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Addons))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ALLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Button6)
        Me.GroupBox1.Controls.Add(Me.ToolStrip1)
        Me.GroupBox1.Controls.Add(Me.RichTextBox1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.GroupBox1.Location = New System.Drawing.Point(9, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(369, 352)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Download Addon"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button6.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button6.Location = New System.Drawing.Point(191, 24)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(95, 23)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "Install Zip"
        Me.ToolTip1.SetToolTip(Me.Button6, "Install an addon that you downloaded from the internet. Must be a zipfile.")
        Me.Button6.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.Black
        Me.ToolStrip1.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.CanOverflow = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(6, 21)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(179, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.AutoSize = False
        Me.ToolStripButton1.BackColor = System.Drawing.Color.Black
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ALLToolStripMenuItem})
        Me.ToolStripButton1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.ToolStripButton1.Padding = New System.Windows.Forms.Padding(2)
        Me.ToolStripButton1.Size = New System.Drawing.Size(175, 23)
        Me.ToolStripButton1.Text = "Sort by category"
        Me.ToolStripButton1.ToolTipText = "Select an addon category." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Default is ALL"
        '
        'ALLToolStripMenuItem
        '
        Me.ALLToolStripMenuItem.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ALLToolStripMenuItem.Name = "ALLToolStripMenuItem"
        Me.ALLToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ALLToolStripMenuItem.Text = "ALL"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Menu
        Me.RichTextBox1.Location = New System.Drawing.Point(191, 82)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(166, 263)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = "Select an addon for description."
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(191, 53)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Download"
        Me.ToolTip1.SetToolTip(Me.Button1, "Download the selected addon.")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Location = New System.Drawing.Point(6, 53)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(179, 292)
        Me.ListBox1.Sorted = True
        Me.ListBox1.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.ListBox1, "Select an addon to see discription. Once selected you can use download to downloa" &
        "d the addon and extract the zip to your addons folder.")
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.ListBox2)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.GroupBox2.Location = New System.Drawing.Point(384, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(292, 352)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Installed Addons"
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button5.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button5.Location = New System.Drawing.Point(6, 140)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(95, 23)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Open Folder"
        Me.ToolTip1.SetToolTip(Me.Button5, "Opens your addons folder.")
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button4.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Location = New System.Drawing.Point(6, 111)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(95, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Delete All"
        Me.ToolTip1.SetToolTip(Me.Button4, "Deletes all addons except the default addons wow needs.")
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button3.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button3.Location = New System.Drawing.Point(6, 82)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(95, 23)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Backup"
        Me.ToolTip1.SetToolTip(Me.Button3, "Backup addons folder. You can find the backup in your interface folder named AddO" &
        "ns.zip.")
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.BackgroundImage = Global.trrblauncher.My.Resources.Resources.DarkBackground
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Location = New System.Drawing.Point(6, 53)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(95, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.Button2, "Delete the selected addon")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.SystemColors.Menu
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Location = New System.Drawing.Point(107, 21)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox2.Size = New System.Drawing.Size(179, 324)
        Me.ListBox2.Sorted = True
        Me.ListBox2.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ListBox2, "These are your addons")
        '
        'Addons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.BackgroundImage = Global.trrblauncher.My.Resources.Resources.lichkingPantheonbackground1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(683, 370)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Addons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Addons"
        Me.GroupBox1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ToolStripButton1 As ToolStripDropDownButton
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents ALLToolStripMenuItem As ToolStripMenuItem
End Class
