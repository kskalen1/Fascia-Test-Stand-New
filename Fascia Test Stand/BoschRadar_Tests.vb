Module BoschRadar_Tests
   Public Structure Bosch_RadaSensorTestVariablesStruct
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend Bosch_Radar_Sensor_T0_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Friend Bosch_Radar_Sensor_T1_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Friend Bosch_Radar_Sensor_T2_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Friend Bosch_Radar_Sensor_T3_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Friend Bosch_Radar_Sensor_T4_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Friend Bosch_Radar_Sensor_T5_TestVariables(1) As Bosch_RadaSensorTestVariablesStruct
   Sub LoadAll_Bosch_Radar_Sensor_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)

      'Loads All Variables Required For Testing Bosch_Radar's

      Load_Bosch_Radar_Sensor_T0_Test_Variables(ModelNumber, Bumper, SensorType)

      Load_Bosch_Radar_Sensor_T1_Test_Variables(ModelNumber, Bumper, SensorType)

      Load_Bosch_Radar_Sensor_T2_Test_Variables(ModelNumber, Bumper, SensorType)

      Load_Bosch_Radar_Sensor_T3_Test_Variables(ModelNumber, Bumper, SensorType)

      Load_Bosch_Radar_Sensor_T4_Test_Variables(ModelNumber, Bumper, SensorType)

   End Sub
   Sub Load_Bosch_Radar_Sensor_T0_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T0" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T0_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T0_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T0 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub
   Sub Load_Bosch_Radar_Sensor_T1_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T1" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T1_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T1_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T1 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub

   Sub Load_Bosch_Radar_Sensor_T2_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T2" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T2_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T2_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T2 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub

   Sub Load_Bosch_Radar_Sensor_T3_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T3" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T3_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T3_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T3 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub
   Sub Load_Bosch_Radar_Sensor_T4_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T4" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T4_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T4_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T4 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub
   Sub Load_Bosch_Radar_Sensor_T5_Test_Variables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Variables Required To Test The Bosch_Radar Sensors
      'Variables Are Loaded From TestInformation Database
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_" & "T5" & "_GEN1")

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM GeneralTests WHERE" & " TestName = '" & SectionName & "'"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               TempString = DBRead("TestDescription")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).TestName = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               Bosch_Radar_Sensor_T5_TestVariables(LeftHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")
               Bosch_Radar_Sensor_T5_TestVariables(RightHand).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The T5 TestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try


   End Sub

   Sub Bosch_Radar_Sensor_Tests(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Runs All Tests On The Left And Right Bosch_Radar Sensors

      'Load All Of The Varabiles Required To Perform The Test (From Testinformation Database)
      LoadAll_Bosch_Radar_Sensor_Test_Variables(ModelNumber, Bumper, SensorType)

      'Turn On Relays To Sensors

      Bosch_Radar.Intialize_CAN()    'Initialize The CAN Hardware

      'Sequence Through All Of The Tests

      'Test T0 (Daimler Software Number)
      Dim T0_Left_Test_Result As Boolean
      Dim T0_Right_Test_Result As Boolean

      Bosch_Radar_Test_T0(ModelNumber, Bumper, SensorType, T0_Left_Test_Result, T0_Right_Test_Result)
      If T0_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T0_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      'Test T1 (Daimler Software Date)
      Dim T1_Left_Test_Result As Boolean
      Dim T1_Right_Test_Result As Boolean

      Bosch_Radar_Test_T1(ModelNumber, Bumper, SensorType, T1_Left_Test_Result, T1_Right_Test_Result)
      If T1_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T1_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      delay(100)

      'Test T2 (Daimler Hardware Number)
      Dim T2_Left_Test_Result As Boolean
      Dim T2_Right_Test_Result As Boolean

      Bosch_Radar_Test_T2(ModelNumber, Bumper, SensorType, T2_Left_Test_Result, T2_Right_Test_Result)
      If T2_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T2_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      delay(100)

      'Test T3 Daimler Hardware Date)
      Dim T3_Left_Test_Result As Boolean
      Dim T3_Right_Test_Result As Boolean

      Bosch_Radar_Test_T3(ModelNumber, Bumper, SensorType, T3_Left_Test_Result, T3_Right_Test_Result)
      If T3_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T3_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      delay(100)

      'Test T4 (Running Series Number)
      Dim T4_Left_Test_Result As Boolean
      Dim T4_Right_Test_Result As Boolean

      Bosch_Radar_Test_T4(ModelNumber, Bumper, SensorType, T4_Left_Test_Result, T4_Right_Test_Result)
      If T4_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T4_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      'Test T5 (Clear Error Memory)
      Dim T5_Left_Test_Result As Boolean
      Dim T5_Right_Test_Result As Boolean

      Bosch_Radar_Test_T5(ModelNumber, Bumper, SensorType, T5_Left_Test_Result, T5_Right_Test_Result)
      If T5_Left_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If T5_Right_Test_Result = False Then
         frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Crimson
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If
      If AbortTesting = True Then
         AllTestsPassed = False
         GoTo Shutdown  'Go To The End Of This Routine To Shutdown the test
      End If

      If AllTestsPassed = True Then    'Trap for a test that has already failed 
         If T0_Left_Test_Result = True And T0_Right_Test_Result = True _
         And T1_Left_Test_Result = True And T1_Right_Test_Result = True _
         And T2_Left_Test_Result = True And T2_Right_Test_Result = True _
         And T3_Left_Test_Result = True And T3_Right_Test_Result = True _
         And T4_Left_Test_Result = True And T4_Right_Test_Result = True _
         And T5_Left_Test_Result = True And T5_Right_Test_Result = True _
         Then
            AllTestsPassed = True
            frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.Color.Lime
            frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.Color.Lime
         Else
            AllTestsPassed = False
         End If
      End If
Shutdown:
      'Turn Off Relays To Sensors
      'Release The Hardware
      ReleaseCANInterface()
   End Sub

   Sub Bosch_Radar_Test_T0(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T0 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT0MessageResponse() As Byte = {}
      Dim LeftSideT0MessageResponse_1() As Byte = {}
      Dim RightSideT0MessageResponse() As Byte = {}
      Dim RightSideT0MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T0_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T0 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T0" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T0 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T0_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T0_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT0MessageResponse)
               Case True
                  .ValueMeasured = Chr(LeftSideT0MessageResponse(5)) & Chr(LeftSideT0MessageResponse(6)) & Chr(LeftSideT0MessageResponse(7)) & Chr(LeftSideT0MessageResponse(9)) & Chr(LeftSideT0MessageResponse(10)) & Chr(LeftSideT0MessageResponse(11)) & Chr(LeftSideT0MessageResponse(12)) & Chr(LeftSideT0MessageResponse(13)) & Chr(LeftSideT0MessageResponse(14)) & Chr(LeftSideT0MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case .MinLimit To .MaxLimit
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True

               Case Else
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False
            End Select

         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T0_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T0_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT0MessageResponse)
               Case True
                  .ValueMeasured = Chr(RightSideT0MessageResponse(5)) & Chr(RightSideT0MessageResponse(6)) & Chr(RightSideT0MessageResponse(7)) & Chr(RightSideT0MessageResponse(9)) & Chr(RightSideT0MessageResponse(10)) & Chr(RightSideT0MessageResponse(11)) & Chr(RightSideT0MessageResponse(12)) & Chr(RightSideT0MessageResponse(13)) & Chr(RightSideT0MessageResponse(14)) & Chr(RightSideT0MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case .MinLimit To .MaxLimit
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True

               Case Else
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False
            End Select
         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T0 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub

   Sub Bosch_Radar_Test_T1(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T1 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT1MessageResponse() As Byte = {}
      Dim LeftSideT1MessageResponse_1() As Byte = {}
      Dim RightSideT1MessageResponse() As Byte = {}
      Dim RightSideT1MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T1_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T1 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T1" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T1 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T1_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T1_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT1MessageResponse)
               Case True
                  .ValueMeasured = LeftSideT1MessageResponse(4) & "_" & LeftSideT1MessageResponse(5) & "_" & LeftSideT1MessageResponse(6) & "_" & LeftSideT1MessageResponse(6).ToString
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True
            End Select

         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T1_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T1_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT1MessageResponse)
               Case True
                  .ValueMeasured = RightSideT1MessageResponse(4) & "_" & RightSideT1MessageResponse(5) & "_" & RightSideT1MessageResponse(6) & "_" & RightSideT1MessageResponse(6).ToString
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True
            End Select

         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T1 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub

   Sub Bosch_Radar_Test_T2(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T2 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT2MessageResponse() As Byte = {}
      Dim LeftSideT2MessageResponse_1() As Byte = {}
      Dim RightSideT2MessageResponse() As Byte = {}
      Dim RightSideT2MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T2_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T2 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T2" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T2 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T2_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T2_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT2MessageResponse)
               Case True
                  .ValueMeasured = Chr(LeftSideT2MessageResponse(5)) & Chr(LeftSideT2MessageResponse(6)) & Chr(LeftSideT2MessageResponse(7)) & Chr(LeftSideT2MessageResponse(9)) & Chr(LeftSideT2MessageResponse(10)) & Chr(LeftSideT2MessageResponse(11)) & Chr(LeftSideT2MessageResponse(12)) & Chr(LeftSideT2MessageResponse(13)) & Chr(LeftSideT2MessageResponse(14)) & Chr(LeftSideT2MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case .MinLimit To .MaxLimit
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True

               Case Else
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False
            End Select


         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T2_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T2_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT2MessageResponse)
               Case True
                  .ValueMeasured = Chr(RightSideT2MessageResponse(5)) & Chr(RightSideT2MessageResponse(6)) & Chr(RightSideT2MessageResponse(7)) & Chr(RightSideT2MessageResponse(9)) & Chr(RightSideT2MessageResponse(10)) & Chr(RightSideT2MessageResponse(11)) & Chr(RightSideT2MessageResponse(12)) & Chr(RightSideT2MessageResponse(13)) & Chr(RightSideT2MessageResponse(14)) & Chr(RightSideT2MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case .MinLimit To .MaxLimit
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True

               Case Else
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False
            End Select

         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T2 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub

   Sub Bosch_Radar_Test_T3(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T3 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT3MessageResponse() As Byte = {}
      Dim LeftSideT3MessageResponse_1() As Byte = {}
      Dim RightSideT3MessageResponse() As Byte = {}
      Dim RightSideT3MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T3_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T3 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T3" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T3 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T3_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T3_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT3MessageResponse)
               Case True
                  .ValueMeasured = LeftSideT3MessageResponse(4) & "_" & LeftSideT3MessageResponse(5) & "_" & LeftSideT3MessageResponse(6) & "_" & LeftSideT3MessageResponse(6).ToString
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True
            End Select

         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T3_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T3_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT3MessageResponse)
               Case True
                  .ValueMeasured = RightSideT3MessageResponse(4) & "_" & RightSideT3MessageResponse(5) & "_" & RightSideT3MessageResponse(6) & "_" & RightSideT3MessageResponse(6).ToString
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True
            End Select
         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T3 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub

   Sub Bosch_Radar_Test_T4(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T4 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT4MessageResponse() As Byte = {}
      Dim LeftSideT4MessageResponse_1() As Byte = {}
      Dim RightSideT4MessageResponse() As Byte = {}
      Dim RightSideT4MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T4_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T4 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T4" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T4 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T4_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T4_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT4MessageResponse)
               Case True
                  .ValueMeasured = Chr(LeftSideT4MessageResponse(5)) & Chr(LeftSideT4MessageResponse(6)) & Chr(LeftSideT4MessageResponse(7)) & Chr(LeftSideT4MessageResponse(9)) & Chr(LeftSideT4MessageResponse(10)) & Chr(LeftSideT4MessageResponse(11)) & Chr(LeftSideT4MessageResponse(12)) & Chr(LeftSideT4MessageResponse(13)) & Chr(LeftSideT4MessageResponse(14)) & Chr(LeftSideT4MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True
            End Select
         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T4_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T4_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT4MessageResponse)
               Case True
                  .ValueMeasured = Chr(RightSideT4MessageResponse(5)) & Chr(RightSideT4MessageResponse(6)) & Chr(RightSideT4MessageResponse(7)) & Chr(RightSideT4MessageResponse(9)) & Chr(RightSideT4MessageResponse(10)) & Chr(RightSideT4MessageResponse(11)) & Chr(RightSideT4MessageResponse(12)) & Chr(RightSideT4MessageResponse(13)) & Chr(RightSideT4MessageResponse(14)) & Chr(RightSideT4MessageResponse(15))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True
            End Select
         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T4 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub
   Sub Bosch_Radar_Test_T5(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String, ByRef LeftHandTestResult As Boolean, ByRef RightHandTestResult As Boolean)
      'Runs The T5 Test On Both The Left And Right Bosch_Radar
      Dim MessageIndex As Short = 0
      Dim LeftSideT5MessageResponse() As Byte = {}
      Dim LeftSideT5MessageResponse_1() As Byte = {}
      Dim RightSideT5MessageResponse() As Byte = {}
      Dim RightSideT5MessageResponse_1() As Byte = {}
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim Result As Boolean = False

      'Load Variables For Test From The Test Information .ini File
      BoschRadar_Tests.Load_Bosch_Radar_Sensor_T5_Test_Variables(ModelNumber, Bumper, SensorType)

      Try

         'Find T5 CAN Messages In Array and Set index
         For loops As Short = 0 To UBound(CANmessages)
            If CANmessages(loops).TX_TestIdentifier = "T5" Then
               MessageIndex = loops 'Set The Index Of The CAN Messages
               Exit For
            End If
            If loops = UBound(CANmessages) Then
               Throw New Exception("T5 Message Was Not Found In Struct")
            End If
         Next

         'Test LeftHand Side
         With Bosch_Radar_Sensor_T5_TestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T5_TestVariables(LeftHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Left", LeftSideT5MessageResponse, True)
               Case True
                  '                  .ValueMeasured = Hex(LeftSideT5MessageResponse(0)) & " " & Hex(LeftSideT5MessageResponse(1))
                  .ValueMeasured = "Complete"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  LeftHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  LeftHandTestResult = True
            End Select
         End With

         'Test RightHand Side
         With Bosch_Radar_Sensor_T5_TestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = Bosch_Radar_Sensor_T5_TestVariables(RightHand).TestName & " Test"

            Select Case SendCANMessageAndWaitForResponse(MessageIndex, "Right", RightSideT5MessageResponse, True)
               Case True
                  .ValueMeasured = "Complete"
                  '                  .ValueMeasured = Hex(RightSideT5MessageResponse(0)) & " " & Hex(RightSideT5MessageResponse(1))
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               Case Else
                  .ValueMeasured = "999"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select

            'Test Measured Value Against Limits
            'And Display Results
            Select Case .ValueMeasured
               Case "999"
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  RightHandTestResult = False

               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  RightHandTestResult = True
            End Select
         End With
      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The Bosch_Radar_Test_T5 Routine See Error Log For Details")
         AllTestsPassed = False
         Exit Sub
      End Try

   End Sub
End Module
