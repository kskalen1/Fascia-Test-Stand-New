Imports System.Drawing
Imports System.Text


Module Global_Vars


   'Fascia Test Stand Variables
   Public SQLDataSource As String
   Public SQLUserID As String
   Public SQLPassword As String
   Friend DeskDebug As Boolean = False
   Public ValidModelNumbers(10) As String
   Public ValidBumpers(10) As String
   Public ValidTestTypes(10) As String
   Public MachineID As Integer
   Public Allocation_Logic_iZuol As Integer
   Public MeterInstalled As Boolean
   Public RelayBoardInstalled As Boolean
   Public TBOX_Ver_1_2_Installed As Boolean
   Public TBOX_Ver_2_0_Installed As Boolean
   Public PeakCanInstalled As Boolean
   Public BarcodeScannerInstalled As Boolean
   Public ValeoSensorMeasurementDeviceInstalled As Boolean
   Public RFIDInstalled As Boolean
   Public SensorMeasurementInProgress As Boolean
   Public LINBoxInstalled As Boolean
   Public LastBackColor As System.Drawing.Color
   Public PaintColorCode As Integer
   Public ProductionSequenceNumber As String
   Public FailString As String
   Public StartButtonDoubleClick As Boolean
   Public NestInRotatedPositionInputPort As Byte
   Public NestInRotatedPositionInputBank As Byte
   Public NestAtHomePositionInputPort As Byte
   Public NestAtHomePositionInputBank As Byte
   Public PartInPlaceInputPort As Byte
   Public CameraConnectedInputPort As Byte
   Public ConnectorAConnectedInputBank As Byte
   Public ArtPlateHarnessConnectedInputPort As Byte
   Public ArtPlateHarnessConnectedInputBank As Byte
   Public StartRestartSwitchInputPort As Byte
   Public StartRestartSwitchInputBank As Byte
   Public NestLockReleaseInputBank As Byte
   Public NestLockReleaseInputPort As Byte
   Public HeaderString As New StringBuilder()
   Public DataString As New StringBuilder()
   Public SensorHarnessTestOnly As Boolean
   Public Sensor1TestInabled As Boolean
   Public Sensor2TestInabled As Boolean
   Public Sensor3TestInabled As Boolean
   Public Sensor4TestInabled As Boolean
   Public Sensor5TestInabled As Boolean
   Public Sensor6TestInabled As Boolean
   Public CameraHarnessInputPort As Byte
   Public CameraHarnessInputBank As Byte

   Public Structure RFID_BITS
      Friend BIT_Number As Integer
      Friend BIT_Description As String
      Friend BIT_Active As Boolean
      Friend BIT_Test_ID As String
      Friend BIT_Fascia_Tester As Boolean
      Friend BIT_Comment As String
      Friend BIT_Last_Read As Integer
   End Structure
   Friend RFID_BIT_DATABASE(192) As RFID_BITS
   Public RFID_InitString As String
   Public RFIDReadComplete As Boolean
   Public Structure TestResultRFIDBITS
      Dim PTS As String
      Dim GSAT As String
      Dim LED As String
      Dim CAMERA As String
      Dim TSM As String
      Dim TEMP As String
      Dim iBSM As String
      Dim RCW As String
      Dim FCW As String
      Dim DTR As String
      Dim BSM As String
      Dim PSAT As String
      Dim HFA As String
      Dim HFAGEN1 As String
      Dim HFAGEN2 As String
      Dim HFAGEN3 As String
      Dim ART As String
      Dim HE As String  '09_25_2022 Added JME HEating Element Change
   End Structure

   Public TestResults_RFID As TestResultRFIDBITS

   Public Structure HFAVariablesStructure
      Friend TestName As String
      Friend TestUnit As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend Brose_HFA_ElectrodeTestVariables(1) As HFAVariablesStructure
   Friend Brose_HFA_BISTTestVariables(1) As HFAVariablesStructure
   Friend Brose_HFA_ProductionDataTestVariables(1) As HFAVariablesStructure
   Friend IEE_HFA_ECUSerialNumberTestVariables(1) As HFAVariablesStructure
   Friend IEE_HFA_AntennaDiagnosticTestVariables(1) As HFAVariablesStructure

   Public Structure PTSVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MinLimit As String
      Friend ValueMeasured As String
      Friend MaxLimit As String
      Friend Result As String
      Friend DelayBeforeMeasurement As String
      Friend NumberOfSensors As String
      Friend TestResultString As String
   End Structure
   Friend PTSTestVariables(5) As PTSVariables

   Public Structure SensorHarnessVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend SensorHarnessTestVariables(1) As SensorHarnessVariables

   Public Structure SensorVariablesStruct
      Friend TestName As String
      Friend MeasurementType As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend PlatFormName As String
      Friend PlatFormID As String
      Friend errorMessages() As String
      Friend masrNum() As String
      Friend SerialNum() As String
      Friend majVersion() As String
      Friend minVersion() As String
      Friend ConfigurationFile As String
      Friend TestResultString As String
      Friend SensorAddress1 As String
      Friend SensorAddress2 As String
      Friend SensorAddress3 As String
      Friend Whichdll As String
   End Structure
   Friend SensorTestVariables(2) As SensorVariablesStruct
   Friend iBSMTestVariables(2) As SensorVariablesStruct
   Friend BSMTestVariables(2) As SensorVariablesStruct
   Friend DTRTestVariables(2) As SensorVariablesStruct
   Friend FCWTestVariables(2) As SensorVariablesStruct
   Friend RCWTestVariables(2) As SensorVariablesStruct

   Public Structure tbetSensors
      Friend masrNum As Integer
      Friend SerialNum As Integer
      Friend majVersion As Integer
      Friend minVersion As Integer
      Friend subVersion As Integer
      Friend lProdNumber As String
      Friend lProdYear As Integer
      Friend lProdWeek As Integer
      Friend lProdDay As Integer
      Friend lSerialNumber As Integer
      Friend errorString As String
      Friend errorCodes As String
   End Structure
   Friend BumperTestResult As String = ""
   Public Structure SensorPreTestVariablesStruct
      Friend PlatFormID As Integer
      Friend BumperLocation As Integer
      Friend TestComplete As Boolean
      Friend TestResult As String
   End Structure
   Friend SensorPreTestVariables As SensorPreTestVariablesStruct

   Friend iBSMPreTestVariables As SensorPreTestVariablesStruct
   Friend BSMPreTestVariables As SensorPreTestVariablesStruct
   Friend DTRPreTestVariables As SensorPreTestVariablesStruct
   Friend FCWPreTestVariables As SensorPreTestVariablesStruct

   Public Structure LampVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForConnection As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend LED_LampTestVariables(1) As LampVariables

   Public Structure TemperatureVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend TempereatureSensorTestVariables(1) As TemperatureVariables

   Public Structure HeatingElementVariables  '09_25_2022 Added JME HEating Element Change
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend HeatingElementTestVariables(1) As HeatingElementVariables

   Public Structure CameraVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend CameraTestVariables(1) As CameraVariables

   Public Structure ARTPlateVariables
      Friend TestName As String
      Friend TestUnit As String
      Friend MeasurementType As String
      Friend MeterRange As String
      Friend MinLimit As String
      Friend MaxLimit As String
      Friend DelayBeforeMeasurement As String
      Friend RelayNumberForLowSideConnect As String
      Friend RelayNumberForHighSideConnect As String
      Friend ValueMeasured As String
      Friend Result As String
      Friend TestResultString As String
   End Structure
   Friend ARTPlateTestVariables(1) As ARTPlateVariables

   Public Structure EchoInfo
      Friend tsStartTime As Double
      Friend tsEndTime As Double
      Friend tsTime As Double
      Friend tsFound As Boolean
      Friend taStartTime As Double
      Friend taEndTime As Double
      Friend taTime As Double
      Friend taFound As Boolean
      Friend teStartTime As Double
      Friend teEndtime As Double
      Friend teTime As Double
      Friend tefound As Boolean
      Friend EchoDelay As Boolean
      Friend DistanceIncm As Double
   End Structure
   Friend ValeoSensorData As EchoInfo
   Friend EchoData(6) As EchoInfo

   Public Structure ValeoSensorEchoLimits
      Friend tsTime_Max As Double
      Friend tsTime_Min As Double
      Friend taTime_Max As Double
      Friend taTime_Min As Double
   End Structure
   Friend EchoLimits As ValeoSensorEchoLimits
   Friend WhichDll As String

   Friend CurrentPartBarcode As String

   Public Enum OperatorIDLoginStatus
      Logged_In
      Logged_Out
   End Enum
   Friend LogoutStatus As Integer

   Public Enum GridColIndex As Integer
      TestName = 0
      MinLimit = 1
      ValueMeasured = 2
      MaxLimit = 3
      Result = 4
   End Enum
   Public Enum RFIDGridColIndex As Integer
      BIT = 0
      Description = 1
      Test_ID = 2
      IARelevant = 3
      RFIDRead = 4
   End Enum
   Public Enum TestDisplayModes
      ManualMode
      AutomaticMode
   End Enum
   Public RowIndex As Integer
   Public IntermecPrinterName As String

   Public PlatformToTest As String = ""
   Public BumperLocationToTest As String = ""
   Public AllowRetestOfPassedFascia As Boolean
   Public AllTestsPassed As Boolean = True
   Public BarcodeIsScanned As Boolean
   Public TimeToStopTesting As Boolean
   '  Public TestResultString As String
   Public TimeAndDateTestEnded As String

   Public OperatorIDLogoutTime As DateTime
   Public OperatorIDInactivityLogoutTime As Double
   Public NextSequenceNumber As Integer

   Public SkipFCW As Boolean

   Friend tbetSensor(4) As tbetSensors

   Friend Loops_Selected As Integer

   Friend TBOX_Ver_6_0_comPort As String

   Friend TBOX_Ver_5_0_CurrentNumberOfSensorsConfigured As Integer
   Friend TBOX_Ver_5_0_CurrentSensorMaskConfiguration As Integer
   Public TBOX_Ver_6_0_Installed As Boolean

   Friend BarcodeScannerComPort As String
   Friend MetercomPort As String
   Friend RelayBoardcomPort As String

   'Start Of General Variables
   Public Password As String
   Public PasswordOk As Boolean
   Public AbortTesting As Boolean = False
   Public AbortButtonPressed As Boolean

   Public Const DefaultLabelDataFilePath = "C:\Logs\"
   Public Const DefaultINIFileName = "\Fascia Test Stand.ini"
   Public Const DefaultIniPath = "C:\Rehau Fascia Test Stand\Fascia Test Stand\iniFiles"
   Public Const DefaultTestInformationINIFileName = "\Fascia Test Stand.ini"
   Public Const DefaultBumperPictureFilePath = "C:\Rehau Fascia Test Stand\Fascia Test Stand\Pictures\Bumper Pictures\"
   Public DefaultTBOXCommunicationINIFileName As String = ""
   Public DefaultTBETConfigurationFilePath As String = ""
   Public DefaultTBETConfigurationFileName As String = ""
   Public cfgED As String
   Public SensorTestRequired As Boolean
   Public ReturnText As String
   Public DataLogPassLabelFileName As String
   Public DataLogErrorLabelFileName As String
   Public TimeToChangeFile As String = "6:59:59 AM"
   Public strDayToChangeFile As String
   Public strFileCreationDay As String
   Public PrinterName As String
   Friend DefaultBackroundColor = KnownColor.Control
   Friend LastLabelPrinted As String
   Public SensorFailedString As String
   Public SensorFailCodes As String
   Public TBOXBumperConfiguration As String

   'Millisecond Timer Declaration **********************************************
   Declare Function timeGetTime Lib "winmm.dll" () As Long

   'Ini File Search Variables *************************************************
   Public Section As String
   Public Key As String
   Public writevalue As String
   Public FileName As String
   Public INI_FileName As String
   Public SearchingIni As Integer

   Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
   Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long
   Declare Ansi Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
   Declare Ansi Function GetPrivateProfileSectionNames Lib "kernel32" Alias "GetPrivateProfileSectionNamesA" (ByVal lpszReturnBuffer As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

   '   =====================================================
   Public Const DSKERR_DISKNOTREADY = 71, DSKERR_DEVICEUNAVAILABLE = 68
   Public Const HWND_TOPMOST = -1, HWND_NOTOPMOST = -2
   Public Const SWP_NOACTIVATE = &H10, SWP_SHOWWINDOW = &H40
End Module
