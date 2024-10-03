Imports System
Imports System.IO

Module Support
   Sub delay(ByVal milliseconds As Long) 'Delays For Milliseconds
      'Sub Delays X number of milliseconds

      Dim CurrentTime As Long
      Dim TimeToEnd As Long

      TimeToEnd = milliseconds + timeGetTime
      Do
         Application.DoEvents()
         CurrentTime = timeGetTime
      Loop Until CurrentTime >= TimeToEnd
   End Sub

   Function ReadINIFile(ByVal Section As String, ByVal Key As String, ByVal DefaultValue As String, ByVal FileName As String) As String

      ' This routine is used to Read Information Stored In .ini Files

      Dim Buffer As String
      Buffer = Space$(4096)
      Dim found As Integer

      found = GetPrivateProfileString(Section, Key, DefaultValue, Buffer, Len(Buffer), FileName)
      If found Then
         ReadINIFile = Left(Buffer, found)
      Else
         ReadINIFile = ""
      End If
   End Function

   Function SplitString(ByRef msg As String, ByVal delimiter As String) As String
      ' This routine is used to Split A String At The Delimiter Passed To It

      Dim ptr As Integer

      ptr = InStr(msg, delimiter, CompareMethod.Text)
      If ptr Then
         SplitString = Left$(msg, InStr(msg, delimiter, CompareMethod.Text) - 1)
         msg = Mid$(msg, InStr(msg, delimiter, CompareMethod.Text) + 1)
      Else
         SplitString = msg
         msg = ""
      End If
   End Function

   Sub WriteINIFile(ByVal Section As String, ByVal Key As String, ByVal writevalue As String, ByVal FileName As String)
      ' This routine is used to Write Information To .ini Files

      Dim msg As String

      '   Create file if it doesn't exist
      If Not File.Exists(FileName) Then
         Dim sr As StreamWriter = File.CreateText(FileName)
         sr.WriteLine("")
         sr.Flush()
         sr.Close()
      End If

      '   Write INI file checking for Errors
      If WritePrivateProfileString(Section, Key, writevalue, FileName) = 0 Then
         msg = "Unable to write to .INI file" & vbCrLf & vbCrLf
         msg = msg & "File Name: " & FileName & vbCrLf
         msg = msg & "[" & Section & "]" & vbCrLf & Key & "="
         MsgBox(msg, MsgBoxStyle.Critical, "ERROR")
         End
      End If
   End Sub

   Function LoadGeneralVariables() As Boolean
      'Loads General Variables From The .ini file

      Dim FileName As String
      Dim DefaultValue As String
      Dim Temp_String As String = ""
      Dim Counter As Integer = 0

      FileName = DefaultIniPath & DefaultINIFileName                       'Set name Of File To Search For Info.
      If File.Exists(FileName) = False Then
         MsgBox("Default.ini file not found" & FileName, MsgBoxStyle.Critical, "Load General Variables.ini ERROR")
         LoadGeneralVariables = False   'Fail Load Variables
         Exit Function
      End If

      Section = "System"

      Key = "MeterInstalled"               'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      MeterInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "RelayBoardInstalled"          'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      RelayBoardInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "PeakCanInstalled"             'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      PeakCanInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "BarcodeScannerInstalled"      'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      BarcodeScannerInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "ValeoSensorMeasurementDeviceInstalled"      'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      ValeoSensorMeasurementDeviceInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "LINBoxInstalled"      'Key String To Look For
      DefaultValue = "1"                   'Set DefaultValue Value in case variable key is not found
      LINBoxInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "RFIDInstalled"               'Key String To Look For
      DefaultValue = "0"                  'Set DefaultValue Value in case variable key is not found
      RFIDInstalled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "BarcodeScannerComPort"       'Key String To Look For
      DefaultValue = ""                   'Set DefaultValue Value in case variable key is not found
      BarcodeScannerComPort = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "MetercomPort"                 'Key String To Look For
      DefaultValue = ""                    'Set DefaultValue Value in case variable key is not found
      MetercomPort = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "RelayBoardcomPort"
      DefaultValue = ""                   'Set DefaultValue Value in case variable key is not found
      RelayBoardcomPort = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "TBOX_Ver_6_0_Installed"      'Key String To Look For
      DefaultValue = "1"                  'Set DefaultValue Value in case variable key is not found
      TBOX_Ver_6_0_Installed = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "TBOX_Ver_6_0_comPort"            'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      TBOX_Ver_6_0_comPort = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "DefaultTBOXCommunicationINIFileName"                     'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      DefaultTBOXCommunicationINIFileName = ReadINIFile(Section, Key, DefaultValue, FileName)


      Key = "DefaultTBETConfigurationFilePath"                     'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      DefaultTBETConfigurationFilePath = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "DefaultTBETConfigurationFileName" 'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      DefaultTBETConfigurationFileName = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "TimeToChangeFile"                'Key String To Look For
      DefaultValue = "6:59:59 AM"             'Set DefaultValue Value in case variable key is not found
      TimeToChangeFile = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "DayToChangeFile"                 'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      strDayToChangeFile = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "FileCreationDay"                 'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      strFileCreationDay = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "CurrentPassLogFileName"          'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      DataLogPassLabelFileName = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "CurrentErrorLogFileName"         'Key String To Look For
      DefaultValue = ""                       'Set DefaultValue Value in case variable key is not found
      DataLogErrorLabelFileName = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "Password"                        'Key String To Look For
      DefaultValue = "Rehau"                  'Set DefaultValue Value in case variable key is not found
      Password = ReadINIFile(Section, Key, DefaultValue, FileName)
      PasswordOk = False

      Key = "OperatorIDInactivityLogoutTime" 'Key String To Look For
      DefaultValue = "15"                    'Set DefaultValue Value in case variable key is not found
      OperatorIDInactivityLogoutTime = Val(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "StationID"                         'Key String To Look For
      DefaultValue = ""                         'Set DefaultValue Value in case variable key is not found
      frmMain.lblStationID.Text = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "MachineID"                         'Key String To Look For
      DefaultValue = ""                         'Set DefaultValue Value in case variable key is not found
      frmMain.lblMachineID.Text = ReadINIFile(Section, Key, DefaultValue, FileName)
      MachineID = Val(frmMain.lblMachineID.Text)

      Key = "Allocation_Logic_iZuol"                         'Key String To Look For
      DefaultValue = ""                         'Set DefaultValue Value in case variable key is not found
      Allocation_Logic_iZuol = Val(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "PrinterName"                             'Key String To Look For
      DefaultValue = "EasyCoder PD42 (300 dpi) - IPL" 'Set DefaultValue Value in case variable key is not found
      IntermecPrinterName = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "SQLDataSource"
      DefaultValue = ""
      SQLDataSource = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "SQLUserID"
      DefaultValue = ""
      SQLUserID = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "SQLPassword"
      DefaultValue = ""
      SQLPassword = ReadINIFile(Section, Key, DefaultValue, FileName)

      Key = "AllowRetestOfPassedFascia"
      DefaultValue = "True"
      AllowRetestOfPassedFascia = Convert.ToBoolean(ReadINIFile(Section, Key, DefaultValue, FileName))


      Key = "CAN_High_Relay"
      DefaultValue = "0"
      CANHighRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "CAN_Low_Relay"
      DefaultValue = "1"
      CANLowRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "BoschSensorPowerRelay"
      DefaultValue = "8"
      BoschSensorPowerRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestLockRelay"
      DefaultValue = "9"
      NestLockRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "ConnectorLockRelay"
      DefaultValue = "10"
      ConnectorLockRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "EngageConnectorPogoPinsRelay"
      DefaultValue = "11"
      EngageConnectorPogoPinsRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "RFIDTriggerRelay"
      DefaultValue = "12"
      RFIDTriggerRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "LINSensorPowerRelay"
      DefaultValue = "13"
      LINSensorPowerRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "ValeoSensorPowerRelay"
      DefaultValue = "14"
      ValeoSensorPowerRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "AutolivSensorPowerRelay"
      DefaultValue = "15"
      AutolivSensorPowerRelay = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "tsTime_Max"
      DefaultValue = "380"
      EchoLimits.tsTime_Max = Convert.ToDouble(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "tsTime_Min"
      DefaultValue = "250"
      EchoLimits.tsTime_Min = Convert.ToDouble(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "taTime_Max"
      DefaultValue = "1400"
      EchoLimits.taTime_Max = Convert.ToDouble(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "taTime_Min"
      DefaultValue = "800"
      EchoLimits.taTime_Min = Convert.ToDouble(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "SensorHarnessTestOnly"      'Key String To Look For
      DefaultValue = "False"             'Set DefaultValue Value in case variable key is not found
      SensorHarnessTestOnly = Convert.ToBoolean(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "StartRestartSwitchInputPort"
      DefaultValue = "1"
      StartRestartSwitchInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "StartRestartSwitchInputBank"
      DefaultValue = "0"
      StartRestartSwitchInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestLockReleaseInputPort"
      DefaultValue = "2"
      NestLockReleaseInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestLockReleaseInputBank"
      DefaultValue = "0"
      NestLockReleaseInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "ArtPlateHarnessConnectedInputPort"
      DefaultValue = "4"
      ArtPlateHarnessConnectedInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "ArtPlateHarnessConnectedInputBank"
      DefaultValue = "0"
      ArtPlateHarnessConnectedInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "CameraHarnessInputPort"
      DefaultValue = "8"
      CameraHarnessInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "CameraHarnessInputBank"
      DefaultValue = "0"
      CameraHarnessInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestInRotatedPositionInputPort"
      DefaultValue = "16"
      NestInRotatedPositionInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestInRotatedPositionInputBank"
      DefaultValue = "0"
      NestInRotatedPositionInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      'Input Port And Bank Assignments
      Key = "NestAtHomePositionInputPort"
      DefaultValue = "32"
      NestAtHomePositionInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "NestAtHomePositionInputBank"
      DefaultValue = "0"
      NestAtHomePositionInputBank = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      Key = "PartInPlaceInputPort"
      DefaultValue = "0"
      PartInPlaceInputPort = Convert.ToByte(ReadINIFile(Section, Key, DefaultValue, FileName))

      'PTS SENSOR VARIABLES
      Key = "Sensor1"
      DefaultValue = "1"
      Sensor1TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "Sensor2"
      DefaultValue = "2"
      Sensor2TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "Sensor3"
      DefaultValue = "3"
      Sensor3TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "Sensor4"
      DefaultValue = "4"
      Sensor4TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "Sensor5"
      DefaultValue = "5"
      Sensor5TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      Key = "Sensor6"
      DefaultValue = "6"
      Sensor6TestInabled = Convert.ToBoolean(Val(ReadINIFile(Section, Key, DefaultValue, FileName)))

      LoadGeneralVariables = True    'Pass Load Variables Routine

   End Function

    Sub SaveDataFileDateInformation()

        'This Routine Saves the DataLog File Information to the .ini file (this is done in case of power loss)

        Dim FileName As String

        FileName = DefaultIniPath & DefaultINIFileName     'Set name Of File To Search For Info.
        'Set name Of File To Search For Info.

        Section = "System"

        Key = "DayToChangeFile"         'Key String To Look For
        Support.WriteINIFile(Section, Key, strDayToChangeFile, FileName)

        Key = "FileCreationDay"         'Key String To Look For
        Support.WriteINIFile(Section, Key, strFileCreationDay, FileName)

        Key = "CurrentPassLogFileName"  'Key String To Look For
        Support.WriteINIFile(Section, Key, DataLogPassLabelFileName, FileName)

    End Sub

    Function LookUpInfo(ByVal SectionName As String, ByVal KeyToLookUp As String) As String

        'This Routine Loads All Part Information From The .ini File
        'Note FindValaidPartNumbers Subroutine Must Be Run Before This Routine Or This Routine Will Fail

        Dim FileName As String
        Dim DefaultValue As String = ""

        FileName = DefaultIniPath & DefaultTestInformationINIFileName                       'Set name Of File To Search For Info.
        If File.Exists(FileName) = False Then
            MsgBox("Default.ini file not found" & FileName, MsgBoxStyle.Critical, "LookUpInfo .ini ERROR")
            LookUpInfo = "Error"   'Fail Lookup Info
            Exit Function
        End If

        LookUpInfo = ReadINIFile(SectionName, KeyToLookUp, DefaultValue, FileName)

    End Function
   Function LookUpTBOXInfo(ByVal SectionName As String, ByVal KeyToLookUp As String) As String

      'This Routine Loads All Part Information From The .ini File
      'Note FindValaidPartNumbers Subroutine Must Be Run Before This Routine Or This Routine Will Fail

      Dim FileName As String
      Dim DefaultValue As String = ""

      FileName = DefaultIniPath & DefaultTBOXCommunicationINIFileName                       'Set name Of File To Search For Info.
      If File.Exists(FileName) = False Then
         MsgBox("TBOX.ini file not found" & FileName, MsgBoxStyle.Critical, "LookUpInfo .ini ERROR")
         LookUpTBOXInfo = "Error"   'Fail Lookup Info
         Exit Function
      End If

      LookUpTBOXInfo = ReadINIFile(SectionName, KeyToLookUp, DefaultValue, FileName)

   End Function
   Sub PrintPartLabel(ByVal TimeAndDateTestFailed As String, ByVal SequenceNumber As String, ByVal FailureString As String)
        Dim PrintString As String = ""
        Dim PrintString1 As String = ""
        Dim PrintString2 As String = ""
        Dim PrintString3 As String = ""

        Try
            Select Case FailureString.Length
                Case 1 To 48
                    PrintString = Mid(FailureString, 1, 48)
                    RawPrinterHelper.SendStringToPrinter(IntermecPrinterName,
                  "<STX><ESC>C;<ETX>" &
                  "<STX><ESC>P<ETX>" &
                  "<STX>E4;F4,FAIL;<ETX>" &
                  "<STX>H0;o1,780;f1;c0;h3;w2;d3,SENSOR TEST FAILED AT:" & TimeAndDateTestFailed & ";<ETX>" &
                  "<STX>H1;o40,780;f1;c0;h2;w2;d3," & PrintString & " ;<ETX>" &
                  "<STX>H2;o150,780;f1;c0;h2;w2;d3,Production Number " & SequenceNumber & ";<ETX>" &
                  "<STX>R;<ETX>" &
                  "<STX><ESC>E4<ETX><STX><CAN><ETX>" &
                  "<STX><ETB><ETX>")

                Case 49 To 98
                    PrintString = Mid(FailureString, 1, 48)
                    PrintString1 = Mid(FailureString, 49, 48)
                    RawPrinterHelper.SendStringToPrinter(IntermecPrinterName,
                  "<STX><ESC>C;<ETX>" &
                  "<STX><ESC>P<ETX>" &
                  "<STX>E4;F4,FAIL;<ETX>" &
                  "<STX>H0;o1,780;f1;c0;h3;w2;d3,SENSOR TEST FAILED AT:" & TimeAndDateTestFailed & ";<ETX>" &
                  "<STX>H1;o40,780;f1;c0;h2;w2;d3," & PrintString & " ;<ETX>" &
                  "<STX>H2;o65,780;f1;c0;h2;w2;d3," & PrintString1 & " ;<ETX>" &
                  "<STX>H3;o150,780;f1;c0;h2;w2;d3,Production Number " & SequenceNumber & ";<ETX>" &
                  "<STX>R;<ETX>" &
                  "<STX><ESC>E4<ETX><STX><CAN><ETX>" &
                  "<STX><ETB><ETX>")

                Case 99 To 148
                    PrintString = Mid(FailureString, 1, 48)
                    PrintString1 = Mid(FailureString, 49, 48)
                    PrintString2 = Mid(FailureString, 97, 48)
                    RawPrinterHelper.SendStringToPrinter(IntermecPrinterName,
                  "<STX><ESC>C;<ETX>" &
                  "<STX><ESC>P<ETX>" &
                  "<STX>E4;F4,FAIL;<ETX>" &
                  "<STX>H0;o1,780;f1;c0;h3;w2;d3,SENSOR TEST FAILED AT:" & TimeAndDateTestFailed & ";<ETX>" &
                  "<STX>H1;o40,780;f1;c0;h2;w2;d3," & PrintString & " ;<ETX>" &
                  "<STX>H2;o65,780;f1;c0;h2;w2;d3," & PrintString1 & " ;<ETX>" &
                  "<STX>H3;o90,780;f1;c0;h2;w2;d3," & PrintString2 & " ;<ETX>" &
                  "<STX>H4;o150,780;f1;c0;h2;w2;d3,Production Number " & SequenceNumber & ";<ETX>" &
                  "<STX>R;<ETX>" &
                  "<STX><ESC>E4<ETX><STX><CAN><ETX>" &
                  "<STX><ETB><ETX>")

                Case Else
                    PrintString = Mid(FailureString, 1, 48)
                    PrintString1 = Mid(FailureString, 49, 48)
                    PrintString2 = Mid(FailureString, 97, 48)
                    PrintString3 = Mid(FailureString, 149, 48)
                    RawPrinterHelper.SendStringToPrinter(IntermecPrinterName,
                  "<STX><ESC>C;<ETX>" &
                  "<STX><ESC>P<ETX>" &
                  "<STX>E4;F4,FAIL;<ETX>" &
                  "<STX>H0;o1,780;f1;c0;h3;w2;d3,SENSOR TEST FAILED AT:" & TimeAndDateTestFailed & ";<ETX>" &
                  "<STX>H1;o40,780;f1;c0;h2;w2;d3," & PrintString & " ;<ETX>" &
                  "<STX>H2;o65,780;f1;c0;h2;w2;d3," & PrintString1 & " ;<ETX>" &
                  "<STX>H3;o90,780;f1;c0;h2;w2;d3," & PrintString2 & " ;<ETX>" &
                  "<STX>H4;o115,780;f1;c0;h2;w2;d3," & PrintString3 & " ;<ETX>" &
                  "<STX>H5;o150,780;f1;c0;h2;w2;d3,Production Number " & SequenceNumber & ";<ETX>" &
                  "<STX>R;<ETX>" &
                  "<STX><ESC>E4<ETX><STX><CAN><ETX>" &
                  "<STX><ETB><ETX>")

            End Select
        Catch ex As Exception
            MsgBox("There was an Error Printing The Product Label" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Label Printing Error")
        End Try

    End Sub

    Sub InitPrinter()
        Try
            RawPrinterHelper.SendStringToPrinter(IntermecPrinterName,
         "<STX><ESC>C;<ETX>" &
         "<STX><ESC>P<ETX>" &
         "<STX><SI>R1<ETX>" &
         "<STX><SI>D200<ETX>" &
         "<STX>R;<ETX>")

        Catch ex As Exception
            MsgBox("There was an Error Printing The Product Label" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Label Printing Error")
        End Try
    End Sub

    Sub WritetoErrorLog(ByVal PassEx As Exception, ByVal RaiseStopError As Boolean, ByVal LogIt As Boolean, Optional ByVal AddInfo As String = "", Optional ByVal ShowPopUp As Boolean = False, Optional ByVal PopUpMsg As String = "")

        If ShowPopUp = True Then MessageBox.Show(PopUpMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        If LogIt = True Then
            Dim FileNameToOpen As String
            Dim TheSubDirectory As String
            Try
                TheSubDirectory = My.Settings.MainErrorLogPath & "Error Log\"
                FileNameToOpen = TheSubDirectory & "Error Log.txt"
                'Make each needed preliminary subdirectory if needed...
                If My.Computer.FileSystem.DirectoryExists(TheSubDirectory) = False Then My.Computer.FileSystem.CreateDirectory(TheSubDirectory)
                My.Computer.FileSystem.WriteAllText(FileNameToOpen, DateTime.Now.ToString & "," & "," & AddInfo & Microsoft.VisualBasic.ControlChars.CrLf & PassEx.ToString & Microsoft.VisualBasic.ControlChars.CrLf & Microsoft.VisualBasic.ControlChars.CrLf, True)

            Catch ex As Exception
                MessageBox.Show("ERROR WRITING TO ERROR LOG, NOT SURE WHAT TO DO ABOUT THIS!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.DoEvents()
         End Try
      End If
   End Sub

   Function LookUpLocationInfo(ByVal SectionName As String, ByVal FileName As String, ByVal KeyToLookUp As String) As String

      'This Routine Loads All Part Information From The .ini File
      'Note FindValaidPartNumbers Subroutine Must Be Run Before This Routine Or This Routine Will Fail

      Dim DefaultValue As String = ""

      If File.Exists(FileName) = False Then
         'MsgBox("Default.ini file not found" & FileName, MsgBoxStyle.Critical, "LookUpInfo .ini ERROR")
         LookUpLocationInfo = "Error"   'Fail Lookup Info
         Exit Function
      End If

      LookUpLocationInfo = ReadINIFile(SectionName, KeyToLookUp, DefaultValue, FileName)

   End Function

   Function BCD(bcdNumber As Byte) As Long
      Dim digit1 As Integer = bcdNumber >> 4
      Dim digit2 As Integer = bcdNumber And &HF
      BCD = (BCD * 100) + digit1 * 10 + digit2
   End Function
End Module
