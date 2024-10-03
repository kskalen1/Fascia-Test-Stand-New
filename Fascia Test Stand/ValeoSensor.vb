Imports NationalInstruments
Imports NationalInstruments.DAQmx
Imports System
Imports System.IO
Imports System.ComponentModel

Module ValeoSensor
   'Contains The Code To Measure The Valeo Sensors
   Private myTask As Task  'A new task is created when the start button is pressed
   Private reader As AnalogMultiChannelReader
   Private dataColumn As DataColumn()             'Channels of Data
   Private dataTable As DataTable = New DataTable 'Table to Display Data
   Public startEdge As DigitalEdgeStartTriggerEdge = DigitalEdgeStartTriggerEdge.Rising
   Private referenceEdge As DigitalEdgeReferenceTriggerEdge = DigitalEdgeReferenceTriggerEdge.Rising
   Public DataStringWriter As TextWriter
   Private runningAnalogTask As Task

   Public data() As NationalInstruments.AnalogWaveform(Of Double)
   Public AcqComplete As Boolean


   Sub MeasureValeoSensor(SensorNumber As Short)
      'SetUp Acquisition Of Waveform And Trigger Pulse
      Dim ChannelString As String = ""
      Select Case SensorNumber
         Case 1
            ChannelString = "Dev1/ai0"
         Case 2
            ChannelString = "Dev1/ai1"
         Case 3
            ChannelString = "Dev1/ai2"
         Case 4
            ChannelString = "Dev1/ai3"
         Case 5
            ChannelString = "Dev1/ai4"
         Case 6
            ChannelString = "Dev1/ai5"
         Case Else

      End Select
      Try
         Dim Result As Boolean = SetupAcquisition(ChannelString)
         delay(50)
         Select Case SensorNumber
            Case 1
               ValeoSensor.SendPulse(0)
            Case 2
               ValeoSensor.SendPulse(1)
            Case 3
               ValeoSensor.SendPulse(2)
            Case 4
               ValeoSensor.SendPulse(3)
            Case 5
               ValeoSensor.SendPulse(4)
            Case 6
               ValeoSensor.SendPulse(5)
            Case Else

         End Select

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "MeasureValeoSensor Routine : Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Sub



   Public Function FindEcho(ByVal sourceArray As NationalInstruments.AnalogWaveform(Of Double)()) As Boolean
      '
      'Valeo Sensor Info
      'ts = send request (Pulse Sent By Tester)
      'ta = sending and attenuation of echo pulse by sensor
      'te echo delay
      'Distance in centimeters = te in micro seconds * .0174

      Dim currentLineIndex As Integer = 0
      Dim timestamp As NationalInstruments.PrecisionDateTime
      Dim SampleTime As Double = 0
      Dim SampleMeasuredValue As Double = 0
      Dim LowFoundInArrayAt As Integer = 0
      Dim HighFoundInArrayAt As Integer = 0

      ''Debug Use 
      'DataStringWriter = File.AppendText("C:\SensorValues.csv")

      'Clear Previous Sensor Info
      ValeoSensorData = Nothing
      If sourceArray Is Nothing Then Return False

      'At The Time Of Writing We Are Only Setting Up 1 Channel At A Time
      For Each waveform As NationalInstruments.AnalogWaveform(Of Double) In sourceArray
         Dim dataCount As Integer = 0
         'Find ts Start
         For sample As Integer = 0 To (waveform.Samples.Count - 1)
            SampleMeasuredValue = waveform.Samples(sample).Value
            timestamp = waveform.Samples(sample).PrecisionTimeStamp
            SampleTime = timestamp.FractionalSeconds()
            ''Debug Use
            'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString)

            If SampleMeasuredValue <= 2.0 Then
               'If a low value is found then
               ValeoSensorData.tsStartTime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               LowFoundInArrayAt = sample
               ''Debug Use
               'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString & "," & "ts Start")
               Exit For
            End If
         Next
         If ValeoSensorData.tsStartTime = Nothing Then Return False 'If A Low Was Not Seen Then Return Echo Not Found

         'Find ts End
         'Start Looking at the point the low was found
         For sample As Integer = LowFoundInArrayAt To (waveform.Samples.Count - 1)
            SampleMeasuredValue = waveform.Samples(sample).Value
            timestamp = waveform.Samples(sample).PrecisionTimeStamp
            SampleTime = timestamp.FractionalSeconds()
            ''Debug Use
            'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString)

            If SampleMeasuredValue >= 4.25 Then
               'If a High value is found then
               ValeoSensorData.tsEndTime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               HighFoundInArrayAt = sample
               ''Debug Use
               'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString & "," & "ts End")
               Exit For
            End If
         Next
         If ValeoSensorData.tsEndTime = Nothing Then Return False 'If A High Was Not Seen Then Return Echo Not Found

         'Check To Make Sure ts pulse found is within the Acceptable Range For ts (in case pulse found is the wrong pulse)
         Select Case (ValeoSensorData.tsEndTime - ValeoSensorData.tsStartTime) * 1000 '(Convert Time Into MicroSeconds)
            Case EchoLimits.tsTime_Min To EchoLimits.tsTime_Max
               'If Value Is In Range for ts times then continue on else
               ValeoSensorData.tsFound = True
            Case Else
               'If Value Is Out Of The Acceptable ts limits then return No Echo Found
               Return False
         End Select

         'Find ta and te Start
         For sample As Integer = HighFoundInArrayAt To (waveform.Samples.Count - 1)
            SampleMeasuredValue = waveform.Samples(sample).Value
            timestamp = waveform.Samples(sample).PrecisionTimeStamp
            SampleTime = timestamp.FractionalSeconds()

            If SampleMeasuredValue <= 2.0 Then
               'If a low value is found then
               'ta start and te start are the same moment in time
               ValeoSensorData.taStartTime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               ValeoSensorData.teStartTime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               LowFoundInArrayAt = sample
               ''Debug Use
               'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString)
               Exit For
            End If
         Next
         If ValeoSensorData.taStartTime = Nothing Then Return False 'If A Low Was Not Seen Then Return Echo Not Found

         'Find ta End
         'Start Looking at the point the low was found
         For sample As Integer = LowFoundInArrayAt To (waveform.Samples.Count - 1)
            SampleMeasuredValue = waveform.Samples(sample).Value
            timestamp = waveform.Samples(sample).PrecisionTimeStamp
            SampleTime = timestamp.FractionalSeconds()
            ''Debug Use
            'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString)

            If SampleMeasuredValue >= 4.25 Then
               'If a High value is found then
               ValeoSensorData.taEndTime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               HighFoundInArrayAt = sample
               ''Debug Use
               'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString & "," & "ta End")
               Exit For
            End If
         Next
         If ValeoSensorData.taEndTime = Nothing Then Return False 'If A High Was Not Seen Then Return Echo Not Found

         'Check To Make Sure ta pulse found is within the Acceptable Range For ts (in case pulse found is the wrong pulse)
         Select Case (ValeoSensorData.taEndTime - ValeoSensorData.taStartTime) * 1000 '(Convert Time Into MicroSeconds)
            Case EchoLimits.taTime_Min To EchoLimits.taTime_Max
               'If Value Is In Range for ts times then continue on else
               ValeoSensorData.taFound = True
            Case Else
               'If Value Is Out Of The Acceptable ts limits then return No Echo Found
               ValeoSensorData.taFound = False
               Return False
         End Select

         'Find te End
         For sample As Integer = HighFoundInArrayAt To (waveform.Samples.Count - 1)
            SampleMeasuredValue = waveform.Samples(sample).Value
            timestamp = waveform.Samples(sample).PrecisionTimeStamp
            SampleTime = timestamp.FractionalSeconds()
            ''Debug Use
            'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString)

            If SampleMeasuredValue <= 2.0 Then
               'If a low value is found then
               ValeoSensorData.teEndtime = SampleTime * 1000 '(Convert Time Into MicroSeconds)
               ValeoSensorData.tefound = True
               LowFoundInArrayAt = sample '
               Debug.Print(sample.ToString)
               ''Debug Use
               'DataStringWriter.WriteLine(SampleTime.ToString & "," & SampleMeasuredValue.ToString & "," & "te End")
               'DataStringWriter.WriteLine(ValeoSensorData.teStartTime.ToString & "," & SampleMeasuredValue.ToString & "," & "te Start Time")
               Exit For
            End If
         Next
         currentLineIndex += 1
      Next
      If ValeoSensorData.teEndtime = Nothing Then Return False 'If A Low Was Not Seen Then Return Echo Not Found
      ''Debug use
      'DataStringWriter.Flush()
      'DataStringWriter.Close()

      'If Code Reaches Here Echo Should Have Been Found
      ValeoSensorData.DistanceIncm = FormatNumber(((ValeoSensorData.teEndtime - ValeoSensorData.teStartTime) * 1000) * 0.0174, 0)  'Distance = te[us] X 0.0174[cm/us]
      ValeoSensorData.tsTime = (ValeoSensorData.tsEndTime - ValeoSensorData.tsStartTime) * 1000
      ValeoSensorData.taTime = (ValeoSensorData.taEndTime - ValeoSensorData.taStartTime) * 1000
      ValeoSensorData.teTime = (ValeoSensorData.teEndtime - ValeoSensorData.teStartTime) * 1000

      Return True


   End Function


   Function SetupAcquisition(ChanelToSetup As String) As Boolean

      'Sets Up The NI Device To Wait For A Triger Pulse

      Try
         ' Create a new task
         myTask = New Task()

         ' Initialize local variables
         Dim sampleRate As Double = 48000
         Dim rangeMin As Double = 0
         Dim rangeMax As Double = 5
         Dim samplesPerChan As Int32 = 500

         startEdge = DigitalEdgeStartTriggerEdge.Rising

         ' Create a channel
         myTask.AIChannels.CreateVoltageChannel(ChanelToSetup, "", AITerminalConfiguration.Rse, rangeMin, rangeMax, AIVoltageUnits.Volts)

         ' Configure timing specs 
         myTask.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChan)

         myTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/PFI0", startEdge)
         myTask.Triggers.ReferenceTrigger.ConfigureNone()

         ' Verify the task
         myTask.Control(TaskAction.Verify)
         reader = New AnalogMultiChannelReader(myTask.Stream)

         '' Use SynchronizeCallbacks to specify that the object 
         '' marshals callbacks across threads appropriately.
         reader.SynchronizeCallbacks = True
         AcqComplete = False
         If runningAnalogTask Is Nothing Then
            ' Change state
            runningAnalogTask = myTask
         End If
         reader.BeginReadWaveform(samplesPerChan, AddressOf myCallback, myTask)
         Return True

      Catch ex As DaqException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Error Seting Up The Acquisition (Valeo Sensor Measurement) : Exception Was Thrown See Error Log For Details")
         AcqComplete = True
         Return False
      End Try


   End Function

   Private Sub myCallback(ByVal ar As IAsyncResult)
      Try
         If (Not (runningAnalogTask Is Nothing)) AndAlso ar.AsyncState.Equals(runningAnalogTask) Then
            data = reader.EndReadWaveform(ar)
            AcqComplete = True
         End If
      Catch ex As DaqException

         'Ignore Exception Here We Don't Want It To Stop The Program If No Echo Is Received
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Error in mycallback(Valeo Sensor Measurement) : Exception Was Thrown See Error Log For Details")
         AcqComplete = True
      Finally
         runningAnalogTask = Nothing
         myTask.Dispose()
      End Try

   End Sub
   Public Sub SendPulse(ChannelToSendPulseOn As Short)
      'Sends A Pulse To The Micro Based On Channel Sent In

      Try
         'Write It High
         Application.DoEvents()
         Call WriteDigPort("Dev1", 0, ChannelToSendPulseOn, True)
         Application.DoEvents()
         'Write It Low
         Call WriteDigPort("Dev1", 0, ChannelToSendPulseOn, False)
         Application.DoEvents()
      Catch ex As DaqException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, " Sending Pulse : Exception Was Thrown See Error Log For Details")
      End Try

   End Sub
   Sub WriteDigPort(ByVal Device As String, ByVal PortNumber As Short, ByVal Channel As Short, ByVal OnOrOFf As Boolean)
      Dim digitalWriteTask As Task = New Task()
      Dim AddressString As String
      Dim Data2Write As UInt32

      Select OnOrOFf
         Case True
            Select Case Channel
               Case 0
                  Data2Write = 1
               Case 1
                  Data2Write = 2
               Case 2
                  Data2Write = 4
               Case 3
                  Data2Write = 8
               Case 4
                  Data2Write = 16
               Case 5
                  Data2Write = 32
               Case 6
                  Data2Write = 64
               Case 7
                  Data2Write = 128
               Case Else

            End Select
         Case False
            Data2Write = 0

      End Select

      AddressString = Device & "/port" & PortNumber

      Try
         '  Create an Digital Output channel and name it.
         digitalWriteTask.DOChannels.CreateChannel(AddressString, "port0", ChannelLineGrouping.OneChannelForAllLines)

         '  Write digital port data. WriteDigitalSingChanSingSampPort writes a single sample
         '  of digital data on demand, so no timeout is necessary.
         Dim writer As DigitalSingleChannelWriter = New DigitalSingleChannelWriter(digitalWriteTask.Stream)
         writer.WriteSingleSamplePort(True, Data2Write)
      Catch ex As DaqException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, " Writing To Digital Port Exception Was Thrown See Error Log For Details")
         If digitalWriteTask IsNot Nothing Then digitalWriteTask.Dispose()
      Finally
         digitalWriteTask.Dispose()
      End Try

   End Sub

   Sub MeasureValeoSensors()
      'Measures All Of The Valeo Sensors

      'SetFlag So timer does not call repeadely
      SensorMeasurementInProgress = True
      'Clear Manual Mode Screen Of Any Previous Measurement Results
      ' ClearPreviousValeoSensorData()

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor One
         Call MeasureValeoSensor(1)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True

         ' Parse Through Array And Find Ehco Value
         Dim Result As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(0) = ValeoSensorData

         Select Case Result
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_1_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_1_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_1_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_1_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_1_ts.Text = ""
               frmMain.lblValeo_Sensor_1_ta.Text = ""
               frmMain.lblValeo_Sensor_1_te.Text = ""
               frmMain.lblValeo_Sensor_1_Distance.Text = "No Echo Found"
               EchoData(0).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(0).DistanceIncm > 20 And EchoData(0).DistanceIncm < 130 Then Exit For
      Next

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor Two
         Call MeasureValeoSensor(2)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True


         ' Parse Through Array And Find Ehco Value
         Dim Result1 As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(1) = ValeoSensorData

         Select Case Result1
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_2_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_2_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_2_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_2_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_2_ts.Text = ""
               frmMain.lblValeo_Sensor_2_ta.Text = ""
               frmMain.lblValeo_Sensor_2_te.Text = ""
               frmMain.lblValeo_Sensor_2_Distance.Text = "No Echo Found"
               EchoData(1).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(1).DistanceIncm > 20 And EchoData(1).DistanceIncm < 130 Then Exit For
      Next

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor Three
         Call MeasureValeoSensor(3)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True

         ' Parse Through Array And Find Ehco Value
         Dim Result2 As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(2) = ValeoSensorData

         Select Case Result2
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_3_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_3_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_3_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_3_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_3_ts.Text = ""
               frmMain.lblValeo_Sensor_3_ta.Text = ""
               frmMain.lblValeo_Sensor_3_te.Text = ""
               frmMain.lblValeo_Sensor_3_Distance.Text = "No Echo Found"
               EchoData(2).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(2).DistanceIncm > 20 And EchoData(2).DistanceIncm < 130 Then Exit For
      Next

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor Four
         Call MeasureValeoSensor(4)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True

         ' Parse Through Array And Find Ehco Value
         Dim Result4 As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(3) = ValeoSensorData

         Select Case Result4
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_4_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_4_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_4_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_4_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_4_ts.Text = ""
               frmMain.lblValeo_Sensor_4_ta.Text = ""
               frmMain.lblValeo_Sensor_4_te.Text = ""
               frmMain.lblValeo_Sensor_4_Distance.Text = "No Echo Found"
               EchoData(3).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(3).DistanceIncm > 20 And EchoData(3).DistanceIncm < 130 Then Exit For
      Next

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor Five
         Call MeasureValeoSensor(5)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True

         ' Parse Through Array And Find Ehco Value
         Dim Result5 As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(4) = ValeoSensorData

         Select Case Result5
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_5_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_5_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_5_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_5_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_5_ts.Text = ""
               frmMain.lblValeo_Sensor_5_ta.Text = ""
               frmMain.lblValeo_Sensor_5_te.Text = ""
               frmMain.lblValeo_Sensor_5_Distance.Text = "No Echo Found"
               EchoData(4).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(4).DistanceIncm > 20 And EchoData(4).DistanceIncm < 130 Then Exit For
      Next

      For NumberOfRetries As Short = 1 To 10
         'Measure Sensor Six
         Call MeasureValeoSensor(6)

         Do
            Application.DoEvents()
            If AbortTesting = True Then Exit Sub
         Loop Until AcqComplete = True

         ' Parse Through Array And Find Ehco Value
         Dim Result6 As Boolean = ValeoSensor.FindEcho(data)
         'Copy Info Into Echo Structure For Use In Main Test Routine
         EchoData(5) = ValeoSensorData


         Select Case Result6
            Case True
               'Echo Found
               frmMain.lblValeo_Sensor_6_Distance.Text = ValeoSensorData.DistanceIncm.ToString & "cm"
               frmMain.lblValeo_Sensor_6_ts.Text = Format(ValeoSensorData.tsTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_6_ta.Text = Format(ValeoSensorData.taTime.ToString, "Fixed") & "µS"
               frmMain.lblValeo_Sensor_6_te.Text = Format(ValeoSensorData.teTime.ToString, "Fixed") & "µS"
            Case False
               'Echo Not Found
               frmMain.lblValeo_Sensor_6_ts.Text = ""
               frmMain.lblValeo_Sensor_6_ta.Text = ""
               frmMain.lblValeo_Sensor_6_te.Text = ""
               frmMain.lblValeo_Sensor_6_Distance.Text = "No Echo Found"
               EchoData(5).DistanceIncm = 0
         End Select
         'IF Measurement Is In Range Then Don't Retry Any More
         If EchoData(5).DistanceIncm > 20 And EchoData(5).DistanceIncm < 130 Then Exit For
      Next

      SensorMeasurementInProgress = False

   End Sub

   Public Sub ClearPreviousValeoSensorData()
      'Clears The Previos Sensor Measurements On the Manual Mode Screen

      frmMain.lblValeo_Sensor_1_Distance.Text = ""
      frmMain.lblValeo_Sensor_1_ts.Text = ""
      frmMain.lblValeo_Sensor_1_ta.Text = ""
      frmMain.lblValeo_Sensor_1_te.Text = ""

      frmMain.lblValeo_Sensor_2_Distance.Text = ""
      frmMain.lblValeo_Sensor_2_ts.Text = ""
      frmMain.lblValeo_Sensor_2_ta.Text = ""
      frmMain.lblValeo_Sensor_2_te.Text = ""

      frmMain.lblValeo_Sensor_3_Distance.Text = ""
      frmMain.lblValeo_Sensor_3_ts.Text = ""
      frmMain.lblValeo_Sensor_3_ta.Text = ""
      frmMain.lblValeo_Sensor_3_te.Text = ""

      frmMain.lblValeo_Sensor_4_Distance.Text = ""
      frmMain.lblValeo_Sensor_4_ts.Text = ""
      frmMain.lblValeo_Sensor_4_ta.Text = ""
      frmMain.lblValeo_Sensor_4_te.Text = ""

      frmMain.lblValeo_Sensor_5_Distance.Text = ""
      frmMain.lblValeo_Sensor_5_ts.Text = ""
      frmMain.lblValeo_Sensor_5_ta.Text = ""
      frmMain.lblValeo_Sensor_5_te.Text = ""

      frmMain.lblValeo_Sensor_6_Distance.Text = ""
      frmMain.lblValeo_Sensor_6_ts.Text = ""
      frmMain.lblValeo_Sensor_6_ta.Text = ""
      frmMain.lblValeo_Sensor_6_te.Text = ""

   End Sub
End Module
