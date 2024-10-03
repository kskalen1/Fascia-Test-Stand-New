Module SensorTests

   Sub LoadSensorTestVariables(ByVal ModelNumber As String, ByVal SensorType As String, ByVal Bumper As String)
      'Loads The LoadSensorTestVariables For The Passed Model Number, Bumper, And Sensor Type
      Dim SensorIndex As Integer = 0
      Dim TempString As String = ""
      Dim SectionName As String = ""
      Dim FasciaType As String = ""
      Dim ModelYear As String = ""
      Dim Generation As String = ""

      'Clear Value Before Next Read Changed JME 03_12_2016
      SensorTestVariables(SensorIndex).TestName = ""

      If frmMain.rbBasic.Checked = True Then FasciaType = "BASIC"
      If frmMain.rbAMG.Checked = True Then FasciaType = "AMG"
      If frmMain.rbPlatform_1.Checked = True Then FasciaType = "MAYBACH"

      If frmMain.rbRadar_Sensor_GEN_1.Checked = True Then Generation = "GEN1"
      If frmMain.rbRadar_Sensor_GEN_2.Checked = True Then Generation = "GEN2"
      If frmMain.rbRadar_Sensor_GEN_3.Checked = True Then Generation = "GEN3"


      SectionName = (ModelNumber & "_" & SensorType & "_" & Bumper & "_" & FasciaType & "_" & Generation)

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.Test_InformationConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "GeneralTests"
      DBCmdString = "Select * FROM RadarSensorTests WHERE" & " TestName = '" & SectionName & "'"

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
               SensorTestVariables(SensorIndex).TestName = DBRead("TestDescription")
               SensorTestVariables(SensorIndex).PlatFormID = DBRead("PlatformID")
               SensorTestVariables(SensorIndex).PlatFormName = DBRead("PlatFormName")
               SensorTestVariables(SensorIndex).ConfigurationFile = DBRead("TbetConfigurationFileName")
               SensorTestVariables(SensorIndex).SensorAddress1 = DBRead("SensorAddress1").ToString
               SensorTestVariables(SensorIndex).SensorAddress2 = DBRead("SensorAddress2").ToString
               SensorTestVariables(SensorIndex).SensorAddress3 = DBRead("SensorAddress3").ToString
               SensorTestVariables(SensorIndex).Whichdll = DBRead("AutolivdllToUse")
               'Load Chassis Code
               ChassisCode.Remove(0, ChassisCode.Length)
               ChassisCode.Append(DBRead("ChassisCode"))
               index = index + 1
            Loop
         End If

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The General Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub

   Sub SensorPreTest(ByVal ModelNumber As String, ByVal SensorType As String, ByVal Bumper As String)
      'Sets The Universal Sensor Test On The Passed Model Number, Bumper, And Sensor Type and kicks off background worker
      Dim SensorIndex As Integer = 0
      Dim LoopTimer As DateTime = Now
      Dim BumperLocation As Integer = 0

      'Load Variables For Test From The Test Information .ini File
      SensorTests.LoadSensorTestVariables(ModelNumber, SensorType, Bumper)

      If SensorTestVariables(SensorIndex).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check PLC And Tester Configuration File For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      WhichDll = SensorTestVariables(SensorIndex).Whichdll
      'Load Config File
      TBET.LoadTbetConfigurationFile(SensorTestVariables(SensorIndex).ConfigurationFile)


      Select Case Bumper
         Case Is = "Front"
            BumperLocation = 1
         Case Is = "Rear"
            BumperLocation = 2
      End Select

      SensorPreTestVariables.BumperLocation = BumperLocation
      SensorPreTestVariables.PlatFormID = Val(SensorTestVariables(SensorIndex).PlatFormID)

      'Save Variables for background worker
      'Start BackGround worker to Test Bumper
      'If Still Busy For Some Reason From Another Test Then Wait Until Thread Completes Then Start Over
      Dim SaveText As String = frmMain.lblTestResult.Text
      frmMain.lblTestResult.Text = "In Sensor Test Routine Waiting For Thread To Complete"
      Do While frmMain.Sensor_BackgroundWorker.IsBusy
         Application.DoEvents()
      Loop
      frmMain.lblTestResult.Text = SaveText

      frmMain.Sensor_BackgroundWorker.RunWorkerAsync()

   End Sub

   Sub SensorTest(ByVal ModelNumber As String, ByVal SensorType As String, ByVal Bumper As String)
      'Runs The Universal Sensor Test On The Passed Model Number, Bumper, And Sensor Type
      'Runs The BSM Sensor Test On The Passed Model Number, Bumper, And Sensor Type

      Dim UniversalSensor_1 As Integer = 0
      Dim LoopTimer As DateTime = Now
      Dim BumperLocation As Integer = 0
      Dim BumperTestResult As String = ""
      Dim UniversalSensor1Index As Integer = Val(SensorTestVariables(UniversalSensor_1).SensorAddress1)
      Dim UniversalSensor2Index As Integer = Val(SensorTestVariables(UniversalSensor_1).SensorAddress2)
      Dim UniversalSensor3Index As Integer = Val(SensorTestVariables(UniversalSensor_1).SensorAddress3)
      ReDim SensorTestVariables(UniversalSensor_1).SerialNum(4)
      ReDim SensorTestVariables(UniversalSensor_1).majVersion(4)
      ReDim SensorTestVariables(UniversalSensor_1).minVersion(4)
      ReDim SensorTestVariables(UniversalSensor_1).errorMessages(4)
      ReDim SensorTestVariables(UniversalSensor_1).masrNum(4)

      'Load Variables For Test From The Test Information .ini File
      SensorTests.LoadSensorTestVariables(ModelNumber, SensorType, Bumper)

      If SensorTestVariables(UniversalSensor_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check PLC And Tester Configuration File For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      frmMain.lblTestResult.Text = SensorTestVariables(UniversalSensor_1).TestName & " Sensor Test"

      If AbortTesting = True Then Exit Sub

      Select Case Bumper
         Case Is = "Front"
            BumperLocation = 1
         Case Is = "Rear"
            BumperLocation = 2
      End Select

      With SensorTestVariables(UniversalSensor_1)
         'Display Test Perameters In Grid
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName
         frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = "N/A"
         frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = "N/A"
         frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "?"
         frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "Testing"
         frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = System.Drawing.SystemColors.ActiveCaption
         frmMain.Refresh()
      End With
      Select Case Bumper
         Case Is = "Front"
            If frmMain.rbDTRTestSelected.Checked = True Then
               frmMain.lbl_BSM_DTR_LH.BackColor = System.Drawing.SystemColors.ActiveCaption
               frmMain.lbl_BSM_DTR_RH.BackColor = System.Drawing.SystemColors.ActiveCaption
               frmMain.lbl_BSM_DTR_LH.Visible = True
               frmMain.lbl_BSM_DTR_RH.Visible = True
               frmMain.lbl_FCW.Visible = False
            End If
            If frmMain.rbFCWTestSelected.Checked = True Then
               frmMain.lbl_FCW.BackColor = System.Drawing.SystemColors.ActiveCaption
               frmMain.lbl_FCW.Visible = True
               frmMain.lbl_BSM_DTR_LH.Visible = False
               frmMain.lbl_BSM_DTR_RH.Visible = False
            End If

         Case Is = "Rear"
            frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = System.Drawing.SystemColors.ActiveCaption
            frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = System.Drawing.SystemColors.ActiveCaption
            If frmMain.rbRCWTestSelected.Checked = True Then
               frmMain.lbl_Rear_RCW.BackColor = System.Drawing.SystemColors.ActiveCaption
            Else
               frmMain.lbl_Rear_RCW.Visible = False
            End If
      End Select

      'Wait for the Background worker to complete the test then display results
      Do
         Application.DoEvents()
      Loop Until SensorPreTestVariables.TestComplete = True

      With SensorTestVariables(UniversalSensor_1)
         'Test Measured Value Against Limits
         'And Display Results

         Select Case SensorPreTestVariables.TestResult
            Case Is = "P"
               'Show Bumper Sensor Info On Screen
               SensorTestVariables(UniversalSensor_1).Result = "P"
               .SerialNum(UniversalSensor1Index) = tbetSensor(UniversalSensor1Index).lSerialNumber.ToString
               .majVersion(UniversalSensor1Index) = CStr(tbetSensor(UniversalSensor1Index).majVersion)
               .minVersion(UniversalSensor1Index) = CStr(tbetSensor(UniversalSensor1Index).minVersion)
               .SerialNum(UniversalSensor2Index) = CStr(tbetSensor(UniversalSensor2Index).SerialNum)
               .majVersion(UniversalSensor2Index) = CStr(tbetSensor(UniversalSensor2Index).majVersion)
               .minVersion(UniversalSensor2Index) = CStr(tbetSensor(UniversalSensor2Index).minVersion)
               .SerialNum(UniversalSensor3Index) = CStr(tbetSensor(UniversalSensor3Index).SerialNum)
               .majVersion(UniversalSensor3Index) = CStr(tbetSensor(UniversalSensor3Index).majVersion)
               .minVersion(UniversalSensor3Index) = CStr(tbetSensor(UniversalSensor3Index).minVersion)
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
               'Use New Product Test Results
               .TestResultString = .Result & "," & "(S1) " & "SN:" & .SerialNum(UniversalSensor1Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor1Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor1Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor1Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor1Index).lProdDay) & ":" & "MjV:" & SensorTestVariables(UniversalSensor_1).majVersion(UniversalSensor1Index) & ":" & "MnV:" & SensorTestVariables(UniversalSensor_1).minVersion(UniversalSensor1Index)
               If frmMain.rbFCWTestSelected.Checked = True Then
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "(S1) " & .SerialNum(UniversalSensor1Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor1Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor1Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor1Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor1Index).lProdDay) & ":" & "MjV:" & SensorTestVariables(UniversalSensor_1).majVersion(UniversalSensor1Index) & ":" & "MnV:" & SensorTestVariables(UniversalSensor_1).minVersion(UniversalSensor1Index) & ","
               ElseIf frmMain.rbRCWTestSelected.Checked = False Then
                  .TestResultString = .TestResultString & .Result & "," & "(S2) " & "SN:" & .SerialNum(UniversalSensor2Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor2Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor2Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor2Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor2Index).lProdDay) & ":" & "MjV:" & SensorTestVariables(UniversalSensor_1).majVersion(UniversalSensor2Index) & ":" & "MnV:" & SensorTestVariables(UniversalSensor_1).minVersion(UniversalSensor2Index)
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "(S1) " & "SN:" & .SerialNum(UniversalSensor1Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor1Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor1Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor1Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor1Index).lProdDay) & "  " & "(S2) " & "SN:" & .SerialNum(UniversalSensor2Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor2Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor2Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor2Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor2Index).lProdDay)
               Else
                  .TestResultString = .TestResultString & .Result & "," & "(S2) " & "SN:" & .SerialNum(UniversalSensor2Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor2Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor2Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor2Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor2Index).lProdDay) & ":" & "MjV:" & SensorTestVariables(UniversalSensor_1).majVersion(UniversalSensor2Index) & ":" & "MnV:" & SensorTestVariables(UniversalSensor_1).minVersion(UniversalSensor2Index)
                  .TestResultString = .TestResultString & .Result & "," & "(S3) " & "SN:" & .SerialNum(UniversalSensor3Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor3Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor3Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor3Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor3Index).lProdDay) & ":" & "MjV:" & SensorTestVariables(UniversalSensor_1).majVersion(UniversalSensor3Index) & ":" & "MnV:" & SensorTestVariables(UniversalSensor_1).minVersion(UniversalSensor3Index)

                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "(S1) " & "SN:" & .SerialNum(UniversalSensor1Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor1Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor1Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor1Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor1Index).lProdDay) & "  " & "(S2) " & "SN:" & .SerialNum(UniversalSensor2Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor2Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor2Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor2Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor2Index).lProdDay) & "  " & "(S3) " & "SN:" & .SerialNum(UniversalSensor3Index) & ":" & "P# " & CStr(tbetSensor(UniversalSensor3Index).lProdNumber) & " " & "DC " & CStr(tbetSensor(UniversalSensor3Index).lProdYear) & "W" & CStr(tbetSensor(UniversalSensor3Index).lProdWeek) & "D" & CStr(tbetSensor(UniversalSensor3Index).lProdDay)
               End If

               Select Case Bumper
                  Case Is = "Front"
                     If frmMain.rbDTRTestSelected.Checked = True Then
                        frmMain.lbl_BSM_DTR_LH.BackColor = Color.Lime
                        frmMain.lbl_BSM_DTR_RH.BackColor = Color.Lime
                        frmMain.lbl_BSM_DTR_LH.Visible = True
                        frmMain.lbl_BSM_DTR_RH.Visible = True
                        frmMain.lbl_FCW.Visible = False
                     End If
                     If frmMain.rbFCWTestSelected.Checked = True Then
                        frmMain.lbl_FCW.BackColor = Color.Lime
                        frmMain.lbl_FCW.Visible = True
                        frmMain.lbl_BSM_DTR_LH.Visible = False
                        frmMain.lbl_BSM_DTR_RH.Visible = False
                     End If

                  Case Is = "Rear"
                     frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Lime
                     frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Lime
                     frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                     frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                     If frmMain.rbRCWTestSelected.Checked = True Then
                        frmMain.lbl_Rear_RCW.BackColor = Color.Lime
                        frmMain.lbl_Rear_RCW.Visible = True
                     Else
                        frmMain.lbl_Rear_RCW.Visible = False
                     End If

               End Select

            Case Is = "F"
               SensorTestVariables(UniversalSensor_1).Result = "F"
               SensorFailed = True
               AllTestsPassed = False
               SensorFailedString = ""
               SensorFailCodes = ""

               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               Select Case Bumper
                  Case Is = "Front"
                     If InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#2") Or InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#3") Then
                        SensorFailedString = "(S1) " & tbetSensor(UniversalSensor1Index).errorString
                        SensorFailCodes = tbetSensor(UniversalSensor1Index).errorCodes
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                        If frmMain.rbDTRTestSelected.Checked = True Then
                           frmMain.lbl_BSM_DTR_LH.BackColor = Color.Crimson
                           frmMain.lbl_BSM_DTR_LH.Visible = True
                        End If
                        If frmMain.rbFCWTestSelected.Checked = True Then
                           frmMain.lbl_FCW.BackColor = Color.Crimson
                           frmMain.rbFCWTestSelected.Visible = True
                        End If
                     Else
                        If frmMain.rbDTRTestSelected.Checked = True Then
                           frmMain.lbl_BSM_DTR_LH.BackColor = Color.LightGray
                           frmMain.lbl_BSM_DTR_LH.Visible = True
                        End If
                        If frmMain.rbFCWTestSelected.Checked = True Then
                           frmMain.lbl_FCW.BackColor = Color.LightGray
                           frmMain.lbl_FCW.Visible = True
                        End If
                     End If
                     If InStr(CStr(tbetSensor(UniversalSensor2Index).errorString), "#3") Then
                        If SensorFailedString = "" Then
                           SensorFailedString = "(S2) " & tbetSensor(UniversalSensor2Index).errorString
                           SensorFailCodes = tbetSensor(UniversalSensor2Index).errorCodes
                        Else
                           SensorFailedString = SensorFailedString & "(S2) " & tbetSensor(UniversalSensor2Index).errorString
                           SensorFailCodes = SensorFailCodes & tbetSensor(UniversalSensor2Index).errorCodes
                        End If
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                        If frmMain.rbDTRTestSelected.Checked = True Then
                           frmMain.lbl_BSM_DTR_RH.BackColor = Color.Crimson
                           frmMain.lbl_BSM_DTR_RH.Visible = True
                        End If
                        If frmMain.rbFCWTestSelected.Checked = True Then
                           frmMain.lbl_FCW.BackColor = Color.Crimson
                           frmMain.lbl_FCW.Visible = True
                        End If
                     Else
                        If frmMain.rbDTRTestSelected.Checked = True Then
                           frmMain.lbl_BSM_DTR_RH.BackColor = Color.LightGray
                           frmMain.lbl_BSM_DTR_RH.Visible = True
                        End If
                     End If

                     If InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#2") = 0 And InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#3") = 0 And InStr(CStr(tbetSensor(UniversalSensor2Index).errorString), "#3") = 0 Then
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "Sensor (Failed!) " & CStr(tbetSensor(0).errorString)
                        SensorFailedString = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value
                        SensorFailCodes = tbetSensor(0).errorCodes
                        If frmMain.rbDTRTestSelected.Checked = True Then
                           frmMain.lbl_BSM_DTR_LH.BackColor = Color.Red
                           frmMain.lbl_BSM_DTR_RH.BackColor = Color.Red
                           frmMain.lbl_BSM_DTR_LH.Visible = True
                           frmMain.lbl_BSM_DTR_RH.Visible = True
                        End If
                        If frmMain.rbFCWTestSelected.Checked = True Then
                           frmMain.lbl_FCW.BackColor = Color.Red
                           frmMain.lbl_FCW.Visible = True
                        End If
                     End If

                     'Use Product Test Results
                     .TestResultString = .Result & "," & SensorFailedString

                  Case Is = "Rear"
                     If frmMain.rbRCWTestSelected.Checked = True Then   'If 3 Sensors Installed
                        If InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#1") Then
                           SensorFailedString = "(S1) " & tbetSensor(UniversalSensor1Index).errorString
                           SensorFailCodes = tbetSensor(UniversalSensor1Index).errorCodes
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Crimson
                           frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                        Else
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.LightGray
                           frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                        End If
                        If InStr(CStr(tbetSensor(UniversalSensor2Index).errorString), "#2") Then
                           SensorFailedString = SensorFailedString & "(S2) " & tbetSensor(UniversalSensor2Index).errorString
                           SensorFailCodes = tbetSensor(UniversalSensor2Index).errorCodes
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                           frmMain.lbl_Rear_RCW.BackColor = Color.Crimson
                           frmMain.lbl_Rear_RCW.Visible = True
                        Else
                           frmMain.lbl_Rear_RCW.BackColor = Color.LightGray
                           frmMain.lbl_Rear_RCW.Visible = True
                        End If

                        If InStr(CStr(tbetSensor(UniversalSensor3Index).errorString), "#4") Then
                           If SensorFailedString = "" Then
                              SensorFailedString = "(S3) " & tbetSensor(UniversalSensor3Index).errorString
                              SensorFailCodes = tbetSensor(UniversalSensor3Index).errorCodes
                           Else
                              SensorFailedString = SensorFailedString & "(S3) " & tbetSensor(UniversalSensor3Index).errorString
                              SensorFailCodes = SensorFailCodes & tbetSensor(UniversalSensor3Index).errorCodes
                           End If
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Crimson
                           frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                        Else
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.LightGray
                           frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                        End If

                     Else  'If Only 2 Sensors (No 3rd RCW) Then

                        If InStr(CStr(tbetSensor(UniversalSensor1Index).errorString), "#1") Then
                           SensorFailedString = "(S1) " & tbetSensor(UniversalSensor1Index).errorString
                           SensorFailCodes = tbetSensor(UniversalSensor1Index).errorCodes
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Crimson
                           frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                        Else
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.LightGray
                           frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                        End If

                        If InStr(CStr(tbetSensor(UniversalSensor2Index).errorString), "#4") Then
                           If SensorFailedString = "" Then
                              SensorFailedString = "(S2) " & tbetSensor(UniversalSensor2Index).errorString
                              SensorFailCodes = tbetSensor(UniversalSensor2Index).errorCodes
                           Else
                              SensorFailedString = SensorFailedString & "(S2) " & tbetSensor(UniversalSensor2Index).errorString
                              SensorFailCodes = SensorFailCodes & tbetSensor(UniversalSensor2Index).errorCodes
                           End If
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = SensorFailedString
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Crimson
                           frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                        Else
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.LightGray
                           frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                        End If

                     End If

                     If frmMain.rbRCWTestSelected.Checked = True Then
                        If InStr(tbetSensor(UniversalSensor1Index).errorString, "#1") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#2") = 0 And InStr(tbetSensor(UniversalSensor3Index).errorString, "#4") = 0 Then
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "Sensor (Failed!) " & CStr(tbetSensor(0).errorString)
                           SensorFailedString = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value
                           SensorFailCodes = tbetSensor(0).errorCodes
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Red
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Red
                           frmMain.lbl_Rear_RCW.BackColor = Color.Red
                        End If
                     Else
                        If InStr(tbetSensor(UniversalSensor1Index).errorString, "#1") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#4") = 0 Then
                           frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "Sensor (Failed!) " & CStr(tbetSensor(0).errorString)
                           SensorFailedString = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value
                           SensorFailCodes = tbetSensor(0).errorCodes
                           frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Red
                           frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Red
                        End If
                     End If
               End Select

               'Record Any Errors That Are Not Attached To A Particular Sensor .. Such as Error 4 Sensor Placement Differs From Config File
               'Check First To Make Sure No other sensor failure have occured we dont want to overwrite a single sensor failure
               If frmMain.rbRCWTestSelected.Checked = True Then
                  If InStr(tbetSensor(UniversalSensor1Index).errorString, "#1") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#2") = 0 And InStr(tbetSensor(UniversalSensor3Index).errorString, "#4") = 0 Then
                     If tbetSensor(0).errorString <> "" Then
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value & CStr(tbetSensor(0).errorString)
                        SensorFailedString = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value
                        SensorFailCodes = SensorFailCodes & tbetSensor(0).errorCodes
                        frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Red
                        frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Red
                        frmMain.lbl_Rear_RCW.BackColor = Color.Red
                     End If
                  End If
               Else
                  If InStr(tbetSensor(UniversalSensor1Index).errorString, "#1") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#4") = 0 And InStr(tbetSensor(UniversalSensor1Index).errorString, "#2") = 0 And InStr(tbetSensor(UniversalSensor1Index).errorString, "#3") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#2") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#3") = 0 And InStr(tbetSensor(UniversalSensor2Index).errorString, "#4") = 0 Then
                     If tbetSensor(0).errorString <> "" Then
                        frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value & CStr(tbetSensor(0).errorString)
                        SensorFailedString = frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value
                        SensorFailCodes = SensorFailCodes & tbetSensor(0).errorCodes
                        frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Red
                        frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Red
                        frmMain.lbl_Rear_RCW.BackColor = Color.Red
                     End If
                  End If
               End If

               'Use New Product Test Results
               .TestResultString = .Result & "," & SensorFailedString
               FailString = FailString & SensorType & ":" & .TestResultString
         End Select
         Debug.Print(.TestResultString)

      End With

      If frmMain.rbFCWTestSelected.Checked = True Then
         'If The FCW Sensor Is Being Tested Then Make A copy Of The Teste Results (Used In Write Data To SQL Routine)
         FCWTestVariables = SensorTestVariables
         Select Case SensorPreTestVariables.TestResult
            Case "P"
               TestResults_RFID.FCW = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            Case "F"
               TestResults_RFID.FCW = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
         End Select
      End If

      If frmMain.rbDTRTestSelected.Checked = True Then
         'If The DTR Sensor Is Being Tested Then Make A copy Of The Teste Results (Used In Write Data To SQL Routine)
         DTRTestVariables = SensorTestVariables
         Select Case SensorPreTestVariables.TestResult
            Case "P"
               TestResults_RFID.DTR = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            Case "F"
               TestResults_RFID.DTR = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
         End Select
      End If

      If frmMain.rbBSMTestSelected.Checked = True Then
         'If The BSM Sensor Is Being Tested Then Make A copy Of The Teste Results (Used In Write Data To SQL Routine)
         BSMTestVariables = SensorTestVariables
         Select Case SensorPreTestVariables.TestResult
            Case "P"
               TestResults_RFID.BSM = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            Case "F"
               TestResults_RFID.BSM = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
         End Select
      End If

      If frmMain.rbiBSMTestSelected.Checked = True Then
         'If The iBSM Sensor Is Being Tested Then Make A copy Of The Teste Results (Used In Write Data To SQL Routine)
         iBSMTestVariables = SensorTestVariables
         Select Case SensorPreTestVariables.TestResult
            Case "P"
               TestResults_RFID.iBSM = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            Case "F"
               TestResults_RFID.iBSM = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
         End Select
      End If

      If frmMain.rbRCWTestSelected.Checked = True Then
         'If The RCW Sensor Is Being Tested Then Make A copy Of The Teste Results (Used In Write Data To SQL Routine)
         RCWTestVariables = SensorTestVariables
         Select Case SensorPreTestVariables.TestResult
            Case "P"
               TestResults_RFID.RCW = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            Case "F"
               TestResults_RFID.RCW = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
         End Select

      End If

   End Sub
   Sub CheckForSensorTest(ByVal ModelNumber As String, ByVal SensorType As String, ByVal Bumper As String)
      'Checks To See If A Sensor Is Installed When It Should Not Be On The Passed Model Number, Bumper, And Sensor Type
      Dim UniversalSensor_1 As Integer = 0
      Dim LoopTimer As DateTime = Now
      Dim BumperLocation As Integer = 0
      Dim BumperTestResult As String = ""
      Dim UniversalSensor1Index As Integer = Val(SensorTestVariables(UniversalSensor_1).SensorAddress1)
      Dim UniversalSensor2Index As Integer = Val(SensorTestVariables(UniversalSensor_1).SensorAddress2)
      ReDim SensorTestVariables(UniversalSensor_1).SerialNum(4)
      ReDim SensorTestVariables(UniversalSensor_1).majVersion(4)
      ReDim SensorTestVariables(UniversalSensor_1).minVersion(4)
      ReDim SensorTestVariables(UniversalSensor_1).errorMessages(4)

      frmMain.lblTestResult.Text = "Check For NO Sensors Test"

      'Load Variables For Test From The Test Information .ini File
      SensorTests.LoadSensorTestVariables(ModelNumber, SensorType, Bumper)
      If AbortTesting = True Then Exit Sub

      If SensorTestVariables(UniversalSensor_1).TestName = "" Then
         AllTestsPassed = False
         AbortTesting = True
         MsgBox("Invalid Test Type Selected, Please Check PLC And Tester Configuration File For Correct Information", MsgBoxStyle.Critical, "Configuration Error")
         frmMain.lblTestResult.BackColor = Color.Crimson
         Exit Sub
      End If

      Select Case Bumper
         Case Is = "Front"
            BumperLocation = 1
         Case Is = "Rear"
            BumperLocation = 2
      End Select

      WhichDll = SensorTestVariables(UniversalSensor_1).Whichdll
      'Load Config File
      TBET.LoadTbetConfigurationFile(SensorTestVariables(UniversalSensor_1).ConfigurationFile)

      With SensorTestVariables(UniversalSensor_1)
         'Display Test Perameters In Grid
         frmMain.Grid_Test.RowCount = frmMain.Grid_Test.RowCount + 1   'Add A Row For The Test In The Grid
         frmMain.Grid_Test.Item(GridColIndex.TestName, RowIndex - 1).Value = .TestName & " (No Sensor Present Test)"
         frmMain.Grid_Test.Item(GridColIndex.MinLimit, RowIndex - 1).Value = "N/A"
         frmMain.Grid_Test.Item(GridColIndex.MaxLimit, RowIndex - 1).Value = "N/A"
         frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "?"
         frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "Testing"
         frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Gold
         frmMain.Refresh()
      End With

      SensorPreTestVariables.BumperLocation = BumperLocation
      SensorPreTestVariables.PlatFormID = Val(SensorTestVariables(UniversalSensor_1).PlatFormID)

      'Save Variables for background worker
      'Start BackGround worker to Test Bumper
      'If Still Busy For Some Reason From Another Test Then Wait Until Thread Completes Then Start Over
      Dim SaveText As String = frmMain.lblTestResult.Text
      frmMain.lblTestResult.Text = "In Sensor Test Routine Waiting For Thread To Complete"
      Do While frmMain.Sensor_BackgroundWorker.IsBusy
         Application.DoEvents()
      Loop
      frmMain.lblTestResult.Text = SaveText

      frmMain.Sensor_BackgroundWorker.RunWorkerAsync()

      delay(1000)
      'Wait for the Background worker to complete the test then display results
      Do
         Application.DoEvents()
      Loop Until SensorPreTestVariables.TestComplete = True Or frmMain.Sensor_BackgroundWorker.IsBusy = False

      With SensorTestVariables(UniversalSensor_1)
         'Test Measured Value Against Limits
         'And Display Results

         Select Case SensorPreTestVariables.TestResult
            Case Is = "P"
               frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "SENSORS SHOULD NOT BE INSTALLED IN THIS BUMPER"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
               frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
               AllTestsPassed = False
               SensorTestVariables(UniversalSensor_1).Result = "F"
               Select Case Bumper
                  Case Is = "Front"
                     frmMain.lbl_BSM_DTR_LH.Visible = True
                     frmMain.lbl_BSM_DTR_LH.BackColor = Color.Crimson
                     frmMain.lbl_BSM_DTR_RH.Visible = True
                     frmMain.lbl_BSM_DTR_RH.BackColor = Color.Crimson
                     frmMain.lbl_FCW.Visible = False
                  Case Is = "Rear"
                     frmMain.lbl_Rear_BSM_iBSM_LH.Visible = True
                     frmMain.lbl_Rear_BSM_iBSM_LH.BackColor = Color.Crimson
                     frmMain.lbl_Rear_BSM_iBSM_RH.Visible = True
                     frmMain.lbl_Rear_BSM_iBSM_RH.BackColor = Color.Crimson
               End Select
               .TestResultString = "F" & "," & "Check For NO  Sensors"
               FailString = FailString & SensorType & ":" & .TestResultString

            Case Is = "F"
               .errorMessages(UniversalSensor1Index) = Trim(tbetSensor(0).errorString)
               If InStr(.errorMessages(UniversalSensor1Index), "9") Then
                  frmMain.lbl_BSM_DTR_LH.BackColor = Color.LightGray
                  frmMain.lbl_BSM_DTR_RH.BackColor = Color.LightGray
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "N/A"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "P"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Lime
                  DTRTestVariables(UniversalSensor_1).Result = "P"
                  .TestResultString = "P" & "," & "Check For NO Sensors"

               Else
                  AllTestsPassed = False
                  frmMain.lbl_BSM_DTR_LH.BackColor = Color.Crimson
                  frmMain.lbl_BSM_DTR_RH.BackColor = Color.Crimson
                  frmMain.Grid_Test.Item(GridColIndex.ValueMeasured, RowIndex - 1).Value = "There Is A Sensor Detected Of Some Type In This Fascia"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Value = "F"
                  frmMain.Grid_Test.Item(GridColIndex.Result, RowIndex - 1).Style.BackColor = Color.Crimson
                  DTRTestVariables(UniversalSensor_1).Result = "P"
                  .TestResultString = "F" & "," & "Check For NO Sensors"
                  FailString = FailString & SensorType & ":" & .TestResultString

               End If
         End Select

      End With

      Select Case SensorPreTestVariables.TestResult
         Case "F"
            TestResults_RFID.FCW = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            TestResults_RFID.DTR = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            TestResults_RFID.BSM = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            TestResults_RFID.iBSM = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
            TestResults_RFID.RCW = "1" 'Set The Test Result To Pass (Used for RFID Rehau .dll)
         Case "P"
            TestResults_RFID.FCW = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
            TestResults_RFID.DTR = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
            TestResults_RFID.BSM = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
            TestResults_RFID.iBSM = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
            TestResults_RFID.RCW = "0" 'Set The Test Result To Fail (Used for RFID Rehau .dll)
      End Select

   End Sub
End Module
