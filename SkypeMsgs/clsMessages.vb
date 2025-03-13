Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class clsMessages
    Private _skypeExport As Export

    Public Class Export
        <JsonPropertyName("userId")>
        Public Property UserId As String

        <JsonPropertyName("exportDate")>
        Public Property ExportDate As DateTime

        <JsonPropertyName("conversations")>
        Public Property Conversations As List(Of Conversation)
    End Class

    Public Class Conversation
        <JsonPropertyName("id")>
        Public Property Id As String

        <JsonPropertyName("displayName")>
        Public Property DisplayName As String

        <JsonPropertyName("version")>
        Public Property Version As Long

        <JsonPropertyName("properties")>
        Public Property Properties As Properties

        <JsonPropertyName("threadProperties")>
        Public Property ThreadProperties As Object

        <JsonPropertyName("MessageList")>
        Public Property MessageList As List(Of Message)
    End Class

    Public Class Properties
        <JsonPropertyName("conversationblocked")>
        Public Property ConversationBlocked As Boolean

        <JsonPropertyName("lastimreceivedtime")>
        Public Property LastImReceivedTime As DateTime?

        <JsonPropertyName("consumptionhorizon")>
        Public Property ConsumptionHorizon As String

        <JsonPropertyName("conversationstatus")>
        Public Property ConversationStatus As String
    End Class

    Public Class Message
        <JsonPropertyName("id")>
        Public Property Id As String

        <JsonPropertyName("displayName")>
        Public Property DisplayName As String

        <JsonPropertyName("originalarrivaltime")>
        Public Property OriginalArrivalTime As DateTime?

        <JsonPropertyName("messagetype")>
        Public Property MessageType As String

        <JsonPropertyName("version")>
        Public Property Version As Long

        <JsonPropertyName("content")>
        Public Property Content As String

        <JsonPropertyName("conversationid")>
        Public Property ConversationId As String

        <JsonPropertyName("from")>
        Public Property From As String

        <JsonPropertyName("properties")>
        Public Property Properties As Dictionary(Of String, Object)

        <JsonPropertyName("amsreferences")>
        Public Property AmsReferences As Object
    End Class

    Public Sub DeserializeSkypeExport(json As String)

        ' deserialize the JSON string into the SkypeExport object
        Dim options As New JsonSerializerOptions() With {
        .PropertyNameCaseInsensitive = True
}
        _skypeExport = JsonSerializer.Deserialize(Of Export)(json, options)

    End Sub

    Public Function GetMessages(searchText As String) As List(Of Message)

        ' find messages containing the search text, ignore the messages with URIObject
        Dim messages As New List(Of Message)
        For Each conversation As Conversation In _skypeExport.Conversations
            For Each msg As Message In conversation.MessageList
                If msg.Content.Contains(searchText) And Not msg.Content.Contains("URIObject") Then
                    messages.Add(msg)
                End If
            Next
        Next

        Return messages

    End Function

    Public Function GetMessagesForDate(searchDate As String) As List(Of Message)

        ' find messages containing the search text, ignore the messages with URIObject
        Dim messages As New List(Of Message)
        For Each conversation As Conversation In _skypeExport.Conversations
            For Each msg As Message In conversation.MessageList
                If Format(msg.OriginalArrivalTime, "MM/dd/yy") = searchDate And Not msg.Content.Contains("URIObject") Then
                    messages.Add(msg)
                End If
            Next
        Next

        ' sort the messages by OriginalArrivalTime
        messages.Sort(Function(x, y) x.OriginalArrivalTime.Value.CompareTo(y.OriginalArrivalTime.Value))

        Return messages

    End Function
End Class
