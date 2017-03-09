Imports Windows.Devices.Enumeration
Imports Windows.Devices.I2C

Namespace Views.i2c
    Public NotInheritable Class Value
        Public Async Function GetHtml(Address As Byte, Value As Byte, Length As Integer) As Task(Of String)
            Try
                Dim Html As New IO.StringWriter
                Html.WriteLine("<html>")
                Html.WriteLine("<Head>")
                Html.WriteLine($"<Title>{App.Inject.SiteName}</Title")
                Html.WriteLine("</Head>")
                Html.WriteLine("<Body>")

                Html.WriteLine($"<b>{App.Inject.SiteName}</b><br><br>")

                Dim Controllers As DeviceInformationCollection = Await DeviceInformation.FindAllAsync(I2cDevice.GetDeviceSelector())
                Dim DeviceSettings = New I2cConnectionSettings(Address)
                DeviceSettings.BusSpeed = I2cBusSpeed.StandardMode
                Dim Device = Await I2cDevice.FromIdAsync(Controllers(0).Id, DeviceSettings)
                Dim Buffer(Length - 1) As Byte
                Device.WriteRead({Value}, Buffer)

                Html.WriteLine($"<b>i2c Devices on: {Address}</b><br>")
                Html.WriteLine($"Value of: {Value} = {Buffer.ToString}")

                Html.WriteLine("<Body>")
                Html.WriteLine("</Body>")
                Html.WriteLine("</html>")
                Return Html.ToString
            Catch ex As Exception
                Return ex.ToString.Replace(vbLf, $"<br>{vbLf}")
            End Try
        End Function
    End Class
End Namespace