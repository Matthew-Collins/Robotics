Imports System.Text
Imports Windows.Networking.Sockets
Imports Windows.Storage.Streams

Public Class WebServer
    Private Const BufferSize = 1024 * 8
    Private WithEvents Listener As New StreamSocketListener
    Public Async Function StartAsync() As Task
        Await Listener.BindServiceNameAsync("8081").AsTask
    End Function
    Private Async Sub Listener_ConnectionReceived(sender As StreamSocketListener, args As StreamSocketListenerConnectionReceivedEventArgs) Handles Listener.ConnectionReceived

        Dim Request As New StringBuilder

        Using Input = args.Socket.InputStream
            Dim Data(BufferSize - 1) As Byte
            Dim Buffer = Data.AsBuffer
            Dim DataRead = CUInt(BufferSize)
            While DataRead = BufferSize
                Await Input.ReadAsync(Buffer, BufferSize, InputStreamOptions.Partial)
                Request.Append(Encoding.UTF8.GetString(Data, 0, Data.Length))
                DataRead = Buffer.Length
            End While
        End Using

        Using Output = args.Socket.OutputStream
            Using Response = Output.AsStreamForWrite

                Dim Page As New IO.StringWriter
                Page.WriteLine("Hello World")

                Dim BodyArray = Encoding.UTF8.GetBytes(Page.ToString)
                Dim BodyStream = New MemoryStream(BodyArray)

                Dim Header = $"HTTP/1.1 200 OK{vbCrLf}Content-Length: {BodyStream.Length}{vbCrLf}Connection: close{vbCrLf}{vbCrLf}"
                Dim HeaderArray = Encoding.UTF8.GetBytes(Header)

                Await Response.WriteAsync(HeaderArray, 0, HeaderArray.Length)
                Await BodyStream.CopyToAsync(Response)
                Await Response.FlushAsync()

            End Using
        End Using

    End Sub

End Class
