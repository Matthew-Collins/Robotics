Imports AdafruitClassLibrary

Public NotInheritable Class MainPage
    Inherits Page

    Private MotorHat As New MotorHat

    Private Stepper1 As MotorHat.Stepper
    Private Stepper2 As MotorHat.Stepper

    Private WithEvents StepperThread1 As New BackgroundWorker
    Private WithEvents StepperThread2 As New BackgroundWorker

    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Await Me.MotorHat.InitAsync(1600)

        Me.Stepper1 = Me.MotorHat.GetStepper(200, 1)
        Me.Stepper2 = Me.MotorHat.GetStepper(200, 2)

        Me.Stepper1.SetSpeed(500)
        Me.Stepper2.SetSpeed(1000)

        Me.StepperThread1.RunWorkerAsync()
        Me.StepperThread2.RunWorkerAsync()

    End Sub
    Private Sub StepperThread1_DoWork(sender As Object, e As DoWorkEventArgs) Handles StepperThread1.DoWork
        Me.Stepper1.step(1000, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.DOUBLE)
        Me.Stepper1.step(1000, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.DOUBLE)
        Me.Stepper1.Release()
    End Sub
    Private Sub StepperThread2_DoWork(sender As Object, e As DoWorkEventArgs) Handles StepperThread2.DoWork
        Me.Stepper2.step(1000, MotorHat.Stepper.Command.FORWARD, MotorHat.Stepper.Style.DOUBLE)
        Me.Stepper2.step(1000, MotorHat.Stepper.Command.BACKWARD, MotorHat.Stepper.Style.DOUBLE)
        Me.Stepper2.Release()
    End Sub
    Private Sub StepperThread1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles StepperThread1.RunWorkerCompleted
        Me.Status.Text &= $"{vbLf}1 - Done"
    End Sub
    Private Sub StepperThread2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles StepperThread2.RunWorkerCompleted
        Me.Status.Text &= $"{vbLf}2 - Done"
    End Sub

End Class
