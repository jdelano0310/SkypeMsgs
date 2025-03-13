<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        txtSearch = New TextBox()
        btnSearch = New Button()
        Label1 = New Label()
        dgvMessages = New DataGridView()
        Received = New DataGridViewTextBoxColumn()
        Message = New DataGridViewTextBoxColumn()
        lblStatus = New Label()
        Label2 = New Label()
        CType(dgvMessages, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtSearch
        ' 
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.Font = New Font("Segoe UI", 10F)
        txtSearch.Location = New Point(4, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.PlaceholderText = "Enter the term to search for"
        txtSearch.Size = New Size(385, 25)
        txtSearch.TabIndex = 0
        ' 
        ' btnSearch
        ' 
        btnSearch.Enabled = False
        btnSearch.Location = New Point(392, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(67, 25)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(4, 32)
        Label1.Name = "Label1"
        Label1.Size = New Size(58, 15)
        Label1.TabIndex = 3
        Label1.Text = "Messages"
        ' 
        ' dgvMessages
        ' 
        dgvMessages.AllowUserToAddRows = False
        dgvMessages.AllowUserToDeleteRows = False
        dgvMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvMessages.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvMessages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMessages.Columns.AddRange(New DataGridViewColumn() {Received, Message})
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvMessages.DefaultCellStyle = DataGridViewCellStyle1
        dgvMessages.Location = New Point(4, 49)
        dgvMessages.MultiSelect = False
        dgvMessages.Name = "dgvMessages"
        dgvMessages.RowHeadersVisible = False
        dgvMessages.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMessages.Size = New Size(702, 376)
        dgvMessages.TabIndex = 6
        ' 
        ' Received
        ' 
        Received.HeaderText = "Received"
        Received.Name = "Received"
        Received.Width = 140
        ' 
        ' Message
        ' 
        Message.HeaderText = "Message"
        Message.Name = "Message"
        Message.Width = 535
        ' 
        ' lblStatus
        ' 
        lblStatus.ForeColor = Color.MediumBlue
        lblStatus.Location = New Point(111, 31)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(348, 18)
        lblStatus.TabIndex = 7
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 8.5F, FontStyle.Italic)
        Label2.ForeColor = Color.RoyalBlue
        Label2.Location = New Point(458, 32)
        Label2.Name = "Label2"
        Label2.Size = New Size(240, 15)
        Label2.TabIndex = 8
        Label2.Text = "Double click to view all messages for the day"
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(709, 427)
        Controls.Add(Label2)
        Controls.Add(lblStatus)
        Controls.Add(dgvMessages)
        Controls.Add(Label1)
        Controls.Add(btnSearch)
        Controls.Add(txtSearch)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Name = "frmMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Skpe Messages Viewer"
        CType(dgvMessages, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvMessages As DataGridView
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Received As DataGridViewTextBoxColumn
    Friend WithEvents Message As DataGridViewTextBoxColumn
End Class
