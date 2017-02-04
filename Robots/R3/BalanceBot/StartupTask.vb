Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net.Http
Imports Windows.ApplicationModel.Background
Imports Windows.System.Threading
Imports AdafruitClassLibrary

Public NotInheritable Class StartupTask
    Implements IBackgroundTask

    Private MotorHat As MotorHat
    Private Stepper As MotorHat.Stepper

    Public Async Sub Run(taskInstance As IBackgroundTaskInstance) Implements IBackgroundTask.Run

        Dim Deferral = taskInstance.GetDeferral()

        Me.MotorHat = New MotorHat()
        Await MotorHat.InitAsync(1600).ConfigureAwait(False)

        Me.Stepper = MotorHat.GetStepper(200, 1)
        Me.Stepper.SetSpeed(200)

        Stepper.step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.SINGLE)
        Stepper.step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.SINGLE)

        Stepper.step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.DOUBLE)
        Stepper.step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.DOUBLE)

        Stepper.step(200, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.INTERLEAVE)
        Stepper.step(200, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.INTERLEAVE)

        'Stepper.step(10, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.MICROSTEP)
        'Stepper.step(10, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.MICROSTEP)

        Stepper.Release()

    End Sub

End Class
