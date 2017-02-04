' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Picon As PiconZero
    Private Sonic As Ultrasonic
    Private WithEvents Timer As New DispatcherTimer With {.Interval = New TimeSpan(0, 0, 3)}

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Async Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Picon = Await PiconZero.NewAsync
        Me.Sonic = New Ultrasonic()
        'Me.Timer.Start()
    End Sub
    Private Sub Channels_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles Channel0.ValueChanged, Channel1.ValueChanged, Channel2.ValueChanged, Channel3.ValueChanged, Channel4.ValueChanged, Channel5.ValueChanged
        With DirectCast(sender, Slider)
            Me.Picon?.SetServo(CByte(.Name.Substring(.Name.Length - 1)), CInt(.Value), CInt(.Maximum), CInt(.Minimum))
        End With
    End Sub
    Private Sub CenterAll_Click(sender As Object, e As RoutedEventArgs) Handles CenterAll.Click
        Me.Channel0.Value = 0
        Me.Channel1.Value = 0
        Me.Channel2.Value = 0
        Me.Channel3.Value = 0
        Me.Channel4.Value = 0
        Me.Channel5.Value = 0
    End Sub
    Private Sub Timer_Tick(sender As Object, e As Object) Handles Timer.Tick
        Me.Distance.Text = $"Distance: {Me.Sonic.GetDistance} at {DateTime.Now:HH:mm:ss}"
    End Sub

End Class
