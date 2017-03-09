Imports Windows.Devices.I2C

Namespace Views.i2c
    Public NotInheritable Class AllDevices
        Public Function GetHtml() As String
            Try
                Dim Html As New IO.StringWriter
                Html.WriteLine("<html>")
                Html.WriteLine("<Head>")
                Html.WriteLine($"<Title>{App.Inject.SiteName}</Title")
                Html.WriteLine("</Head>")
                Html.WriteLine("<Body>")

                Html.WriteLine($"<b>{App.Inject.SiteName}</b><br><br>")

                Html.WriteLine("<b>All i2c Devices:</b><br>")
                Html.WriteLine(I2cDevice.GetDeviceSelector())

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
