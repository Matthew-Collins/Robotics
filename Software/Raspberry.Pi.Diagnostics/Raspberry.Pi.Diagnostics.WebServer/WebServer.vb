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
        Me.Deferral.Complete()
    End Sub
    Private Async Sub Listener_Request(sender As Object, e As HttpListenerRequestEventArgs) Handles Listener.Request
        If e.Request.Method = HttpMethods.Get Then

            'Read Uri Address
            Dim State = WebServerStates.Root
            For Each Character In e.Request.RequestUri.PathAndQuery.ToCharArray

                Select Case State

                    Case WebServerStates.Root
                        If Character = "i" OrElse Character = "I" Then
                            State = WebServerStates.i2c_i
                        End If

                    Case WebServerStates.i2c_i
                        If Character = "2" Then
                            State = WebServerStates.i2c_i2
                        End If

                    Case WebServerStates.i2c_i2
                        If Character = "c" OrElse Character = "C" Then
                            State = WebServerStates.i2c
                        End If

                    Case WebServerStates.i2c
                        If Character = "/" Then

                        Else
                            Exit For
                        End If


                End Select

            Next

            'Service Pages per Uri Address
            Select Case State
                Case WebServerStates.Root
                    Await e.Response.WriteContentAsync((New Views.Home.Welcome).GetHtml)

                Case WebServerStates.i2c
                    Await e.Response.WriteContentAsync((New Views.i2c.AllDevices).GetHtml)

                Case Else
                    'TODO: Reset Path to Root
                    Await e.Response.WriteContentAsync((New Views.Home.Welcome).GetHtml)

            End Select

        Else
            e.Response.MethodNotAllowed()
        End If
        e.Response.Close()
    End Sub
End Class

