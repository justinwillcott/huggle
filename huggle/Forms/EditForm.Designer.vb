<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Summary = New System.Windows.Forms.TextBox()
        Me.SummaryLabel = New System.Windows.Forms.Label()
        Me.Minor = New System.Windows.Forms.CheckBox()
        Me.Watch = New System.Windows.Forms.CheckBox()
        Me.Save = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.EditTab = New System.Windows.Forms.TabPage()
        Me.FindGroup = New System.Windows.Forms.GroupBox()
        Me.ReplaceAll = New System.Windows.Forms.Button()
        Me.ReplaceLabel = New System.Windows.Forms.Label()
        Me.Replace = New System.Windows.Forms.TextBox()
        Me.FindInfo = New System.Windows.Forms.Label()
        Me.MatchCase = New System.Windows.Forms.CheckBox()
        Me.FindPrevious = New System.Windows.Forms.Button()
        Me.FindNext = New System.Windows.Forms.Button()
        Me.Find = New System.Windows.Forms.TextBox()
        Me.FindLabel = New System.Windows.Forms.Label()
        Me.WaitMessage = New System.Windows.Forms.Label()
        Me.EditStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditFind = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewTab = New System.Windows.Forms.TabPage()
        Me.ChangesTab = New System.Windows.Forms.TabPage()
        Me.EditMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeystrokeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.PageMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PageSaveToFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.PageSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PageSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.PageCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewSyntax = New System.Windows.Forms.ToolStripMenuItem()
        Me.PageText = New Huggle.WikiTextBox()
        Me.Preview = New Huggle.WebBrowser()
        Me.Diff = New Huggle.WebBrowser()
        Me.Tabs.SuspendLayout()
        Me.EditTab.SuspendLayout()
        Me.FindGroup.SuspendLayout()
        Me.EditStrip.SuspendLayout()
        Me.PreviewTab.SuspendLayout()
        Me.ChangesTab.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Enabled = False
        Me.Summary.Location = New System.Drawing.Point(13, 691)
        Me.Summary.Margin = New System.Windows.Forms.Padding(4)
        Me.Summary.MaxLength = 250
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(1013, 22)
        Me.Summary.TabIndex = 3
        Me.Summary.Text = "test"
        '
        'SummaryLabel
        '
        Me.SummaryLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SummaryLabel.AutoSize = True
        Me.SummaryLabel.Location = New System.Drawing.Point(10, 670)
        Me.SummaryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.SummaryLabel.Name = "SummaryLabel"
        Me.SummaryLabel.Size = New System.Drawing.Size(71, 17)
        Me.SummaryLabel.TabIndex = 2
        Me.SummaryLabel.Text = "Summary:"
        '
        'Minor
        '
        Me.Minor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Minor.AutoSize = True
        Me.Minor.Enabled = False
        Me.Minor.Location = New System.Drawing.Point(553, 660)
        Me.Minor.Margin = New System.Windows.Forms.Padding(4)
        Me.Minor.Name = "Minor"
        Me.Minor.Size = New System.Drawing.Size(92, 21)
        Me.Minor.TabIndex = 4
        Me.Minor.Text = "Minor edit"
        Me.Minor.UseVisualStyleBackColor = True
        '
        'Watch
        '
        Me.Watch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Watch.AutoSize = True
        Me.Watch.Enabled = False
        Me.Watch.Location = New System.Drawing.Point(668, 660)
        Me.Watch.Margin = New System.Windows.Forms.Padding(4)
        Me.Watch.Name = "Watch"
        Me.Watch.Size = New System.Drawing.Size(132, 21)
        Me.Watch.TabIndex = 5
        Me.Watch.Text = "Watch this page"
        Me.Watch.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.Location = New System.Drawing.Point(818, 655)
        Me.Save.Margin = New System.Windows.Forms.Padding(4)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(100, 28)
        Me.Save.TabIndex = 6
        Me.Save.Text = "Save"
        Me.Save.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(926, 656)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Cancel.TabIndex = 7
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.EditTab)
        Me.Tabs.Controls.Add(Me.PreviewTab)
        Me.Tabs.Controls.Add(Me.ChangesTab)
        Me.Tabs.ItemSize = New System.Drawing.Size(80, 20)
        Me.Tabs.Location = New System.Drawing.Point(4, 33)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(4)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(1040, 597)
        Me.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.Tabs.TabIndex = 1
        '
        'EditTab
        '
        Me.EditTab.Controls.Add(Me.FindGroup)
        Me.EditTab.Controls.Add(Me.WaitMessage)
        Me.EditTab.Controls.Add(Me.PageText)
        Me.EditTab.Location = New System.Drawing.Point(4, 24)
        Me.EditTab.Margin = New System.Windows.Forms.Padding(4)
        Me.EditTab.Name = "EditTab"
        Me.EditTab.Padding = New System.Windows.Forms.Padding(4)
        Me.EditTab.Size = New System.Drawing.Size(1032, 569)
        Me.EditTab.TabIndex = 0
        Me.EditTab.Text = "Edit"
        Me.EditTab.UseVisualStyleBackColor = True
        '
        'FindGroup
        '
        Me.FindGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FindGroup.Controls.Add(Me.ReplaceAll)
        Me.FindGroup.Controls.Add(Me.ReplaceLabel)
        Me.FindGroup.Controls.Add(Me.Replace)
        Me.FindGroup.Controls.Add(Me.FindInfo)
        Me.FindGroup.Controls.Add(Me.MatchCase)
        Me.FindGroup.Controls.Add(Me.FindPrevious)
        Me.FindGroup.Controls.Add(Me.FindNext)
        Me.FindGroup.Controls.Add(Me.Find)
        Me.FindGroup.Controls.Add(Me.FindLabel)
        Me.FindGroup.Location = New System.Drawing.Point(8, 504)
        Me.FindGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.FindGroup.Name = "FindGroup"
        Me.FindGroup.Padding = New System.Windows.Forms.Padding(4)
        Me.FindGroup.Size = New System.Drawing.Size(1013, 52)
        Me.FindGroup.TabIndex = 1
        Me.FindGroup.TabStop = False
        '
        'ReplaceAll
        '
        Me.ReplaceAll.Enabled = False
        Me.ReplaceAll.Location = New System.Drawing.Point(901, 15)
        Me.ReplaceAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ReplaceAll.Name = "ReplaceAll"
        Me.ReplaceAll.Size = New System.Drawing.Size(100, 28)
        Me.ReplaceAll.TabIndex = 8
        Me.ReplaceAll.Text = "Replace all"
        Me.ReplaceAll.UseVisualStyleBackColor = True
        '
        'ReplaceLabel
        '
        Me.ReplaceLabel.AutoSize = True
        Me.ReplaceLabel.Location = New System.Drawing.Point(592, 21)
        Me.ReplaceLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ReplaceLabel.Name = "ReplaceLabel"
        Me.ReplaceLabel.Size = New System.Drawing.Size(64, 17)
        Me.ReplaceLabel.TabIndex = 5
        Me.ReplaceLabel.Text = "Replace:"
        '
        'Replace
        '
        Me.Replace.BackColor = System.Drawing.SystemColors.Window
        Me.Replace.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Replace.Location = New System.Drawing.Point(667, 17)
        Me.Replace.Margin = New System.Windows.Forms.Padding(4)
        Me.Replace.Name = "Replace"
        Me.Replace.Size = New System.Drawing.Size(225, 22)
        Me.Replace.TabIndex = 7
        '
        'FindInfo
        '
        Me.FindInfo.AutoSize = True
        Me.FindInfo.Location = New System.Drawing.Point(636, 21)
        Me.FindInfo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.FindInfo.Name = "FindInfo"
        Me.FindInfo.Size = New System.Drawing.Size(12, 17)
        Me.FindInfo.TabIndex = 6
        Me.FindInfo.Text = " "
        Me.FindInfo.Visible = False
        '
        'MatchCase
        '
        Me.MatchCase.AutoSize = True
        Me.MatchCase.Location = New System.Drawing.Point(475, 20)
        Me.MatchCase.Margin = New System.Windows.Forms.Padding(4)
        Me.MatchCase.Name = "MatchCase"
        Me.MatchCase.Size = New System.Drawing.Size(102, 21)
        Me.MatchCase.TabIndex = 4
        Me.MatchCase.Text = "Match case"
        Me.MatchCase.UseVisualStyleBackColor = True
        '
        'FindPrevious
        '
        Me.FindPrevious.Enabled = False
        Me.FindPrevious.Location = New System.Drawing.Point(383, 15)
        Me.FindPrevious.Margin = New System.Windows.Forms.Padding(4)
        Me.FindPrevious.Name = "FindPrevious"
        Me.FindPrevious.Size = New System.Drawing.Size(84, 28)
        Me.FindPrevious.TabIndex = 3
        Me.FindPrevious.Text = "Previous"
        Me.FindPrevious.UseVisualStyleBackColor = True
        '
        'FindNext
        '
        Me.FindNext.Enabled = False
        Me.FindNext.Location = New System.Drawing.Point(291, 15)
        Me.FindNext.Margin = New System.Windows.Forms.Padding(4)
        Me.FindNext.Name = "FindNext"
        Me.FindNext.Size = New System.Drawing.Size(84, 28)
        Me.FindNext.TabIndex = 2
        Me.FindNext.Text = "Next"
        Me.FindNext.UseVisualStyleBackColor = True
        '
        'Find
        '
        Me.Find.BackColor = System.Drawing.SystemColors.Window
        Me.Find.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Find.Location = New System.Drawing.Point(56, 17)
        Me.Find.Margin = New System.Windows.Forms.Padding(4)
        Me.Find.Name = "Find"
        Me.Find.Size = New System.Drawing.Size(225, 22)
        Me.Find.TabIndex = 1
        '
        'FindLabel
        '
        Me.FindLabel.AutoSize = True
        Me.FindLabel.Location = New System.Drawing.Point(8, 21)
        Me.FindLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.FindLabel.Name = "FindLabel"
        Me.FindLabel.Size = New System.Drawing.Size(39, 17)
        Me.FindLabel.TabIndex = 0
        Me.FindLabel.Text = "Find:"
        '
        'WaitMessage
        '
        Me.WaitMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WaitMessage.BackColor = System.Drawing.SystemColors.Control
        Me.WaitMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WaitMessage.Location = New System.Drawing.Point(8, 10)
        Me.WaitMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.WaitMessage.Name = "WaitMessage"
        Me.WaitMessage.Size = New System.Drawing.Size(1013, 490)
        Me.WaitMessage.TabIndex = 0
        Me.WaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'EditStrip
        '
        Me.EditStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditUndo, Me.EditRedo, Me.EditSeparator1, Me.EditCut, Me.EditCopy, Me.EditPaste, Me.EditDelete, Me.EditSeparator2, Me.EditSelectAll, Me.EditSeparator3, Me.EditFind})
        Me.EditStrip.Name = "ContextMenu"
        Me.EditStrip.OwnerItem = Me.EditMenu
        Me.EditStrip.Size = New System.Drawing.Size(141, 214)
        '
        'EditUndo
        '
        Me.EditUndo.Enabled = False
        Me.EditUndo.Name = "EditUndo"
        Me.EditUndo.Size = New System.Drawing.Size(140, 24)
        Me.EditUndo.Text = "Undo"
        '
        'EditRedo
        '
        Me.EditRedo.Enabled = False
        Me.EditRedo.Name = "EditRedo"
        Me.EditRedo.Size = New System.Drawing.Size(140, 24)
        Me.EditRedo.Text = "Redo"
        '
        'EditSeparator1
        '
        Me.EditSeparator1.Name = "EditSeparator1"
        Me.EditSeparator1.Size = New System.Drawing.Size(137, 6)
        '
        'EditCut
        '
        Me.EditCut.Enabled = False
        Me.EditCut.Name = "EditCut"
        Me.EditCut.Size = New System.Drawing.Size(140, 24)
        Me.EditCut.Text = "Cut"
        '
        'EditCopy
        '
        Me.EditCopy.Enabled = False
        Me.EditCopy.Name = "EditCopy"
        Me.EditCopy.Size = New System.Drawing.Size(140, 24)
        Me.EditCopy.Text = "Copy"
        '
        'EditPaste
        '
        Me.EditPaste.Enabled = False
        Me.EditPaste.Name = "EditPaste"
        Me.EditPaste.Size = New System.Drawing.Size(140, 24)
        Me.EditPaste.Text = "Paste"
        '
        'EditDelete
        '
        Me.EditDelete.Enabled = False
        Me.EditDelete.Name = "EditDelete"
        Me.EditDelete.ShortcutKeyDisplayString = ""
        Me.EditDelete.Size = New System.Drawing.Size(140, 24)
        Me.EditDelete.Text = "Delete"
        '
        'EditSeparator2
        '
        Me.EditSeparator2.Name = "EditSeparator2"
        Me.EditSeparator2.Size = New System.Drawing.Size(137, 6)
        '
        'EditSelectAll
        '
        Me.EditSelectAll.Name = "EditSelectAll"
        Me.EditSelectAll.Size = New System.Drawing.Size(140, 24)
        Me.EditSelectAll.Text = "Select All"
        '
        'EditSeparator3
        '
        Me.EditSeparator3.Name = "EditSeparator3"
        Me.EditSeparator3.Size = New System.Drawing.Size(137, 6)
        '
        'EditFind
        '
        Me.EditFind.Name = "EditFind"
        Me.EditFind.Size = New System.Drawing.Size(140, 24)
        Me.EditFind.Text = "Find"
        '
        'PreviewTab
        '
        Me.PreviewTab.Controls.Add(Me.Preview)
        Me.PreviewTab.Location = New System.Drawing.Point(4, 24)
        Me.PreviewTab.Margin = New System.Windows.Forms.Padding(4)
        Me.PreviewTab.Name = "PreviewTab"
        Me.PreviewTab.Padding = New System.Windows.Forms.Padding(4)
        Me.PreviewTab.Size = New System.Drawing.Size(1032, 569)
        Me.PreviewTab.TabIndex = 1
        Me.PreviewTab.Text = "Preview"
        Me.PreviewTab.UseVisualStyleBackColor = True
        '
        'ChangesTab
        '
        Me.ChangesTab.Controls.Add(Me.Diff)
        Me.ChangesTab.Location = New System.Drawing.Point(4, 24)
        Me.ChangesTab.Margin = New System.Windows.Forms.Padding(4)
        Me.ChangesTab.Name = "ChangesTab"
        Me.ChangesTab.Padding = New System.Windows.Forms.Padding(4)
        Me.ChangesTab.Size = New System.Drawing.Size(1032, 569)
        Me.ChangesTab.TabIndex = 2
        Me.ChangesTab.Text = "Changes"
        Me.ChangesTab.UseVisualStyleBackColor = True
        '
        'EditMenu
        '
        Me.EditMenu.DropDown = Me.EditStrip
        Me.EditMenu.Name = "EditMenu"
        Me.EditMenu.Size = New System.Drawing.Size(47, 24)
        Me.EditMenu.Text = "Edit"
        '
        'KeystrokeTimer
        '
        Me.KeystrokeTimer.Enabled = True
        Me.KeystrokeTimer.Interval = 1000
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageMenu, Me.EditMenu, Me.ViewMenu})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuBar.Size = New System.Drawing.Size(1048, 28)
        Me.MenuBar.TabIndex = 0
        Me.MenuBar.Text = "MenuStrip1"
        '
        'PageMenu
        '
        Me.PageMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageSaveToFile, Me.PageSeparator1, Me.PageSave, Me.PageCancel})
        Me.PageMenu.Name = "PageMenu"
        Me.PageMenu.Size = New System.Drawing.Size(54, 24)
        Me.PageMenu.Text = "Page"
        '
        'PageSaveToFile
        '
        Me.PageSaveToFile.Name = "PageSaveToFile"
        Me.PageSaveToFile.Size = New System.Drawing.Size(161, 24)
        Me.PageSaveToFile.Text = "Save to file..."
        '
        'PageSeparator1
        '
        Me.PageSeparator1.Name = "PageSeparator1"
        Me.PageSeparator1.Size = New System.Drawing.Size(158, 6)
        '
        'PageSave
        '
        Me.PageSave.Name = "PageSave"
        Me.PageSave.Size = New System.Drawing.Size(161, 24)
        Me.PageSave.Text = "Save"
        '
        'PageCancel
        '
        Me.PageCancel.Name = "PageCancel"
        Me.PageCancel.ShortcutKeyDisplayString = ""
        Me.PageCancel.Size = New System.Drawing.Size(161, 24)
        Me.PageCancel.Text = "Cancel"
        '
        'ViewMenu
        '
        Me.ViewMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewSyntax})
        Me.ViewMenu.Name = "ViewMenu"
        Me.ViewMenu.Size = New System.Drawing.Size(53, 24)
        Me.ViewMenu.Text = "View"
        '
        'ViewSyntax
        '
        Me.ViewSyntax.Checked = True
        Me.ViewSyntax.CheckOnClick = True
        Me.ViewSyntax.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewSyntax.Name = "ViewSyntax"
        Me.ViewSyntax.Size = New System.Drawing.Size(180, 24)
        Me.ViewSyntax.Text = "Syntax coloring"
        '
        'PageText
        '
        Me.PageText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageText.ContextMenuStrip = Me.EditStrip
        Me.PageText.DetectUrls = False
        Me.PageText.HideSelection = False
        Me.PageText.Location = New System.Drawing.Point(8, 10)
        Me.PageText.Margin = New System.Windows.Forms.Padding(4)
        Me.PageText.Name = "PageText"
        Me.PageText.Size = New System.Drawing.Size(1012, 491)
        Me.PageText.TabIndex = 0
        Me.PageText.Text = ""
        '
        'Preview
        '
        Me.Preview.AllowWebBrowserDrop = False
        Me.Preview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Preview.IsWebBrowserContextMenuEnabled = False
        Me.Preview.Location = New System.Drawing.Point(4, 4)
        Me.Preview.Margin = New System.Windows.Forms.Padding(4)
        Me.Preview.MinimumSize = New System.Drawing.Size(27, 25)
        Me.Preview.Name = "Preview"
        Me.Preview.ScriptErrorsSuppressed = True
        Me.Preview.Size = New System.Drawing.Size(1024, 561)
        Me.Preview.TabIndex = 0
        Me.Preview.WebBrowserShortcutsEnabled = False
        '
        'Diff
        '
        Me.Diff.AllowWebBrowserDrop = False
        Me.Diff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Diff.IsWebBrowserContextMenuEnabled = False
        Me.Diff.Location = New System.Drawing.Point(4, 4)
        Me.Diff.Margin = New System.Windows.Forms.Padding(4)
        Me.Diff.MinimumSize = New System.Drawing.Size(27, 25)
        Me.Diff.Name = "Diff"
        Me.Diff.ScriptErrorsSuppressed = True
        Me.Diff.Size = New System.Drawing.Size(1024, 561)
        Me.Diff.TabIndex = 1
        Me.Diff.WebBrowserShortcutsEnabled = False
        '
        'EditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 726)
        Me.Controls.Add(Me.MenuBar)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Watch)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Minor)
        Me.Controls.Add(Me.SummaryLabel)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuBar
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(567, 359)
        Me.Name = "EditForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit"
        Me.Tabs.ResumeLayout(False)
        Me.EditTab.ResumeLayout(False)
        Me.FindGroup.ResumeLayout(False)
        Me.FindGroup.PerformLayout()
        Me.EditStrip.ResumeLayout(False)
        Me.PreviewTab.ResumeLayout(False)
        Me.ChangesTab.ResumeLayout(False)
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Summary As System.Windows.Forms.TextBox
    Friend WithEvents SummaryLabel As System.Windows.Forms.Label
    Friend WithEvents Minor As System.Windows.Forms.CheckBox
    Friend WithEvents Watch As System.Windows.Forms.CheckBox
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents EditTab As System.Windows.Forms.TabPage
    Friend WithEvents WaitMessage As System.Windows.Forms.Label
    Friend WithEvents PreviewTab As System.Windows.Forms.TabPage
    Friend WithEvents Preview As Huggle.WebBrowser
    Friend WithEvents KeystrokeTimer As System.Windows.Forms.Timer
    Friend WithEvents PageText As Huggle.WikiTextBox
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents PageMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageSaveToFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewSyntax As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditRedo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditFind As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ReplaceAll As System.Windows.Forms.Button
    Friend WithEvents ReplaceLabel As System.Windows.Forms.Label
    Friend WithEvents Replace As System.Windows.Forms.TextBox
    Friend WithEvents FindInfo As System.Windows.Forms.Label
    Friend WithEvents MatchCase As System.Windows.Forms.CheckBox
    Friend WithEvents FindPrevious As System.Windows.Forms.Button
    Friend WithEvents FindNext As System.Windows.Forms.Button
    Friend WithEvents Find As System.Windows.Forms.TextBox
    Friend WithEvents FindLabel As System.Windows.Forms.Label
    Friend WithEvents ChangesTab As System.Windows.Forms.TabPage
    Friend WithEvents Diff As Huggle.WebBrowser
End Class
