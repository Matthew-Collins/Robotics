Public NotInheritable Class MainPage
    Inherits Page

    Private BerryIMU As New BerryIMU
    Private WithEvents Timer As New DispatcherTimer With {.Interval = New TimeSpan(0, 0, 1)}

    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.BerryIMU = Await BerryIMU.NewAsync
        Me.Timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As Object) Handles Timer.Tick
        Timer.Stop()

        Dim Axis = Me.BerryIMU.GetValues()

        Me.GyroX.Value = Axis.GyroX
        Me.GyroY.Value = Axis.GyroY
        Me.GyroZ.Value = Axis.GyroZ
        Me.AccelerometerX.Value = Axis.AccelerometerX
        Me.AccelerometerY.Value = Axis.AccelerometerY
        Me.AccelerometerZ.Value = Axis.AccelerometerZ

        'Me.FilteredX.Value
        'Me.FilteredY.Value
        'Me.FilteredZ.Value

        Me.LastUpdated.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss:fffffff}"

        Timer.Start()
    End Sub

End Class
