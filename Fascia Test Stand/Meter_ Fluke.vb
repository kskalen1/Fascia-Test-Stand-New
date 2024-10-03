Imports System
Imports System.IO
Public Enum MeterRates
    Fast = 1
    Medium = 2
    Slow = 3
End Enum
Public Enum MeterFormats
    Measurement_With_Units
    Measurement_WithOut_Units
End Enum
Module Meter
    Dim WithEvents spMeter As New IO.Ports.SerialPort
 
    Sub MeterSerialPortOpen()
        'Sets The Communication Port Parameters & Opens the Serial Port To The Meter

      Select Case spMeter.IsOpen
         Case Is = True
            'If Port Is Open Close then
            spMeter.Close()
            'If Port Is Closed Open It.
            Try
               With spMeter
                  .PortName = MetercomPort
                  .BaudRate = 9600
                  .Parity = IO.Ports.Parity.None
                  .DataBits = 8
                  .StopBits = IO.Ports.StopBits.One
                  .DiscardNull = False
                  .ReadTimeout = 1000
               End With
               spMeter.Open()
               frmMain.lblMetercomPort.Text = MetercomPort
               frmMain.indMeterComPortOpen.BackColor = Color.Green
               'Send Remote Lockout Command To Meter (Locks Out Front Panel Control)
            Catch ex As Exception
               MsgBox(ex.ToString)
               frmMain.indMeterComPortOpen.BackColor = Color.Red
               Exit Sub
            End Try

         Case Is = False
            'If Port Is Closed Open It.
            Try
               With spMeter
                  .PortName = MetercomPort
                  .BaudRate = 9600
                  .Parity = IO.Ports.Parity.None
                  .DataBits = 8
                  .StopBits = IO.Ports.StopBits.One
                  .DiscardNull = False
                  .ReadTimeout = 1000
               End With
               spMeter.Open()
               frmMain.lblMetercomPort.Text = MetercomPort
               frmMain.indMeterComPortOpen.BackColor = Color.Green
               'Send Remote Lockout Command To Meter (Locks Out Front Panel Control)
            Catch ex As Exception
               MsgBox(ex.ToString)
               frmMain.indMeterComPortOpen.BackColor = Color.Red
               Exit Sub
            End Try
      End Select

    End Sub

    Public Function MeterCommunication(ByVal MeterCommand As String) As String  'Communicates With Meter Via COM Port
        Dim ReturnString As String = ""

        MeterCommunication = ""

        'Clear Out Buffer before write & read

        spMeter.DiscardInBuffer()

      'Write Commands To The Meter Via Com Port
      spMeter.NewLine = vbCr
      Try
         spMeter.Write(MeterCommand & vbCr)  'Send Out Command
         delay(50)
            'Allow time for Meter to Respond
            ReturnString = spMeter.ReadLine
            Select Case ReturnString
                Case Is = "=>"
                    MeterCommunication = "Command Received And Complete"
                Case Else
                    MeterCommunication = ReturnString
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
            MeterCommunication = "ERROR"
            Exit Function
        End Try

    End Function

    Public Function Meter_IDN() As String
        'Function Sends The Identification (IDN?) Command to the Meter and waits for a response
        Meter_IDN = Meter.MeterCommunication("*IDN?")
    End Function

    Public Function Meter_RST() As String
        'Function Sends The Reset (RST) Command to the Meter and waits for a response
        Meter_RST = Meter.MeterCommunication("*RST")
    End Function

    Public Function Meter_ADC() As String
        'Function Sends Amps DC (ADC) Command to the Meter and waits for a response
        Meter_ADC = Meter.MeterCommunication("ADC")
    End Function

    Public Function Meter_CONT() As String
        'Function Sends The Continuity (CONT) Command to the Meter and waits for a response
        Meter_CONT = Meter.MeterCommunication("CONT")
    End Function

    Public Function Meter_DIODE() As String
        'Function Sends The Diode (DIODE) Command to the Meter and waits for a response
        Meter_DIODE = Meter.MeterCommunication("DIODE")
    End Function

    Public Function Meter_FREQ() As String
        'Function Sends The Frequency (FREQ) Command to the Meter and waits for a response
        Meter_FREQ = Meter.MeterCommunication("FREQ")
    End Function

    Public Function Meter_OHMS() As String
        'Function Sends The Resistance (OHMS) Command to the Meter and waits for a response
        Meter_OHMS = Meter.MeterCommunication("OHMS")
    End Function

    Public Function Meter_VAC() As String
        'Function Sends The Voltage AC (VAC) Command to the Meter and waits for a response
        Meter_VAC = Meter.MeterCommunication("VAC")
    End Function

    Public Function Meter_VDC() As String
        'Function Sends The Voltage DC (VDC) Command to the Meter and waits for a response
        Meter_VDC = Meter.MeterCommunication("VDC")
    End Function

    Public Function Meter_Range(ByVal Range As Integer) As String
        'Function Sends The Range Command with the passed Range to the Meter and waits for a response
        'If Range Is Set To Auto Then Send the Auto Command Else Send The Range Command
        Select Case Range
            Case 0
                Meter_Range = Meter.MeterCommunication("AUTO")
            Case Else
                Meter_Range = Meter.MeterCommunication("RANGE " & Range.ToString)
        End Select
    End Function

    Public Function Meter_RATE(ByVal RATE As MeterRates) As String
        'Function Sends The Rate Command with the passed Rate to the Meter and waits for a response
        Select Case RATE
            Case Is = MeterRates.Fast
                Meter_RATE = Meter.MeterCommunication("RATE F")
            Case Is = MeterRates.Medium
                Meter_RATE = Meter.MeterCommunication("RATE M")
            Case Is = MeterRates.Slow
                Meter_RATE = Meter.MeterCommunication("RATE S")
            Case Else
                Meter_RATE = "WRONG RATE"
        End Select

    End Function

    Public Function Meter_REMOTE_LOCKOUT() As String
        'Function Sends The REMOTE Command With Front Panel Lockout (RWLS) Command to the Meter and waits for a response
        Meter_REMOTE_LOCKOUT = Meter.MeterCommunication("RWLS")
    End Function

    Public Function Meter_MeasurementFormat(ByVal MeterFormat As MeterFormats) As String
        'Function Sends The Rate Command with the passed Rate to the Meter and waits for a response
        Select Case MeterFormat
            Case Is = MeterFormats.Measurement_With_Units
                Meter_MeasurementFormat = Meter.MeterCommunication("FORMAT 2")
            Case Is = MeterFormats.Measurement_WithOut_Units
                Meter_MeasurementFormat = Meter.MeterCommunication("FORMAT 1")
            Case Else
                Meter_MeasurementFormat = "WRONG RATE"
        End Select

    End Function
    Public Function Meter_TakeMeasurement() As String
        'Function Sends The MEAS1? Command to the Meter and waits for a response
        Meter_TakeMeasurement = Meter.MeterCommunication("MEAS1?")
    End Function

End Module
