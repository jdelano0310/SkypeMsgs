Imports System.IO
Imports System.Web
Imports System.Web.HttpUtility
Imports Windows.ApplicationModel.Search

Public Class frmMain
    Dim messagesFile As String
    Dim _jsonString As String = ""
    Dim clsMessages As New clsMessages()

    Private Sub DisplayStatus(message As String)
        lblStatus.Text = message
        Application.DoEvents()
    End Sub
    Private Function ConvertToLocalTime(msgTime As DateTime) As DateTime
        ' convert the UTC time to local time
        Dim utcTime As DateTime = msgTime
        Dim localTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local)
        Return localTime
    End Function
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' look for message.json file
        messagesFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "messages.json")

        If Not File.Exists(messagesFile) Then
            MessageBox.Show($"The messages.json file is missing. Please make sure the file is in the same directory as the application.{vbCrLf}{messagesFile}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ' search for messages

        ' load the messages from the JSON file
        If _jsonString.Length = 0 Then
            DisplayStatus("Loading messages")
            ' load the JSON file if it hasn't already been loaded
            _jsonString = File.ReadAllText(messagesFile)
        End If

        clsMessages.DeserializeSkypeExport(_jsonString)

        DisplayStatus($"Searching for {txtSearch.Text}")

        Dim messages As List(Of clsMessages.Message) = clsMessages.GetMessages(txtSearch.Text)

        dgvMessages.Rows.Clear()
        For Each msg As clsMessages.Message In messages
            ' use the HttpUtility.HtmlDecode method to decode the HTML entities in the message content
            dgvMessages.Rows.Add(Format(ConvertToLocalTime(msg.OriginalArrivalTime), "MM/dd/yy hh:mm:ss tt"), HttpUtility.HtmlDecode(msg.Content))
        Next
        DisplayStatus($"Found {dgvMessages.Rows.Count} messages containing ""{txtSearch.Text}""")


    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        ' the search button is enabled only if the search text box is not empty
        btnSearch.Enabled = txtSearch.Text.Length > 0

    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress

        If e.KeyChar = Chr(13) Then
            ' simulate a click on the search button when the Enter key is pressed
            btnSearch.PerformClick()
            e.Handled = True
        End If

    End Sub

    Private Sub dgvMessages_DoubleClick(sender As Object, e As EventArgs) Handles dgvMessages.DoubleClick

        ' if nothing is selected then exit
        If dgvMessages.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        Dim viewDate As String = Format(CDate(dgvMessages.SelectedRows(0).Cells(0).Value), "MM/dd/yy")

        ' display all messages for the selected date
        Dim frmDateMessages As New frmDateMessages()
        frmDateMessages.Text = "Messages on " & viewDate
        frmDateMessages.clsMessages = clsMessages
        frmDateMessages.searchDate = viewDate
        frmDateMessages.ShowDialog()

    End Sub
End Class