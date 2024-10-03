Imports System
Imports System.IO

Module Tests

   Sub LoadPTSSensorTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The PTSSensor Test Variables For The Passed Model Number, Bumper, And PTS Type

      Dim TempString As String = ""
      Dim SectionName As String = ""

      'Change Section Name Based On Generation Of Sensor
      If frmMain.rbPTS_GEN_1.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN1")
      End If

      If frmMain.rbPTS_GEN_2.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN2")
      End If

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
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).TestName = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("MinLimit").ToString
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).MinLimit = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("MaxLimit")
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).MaxLimit = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("TestUnit")
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).TestUnit = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("MeasurementType")
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).MeasurementType = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("DelayBeforeMeasurement")
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).DelayBeforeMeasurement = SplitString(TempString, ",")
               Next Loops

               TempString = DBRead("NumberOfSensors")
               For Loops As Integer = 0 To 5
                  PTSTestVariables(Loops).NumberOfSensors = SplitString(TempString, ",")
               Next Loops

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")
      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub
    Sub PTSSensorTests(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
        'Runs The PTS Tests On The Passed Model Number, Bumper, And Sensor Type

        Dim Result As Boolean = False

        Dim LoopTimer As DateTime = Now
        Dim ReturnedBytes As Byte() = Nothing
        Dim ReturnString As String = ""
        Dim StartSensorNumber As Integer = 0
        Dim StopSensorNumber As Integer = 0

        'Load Variables For Test From The Test Information .ini File
        Tests.LoadPTSSensorTestVariables(ModelNumber, Bumper, SensorType)
        If AbortTesting = True Then Exit Sub

        If PTSTestVariables(0).TestName = "" Then
            AllTestsPassed = False
            AbortTesting = True
            MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
            frmMain.lblTestResult.BackColor = Color.Crimson
            Exit Sub
        End If

        'Show First Test Place In Grid So User Knows What Test Is Pending
        With PTSTestVariables(0)
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "TESTING"
            frmMain.lblTestResult.Text = PTSTestVariables(0).TestName & " Test"
        End With

        ''Call The Routine To Measure All Of The Valeo Sensors
        'ValeoSensor.MeasureValeoSensors()

        TBOX.LoadTBOXCommunicationProtocall()
        Result = TBOX.WriteBumperSetup(ReturnString)
        Select Case Result
            Case True

            Case Else

                Throw New Exception("Could Not Write Bumper Setup")

        End Select

        'Call The Routine To Measure All Bosch Sensors
        Result = DirectEchoesRequest(ReturnString)


        'Test Sensors
        'TestResultString = TestResultString & SensorType & ","
        Dim NumberOfSensors As Integer = 0
        For NumberOfSensors = 0 To Val(PTSTestVariables(0).NumberOfSensors - 1)
            With PTSTestVariables(NumberOfSensors)
                If NumberOfSensors <> 0 Then
                    'Do Not Add A Row Here For The First Sensor It Was Added Above
                    'Display Test Perameters In Grid
                    frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
                    frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
                    frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
                    frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
                End If

                Select Case Val(PTSTestVariables(0).NumberOfSensors)
                    Case 6
                        Select Case NumberOfSensors
                            Case Is = 0
                                If Sensor1TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_1
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 1
                                If Sensor2TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_2
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 2
                                If Sensor3TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_3
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 3
                                If Sensor4TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_4
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 4
                                If Sensor5TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_5
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 5
                                If Sensor6TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_6
                                Else
                                    .ValueMeasured = "9999"
                                End If
                        End Select
                    Case 4
                        Select Case NumberOfSensors
                            Case Is = 0
                                If Sensor1TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_1
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 1
                                If Sensor2TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_2
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 2
                                If Sensor3TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_3
                                Else
                                    .ValueMeasured = "9999"
                                End If

                            Case Is = 3
                                If Sensor4TestInabled = True Then
                                    .ValueMeasured = TBOXEchoResponses.Sensor_4
                                Else
                                    .ValueMeasured = "9999"
                                End If
                        End Select

                    Case Else
                        MsgBox("Wrong Number Of Sensors Selected", MsgBoxStyle.Exclamation)
                        AbortTesting = True
                        Exit Sub
                End Select

                'Test Measured Value Against Limits
                'And Display Results
                Select Case .ValueMeasured
                    Case Is = 0
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "No Echo"
                    Case Is = 9999
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "TEST BYPASSED"
                    Case Else
                        Debug.Print(.ValueMeasured)
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured & " " & .TestUnit
                        Debug.Print(frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value)
                End Select

                Select Case Val(.ValueMeasured)
                    Case 0 To 248
                        Select Case Val(.ValueMeasured)
                            Case Is > Val(.MaxLimit)
                                .Result = "F"
                                AllTestsPassed = False
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                                .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                                FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                                Select Case Val(PTSTestVariables(0).NumberOfSensors)
                                    Case 6
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH_OB.BackColor = Color.Crimson
                                                frmMain.osPTS_Front_LH_OB.BackColor = Color.Crimson
                                            Case 1
                                                frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                                frmMain.osPTS_LH.BackColor = Color.Crimson

                                            Case 2
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                            Case 3
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                            Case 4
                                                frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                                frmMain.osPTS_RH.BackColor = Color.Crimson

                                            Case 5
                                                frmMain.lbl_PTS_RH_OB.BackColor = Color.Crimson
                                                frmMain.osPTS_Front_RH_OB.BackColor = Color.Crimson
                                        End Select
                                    Case 4
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                                frmMain.osPTS_LH.BackColor = Color.Crimson
                                            Case 1
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                            Case 2
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                            Case 3
                                                frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                                frmMain.osPTS_RH.BackColor = Color.Crimson
                                        End Select
                                    Case Else
                                        'Nothing
                                End Select

                            Case Is < Val(.MinLimit)
                                .Result = "F"
                                AllTestsPassed = False
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                                .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured

                                FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","

                                Select Case Val(PTSTestVariables(0).NumberOfSensors)
                                    Case 6
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH_OB.BackColor = Color.Crimson
                                                frmMain.osPTS_Front_LH_OB.BackColor = Color.Crimson
                                            Case 1
                                                frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                                frmMain.osPTS_LH.BackColor = Color.Crimson

                                            Case 2
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                            Case 3
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                            Case 4
                                                frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                                frmMain.osPTS_RH.BackColor = Color.Crimson

                                            Case 5
                                                frmMain.lbl_PTS_RH_OB.BackColor = Color.Crimson
                                                frmMain.osPTS_Front_RH_OB.BackColor = Color.Crimson
                                        End Select
                                    Case 4
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                                frmMain.osPTS_LH.BackColor = Color.Crimson
                                            Case 1
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                            Case 2
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                            Case 3
                                                frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                                frmMain.osPTS_RH.BackColor = Color.Crimson
                                        End Select
                                    Case Else
                                        'Nothing
                                End Select

                            Case Else
                                .Result = "P"
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                                frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                                .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured

                                Select Case Val(PTSTestVariables(0).NumberOfSensors)
                                    Case 6
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH_OB.BackColor = Color.Lime
                                                frmMain.osPTS_Front_LH_OB.BackColor = Color.Lime
                                            Case 1
                                                frmMain.lbl_PTS_LH.BackColor = Color.Lime
                                                frmMain.osPTS_LH.BackColor = Color.Lime

                                            Case 2
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Lime
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Lime

                                            Case 3
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Lime
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Lime

                                            Case 4
                                                frmMain.lbl_PTS_RH.BackColor = Color.Lime
                                                frmMain.osPTS_RH.BackColor = Color.Lime

                                            Case 5
                                                frmMain.lbl_PTS_RH_OB.BackColor = Color.Lime
                                                frmMain.osPTS_Front_RH_OB.BackColor = Color.Lime
                                        End Select
                                    Case 4
                                        Select Case NumberOfSensors
                                            Case 0
                                                frmMain.lbl_PTS_LH.BackColor = Color.Lime
                                                frmMain.osPTS_LH.BackColor = Color.Lime
                                            Case 1
                                                frmMain.lbl_PTS_LH_CTR.BackColor = Color.Lime
                                                frmMain.osPTS_LH_CTR.BackColor = Color.Lime

                                            Case 2
                                                frmMain.lbl_PTS_RH_CTR.BackColor = Color.Lime
                                                frmMain.osPTS_RH_CTR.BackColor = Color.Lime

                                            Case 3
                                                frmMain.lbl_PTS_RH.BackColor = Color.Lime
                                                frmMain.osPTS_RH.BackColor = Color.Lime
                                        End Select
                                    Case Else
                                        'Nothing
                                End Select

                        End Select

                    Case 9999
                        'Test Has Been Disabled Make It Pass No Matter What (Rehau Asked For This To Be Implamented
                        .Result = "P"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                        .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & "TEST BYPASSED"

                        Select Case Val(PTSTestVariables(0).NumberOfSensors)
                            Case 6
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH_OB.BackColor = Color.Orange
                                        frmMain.osPTS_Front_LH_OB.BackColor = Color.Orange
                                    Case 1
                                        frmMain.lbl_PTS_LH.BackColor = Color.Orange
                                        frmMain.osPTS_LH.BackColor = Color.Orange

                                    Case 2
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Orange
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Orange

                                    Case 3
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Orange
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Orange

                                    Case 4
                                        frmMain.lbl_PTS_RH.BackColor = Color.Orange
                                        frmMain.osPTS_RH.BackColor = Color.Orange

                                    Case 5
                                        frmMain.lbl_PTS_RH_OB.BackColor = Color.Orange
                                        frmMain.osPTS_Front_RH_OB.BackColor = Color.Orange
                                End Select
                            Case 4
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH.BackColor = Color.Orange
                                        frmMain.osPTS_LH.BackColor = Color.Orange
                                    Case 1
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Orange
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Orange

                                    Case 2
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Orange
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Orange

                                    Case 3
                                        frmMain.lbl_PTS_RH.BackColor = Color.Orange
                                        frmMain.osPTS_RH.BackColor = Color.Orange
                                End Select
                            Case Else
                                'Nothing
                        End Select

                    Case Else
                        .Result = "F"
                        AllTestsPassed = False
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                        .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                        FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                End Select
            End With
        Next




        TestResults_RFID.PTS = "1" 'Set The Over All Pass Fail Status To Pass As A Default (This IS Used In The RFID Reahu .dll)
        For NumberOfSensors = 0 To Val(PTSTestVariables(0).NumberOfSensors - 1)
            With PTSTestVariables(NumberOfSensors)
                If .Result = "F" Then
                    TestResults_RFID.PTS = "0" 'Set The Over All Pass Fail Status To Fail (This IS Used In The RFID Reahu .dll)
                    Exit For
                End If
            End With
        Next



    End Sub

    Sub ProductionData(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)



        Dim Result As Boolean = False
        Dim ReturnString As String = ""
        Dim LoopTimer As DateTime = Now



        'Load Variables For Test From The Test Information .ini File
        Tests.LoadPTSSensorTestVariables(ModelNumber, Bumper, SensorType)
        If AbortTesting = True Then Exit Sub

        If PTSTestVariables(0).TestName = "" Then
            AllTestsPassed = False
            AbortTesting = True
            MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
            frmMain.lblTestResult.BackColor = Color.Crimson
            Exit Sub
        End If

        'Call The Routine To Get Production Data All Bosch Sensors

        Result = EchoeProductionData(ReturnString)


        Select Case Result
            Case Is = True
                'Display Results
            Case Else
                'MsgBox("Could Not Read Echo Responses" & vbCrLf & " Please Turn Off Power On Test Box Wait A Few Seconds Then Turn Back On And Restart Test", MsgBoxStyle.Exclamation)
                Throw New Exception("Bosch Box Error")
                Exit Sub
        End Select












    End Sub


    Sub CheckForPTSSensorTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
        'Checks To see if the PTS Sensors are installed when they should not be.

        Dim Result As Boolean = False

        Dim LoopTimer As DateTime = Now
        Dim ReturnString As String = ""
        Dim BoschBoxToUse As Integer = 0
        Dim NewNumberOfSensorsConfigurationRequired As Boolean = False
        Dim NewSensorMaskConfigurationRequired As Boolean = False
        Dim StartSensorNumber As Integer = 0
        Dim StopSensorNumber As Integer = 0

        'Load Variables For Test From The Test Information .ini File
        Tests.LoadPTSSensorTestVariables(ModelNumber, Bumper, SensorType)

        'Show First Test Place In Grid So User Knows What Test Is Pending
        With PTSTestVariables(0)
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName & " (No Sensor Present Test)"
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "TESTING"
            frmMain.lblTestResult.Text = PTSTestVariables(0).TestName & " Test"
        End With

        LoopTimer = Now.AddMilliseconds(PTSTestVariables(0).DelayBeforeMeasurement)
        Do
            Application.DoEvents()
        Loop Until Now > LoopTimer


        'Call The Routine To Measure All Bosch Sensors
        Result = DirectEchoesRequest(ReturnString)
        Select Case Result
            Case Is = True
                'Display Results
            Case Else
                'MsgBox("Could Not Read Echo Responses" & vbCrLf & " Please Turn Off Power On Test Box Wait A Few Seconds Then Turn Back On And Restart Test", MsgBoxStyle.Exclamation)
                Throw New Exception("Bosch Box Error")
                Exit Sub
        End Select


        ' TestResultString = TestResultString & SensorType & ","
        For NumberOfSensors = 0 To Val(PTSTestVariables(0).NumberOfSensors - 1)
            With PTSTestVariables(NumberOfSensors)
                If NumberOfSensors <> 0 Then
                    'Do Not Add A Row Here For The First Sensor It Was Added Above
                    'Display Test Perameters In Grid
                    frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
                    frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName & " (No Sensor Present Test)"
                    frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
                    frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
                End If

                Select Case Val(PTSTestVariables(0).NumberOfSensors)
                    Case 6
                        Select Case NumberOfSensors
                            Case Is = 0
                                .ValueMeasured = TBOXEchoResponses.Sensor_1
                            Case Is = 1
                                .ValueMeasured = TBOXEchoResponses.Sensor_2
                            Case Is = 2
                                .ValueMeasured = TBOXEchoResponses.Sensor_3
                            Case Is = 3
                                .ValueMeasured = TBOXEchoResponses.Sensor_4
                            Case Is = 4
                                .ValueMeasured = TBOXEchoResponses.Sensor_5
                            Case Is = 5
                                .ValueMeasured = TBOXEchoResponses.Sensor_6
                        End Select

                    Case 4
                        Select Case NumberOfSensors
                            Case Is = 0
                                .ValueMeasured = TBOXEchoResponses.Sensor_1
                            Case Is = 1
                                .ValueMeasured = TBOXEchoResponses.Sensor_2
                            Case Is = 2
                                .ValueMeasured = TBOXEchoResponses.Sensor_3
                            Case Is = 3
                                .ValueMeasured = TBOXEchoResponses.Sensor_4
                            Case Else
                                '  Do Nothing
                        End Select

                    Case Else
                        MsgBox("Wrong Number Of Sensors Selected", MsgBoxStyle.Exclamation)
                        AbortTesting = True
                        Exit Sub
                End Select

                'Test Measured Value Against Limits
                'And Display Results
                Select Case Val(.ValueMeasured)
                    Case 0
                        .Result = "P"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                        .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured

                        Select Case Val(PTSTestVariables(0).NumberOfSensors)
                            Case 6
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH_OB.BackColor = Color.Lime
                                        frmMain.osPTS_Front_LH_OB.BackColor = Color.Lime
                                    Case 1
                                        frmMain.lbl_PTS_LH.BackColor = Color.Lime
                                        frmMain.osPTS_LH.BackColor = Color.Lime

                                    Case 2
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Lime
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Lime

                                    Case 3
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Lime
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Lime

                                    Case 4
                                        frmMain.lbl_PTS_RH.BackColor = Color.Lime
                                        frmMain.osPTS_RH.BackColor = Color.Lime

                                    Case 5
                                        frmMain.lbl_PTS_RH_OB.BackColor = Color.Lime
                                        frmMain.osPTS_Front_RH_OB.BackColor = Color.Lime
                                End Select
                            Case 4
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH.BackColor = Color.Lime
                                        frmMain.osPTS_LH.BackColor = Color.Lime
                                    Case 1
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Lime
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Lime

                                    Case 2
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Lime
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Lime

                                    Case 3
                                        frmMain.lbl_PTS_RH.BackColor = Color.Lime
                                        frmMain.osPTS_RH.BackColor = Color.Lime
                                End Select
                            Case Else
                                'Nothing
                        End Select

                    Case 1 To 248
                        .Result = "F"
                        AllTestsPassed = False
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                        frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                        .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                        FailString = .TestName & ":" & "Sensor Installed" & ","
                        Select Case Val(PTSTestVariables(0).NumberOfSensors)
                            Case 6
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH_OB.BackColor = Color.Crimson
                                        frmMain.osPTS_Front_LH_OB.BackColor = Color.Crimson
                                    Case 1
                                        frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                        frmMain.osPTS_LH.BackColor = Color.Crimson

                                    Case 2
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                    Case 3
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                    Case 4
                                        frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                        frmMain.osPTS_RH.BackColor = Color.Crimson

                                    Case 5
                                        frmMain.lbl_PTS_RH_OB.BackColor = Color.Crimson
                                        frmMain.osPTS_Front_RH_OB.BackColor = Color.Crimson
                                End Select
                            Case 4
                                Select Case NumberOfSensors
                                    Case 0
                                        frmMain.lbl_PTS_LH.BackColor = Color.Crimson
                                        frmMain.osPTS_LH.BackColor = Color.Crimson
                                    Case 1
                                        frmMain.lbl_PTS_LH_CTR.BackColor = Color.Crimson
                                        frmMain.osPTS_LH_CTR.BackColor = Color.Crimson

                                    Case 2
                                        frmMain.lbl_PTS_RH_CTR.BackColor = Color.Crimson
                                        frmMain.osPTS_RH_CTR.BackColor = Color.Crimson

                                    Case 3
                                        frmMain.lbl_PTS_RH.BackColor = Color.Crimson
                                        frmMain.osPTS_RH.BackColor = Color.Crimson
                                End Select
                            Case Else
                                'Nothing
                        End Select

                End Select

            End With
        Next

        TestResults_RFID.PTS = "1" 'Set The Over All Pass Fail Status To Pass As A Default (This IS Used In The RFID Reahu .dll)
        For NumberOfSensors = 0 To Val(PTSTestVariables(0).NumberOfSensors - 1)
            With PTSTestVariables(NumberOfSensors)
                If .Result = "F" Then
                    TestResults_RFID.PTS = "0" 'Set The Over All Pass Fail Status To Fail (This IS Used In The RFID Reahu .dll)
                    Exit For
                End If
            End With
        Next

    End Sub

    Sub LEDLampTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal LampType As String)
      'Loads The Lamp Test Variables For The Passed Model Number, Bumper, And Lamp Type
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim TempString As String = ""
      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & LampType & "_GEN1")

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
               LED_LampTestVariables(LeftHand).TestName = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               LED_LampTestVariables(LeftHand).MinLimit = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               LED_LampTestVariables(LeftHand).MaxLimit = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               LED_LampTestVariables(LeftHand).TestUnit = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               LED_LampTestVariables(LeftHand).MeasurementType = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("MeterRange")
               LED_LampTestVariables(LeftHand).MeterRange = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).MeterRange = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               LED_LampTestVariables(LeftHand).DelayBeforeMeasurement = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               LED_LampTestVariables(LeftHand).RelayNumberForConnection = SplitString(TempString, ",")
               LED_LampTestVariables(RightHand).RelayNumberForConnection = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The LoadLampTestVariables Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub

   '/////////////////////////////////////////////////////////////////
   Sub LEDLampTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal LampType As String)
      'Runs The Lamp Tests On The Passed Model Number, Bumper, And Lamp Type
      Dim LeftHand As Integer = 0
      Dim RightHand As Integer = 1
      Dim LoopTimer As DateTime = Now
      Dim ReturnedMessage As String = ""

      Try
         'Set Meter To Current Measurements
         ReturnedMessage = Meter_ADC()

         'Load Variables For Test From The Test Information .ini File
         Tests.LEDLampTestVariables(ModelNumber, Bumper, LampType)


         If LED_LampTestVariables(LeftHand).TestName = "" Then
            AllTestsPassed = False
            AbortTesting = True
            frmMain.lblTestResult.BackColor = Color.Crimson
            Throw New Exception("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information")
            Exit Sub
         End If

         'Test LeftHand Side
         With LED_LampTestVariables(LeftHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lblTestResult.Text = LED_LampTestVariables(LeftHand).TestName & " Test"
            frmMain.lbl_LAMP_DRIVER.Visible = True
            frmMain.lbl_LAMP_DRIVER.BackColor = Color.SkyBlue

            'Turn On Relays For Test
            RelayBoard.Turn_On_Relay(Val(LED_LampTestVariables(LeftHand).RelayNumberForConnection))
            LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)

            Do
               Application.DoEvents()
               .ValueMeasured = Format(Val(Meter.Meter_TakeMeasurement), "####0.##") & " " & .TestUnit
               frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               delay(20)
            Loop Until Now > LoopTimer

            'Test Measured Value Against Limits
            'And Display Results
            Select Case Val(.ValueMeasured)
               Case Is > Val(.MaxLimit)
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  frmMain.lbl_LAMP_DRIVER.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  TestResults_RFID.LED = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
               Case Is < Val(.MinLimit)
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  frmMain.lbl_LAMP_DRIVER.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  TestResults_RFID.LED = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  frmMain.lbl_LAMP_DRIVER.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  TestResults_RFID.LED = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
            End Select

         End With

         'Turn Off Relay When Test Is Complete
         RelayBoard.Turn_Off_Relay(Val(LED_LampTestVariables(LeftHand).RelayNumberForConnection))
         delay(250)

         'Test RightHand Side
         With LED_LampTestVariables(RightHand)
            'Display Test Perameters In Grid
            frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
            frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
            frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
            frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit

            frmMain.lbl_LAMP_PASSENGER.Visible = True
            frmMain.lbl_LAMP_PASSENGER.BackColor = Color.SkyBlue

            frmMain.lblTestResult.Text = LED_LampTestVariables(RightHand).TestName & " Test"

            'Turn On Relays For Test
            RelayBoard.Turn_On_Relay(Val(LED_LampTestVariables(RightHand).RelayNumberForConnection))
            LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)

            Do
               Application.DoEvents()
               .ValueMeasured = Format(Val(Meter.Meter_TakeMeasurement), "####0.##") & " " & .TestUnit
               frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
               delay(20)
            Loop Until Now > LoopTimer

            'Test Measured Value Against Limits
            'And Display Results
            Select Case Val(.ValueMeasured)
               Case Is > Val(.MaxLimit)
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  frmMain.lbl_LAMP_PASSENGER.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  TestResults_RFID.LED = "0" 'Set Result To Fail (Used In RFID Rehau .dll)

               Case Is < Val(.MinLimit)
                  .Result = "F"
                  AllTestsPassed = False
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  frmMain.lbl_LAMP_PASSENGER.BackColor = Color.Crimson
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","
                  TestResults_RFID.LED = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
               Case Else
                  .Result = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  frmMain.lbl_LAMP_PASSENGER.BackColor = Color.Lime
                  .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
                  TestResults_RFID.LED = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
            End Select

            RelayBoard.Turn_Off_Relay(Val(LED_LampTestVariables(RightHand).RelayNumberForConnection))
         End With

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error In The LampTests Routine : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True

      End Try

   End Sub



   Sub LoadTemperatureSensorTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Lamp Test Variables For The Passed Model Number, Bumper, And Lamp Type
      Dim TempSensor_1 As Integer = 0
      Dim TempString As String = ""

      Dim SectionName As String = ""

      'Change Section Name Based On Generation Of Sensor
      If frmMain.rbTEMP_GEN_1.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN1")
      End If

      If frmMain.rbTEMP_GEN_2.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN2")
      End If

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
               TempereatureSensorTestVariables(TempSensor_1).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               TempereatureSensorTestVariables(TempSensor_1).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               TempereatureSensorTestVariables(TempSensor_1).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               TempereatureSensorTestVariables(TempSensor_1).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               TempereatureSensorTestVariables(TempSensor_1).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("MeterRange")
               TempereatureSensorTestVariables(TempSensor_1).MeterRange = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               TempereatureSensorTestVariables(TempSensor_1).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               TempereatureSensorTestVariables(TempSensor_1).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForHighSideConnect")
               TempereatureSensorTestVariables(TempSensor_1).RelayNumberForHighSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub

   Sub TemperatureSensorTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Runs The Temerature Sensor Test On The Passed Model Number, Bumper, And Sensor Type
      Dim TempSensor_1 As Integer = 0
      Dim LoopTimer As DateTime = Now

      'Set Meter To Resistance Mode
      Meter.Meter_OHMS()
      delay(1000)

      'Load Variables For Test From The Test Information .ini File
      Tests.LoadTemperatureSensorTestVariables(ModelNumber, Bumper, SensorType)

      If TempereatureSensorTestVariables(TempSensor_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      'Test Sensor
      With TempereatureSensorTestVariables(TempSensor_1)
         'Display Test Perameters In Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
         frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
         frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
         frmMain.lbl_TEMP.BackColor = System.Drawing.SystemColors.ActiveCaption


         frmMain.lblTestResult.Text = TempereatureSensorTestVariables(TempSensor_1).TestName & " Test"

         'Turn On Relays For Test
         RelayBoard.Turn_On_Relay(Val(.RelayNumberForLowSideConnect))
         RelayBoard.Turn_On_Relay(Val(.RelayNumberForHighSideConnect))
         Debug.Print(Val(.RelayNumberForLowSideConnect))
         Debug.Print(Val(.RelayNumberForHighSideConnect))

         LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)
         Do
            Application.DoEvents()
            .ValueMeasured = Format(Val(Meter.Meter_TakeMeasurement), "####0.##") & " " & .TestUnit
            Select Case Val(.ValueMeasured)
               Case Is = 1000000000
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "OPEN"
               Case Else
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select
            delay(20)
         Loop Until Now > LoopTimer

         'Test Measured Value Against Limits
         'And Display Results
         Select Case Val(.ValueMeasured)
            Case Is > Val(.MaxLimit)
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lbl_TEMP.BackColor = Color.Crimson
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
               FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","

            Case Is < Val(.MinLimit)
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lbl_TEMP.BackColor = Color.Crimson
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
               FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","

            Case Else
               .Result = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
               frmMain.lbl_TEMP.BackColor = Color.Lime
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured

         End Select

         'Turn Off Relays When Test Is Complete
         RelayBoard.Turn_Off_Relay(Val(.RelayNumberForLowSideConnect))
         RelayBoard.Turn_Off_Relay(Val(.RelayNumberForHighSideConnect))

      End With

      'Save OverAll Result For RFID Rehau .dll
      If TempereatureSensorTestVariables(TempSensor_1).Result = "P" Then
         TestResults_RFID.TEMP = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
      Else
         TestResults_RFID.TEMP = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
      End If
   End Sub
   Sub LoadCameraTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Camera Test Variables For The Passed Model Number, Bumper, And Lamp Type

      Dim TempSensor_1 As Integer = 0
      Dim TempString As String = ""
      Dim SectionName As String = ""

      'Change Section Name Based On Generation Of Sensor
      If frmMain.rbCAMERA_GEN_1.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN1")
      End If

      If frmMain.rbCAMERA_GEN_2.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN2")
      End If

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
               CameraTestVariables(TempSensor_1).TestName = DBRead("TestDescription")
               'CameraTestVariables(TempSensor_1).MinLimit = DBRead("MinLimit").ToString
               'CameraTestVariables(TempSensor_1).MaxLimit = DBRead("MaxLimit")
               'CameraTestVariables(TempSensor_1).TestUnit = DBRead("TestUnit")
               'CameraTestVariables(TempSensor_1).MeasurementType = DBRead("MeasurementType")
               'CameraTestVariables(TempSensor_1).MeterRange = DBRead("MeterRange")
               CameraTestVariables(TempSensor_1).DelayBeforeMeasurement = DBRead("DelayBeforeMeasurement")
               'CameraTestVariables(TempSensor_1).RelayNumberForLowSideConnect = DBRead("RelayNumberForLowSideConnect")
               'CameraTestVariables(TempSensor_1).RelayNumberForHighSideConnect = DBRead("RelayNumberForHighSideConnect")
               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub
   Sub CameraTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Runs The Camera Test On The Passed Model Number, Bumper, And Sensor Type
      Dim Camera_1 As Integer = 0
      Dim LoopTimer As DateTime = Now

      'Load Variables For Test From The Test Information .ini File
      Tests.LoadCameraTestVariables(ModelNumber, Bumper, SensorType)

      If CameraTestVariables(Camera_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      frmMain.lblTestResult.Text = CameraTestVariables(Camera_1).TestName & " Test"

      'Test Sensor
      With CameraTestVariables(Camera_1)
         'Display Test Perameters In Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
         'frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
         'frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
         frmMain.lblCamera.BackColor = System.Drawing.SystemColors.ActiveCaption

         LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)
         Do
            Application.DoEvents()
            .ValueMeasured = CameraHarnessPresent().ToString
            Select Case .ValueMeasured
               Case "True", "1", "-1"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "CLOSED"
               Case Else
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "OPEN"
            End Select
            delay(20)
         Loop Until Now > LoopTimer

         'Test Measured Value Against Limits
         'And Display Results
         Select Case .ValueMeasured
            Case Is = "False"
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lblCamera.BackColor = Color.Crimson
               .TestResultString = .Result & "," & "Failed Camera Harness Test"
               FailString = .TestName & ":" & "Failed Camera Harness Test"
               TestResults_RFID.CAMERA = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
            Case Else
               .Result = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
               frmMain.lblCamera.BackColor = Color.Lime
               .TestResultString = .Result & "," & "Passed Camera Harness Test"
               TestResults_RFID.CAMERA = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
         End Select

      End With

   End Sub


   Sub LoadARTPlateTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Camera Test Variables For The Passed Model Number, Bumper, And Lamp Type

      Dim ARTPlateSensor_1 As Integer = 0
      Dim TempString As String = ""
      Dim SectionName As String = ""

      'Change Section Name Based On Generation Of Sensor
      If frmMain.rbART_PLATE_GEN_1.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN1")
      End If

      If frmMain.rbART_PLATE_GEN_2.Checked = True Then
         SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN2")
      End If

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
               ARTPlateTestVariables(ARTPlateSensor_1).TestName = DBRead("TestDescription")
               ARTPlateTestVariables(ARTPlateSensor_1).DelayBeforeMeasurement = DBRead("DelayBeforeMeasurement")
               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub

   Sub ARTPlateTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Runs The ART PLATE Test On The Passed Model Number, Bumper, And Sensor Type
      Dim ARTPlate_1 As Integer = 0
      Dim LoopTimer As DateTime = Now

      'Load Variables For Test From The Test Information .ini File
      Tests.LoadARTPlateTestVariables(ModelNumber, Bumper, SensorType)

      If ARTPlateTestVariables(ARTPlate_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      frmMain.lblTestResult.Text = ARTPlateTestVariables(ARTPlate_1).TestName & " Test"

      'Test Sensor
      With ARTPlateTestVariables(ARTPlate_1)
         'Display Test Perameters In Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
         'frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
         'frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
         frmMain.lblArtPlate.BackColor = System.Drawing.SystemColors.ActiveCaption

         LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)
         Do
            Application.DoEvents()
            .ValueMeasured = ARTPlateHarnessPresent().ToString
            Select Case .ValueMeasured
               Case "True", "1", "-1"
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "CLOSED"
               Case Else
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "OPEN"
            End Select
            delay(20)
         Loop Until Now > LoopTimer

         'Test Measured Value Against Limits
         'And Display Results
         Select Case .ValueMeasured
            Case Is = "False"
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lblArtPlate.BackColor = Color.Crimson
               .TestResultString = .Result & "," & "Failed ART Plate Harness Test"
               FailString = .TestName & ":" & "Failed ART Plate Harness Test"
               TestResults_RFID.ART = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
            Case Else
               .Result = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
               frmMain.lblArtPlate.BackColor = Color.Lime
               .TestResultString = .Result & "," & "Passed ART Plate  Harness Test"
               TestResults_RFID.ART = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
         End Select

      End With

   End Sub

   '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   '09_25_2022 JME Added For Heater Element
   Sub LoadHeatingElementTestVariables(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Loads The Lamp Test Variables For The Passed Model Number, Bumper, And Lamp Type
      Dim TempSensor_1 As Integer = 0
      Dim TempString As String = ""

      Dim SectionName As String = ""

      SectionName = (ModelNumber & "_" & Bumper & "_" & SensorType & "_GEN1")


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
               HeatingElementTestVariables(TempSensor_1).TestName = SplitString(TempString, ",")

               TempString = DBRead("MinLimit").ToString
               HeatingElementTestVariables(TempSensor_1).MinLimit = SplitString(TempString, ",")

               TempString = DBRead("MaxLimit")
               HeatingElementTestVariables(TempSensor_1).MaxLimit = SplitString(TempString, ",")

               TempString = DBRead("TestUnit")
               HeatingElementTestVariables(TempSensor_1).TestUnit = SplitString(TempString, ",")

               TempString = DBRead("MeasurementType")
               HeatingElementTestVariables(TempSensor_1).MeasurementType = SplitString(TempString, ",")

               TempString = DBRead("MeterRange")
               HeatingElementTestVariables(TempSensor_1).MeterRange = SplitString(TempString, ",")

               TempString = DBRead("DelayBeforeMeasurement")
               HeatingElementTestVariables(TempSensor_1).DelayBeforeMeasurement = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForLowSideConnect")
               HeatingElementTestVariables(TempSensor_1).RelayNumberForLowSideConnect = SplitString(TempString, ",")

               TempString = DBRead("RelayNumberForHighSideConnect")
               HeatingElementTestVariables(TempSensor_1).RelayNumberForHighSideConnect = SplitString(TempString, ",")

               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No Data In Database File")

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub

   '09_25_2022 JME Added For Heater Element
   Sub HeatingElementTest(ByVal ModelNumber As String, ByVal Bumper As String, ByVal SensorType As String)
      'Runs The Temerature Sensor Test On The Passed Model Number, Bumper, And Sensor Type
      Dim TempSensor_1 As Integer = 0
      Dim LoopTimer As DateTime = Now

      'Set Meter To Resistance Mode
      Meter.Meter_OHMS()
      delay(1000)

      'Load Variables For Test From The Test Information .ini File
      Tests.LoadHeatingElementTestVariables(ModelNumber, Bumper, SensorType)

      If HeatingElementTestVariables(TempSensor_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check Tester Configuration Database For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      'Test Sensor
      With HeatingElementTestVariables(TempSensor_1)
         'Display Test Perameters In Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         Debug.Print(frmMain.Grid_Test.RowCount.ToString)
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
         frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = .MinLimit & " " & .TestUnit
         frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = .MaxLimit & " " & .TestUnit
         frmMain.lbl_Heating_Element.BackColor = System.Drawing.SystemColors.ActiveCaption


         frmMain.lblTestResult.Text = HeatingElementTestVariables(TempSensor_1).TestName & " Test"

         'Turn On Relays For Test
         RelayBoard.Turn_On_Relay(Val(.RelayNumberForLowSideConnect))
         RelayBoard.Turn_On_Relay(Val(.RelayNumberForHighSideConnect))
         Debug.Print(Val(.RelayNumberForLowSideConnect))
         Debug.Print(Val(.RelayNumberForHighSideConnect))

         LoopTimer = Now.AddMilliseconds(.DelayBeforeMeasurement)
         Do
            Application.DoEvents()
            .ValueMeasured = Format(Val(Meter.Meter_TakeMeasurement), "####0.##") & " " & .TestUnit
            Select Case Val(.ValueMeasured)
               Case Is = 1000000000
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "OPEN"
               Case Else
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = .ValueMeasured
            End Select
            delay(20)
         Loop Until Now > LoopTimer

         'Test Measured Value Against Limits
         'And Display Results
         Select Case Val(.ValueMeasured)
            Case Is > Val(.MaxLimit)
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lbl_Heating_Element.BackColor = Color.Crimson
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
               FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","

            Case Is < Val(.MinLimit)
               .Result = "F"
               AllTestsPassed = False
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               frmMain.lbl_Heating_Element.BackColor = Color.Crimson
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured
               FailString = FailString & .TestName & ":" & .Result & "," & .ValueMeasured & ","

            Case Else
               .Result = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
               frmMain.lbl_Heating_Element.BackColor = Color.Lime
               .TestResultString = .Result & "," & .MinLimit & "," & .MaxLimit & "," & .ValueMeasured

         End Select

         'Turn Off Relays When Test Is Complete
         RelayBoard.Turn_Off_Relay(Val(.RelayNumberForLowSideConnect))
         RelayBoard.Turn_Off_Relay(Val(.RelayNumberForHighSideConnect))

      End With

      'Save OverAll Result For RFID Rehau .dll
      If HeatingElementTestVariables(TempSensor_1).Result = "P" Then
         TestResults_RFID.HE = "1" 'Set Result To Pass (Used In RFID Rehau .dll)
      Else
         TestResults_RFID.HE = "0" 'Set Result To Fail (Used In RFID Rehau .dll)
      End If
   End Sub
End Module



