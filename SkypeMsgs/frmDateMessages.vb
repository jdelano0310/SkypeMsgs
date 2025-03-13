Imports SkypeMsgs.clsMessages
Imports System.Web
Imports System.IO
Imports Microsoft.AspNetCore.Components

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

    Private Async Sub frmDateMessages_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' display the messages for the selected date
        Dim dateMessages As List(Of clsMessages.Message) = clsMessages.GetMessagesForDate(searchDate)
        Dim htmlTest As String = $"<html><head><title>Messages for {searchDate}</title><style>tr {{display: block; padding-bottom: 5px}}</style></head><body><table style='width:95%;margin-left: auto; margin-right: auto'>#HTMLString#</table></body></html>"
        Dim tempStr As String = ""
        Dim msgTime As String
        Dim lastFromName As String = ""
        Dim tdStyle As String = "style='inline-size: 120ch; word-break: break-all; padding: 6px 6px 6px 6px;border-radius: 10px;background-color:{displayBGColor};width: 90%'"
        Dim spanStyle As String = "style='color:#D3D3D3;font-size: 8pt; padding-left: 10px; width: 10%'"

        htmlFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "temp.html")

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

                        tdStyle = tdStyle.Replace("{displayBGColor}", displayBGColor) ' replace the placeholder with the actual color

                        ' use the HttpUtility.HtmlDecode method to decode the HTML entities in the message content
                        tempStr += $"<tr><td colspan=2>{displayName}</td></tr><tr><td {tdStyle}>{HttpUtility.HtmlDecode(msg.Content)}</td><td><span {spanStyle}>{msgTime}<span></td></tr>"

                        tdStyle = tdStyle.Replace(displayBGColor, "{displayBGColor}") ' restore the placeholder
                    End If

                Case "Event/Call"
                    ' exclude call start messages
                    If Not msg.Content.Contains("start") Then
                        eventCall = ParseCallDetails(msg.Content)

                        If eventCall.Duration > 0 Then
                            tempStr += $"<tr><td colspan=2><b>{msgTime}</b>   {eventCall.FromName} called {eventCall.ToName} for {Format(eventCall.Duration / 60, "###.#0")} minutes</td></tr>"
                        Else
                            tempStr += $"<tr><td colspan=2><b>{msgTime}</b>  {eventCall.ToName} <span style='color: red'>Missed</span> call from {eventCall.FromName}</td></tr>"
                        End If

                    End If
            End Select

        Next

        ' place the HTML im a file for WebBrowser control to display
        tempStr = tempStr.Replace(eventCall.ToIdentity, eventCall.ToName) ' replace the identity with the name
        tempStr = tempStr.Replace(eventCall.FromIdentity, eventCall.FromName) ' replace the identity with the name

        htmlTest = htmlTest.Replace("#HTMLString#", tempStr)
        File.WriteAllText(htmlFileName, htmlTest)

        Await bwvMessages.WebView.EnsureCoreWebView2Async()

        bwvMessages.WebView.CoreWebView2.Navigate("file:///" & htmlFileName)
    End Sub

    Private Sub frmDateMessages_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            ' delete the temporary HTML file, if this fails, it's not a big deal
            If htmlFileName.Length > 0 Then
                File.Delete(htmlFileName)
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class