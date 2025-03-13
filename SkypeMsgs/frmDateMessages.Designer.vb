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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDateMessages))
        bwvMessages = New Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView()
        SuspendLayout()
        ' 
        ' bwvMessages
        ' 
        bwvMessages.Dock = DockStyle.Fill
        bwvMessages.Location = New Point(0, 0)
        bwvMessages.Name = "bwvMessages"
        bwvMessages.Size = New Size(800, 450)
        bwvMessages.TabIndex = 0
        bwvMessages.Text = "BlazorWebView1"
        ' 
        ' frmDateMessages
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(bwvMessages)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frmDateMessages"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Date View"
        ResumeLayout(False)
    End Sub

    Friend WithEvents bwvMessages As Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView
End Class
