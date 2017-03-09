Imports Windows.Devices.Enumeration
Imports Windows.Devices.I2C

Public Class PiconZero

    'Dependancy: Must have 4Tronix PiconZero Hat

    'Registers / etc:
    Const ADDRESS As Integer = &H22
    'Const MOTORA As Byte = 0
    Const OUTCFG0 As Byte = 2
    Const OUTPUT0 As Byte = 8
    'Const INCFG0 As Byte = 14
    'Const SETBRIGHT As Byte = 18
    'Const UPDATENOW As Byte = 19
    Const RESET As Byte = 20

    'Other:
    Const StartPoint As Integer = 0
    Const EndPoint As Integer = 180

    Public Bus As I2cDevice

    Public PlugOrder0To5 As Boolean = True
    Public Trims(15) As Integer
    Public Reversals(15) As Boolean
    Public EndPoints(15, 1) As Integer

    Public Shared Async Function NewAsync() As Task(Of PiconZero)
        Dim PiconZero As New PiconZero
        With PiconZero

            'Get i2c Device
            Dim Controllers As DeviceInformationCollection = Await DeviceInformation.FindAllAsync(I2cDevice.GetDeviceSelector())
            Dim Settings = New I2cConnectionSettings(ADDRESS)
            Settings.BusSpeed = I2cBusSpeed.StandardMode
            'Settings.SharingMode = I2cSharingMode.Exclusive
            .Bus = Await I2cDevice.FromIdAsync(Controllers(0).Id, Settings)
            Debug.WriteLine(.Bus.DeviceId)

            'Reset Device
            .CleanUp()

            'Set All Channels to Servos
            For Channel As Byte = 0 To 5
                Call .Bus.Write({OUTCFG0 + Channel, CByte(2)})
            Next

        End With
        Return PiconZero
    End Function

    Public Sub CleanUp()
        Me.Bus.Write({RESET, CByte(0)})
    End Sub

    Public Sub SetServo(Channel As Byte, Value As Integer, Optional Max As Integer = 90, Optional Min As Integer = -90)
        Value = CInt((GetReversal(Channel, (Value - Min) / (Max - Min)) * (EndPoint - StartPoint)) + StartPoint)
        Value += Me.Trims(Channel)
        Value = GetEndPointAdjustment(Channel, Value)
        SetOutput(GetChannel(Channel), CByte(Value))
    End Sub

    Public Function GetChannel(Channel As Byte) As Byte
        If Me.PlugOrder0To5 Then
            Return Channel
        Else
            Return CByte(5) - Channel
        End If
    End Function

    Public Function GetReversal(Channel As Byte, Percent As Double) As Double
        If Me.Reversals(Channel) Then
            Return 1 - Percent
        Else
            Return Percent
        End If
    End Function

    Public Function GetEndPointAdjustment(Channel As Byte, Value As Integer) As Integer
        If Value < Me.EndPoints(Channel, 0) Then
            Return Me.EndPoints(Channel, 0)
        ElseIf Me.EndPoints(Channel, 1) > 0 AndAlso Value > Me.EndPoints(Channel, 1) Then
            Return Me.EndPoints(Channel, 1)
        Else
            Return Value
        End If
    End Function

    Private Sub SetOutput(Channel As Byte, Value As Byte)
        Debug.WriteLine($"Channel: {Channel} = {Value}")
        Me.Bus.Write({OUTPUT0 + Channel, Value})
    End Sub

End Class
