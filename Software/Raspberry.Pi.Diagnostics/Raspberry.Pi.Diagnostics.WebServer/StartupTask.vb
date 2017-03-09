Imports Windows.ApplicationModel.Background

Public NotInheritable Class StartupTask
    Implements IBackgroundTask
    Public Sub Run(taskInstance As IBackgroundTaskInstance) Implements IBackgroundTask.Run
        Dim WebServer As New WebServer(taskInstance.GetDeferral())
        WebServer.Start()
    End Sub
End Class
