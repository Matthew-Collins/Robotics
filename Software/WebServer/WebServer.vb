Imports System.Net
Imports System.Net.Http

Public NotInheritable Class WebServer
    Private Deferral As Background.BackgroundTaskDeferral
    Private WithEvents Listener As New HttpListener(IPAddress.Parse("192.168.1.107"), 8081)
    Public Sub New(Deferral As Background.BackgroundTaskDeferral)
        Me.Deferral = Deferral
    End Sub
    Public Sub Start()
        Me.Listener.Start()
    End Sub
    Public Sub [Stop]()
        Me.Listener.Close()
    End Sub
    Private Async Sub Listener_Request(sender As Object, e As HttpListenerRequestEventArgs) Handles Listener.Request
        If e.Request.Method = HttpMethods.Get Then
            Await e.Response.WriteContentAsync($"Hello from Server at: {DateTime.Now}{vbCrLf}")
        Else
            e.Response.MethodNotAllowed()
        End If
        e.Response.Close()
    End Sub
End Class
