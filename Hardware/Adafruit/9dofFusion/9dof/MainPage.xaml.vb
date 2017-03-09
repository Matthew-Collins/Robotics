Imports AdafruitClassLibrary

Public NotInheritable Class MainPage
    Inherits Page

    Private Sensor As Bno055
    Private WithEvents Timer As New DispatcherTimer With {.Interval = New TimeSpan(0, 0, 2)}

    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Me.Sensor = New Bno055

        Await Me.Sensor.InitBNO055Async(Bno055.OperationMode.OPERATION_MODE_NDOF, "UART0", 18)

        Dim sysStatus = Me.Sensor.GetSystemStatus(True)
        Me.LastUpdated.Text = String.Format("System Status: Status {0}, Self-Test {1}, Error {2}",
                                            sysStatus.StatusReg,
                                            sysStatus.SelfTestResult,
                                            sysStatus.ErrorReg)

        Dim revision = Me.Sensor.GetRevision()
        Me.LastUpdated.Text &= String.Format("Revision: Software {0}, Bootloader {1}, Accel ID {2}, Mag ID {3}, Gyro ID {4}",
                                             revision.Software,
                                             revision.Bootloader,
                                             revision.AccelID,
                                             revision.MagID,
                                             revision.GyroID)

        Task.Delay(1000).Wait()

        Me.Timer.Start()

    End Sub

    Private Sub Timer_Tick(sender As Object, e As Object) Handles Timer.Tick

        Dim Gyro = Me.Sensor.ReadGyroscope
        'Dim Accelerometer = Me.Sensor.ReadAccelerometer

        Me.GyroX.Value = Gyro.X
        'Me.GyroY.Value = Gyro.Y
        'Me.GyroZ.Value = Gyro.Z

        'Me.AccelerometerX.Value = Accelerometer.X
        'Me.AccelerometerY.Value = Accelerometer.Y
        'Me.AccelerometerZ.Value = Accelerometer.Z

        Me.LastUpdated.Text = DateTime.Now.ToString("HH:mm:ss")

    End Sub
End Class
