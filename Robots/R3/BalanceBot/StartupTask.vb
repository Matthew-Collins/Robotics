Imports AdafruitClassLibrary
Imports Windows.ApplicationModel.Background

Public NotInheritable Class StartupTask
    Implements IBackgroundTask

    Private MotorHat As MotorHat
    Private Steppers As New List(Of MotorHat.Stepper)

    Public Async Sub Run(taskInstance As IBackgroundTaskInstance) Implements IBackgroundTask.Run

        Dim Deferral = taskInstance.GetDeferral()

        Me.MotorHat = New MotorHat()
        Await MotorHat.InitAsync(1600).ConfigureAwait(False)

        Me.Steppers.Add(MotorHat.GetStepper(200, 1))
        Me.Steppers.Add(MotorHat.GetStepper(200, 2))

        For x = 0 To 1
            Me.Steppers(x).SetSpeed(500)

            Me.Steppers(x).step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.SINGLE)
            Me.Steppers(x).step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.SINGLE)

            Me.Steppers(x).step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.DOUBLE)
            Me.Steppers(x).step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.DOUBLE)

            Me.Steppers(x).step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.INTERLEAVE)
            Me.Steppers(x).step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.INTERLEAVE)

            'Stepper.step(10, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.MICROSTEP)
            'Stepper.step(10, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.MICROSTEP)

            Me.Steppers(x).Release()
        Next

    End Sub

End Class
