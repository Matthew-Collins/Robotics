Imports Windows.Devices.Enumeration
Imports Windows.Devices.I2C

Public Class BerryIMU

    'Dependancy: Must have OzzMaker.com BerryIMU v1.6

    'i2c Revisions
    Const REVISION_PI1 As Integer = &H0
    Const REVISION_PI2_AND_PI3 As Integer = &H1

    'Registers / etc:
    Const MAG_ADDRESS As Integer = &H1E
    Const ACC_ADDRESS As Integer = &H6A '&H1e Switched
    Const GYR_ADDRESS As Integer = &H1E '&H6a

    'LSM9DS0 Gyro Registers  
    Const WHO_AM_I_G As Integer = &HF
    Const CTRL_REG1_G As Integer = &H20
    Const CTRL_REG2_G As Integer = &H21
    Const CTRL_REG3_G As Integer = &H22
    Const CTRL_REG4_G As Integer = &H23
    Const CTRL_REG5_G As Integer = &H24
    Const REFERENCE_G As Integer = &H25
    Const STATUS_REG_G As Integer = &H27
    Const OUT_X_L_G As Integer = &H2A '&H28 Switched
    Const OUT_X_H_G As Integer = &H2B '&H29
    Const OUT_Y_L_G As Integer = &H28 '&H2A
    Const OUT_Y_H_G As Integer = &H29 '&H2B
    Const OUT_Z_L_G As Integer = &H2C
    Const OUT_Z_H_G As Integer = &H2D
    Const FIFO_CTRL_REG_G As Integer = &H2E
    Const FIFO_SRC_REG_G As Integer = &H2F
    Const INT1_CFG_G As Integer = &H30
    Const INT1_SRC_G As Integer = &H31
    Const INT1_THS_XH_G As Integer = &H32
    Const INT1_THS_XL_G As Integer = &H33
    Const INT1_THS_YH_G As Integer = &H34
    Const INT1_THS_YL_G As Integer = &H35
    Const INT1_THS_ZH_G As Integer = &H36
    Const INT1_THS_ZL_G As Integer = &H37
    Const INT1_DURATION_G As Integer = &H38

    'LSM9DS0 Accel And Magneto Registers
    Const OUT_TEMP_L_XM As Integer = &H5
    Const OUT_TEMP_H_XM As Integer = &H6
    Const STATUS_REG_M As Integer = &H7
    Const OUT_X_L_M As Integer = &H8
    Const OUT_X_H_M As Integer = &H9
    Const OUT_Y_L_M As Integer = &HA
    Const OUT_Y_H_M As Integer = &HB
    Const OUT_Z_L_M As Integer = &HC
    Const OUT_Z_H_M As Integer = &HD
    Const WHO_AM_I_XM As Integer = &HF
    Const INT_CTRL_REG_M As Integer = &H12
    Const INT_SRC_REG_M As Integer = &H13
    Const INT_THS_L_M As Integer = &H14
    Const INT_THS_H_M As Integer = &H15
    Const OFFSET_X_L_M As Integer = &H16
    Const OFFSET_X_H_M As Integer = &H17
    Const OFFSET_Y_L_M As Integer = &H18
    Const OFFSET_Y_H_M As Integer = &H19
    Const OFFSET_Z_L_M As Integer = &H1A
    Const OFFSET_Z_H_M As Integer = &H1B
    Const REFERENCE_X As Integer = &H1C
    Const REFERENCE_Y As Integer = &H1D
    Const REFERENCE_Z As Integer = &H1E
    Const CTRL_REG0_XM As Integer = &H1F
    Const CTRL_REG1_XM As Integer = &H20
    Const CTRL_REG2_XM As Integer = &H21
    Const CTRL_REG3_XM As Integer = &H22
    Const CTRL_REG4_XM As Integer = &H23
    Const CTRL_REG5_XM As Integer = &H24
    Const CTRL_REG6_XM As Integer = &H25
    Const CTRL_REG7_XM As Integer = &H26
    Const STATUS_REG_A As Integer = &H27
    Const OUT_X_L_A As Integer = &H28
    Const OUT_X_H_A As Integer = &H29
    Const OUT_Y_L_A As Integer = &H2A
    Const OUT_Y_H_A As Integer = &H2B
    Const OUT_Z_L_A As Integer = &H2C
    Const OUT_Z_H_A As Integer = &H2D
    Const FIFO_CTRL_REG As Integer = &H2E
    Const FIFO_SRC_REG As Integer = &H2F
    Const INT_GEN_1_REG As Integer = &H30
    Const INT_GEN_1_SRC As Integer = &H31
    Const INT_GEN_1_THS As Integer = &H32
    Const INT_GEN_1_DURATION As Integer = &H33
    Const INT_GEN_2_REG As Integer = &H34
    Const INT_GEN_2_SRC As Integer = &H35
    Const INT_GEN_2_THS As Integer = &H36
    Const INT_GEN_2_DURATION As Integer = &H37
    Const CLICK_CFG As Integer = &H38
    Const CLICK_SRC As Integer = &H39
    Const CLICK_THS As Integer = &H3A
    Const TIME_LIMIT As Integer = &H3B
    Const TIME_LATENCY As Integer = &H3C
    Const TIME_WINDOW As Integer = &H3D

    Const RAD_TO_DEG As Decimal = 57.29578D
    Const M_PI As Decimal = 3.1415926535897931D
    Const G_GAIN As Decimal = 0.07D
    Const AA As Decimal = 0.4D

    Public Accelerometer As I2cDevice
    Public Gyro As I2cDevice

    Public Shared Async Function NewAsync() As Task(Of BerryIMU)
        Dim BerryIMU As New BerryIMU
        With BerryIMU

            'Detect Hardware
            Dim Controllers As DeviceInformationCollection = Await DeviceInformation.FindAllAsync(I2cDevice.GetDeviceSelector())

            'Initialise the Accelerometer
            Dim AccelerometerSettings = New I2cConnectionSettings(ACC_ADDRESS)
            AccelerometerSettings.BusSpeed = I2cBusSpeed.FastMode
            .Accelerometer = Await I2cDevice.FromIdAsync(Controllers(0).Id, AccelerometerSettings)

            .Accelerometer.Write({CTRL_REG1_G, &H0, &H0, &H11, &H11}) 'Normal power mode, all axes enabled
            .Accelerometer.Write({CTRL_REG4_G, &H0, &H11, &H0, &H0})  'Continuos update, 2000 dps full scale

            'Initialise the Ayroscope
            Dim GyroSettings = New I2cConnectionSettings(GYR_ADDRESS)
            GyroSettings.BusSpeed = I2cBusSpeed.FastMode
            .Gyro = Await I2cDevice.FromIdAsync(Controllers(0).Id, GyroSettings)

            .Gyro.Write({CTRL_REG1_XM, &H1, &H10, &H1, &H11})  'z, y, x axis enabled, continuos update, 100 Hz data rate
            .Gyro.Write({CTRL_REG2_XM, &H0, &H10, &H0, &H0})   '+ / - 16 G full scale

        End With
        Return BerryIMU
    End Function
    Public Sub GetValues(ByRef GyroX As Double, ByRef GyroY As Double, ByRef GyroZ As Double,
                         ByRef AccelerometerX As Double, ByRef AccelerometerY As Double, ByRef AccelerometerZ As Double,
                         ByRef FilteredX As Double, ByRef FilteredY As Double, ByRef FilteredZ As Double)

        Dim GyroBuffer(5) As Byte
        Me.Gyro.WriteRead({OUT_Y_L_G}, GyroBuffer)
        GyroX = BitConverter.ToInt16(GyroBuffer, 2)
        GyroY = BitConverter.ToInt16(GyroBuffer, 0)
        GyroZ = BitConverter.ToInt16(GyroBuffer, 4)

        Dim AccelerometerBuffer(5) As Byte
        Me.Accelerometer.WriteRead({OUT_X_L_A}, GyroBuffer)
        AccelerometerX = BitConverter.ToInt16(AccelerometerBuffer, 0)
        AccelerometerY = BitConverter.ToInt16(AccelerometerBuffer, 2)
        AccelerometerZ = BitConverter.ToInt16(AccelerometerBuffer, 4)

        'Filter Values
        FilteredX = GyroX
        FilteredY = GyroY
        FilteredZ = GyroZ

    End Sub
End Class
