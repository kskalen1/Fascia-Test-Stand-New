Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.String
Imports System.Text

Module TBET
   Private Declare Function SetEnvironmentVariable Lib "kernel32.dll" Alias "SetEnvironmentVariableA" (ByVal lpName As String, ByVal lpValue As String) As Integer

   'Old TBET
   Private Declare Function TBET_LoadConfigFile Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_LoadConfigFile@4" (ByVal cfgFile As String) As Integer
   Private Declare Function TBET_Testbumper Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_Testbumper@20" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal readOnlyTests As Integer, ByRef pnNumErrorCodes As Integer, ByRef perrorCode As Integer) As Integer
   Private Declare Function TBET_ErrorCodeToText Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_ErrorCodeToText@12" (ByVal errorCode As Integer, ByVal errorTextBuffer As Byte, ByVal bufferSize As Integer) As Integer
   Private Declare Function TBET_GetSensorMASR Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_GetSensorMASR@12" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal senAddress As Integer) As Integer
   Private Declare Function TBET_GetSensorSN Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_GetSensorSN@12" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal senAddress As Integer) As Integer
   Private Declare Function TBET_GetNumSensors Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_GetNumSensors@8" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer) As Integer
   Private Declare Function TBET_GetSensorVer Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\tbet.dll" Alias "_TBET_GetSensorVer@20" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal senAddress As Integer, ByRef pMajVer As Integer, ByRef pMinVer As Integer) As Integer

   'MXBET
   Private Declare Function BET_LoadConfigFile Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_LoadConfigFile@4" (ByVal cfgFile As String) As Integer
   Private Declare Function BET_Testbumper Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_Testbumper@20" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal readOnlyTests As Integer, ByRef pnNumErrorCodes As Integer, ByRef perrorCode As Integer) As Integer
   Private Declare Function BET_ErrorCodeToText Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_ErrorCodeToText@12" (ByVal errorCode As Integer, ByRef errorTextBuffer As String, ByRef bufferSize As Integer) As Integer
   Private Declare Function BET_GetNumSensors Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_GetNumSensors@0" (ByVal nPlatformID As Int16, ByVal nBumperType As Int16) As Int16
   Private Declare Function BET_GetSensorVer Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_GetSensorVer@16" (ByVal nPlatformID As Integer, ByVal senAddress As Integer, ByRef pMajVer As Integer, ByRef pMinVer As Integer) As Integer
   Private Declare Function BET_GetSensorInfo Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_GetSensorInfo@28" (ByVal nPlatformID As Integer, ByVal address As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal pProdNum As String, ByRef pYear As Integer, ByRef pWeek As Integer, ByRef pDay As Integer, ByRef pSerialNum As Integer) As Integer
   Private Declare Function BET_SetBumperType Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mxbet.dll" Alias "_BET_SetBumperType@20" (ByVal nPlatformCode As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal sChassisCode As StringBuilder, ByVal bAMG_BM As Integer, ByVal bAMG_SA As Integer, ByVal nColorCode As Integer) As Integer


   'MRBET
   Private Declare Function MRBET_LoadConfigFile Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_LoadConfigFile@4" (ByVal cfgFile As String) As Integer
   Private Declare Function MRBET_Testbumper Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_Testbumper@20" (ByVal nPlatformID As Integer, ByVal nBumperType As Integer, ByVal readOnlyTests As Integer, ByRef pnNumErrorCodes As Integer, ByRef perrorCode As Integer) As Integer
   Private Declare Function MRBET_ErrorCodeToText Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_ErrorCodeToText@12" (ByVal errorCode As Integer, ByRef errorTextBuffer As String, ByRef bufferSize As Integer) As Integer
   Private Declare Function MRBET_GetNumSensors Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_GetNumSensors@0" (ByVal nPlatformID As Int16, ByVal nBumperType As Int16) As Int16
   Private Declare Function MRBET_GetSensorVer Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_GetSensorVerExt@20" (ByVal nPlatformID As Int16, ByVal senAddress As Int16, ByRef pMajVer As Int16, ByRef pMinVer As Int16, ByRef psubVer As Int16) As Int16
   Private Declare Function MRBET_GetSensorInfo Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_GetSensorInfo@28" (ByVal nPlatformID As Integer, ByVal address As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal pProdNum As String, ByRef pYear As Integer, ByRef pWeek As Integer, ByRef pDay As Integer, ByRef pSerialNum As Integer) As Integer
   Private Declare Function MRBET_SetBumperType Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_SetBumperType@20" (ByVal nPlatformCode As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal sChassisCode As StringBuilder, ByVal bAMG_BM As Integer, ByVal bAMG_SA As Integer, ByVal nColorCode As Integer) As Integer
   Private Declare Function MRBET_WriteBumperProductNumber Lib "C:\Rehau Fascia Test Stand\Fascia Test Stand\dlls\mrbet.dll" Alias "_BET_WriteBumperProductNumber@4" (<MarshalAs(UnmanagedType.LPStr)> ByVal pProdNumber As String())

   '_BET_GetZBANumber@12	   
   '_BET_FastPing@8	
   '_BET_SetProgressCallback@8	


   Public ChassisCode As New StringBuilder
   Public SA_Chassis_Selected As Integer
   Public BM_Chassis_Selected As Integer
   Public Basic_Chassis_Selected As Integer
   Public SensorFailed As Boolean
   Public PlatformIndex(200) As Integer

   Const TBET_MAX_ERRORS = (22)

   Const SEN_ERR_MASK = &HFFFFFF
   Const SEN_MASK = &HF000000
   Const MAX_BUMPER_SENSORS = 4


    Function LoadTbetConfigurationFile(ByVal TbetConfigurationFile As String) As Boolean
        'Dim cfgFile As String
        Dim retValue As Integer
        Dim errorArray(0 To TBET_MAX_ERRORS) As Integer
        Dim errorTextBuffer(0 To 500) As Byte

      'First load the configuration file
        retValue = 0
      Select Case WhichDll
         Case "MRBET"
            retValue = MRBET_LoadConfigFile(DefaultTBETConfigurationFilePath & TbetConfigurationFile)
         Case "MXBET"
            retValue = BET_LoadConfigFile(DefaultTBETConfigurationFilePath & TbetConfigurationFile)
         Case "BET"
            retValue = TBET_LoadConfigFile(DefaultTBETConfigurationFilePath & TbetConfigurationFile)
      End Select

      frmMain.textbox.Text = ""
      If retValue = 0 Then
         frmMain.indTbetConfigurationFile.BackColor = Color.Red
         LoadTbetConfigurationFile = False
         frmMain.lblTbetConfigurationFile.Text = ""
      Else
         frmMain.indTbetConfigurationFile.BackColor = Color.Green
         frmMain.lblTbetConfigurationFile.Text = TbetConfigurationFile
         LoadTbetConfigurationFile = True
      End If

      'load the platform names for the configuration file'
      frmMain.platformCB.Items.Clear()

      Dim PlatformString As String = ""
      Dim IndexNumber As Short = 0
      For Loops As Integer = 1 To 200      'PLATFORMS
         PlatformString = LookUpPlatForms("PLATFORMS", "PLATFORM" & Loops.ToString, TbetConfigurationFile)
         If PlatformString <> "" Then
            frmMain.platformCB.Items.Add(LookUpPlatForms("PLATFORMS", "PLATFORM" & Loops.ToString, TbetConfigurationFile))
            PlatformIndex(IndexNumber) = Loops
            IndexNumber = IndexNumber + 1
         End If
      Next Loops

      frmMain.platformCB.SelectedIndex = 1

   End Function

   Sub TestBumper(ByVal platform As Integer, ByVal bumper As Integer, ByVal DisplayMode As TestDisplayModes, ByRef TestResult As String)
      'Dim cfgFile As String
      Dim retValue As Integer = 0
      Dim retValue2 As Integer
      Dim errorArray(0 To 50) As Integer
      Dim errorTextBuffer(0 To 499) As String
      Dim numErrors As Integer
      Dim majVer As Short
      Dim minVer As Short
      Dim subverison As ULong
      Dim lProdNumber As String = ""
      Dim lProdYear As Integer = 0
      Dim lProdWeek As Integer = 0
      Dim lProdDay As Integer = 0
      Dim lSerialNumber As Integer = 0
      Dim SavedPlatformName As String = ""
      Dim iPlatformToTest As SqlInt16 = 166

      'configuration file must be loaded first (done from form load routine  (frmMain)

      Select Case DisplayMode
         Case TestDisplayModes.AutomaticMode
            Try
               Select Case PlatformToTest
                  Case Is = "W205"
                     iPlatformToTest = 205
                  Case Else
                     iPlatformToTest = 205
               End Select

               Select Case WhichDll
                  Case "MRBET"
                     retValue = MRBET_SetBumperType(iPlatformToTest, ChassisCode, BM_Chassis_Selected, SA_Chassis_Selected, PaintColorCode)
                        'PlatformToTest = SavedPlatformName
                  Case "MXBET"
                     retValue = BET_SetBumperType(iPlatformToTest, ChassisCode, BM_Chassis_Selected, SA_Chassis_Selected, PaintColorCode)
                     'PlatformToTest = SavedPlatformName
                  Case Else

               End Select

            Catch ex As Exception
               ' Error handling
               MessageBox.Show("Set Bumper Type Failed With exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
               AbortTesting = True
               Exit Sub
            End Try

            Try
               Select Case WhichDll
                  Case "MRBET"
                     retValue = MRBET_Testbumper(platform, bumper, 0, numErrors, errorArray(0))
                  Case "MXBET"
                     retValue = BET_Testbumper(platform, bumper, 0, numErrors, errorArray(0))

                  Case Else

               End Select


            Catch ex As Exception
               ' Error handling
               MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
               AbortTesting = True
               'exit sub
            End Try

            'if the result was positive set the background color to green
            'otherwise set it to red and list all the errors


            Select Case WhichDll
               Case "MRBET"
                  If retValue = 1 Then
                     TestResult = "P"
                     Try
                        'Regardless of the result, try to obtain the sensor SN
                        For index = 1 To MAX_BUMPER_SENSORS
                           retValue = MRBET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                           tbetSensor(index).lProdNumber = lProdNumber
                           tbetSensor(index).lProdYear = lProdYear
                           tbetSensor(index).lProdWeek = lProdWeek
                           tbetSensor(index).lProdDay = lProdDay
                           tbetSensor(index).SerialNum = lSerialNumber.ToString
                           tbetSensor(index).lSerialNumber = lSerialNumber
                           retValue2 = MRBET_GetSensorVer(platform, index, majVer, minVer, subverison)
                           tbetSensor(index).majVersion = majVer
                           tbetSensor(index).minVersion = minVer
                           tbetSensor(index).subVersion = subverison
                        Next index
                     Catch ex As Exception
                        ' Error handling
                        'MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                        Exit Sub
                     End Try

                  Else
                     TestResult = "F"
                     Dim errorarrayIndex As Integer = 0
                     Dim ErrorString As String = ""
                     tbetSensor(0).errorString = ""
                     tbetSensor(1).errorString = ""
                     tbetSensor(2).errorString = ""
                     tbetSensor(3).errorString = ""
                     tbetSensor(4).errorString = ""
                     tbetSensor(0).errorCodes = ""
                     tbetSensor(1).errorCodes = ""
                     tbetSensor(2).errorCodes = ""
                     tbetSensor(3).errorCodes = ""
                     tbetSensor(4).errorCodes = ""

                     For index = 0 To numErrors - 1
                        ErrorString = ""
                        ErrorString = ErrorCodeToText(errorArray(index))
                        If ErrorString.Contains("#1") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For
                        ElseIf ErrorString.Contains("#1") And ErrorString.Contains("#2") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#2") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#3") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#1") And ErrorString.Contains("#2") And ErrorString.Contains("#3") Then
                           'If Three Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(3).errorString = tbetSensor(3).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#1") Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & ErrorString & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#2") Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & ErrorString & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#3") Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & ErrorString & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#4") Then
                           tbetSensor(4).errorString = tbetSensor(4).errorString & ErrorString & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        Else
                           'If Not a Sensor Error But System Type Error Then Set error String in array index zero
                           tbetSensor(0).errorString = tbetSensor(0).errorString & ErrorString
                           tbetSensor(0).errorCodes = tbetSensor(0).errorCodes & SEN_ERR_MASK And errorArray(index) & ","
                        End If
                     Next


                     'Regardless of the result, try to obtain the sensor SN
                     Try
                        For index = 1 To MAX_BUMPER_SENSORS
                           retValue = MRBET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                           tbetSensor(index).SerialNum = lSerialNumber.ToString
                           tbetSensor(index).lSerialNumber = lSerialNumber
                           tbetSensor(index).lProdNumber = lProdNumber
                           tbetSensor(index).lProdYear = lProdYear
                           tbetSensor(index).lProdWeek = lProdWeek
                           tbetSensor(index).lProdDay = lProdDay
                           retValue2 = MRBET_GetSensorVer(platform, index, majVer, minVer, subverison)
                           tbetSensor(index).majVersion = majVer
                           tbetSensor(index).minVersion = minVer
                           tbetSensor(index).subVersion = subverison
                        Next index
                     Catch ex As Exception
                        ' Error handling
                        '        MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                        Exit Sub
                     End Try

                  End If

               Case "MXBET"
                  If retValue = 1 Then
                     TestResult = "P"
                     Try
                        'Regardless of the result, try to obtain the sensor SN
                        For index = 1 To MAX_BUMPER_SENSORS
                           retValue = BET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                           tbetSensor(index).lProdNumber = lProdNumber
                           tbetSensor(index).lProdYear = lProdYear
                           tbetSensor(index).lProdWeek = lProdWeek
                           tbetSensor(index).lProdDay = lProdDay
                           tbetSensor(index).SerialNum = lSerialNumber.ToString
                           tbetSensor(index).lSerialNumber = lSerialNumber
                           retValue2 = BET_GetSensorVer(platform, index, majVer, minVer)
                           tbetSensor(index).majVersion = majVer
                           tbetSensor(index).minVersion = minVer
                        Next index
                     Catch ex As Exception
                        ' Error handling
                        'MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                        Exit Sub
                     End Try

                  Else
                     TestResult = "F"
                     Dim errorarrayIndex As Integer = 0
                     Dim ErrorString As String = ""
                     tbetSensor(0).errorString = ""
                     tbetSensor(1).errorString = ""
                     tbetSensor(2).errorString = ""
                     tbetSensor(3).errorString = ""
                     tbetSensor(4).errorString = ""
                     tbetSensor(0).errorCodes = ""
                     tbetSensor(1).errorCodes = ""
                     tbetSensor(2).errorCodes = ""
                     tbetSensor(3).errorCodes = ""
                     tbetSensor(4).errorCodes = ""

                     For index = 0 To numErrors - 1
                        ErrorString = ""
                        ErrorString = ErrorCodeToText(errorArray(index))
                        If ErrorString.Contains("#1") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For
                        ElseIf ErrorString.Contains("#1") And ErrorString.Contains("#2") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#2") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#3") And ErrorString.Contains("#4") Then
                           'If Two Errors Are Returned In 1 String Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(4).errorString = tbetSensor(4).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#1") And ErrorString.Contains("#2") And ErrorString.Contains("#3") Then
                           'If Three Errors Are Returned In 1 String Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(2).errorString = tbetSensor(2).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                           tbetSensor(3).errorString = tbetSensor(3).errorString & SplitString(ErrorString, "-") & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                           Exit For

                        ElseIf ErrorString.Contains("#1") Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & ErrorString & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#2") Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & ErrorString & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#3") Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & ErrorString & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        ElseIf ErrorString.Contains("#4") Then
                           tbetSensor(4).errorString = tbetSensor(4).errorString & ErrorString & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","

                        Else
                           'If Not a Sensor Error But System Type Error Then Set error String in array index zero
                           tbetSensor(0).errorString = tbetSensor(0).errorString & ErrorString
                           tbetSensor(0).errorCodes = tbetSensor(0).errorCodes & SEN_ERR_MASK And errorArray(index) & ","
                        End If
                     Next


                     'Regardless of the result, try to obtain the sensor SN
                     Try
                        For index = 1 To MAX_BUMPER_SENSORS
                           retValue = BET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                           tbetSensor(index).SerialNum = lSerialNumber.ToString
                           tbetSensor(index).lSerialNumber = lSerialNumber
                           tbetSensor(index).lProdNumber = lProdNumber
                           tbetSensor(index).lProdYear = lProdYear
                           tbetSensor(index).lProdWeek = lProdWeek
                           tbetSensor(index).lProdDay = lProdDay
                           retValue2 = BET_GetSensorVer(platform, index, majVer, minVer)
                           tbetSensor(index).majVersion = majVer
                           tbetSensor(index).minVersion = minVer
                        Next index
                     Catch ex As Exception
                        ' Error handling
                        '        MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                        Exit Sub
                     End Try

                  End If

               Case Else

            End Select


         Case TestDisplayModes.ManualMode
            'Clear Out Previous Measurements
            CleartbetSensorInfo()

            frmMain.lblTbetTestResult.Text = "Test In Progress Please Wait (Test Can Take 30 to 40 Seconds To Complete)"
            frmMain.lblTbetTestResult.BackColor = Color.GreenYellow
            frmMain.Refresh()

            Select Case WhichDll
               Case "MRBET"
                  Try
                     Debug.Print(ChassisCode.ToString)
                     Select Case PlatformToTest
                        Case Is = "W205"
                           iPlatformToTest = 205
                        Case Else
                           iPlatformToTest = 205
                     End Select
                     retValue = MRBET_SetBumperType(iPlatformToTest, ChassisCode, BM_Chassis_Selected, SA_Chassis_Selected, PaintColorCode)
                  Catch ex As Exception
                     ' Error handling
                     MessageBox.Show("Set Bumper Type Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                     Exit Sub
                  End Try

                  Try
                     retValue = MRBET_Testbumper(platform, bumper, 0, numErrors, errorArray(0))

                  Catch ex As Exception
                     ' Error handling
                     MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                     'exit sub
                  End Try

                  'if the result was positive set the background color to green
                  'otherwise set it to red and list all the errors
                  If retValue = 1 Then
                     frmMain.textbox.BackColor = Color.Lime
                     frmMain.textbox.Text = frmMain.textbox.Text & "Testing of Platform: " & frmMain.platformCB.SelectedItem.ToString & " On: " & frmMain.bumperCB.SelectedItem.ToString & " sensors was successful!" & vbCrLf
                     frmMain.lblTbetTestResult.BackColor = Color.Lime
                     frmMain.lblTbetTestResult.Text = "P"

                     'Show how many sensors were detected
                     'Dim NumberOfSensors As Int16 = BET_GetNumSensors(platform, bumper)
                     'frmMain.textbox.Text = frmMain.textbox.Text + "Bumper has " + NumberOfSensors.ToString + " sensors"
                     'Run the test bumper
                     SensorFailed = False
                  Else
                     frmMain.lblTbetTestResult.BackColor = Color.Red
                     frmMain.lblTbetTestResult.Text = "F"
                     frmMain.textbox.BackColor = Color.Red

                     'Display every error on a separate line
                     Dim errorarrayIndex As Integer = 0
                     Dim ErrorString As String = ""
                     tbetSensor(0).errorString = ""
                     tbetSensor(1).errorString = ""
                     tbetSensor(2).errorString = ""
                     tbetSensor(3).errorString = ""
                     tbetSensor(4).errorString = ""
                     tbetSensor(0).errorCodes = ""
                     tbetSensor(1).errorCodes = ""
                     tbetSensor(2).errorCodes = ""
                     tbetSensor(3).errorCodes = ""
                     tbetSensor(4).errorCodes = ""
                     SensorFailed = True
                     For index = 0 To numErrors - 1
                        ErrorString = ""
                        ErrorString = ErrorCodeToText(errorArray(index))
                        If ErrorString.Contains("#1") Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & ErrorString & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#2") Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & ErrorString & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#3") Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & ErrorString & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#4") Then
                           tbetSensor(4).errorString = tbetSensor(4).errorString & ErrorString & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        Else
                           'If Not a Sensor Error But System Type Error Then Set error String in array index zero
                           tbetSensor(0).errorString = tbetSensor(0).errorString & ErrorString
                           tbetSensor(0).errorCodes = tbetSensor(0).errorCodes & SEN_ERR_MASK And errorArray(index) & ","
                        End If
                     Next

                     For index = 0 To numErrors - 1
                        frmMain.textbox.Text = frmMain.textbox.Text + ErrorCodeToText(errorArray(index)) + vbCrLf
                     Next
                  End If

                  'Regardless of the result, try to obtain the sensor SN
                  For index = 1 To MAX_BUMPER_SENSORS
                     retValue = MRBET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                     tbetSensor(index).SerialNum = lSerialNumber.ToString
                     tbetSensor(index).lSerialNumber = lSerialNumber
                     tbetSensor(index).lProdNumber = lProdNumber
                     tbetSensor(index).lProdYear = lProdYear
                     tbetSensor(index).lProdWeek = lProdWeek
                     tbetSensor(index).lProdDay = lProdDay
                     retValue2 = MRBET_GetSensorVer(platform, index, majVer, minVer, subverison)
                     tbetSensor(index).majVersion = majVer
                     tbetSensor(index).minVersion = minVer
                     tbetSensor(index).subVersion = subverison
                     Select Case index
                        Case Is = 1
                           If retValue <> 0 Then
                              'Show Results For Sensor 1 on screen
                              frmMain.lbltbetSerialNumSensor1.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor1.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor1.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetSubVersionSensor1.Text = CStr(tbetSensor(index).subVersion)
                              frmMain.lbltbetmasrNumSensor1.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 2
                           If retValue <> 0 Then
                              'Show Results For Sensor 2 on screen
                              frmMain.lbltbetSerialNumSensor2.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor2.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor2.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetSubVersionSensor2.Text = CStr(tbetSensor(index).subVersion)
                              frmMain.lbltbetmasrNumSensor2.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 3
                           If retValue <> 0 Then
                              'Show Results For Sensor 3 on screen
                              frmMain.lbltbetSerialNumSensor3.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor3.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor3.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetSubVersionSensor3.Text = CStr(tbetSensor(index).subVersion)
                              frmMain.lbltbetmasrNumSensor3.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 4
                           If retValue <> 0 Then
                              'Show Results For Sensor 4 on screen
                              frmMain.lbltbetSerialNumSensor4.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor4.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor4.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetSubVersionSensor4.Text = CStr(tbetSensor(index).subVersion)
                              frmMain.lbltbetmasrNumSensor4.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Else

                     End Select

                  Next index


               Case "MXBET"
                  Try
                     Debug.Print(ChassisCode.ToString)
                     Select Case PlatformToTest
                        Case Is = "W166MP"
                           iPlatformToTest = 166
                        Case Is = "C292"
                           iPlatformToTest = 292
                        Case Else
                           iPlatformToTest = 166
                     End Select
                     retValue = BET_SetBumperType(iPlatformToTest, ChassisCode, BM_Chassis_Selected, SA_Chassis_Selected, PaintColorCode)
                  Catch ex As Exception
                     ' Error handling
                     MessageBox.Show("Set Bumper Type Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                     Exit Sub
                  End Try

                  Try
                     retValue = BET_Testbumper(platform, bumper, 0, numErrors, errorArray(0))

                  Catch ex As Exception
                     ' Error handling
                     MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                     'exit sub
                  End Try

                  'if the result was positive set the background color to green
                  'otherwise set it to red and list all the errors
                  If retValue = 1 Then
                     frmMain.textbox.BackColor = Color.Lime
                     frmMain.textbox.Text = frmMain.textbox.Text & "Testing of Platform: " & frmMain.platformCB.SelectedItem.ToString & " On: " & frmMain.bumperCB.SelectedItem.ToString & " sensors was successful!" & vbCrLf
                     frmMain.lblTbetTestResult.BackColor = Color.Lime
                     frmMain.lblTbetTestResult.Text = "P"

                     'Show how many sensors were detected
                     'Dim NumberOfSensors As Int16 = BET_GetNumSensors(platform, bumper)
                     'frmMain.textbox.Text = frmMain.textbox.Text + "Bumper has " + NumberOfSensors.ToString + " sensors"
                     'Run the test bumper
                     SensorFailed = False
                  Else
                     frmMain.lblTbetTestResult.BackColor = Color.Red
                     frmMain.lblTbetTestResult.Text = "F"
                     frmMain.textbox.BackColor = Color.Red

                     'Display every error on a separate line
                     Dim errorarrayIndex As Integer = 0
                     Dim ErrorString As String = ""
                     tbetSensor(0).errorString = ""
                     tbetSensor(1).errorString = ""
                     tbetSensor(2).errorString = ""
                     tbetSensor(3).errorString = ""
                     tbetSensor(4).errorString = ""
                     tbetSensor(0).errorCodes = ""
                     tbetSensor(1).errorCodes = ""
                     tbetSensor(2).errorCodes = ""
                     tbetSensor(3).errorCodes = ""
                     tbetSensor(4).errorCodes = ""
                     SensorFailed = True
                     For index = 0 To numErrors - 1
                        ErrorString = ""
                        ErrorString = ErrorCodeToText(errorArray(index))
                        If ErrorString.Contains("#1") Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & ErrorString & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#2") Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & ErrorString & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#3") Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & ErrorString & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#4") Then
                           tbetSensor(4).errorString = tbetSensor(4).errorString & ErrorString & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        Else
                           'If Not a Sensor Error But System Type Error Then Set error String in array index zero
                           tbetSensor(0).errorString = tbetSensor(0).errorString & ErrorString
                           tbetSensor(0).errorCodes = tbetSensor(0).errorCodes & SEN_ERR_MASK And errorArray(index) & ","
                        End If
                     Next

                     For index = 0 To numErrors - 1
                        frmMain.textbox.Text = frmMain.textbox.Text + ErrorCodeToText(errorArray(index)) + vbCrLf
                     Next
                  End If

                  'Regardless of the result, try to obtain the sensor SN
                  For index = 1 To MAX_BUMPER_SENSORS
                     retValue = BET_GetSensorInfo(platform, index, lProdNumber, lProdYear, lProdWeek, lProdDay, lSerialNumber)
                     tbetSensor(index).SerialNum = lSerialNumber.ToString
                     tbetSensor(index).lSerialNumber = lSerialNumber
                     tbetSensor(index).lProdNumber = lProdNumber
                     tbetSensor(index).lProdYear = lProdYear
                     tbetSensor(index).lProdWeek = lProdWeek
                     tbetSensor(index).lProdDay = lProdDay
                     retValue2 = BET_GetSensorVer(platform, index, majVer, minVer)
                     tbetSensor(index).majVersion = majVer
                     tbetSensor(index).minVersion = minVer

                     Select Case index
                        Case Is = 1
                           If retValue <> 0 Then
                              'Show Results For Sensor 1 on screen
                              frmMain.lbltbetSerialNumSensor1.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor1.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor1.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetmasrNumSensor1.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 2
                           If retValue <> 0 Then
                              'Show Results For Sensor 2 on screen
                              frmMain.lbltbetSerialNumSensor2.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor2.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor2.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetmasrNumSensor2.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 3
                           If retValue <> 0 Then
                              'Show Results For Sensor 3 on screen
                              frmMain.lbltbetSerialNumSensor3.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor3.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor3.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetmasrNumSensor3.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Is = 4
                           If retValue <> 0 Then
                              'Show Results For Sensor 4 on screen
                              frmMain.lbltbetSerialNumSensor4.Text = tbetSensor(index).SerialNum
                              frmMain.lbltbetmajVersionSensor4.Text = CStr(tbetSensor(index).majVersion)
                              frmMain.lbltbetminVersionSensor4.Text = CStr(tbetSensor(index).minVersion)
                              frmMain.lbltbetmasrNumSensor4.Text = "Product # " & CStr(tbetSensor(index).lProdNumber) & " " & "DC " & CStr(tbetSensor(index).lProdYear) & "W" & CStr(tbetSensor(index).lProdWeek) & "D" & CStr(tbetSensor(index).lProdDay)
                           End If
                        Case Else

                     End Select

                  Next index

               Case Else
                  Try
                     retValue = TBET_Testbumper(platform, bumper, 0, numErrors, errorArray(0))
                  Catch ex As Exception
                     ' Error handling
                     MessageBox.Show("Testbumper Failed with exception: " + ex.Message, "TestBumper Routine Exception", MessageBoxButtons.OK)
                     'exit sub
                  End Try

                  'if the result was positive set the background color to green
                  'otherwise set it to red and list all the errors
                  If retValue = 1 Then
                     frmMain.textbox.BackColor = Color.Lime
                     frmMain.textbox.Text = frmMain.textbox.Text & "Testing of Platform: " & frmMain.platformCB.SelectedItem.ToString & " On: " & frmMain.bumperCB.SelectedItem.ToString & " with " & CStr(TBET_GetNumSensors(platform, bumper)) & " sensors was successful!" & vbCrLf
                     frmMain.lblTbetTestResult.BackColor = Color.Lime
                     frmMain.lblTbetTestResult.Text = "P"
                     SensorFailed = False

                     'Regardless of the result, try to obtain the sensor SN
                     For index = 1 To MAX_BUMPER_SENSORS
                        tbetSensor(index).masrNum = TBET_GetSensorMASR(platform, bumper, index)
                        tbetSensor(index).SerialNum = TBET_GetSensorSN(platform, bumper, index)
                        retValue2 = TBET_GetSensorVer(platform, bumper, index, majVer, minVer)
                        tbetSensor(index).majVersion = majVer
                        tbetSensor(index).minVersion = minVer
                     Next index
                     'Show Bumper Sensor Info On Screen
                     For index = 1 To MAX_BUMPER_SENSORS
                        Select Case index

                           Case Is = 1
                              Select Case tbetSensor(index).masrNum
                                 Case Is <> -1
                                    'Show Results Of Sensor 1 on screen
                                    frmMain.lbltbetmasrNumSensor1.Text = CStr(tbetSensor(1).masrNum)
                                    frmMain.lbltbetSerialNumSensor1.Text = CStr(tbetSensor(1).SerialNum)
                                    frmMain.lbltbetmajVersionSensor1.Text = CStr(tbetSensor(1).majVersion)
                                    frmMain.lbltbetminVersionSensor1.Text = CStr(tbetSensor(1).minVersion)
                                 Case Else
                              End Select
                           Case Is = 2
                              Select Case tbetSensor(index).masrNum
                                 Case Is <> -1
                                    'Show Results Of Sensor 2 on screen
                                    frmMain.lbltbetmasrNumSensor2.Text = CStr(tbetSensor(2).masrNum)
                                    frmMain.lbltbetSerialNumSensor2.Text = CStr(tbetSensor(2).SerialNum)
                                    frmMain.lbltbetmajVersionSensor2.Text = CStr(tbetSensor(2).majVersion)
                                    frmMain.lbltbetminVersionSensor2.Text = CStr(tbetSensor(2).minVersion)
                                 Case Else
                              End Select
                           Case Is = 3
                              Select Case tbetSensor(index).masrNum
                                 Case Is <> -1
                                    'Show Results Of Sensor 3 on screen
                                    frmMain.lbltbetmasrNumSensor3.Text = CStr(tbetSensor(3).masrNum)
                                    frmMain.lbltbetSerialNumSensor3.Text = CStr(tbetSensor(3).SerialNum)
                                    frmMain.lbltbetmajVersionSensor3.Text = CStr(tbetSensor(3).majVersion)
                                    frmMain.lbltbetminVersionSensor3.Text = CStr(tbetSensor(3).minVersion)
                                 Case Else
                              End Select

                           Case Is = 4
                              Select Case tbetSensor(index).masrNum
                                 Case Is <> -1
                                    'Show Results Of Sensor 4 on screen
                                    frmMain.lbltbetmasrNumSensor4.Text = CStr(tbetSensor(4).masrNum)
                                    frmMain.lbltbetSerialNumSensor4.Text = CStr(tbetSensor(4).SerialNum)
                                    frmMain.lbltbetmajVersionSensor4.Text = CStr(tbetSensor(4).majVersion)
                                    frmMain.lbltbetminVersionSensor4.Text = CStr(tbetSensor(4).minVersion)
                                 Case Else
                              End Select

                        End Select
                     Next
                     'Show how many sensors were detected
                     frmMain.textbox.Text = frmMain.textbox.Text + "Bumper has " + CStr(TBET_GetNumSensors(platform, bumper)) + " sensors"
                     'Run the test bumper

                  Else
                     frmMain.lblTbetTestResult.BackColor = Color.Red
                     frmMain.lblTbetTestResult.Text = "F"
                     frmMain.textbox.BackColor = Color.Red
                     'Display every error on a separate line
                     frmMain.lblTbetTestResult.BackColor = Color.Red
                     frmMain.lblTbetTestResult.Text = "F"
                     frmMain.textbox.BackColor = Color.Red
                     SensorFailed = True

                     'Display every error on a separate line
                     Dim errorarrayIndex As Integer = 0
                     Dim ErrorString As String = ""
                     tbetSensor(0).errorString = ""
                     tbetSensor(1).errorString = ""
                     tbetSensor(2).errorString = ""
                     tbetSensor(3).errorString = ""
                     tbetSensor(4).errorString = ""
                     tbetSensor(0).errorCodes = ""
                     tbetSensor(1).errorCodes = ""
                     tbetSensor(2).errorCodes = ""
                     tbetSensor(3).errorCodes = ""
                     tbetSensor(4).errorCodes = ""

                     For index = 0 To numErrors - 1
                        ErrorString = ""
                        ErrorString = ErrorCodeToText(errorArray(index))
                        If ErrorString.Contains("#1") Then
                           tbetSensor(1).errorString = tbetSensor(1).errorString & ErrorString & "_"
                           tbetSensor(1).errorCodes = tbetSensor(1).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#2") Then
                           tbetSensor(2).errorString = tbetSensor(2).errorString & ErrorString & "_"
                           tbetSensor(2).errorCodes = tbetSensor(2).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#3") Then
                           tbetSensor(3).errorString = tbetSensor(3).errorString & ErrorString & "_"
                           tbetSensor(3).errorCodes = tbetSensor(3).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        ElseIf ErrorString.Contains("#4") Then
                           tbetSensor(4).errorString = tbetSensor(4).errorString & ErrorString & "_"
                           tbetSensor(4).errorCodes = tbetSensor(4).errorCodes & (SEN_ERR_MASK And errorArray(index)).ToString & ","
                        Else
                           'If Not a Sensor Error But System Type Error Then Set error String in array index zero
                           tbetSensor(0).errorString = tbetSensor(0).errorString & ErrorString
                           tbetSensor(0).errorCodes = tbetSensor(0).errorCodes & SEN_ERR_MASK And errorArray(index) & ","
                        End If
                     Next

                     For index = 0 To numErrors - 1
                        frmMain.textbox.Text = frmMain.textbox.Text + ErrorCodeToText(errorArray(index)) + vbCrLf
                     Next
                  End If
                  'Regardless of the result, try to obtain the sensor SN
                  For index = 1 To MAX_BUMPER_SENSORS
                     tbetSensor(index).masrNum = TBET_GetSensorMASR(platform, bumper, index)
                     tbetSensor(index).SerialNum = TBET_GetSensorSN(platform, bumper, index)
                     retValue2 = TBET_GetSensorVer(platform, bumper, index, majVer, minVer)
                     tbetSensor(index).majVersion = majVer
                     tbetSensor(index).minVersion = minVer
                  Next index
                  'Show Bumper Sensor Info On Screen
                  For index = 1 To MAX_BUMPER_SENSORS
                     Select Case index

                        Case Is = 1
                           Select Case tbetSensor(index).masrNum
                              Case Is <> -1
                                 'Show Results Of Sensor 1 on screen
                                 frmMain.lbltbetmasrNumSensor1.Text = CStr(tbetSensor(1).masrNum)
                                 frmMain.lbltbetSerialNumSensor1.Text = CStr(tbetSensor(1).SerialNum)
                                 frmMain.lbltbetmajVersionSensor1.Text = CStr(tbetSensor(1).majVersion)
                                 frmMain.lbltbetminVersionSensor1.Text = CStr(tbetSensor(1).minVersion)
                              Case Else
                           End Select
                        Case Is = 2
                           Select Case tbetSensor(index).masrNum
                              Case Is <> -1
                                 'Show Results Of Sensor 2 on screen
                                 frmMain.lbltbetmasrNumSensor2.Text = CStr(tbetSensor(2).masrNum)
                                 frmMain.lbltbetSerialNumSensor2.Text = CStr(tbetSensor(2).SerialNum)
                                 frmMain.lbltbetmajVersionSensor2.Text = CStr(tbetSensor(2).majVersion)
                                 frmMain.lbltbetminVersionSensor2.Text = CStr(tbetSensor(2).minVersion)
                              Case Else
                           End Select
                        Case Is = 3
                           Select Case tbetSensor(index).masrNum
                              Case Is <> -1
                                 'Show Results Of Sensor 3 on screen
                                 frmMain.lbltbetmasrNumSensor3.Text = CStr(tbetSensor(3).masrNum)
                                 frmMain.lbltbetSerialNumSensor3.Text = CStr(tbetSensor(3).SerialNum)
                                 frmMain.lbltbetmajVersionSensor3.Text = CStr(tbetSensor(3).majVersion)
                                 frmMain.lbltbetminVersionSensor3.Text = CStr(tbetSensor(3).minVersion)
                              Case Else
                           End Select

                        Case Is = 4
                           Select Case tbetSensor(index).masrNum
                              Case Is <> -1
                                 'Show Results Of Sensor 4 on screen
                                 frmMain.lbltbetmasrNumSensor4.Text = CStr(tbetSensor(4).masrNum)
                                 frmMain.lbltbetSerialNumSensor4.Text = CStr(tbetSensor(4).SerialNum)
                                 frmMain.lbltbetmajVersionSensor4.Text = CStr(tbetSensor(4).majVersion)
                                 frmMain.lbltbetminVersionSensor4.Text = CStr(tbetSensor(4).minVersion)
                              Case Else
                           End Select

                     End Select
                  Next

            End Select


      End Select

   End Sub
   Sub TbetInitialize()
      'Dim PlatformNames As Variant
      Dim Bumpers(1) As Object
      Dim index As Integer

      Bumpers(0) = "Front Bumper"
      Bumpers(1) = "Rear Bumper"

      For index = LBound(Bumpers) To UBound(Bumpers)
         frmMain.bumperCB.Items.Add(Bumpers(index))
      Next
      frmMain.bumperCB.SelectedIndex = 0

      frmMain.chassisCodeCB.Items.Add("W")
      frmMain.chassisCodeCB.Items.Add("V")
      frmMain.chassisCodeCB.Items.Add("C")
      frmMain.chassisCodeCB.Items.Add("S")
      frmMain.chassisCodeCB.Items.Add("A")
      frmMain.chassisCodeCB.Items.Add("R")
      frmMain.chassisCodeCB.Items.Add("CL")
      frmMain.chassisCodeCB.Items.Add("VV")
      frmMain.chassisCodeCB.Items.Add("VF")
      frmMain.chassisCodeCB.Items.Add("G")
      frmMain.chassisCodeCB.Items.Add("GV")
      frmMain.chassisCodeCB.Items.Add("T")
      frmMain.chassisCodeCB.Items.Add("X")
      frmMain.chassisCodeCB.SelectedIndex = 0

      'Check To Make Sure The Windows Enviroment Variable "Path" has the Default TBET Configuration Path In It
      'If It Is Not In The "Path" Statement Add It
      CheckTbetPath(DefaultTBETConfigurationFilePath)
   End Sub

   Function ErrorCodeToText(ByVal errorCode As Integer) As String

      Select Case WhichDll
         Case True
            'Use this set of error codes for the New Mxbet.dll
            Select Case (SEN_ERR_MASK And errorCode)
               Case 1
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The CAN hardware or the driver is missing!")
               Case 2
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The platform ID is not valid!")
               Case 3
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The bumper type is not valid!")
               Case 4
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The placement of the sensors differs from the config. file!")
               Case 5
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ did not answer!")
               Case 6
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ has the wrong software version!")
               Case 7
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ is not programmed!")
               Case 8
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ did not respond to EEPROM read!")
               Case 9
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: No sensor was detected!")
               Case 10
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported voltage error!")
               Case 11
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ Sensor reported hardware failure!")
               Case 12
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported unstable address error!")
               Case 13
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported internal noise error!")
               Case 14
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported initialization error!")
               Case 15
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported calibration error!")
               Case 16
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported hardware online failure!")
               Case 17
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ failed clutter test!")
               Case 18
                  ErrorCodeToText = "The tbet configuration file *.cfg is not sealed"
               Case 19
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ Is in the wrong operating mode!")
               Case 20
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: mismatch of PLATFORM_ID and JIT-Label data (ChasisCode/PlatformCode/Varaiant!")
               Case 21
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: DTR OFFSET could not be set to EEPROM!")
               Case Else
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "System Error %!")
            End Select

         Case Else
            'Use this set of Error Codes for the old Tbett.dll
            Select Case (SEN_ERR_MASK And errorCode)
               Case 1
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The CAN hardware or the driver is missing!")
               Case 2
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The platform ID is not valid!")
               Case 3
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The bumper type is not valid!")
               Case 4
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: The placement of the sensors differs from the config. file!")
               Case 5
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ did not answer!")
               Case 6
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ has the wrong software version!")
               Case 7
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ is not programmed!")
               Case 8
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ did not respond to EEPROM read!")
               Case 9
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "Error %: No sensor was detected!")
               Case 10
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported low voltage error!")
               Case 11
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported high voltage error!")
               Case 12
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported unstable address error!")
               Case 13
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported internal noise error!")
               Case 14
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported initialization error!")
               Case 15
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported calibration error!")
               Case 16
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ reported hardware error!")
               Case 17
                  ErrorCodeToText = Interpreterror(errorCode, "Error %: Sensor #^ failed clutter test!")
               Case 18
                  ErrorCodeToText = "The tbet configuration file *.cfg is not sealed"
               Case Else
                  ErrorCodeToText = Interpreterror(SEN_ERR_MASK And errorCode, "System Error %!")
            End Select
      End Select

   End Function


    Function Interpreterror(ByVal errorsCode As Integer, ByVal template As String) As String

        Dim errors As Integer
        Dim text As String = ""
        Dim sensorMask As Integer
        Dim shiftBy As Integer
        Dim Array() As Object = Nothing
        Dim TBETErrorArray(10, MAX_BUMPER_SENSORS) As Integer

        sensorMask = (errorsCode And SEN_MASK) / (2 ^ 24) 'equivalent of errorsCode >> 24
        errors = errorsCode And SEN_ERR_MASK

        If sensorMask <> 0 Then
            For ii = 0 To MAX_BUMPER_SENSORS

                shiftBy = 2 ^ ii
                If (sensorMask And (1 * shiftBy)) Then
                    text = text + TBETFormat(template, errors, ii + 1) & "-"
                End If
            Next
        Else
            'in case when the sensor mask is empty then deal with it as if it were
            ' a general errors
            text = text + TBETFormatSingleError(template, errors)
        End If

            '            text = text + TBETFormat(template, Array(errors)) + vbCrLf
        Interpreterror = text
    End Function

    Function TBETFormat(ByVal str As String, ByVal errors As Integer, ByVal SensorNumber As Integer)

        Dim res As String
        res = str
        Dim errorString As String
        errorString = errors.ToString

        res = res.Replace("%", errors)
        res = res.Replace("^", SensorNumber)

        TBETFormat = res
    End Function

    Function TBETFormatSingleError(ByVal str, ByVal args)
        Dim res As String
        res = str

        res = res.Replace("%", args)
        res = res.Replace("^", "")

        TBETFormatSingleError = res
    End Function

    Sub CleartbetSensorInfo()
        For index = 1 To MAX_BUMPER_SENSORS
            'Clear Out Previous Values
            tbetSensor(index).masrNum = 0
            tbetSensor(index).SerialNum = 0
            tbetSensor(index).majVersion = 0
            tbetSensor(index).minVersion = 0
        Next index
        'Clear Previous Results Of Sensor 1 on screen
        frmMain.lbltbetmasrNumSensor1.Text = ""
        frmMain.lbltbetSerialNumSensor1.Text = ""
        frmMain.lbltbetmajVersionSensor1.Text = ""
        frmMain.lbltbetminVersionSensor1.Text = ""

        'Clear Previous Results Of Sensor 2 on screen
        frmMain.lbltbetmasrNumSensor2.Text = ""
        frmMain.lbltbetSerialNumSensor2.Text = ""
        frmMain.lbltbetmajVersionSensor2.Text = ""
        frmMain.lbltbetminVersionSensor2.Text = ""

        'Clear Previous Results Of Sensor 3 on screen
        frmMain.lbltbetmasrNumSensor3.Text = ""
        frmMain.lbltbetSerialNumSensor3.Text = ""
        frmMain.lbltbetmajVersionSensor3.Text = ""
        frmMain.lbltbetminVersionSensor3.Text = ""

        'Clear Previous Results Of Sensor 4 on screen
        frmMain.lbltbetmasrNumSensor4.Text = ""
        frmMain.lbltbetSerialNumSensor4.Text = ""
        frmMain.lbltbetmajVersionSensor4.Text = ""
        frmMain.lbltbetminVersionSensor4.Text = ""

        frmMain.lblTbetTestResult.BackColor = Color.Empty
        frmMain.lblTbetTestResult.Text = ""
        frmMain.textbox.Text = ""
        frmMain.textbox.BackColor = Color.Empty
        frmMain.Refresh()
    End Sub
    Function LookUpPlatForms(ByVal SectionName As String, ByVal KeyToLookUp As String, ByVal ConfigFileName As String) As String

        'This Routine Loads All Part Information From The .ini File
        'Note FindValaidPartNumbers Subroutine Must Be Run Before This Routine Or This Routine Will Fail

        Dim DefaultValue As String = ""

        If File.Exists(DefaultTBETConfigurationFilePath & ConfigFileName) = False Then
            MsgBox("Default TBET Configuration file not found" & FileName, MsgBoxStyle.Critical, "LookUpInfo .ini ERROR")
            LookUpPlatForms = "Error"   'Fail Lookup Info
            Exit Function
        End If

        LookUpPlatForms = ReadINIFile(SectionName, KeyToLookUp, DefaultValue, DefaultTBETConfigurationFilePath & ConfigFileName)

    End Function
    Sub CheckTbetPath(ByVal DefaultPath As String)
        'Checks To Make Sure That The Default Tbet file path is in the Enviroment 'Path" Variable
        Dim PathString As String = Environment.GetEnvironmentVariable("Path")
        Select Case InStr(PathString, DefaultPath)
            Case Is = 0
                'Path Statement Is Missing So Add It
                Environment.SetEnvironmentVariable("Path", PathString & ";" & DefaultPath)
            Case Else
                'Path Was In The String So Do Nothing
        End Select
    End Sub
    Private Function toLatin1(ByVal str As String) As String
        Dim bytes() As Byte
        'Dim enc As System.Text.Encoding = Encoding.Default
        Dim enc As New System.Text.ASCIIEncoding()
        bytes = enc.GetBytes(str)
        Dim latin1 As Encoding = Encoding.GetEncoding(28591)
        'bytes = Encoding.Convert(Encoding.UTF8, latin1, bytes)
        str = latin1.GetString(bytes)

        Return str

    End Function

End Module
