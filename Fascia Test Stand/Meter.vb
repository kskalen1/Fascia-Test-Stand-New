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
            delay(250)
            'If Port Is Closed Open It.
            Try
               With spMeter
                  .PortName = MetercomPort
                  .BaudRate = 19200
                  .Parity = IO.Ports.Parity.None
                  .DataBits = 8
                  .StopBits = IO.Ports.StopBits.One
                  .DiscardNull = False
                  .ReadTimeout = 2000
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
                  .BaudRate = 19200
                  .Parity = IO.Ports.Parity.None
                  .DataBits = 8
                  .StopBits = IO.Ports.StopBits.One
                  .DiscardNull = False
                  .ReadTimeout = 2000
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

   Public Function MeterCommunication(ByVal MeterCommand As String, ByVal NumberOfReturnLines As Short) As String  'Communicates With Meter Via COM Port
      Dim ReturnString As String = ""

      MeterCommunication = ""

      'Clear Out Buffer before write & read

      spMeter.DiscardInBuffer()

      'Write Commands To The Meter Via Com Port
      spMeter.NewLine = vbCr
      delay(20)
      Try
         spMeter.Write(MeterCommand & vbCr)  'Send Out Command
         delay(200)
         'Allow time for Meter to Respond
         For Loops As Short = 1 To NumberOfReturnLines
            ReturnString = spMeter.ReadLine
            MeterCommunication = ReturnString
            Threading.Thread.Sleep(50)
         Next Loops
      Catch ex As Exception
         MsgBox(ex.ToString)
         MeterCommunication = "ERROR"
         Exit Function
      End Try

   End Function

   Public Function Meter_IDN() As String
      'Function Sends The Identification (IDN?) Command to the Meter and waits for a response
      Meter_IDN = Meter.MeterCommunication("*IDN?", 2)
   End Function

    Public Function Meter_RST() As String
      'Function Sends The Reset (RST) Command to the Meter and waits for a response
      Meter_RST = Meter.MeterCommunication("*RST", 1)
   End Function

    Public Function Meter_ADC() As String
      'Function Sends Amps DC (ADC) Command to the Meter and waits for a response
      Meter_ADC = Meter.MeterCommunication("FUNC CURRent:DC", 1)
      Meter.MeterCommunication("CURRent:DC:RANGe:UPPer 20", 1)

   End Function

   Public Function Meter_OHMS() As String
      'Function Sends The Resistance (OHMS) Command to the Meter and waits for a response
      Meter_OHMS = Meter.MeterCommunication("FUNC RESistance", 1)
   End Function

   Public Function Meter_VAC() As String
      'Function Sends The Voltage AC (VAC) Command to the Meter and waits for a response
      Meter_VAC = Meter.MeterCommunication("FUNC VOLT:AC", 1)
   End Function

    Public Function Meter_VDC() As String
      'Function Sends The Voltage DC (VDC) Command to the Meter and waits for a response
      Meter_VDC = Meter.MeterCommunication("FUNC VOLT:DC", 1)
   End Function

   Public Function Meter_TakeMeasurement() As String
      'Function Sends The MEAS1? Command to the Meter and waits for a response
      Meter_TakeMeasurement = Meter.MeterCommunication("FETC?", 2)
   End Function

End Module
