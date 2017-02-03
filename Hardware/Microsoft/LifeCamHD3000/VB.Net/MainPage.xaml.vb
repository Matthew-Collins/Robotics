Imports Windows.Media.Capture

Public NotInheritable Class MainPage
    Inherits Page

    Private WebCam As New MediaCapture

    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Await WebCam.InitializeAsync
        Me.WebCamView.Source = Me.WebCam
        Await WebCam.StartPreviewAsync
    End Sub

End Class
