Public NotInheritable Class MainPage
    Inherits Page

    Private BerryIMU As New BerryIMU
    Private WithEvents Timer As New DispatcherTimer With {.Interval = New TimeSpan(0, 0, 3)}

    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.BerryIMU = Await BerryIMU.NewAsync
        Me.Timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As Object) Handles Timer.Tick

        Me.BerryIMU.GetValues(Me.GyroX.Value,
                              Me.GyroY.Value,
                              Me.GyroZ.Value,
                              Me.AccelerometerX.Value,
                              Me.AccelerometerY.Value,
                              Me.AccelerometerZ.Value,
                              Me.FilteredX.Value,
                              Me.FilteredY.Value,
                              Me.FilteredZ.Value)

    End Sub

End Class
