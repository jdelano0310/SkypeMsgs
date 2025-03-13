<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDateMessages
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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDateMessages))
        dgvMessages = New DataGridView()
        From = New DataGridViewTextBoxColumn()
        Message = New DataGridViewTextBoxColumn()
        Received = New DataGridViewTextBoxColumn()
        CType(dgvMessages, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvMessages
        ' 
        dgvMessages.AllowUserToAddRows = False
        dgvMessages.AllowUserToDeleteRows = False
        dgvMessages.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvMessages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMessages.Columns.AddRange(New DataGridViewColumn() {From, Message, Received})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        dgvMessages.DefaultCellStyle = DataGridViewCellStyle4
        dgvMessages.Dock = DockStyle.Fill
        dgvMessages.Location = New Point(0, 0)
        dgvMessages.MultiSelect = False
        dgvMessages.Name = "dgvMessages"
        dgvMessages.RowHeadersVisible = False
        dgvMessages.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvMessages.Size = New Size(800, 450)
        dgvMessages.TabIndex = 7
        ' 
        ' From
        ' 
        DataGridViewCellStyle1.ForeColor = Color.Black
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.Black
        From.DefaultCellStyle = DataGridViewCellStyle1
        From.HeaderText = "From"
        From.Name = "From"
        ' 
        ' Message
        ' 
        DataGridViewCellStyle2.SelectionForeColor = Color.Black
        Message.DefaultCellStyle = DataGridViewCellStyle2
        Message.HeaderText = "Message"
        Message.Name = "Message"
        Message.Width = 535
        ' 
        ' Received
        ' 
        DataGridViewCellStyle3.ForeColor = Color.Black
        DataGridViewCellStyle3.SelectionBackColor = Color.White
        DataGridViewCellStyle3.SelectionForeColor = Color.Black
        Received.DefaultCellStyle = DataGridViewCellStyle3
        Received.HeaderText = "Received"
        Received.Name = "Received"
        Received.Width = 140
        ' 
        ' frmDateMessages
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvMessages)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frmDateMessages"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Date View"
        CType(dgvMessages, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvMessages As DataGridView
    Friend WithEvents From As DataGridViewTextBoxColumn
    Friend WithEvents Message As DataGridViewTextBoxColumn
    Friend WithEvents Received As DataGridViewTextBoxColumn

End Class
