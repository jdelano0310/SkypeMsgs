Imports SkypeMsgs.clsMessages
Imports System.Web
Imports System.IO

Public Class frmDateMessages
    Public clsMessages As clsMessages
    Public searchDate As String

    Private Class clsEventCall
        Public FromName As String
        Public FromIdentity As String
        Public FromBGColor As String
        Public ToName As String
        Public ToIdentity As String
        Public ToBGColor As String
        Public Duration As Integer
    End Class

    Dim eventCall As New clsEventCall
    Dim htmlFileName As String
    Dim displayName As String
    Dim displayBGColor As String

    Private Function ParseCallDetails(xmlString As String) As clsEventCall

        Dim xmlDoc As XDocument = XDocument.Parse(xmlString)

        eventCall.FromName = xmlDoc.Descendants("part")(0).Element("name").Value
        eventCall.FromIdentity = xmlDoc.Descendants("part")(0).FirstAttribute.Value
        eventCall.FromBGColor = "#C0C0C0"
        eventCall.ToName = xmlDoc.Descendants("part")(1).Element("name").Value
        eventCall.ToIdentity = xmlDoc.Descendants("part")(1).FirstAttribute.Value
        eventCall.ToBGColor = "#87CEFA"

        If xmlString.Contains("""missed""") Then
            eventCall.Duration = 0
        Else
            eventCall.Duration = Convert.ToInt32(Split(xmlDoc.Descendants("part")(0).Element("duration").Value, ".")(0)) ' extract just the left number
        End If

        Return eventCall
    End Function

    Private Sub GetDisplaName(msg As clsMessages.Message)

        Dim msgFrom As String = msg.From.Replace("8:", "")
        Dim msgDisplayName As String = IIf(msg.DisplayName Is Nothing, msgFrom, msg.DisplayName)

        ' get the display name for the identity
        If eventCall.FromName Is Nothing Then
            displayName = msgDisplayName
            displayBGColor = "#C0C0C0"

            eventCall.FromName = displayName
            eventCall.FromBGColor = "#C0C0C0"
            eventCall.FromIdentity = msgFrom

        ElseIf eventCall.ToName Is Nothing And eventCall.FromName <> msgDisplayName Then
            displayName = msgDisplayName
            displayBGColor = "#87CEFA"

            eventCall.ToName = displayName
            eventCall.ToBGColor = "#87CEFA"
            eventCall.ToIdentity = msgFrom
        Else

            If msgFrom = eventCall.FromIdentity Then
                displayName = eventCall.FromName
                displayBGColor = eventCall.FromBGColor
            Else
                displayName = eventCall.ToName
                displayBGColor = eventCall.ToBGColor
            End If

        End If

        If displayName = "" Then
            displayName = msgFrom
        End If

    End Sub

    Private Function ConvertToLocalTime(msgTime As DateTime) As DateTime
        ' convert the UTC time to local time
        Dim utcTime As DateTime = msgTime
        Dim localTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local)
        Return localTime
    End Function

    Private Sub frmDateMessages_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' display the messages for the selected date
        Dim dateMessages As List(Of clsMessages.Message) = clsMessages.GetMessagesForDate(searchDate)
        Dim msgTime As String
        Dim lastFromName As String = ""
        Dim displayMessage As String = ""
        Dim addRow As Boolean = False

        For Each msg As clsMessages.Message In dateMessages

            msgTime = Format(ConvertToLocalTime(msg.OriginalArrivalTime), "hh:mm:ss tt")
            Select Case msg.MessageType
                Case "RichText", "Text"
                    ' exclude call log messages
                    If Not msg.ConversationId.Contains("calllog") Then
                        GetDisplaName(msg)

                        ' display the name if it is different from the previous message
                        If lastFromName <> displayName Then
                            lastFromName = displayName
                        Else
                            displayName = ""
                        End If

                        displayMessage = msg.Content
                        addRow = True
                    End If

                Case "Event/Call"
                    ' exclude call start messages
                    If Not msg.Content.Contains("start") Then
                        eventCall = ParseCallDetails(msg.Content)

                        If eventCall.Duration > 0 Then
                            displayMessage = $"{eventCall.FromName} called {eventCall.ToName} for {Format(eventCall.Duration / 60, "###.#0")} minutes"
                            displayBGColor = "#FFFFFF"
                            displayName = "Ended"
                        Else
                            displayMessage = $"{eventCall.ToName} Missed call from {eventCall.FromName}"
                            displayBGColor = "#FF474C"
                        End If

                        addRow = True
                    End If
            End Select

            If addRow Then
                dgvMessages.Rows.Add(displayName, HttpUtility.HtmlDecode(displayMessage), msgTime)
                dgvMessages.Rows(dgvMessages.Rows.Count - 1).Cells(1).Style.BackColor = ColorTranslator.FromHtml(displayBGColor)
                dgvMessages.Rows(dgvMessages.Rows.Count - 1).Cells(1).Style.SelectionBackColor = ColorTranslator.FromHtml(displayBGColor)
            End If

            addRow = False
        Next

        ' fix from name in the grid to the display name
        For Each row As DataGridViewRow In dgvMessages.Rows
            If row.Cells(0).Value = eventCall.FromIdentity Then
                row.Cells(0).Value = eventCall.FromName
            End If

            If row.Cells(0).Value = eventCall.ToIdentity Then
                row.Cells(0).Value = eventCall.ToName
            End If
        Next

    End Sub

    Private Sub frmDateMessages_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub
End Class