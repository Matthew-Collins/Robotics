Namespace Views.Home
    Public NotInheritable Class Welcome
        Public Function GetHtml() As String
            Dim Html As New IO.StringWriter
            Html.WriteLine("<html>")
            Html.WriteLine("<Head>")
            Html.WriteLine($"<Title>{App.Inject.SiteName}</Title")
            Html.WriteLine("</Head>")
            Html.WriteLine("<Body>")

            Html.WriteLine($"<b>{App.Inject.SiteName}</b><br><br>")

            Html.WriteLine("<i>Controllers:</i><br>")
            Html.WriteLine("<a href=""i2c"">i2c Devices</a><br>")
            Html.WriteLine("<a href=""gpio"">GPIO Connectors</a><br>")

            Html.WriteLine("</Body>")
            Html.WriteLine("</html>")
            Return Html.ToString
        End Function
    End Class
End Namespace

