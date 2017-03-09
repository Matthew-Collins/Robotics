Imports Windows.Devices.Enumeration
Imports Windows.Devices.I2C

Namespace Model
    Public NotInheritable Class i2c
        'Public Shared Async Function NewAsync() As Task(Of i2c)
        '    Dim i2c As New i2c

        '    Dim Controllers As DeviceInformationCollection = Await DeviceInformation.FindAllAsync(I2cDevice.GetDeviceSelector())
        '    'Dim Settings = New I2cConnectionSettings(ADDRESS)
        '    'Settings.BusSpeed = I2cBusSpeed.StandardMode
        '    ''Settings.SharingMode = I2cSharingMode.Exclusive
        '    '.Bus = Await I2cDevice.FromIdAsync(Controllers(0).Id, Settings)
        '    'Debug.WriteLine(.Bus.DeviceId)

        '    Return i2c
        'End Function
        Public Shared Function GetHtml() As String

            'Dim Controllers As DeviceInformationCollection = Await DeviceInformation.FindAllAsync()

            Return I2cDevice.GetDeviceSelector()

        End Function
    End Class
End Namespace
