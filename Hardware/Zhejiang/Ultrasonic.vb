Imports Windows.Devices.Gpio

Public Class Ultrasonic

    'Dependancy: Must have HC-SR04 Ultrasonic Senor

    Public Pin As GpioPin

    Public Enum States
        State_Before_Triggered = 0
        State_After_Triggered = 1
        State_Signal_Back = 2
        State_Signal_Back_And_Stopped = 3
        State_TimeOut_Starting = 10
        State_TimeOut_Stopping = 20
    End Enum

    Public Property Max_Attempts As Integer = 1000

    Public Sub New(Optional PinNumber As Integer = 20)
        Dim Controller = GpioController.GetDefault
        Me.Pin = Controller?.OpenPin(PinNumber)
    End Sub

    Public Function GetDistance() As Integer
        Dim Distance As Integer = -1
        Dim State As States
        Dim Stopwatch As New Stopwatch
        Dim Attempts As Integer

        State = States.State_Before_Triggered
        Me.Pin.SetDriveMode(GpioPinDriveMode.Output)
        Me.Pin.Write(GpioPinValue.High)
        Task.Delay(1).Wait()
        Me.Pin.Write(GpioPinValue.Low)
        Me.Pin.SetDriveMode(GpioPinDriveMode.Input)

        State = States.State_After_Triggered

        Do
            Select Case State

                Case States.State_After_Triggered
                    If Me.Pin.Read = GpioPinValue.High Then
                        State = States.State_Signal_Back
                        Debug.WriteLine($"{Attempts} - High - {State.ToString}")
                        Stopwatch.Start()
                        Attempts = 0
                    Else
                        Attempts += 1
                        Debug.WriteLine($"{Attempts} - Low - {State.ToString}")
                        If Attempts > Max_Attempts Then
                            State = States.State_TimeOut_Starting
                            Exit Do
                        End If
                    End If

                Case States.State_Signal_Back
                    If Me.Pin.Read = GpioPinValue.Low Then
                        State = States.State_Signal_Back_And_Stopped
                        Debug.WriteLine($"{Attempts} - Low - {State.ToString}")
                        Exit Do
                    Else
                        Attempts += 1
                        Debug.WriteLine($"{Attempts} - High - {State.ToString}")
                        If Attempts > Max_Attempts Then
                            State = States.State_TimeOut_Stopping
                            Exit Do
                        End If
                    End If

            End Select
        Loop
        Stopwatch.Stop()
        Debug.WriteLine($"{Attempts} - Done - {State.ToString}")

        Select Case State
            Case States.State_Signal_Back_And_Stopped
                Distance = CInt(Stopwatch.Elapsed.TotalSeconds * 34000 / 2)
                Debug.WriteLine($"Elapsed:  {Stopwatch.Elapsed.TotalSeconds}")
                Debug.WriteLine($"Distance: {Distance}")
            Case States.State_TimeOut_Starting
                Distance = -10
            Case States.State_TimeOut_Stopping
                Distance = -20
            Case Else
                Distance = -1
        End Select

        Return Distance
    End Function
End Class
