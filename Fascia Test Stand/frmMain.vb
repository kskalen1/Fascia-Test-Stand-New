
Imports System.IO
Imports REHAU_RMS2011_FosCl
Imports Fascia_Test_Stand.PCANLight

Public Class frmMain
   Public BoschBoxErrorStringWriter As TextWriter

   Private Declare Function SetEnvironmentVariable Lib "kernel32.dll" Alias "SetEnvironmentVariableA" (ByVal lpName As String, ByVal lpValue As String) As Integer
   Dim ActiveHardware As HardwareType
   ' CAN messages Array. Store the Message Status for its display
   '
   Dim LastMsgsList As ArrayList
#Region "Structures"
   ' Message Status structure used to show CAN Messages
   ' in a ListView
   '
   Structure MessageStatus
      Private Msg As TCLightMsg
      Private iIndex As Integer

      Public Sub New(ByVal CanMsg As TCLightMsg, ByVal Index As Integer)
         Msg = CanMsg
         iIndex = Index
      End Sub

      Public ReadOnly Property CANMessage() As TCLightMsg
         Get
            Return Msg
         End Get
      End Property

      Public ReadOnly Property Position() As Integer
         Get
            Return iIndex
         End Get
      End Property
   End Structure
#End Region

   Private Sub updateTimeAndDate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updateTimeAndDate.Tick
      'Updates The Time And Date On The Main Screen
      Me.ToolStripStatusLabelTime.Text = Format(Now, "Medium Time")
      Me.ToolStripStatusLabelDate.Text = Format(Now, "Short Date")

   End Sub

   Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
      'Cancel Any BackGround Workers That May Still Be Running
      Sensor_BackgroundWorker.CancelAsync()

      RelayBoard.Turn_Off_All_Relays()
      RelayBoardPort.ClosePort()
      End
   End Sub

   Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
      If _oRehau_RFID_Class IsNot Nothing Then
         _oRehau_RFID_Class.bCancel = True
         _oRehau_RFID_Class = Nothing
      End If
   End Sub

   Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

      DeskDebug = False

      'Load General System Parameters from .ini file
      Support.LoadGeneralVariables()

      'Remove Tabs At Startup
      tabMain.TabPages.Remove(tabRelayBoard)
      tabMain.TabPages.Remove(tabAutoliv)
      tabMain.TabPages.Remove(tabLabels)
      tabMain.TabPages.Remove(tabBKMeter)
      tabMain.TabPages.Remove(tabValeoSensor)
      tabMain.TabPages.Remove(tabRFID)
      tabMain.TabPages.Remove(tabDatabaseEditors)
      tabMain.TabPages.Remove(tabPictures)
      tabMain.TabPages.Remove(tabPeakCAN)
      tabMain.TabPages.Remove(tabTBOX6)

      Select Case DeskDebug
         Case True
            'Dont initialze Instruments If Working at desk and not tester
         Case Else
            'Initialize Meter Com Port
            If MeterInstalled = True Then
               'Initialize Meter Com Port
               Meter.MeterSerialPortOpen()
               Meter_RST()
            End If

            If PeakCanInstalled = True Then
               ''If the Peak CAN Ineterface Is Installed Then initalize Tbet Configuration
               TBET.TbetInitialize()
               cbbHws.SelectedIndex = 0
               cbbBaudrates.SelectedIndex = 0
               cbbIO.Text = "0378"
               cbbInterrupt.Text = "7"
               cbbMsgType.SelectedIndex = 0  'USB
               ' Set common event handlers
               '
               AssignEvents()
               Bosch_Radar.LoadCanMessages_Standard()    'Load The Standard CAN Message From The Database
            End If

            If TBOX_Ver_6_0_Installed = True Then
               'Load all Tbox Communication Commands
               TBOX.LoadTBOXCommunicationProtocall()
               'Load TBOX Communication Parameters
               TBOX.TBOX_6_0_SerialPortOpen()
            End If

            If RelayBoardInstalled = True Then
               'Initialize the Relay Board
               RelayBoard.Initialize()
               'Enable The ScanSwitches Timer
               tmrScanSwitches.Enabled = True
            End If

            'Send Init Commands To Printer
            InitPrinter()

            'Load The RFID BIT Descriptions From Database
            GetRFIDBitsFromDatabase()

            If RFIDInstalled = True Then
               'Setup For RFID
               Me.btnDoOrder.Enabled = False
               Me.btnConfirmOrder.Enabled = False
               Me.btnConfirmOrderNio.Enabled = False
               ' Start The RFID Client For Receiving and transmitting to RFID System
               If _oRehau_RFID_Class IsNot Nothing Then
                  _oRehau_RFID_Class.bCancel = True
                  _oRehau_RFID_Class = Nothing
               End If
               Threading.Thread.Sleep(1000)
               vStartFosClient()
               Threading.Thread.Sleep(500)
               vStartFosClient()
            End If

            'Set Default Text So Load Of Image And .ini File Does Not Fail
            If rbPlatform_1.Text = "Platform_1" Or rbPlatform_1.Text = "" Then
               rbPlatform_1.Text = "X296"
            End If

            If LookUpInfo("System", "TesterType") = "FRONT" Then
               rbFrontSelected.Checked = True
               SetBumperScreen("Front")
               cbArtPlateTestDisabled.Visible = True
               cbArtPlateTestDisabled.Checked = True
            Else
               rbRearSelected.Checked = True
               SetBumperScreen("Rear")
               cbArtPlateTestDisabled.Visible = False
            End If

            rbNoneSelected.Checked = True


            'Find Avalable Platforms
            rbPlatform_1.Text = "Platform_1"

            FindAvalablePlatforms()


      End Select

   End Sub

   Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
      Me.Close()
   End Sub

   Sub UpdateLabelInformation(ByVal TypeOfLabel As String)
      'Updates the Screen and the label with the current information.
      Dim JulianDay As System.DateTime
      Dim JulianDayOfYear As Int32

      Select Case TypeOfLabel
         Case Is = "PassLabel"
            'Update label

         Case Is = "FailLabel"
            'Update label

         Case Else
            'No Data
      End Select


      Dim DateCodeString As String = ""
      Dim YearString As String = ""

      'Set Variables = To Todays date and time
      JulianDay = Now
      JulianDayOfYear = JulianDay.DayOfYear


   End Sub
   Private Sub UtilitiesTabsShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtilitiesTabsShowToolStripMenuItem.Click
      If UtilitiesTabsShowToolStripMenuItem.Checked = True Then
         frmPassword.ShowDialog()
         frmPassword.Dispose()
         Select Case PasswordOk
            Case True
               'If Password matched then show all the controls on the Utility Tabs
               UtilitiesTabsShowToolStripMenuItem.CheckState = CheckState.Checked
               tabMain.TabPages.Add(tabRFID)

               If MeterInstalled = True Then
                  tabMain.TabPages.Add(tabBKMeter)
               End If

               If RelayBoardInstalled = True Then
                  tabMain.TabPages.Add(tabRelayBoard)
               End If

               If TBOX_Ver_6_0_Installed = True Then
                  tabMain.TabPages.Add(tabTBOX6)
               End If

               tabMain.TabPages.Add(tabLabels)
               tabMain.TabPages.Add(tabDatabaseEditors)
               'tabMain.TabPages.Add(tabPictures)

               AbortTesting = True

            Case False
               'If Password dose not match then show all the controls on the Utility Tabs
               UtilitiesTabsShowToolStripMenuItem.CheckState = CheckState.Unchecked
               tabMain.TabPages.Remove(tabRelayBoard)
               tabMain.TabPages.Remove(tabLabels)
               tabMain.TabPages.Remove(tabBKMeter)
               tabMain.TabPages.Remove(tabRFID)
               tabMain.TabPages.Remove(tabDatabaseEditors)
               tabMain.TabPages.Remove(tabPictures)
               tabMain.TabPages.Remove(tabTBOX6)
               PasswordOk = False
         End Select
      Else
         UtilitiesTabsShowToolStripMenuItem.CheckState = CheckState.Unchecked
         tabMain.TabPages.Remove(tabRelayBoard)
         tabMain.TabPages.Remove(tabLabels)
         tabMain.TabPages.Remove(tabBKMeter)
         tabMain.TabPages.Remove(tabRFID)
         tabMain.TabPages.Remove(tabDatabaseEditors)
         tabMain.TabPages.Remove(tabPictures)
         tabMain.TabPages.Remove(tabTBOX6)
         PasswordOk = False
      End If

   End Sub

   Private Sub btnMeterSendIDN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeterSendIDN.Click
      Dim ReturnedMessage As String = ""

      btnMeterSendIDN.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_IDN()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnMeterSendIDN.Enabled = True
   End Sub

   Private Sub btnMeterSendRST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeterSendRST.Click
      Dim ReturnedMessage As String = ""

      btnMeterSendRST.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_RST()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnMeterSendRST.Enabled = True

   End Sub

   Private Sub btnAmpsDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmpsDC.Click
      Dim ReturnedMessage As String = ""

      btnAmpsDC.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_ADC()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnAmpsDC.Enabled = True

   End Sub
   Private Sub btnMeterOHMS_Click_1(sender As Object, e As EventArgs) Handles btnMeterOHMS.Click
      Dim ReturnedMessage As String = ""

      btnMeterOHMS.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_OHMS()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnMeterOHMS.Enabled = True

   End Sub

   Private Sub btnMeterVoltsAC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeterVoltsAC.Click
      Dim ReturnedMessage As String = ""

      btnMeterVoltsAC.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_VAC()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnMeterVoltsAC.Enabled = True

   End Sub

   Private Sub btnMeterVDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeterVDC.Click
      Dim ReturnedMessage As String = ""

      btnMeterVDC.Enabled = False
      Me.lblMeterRXStatus.Text = ""
      ReturnedMessage = Meter_VDC()
      Me.lblMeterRXStatus.Text = ReturnedMessage
      btnMeterVDC.Enabled = True

   End Sub

   Private Sub btnMeterTakeMeasuement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeterTakeMeasuement.Click

      btnMeterTakeMeasuement.Enabled = False
      Me.lblMeterMeasurementReading.Text = Format(Val(Meter.Meter_TakeMeasurement), "#####.#####")
      Me.lblMeterRXStatus.Text = "Measurement Taken"
      btnMeterTakeMeasuement.Enabled = True
   End Sub

   Private Sub btnLoadNewTbetConfigFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadNewTbetConfigFile.Click
      'Declare a variable as a FileDialog object.
      Dim fd As New OpenFileDialog
      Dim Result As Object

      'Set A Default Platform And Chassis Code
      PlatformToTest = "W205"


      'Use a With...End With block to reference the FileDialog object.
      fd.FileName = ""
      With fd
         'Do Not Allow the selection of multiple files.
         .Multiselect = False
         'Add filter'
         .InitialDirectory = DefaultTBETConfigurationFilePath
         .Filter = "Tbet Config Files (*.cfg)|*.cfg"
         .FileName = DefaultTBETConfigurationFileName
         Result = .ShowDialog()
      End With

      Select Case Result
         Case Is = 1
            DefaultTBETConfigurationFileName = fd.SafeFileName
            If InStr(DefaultTBETConfigurationFileName, "MXBET", CompareMethod.Text) <> 0 Then
               WhichDll = "MXBET"

            ElseIf InStr(DefaultTBETConfigurationFileName, "MRBET", CompareMethod.Text) <> 0 Then
               WhichDll = "MRBET"

            Else
               WhichDll = "BET"

            End If

            TBET.LoadTbetConfigurationFile(DefaultTBETConfigurationFileName)

         Case Else

      End Select

      'Set the object variable to Nothing.
      fd = Nothing
   End Sub

   Private Sub btnTestBumper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestBumper.Click
      btnTestBumper.Enabled = False
      Application.DoEvents()
      Dim BumperTestResult As String = ""

      If InStr(DefaultTBETConfigurationFileName, "MXBET", CompareMethod.Text) <> 0 Then
         WhichDll = "MXBET"

      ElseIf InStr(DefaultTBETConfigurationFileName, "MRBET", CompareMethod.Text) <> 0 Then
         WhichDll = "MRBET"

      Else
         WhichDll = "BET"
      End If

      'Get the user selection for the platform and the bumper
      TBET.TestBumper(PlatformIndex(Me.platformCB.SelectedIndex), Me.bumperCB.SelectedIndex + 1, TestDisplayModes.ManualMode, BumperTestResult)

      btnTestBumper.Enabled = True
   End Sub

   Private Sub cmdTurnOffAllRelays_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTurnOffAllRelays.Click
      Turn_Off_All_Relays()
   End Sub

   Private Sub cbRelay_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_1.CheckedChanged
      Select Case cbRelay_1.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_1)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_1)
      End Select
   End Sub

   Private Sub cbRelay_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_2.CheckedChanged
      Select Case cbRelay_2.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_2)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_2)
      End Select

   End Sub

   Private Sub cbRelay_3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_3.CheckedChanged
      Select Case cbRelay_3.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_3)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_3)
      End Select

   End Sub

   Private Sub cbRelay_4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_4.CheckedChanged
      Select Case cbRelay_4.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_4)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_4)
      End Select

   End Sub

   Private Sub cbRelay_5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_5.CheckedChanged
      Select Case cbRelay_5.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_5)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_5)
      End Select

   End Sub

   Private Sub cbRelay_6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_6.CheckedChanged
      Select Case cbRelay_6.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_6)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_6)
      End Select

   End Sub

   Private Sub cbRelay_7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_7.CheckedChanged
      Select Case cbRelay_7.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_7)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_7)
      End Select

   End Sub

   Private Sub cbRelay_8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_8.CheckedChanged
      Select Case cbRelay_8.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_8)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_8)
      End Select
   End Sub

   Private Sub cbRelay_9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_9.CheckedChanged
      Select Case cbRelay_9.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_9)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_9)
      End Select
   End Sub

   Private Sub cbRelay_10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_10.CheckedChanged
      Select Case cbRelay_10.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_10)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_10)
      End Select
   End Sub

   Private Sub cbRelay_11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_11.CheckedChanged
      Select Case cbRelay_11.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_11)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_11)
      End Select

   End Sub

   Private Sub cbRelay_12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_12.CheckedChanged
      Select Case cbRelay_12.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_12)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_12)
      End Select

   End Sub

   Private Sub cbRelay_13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_13.CheckedChanged
      Select Case cbRelay_13.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_13)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_13)
      End Select

   End Sub

   Private Sub cbRelay_14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_14.CheckedChanged
      Select Case cbRelay_14.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_14)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_14)
      End Select

   End Sub

   Private Sub cbRelay_15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_15.CheckedChanged, cbBoschPower.CheckedChanged
      Select Case cbRelay_15.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_15)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_15)
      End Select

   End Sub

   Private Sub cbRelay_16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRelay_16.CheckedChanged
      Select Case cbRelay_16.Checked
         Case Is = True
            Turn_On_Relay(Relays.Relay_16)
         Case Is = False
            Turn_Off_Relay(Relays.Relay_16)
      End Select

   End Sub

   Private Sub tabMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMain.SelectedIndexChanged
      Select Case PasswordOk
         Case Is = True

         Case Else
            'Dont Alow Selection of another tab if Password is invalid
            Me.tabMain.SelectedTab = tabSelectPartInformation
      End Select
   End Sub

   Private Sub btnStartTesting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartTesting.Click
      'Start Testing
      Dim TimeTestStarted As DateTime
      Dim StopTesting As Boolean = False
      Dim RetestFailedFascia As Boolean = False
      Dim TimeToAbortRFIDRead As Long = 0

      btnStartTesting.Enabled = False
      BarcodeIsScanned = False
      TimeToStopTesting = False
      btnStopTesting.Enabled = True
      AbortTesting = False
      Do
         FailString = ""
         StartButtonDoubleClick = False
         Application.DoEvents()
         Select Case Me.rbRFIDTestModeAutomatic.Checked
            Case Is = True
               If RetestFailedFascia Then
                  'Re-Enable The ScanSwitches Timer
                  tmrScanSwitches.Enabled = True
                  lblTestResult.Text = "Retesting Fascia"
                  lblTestResult.BackColor = Color.Yellow

                  'Clear All Previous Test Results
                  InitializeForNewTest()

                  Application.DoEvents()

                  RFIDReadComplete = False
                  Application.DoEvents()
                  TriggerRFIDRead() 'Activate "Buzzer" Which Should Trigger An RFID Read

                  'Wait For RFID Read And Order Complete From Rehau .dll
                  lblTestResult.Text = "Waiting For RFID Read"

                  TimeToAbortRFIDRead = timeGetTime + 5000
                  Do
                     Application.DoEvents()
                     btnOperatorAbort.Refresh()
                     If timeGetTime > TimeToAbortRFIDRead Then
                        AbortTesting = True
                     End If
                     If AbortTesting = True Then Exit Do
                     delay(5)
                  Loop Until RFIDReadComplete = True

                  'If Testing Should Be Aborted Then
                  If AbortTesting = True Then Exit Do

                  'If RFID Read Completed Correctly Then Confirm Order
                  If RFIDReadComplete = True Then
                     ConfirmOrder() 'Confrim That The Order Was Read And Save All Relvent Data To Send Back To Rehau RFID .dll
                     UpdateRFIDInfoToMainScreen()
                     Me.lblProductionNumber.Text = _oOrder.strPnr.Trim 'Display The Production Number
                     ProductionSequenceNumber = Me.lblProductionNumber.Text
                  End If

                  'Check To Make Sure Part IS Okay To Test Or Has been tested before
                  'Check Status of bytes sent from order in string _oOrder.strGsb
                  If CheckForOKToTestFascia(_oOrder.strGsb, ReturnText) = True Then
                     SensorTestRequired = True
                  Else
                     SensorTestRequired = False
                  End If

                  If SensorTestRequired = True Then
                     'RunTests Based On Which Ones Have Been Decoded On Barcode
                     lblTestResult.Text = "Running Tests Based On RFID"
                     lblTestResult.BackColor = System.Drawing.SystemColors.ActiveCaption
                     lbloverallTestResult.Text = "Testing"
                     lbloverallTestResult.BackColor = System.Drawing.SystemColors.ActiveCaption
                  Else
                     'No Testing Should Be Done On This Bumper
                     lblTestResult.Text = ReturnText
                     lblTestResult.BackColor = Color.Yellow
                     AbortTesting = True
                     Exit Do
                  End If

                  DecodeRFIDBits(_oOrder.strBPtV)  'Decode Order and setup tests accordingly.

               Else

                  'Re-Enable The ScanSwitches Timer
                  tmrScanSwitches.Enabled = True

                  RetestFailedFascia = False

                  'Wait For Operator
                  lblTestResult.BackColor = Color.Yellow
                  lblTestResult.Text = "Waiting For Part To Be Loaded"
                  Do
                     Application.DoEvents()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(250)
                  Loop Until PartInPlace()

                  lblTestResult.Text = "Waiting For Nest Lock Release Switch"
                  Do
                     Application.DoEvents()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(250)
                  Loop Until NestLockRelease()

                  If NestLockRelease() Then
                     'Fire Relay 
                     RelayBoard.Turn_On_Relay(NestLockRelay)
                  End If

                  'Wait For Operator To Rotate The Nest
                  lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
                  lblTestResult.BackColor = Color.Yellow
                  Do
                     Application.DoEvents()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(250)
                  Loop Until NestInRotatedPosition()

                  If NestInRotatedPosition() Then
                     'Turn Off Relay 
                     RelayBoard.Turn_Off_Relay(NestLockRelay)
                  End If

                  lblTestResult.Text = "Waiting For Start Test Switch"
                  lblTestResult.BackColor = Color.Yellow
                  Do
                     Application.DoEvents()
                     ScanSwitchValues()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(250)
                  Loop Until StartRestartSwitch()

                  If StartRestartSwitch() Then
                     'Fire Connector Lock Relay
                     RelayBoard.Turn_On_Relay(ConnectorLockRelay)
                     delay(150)
                     RelayBoard.Turn_On_Relay(EngageConnectorPogoPinsRelay)
                  End If

                  'Clear All Previous Results  
                  InitializeForNewTest()

                  Application.DoEvents()

                  RFIDReadComplete = False
                  Application.DoEvents()
                  TriggerRFIDRead() 'Activate "Buzzer" Which Should Trigger An RFID Read

                  'Wait For RFID Read And Order Complete From Rehau .dll
                  lblTestResult.Text = "Waiting For RFID Read"

                  TimeToAbortRFIDRead = timeGetTime + 5000
                  Do
                     Application.DoEvents()
                     btnOperatorAbort.Refresh()
                     If timeGetTime > TimeToAbortRFIDRead Then
                        AbortTesting = True
                     End If
                     If AbortTesting = True Then Exit Do
                     delay(5)
                  Loop Until RFIDReadComplete = True

                  'If Testing Should Be Aborted Then
                  If AbortTesting = True Then Exit Do

                  'If RFID Read Completed Correctly Then Confirm Order
                  If RFIDReadComplete = True Then
                     ConfirmOrder() 'Confrim That The Order Was Read And Save All Relvent Data To Send Back To Rehau RFID .dll
                     UpdateRFIDInfoToMainScreen()
                     Me.lblProductionNumber.Text = _oOrder.strPnr.Trim 'Display The Production Number
                     ProductionSequenceNumber = Me.lblProductionNumber.Text
                  End If

                  'Check To Make Sure Part IS Okay To Test Or Has been tested before
                  'Check Status of bytes sent from order in string _oOrder.strGsb
                  If CheckForOKToTestFascia(_oOrder.strGsb, ReturnText) = True Then
                     SensorTestRequired = True
                  Else
                     SensorTestRequired = False
                  End If

                  If SensorTestRequired = True Then
                     'RunTests Based On Which Ones Have Been Decoded On Barcode
                     lblTestResult.Text = "Running Tests Based On RFID"
                     lblTestResult.BackColor = System.Drawing.SystemColors.ActiveCaption
                     lbloverallTestResult.Text = "Testing"
                     lbloverallTestResult.BackColor = System.Drawing.SystemColors.ActiveCaption
                  Else
                     'No Testing Should Be Done On This Bumper
                     lblTestResult.Text = ReturnText
                     lblTestResult.BackColor = Color.Yellow
                     AbortTesting = True
                     Exit Do
                  End If

                  DecodeRFIDBits(_oOrder.strBPtV)  'Decode Order and setup tests accordingly.
               End If

            Case Else
               'RunTests Based On Which Ones Have Been Selected By The User

               lblTestResult.Text = "Waiting For Nest In Position And Start Button"
               lblTestResult.BackColor = Color.Yellow
               If RelayBoardInstalled = True Then
                  Do
                     Application.DoEvents()
                     ScanSwitchValues()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(50)
                  Loop Until StartRestartSwitch() Or StartButtonDoubleClick = True
               End If

               If StartRestartSwitch() Then
                  'Fire Connector Lock Relay
                  RelayBoard.Turn_On_Relay(ConnectorLockRelay)
                  delay(150)
                  RelayBoard.Turn_On_Relay(EngageConnectorPogoPinsRelay)
               End If

               'Clear All Previous Test Results
               InitializeForNewTest()

               If cb_PTS_TestSelected.Checked = True Then
                  If RelayBoardInstalled = True Then
                     'Wait For Operator To Hit The Nest Lock Relase Button
                     lblTestResult.Text = "Waiting For Fixture Nest Lock Release Switch"
                     lblTestResult.BackColor = Color.Yellow
                     Do
                        Application.DoEvents()
                        btnOperatorAbort.Refresh()
                        If AbortTesting = True Then Exit Do
                        delay(250)
                     Loop Until NestLockRelease()

                     If NestLockRelease() Then
                        'Fire Relay 
                        RelayBoard.Turn_On_Relay(NestLockRelay)
                     End If

                     'Wait For Operator To Rotate The Nest
                     lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
                     lblTestResult.BackColor = Color.Yellow
                     Do
                        Application.DoEvents()
                        btnOperatorAbort.Refresh()
                        If AbortTesting = True Then Exit Do
                        delay(250)
                     Loop Until NestInRotatedPosition()

                     If NestInRotatedPosition() Then
                        'Turn Off Relay 
                        RelayBoard.Turn_Off_Relay(NestLockRelay)
                     End If
                  End If
               End If


               StartButtonDoubleClick = False

               Application.DoEvents()


               frmProductionNumber.ShowDialog()
               Application.DoEvents()

               If Me.rbFrontSelected.Checked = True Then  'Set Button So That Front Tests Will Be Tested
                  Me.SetBumperScreen("Front")

                  'Set Bumper Location To Test
                  BumperLocationToTest = "Front"
               End If

               If Me.rbRearSelected.Checked = True Then  'Set Button So That Front Tests Will Be Tested
                  Me.SetBumperScreen("Rear")

                  'Set Bumper Location To Test
                  BumperLocationToTest = "Rear"
               End If

               lblTestResult.Text = "Running Tests Based On User Selection"
               lblTestResult.BackColor = System.Drawing.SystemColors.ActiveCaption
               lbloverallTestResult.Text = "Testing"
               lbloverallTestResult.BackColor = System.Drawing.SystemColors.GradientActiveCaption

         End Select

         'Disable The ScanSwitches Timer
         tmrScanSwitches.Enabled = False

         'turn on start of test timer
         TimeTestStarted = Now

         'If The Hardware Is Installed Then Run These Selected Tests
         If RelayBoardInstalled = True Then
            If cb_HFA_TestSelected.Checked = True And LINBoxInstalled = True Then
               RelayBoard.Turn_On_Relay(LINSensorPowerRelay)
               delay(1500)
            End If
            If cb_PTS_TestSelected.Checked = True Then
               RelayBoard.Turn_On_Relay(BoschSensorPowerRelay)
               delay(500)
            End If
         End If



            'If Testing Should Be Aborted Then
            If AbortTesting = True Then Exit Do

         'Check For PTS Tests Here
         If TBOX_Ver_6_0_Installed = True Then
            If cb_PTS_TestSelected.Checked = True Then
                    Tests.PTSSensorTests(PlatformToTest, BumperLocationToTest, "PTS")
                    delay(1500)
                    Tests.ProductionData(PlatformToTest, BumperLocationToTest, "PTS")
                    delay(1500)
                Else
               If rbRFIDTestModeAutomatic.Checked = True Then
                        Tests.CheckForPTSSensorTest(PlatformToTest, BumperLocationToTest, "PTS")
                        delay(500)
                    End If
            End If
            'If Testing Should Be Aborted Then
            If AbortTesting = True Then Exit Do
         End If

         'If Testing Should Be Aborted Then
         If AbortTesting = True Then Exit Do

         If RelayBoardInstalled = True Then
            If cb_HFA_TestSelected.Checked = True And LINBoxInstalled = True Then
               RelayBoard.Turn_Off_Relay(LINSensorPowerRelay)
               delay(500)
            End If
            If cb_PTS_TestSelected.Checked = True Then
               RelayBoard.Turn_Off_Relay(BoschSensorPowerRelay)
               delay(500)
            End If
         End If


         If cb_LEDLamp_TestSelected.Checked = True Then
            'Run The Turn Signal Lamp Test If Selected
            Tests.LEDLampTest(PlatformToTest, BumperLocationToTest, "LED")
         End If

         'If Testing Should Be Aborted Then
         If AbortTesting = True Then Exit Do

         If cb_CAMERA_TestSelected.Checked = True And BumperLocationToTest = "Front" Then
            'Check For No Camera Harness Installed
            Tests.CameraTest(PlatformToTest, BumperLocationToTest, "CAM")
         End If

         'If Testing Should Be Aborted Then
         If AbortTesting = True Then Exit Do

         If cb_ART_PLATE_TestSelected.Checked = True And BumperLocationToTest = "Front" And cbArtPlateTestDisabled.Checked = False Then
            'Check For No ART Plate Harness Installed
            Tests.ARTPlateTest(PlatformToTest, BumperLocationToTest, "ARTPLATE")
         End If

         'If Testing Should Be Aborted Then
         If AbortTesting = True Then Exit Do

         If cb_TEMP_TestSelected.Checked = True And BumperLocationToTest = "Front" Then
            'Check The Temperature Sensor
            Tests.TemperatureSensorTest(PlatformToTest, BumperLocationToTest, "TEMP")
         End If

         'If Testing Should Be Aborted Then
         If AbortTesting = True Then Exit Do

         '09_25_2022 JME Added For Heater Element
         If rbPlatform_1.Checked = True AndAlso BumperLocationToTest = "Front" Then 'Z296 Is On Platform 1
            'Test The Heating Element
            Tests.HeatingElementTest(PlatformToTest, BumperLocationToTest, "HE")
         End If

         ToolStripStatusLabeTotalTestTime.Text = DateDiff(DateInterval.Second, TimeTestStarted, Now).ToString & " Seconds"
         TimeAndDateTestEnded = Format(Now, "HH:mm") & ", " & Format(Now, "MM/dd/yyyy")

         tmrScanSwitches.Enabled = True

         Select Case Me.rbRFIDTestModeAutomatic.Checked
            Case Is = True
               If Me.Grid_Test.RowCount > 0 Then
                  Me.Grid_Test.Rows(Me.Grid_Test.RowCount - 1).Selected = False
               End If

               Select Case AllTestsPassed
                  Case Is = True
                     'Fasica Is Good
                     'Show Results On Screen
                     lbloverallTestResult.Text = "FASCIA PASSED"
                     lbloverallTestResult.BackColor = Color.Lime

                     'Confirm The Results To The Rehau RFID .dll
                     ConfirmRFIDProcessingResults()

                     lblTestResult.Text = "Waiting For Nest In Position Switch And Part In Nest Position Switches To Go Low"
                     'Re-Enable The ScanSwitches Timer

                     WriteDataToSQLDatabase(BumperLocationToTest, ProductionSequenceNumber, "Automatic Test Mode")

                     If RelayBoardInstalled = True Then
                        'Wait For Operator To Hit The Nest Lock Relase Button
                        lblTestResult.Text = "Remove Harness, Releae Lock and Rotate Home"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop Until NestLockRelease()

                        If NestLockRelease() Then
                           'Fire Relay 
                           RelayBoard.Turn_On_Relay(NestLockRelay)
                        End If

                        'Wait For Operator To Rotate The Nest
                        lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated To Home Position"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop Until NestAtHomePosition()

                        If NestAtHomePosition() Then
                           'Turn Off Relay's
                           RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
                           RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
                           RelayBoard.Turn_Off_Relay(NestLockRelay)
                        End If

                        'Wait For Operator To Remove The Part
                        lblTestResult.Text = "Waiting For The Part To Be Removed"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop While PartInPlace()

                     End If


                     'Clear All Previous Tests Check Boxes Before Decoding Of new RFID String
                     ClearTestCheckBoxes()
                     'Reset Flag To False If Fascia Passed (Could Be set To True From Retest)
                     RetestFailedFascia = False

                  Case Is = False
                     'Fasica Is No Good
                     'Show Results On Screen
                     lblAbortTestMessage.SendToBack()
                     lbloverallTestResult.Text = "FASCIA FAILED"
                     lbloverallTestResult.BackColor = Color.Crimson
                     Support.PrintPartLabel(Now, ProductionSequenceNumber, FailString)

                     'Putup Retest Or Fail Message On Screen
                     lblTestResult.Text = "If You Wish To Retest This Fasica, Click Cancel or Reset On The JITMont Screen Then Press The Start Test Switch To quit Testing Hit The Abort Button"
                     Do
                        'Wait For Start /Restart Switch Or Wait For Part And Nest In Position To Go Low
                        Application.DoEvents()
                        If StartRestartSwitch() = True Or StartButtonDoubleClick = True Then
                           RetestFailedFascia = True
                           'Clear Test Screen,  Clear Previous Measurements ect..)
                           InitializeForNewTest()
                           StartButtonDoubleClick = False
                           Exit Do
                        End If
                        If AbortTesting = True Then Exit Do
                     Loop

                     If RelayBoardInstalled = True AndAlso RetestFailedFascia = False Then
                        'If Retest Is Not Wanted Then
                        AbortTesting = False
                        AbortButtonPressed = False
                        'Wait For Operator To Hit The Nest Lock Relase Button
                        lblTestResult.Text = "Waiting For Fixture Nest Lock Release Switch"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop Until NestLockRelease()

                        If NestLockRelease() Then
                           'Fire Relay 
                           RelayBoard.Turn_On_Relay(NestLockRelay)
                        End If

                        'Wait For Operator To Rotate The Nest
                        lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop Until NestAtHomePosition()

                        If NestAtHomePosition() Then
                           RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
                           RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
                           RelayBoard.Turn_Off_Relay(NestLockRelay)
                        End If

                        'Wait For Operator To Remove The Part
                        lblTestResult.Text = "Waiting For The Part To Be Removed"
                        lblTestResult.BackColor = Color.Yellow
                        Do
                           Application.DoEvents()
                           btnOperatorAbort.Refresh()
                           If AbortTesting = True Then Exit Do
                           delay(250)
                        Loop While PartInPlace()
                        'Clear All Previous Tests Check Boxes Before Decoding Of new RFID String
                        ClearTestCheckBoxes()
                     End If
               End Select

            Case Is = False 'Manual Mode Selected
               If Me.Grid_Test.RowCount > 0 Then
                  Me.Grid_Test.Rows(Me.Grid_Test.RowCount - 1).Selected = False
               End If
               Select Case AllTestsPassed
                  Case Is = True
                     'Fasica Is Good
                     'Show Results On Screen
                     lbloverallTestResult.Text = "FASCIA PASSED"
                     lbloverallTestResult.BackColor = Color.Lime
                     WriteDataToSQLDatabase(BumperLocationToTest, ProductionSequenceNumber, "Emergency Test Mode")
                     If RelayBoardInstalled = True Then
                        If cb_PTS_TestSelected.Checked = True Then
                           'Wait For Operator To Hit The Nest Lock Relase Button
                           lblTestResult.Text = "Disconnect Harness, Release Lock and Rotate Home"
                           lblTestResult.BackColor = Color.Yellow
                           Do
                              Application.DoEvents()
                              btnOperatorAbort.Refresh()
                              If AbortTesting = True Then Exit Do
                              delay(250)
                           Loop Until NestLockRelease()

                           If NestLockRelease() Then
                              'Fire Relay 
                              RelayBoard.Turn_On_Relay(NestLockRelay)
                           End If

                           'Wait For Operator To Rotate The Nest
                           lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
                           lblTestResult.BackColor = Color.Yellow
                           Do
                              Application.DoEvents()
                              btnOperatorAbort.Refresh()
                              If AbortTesting = True Then Exit Do
                              delay(250)
                           Loop Until NestAtHomePosition()
                        End If
                        If NestAtHomePosition() Then
                           RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
                           RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
                           RelayBoard.Turn_Off_Relay(NestLockRelay)
                        End If
                     End If

                  Case Is = False
                     'Fasica Is No Good
                     'Show Results On Screen
                     lbloverallTestResult.Text = "FASCIA FAILED"
                     lbloverallTestResult.BackColor = Color.Crimson
                     Support.PrintPartLabel(Now, ProductionSequenceNumber, FailString)

                     WriteDataToSQLDatabase(BumperLocationToTest, ProductionSequenceNumber, "Emergency Test Mode")
                     If RelayBoardInstalled = True Then
                        If cb_PTS_TestSelected.Checked = True Or rbNoRadarSensors.Checked = False Then
                           'Wait For Operator To Hit The Nest Lock Relase Button
                           lblTestResult.Text = "Waiting For Fixture Nest Lock Release Switch"
                           lblTestResult.BackColor = Color.Yellow
                           Do
                              Application.DoEvents()
                              btnOperatorAbort.Refresh()
                              If AbortTesting = True Then Exit Do
                              delay(250)
                           Loop Until NestLockRelease()

                           If NestLockRelease() Then
                              'Fire Relay 
                              RelayBoard.Turn_On_Relay(NestLockRelay)
                           End If

                           'Wait For Operator To Rotate The Nest
                           lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
                           lblTestResult.BackColor = Color.Yellow
                           Do
                              Application.DoEvents()
                              btnOperatorAbort.Refresh()
                              If AbortTesting = True Then Exit Do
                              delay(250)
                           Loop Until NestAtHomePosition()
                        End If
                        If NestAtHomePosition() Then
                           RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
                           RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
                           RelayBoard.Turn_Off_Relay(NestLockRelay)
                        End If
                     End If
               End Select
               lblAbortTestMessage.SendToBack()
               lblTestResult.Text = "Waiting For Next Manual Test Or RFID In Automatic Mode"
               lblTestResult.BackColor = Color.Yellow
               btnStartTesting.Enabled = True
               btnStopTesting.Enabled = False
               tmrScanSwitches.Enabled = True
               lblTestResult.Text = "Waiting For Start Button To Go Low"
               lblTestResult.BackColor = Color.Yellow
               If RelayBoardInstalled = True Then
                  Do
                     Application.DoEvents()
                     ScanSwitchValues()
                     btnOperatorAbort.Refresh()
                     If AbortTesting = True Then Exit Do
                     delay(50)
                  Loop Until StartRestartSwitch() = False
               End If
         End Select

         'If No Relay Board Then Only Run Loop Once
         If RelayBoardInstalled = False Then
            TimeToStopTesting = True
            lblTestResult.Text = "Waiting For Next Manual Test Or Start Switch To Be Pressed"
         Else
            RelayBoard.Turn_Off_All_Relays_Fast()
         End If

      Loop Until TimeToStopTesting = True

      Sensor_BackgroundWorker.CancelAsync()
      'Re-Enable The ScanSwitches Timer
      tmrScanSwitches.Enabled = True
      Application.DoEvents()

      If AbortTesting = True Then
         AbortTesting = False
         If RelayBoardInstalled = True Then
            If NestAtHomePosition() = False Then
               'Wait For Operator To Hit The Nest Lock Relase Button
               lblTestResult.Text = "Waiting For Fixture Nest Lock Release Switch"
               lblTestResult.BackColor = Color.Yellow
               Do
                  Application.DoEvents()
                  btnOperatorAbort.Refresh()
                  If AbortTesting = True Then Exit Do
                  delay(250)
               Loop Until NestLockRelease()

               If NestLockRelease() Then
                  'Fire Relay 
                  RelayBoard.Turn_On_Relay(NestLockRelay)
               End If

               'Wait For Operator To Rotate The Nest
               lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
               lblTestResult.BackColor = Color.Yellow
               Do
                  Application.DoEvents()
                  btnOperatorAbort.Refresh()
                  If AbortTesting = True Then Exit Do
                  delay(250)
               Loop Until NestAtHomePosition()
            End If
            If NestAtHomePosition() Then
               'Turn Off Relay's
               RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
               RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
               RelayBoard.Turn_Off_Relay(NestLockRelay)
            End If
         End If

         If RelayBoardInstalled = True Then
            'Turn Off Any Relays On From Previous Tests If Any
            RelayBoard.Turn_Off_All_Relays()
         End If

         lblAbortTestMessage.SendToBack()
         lblTestResult.Text = "Test Was Aborted"
         lblTestResult.BackColor = Color.Crimson
         lbloverallTestResult.Text = "FASCIA FAILED"
         lbloverallTestResult.BackColor = Color.Crimson

         Sensor_BackgroundWorker.CancelAsync()
         'Force Stop Test
         btnStartTesting.Enabled = True
         btnStopTesting.Enabled = False
         TimeToStopTesting = True
         AbortTesting = False
      End If

   End Sub


   Private Sub btnStopTesting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopTesting.Click
      btnStartTesting.Enabled = True
      btnStopTesting.Enabled = False
      TimeToStopTesting = True
   End Sub

   Sub ClearTestGrid()
      Me.Grid_Test.RowCount = 0
      RowIndex = 0
   End Sub

   Sub InitializeForNewTest()
      'Clears the test screen of prior test results, clears test variables ect..

      'Clear the test grid
      ClearTestGrid()
      Me.Refresh()

      'Clear Out Test Variables
      AllTestsPassed = True
      ToolStripStatusLabeTotalTestTime.Text = ""
      Me.lblProductionNumber.Text = ""
      lbloverallTestResult.Text = "Wating"
      lbloverallTestResult.BackColor = Color.WhiteSmoke
      ClearBumperScreen("Front")
      Application.DoEvents()
      ClearBumperScreen("Rear")
      Application.DoEvents()
      pbBumper.Image = Nothing
      'Set All TestResults To ? Before Test Runs
      txtMsg.Clear()
      ResetTestResultsRFID()

      Application.DoEvents()

   End Sub


   Private Sub Grid_Test_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Grid_Test.RowsAdded
      RowIndex = RowIndex + 1
      'Scroll to the last row.
      Me.Grid_Test.FirstDisplayedScrollingRowIndex = Me.Grid_Test.RowCount - 1
      'Select the last row.
      Me.Grid_Test.Rows(Me.Grid_Test.RowCount - 1).Selected = True
   End Sub

   Sub DataLogLabelInformation(ByVal TimeAndDate As String, ByVal BumperLocationTested As String, ByVal ProductNumber As String)
      'Logs data when a label is printed
      Dim DayToChangeFile As System.DateTime
      Dim FileCreationDate As System.DateTime
      Dim timetochange As System.DateTime
      Dim timerightnow As System.DateTime
      Dim datetochange As System.DateTime
      Dim daterightnow As System.DateTime
      Dim FileDate As System.DateTime
      Dim FileNameDate As String
      Dim DataLogString As String = ""
      Dim WriteHeaderString As Boolean = False

      'Create File Name Using Day Of Year And The Year
      FileDate = Now
      FileNameDate = (FileDate.Month.ToString & "_" & FileDate.Year.ToString)
      DataLogPassLabelFileName = "C:\Logs\DataLog_" + FileNameDate + ".CSV"

      Dim aStringWriter As TextWriter
      aStringWriter = New StringWriter()

      'Convert Saved Strings To DateTime Formated Variables So That They Can Be Used To Compare
      timetochange = TimeToChangeFile
      timerightnow = Format(Now, "t")

      datetochange = strDayToChangeFile
      daterightnow = Format(Now, "long date")

      On Error Resume Next
      'Check To See If Directory exsists if not create it
      If Dir(DefaultLabelDataFilePath, vbDirectory) = "" Then
         MkDir(DefaultLabelDataFilePath)
      End If

      If File.Exists(DataLogPassLabelFileName) = False Then
         'If the File is not there then we will want to put a header in the file
         WriteHeaderString = True
      End If

      'Save The Information To The Data Log File
      aStringWriter = File.AppendText(DataLogPassLabelFileName)
      If WriteHeaderString = True Then
         aStringWriter.WriteLine(HeaderString)
      End If
      aStringWriter.WriteLine(DataString)
      aStringWriter.Flush()
      aStringWriter.Close()

   End Sub
   Sub WriteDataToSQLDatabase(ByVal BumperLocationTested As String, ByVal ProductNumber As String, ByVal TestMode As String)
      'Setup Temporary Strings To Save Data Into
      Dim Fascia_Number_Tested As SqlString = "NA"
      Dim Fascia_Type_Tested As SqlString = "NA"
      Dim Test_Date As SqlString = "NA"
      Dim Test_Time As SqlString = "NA"
      Dim Overall_Result As SqlString = "NA"

      Dim PTS_LH_OTB As SqlString = "NA,NA,NA,NA"
      Dim PTS_LH As SqlString = "NA,NA,NA,NA"
      Dim PTS_LH_CTR As SqlString = "NA,NA,NA,NA"
      Dim PTS_RH_CTR As SqlString = "NA,NA,NA,NA"
      Dim PTS_RH As SqlString = "NA,NA,NA,NA"
      Dim PTS_RH_OTB As SqlString = "NA,NA,NA,NA"

      Dim Brose_HFA_Electrode_Test As SqlString = "NA,NA,NA,NA"
      Dim Brose_HFA_BIST_Test As SqlString = "NA,NA,NA,NA"
      Dim Brose_HFA_ProductionData_Test As SqlString = "NA,NA,NA,NA"
      Dim LED_Lamps_Test As SqlString = "NA,NA,NA,NA,NA,NA,NA,NA"
      Dim Temp_Test As SqlString = "NA,NA,NA,NA"
      Dim Camera_Test As SqlString = "NA,NA,NA,NA"
      Dim ARTPlate_Test As SqlString = "NA,NA,NA,NA"
        Dim HeatingElement_Test As SqlString = "NA,NA,NA,NA"     '09_25_2022 Added For Heating Element Test

        Dim PTS_LH_OTB_YEAR As SqlString = "NA"
        Dim PTS_LH_OTB_MONTH As SqlString = "NA"
        Dim PTS_LH_OTB_DAY As SqlString = "NA"
        Dim PTS_LH_OTB_LINE As SqlString = "NA"
        Dim PTS_LH_OTB_PRODNUM As SqlString = "NA"
        Dim PTS_LH_OTB_ROM As SqlString = "NA"
        Dim PTS_LH_YEAR As SqlString = "NA"
        Dim PTS_LH_MONTH As SqlString = "NA"
        Dim PTS_LH_DAY As SqlString = "NA"
        Dim PTS_LH_LINE As SqlString = "Na"
        Dim PTS_LH_PRODNUM As SqlString = "NA"
        Dim PTS_LH_ROM As SqlString = "NA"
        Dim PTS_LH_CTR_YEAR As SqlString = "NA"
        Dim PTS_LH_CTR_MONTH As SqlString = "NA"
        Dim PTS_LH_CTR_DAY As SqlString = "NA"
        Dim PTS_LH_CTR_LINE As SqlString = "NA"
        Dim PTS_LH_CTR_PRODNUM As SqlString = "NA"
        Dim PTS_LH_CTR_ROM As SqlString = "NA"
        Dim PTS_RH_CTR_YEAR As SqlString = "NA"
        Dim PTS_RH_CTR_MONTH As SqlString = "NA"
        Dim PTS_RH_CTR_DAY As SqlString = "NA"
        Dim PTS_RH_CTR_LINE As SqlString = "NA"
        Dim PTS_RH_CTR_PRODNUM As SqlString = "NA"
        Dim PTS_RH_CTR_ROM As SqlString = "NA"
        Dim PTS_RH_YEAR As SqlString = "NA"
        Dim PTS_RH_MONTH As SqlString = "NA"
        Dim PTS_RH_DAY As SqlString = "NA"
        Dim PTS_RH_LINE As SqlString = "NA"
        Dim PTS_RH_PRODNUM As SqlString = "NA"
        Dim PTS_RH_ROM As SqlString = "NA"
        Dim PTS_RH_OTB_YEAR As SqlString = "NA"
        Dim PTS_RH_OTB_MONTH As SqlString = "NA"
        Dim PTS_RH_OTB_DAY As SqlString = "NA"
        Dim PTS_RH_OTB_LINE As SqlString = "NA"
        Dim PTS_RH_OTB_PRODNUM As SqlString = "NA"
        Dim PTS_RH_OTB_ROM As SqlString = "NA"


        'Transfer TestResults to SQL Strings before write (Test Lenght of Strings So Write Does Not Crash)
        Fascia_Number_Tested = ProductNumber

      Select Case Me.rbBasic.Checked = True
         Case True
            Fascia_Type_Tested = BumperLocationTested & "_BASIC_" & PlatformToTest
         Case Else
      End Select

      Select Case Me.rbAMG.Checked = True
         Case True
            Fascia_Type_Tested = BumperLocationTested & "_AMG_" & PlatformToTest
         Case Else
      End Select

      Select Case Me.rbPlatform_1.Checked = True
         Case True
            Fascia_Type_Tested = BumperLocationTested & "_MAYBACH_" & PlatformToTest
         Case Else
      End Select

      Test_Date = Now.Date.ToShortDateString

      Test_Time = Now.ToShortTimeString

      Overall_Result = "Pass" 'Assume Pass Becuase We Are Only Saving Pass Data

      If cb_PTS_TestSelected.Checked = True Then
         'Parse Out Test Result Strings For PTS Sensor Tests
         Dim NumberOfSensors As Integer = 0
         NumberOfSensors = 6

         For NumberOfLoops As Integer = 0 To NumberOfSensors - 1
            With PTSTestVariables(NumberOfLoops)
               Select Case NumberOfSensors
                  Case 6
                     Select Case NumberOfLoops
                        Case 0
                           PTS_LH_OTB = .TestResultString
                        Case 1
                           PTS_LH = .TestResultString
                        Case 2
                           PTS_LH_CTR = .TestResultString
                        Case 3
                           PTS_RH_CTR = .TestResultString
                        Case 4
                           PTS_RH = .TestResultString
                        Case 5
                           PTS_RH_OTB = .TestResultString
                     End Select
                  Case 4
                     Select Case NumberOfLoops
                        Case 0
                           PTS_LH = .TestResultString
                        Case 1
                           PTS_LH_CTR = .TestResultString
                        Case 2
                           PTS_RH_CTR = .TestResultString
                        Case 3
                           PTS_RH = .TestResultString
                     End Select

                  Case Else

               End Select
            End With
         Next
      Else

      End If

      If cb_HFA_TestSelected.Checked = True Then
         If Brose_HFA_ElectrodeTestVariables(0).TestResultString <> "" Then
            Brose_HFA_Electrode_Test = Brose_HFA_ElectrodeTestVariables(0).TestResultString
         End If

         If Brose_HFA_BISTTestVariables(0).TestResultString <> "" Then
            Brose_HFA_BIST_Test = Brose_HFA_BISTTestVariables(0).TestResultString
         End If

         If Brose_HFA_ProductionDataTestVariables(0).TestResultString <> "" Then
            Brose_HFA_ProductionData_Test = Brose_HFA_ProductionDataTestVariables(0).TestResultString
         End If

      End If

      If cb_TEMP_TestSelected.Checked = True Then
         If TempereatureSensorTestVariables(0).TestResultString <> "" Then
            Temp_Test = TempereatureSensorTestVariables(0).TestResultString
         End If
      End If

      If cb_CAMERA_TestSelected.Checked = True Then
         If CameraTestVariables(0).TestResultString <> "" Then
            Camera_Test = CameraTestVariables(0).TestResultString
         End If
      End If

      If cb_ART_PLATE_TestSelected.Checked = True Then
         If ARTPlateTestVariables(0).TestResultString <> "" Then
            ARTPlate_Test = ARTPlateTestVariables(0).TestResultString
         End If
      End If

      If cb_HeatingElement.Checked = True Then '09_25_2022 Added For Heating Element Test
         If HeatingElementTestVariables(0).TestResultString <> "" Then
            HeatingElement_Test = HeatingElementTestVariables(0).TestResultString
         End If
      End If

      Dim LeftHandLamp As Integer = 0
      Dim RightHandLamp As Integer = 1
      'If Turn Signal Test Was Run Then Save The Test Data
      If cb_LEDLamp_TestSelected.Checked = True Then
         LED_Lamps_Test = LED_LampTestVariables(LeftHandLamp).TestResultString & "," & LED_LampTestVariables(RightHandLamp).TestResultString
      End If

      'Create a Header String For Use When Writing To The File
      HeaderString.Length = 0
      HeaderString.Append("Fascia_Number_Tested,")
      HeaderString.Append("Fascia_Type_Tested,")
      HeaderString.Append("Test_Date,")
      HeaderString.Append("Test_Time,")
      HeaderString.Append("Test_Mode,")
      HeaderString.Append("Overall_Test_Result,")

      HeaderString.Append("PTS_LH_OTB:Result,")
      HeaderString.Append("PTS_LH_OTB:MIN,")
      HeaderString.Append("PTS_LH_OTB:MAX,")
      HeaderString.Append("PTS_LH_OTB:Measured Value,")

      HeaderString.Append("PTS_LH:Result,")
      HeaderString.Append("PTS_LH:MIN,")
      HeaderString.Append("PTS_LH:MAX,")
      HeaderString.Append("PTS_LH:Measured Value,")

      HeaderString.Append("PTS_LH_CTR:Result,")
      HeaderString.Append("PTS_LH_CTR:MIN,")
      HeaderString.Append("PTS_LH_CTR:MAX,")
      HeaderString.Append("PTS_LH_CTR:Measured Value,")

      HeaderString.Append("PTS_RH_CTR:Result,")
      HeaderString.Append("PTS_RH_CTR:MIN,")
      HeaderString.Append("PTS_RH_CTR:MAX,")
      HeaderString.Append("PTS_RH_CTR:Measured Value,")

      HeaderString.Append("PTS_RH:Result,")
      HeaderString.Append("PTS_RH:MIN,")
      HeaderString.Append("PTS_RH:MAX,")
      HeaderString.Append("PTS_RH:Measured Value,")

      HeaderString.Append("PTS_RH_OTB:Result,")
      HeaderString.Append("PTS_RH_OTB:MIN,")
      HeaderString.Append("PTS_RH_OTB:MAX,")
      HeaderString.Append("PTS_RH_OTB:Measured Value,")

      HeaderString.Append("HFA_Electrode_Test:Result,")
      HeaderString.Append("HFA_Electrode_Test:MIN,")
      HeaderString.Append("HFA_Electrode_Test:MAX,")
      HeaderString.Append("HFA_Electrode_Test:Measured Value,")

      HeaderString.Append("HFA_BIST_Test:Result,")
      HeaderString.Append("HFA_BIST_Test:MIN,")
      HeaderString.Append("HFA_BIST_Test:MAX,")
      HeaderString.Append("HFA_BIST_Test:Measured Value,")

      HeaderString.Append("HFA_ProductionData_Test:Result,")
      HeaderString.Append("HFA_ProductionData_Test:MIN,")
      HeaderString.Append("HFA_ProductionData_Test:MAX,")
      HeaderString.Append("HFA_ProductionData_Test:Measured Value,")

      HeaderString.Append("LED_DRIVER:Result,")
      HeaderString.Append("LED_DRIVER:MIN,")
      HeaderString.Append("LED_DRIVER:MAX,")
      HeaderString.Append("LED_DRIVER:Measured Value,")

      HeaderString.Append("LED_PASSENGER:Result,")
      HeaderString.Append("LED_PASSENGER:MIN,")
      HeaderString.Append("LED_PASSENGER:MAX,")
      HeaderString.Append("LED_PASSENGER:Measured Value,")

      HeaderString.Append("TEMP_SENSOR:Result,")
      HeaderString.Append("TEMP_SENSOR:MIN,")
      HeaderString.Append("TEMP_SENSOR:MAX,")
      HeaderString.Append("TEMP_SENSOR:Measured Value,")

      HeaderString.Append("CAMERA_HARNESS:Result,")
      HeaderString.Append("CAMERA_HARNESS:MIN,")
      HeaderString.Append("CAMERA_HARNESS:MAX,")
      HeaderString.Append("CAMERA_HARNESS:Measured Value,")

      HeaderString.Append("ART_PALATE_HARNESS:Result,")
      HeaderString.Append("ART_PALATE_HARNESS:MIN,")
      HeaderString.Append("ART_PALATE_HARNESS:MAX,")
      HeaderString.Append("ART_PALATE_HARNESS:Measured Value,")

      HeaderString.Append("HEATING_ELEMENT:Result,") '09_25_2022 Added For Heating Element Test
      HeaderString.Append("HEATING_ELEMENT:MIN,")
      HeaderString.Append("HEATING_ELEMENT:MAX,")
        HeaderString.Append("HEATING_ELEMENT:Measured Value,")

        HeaderString.Append("PTS_LH_OTB:Year,")
        HeaderString.Append("PTS_LH_OTB:Month,")
        HeaderString.Append("PTS_LH_OTB:Day,")
        HeaderString.Append("PTS_LH_OTB:Line,")
        HeaderString.Append("PTS_LH_OTB:Production #,")
        HeaderString.Append("PTS_LH_OTB:ROM,")

        HeaderString.Append("PTS_LH:Year,")
        HeaderString.Append("PTS_LH:Month,")
        HeaderString.Append("PTS_LH:Day,")
        HeaderString.Append("PTS_LH:Line,")
        HeaderString.Append("PTS_LH:Production #,")
        HeaderString.Append("PTS_LH:ROM,")

        HeaderString.Append("PTS_LH_CTR:Year,")
        HeaderString.Append("PTS_LH_CTR:Month,")
        HeaderString.Append("PTS_LH_CTR:Day,")
        HeaderString.Append("PTS_LH_CTR:Line,")
        HeaderString.Append("PTS_LH_CTR:Production #,")
        HeaderString.Append("PTS_LH_CTR:ROM,")

        HeaderString.Append("PTS_RH_CTR:Year,")
        HeaderString.Append("PTS_RH_CTR:Month,")
        HeaderString.Append("PTS_RH_CTR:Day,")
        HeaderString.Append("PTS_RH_CTR:Line,")
        HeaderString.Append("PTS_RH_CTR:Production #,")
        HeaderString.Append("PTS_RH_CTR:ROM,")

        HeaderString.Append("PTS_RH:Year,")
        HeaderString.Append("PTS_RH:Month,")
        HeaderString.Append("PTS_RH:Day,")
        HeaderString.Append("PTS_RH:Line,")
        HeaderString.Append("PTS_RH:Production #,")
        HeaderString.Append("PTS_RH:ROM,")

        HeaderString.Append("PTS_RH_OTB:Year,")
        HeaderString.Append("PTS_RH_OTB:Month,")
        HeaderString.Append("PTS_RH_OTB:Day,")
        HeaderString.Append("PTS_RH_OTB:Line,")
        HeaderString.Append("PTS_RH_OTB:Production #,")
        HeaderString.Append("PTS_RH_OTB:ROM,")

        DataString.Length = 0
        DataString.Append(Fascia_Number_Tested + ",")
      DataString.Append(Fascia_Type_Tested + ",")
      DataString.Append(Test_Date + ",")
      DataString.Append(Test_Time + ",")
      DataString.Append(TestMode + ",")
      DataString.Append(Overall_Result + ",")
      DataString.Append(PTS_LH_OTB + ",")
      DataString.Append(PTS_LH + ",")
      DataString.Append(PTS_LH_CTR + ",")
      DataString.Append(PTS_RH_CTR + ",")
      DataString.Append(PTS_RH + ",")
      DataString.Append(PTS_RH_OTB + ",")
      DataString.Append(Brose_HFA_Electrode_Test + ",")
      DataString.Append(Brose_HFA_BIST_Test + ",")
      DataString.Append(Brose_HFA_ProductionData_Test + ",")
      DataString.Append(LED_Lamps_Test + ",")
      DataString.Append(Temp_Test + ",")
      DataString.Append(Camera_Test + ",")
      DataString.Append(ARTPlate_Test + ",")
        DataString.Append(HeatingElement_Test + ",") '09_25_2022 Added For Heating Element Test

        DataString.Append(PTS_LH_OTB_YEAR + ",")
        DataString.Append(PTS_LH_OTB_MONTH + ",")
        DataString.Append(PTS_LH_OTB_DAY + ",")
        DataString.Append(PTS_LH_OTB_LINE + ",")
        DataString.Append(PTS_LH_OTB_PRODNUM + ",")
        DataString.Append(PTS_LH_OTB_ROM + ",")

        DataString.Append(PTS_LH_YEAR + ",")
        DataString.Append(PTS_LH_MONTH + ",")
        DataString.Append(PTS_LH_DAY + ",")
        DataString.Append(PTS_LH_LINE + ",")
        DataString.Append(PTS_LH_PRODNUM + ",")
        DataString.Append(PTS_LH_ROM + ",")

        DataString.Append(PTS_LH_CTR_YEAR + ",")
        DataString.Append(PTS_LH_CTR_MONTH + ",")
        DataString.Append(PTS_LH_CTR_DAY + ",")
        DataString.Append(PTS_LH_CTR_LINE + ",")
        DataString.Append(PTS_LH_CTR_PRODNUM + ",")
        DataString.Append(PTS_LH_CTR_ROM + ",")

        DataString.Append(PTS_RH_CTR_YEAR + ",")
        DataString.Append(PTS_RH_CTR_MONTH + ",")
        DataString.Append(PTS_RH_CTR_DAY + ",")
        DataString.Append(PTS_RH_CTR_LINE + ",")
        DataString.Append(PTS_RH_CTR_PRODNUM + ",")
        DataString.Append(PTS_RH_CTR_ROM + ",")

        DataString.Append(PTS_RH_YEAR + ",")
        DataString.Append(PTS_RH_MONTH + ",")
        DataString.Append(PTS_RH_DAY + ",")
        DataString.Append(PTS_RH_LINE + ",")
        DataString.Append(PTS_RH_PRODNUM + ",")
        DataString.Append(PTS_RH_ROM + ",")

        DataString.Append(PTS_RH_OTB_YEAR + ",")
        DataString.Append(PTS_RH_OTB_MONTH + ",")
        DataString.Append(PTS_RH_OTB_DAY + ",")
        DataString.Append(PTS_RH_OTB_LINE + ",")
        DataString.Append(PTS_RH_OTB_PRODNUM + ",")
        DataString.Append(PTS_RH_OTB_ROM + ",")


        'Save data to CSV file SQL Database No Longer Used
        DataLogLabelInformation(TimeAndDateTestEnded, BumperLocationToTest, ProductNumber)

   End Sub

   Private Sub btnOperatorAbort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOperatorAbort.Click
      AbortTesting = True
      AbortButtonPressed = True
      If TimeToStopTesting = False And AbortButtonPressed = True Then
         lblAbortTestMessage.BringToFront()
      End If

   End Sub

   Sub SetBumperScreen(ByVal BumberLocationToSet As String)

      'Removed Temproaryly //////////////////////////////////
      Dim SectionName As String = "Symbol_Locations"
      'Determine The Bumper Name Based On The Platform Selected On The Main Screen
      Dim BumperPictureFileName As String = DefaultBumperPictureFilePath
      If rbNoneSelected.Checked = True Then
         'Use Default Picture And .ini until correct value is seen
         BumperPictureFileName = BumperPictureFileName & "Default"
      Else
         If rbPlatform_1.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_1.Text
            TBOXBumperConfiguration = rbPlatform_1.Text
         ElseIf rbPlatform_2.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_2.Text
            TBOXBumperConfiguration = rbPlatform_2.Text
         ElseIf rbPlatform_3.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_3.Text
            TBOXBumperConfiguration = rbPlatform_3.Text
         ElseIf rbPlatform_4.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_4.Text
            TBOXBumperConfiguration = rbPlatform_4.Text
         ElseIf rbPlatform_5.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_5.Text
            TBOXBumperConfiguration = rbPlatform_5.Text
         End If
      End If
      'Determine If It Is A Front Or Rear Bumper And Then Add That To The File Name
      If rbFrontSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_Front"
         If TBOXBumperConfiguration.Contains("_FRONT") Then
         Else
            TBOXBumperConfiguration = TBOXBumperConfiguration & "_FRONT"
         End If

      ElseIf rbRearSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_Rear"
         If TBOXBumperConfiguration.Contains("_REAR") Then
         Else
            TBOXBumperConfiguration = TBOXBumperConfiguration & "_REAR"
         End If

      End If

      'Determine Bumper And Then Add That To The File Name
      If rbBasic.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_BASIC"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_BASIC_TX"
      ElseIf rbAMG.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_AMG"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_AMG_TX"
      ElseIf rbPlatform_1.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_MAYBACH"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_MAYBACH_TX"
      ElseIf rbNoneSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & ""
      End If
      If BumperPictureFileName.Contains("Platform_1") Then
         'Set A Defaul Value For TBOX So It Does Not Fail On Load
         TBOXBumperConfiguration = ""
         TBOXBumperConfiguration = "X296_FRONT_BASIC_TX"
         Exit Sub
      End If
      Try
         pbBumper.Image = Image.FromFile(BumperPictureFileName & ".png")
         pbBumper.SendToBack()
         lblAbortTestMessage.SendToBack()
      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The RFID Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

      BumperPictureFileName = BumperPictureFileName & ".ini"

      Application.DoEvents()

      'cbHFATestSelected
      If cb_HFA_TestSelected.Checked Then
         'Set The HFA Properties
         lbl_HFA.Visible = True
         lbl_HFA.Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_HFA_Left")))
         lbl_HFA.Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_HFA_TOP")))
         lbl_HFA.BackColor = Color.LightGray
      Else
         lbl_HFA.Visible = False
      End If

      Select Case cb_TEMP_TestSelected.Checked
         Case True
            'Set The Temp Sensor Properties
            With lbl_TEMP
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_TEMP_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_TEMP_TOP")))
               .BackColor = Color.LightGray
            End With

         Case Else
            With lbl_TEMP
               .Visible = False
            End With

      End Select

      Select Case cb_HeatingElement.Checked Or rbPlatform_1.Checked
         Case True
            'Set The Heating Element Properties
            With lbl_Heating_Element
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_HEATINGELEMENT_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_HEATINGELEMENT_TOP")))
               .BackColor = Color.LightGray
            End With

         Case Else
            With lbl_Heating_Element
               .Visible = False
            End With

      End Select

      Select Case cb_PTS_TestSelected.Checked
         Case True
            'Set The Right Hand Out board PTS Sensor Properties
            With lbl_PTS_RH_OB
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS12"
               Else
                  .Text = "PTS6"
               End If
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_OB_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_OB_TOP")))
               .BackColor = Color.LightGray
            End With

            With osPTS_Front_RH_OB
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_Front_RH_OB_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_Front_RH_OB_TOP")))
               .BackColor = Color.LightGray
            End With

            With lbl_PTS_LH_OB
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS7"
               Else
                  .Text = "PTS1"
               End If
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_OB_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_OB_TOP")))
               .BackColor = Color.LightGray
            End With

            With osPTS_Front_LH_OB
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_Front_LH_OB_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_Front_LH_OB_TOP")))
               .BackColor = Color.LightGray
            End With

            With lbl_PTS_RH
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS11"
               Else
                  .Text = "PTS5"
               End If
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_Top")))
               .BackColor = Color.LightGray
            End With

            With osPTS_RH
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_RH_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_RH_TOP")))
               .BackColor = Color.LightGray
            End With

            With lbl_PTS_LH
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS8"
               Else
                  .Text = "PTS2"
               End If
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_Top")))
               .BackColor = Color.LightGray
            End With

            With osPTS_LH
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_LH_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_LH_TOP")))
               .BackColor = Color.LightGray
            End With

            With lbl_PTS_RH_CTR
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS10"
               Else
                  .Text = "PTS4"
               End If

               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_CTR_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_RH_CTR_Top")))
               .BackColor = Color.LightGray
            End With

            With osPTS_RH_CTR
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_RH_CTR_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_RH_CTR_TOP")))
               .BackColor = Color.LightGray
            End With

            With lbl_PTS_LH_CTR
               If rbFrontSelected.Checked = False Then
                  .Text = "PTS9"
               Else
                  .Text = "PTS3"
               End If
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_CTR_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_PTS_LH_CTR_Top")))
               .BackColor = Color.LightGray
            End With

            With osPTS_LH_CTR
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_LH_CTR_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "osPTS_LH_CTR_TOP")))
               .BackColor = Color.LightGray
            End With

         Case Else
            'Set The Right Hand Out board PTS Sensor Properties
            With lbl_PTS_RH_OB
               .Visible = False
            End With
            With osPTS_Front_RH_OB
               .Visible = False
            End With

            'Set The Left Hand Out board PTS Sensor Properties
            With lbl_PTS_LH_OB
               .Visible = False
            End With
            With osPTS_Front_LH_OB
               .Visible = False
            End With

            'Set The Right Hand PTS Sensor Properties
            With lbl_PTS_RH
               .Visible = False
            End With
            With osPTS_RH
               '  .Visible = False
            End With

            'Set The Left Hand PTS Sensor Properties
            With lbl_PTS_LH
               .Visible = False
            End With
            With osPTS_LH
               .Visible = False
            End With

            'Set The Right Hand Center PTS Sensor Properties
            With lbl_PTS_RH_CTR
               .Visible = False
            End With
            With osPTS_RH_CTR
               .Visible = False
            End With

            'Set The Left Hand Center PTS Sensor Properties
            With lbl_PTS_LH_CTR
               .Visible = False
            End With
            With osPTS_LH_CTR
               .Visible = False
            End With

      End Select

      'Set The Right Hand LAMP Properties
      Select Case cb_LEDLamp_TestSelected.Checked
         Case Is = True
            With lbl_LAMP_PASSENGER
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_LAMP_PASSENGER_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_LAMP_PASSENGER_TOP")))
               .BackColor = Color.LightGray
            End With

            'Set The Left Hand LAMP Properties
            With lbl_LAMP_DRIVER
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_LAMP_DRIVER_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_LAMP_DRIVER_Top")))
               .BackColor = Color.LightGray
            End With
         Case Else
            With lbl_LAMP_PASSENGER
               .Visible = False
            End With

            'Set The Left Hand LED Properties
            With lbl_LAMP_DRIVER
               .Visible = False
            End With
      End Select

      Select Case cb_CAMERA_TestSelected.Checked
         Case True
            ''Set The Camera Shape Properties
            With lblCamera
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_Camera_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_Camera_TOP")))
               .BackColor = Color.LightGray
            End With

         Case False
            With lblCamera
               .Visible = False
            End With
      End Select

      Select Case cb_ART_PLATE_TestSelected.Checked
         Case True
            ''Set The Art Plate Shape Properties
            With lblArtPlate
               .Visible = True
               .Left = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_ArtPlate_Left")))
               .Top = Convert.ToInt16(Val(LookUpLocationInfo(SectionName, BumperPictureFileName, "lbl_ArtPlate_TOP")))
               .BackColor = Color.LightGray
            End With

         Case False
            With lblArtPlate
               .Visible = False
            End With
      End Select

   End Sub
   Sub ClearBumperScreen(ByVal BumperScreenToClear As String)

      Select Case BumperScreenToClear
         Case Is = "Front"
            ''Set The Main Screen Picture
            'pbBumper.Image = Fascia_Test_Stand.My.Resources.Front_Bumper

            'Set The FCW Properties
            With lbl_FCW
               .Visible = False
            End With

            'Set The BSM/DTR Right Hand Properties
            With lbl_BSM_DTR_RH
               .Visible = False
            End With

            'Set The BSM/DTR Left Hand Properties
            With lbl_BSM_DTR_LH
               .Visible = False
            End With

            'Set The Right Hand BSM/iBSM Properties
            With lbl_Rear_BSM_iBSM_RH
               .Visible = False
            End With

            'Set The Left Hand BSM/iBSM Properties
            With lbl_Rear_BSM_iBSM_LH
               .Visible = False
            End With

            'Set The Right Hand Out board PTS Sensor Properties
            With lbl_PTS_RH_OB
               .Visible = False
            End With
            'Set The Right Hand Out board PTS Sensor Shape Properties
            With osPTS_Front_RH_OB
               .Visible = False
            End With

            'Set The Left Hand Out board PTS Sensor Properties
            With lbl_PTS_LH_OB
               .Visible = False
            End With
            'Set The Left Hand Out board PTS Sensor Shape Properties
            With osPTS_Front_LH_OB
               .Visible = False
            End With

            'Set The Right Hand PTS Sensor Properties
            With lbl_PTS_RH
               .Visible = False
            End With
            'Set The Right Hand PTS Sensor Shape Properties
            With osPTS_RH
               .Visible = False
            End With

            'Set The Left Hand PTS Sensor Properties
            With lbl_PTS_LH
               .Visible = False
            End With
            'Set The Left Hand PTS Sensor Shape Properties
            With osPTS_LH
               .Visible = False
            End With

            'Set The Right Hand Center PTS Sensor Properties
            With lbl_PTS_RH_CTR
               .Visible = False
            End With
            'Set The Right Hand Center PTS Sensor Shape Properties
            With osPTS_RH_CTR
               .Visible = False
            End With

            'Set The Left Hand Center PTS Sensor Properties
            With lbl_PTS_LH_CTR
               .Visible = False
            End With
            'Set The Left Hand Center PTS Sensor Shape Properties
            With osPTS_LH_CTR
               .Visible = False
            End With

            'Set The HFA Shape Properties
            With lbl_HFA
               .Visible = False
            End With

            'Set The RCW Properties
            With lbl_Rear_RCW
               .Visible = False
            End With

            'Set The Temp Sensor Properties
            With lbl_TEMP
               .Visible = False
            End With

            'Set The Camera Sensor Properties
            With lblCamera
               .Visible = False
            End With

            'Set The ArtPlate Sensor Properties
            With lblArtPlate
               .Visible = False
            End With

            'Set The Heating Element Properties
            With lbl_Heating_Element
               .Visible = False
            End With

         Case Is = "Rear"
            ''Set The Main Screen Picture
            'pbBumper.Image = Fascia_Test_Stand.My.Resources.Rear_Bumper

            'Set The FCW Properties
            With lbl_FCW
               .Visible = False
            End With

            'Set The BSM/DTR Right Hand Properties
            With lbl_BSM_DTR_RH
               .Visible = False
            End With

            'Set The BSM/DTR Left Hand Properties
            With lbl_BSM_DTR_LH
               .Visible = False
            End With

            'Set The Right Hand PTS Sensor Properties
            With lbl_PTS_RH
               .Visible = False
            End With
            'Set The Right Hand PTS Sensor Shape Properties
            With osPTS_RH
               .Visible = False
            End With

            'Set The Left Hand PTS Sensor Properties
            With lbl_PTS_LH
               .Visible = False
            End With
            'Set The Left Hand PTS Sensor Shape Properties
            With osPTS_LH
               .Visible = False
            End With

            'Set The Right Hand Center PTS Sensor Properties
            With lbl_PTS_RH_CTR
               .Visible = False
            End With
            'Set The Right Hand Center PTS Sensor Shape Properties
            With osPTS_RH_CTR
               .Visible = False
            End With

            'Set The Left Hand Center PTS Sensor Properties
            With lbl_PTS_LH_CTR
               .Visible = False
            End With
            'Set The Left Hand Center PTS Sensor Shape Properties
            With osPTS_LH_CTR
               .Visible = False
            End With

            'Set The Right Hand BSM/iBSM Properties
            With lbl_Rear_BSM_iBSM_RH
               .Visible = False
            End With

            'Set The Left Hand BSM/iBSM Properties
            With lbl_Rear_BSM_iBSM_LH
               .Visible = False
            End With

            'Set The HFA Shape Properties
            With lbl_HFA
               .Visible = False
            End With

            'Set The RCW Properties
            With lbl_Rear_RCW
               .Visible = False
            End With

            'Set The Camera Sensor Properties
            With lblCamera
               .Visible = False
            End With

            'Set The ArtPlate Sensor Properties
            With lblArtPlate
               .Visible = False
            End With

            'Set The LED LAMP Properties
            With lbl_LAMP_DRIVER
               .Visible = False
            End With

            'Set The LED LAMP Properties
            With lbl_LAMP_PASSENGER
               .Visible = False
            End With

      End Select
   End Sub

   Function CheckForOKToTestFascia(ByVal StatusOFPart As String, ByRef TextToReturn As String) As Boolean
      Dim StatusStringArray As Char()
      StatusStringArray = StatusOFPart.ToCharArray

      'Check String For The Current Status OF The Part
      'String Positions 1 - 3 are not currently used

      Select Case StatusStringArray(3) 'Zero Based So 4th position is in array location 3
         Case Is = "0"
            'Do Nothing

         Case Is = "1"
            CheckForOKToTestFascia = False
            TextToReturn = "Fasica Has NOT Completed Previous Assembly Steps So Test Sequence Will Be Aborted"
            Exit Function
      End Select

      Select Case StatusStringArray(4) 'Zero Based So 5th position is in array location 4
         Case Is = "0"
            'Do nothing

         Case Is = "1"
            If AllowRetestOfPassedFascia = True Then
               CheckForOKToTestFascia = True
               TextToReturn = "Sensor Test Has Passed Previously And Is Now Being Retested"
               Exit Function
            Else
               CheckForOKToTestFascia = False
               TextToReturn = "Sensor Test Has Passed Previously And Retesting Of Fascias Has Been Denied"
               Exit Function
            End If
      End Select

      Select Case StatusStringArray(5) 'Zero Based So 6th position is in array location 5
         Case Is = "0"
            'Do nothing

         Case Is = "1"
            CheckForOKToTestFascia = False
            TextToReturn = "Fasica Has Failed Previous Assembly Steps So Test Sequence Will Be Aborted"
            Exit Function
      End Select

      Select Case StatusStringArray(6) 'Zero Based So 7th position is in array location 6
         Case Is = "0"
            'Do nothing
         Case Is = "1"
            If AllowRetestOfPassedFascia = True Then
               CheckForOKToTestFascia = True
               TextToReturn = "Sensor Test Has Failed Previously And Is Now Being Retested"
               Exit Function
            Else
               CheckForOKToTestFascia = False
               TextToReturn = "Sensor Test Has Failed Previously And Retesting Of Fascias Has Been Denied"
               Exit Function
            End If
      End Select

      Select Case StatusStringArray(7) 'Zero Based So 8th position is in array location 7
         Case Is = "0"
            CheckForOKToTestFascia = False
            TextToReturn = "Status Unknown Testing Will Abort"

         Case Is = "1"
            CheckForOKToTestFascia = True
            TextToReturn = "Running Sensor Tests For This Fascia"
      End Select


   End Function

   Private Sub Sensor_BackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Sensor_BackgroundWorker.DoWork

      'Starts Test When PreTest Is Called
      SensorPreTestVariables.TestComplete = False
      SensorPreTestVariables.TestResult = "F"
      TBET.TestBumper(SensorPreTestVariables.PlatFormID, SensorPreTestVariables.BumperLocation, TestDisplayModes.AutomaticMode, SensorPreTestVariables.TestResult)

   End Sub

   Private Sub Sensor_BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Sensor_BackgroundWorker.RunWorkerCompleted
      SensorPreTestVariables.TestComplete = True
   End Sub

   Sub ClearTestCheckBoxes()
      'Clear All Test Check Boxes Before New RFID Scan
      cb_HFA_TestSelected.Checked = False
      cb_PTS_TestSelected.Checked = False
      cb_LEDLamp_TestSelected.Checked = False
      cb_CAMERA_TestSelected.Checked = False
      rbNoRadarSensors.Checked = True
      cb_HeatingElement.Checked = False
      cb_TEMP_TestSelected.Checked = False
   End Sub

   Private Sub chassisCodeCB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chassisCodeCB.SelectedIndexChanged
      ChassisCode.Remove(0, ChassisCode.Length)
      ChassisCode.Append(Me.chassisCodeCB.Text)
   End Sub

   Private Sub btnScanSwitches_Click(sender As Object, e As EventArgs) Handles btnScanSwitches.Click
      'Scans The Switch Banks (Calls Routine)

      ScanSwitchValues()

   End Sub

   Private Sub cmdMeasureValeoSensors_Click(sender As Object, e As EventArgs) Handles cmdMeasureValeoSensors.Click
      cmdMeasureValeoSensors.Enabled = False

      'Call The Routine To Measure All Of The Valeo Sensors
      ValeoSensor.MeasureValeoSensors()

      'Re-Enable Button
      cmdMeasureValeoSensors.Enabled = True
   End Sub

   Private Sub tmrScanSwitches_Tick(sender As Object, e As EventArgs) Handles tmrScanSwitches.Tick
      'Call Scan Switches Routine When Not Running Tests
      ScanSwitchValues()
   End Sub

   Private Sub chkMeasureSensorsContinuously_CheckedChanged(sender As Object, e As EventArgs) Handles chkMeasureSensorsContinuously.CheckedChanged
      Select Case chkMeasureSensorsContinuously.Checked
         Case True
            tmrMeasureSensors.Enabled = True
            chkMeasureSensorsContinuously.BackColor = Color.Lime
         Case False
            tmrMeasureSensors.Enabled = False
            chkMeasureSensorsContinuously.BackColor = Color.WhiteSmoke
      End Select
   End Sub

   Private Sub tmrMeasureSensors_Tick(sender As Object, e As EventArgs) Handles tmrMeasureSensors.Tick
      'If Still Running Previous Measurement Then Exit
      If SensorMeasurementInProgress = True Then Exit Sub

      Select Case chkMeasureSensorsContinuously.Checked
         Case True
            ValeoSensor.MeasureValeoSensors()
         Case False

      End Select
   End Sub

   Private Sub rbRearSelected_CheckedChanged(sender As Object, e As EventArgs) Handles rbRearSelected.CheckedChanged
      Select Case rbRearSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
            SetTestLists("Rear")
         Case False

      End Select
   End Sub

   Private Sub rbFrontSelected_CheckedChanged(sender As Object, e As EventArgs) Handles rbFrontSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
            SetTestLists("Front")
         Case False

      End Select
   End Sub

   Private Sub rbRFIDTestModeAutomatic_CheckedChanged(sender As Object, e As EventArgs) Handles rbRFIDTestModeAutomatic.CheckedChanged
      Select Case rbRFIDTestModeAutomatic.Checked
         Case True
            pnlManualMode.Visible = False
            pnlAutomaticMode.Visible = True
         Case False

      End Select
   End Sub

   Private Sub rbEmergencyMode_CheckedChanged(sender As Object, e As EventArgs) Handles rbEmergencyMode.CheckedChanged
      Select Case rbEmergencyMode.Checked
         Case True
            frmPassword.ShowDialog()
            frmPassword.Dispose()
            Select Case PasswordOk
               Case True
                  pnlManualMode.Visible = True
                  pnlAutomaticMode.Visible = False
               Case Else
                  pnlManualMode.Visible = False
                  rbRFIDTestModeAutomatic.Checked = True
            End Select

         Case False

      End Select

   End Sub

   Private Sub btnShowDatabase_Click(sender As Object, e As EventArgs) Handles btnShowDatabase.Click

      frmRFIDDatabase.ShowDialog()
      frmRFIDDatabase.Dispose()

   End Sub

   Private Sub btnShowGeneralTestsDatabase_Click(sender As Object, e As EventArgs) Handles btnShowGeneralTestsDatabase.Click

      frmGeneralTestInformationDatabase.ShowDialog()
      frmGeneralTestInformationDatabase.Dispose()

   End Sub

   Private Sub btnShowSensorTestDatabase_Click(sender As Object, e As EventArgs) Handles btnShowSensorTestDatabase.Click

      frmSensorTestInformationDatabase.ShowDialog()
      frmSensorTestInformationDatabase.Dispose()

   End Sub

   Sub FindAvalablePlatforms()
      'Sub Finds All Platforms That Are Listed In The RFID Database And Are Active For The Fasica Tester
      'Sub Also Populates The Platform Names On The Main Screen For Emergency Mode Use

      For index As Short = 1 To 192
         Select Case RFID_BIT_DATABASE(index).BIT_Test_ID.ToUpper
            Case "MODEL ID"
               Select Case RFID_BIT_DATABASE(index).BIT_Active
                  Case True
                     Select Case RFID_BIT_DATABASE(index).BIT_Fascia_Tester
                        Case True
                           If Me.rbPlatform_1.Text = "Platform_1" Then
                              Me.rbPlatform_1.Text = RFID_BIT_DATABASE(index).BIT_Description
                              Me.rbPlatform_1.Visible = True
                              Me.rbPlatform_1.Checked = False
                              Me.rbPlatform_1.Checked = True
                              Exit Select
                           End If

                           If Me.rbPlatform_2.Text = "Platform_2" Then
                              Me.rbPlatform_2.Text = RFID_BIT_DATABASE(index).BIT_Description
                              Me.rbPlatform_2.Visible = True
                              Exit Select
                           End If

                           If Me.rbPlatform_3.Text = "Platform_3" Then
                              Me.rbPlatform_3.Text = RFID_BIT_DATABASE(index).BIT_Description
                              Me.rbPlatform_3.Visible = True
                              Exit Select
                           End If

                           If Me.rbPlatform_4.Text = "Platform_4" Then
                              Me.rbPlatform_4.Text = RFID_BIT_DATABASE(index).BIT_Description
                              Me.rbPlatform_4.Visible = True
                              Exit Select
                           End If

                           If Me.rbPlatform_5.Text = "Platform_5" Then
                              Me.rbPlatform_5.Text = RFID_BIT_DATABASE(index).BIT_Description
                              Me.rbPlatform_5.Visible = True
                              Exit Select
                           End If

                        Case False

                     End Select
                  Case False

               End Select
            Case Else

         End Select
      Next

   End Sub

   Private Sub rbPlatform_1_CheckedChanged(sender As Object, e As EventArgs) Handles rbPlatform_1.CheckedChanged
      Select Case rbPlatform_1.Checked
         Case True
            PlatformToTest = rbPlatform_1.Text
         Case False

      End Select
   End Sub

   Private Sub rbPlatform_2_CheckedChanged(sender As Object, e As EventArgs) Handles rbPlatform_2.CheckedChanged
      Select Case rbPlatform_2.Checked
         Case True
            PlatformToTest = rbPlatform_2.Text
         Case False

      End Select
   End Sub

   Private Sub rbPlatform_3_CheckedChanged(sender As Object, e As EventArgs) Handles rbPlatform_3.CheckedChanged
      Select Case rbPlatform_3.Checked
         Case True
            PlatformToTest = rbPlatform_3.Text
         Case False

      End Select

   End Sub

   Private Sub rbPlatform_4_CheckedChanged(sender As Object, e As EventArgs) Handles rbPlatform_4.CheckedChanged
      Select Case rbPlatform_4.Checked
         Case True
            PlatformToTest = rbPlatform_4.Text
         Case False

      End Select

   End Sub

   Private Sub rbPlatform_5_CheckedChanged(sender As Object, e As EventArgs) Handles rbPlatform_5.CheckedChanged
      Select Case rbPlatform_5.Checked
         Case True
            PlatformToTest = rbPlatform_5.Text
         Case False

      End Select

   End Sub

   Private Sub tmrFlashLabels_Tick(sender As Object, e As EventArgs) Handles tmrFlashLabels.Tick
      Select Case lbloverallTestResult.Text
         Case Is = "", "Not Tested", "Wating"
            'Don't Flash Text
         Case Else
            'Flash The Text By Changing The Background Color
            Select Case lbloverallTestResult.BackColor
               Case Color.WhiteSmoke
                  lbloverallTestResult.BackColor = LastBackColor
               Case Else
                  LastBackColor = lbloverallTestResult.BackColor
                  lbloverallTestResult.BackColor = Color.WhiteSmoke
            End Select
      End Select
   End Sub

   Private Sub btnPrintTestProductLabel_Click(sender As Object, e As EventArgs) Handles btnPrintTestProductLabel.Click

      Support.PrintPartLabel(Format(Now, "HH:mm") & ", " & Format(Now, "MM/dd/yyyy"), "123456789", "Test Fail Label")

   End Sub

   Private Sub lblStartRestartSwitch_DoubleClick(sender As Object, e As EventArgs) Handles lblStartRestartSwitch.DoubleClick
      StartButtonDoubleClick = True
   End Sub

   Private Sub platformCB_TextChanged(sender As Object, e As EventArgs) Handles platformCB.TextChanged
      If InStr(platformCB.Text, "W166", CompareMethod.Text) <> 0 Then
         chassisCodeCB.Text = "W"
      End If

      If InStr(platformCB.Text, "C292", CompareMethod.Text) <> 0 Then
         chassisCodeCB.Text = "C"
      End If

   End Sub

   Sub SetTestLists(ByVal BumberLocationToSet As String)
      'Enables Or Disables Tests Displayed In Emergency Mode Based On Bumper Location

      Select Case BumberLocationToSet
         Case Is = "Front"
            'General Tests
            cb_PTS_TestSelected.Checked = False
            pnlPTS.Enabled = True

            cb_HFA_TestSelected.Checked = False
            pnlHFA.Enabled = False

            cb_LEDLamp_TestSelected.Checked = False
            pnlLED.Enabled = False

            cb_TEMP_TestSelected.Checked = False
            pnlTEMP.Enabled = True

            cb_CAMERA_TestSelected.Checked = False
            pnlCAMERA.Enabled = True

            cb_ART_PLATE_TestSelected.Checked = False
            pnlARTPLATE.Enabled = True

         Case Is = "Rear"
            'General Tests
            cb_PTS_TestSelected.Checked = False
            pnlPTS.Enabled = True

            cb_HFA_TestSelected.Checked = False
            pnlHFA.Enabled = True

            cb_LEDLamp_TestSelected.Checked = False
            pnlLED.Enabled = True

            cb_TEMP_TestSelected.Checked = False
            pnlTEMP.Enabled = False

            cb_CAMERA_TestSelected.Checked = False
            pnlCAMERA.Enabled = False

            cb_ART_PLATE_TestSelected.Checked = False
            pnlARTPLATE.Enabled = False

         Case Else

      End Select


   End Sub
#Region "ini File Creation"
   Private Sub btnCreateFrontPictureIni_Click(sender As Object, e As EventArgs) Handles btnCreateFrontPictureIni.Click
      'Creates Or Modfies The Front .ini files used for locating the symbols on screen for the currently selected image

      Dim SectionName As String = "Symbol_Locations"

      'Determine The Bumper Name Based On The Platform Selected On The Main Screen
      Dim BumperPictureFileName As String = DefaultBumperPictureFilePath
      If rbNoneSelected.Checked = True Then
         'Use Default Picture And .ini until correct value is seen
         BumperPictureFileName = BumperPictureFileName & "Default"
      Else
         If rbPlatform_1.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_1.Text
            TBOXBumperConfiguration = rbPlatform_1.Text
         ElseIf rbPlatform_2.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_2.Text
            TBOXBumperConfiguration = rbPlatform_2.Text
         ElseIf rbPlatform_3.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_3.Text
            TBOXBumperConfiguration = rbPlatform_3.Text
         ElseIf rbPlatform_4.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_4.Text
            TBOXBumperConfiguration = rbPlatform_4.Text
         ElseIf rbPlatform_5.Checked = True Then
            BumperPictureFileName = BumperPictureFileName & rbPlatform_5.Text
            TBOXBumperConfiguration = rbPlatform_5.Text
         End If
      End If
      'Determine If It Is A Front Or Rear Bumper And Then Add That To The File Name
      If rbFrontSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_Front"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_FRONT"
      ElseIf rbRearSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_Rear"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_REAR"
      End If

      'Determine Bumper And Then Add That To The File Name
      If rbBasic.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_BASIC"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_BASIC_TX"
      ElseIf rbAMG.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_AMG"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_AMG_TX"
      ElseIf rbPlatform_1.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & "_MAYBACH"
         TBOXBumperConfiguration = TBOXBumperConfiguration & "_MAYBACH_TX"
      ElseIf rbNoneSelected.Checked = True Then
         BumperPictureFileName = BumperPictureFileName & ""
      End If
      If BumperPictureFileName.Contains("Platform_1") Then
         'Set A Defaul Value For TBOX So It Does Not Fail On Load
         TBOXBumperConfiguration = ""
         TBOXBumperConfiguration = "X296_FRONT_BASIC_TX"
         Exit Sub
      End If
      Try
         pbBumper.Image = Image.FromFile(BumperPictureFileName & ".png")
         pbBumper.SendToBack()
         lblAbortTestMessage.SendToBack()
      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The RFID Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

      BumperPictureFileName = BumperPictureFileName & ".ini"
      ''Set The FCW Properties
      WriteINIFile(SectionName, "lbl_FCW_Left", lbl_FCW.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_FCW_TOP", lbl_FCW.Top.ToString, BumperPictureFileName)

      ''Set The BSM/DTR Right Hand Properties
      WriteINIFile(SectionName, "lbl_BSM_DTR_RH_Left", lbl_BSM_DTR_RH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_BSM_DTR_RH_TOP", lbl_BSM_DTR_RH.Top.ToString, BumperPictureFileName)

      ''Set The BSM/DTR Left Hand Properties
      WriteINIFile(SectionName, "lbl_BSM_DTR_LH_Left", lbl_BSM_DTR_LH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_BSM_DTR_LH_TOP", lbl_BSM_DTR_LH.Top.ToString, BumperPictureFileName)

      'With lbl_Rear_BSM_iBSM_RH
      WriteINIFile(SectionName, "lbl_Rear_BSM_iBSM_RH_Left", lbl_Rear_BSM_iBSM_RH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_Rear_BSM_iBSM_RH_TOP", lbl_Rear_BSM_iBSM_RH.Top.ToString, BumperPictureFileName)

      'With lbl_Rear_BSM_iBSM_LH
      WriteINIFile(SectionName, "lbl_Rear_BSM_iBSM_LH_Left", lbl_Rear_BSM_iBSM_LH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_Rear_BSM_iBSM_LH_TOP", lbl_Rear_BSM_iBSM_LH.Top.ToString, BumperPictureFileName)

      ''Set The RCW Properties
      WriteINIFile(SectionName, "lbl_Rear_RCW_Left", lbl_Rear_RCW.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_Rear_RCW_TOP", lbl_Rear_RCW.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand Out board PTS Sensor Properties
      WriteINIFile(SectionName, "lbl_PTS_RH_OB_Left", lbl_PTS_RH_OB.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_RH_OB_TOP", lbl_PTS_RH_OB.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand Out board PTS Sensor Shape Properties
      WriteINIFile(SectionName, "osPTS_Front_RH_OB_Left", osPTS_Front_RH_OB.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_Front_RH_OB_TOP", osPTS_Front_RH_OB.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand Out board PTS Sensor Properties
      'With lbl_PTS_LH_OB
      WriteINIFile(SectionName, "lbl_PTS_LH_OB_Left", lbl_PTS_LH_OB.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_LH_OB_TOP", lbl_PTS_LH_OB.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand Out board PTS Sensor Shape Properties
      'With osPTS_Front_LH_OB
      WriteINIFile(SectionName, "osPTS_Front_LH_OB_Left", osPTS_Front_LH_OB.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_Front_LH_OB_TOP", osPTS_Front_LH_OB.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand PTS Sensor Properties
      'With lbl_PTS_RH
      WriteINIFile(SectionName, "lbl_PTS_RH_Left", lbl_PTS_RH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_RH_TOP", lbl_PTS_RH.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand PTS Sensor Shape Properties
      'With osPTS_RH
      WriteINIFile(SectionName, "osPTS_RH_Left", osPTS_RH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_RH_TOP", osPTS_RH.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand PTS Sensor Properties
      'With lbl_PTS_LH
      WriteINIFile(SectionName, "lbl_PTS_LH_Left", lbl_PTS_LH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_LH_TOP", lbl_PTS_LH.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand PTS Sensor Shape Properties
      'With osPTS_LH
      WriteINIFile(SectionName, "osPTS_LH_Left", osPTS_LH.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_LH_TOP", osPTS_LH.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand Center PTS Sensor Properties
      'With lbl_PTS_RH_CTR
      WriteINIFile(SectionName, "lbl_PTS_RH_CTR_Left", lbl_PTS_RH_CTR.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_RH_CTR_TOP", lbl_PTS_RH_CTR.Top.ToString, BumperPictureFileName)

      ''Set The Right Hand Center PTS Sensor Shape Properties
      'With osPTS_RH_CTR
      WriteINIFile(SectionName, "osPTS_RH_CTR_Left", osPTS_RH_CTR.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_RH_CTR_TOP", osPTS_RH_CTR.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand Center PTS Sensor Properties
      'With lbl_PTS_LH_CTR
      WriteINIFile(SectionName, "lbl_PTS_LH_CTR_Left", lbl_PTS_LH_CTR.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_PTS_LH_CTR_TOP", lbl_PTS_LH_CTR.Top.ToString, BumperPictureFileName)

      ''Set The Left Hand Center PTS Sensor Shape Properties
      'With osPTS_LH_CTR
      WriteINIFile(SectionName, "osPTS_LH_CTR_Left", osPTS_LH_CTR.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "osPTS_LH_CTR_TOP", osPTS_LH_CTR.Top.ToString, BumperPictureFileName)

      WriteINIFile(SectionName, "lbl_LAMP_PASSENGER_Left", lbl_LAMP_PASSENGER.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_LAMP_PASSENGER_TOP", lbl_LAMP_PASSENGER.Top.ToString, BumperPictureFileName)

      WriteINIFile(SectionName, "lbl_LAMP_DRIVER_Left", lbl_LAMP_DRIVER.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_LAMP_DRIVER_Top", lbl_LAMP_DRIVER.Top.ToString, BumperPictureFileName)

      ''Set The HFA Shape Properties
      'With lbl_HFA
      WriteINIFile(SectionName, "lbl_HFA_Left", lbl_HFA.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_HFA_TOP", lbl_HFA.Top.ToString, BumperPictureFileName)

      ''Set The Tempreature Sensor Shape Properties
      WriteINIFile(SectionName, "lbl_TEMP_Left", lbl_TEMP.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_TEMP_TOP", lbl_TEMP.Top.ToString, BumperPictureFileName)

      ''Set The Camera Sensor Shape Properties
      WriteINIFile(SectionName, "lbl_Camera_Left", lblCamera.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_Camera_TOP", lblCamera.Top.ToString, BumperPictureFileName)

      ''Set The ART PLATE Sensor Shape Properties
      WriteINIFile(SectionName, "lbl_ArtPlate_Left", lblArtPlate.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_ArtPlate_TOP", lblArtPlate.Top.ToString, BumperPictureFileName)

      ''Set The HEating Element Shape Properties
      WriteINIFile(SectionName, "lbl_TEMP_Left", lbl_TEMP.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_TEMP_TOP", lbl_TEMP.Top.ToString, BumperPictureFileName)

      ''Set The HEating Element Shape Properties
      WriteINIFile(SectionName, "lbl_HEATINGELEMENT_Left", lbl_Heating_Element.Left.ToString, BumperPictureFileName)
      WriteINIFile(SectionName, "lbl_HEATINGELEMENT_TOP", lbl_Heating_Element.Top.ToString, BumperPictureFileName)

   End Sub


#End Region 'ini File Creation

   Private Sub rbFrontSelected_Click(sender As Object, e As EventArgs) Handles rbFrontSelected.Click
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
            SetTestLists("Front")
         Case False

      End Select
   End Sub

   Private Sub cb_PTS_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_PTS_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

   Private Sub cb_LEDLamp_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_LEDLamp_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

   Private Sub cb_HFA_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_HFA_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub
   Private Sub cb_TEMP_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_TEMP_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

   Private Sub cb_CAMERA_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_CAMERA_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

   Private Sub cb_ART_PLATE_TestSelected_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ART_PLATE_TestSelected.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub
   Private Sub rbAMG_CheckedChanged(sender As Object, e As EventArgs) Handles rbAMG.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

   Private Sub rbBasic_CheckedChanged(sender As Object, e As EventArgs) Handles rbBasic.CheckedChanged
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub


   Private Sub cb_PSAT_TestSelected_CheckedChanged(sender As Object, e As EventArgs)
      Select Case rbFrontSelected.Checked
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub
   Private Sub cb_HeatingElement_CheckedChanged(sender As Object, e As EventArgs) Handles cb_HeatingElement.CheckedChanged
      Select Case rbFrontSelected.Checked '09_25_2022  Added JME Heating Element Test
         Case True
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Front")
         Case False
            ClearBumperScreen("Front")
            ClearBumperScreen("Rear")
            SetBumperScreen("Rear")
      End Select
   End Sub

#Region "RFID CODE"
   Private _oRehau_RFID_Class As clsFosCl       'Object of REHAU_RMS2011_FosCl
   Private _oOrder As Order                     'Object, which stores the application data
   Private _oOrderConfirmation As OrderConfirm  'Object, which stores the order confirmation data

#Region "RFID/Init"

   Private Sub vStartFosClient()
      If _oRehau_RFID_Class Is Nothing Then
         'The REHAU dll instantiate
         _oRehau_RFID_Class = New REHAU_RMS2011_FosCl.clsFosCl()
         'Set 'message level according to the GUI-value
         vSet_oFosMsglevel(cbMsgLevel.Text)
         'Event handler register for logging events (before init, otherwise the messages come to nothing)
         AddHandler _oRehau_RFID_Class.MfRehauFosCl, AddressOf _oFosCl_MfRehauFosCl
         ' Eventhandler registrieren fuer Error-Events
         AddHandler _oRehau_RFID_Class.ErrorFromFosCl, AddressOf _oFosCl_ErrorFromFosCl
         ' Eventhandler registrieren fuer die NewOrder-Events
         AddHandler _oRehau_RFID_Class.NewOrderFromREHAU, AddressOf _oFosCl_NewOrderFromREHAU

         ' Die REHAU-Dll initialisieren
         'These machines pattern formed from the ordinals of the characteristics required to evaluate the machine.
         ' Dim strKoP As String = "000000000100000000000000000100110000000000000000000001110000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000"
         '                         |        |         |         |         |         |         |         |         |         |         |         |         |         |         |         |         |         |         |         |
         '                         1        10        20        30        40        50        60        70        80        90        100       110       120       130       140       150       160       170       180       190 192-stelliger Konfektionspattern
         'strKoP Is Set By Values Active In The RFID Database 
         Dim strKoP As String = RFID_InitString

         Try
            _oRehau_RFID_Class.Init(Me, MachineID, Allocation_Logic_iZuol, strKoP, 52011)
            '        _oRehau_RFID_Class.Init(this, 4711, 1, strKoP, 52011);
            '           |     |     |   |       |_ TCP/IP Port
            '           |     |     |   |_ Machine Processing Key
            '           |     |     |_ Alloocation Logic (iZuol)
            '           |     |_ Machine-ID
            '           |_Parent Object

         Catch ex As Exception
            WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Initializing the RFID Client : An Exception Was Thrown See Error Log For Details")
            AbortTesting = True
         End Try
      End If
   End Sub
#End Region

#Region "General GUI-Events"

   Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
      vStartFosClient()
   End Sub
   Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
      If _oRehau_RFID_Class IsNot Nothing Then
         _oRehau_RFID_Class.bCancel = True
         _oRehau_RFID_Class = Nothing
      End If
   End Sub
   Private Sub cbMsgLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMsgLevel.SelectedIndexChanged
      vSet_oFosMsglevel(cbMsgLevel.Text)
   End Sub

#End Region '"General GUI-Events"

#Region "GUI events that simulate the parts manufacturing"

   Private Sub btnDoOrder_Click(sender As Object, e As EventArgs) Handles btnDoOrder.Click
      ' We accept only times everything as OK to
      Dim strGsb As String = strMakeGsbString(True, False, False)
      Dim strIo As String = _oOrder.strBPtV 'Everything was default is reported as OK
      Dim strBio As String = "".PadRight(192, "0"c) ' biO war keines der Merkmale
      Dim strNio As String = "".PadRight(192, "0"c) ' niO war keines der Merkmale
      _oOrderConfirmation = New OrderConfirm(_oOrder.iMID, _oOrder.iZuol, _oOrder.strBPtM, _oOrder.strAnr, _oOrder.iAid, _oOrder.strPnr, _oOrder.strMnr, _oOrder.strMbz, strGsb, _oOrder.strBPtV, strIo, strBio, strNio)
      SetMsg("New Order Complete")

   End Sub

   Private Sub btnConfirmOrder_Click(sender As Object, e As EventArgs) Handles btnConfirmOrder.Click
      'That is, Overall status io and the individual features at io correspond to the default values
      'In bio and everything is not OK 0
      'Then, using the method bRueckmeldungToREHAU the data in the RMS REHAU push 2011 Fossil
      SetOrderRueckmeld(True)
      PrintOrderRueckmeld()
      _oRehau_RFID_Class.bRueckmeldungToREHAU(_oOrderConfirmation.iMID, _oOrderConfirmation.iZuol, _oOrderConfirmation.strBPtM, _oOrderConfirmation.strAnr, _oOrderConfirmation.iAid, _oOrderConfirmation.strPnr, _oOrderConfirmation.strMnr, _oOrderConfirmation.strMbz, _oOrderConfirmation.strGsb, _oOrderConfirmation.strBPtV, _oOrderConfirmation.strBPtIo, _oOrderConfirmation.strBPtBio, _oOrderConfirmation.strBPtNio)
      SetMsg("Conducted Order Confirmation io")

   End Sub

   Private Sub btnConfirmOrderNio_Click(sender As Object, e As EventArgs) Handles btnConfirmOrderNio.Click
      ' The overall status of the part is also set to NOK
      ' Anschliessend mit der Methode bRueckmeldungToREHAU die Daten in die REHAU_RMS2011_FosCl schieben
      SetOrderRueckmeld(False) ' Bei false wird das erste Bear
      PrintOrderRueckmeld()
      _oRehau_RFID_Class.bRueckmeldungToREHAU(_oOrderConfirmation.iMID, _oOrderConfirmation.iZuol, _oOrderConfirmation.strBPtM, _oOrderConfirmation.strAnr, _oOrderConfirmation.iAid, _oOrderConfirmation.strPnr, _oOrderConfirmation.strMnr, _oOrderConfirmation.strMbz, _oOrderConfirmation.strGsb, _oOrderConfirmation.strBPtV, _oOrderConfirmation.strBPtIo, _oOrderConfirmation.strBPtBio, _oOrderConfirmation.strBPtNio)
      SetMsg("Conducted Order Confirmation Nio")

   End Sub
   ''' <summary>
   ''' Setzt den MsgLevel der REHAU_RMS2011_FosCl.
   ''' This can be used to control how frequently come MfRehauFosCl events
   ''' </summary>
   ''' <param name="strValue"></param>
   Private Sub vSet_oFosMsglevel(ByVal strValue As String)
      If _oRehau_RFID_Class IsNot Nothing Then
         _oRehau_RFID_Class.iMsgLevel = CInt(Math.Truncate(DirectCast(System.Enum.Parse(GetType(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel), strValue), REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel)))
      End If
   End Sub

#End Region '"GUI events that simulate the parts manufacturing"

#Region "internal helper methods"
   ''' <summary>
   ''' Internal method to set the data of the feedback object
   ''' </summary>
   ''' <param name="IstIo">true wenn Teil iO ist</param>
   Private Sub SetOrderRueckmeld(ByVal IstIo As Boolean)
      If Not (_oOrder Is Nothing) AndAlso Not (_oOrderConfirmation Is Nothing) Then
         If IstIo Then
            _oOrderConfirmation.strBPtIo = _oOrderConfirmation.strBPtV 'Everything was default is reported as OK
            _oOrderConfirmation.strBPtBio = "".PadRight(192, "0"c) 'Bio is none of the characteristics
            _oOrderConfirmation.strBPtNio = "".PadRight(192, "0"c) 'Nio is none of the characteristics
            _oOrderConfirmation.strGsb = strMakeGsbString(IstIo, False, (Not IstIo))
         Else
            ' Make as first default to nio feature
            Dim iPos As Integer = _oOrder.strBPtV.IndexOf("1")
            _oOrderConfirmation.strBPtIo = ReplaceCharByIndex(_oOrderConfirmation.strBPtV, "0"c, iPos)      ' reset in strio
            _oOrderConfirmation.strBPtNio = "".PadRight(iPos, "0"c) & "1" & "".PadRight(192 - iPos, "0"c)   ' put in strNio
            _oOrderConfirmation.strGsb = strMakeGsbString(IstIo, False, IstIo)
         End If
      End If
   End Sub

   Private Sub PrintOrderRueckmeld()
      SetMsg(ControlChars.CrLf & "#TS#: Rueckmeldedaten sind jetzt")
      SetMsg(String.Format("  Machine Id        : {0}", _oOrderConfirmation.iMID))
      SetMsg(String.Format("  Allocation logic  : {0}", _oOrderConfirmation.iZuol))
      SetMsg(String.Format("  BPt Maschine      : {0}", _oOrderConfirmation.strBPtM))
      SetMsg(String.Format("  Order Serial #    : {0}", _oOrderConfirmation.strAnr))
      SetMsg(String.Format("  Assignments-ID    : {0}", _oOrderConfirmation.iAid))
      SetMsg(String.Format("  Prodnr            : {0}", _oOrderConfirmation.strPnr))
      SetMsg(String.Format("  Matnr             : {0}", _oOrderConfirmation.strMnr))
      SetMsg(String.Format("  Mat.Description   : {0}", _oOrderConfirmation.strMbz))
      SetMsg(String.Format("  Overall status    : {0}", _oOrderConfirmation.strGsb))
      SetMsg(String.Format("  BPt Vorgabe       : {0}", _oOrderConfirmation.strBPtV))
      SetMsg(String.Format("  BPt iO            : {0}", _oOrderConfirmation.strBPtIo))
      SetMsg(String.Format("  BPt biO           : {0}", _oOrderConfirmation.strBPtBio))
      SetMsg(String.Format("  BPt niO           : {0}", _oOrderConfirmation.strBPtNio))
      SetMsg("")
      Return

   End Sub

   Private Function ReplaceCharByIndex(ByVal strValue As String, ByVal chrReplace As Char, ByVal intIndex As Integer) As String
      Return (If(intIndex < strValue.Length AndAlso intIndex > -1, String.Concat(strValue.Substring(0, intIndex), chrReplace.ToString(), strValue.Substring(intIndex + 1)), strValue))
   End Function

   Private Function strMakeGsbString(ByVal bIo As Boolean, ByVal bBio As Boolean, ByVal bNio As Boolean) As String
      Dim strRet As String = "00000100"
      'The following if .. else if ... design ensures that only one of the' bits' set
      If bIo Then
         strRet = "00000001"
      ElseIf bBio Then
         strRet = "00000010"
      ElseIf bNio Then
         strRet = "00000100"
      End If
      Return (strRet)
   End Function
#End Region 'Internal Helper Methods


#Region "Events From REHAU_RMS2011_FosCl"

   Private Sub _oFosCl_ErrorFromFosCl(ByVal sender As Object, ByVal e As ErrorFromFosClEventArgs)
      SetMsg("#TS#: ERROR REHAU=> " & e.strMsg & " " & e.CausingException.Message)
   End Sub

   Private Sub _oFosCl_MfRehauFosCl(ByVal sender As Object, ByVal e As MessageFromFosClEventArgs)
      ' Display This issue on GUI
      SetMsg("#TS#: REHAU=> " & e.strMsg)
   End Sub

   Private Sub _oFosCl_NewOrderFromREHAU(ByVal sender As Object, ByVal e As NewOrderFromREHAUFosClEventArgs)
      'The user data is located in the EventArgs
      '
      'Output the data as a log text 'For Illustration
      SetMsg(ControlChars.CrLf & "#TS#: NewOrderFromREHAU received")
      SetMsg(String.Format("  Machine Id        : {0}", e.iMID))
      SetMsg(String.Format("  Allocation logic  : {0}", e.iZuol))
      SetMsg(String.Format("  BPt Maschine      : {0}", e.strBPtM))
      SetMsg(String.Format("  Order Serial #    : {0}", e.strAnr))
      SetMsg(String.Format("  Assignments-ID    : {0}", e.iAid))
      SetMsg(String.Format("  Prodnr            : {0}", e.strPnr))
      SetMsg(String.Format("  Matnr             : {0}", e.strMnr))
      SetMsg(String.Format("  Mat.Description   : {0}", e.strMbz))
      SetMsg(String.Format("  Overall status    : {0}", e.strGsb))
      SetMsg(String.Format("  BPt Vorgabe       : {0}", e.strBPtV))
      SetMsg("")

      ' Remember ... and in internal order data
      _oOrder = New Order(e.iMID, e.iZuol, e.strBPtM, e.strAnr, e.iAid, e.strPnr, e.strMnr, e.strMbz, e.strGsb, e.strBPtV)
      ' Now the confirmation button can be enabled
      'NOTE: Since the event _oFosCl_NewOrderFromREHAU is thrown from the handler thread of REHAU_RMS2011_FosCl,
      'GUI elements can not be addressed directly, since another thread. Therefore, as in the following, the 
      'Use 'Invoke wrapper methods
      SetEnableBtn(Me.btnDoOrder, True)
      SetEnableBtn(Me.btnConfirmOrder, True)
      SetEnableBtn(Me.btnConfirmOrderNio, True) ' So ist der Aufruf MIT Invoke-Wrapper

      'TODO: May Need To Change This Method If Race Condition Occures
      delay(50)
      Application.DoEvents()
      RFIDReadComplete = True 'Set Flag So That Main Routine Knows A Read And New Order Are Complete

   End Sub
#End Region

#Region "Set methods for the GUI elements (Wg multithreading)"

   ''' <summary>
   ''' Method with a message to the txt msg is appended to it. 
   ''' Is necessary because is possibly purely attachments from another thread in TxtMsg.
   ''' About Taking also scroll to the end and limiting the content to 50000 characters 
   ''' </summary>
   ''' <param name="text">Nachricht, die angehaengt werden soll</param>
   Private Sub SetMsg(ByVal text As String)
      Const ciMaxCharacters As Integer = 50000
      ' InvokeRequired required compares the thread ID of the
      ' calling thread to the thread ID of the creating thread.
      ' If these threads are different, it returns true.
      If Me.txtMsg.InvokeRequired Then
         ' Invoke required
         ' SetMsg calls himself via Invoke
         Dim d As New SetTextCallback(AddressOf SetMsg)
         Try
            Me.Invoke(d, New Object() {text})
         Catch
         End Try
      Else
         'Sometimes comes from a thread even to a message when TxtMsg is already Disposed because the program is terminated. 
         'So here's just a try aussenrum built. If that happens, we simply oblivious to this message. 
         'Therefore, the catch is also empty. (HR)    
         Try
            Me.SuspendLayout()
            ' ggf Zeitstempel ausgeben
            'string strOut = text.Replace("#TS#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff "));
            Dim strOut As String = text.Replace("#TS#", Date.Now.ToString("HH:mm:ss:fff ")) ' bisesrl kuerzer
            Me.txtMsg.AppendText(strOut & Environment.NewLine)
            ' groessenbeschraenkung fuer den Inhalt von txtMsg
            If txtMsg.Text.Length > ciMaxCharacters Then
               ' Cut lines after the first 50000 characters
               Dim iLast As Integer = txtMsg.Text.IndexOf(ControlChars.CrLf, txtMsg.Text.Length - ciMaxCharacters)
               If iLast > 1 Then
                  txtMsg.Text = txtMsg.Text.Substring(iLast)
                  txtMsg.SelectionStart = txtMsg.Text.Length
                  txtMsg.ScrollToCaret()
               End If
            End If
            Me.ResumeLayout()
         Catch
         End Try
      End If
   End Sub
   ' This delegate enables asynchronous calls for setting
   ' the text property on a TextBox control.
   Private Delegate Sub SetTextCallback(ByVal text As String)


   'Method to a button from another thread to enablen / disablen
   Private Sub SetEnableBtn(ByVal cmdLocal As Button, ByVal bEnable As Boolean)
      ' InvokeRequired required compares the thread ID of the
      ' calling thread to the thread ID of the creating thread.
      ' If these threads are different, it returns true.
      If cmdLocal.InvokeRequired Then
         ' Invoke required
         ' SetText calls himself via Invoke
         Dim d As New SetEnableBtnCallback(AddressOf SetEnableBtn)
         Me.Invoke(d, New Object() {cmdLocal, bEnable})
      Else
         Try
            cmdLocal.Enabled = bEnable
         Catch
         End Try
      End If
   End Sub
   ' This delegate enables asynchronous calls for setting enable property of a button  TextBox control.
   Private Delegate Sub SetEnableBtnCallback(ByVal btn As Button, ByVal bEna As Boolean)


#End Region

   Public Sub New()

      ' This call is required by the designer.
      InitializeComponent()

      ' Enter 'all available message levels in the combobox
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Dump.ToString()) ' List All Messages
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Verbose.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Info.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Normal.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Important.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Warnung.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Error.ToString())
      cbMsgLevel.Items.Add(REHAU_RMS2011_FosCl.MessageFromFosClEventArgs.MsgLevel.Fatal.ToString()) ' List Only The Fatal Mistakes
      cbMsgLevel.SelectedItem = cbMsgLevel.Items(0) ' Set ComboBox to dump (Report All)

      'For PeakCAN Light Drivers
      ActiveHardware = -1
      ' Create a list to store the displayed mesasges 
      '
      LastMsgsList = New ArrayList

   End Sub

   Sub ResetTestResultsRFID()
      'Resets all bits of the TestResults For The RFID Rehau .dll
      TestResults_RFID.RCW = "?"
      TestResults_RFID.CAMERA = "?"
      TestResults_RFID.DTR = "?"
      TestResults_RFID.FCW = "?"
      TestResults_RFID.GSAT = "?"
      TestResults_RFID.iBSM = "?"
      TestResults_RFID.LED = "?"
      TestResults_RFID.PTS = "?"
      TestResults_RFID.TEMP = "?"
      TestResults_RFID.TSM = "?"
      TestResults_RFID.BSM = "?"
      TestResults_RFID.PSAT = "?"
      TestResults_RFID.HFA = "?"
      TestResults_RFID.ART = "?"
      Me.lblMID.Text = "?"
      Me.lbliZoul.Text = "?"
      Me.lblBPtm.Text = "?"
      Me.lblstrAnr.Text = "?"
      Me.lbliAid.Text = "?"
      Me.lblstrPnr.Text = "?"
      Me.lblstrMnr.Text = "?"
      Me.lblstrMbz.Text = "?"
      Me.lblstrGsb.Text = "?"
      Me.lstTestList.Items.Clear()
      Application.DoEvents()
   End Sub

   Sub ConfirmOrder()
      ' Default Is everything as OK
      Dim strGsb As String = strMakeGsbString(True, False, False)
      Dim strIo As String = _oOrder.strBPtV 'Everything was default is reported as OK
      Dim strBio As String = "".PadRight(192, "0"c) ' biO war keines der Merkmale
      Dim strNio As String = "".PadRight(192, "0"c) ' niO war keines der Merkmale
      _oOrderConfirmation = New OrderConfirm(_oOrder.iMID, _oOrder.iZuol, _oOrder.strBPtM, _oOrder.strAnr, _oOrder.iAid, _oOrder.strPnr, _oOrder.strMnr, _oOrder.strMbz, strGsb, _oOrder.strBPtV, strIo, strBio, strNio)
      SetMsg("New Order Confirmed " & Now.ToString)

   End Sub
   Sub ConfirmRFIDProcessingResults()
      'Confirms The Results To The Rehau RFID .dll

      'Set All Bits To Pass Fail Status
      SetAcknowledgementRFIDBits(_oOrder.strBPtV)

      'Confirm The Order And Return Back Processing Results To Rehau RFID .dll
      PrintOrderRueckmeld()   'For Debug Purposes (Will Show On RFID Example Tab)
      _oRehau_RFID_Class.bRueckmeldungToREHAU(_oOrderConfirmation.iMID, _oOrderConfirmation.iZuol, _oOrderConfirmation.strBPtM, _oOrderConfirmation.strAnr, _oOrderConfirmation.iAid, _oOrderConfirmation.strPnr, _oOrderConfirmation.strMnr, _oOrderConfirmation.strMbz, _oOrderConfirmation.strGsb, _oOrderConfirmation.strBPtV, _oOrderConfirmation.strBPtIo, _oOrderConfirmation.strBPtBio, _oOrderConfirmation.strBPtNio)
      SetMsg("Conducted Order Confirmation " & Now.ToString)

   End Sub

   Sub SetAcknowledgementRFIDBits(ByVal RFID_BITS As String)
      'Checks Pass Fail Results And Sets AckNowledegment Bits Accordingly
      'This Function Returns The bit String That Will Be Sent Back To The RFID System via the Rehau .dll

      Dim RFIDValue As Char() = ""
      Dim FasciaType As String = ""
      Dim SectionName As String = ""
      Dim SetAcknowledgementRFIDBits_io_String As String = ""
      Dim SetAcknowledgementRFIDBits_nio_String As String = ""
      Dim SetAcknowledgementRFIDBits_bio_String As String = ""

      Dim SetAcknowledgementRFIDBitsArrayiO(191) As Char
      Dim SetAcknowledgementRFIDBitsArrayniO(191) As Char
      Dim SetAcknowledgementRFIDBitsArraybiO(191) As Char

      Dim SomethingFailed As Boolean = False

      Try   'Try To Decode The RFID_BITS
         If RFID_BITS.Length <> 192 Then
            'There Must Be 192 Bits In Order To Acknowledgement Correctly If Not Error Out
            AllTestsPassed = False
            AbortTesting = True
            Me.lblTestResult.BackColor = Color.Crimson
            Throw New Exception("The RFID BIT STRING WAS NOT 192 BYTES IN LENGTH (in the SetAcknowledgementRFIDBits Routine")
         End If

         RFIDValue = RFID_BITS.ToCharArray()
         For StringArrayIndex As Short = 0 To 191
            If RFID_BIT_DATABASE(StringArrayIndex + 1).BIT_Fascia_Tester = "1" Then
               'If This BIT IS Used On The Fascia Tester Then Figure Out What The Bit Is Then Set Or Reset The Corisponding BIT
               Select Case RFID_BIT_DATABASE(StringArrayIndex + 1).BIT_Test_ID.ToUpper
                  Case "FRONT"
                     'Bit IS Allways Set No Matter What The Result IS
                     SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                     SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                     SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                  Case "REAR"
                     'Bit IS Allways Set No Matter What The Result IS
                     SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                     SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                     SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                  Case "BASIC"
                     'Bit IS Allways Set No Matter What The Result IS
                     SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                     SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                     SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                  Case "AMG"
                     'Bit IS Allways Set No Matter What The Result IS
                     SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                     SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                     SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                  Case "MODEL ID"   'Decode And Display The Proper ModelID Selected
                     'Bit IS Allways Set No Matter What The Result IS
                     SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                     SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                     SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                  Case "PTSGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.PTS
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "PTSGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.PTS
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "CAMERA"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.CAMERA
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "TEMP"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.TEMP
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "ART"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.ART
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "LED"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.LED
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "HFA"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.HFA
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "HFAGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.HFAGEN1
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "HFAGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.HFAGEN2
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select

                  Case "HFAGEN3"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                        Case "1"
                           Select Case TestResults_RFID.HFAGEN3
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

                           End Select
                     End Select
#Region "RADAR"
                  Case "IBSMGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.iBSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "IBSMGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.iBSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "IBSMGEN3"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.iBSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "BSMGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.BSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "BSMGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.BSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "BSMGEN3"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.BSM
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "FCWGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.FCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select


                  Case "FCWGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.FCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "FCWGEN3"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.FCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "DTRGEN1" 'DP (DTR) GEN1 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.DTR
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "DTRGEN2" 'DP (DTR) GEN2 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.DTR
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "DTRGEN3" 'DP (DTR) GEN3 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.DTR
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select



                  Case "RCWGEN1" 'BSM/RCW GEN1 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.RCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "RCWGEN2" 'BSM/RCW GEN2 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.RCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "RCWGEN3" 'BSM/RCW GEN3 Test Limits Selected
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.RCW
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "HFAGEN1"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.HFA
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "HFAGEN2"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.HFA
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select

                  Case "HFAGEN3"
                     Select Case RFIDValue(StringArrayIndex)
                        Case "0"
                           SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                           SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                           SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                        Case "1"
                           Select Case TestResults_RFID.HFA
                              Case "1", "?"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "1"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                              Case "0"
                                 SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
                                 SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"
                           End Select
                     End Select
#End Region
                  Case Else
                     'Throw An Error There Were No Cases Found For This Enabled BIT (as Defined in the RFID_BIT Database)
                     Throw New Exception("RFID BIT Number: [" & (StringArrayIndex + 1).ToString & "] Has A Test ID (In The RFID_BIT Database that is not valid) [" & RFID_BIT_DATABASE(StringArrayIndex + 1).BIT_Test_ID.ToUpper & "] Is Not Vaild (no tests are avalable for this value)")

               End Select

            Else
               'Set RFID Bit To Zero
               SetAcknowledgementRFIDBitsArrayiO(StringArrayIndex) = "0"
               SetAcknowledgementRFIDBitsArrayniO(StringArrayIndex) = "0"
               SetAcknowledgementRFIDBitsArraybiO(StringArrayIndex) = "0"

            End If 'End If Then For RFIDValue = 1

         Next

         For loops As Short = 0 To 191
            SetAcknowledgementRFIDBits_io_String = SetAcknowledgementRFIDBits_io_String & SetAcknowledgementRFIDBitsArrayiO(loops)
            SetAcknowledgementRFIDBits_nio_String = SetAcknowledgementRFIDBits_nio_String & SetAcknowledgementRFIDBitsArrayniO(loops)
            SetAcknowledgementRFIDBits_bio_String = SetAcknowledgementRFIDBits_bio_String & SetAcknowledgementRFIDBitsArraybiO(loops)
            If SetAcknowledgementRFIDBitsArrayniO(loops) = "1" Then
               SomethingFailed = True
            End If
         Next

         'Set Values To Return Back To Rehau RFID .dll
         _oOrderConfirmation.strBPtIo = SetAcknowledgementRFIDBits_io_String
         _oOrderConfirmation.strBPtBio = SetAcknowledgementRFIDBits_nio_String
         _oOrderConfirmation.strBPtNio = SetAcknowledgementRFIDBits_bio_String

         Select Case SomethingFailed
            Case True
               'Removed Per Andreas Flemming Nothing on the tester should send a failed response
               '_oOrderConfirmation.strGsb = strMakeGsbString(False, False, True)
            Case False
               _oOrderConfirmation.strGsb = strMakeGsbString(True, False, False)
         End Select

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Decoding The RFID string : An Exception Was Thrown See Error Log For Details")
         SomethingFailed = True
         _oOrderConfirmation.strGsb = strMakeGsbString(SomethingFailed, False, (Not SomethingFailed))
         _oOrderConfirmation.strBPtIo = "".PadRight(192, "0"c)
         _oOrderConfirmation.strBPtNio = "".PadRight(192, "0"c)
         _oOrderConfirmation.strBPtBio = "".PadRight(192, "0"c)
         AbortTesting = True
         Exit Sub
      End Try

   End Sub

   Private Sub btnManualDecode_Click(sender As Object, e As EventArgs) Handles btnManualDecode.Click
      'Clear Test Screen,  Clear Previous Measurements ect..)
      InitializeForNewTest()

      RFIDReadComplete = False
      AbortTesting = False

      'Manually Triggers An RFID Read
      TriggerRFIDRead()

      'Wait For RFID Read And Order Complete From Rehau .dll
      Dim TimeToAbortRFIDRead As Long = timeGetTime + 5000
      Do
         Application.DoEvents()
         btnOperatorAbort.Refresh()
         If timeGetTime > TimeToAbortRFIDRead Then
            AbortTesting = True
         End If
         If AbortTesting = True Then Exit Do
         delay(5)
      Loop Until RFIDReadComplete = True

      If AbortTesting = False Then
         'If RFID Read Completed Correctly Then Confirm Order
         If RFIDReadComplete = True Then
            ConfirmOrder() 'Confrim That The Order Was Read And Save All Relvent Data To Send Back To Rehau RFID .dll
            Me.lblProductionNumber.Text = _oOrder.strPnr 'Display The Production Number
         End If
      End If

      If AbortTesting = False Then
         DecodeRFIDBits(_oOrder.strBPtV)  'Decode Order and setup tests accordingly.
      End If

   End Sub

   Sub UpdateRFIDInfoToMainScreen()
      'Displays the infromation read from the RFID order onto the main Screen
      If _oOrder IsNot Nothing Then
         Me.lblMID.Text = _oOrder.iMID.ToString.Trim
         Me.lbliZoul.Text = _oOrder.iZuol.ToString.Trim
         Me.lblBPtm.Text = _oOrder.strBPtM.Trim
         Me.lblstrAnr.Text = _oOrder.strAnr.Trim
         Me.lbliAid.Text = _oOrder.iAid.ToString.Trim
         Me.lblstrPnr.Text = _oOrder.strPnr.Trim
         Me.lblstrMnr.Text = _oOrder.strMnr.Trim
         Me.lblstrMbz.Text = _oOrder.strMbz.Trim
         Me.lblstrGsb.Text = _oOrder.strGsb.Trim
      End If

   End Sub
#End Region 'RFID

#Region "PEAK CAN"

   Private Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click
      Dim strInfo As String = ""
      Dim Res As CANResult

      ' We execute the "VersionInfo" function of the PCANLight 
      ' using as parameter the Hardware type and a string 
      ' variable to get the info.
      ' 
      Try
         Res = CANLight.VersionInfo(cbbHws.SelectedIndex, strInfo)
         strInfo = strInfo.Replace(vbLf, vbCrLf)

         ' The function was successfully executed
         '			
         If (Res = CANResult.ERR_OK) Then
            ' We show the Version Information
            '
            txtInfo.Text = strInfo
         Else
            ' An error occurred.  We show the error.
            '
            txtInfo.Text = "Error: " + Res.ToString()
         End If

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The RFID Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Sub

   Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
      Dim Res As CANResult

      ' According with the active parameters/hardware, we
      ' use one of the two possible "Init" PCANLight functions.
      ' One is for Plug And Play hardware, and the other for
      ' Not P&P.
      '
      If (cbbIO.Enabled) Then
         ' Not P&P Hardware
         '
         Res = CANLight.Init(cbbHws.SelectedIndex, cbbBaudrates.Tag(), cbbMsgType.SelectedIndex, Convert.ToInt32(cbbIO.Text, 16), Convert.ToByte(cbbInterrupt.Text))
      Else
         ' Not P&P Hardware
         '
         Res = CANLight.Init(cbbHws.SelectedIndex, cbbBaudrates.Tag(), cbbMsgType.SelectedIndex)
      End If


      ' The Hardware was successfully initiated
      '
      If (Res = CANResult.ERR_OK) Then
         ' We save the hardware type which is currently 
         ' initiated
         '
         ActiveHardware = cbbHws.SelectedIndex

         ' We start to read from the CAN Queue
         '
         tmrRead.Enabled = True

         ' We show the information of the configured 
         ' and initiated hardware
         '
         btnWrite.Enabled = True
         btnRelease.Enabled = True
         btnInit.Enabled = False

         txtInfo.Text = "Active Hardware: " + cbbHws.Text
         txtInfo.Text += vbCrLf + "Baud Rate: " + cbbBaudrates.Text
         txtInfo.Text += vbCrLf + "Frame Type: " + cbbMsgType.Text
         ' If was a no P&P Hardware, we show additional information
         '
         If (cbbIO.Enabled) Then
            txtInfo.Text += vbCrLf + "I/O Addr.: " + cbbIO.Text + "h"
            txtInfo.Text += vbCrLf + "Interrupt: " + cbbInterrupt.Text
         End If
      Else
         ' An error occurred.  We show the error.
         '
         txtInfo.Text = "Error: " + Res.ToString()
      End If

   End Sub

   Private Sub btnRelease_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
      Dim Res As CANResult

      ' We stopt to read from tehe CAN Queue
      '
      tmrRead.Enabled = False

      ' We close the active hardware using the 
      ' "Close" function of the PCANLight using 
      ' as parameter the Hardware type.
      '
      Res = CANLight.Close(ActiveHardware)

      If (Res = CANResult.ERR_OK) Then
         ' The Hardware was successfully closed
         '
         txtInfo.Text = "Hardware was successfully Released." + vbCrLf
      Else
         ' An error occurred.  We show the error.
         '
         txtInfo.Text = "Error: " + Res.ToString()
      End If

      ' We set the varibale of active hardware to None
      ' and activate/deactivate the corresponding buttons
      '
      ActiveHardware = -1
      btnInit.Enabled = True
      btnWrite.Enabled = False
      btnRelease.Enabled = False
   End Sub

   Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
      Dim Res As CANResult
      Dim CurrentTextBox As TextBox
      Dim MsgToSend As TCLightMsg

      MsgToSend = New TCLightMsg

      ' We configurate the Message.  The ID (max 0x1FF),
      ' Length of the Data, Message Type (Standard in 
      ' this example) and die data
      '
      MsgToSend.ID = Convert.ToInt32(txtID.Text, 16)
      MsgToSend.Len = Convert.ToByte(nudLength.Value)
      If (chbExtended.Checked) Then
         MsgToSend.MsgType = MsgTypes.MSGTYPE_EXTENDED
      Else
         MsgToSend.MsgType = MsgTypes.MSGTYPE_STANDARD
      End If

      ' If a remote frame will be sent, the data bytes are not important.
      '
      If (chbRemote.Checked) Then
         MsgToSend.MsgType = MsgToSend.MsgType Or MsgTypes.MSGTYPE_RTR
      Else
         ' We get so much data as the Len of the message
         '
         CurrentTextBox = txtData0
         For I As Integer = 0 To MsgToSend.Len - 1
            MsgToSend.DATA(I) = Convert.ToByte(Convert.ToInt32(CurrentTextBox.Text, 16))
            If (I < 7) Then
               CurrentTextBox = Me.GetNextControl(CurrentTextBox, True)
            End If
         Next
      End If

      ' The message is sent to the configured hardware
      '
      Res = CANLight.Write(ActiveHardware, MsgToSend)


      If (Res = CANResult.ERR_OK) Then
         ' The Hardware was successfully sent
         '
         txtInfo.Text = "Message was successfully SENT." + vbCrLf
      Else
         ' An error occurred.  We show the error.
         '
         txtInfo.Text = "Error: " + Res.ToString()
      End If

   End Sub

   Private Sub tmrRead_Tick(sender As Object, e As EventArgs) Handles tmrRead.Tick
      Dim MyMsg As TCLightMsg = Nothing
      Dim Res As CANResult

      ' We read at least one time the queue looking for messages.
      ' If a message is found, we look again trying to find more.
      ' If the queue is empty or an error occurr, we get out from
      ' the dowhile statement.
      '			
      Do
         Res = CANLight.Read(ActiveHardware, MyMsg)

         ' A message was received
         ' We process the message
         '
         If (Res = CANResult.ERR_OK) Then
            ProcessMessage(MyMsg)
         End If
      Loop While ((Res And CANResult.ERR_QRCVEMPTY) <> CANResult.ERR_QRCVEMPTY)
   End Sub

   Private Sub ModifyMsgEntry(ByVal LastMsg As MessageStatus, ByVal NewMsg As TCLightMsg)
      Dim strLastData, strNewData As String
      Dim NewStatus As MessageStatus
      Dim CurrItem As ListViewItem
      Dim iLen, iCount As Integer

      strNewData = ""
      strLastData = ""

      ' Values from the last Messages
      '
      CurrItem = lstMessages.Items.Item(LastMsg.Position)
      iCount = Convert.ToInt32(CurrItem.SubItems.Item(4).Text)
      strLastData = CurrItem.SubItems.Item(3).Text
      iLen = LastMsg.CANMessage.Len

      ' New values
      '
      If (NewMsg.MsgType And MsgTypes.MSGTYPE_RTR) <> 0 Then
         strNewData = "Remote Request"
      Else
         For I As Integer = 0 To NewMsg.Len - 1
            strNewData += String.Format("{0:X2} ", NewMsg.DATA(I))
         Next
      End If

      ' Count and Time are updated
      '
      iCount += 1

      ' Set the changes
      '
      If iLen <> NewMsg.Len Then
         iLen = NewMsg.Len
         CurrItem.SubItems.Item(2).Text = iLen.ToString()
      End If

      If strLastData <> strNewData Then
         CurrItem.SubItems.Item(3).Text = strNewData
      End If

      CurrItem.SubItems.Item(4).Text = iCount.ToString()

      ' Save the new Status of the message
      ' NOTE: The objects are saved in the same index position which
      ' they use in the Listview
      '
      NewStatus = New MessageStatus(NewMsg, LastMsg.Position)
      LastMsgsList.RemoveAt(LastMsg.Position)
      LastMsgsList.Insert(LastMsg.Position, NewStatus)
   End Sub

   Private Sub InsertMsgEntry(ByVal NewMsg As TCLightMsg)
      Dim strNewData, strTemp As String
      Dim CurrItem As ListViewItem
      Dim CurrMsg As MessageStatus

      strTemp = ""
      strNewData = ""

      ' Add the new ListView Item with the Type of the message
      '
      If NewMsg.MsgType And MsgTypes.MSGTYPE_EXTENDED <> 0 Then
         strTemp = "EXTENDED"
      Else
         strTemp = "STANDARD"
      End If

      If NewMsg.MsgType And MsgTypes.MSGTYPE_RTR = MsgTypes.MSGTYPE_RTR Then
         strTemp += "/RTR"
      End If

      CurrItem = lstMessages.Items.Add(strTemp)

      ' We format the ID of the message and show it
      '
      If NewMsg.MsgType And MsgTypes.MSGTYPE_EXTENDED <> 0 Then
         CurrItem.SubItems.Add(String.Format("{0:X8}h", NewMsg.ID))
      Else
         CurrItem.SubItems.Add(String.Format("{0:X3}h", NewMsg.ID))
      End If

      ' We set the length of the Message
      '
      CurrItem.SubItems.Add(NewMsg.Len.ToString())

      ' We format the data of the message. Each data is one
      ' byte of Hexadecimal data
      ' 
      If NewMsg.MsgType And MsgTypes.MSGTYPE_RTR = MsgTypes.MSGTYPE_RTR Then
         strNewData = "Remote Request"
      Else
         For I As Integer = 0 To NewMsg.Len - 1
            strNewData += String.Format("{0:X2} ", NewMsg.DATA(I))
         Next
      End If

      CurrItem.SubItems.Add(strNewData)

      ' The message is the First, so count is 1 and there
      ' is not any time difference between messages.
      '
      CurrItem.SubItems.Add("1")

      ' We add this status in the last message list
      '
      CurrMsg = New MessageStatus(NewMsg, CurrItem.Index)
      LastMsgsList.Add(CurrMsg)
   End Sub

   Private Sub ProcessMessage(ByVal MyMsg As TCLightMsg)
      Dim bFound As Boolean = False
      Dim CurrMessage As MessageStatus

      ' Initialization
      '
      CurrMessage = New MessageStatus

      ' We search if a message (Smae ID, Type and same Net) is
      ' already received or if this is a new message
      '
      For I As Integer = 0 To LastMsgsList.Count - 1
         CurrMessage = LastMsgsList(I)
         If CurrMessage.CANMessage.ID = MyMsg.ID Then
            If CurrMessage.CANMessage.MsgType = MyMsg.MsgType Then
               bFound = True
               Exit For
            End If
         End If
      Next

      If bFound Then
         ' Messages of this kind are already received; we make an update
         '
         ModifyMsgEntry(CurrMessage, MyMsg)
      Else
         ' Messages of this kind are not received; we make a new entry
         '
         InsertMsgEntry(MyMsg)
      End If
   End Sub

   ' This help function assign the same function to the PressKey 
   ' event of the Data fields
   '
   Private Sub AssignEvents()
      AddHandler txtData0.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData1.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData2.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData3.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData4.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData5.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData6.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData7.KeyPress, AddressOf Me.txtID_KeyPress
      AddHandler txtData1.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData2.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData3.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData4.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData5.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData6.Leave, AddressOf Me.txtData0_Leave
      AddHandler txtData7.Leave, AddressOf Me.txtData0_Leave
   End Sub


   Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
      Dim iCheck As Byte

      ' We convert the Character to its Upper case equivalent and 
      ' get its numeric value
      '
      iCheck = Convert.ToByte(Char.ToUpper(e.KeyChar))

      ' The Key is the Delete (Backspace) Key
      '
      If iCheck = 8 Then
         Exit Sub
      End If

      ' The Key is a number between 0-9
      '
      If iCheck > 47 And iCheck < 58 Then
         Return
      End If

      ' The Key is a character between A-F
      '
      If iCheck > 64 And iCheck < 71 Then
         Return
      End If

      ' Is neither a number nor a character between A(a) and F(f)
      '
      e.Handled = True
   End Sub

   Private Sub txtID_Leave(sender As Object, e As EventArgs) Handles txtID.Leave
      Dim TextLength, MaxValue As Integer

      ' Calculate the text length and Maximum ID value according
      ' with the Message Type
      '
      If chbExtended.Checked Then
         TextLength = 8
         MaxValue = &H1FFFFFFF
      Else
         TextLength = 3
         MaxValue = &H7FF
      End If

      ' The Textbox for the ID is represented with 3 characters for 
      ' Standard and 8 characters for extended messages.
      ' Therefore if the Length of the text is smaller than TextLength,  
      ' we add "0"
      '
      While txtID.Text.Length <> TextLength
         txtID.Text = "0" + txtID.Text
      End While

      ' Because in this example will be sent only Standard messages
      ' we check that the ID is not bigger than 0x7FF
      '
      If Convert.ToInt32(txtID.Text, 16) > MaxValue Then
         txtID.Text = String.Format("{0:X" + TextLength.ToString() + "}", MaxValue)
      End If

   End Sub

   Private Sub txtData0_Leave(sender As Object, e As EventArgs) Handles txtData0.Leave
      Dim CurrentTextbox As TextBox

      ' All the Textbox Data fields are represented with 2 characters.
      ' Therefore if the Length of the text is smaller than 2, we add
      ' a "0"
      '
      If sender.GetType().Name = "TextBox" Then
         CurrentTextbox = sender
         While CurrentTextbox.Text.Length <> 2
            CurrentTextbox.Text = "0" + CurrentTextbox.Text
         End While
      End If
   End Sub

   Private Sub nudLength_ValueChanged(sender As Object, e As EventArgs) Handles nudLength.ValueChanged
      Dim CurrentTextBox As TextBox

      CurrentTextBox = txtData0

      ' We enable so much TextBox Data fields as the length of the
      ' message will be, that is the value of the UpDown control
      '
      For I As Integer = 0 To 7
         CurrentTextBox.Enabled = I < nudLength.Value
         If I < 7 Then
            CurrentTextBox = Me.GetNextControl(CurrentTextBox, True)
            If CurrentTextBox Is Nothing Then
               Exit For
            End If
         End If
      Next

   End Sub

   Private Sub chbExtended_CheckedChanged(sender As Object, e As EventArgs) Handles chbExtended.CheckedChanged
      Dim iTemp As Integer

      If chbExtended.Checked Then
         txtID.MaxLength = 8
      Else
         txtID.MaxLength = 3
      End If

      If txtID.Text.Length > txtID.MaxLength Then
         iTemp = Convert.ToInt32(txtID.Text, 16)
         If iTemp < &H7FF Then
            txtID.Text = String.Format("{0:X3}", iTemp)
         Else
            txtID.Text = "7FF"
         End If
      End If

      txtID_Leave(txtID, New EventArgs)
   End Sub

   Private Sub chbRemote_CheckedChanged(sender As Object, e As EventArgs) Handles chbRemote.CheckedChanged
      Dim CurrentTextBox As TextBox

      CurrentTextBox = txtData0

      ' We enable so much TextBox Data fields as the length of the
      ' message will be, that is the value of the UpDown control.
      '
      For I As Integer = 0 To 7
         CurrentTextBox.Visible = Not chbRemote.Checked
         If I < 7 Then
            CurrentTextBox = Me.GetNextControl(CurrentTextBox, True)
            If CurrentTextBox Is Nothing Then
               Exit Sub
            End If
         End If
      Next
   End Sub

   Private Sub txtInfo_DoubleClick(sender As Object, e As EventArgs) Handles txtInfo.DoubleClick
      ' We clear the Information edit box
      '
      txtInfo.Text = ""

   End Sub

   Private Sub lstMessages_DoubleClick(sender As Object, e As EventArgs) Handles lstMessages.DoubleClick
      lstMessages.Items.Clear()
      LastMsgsList.Clear()
   End Sub

   Private Sub cbbHws_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbHws.SelectedIndexChanged
      Dim bShowIO As Boolean

      ' According with the selection in the Hardware list, 
      ' we show/hide the input controls for I/O Address and 
      ' Interrupt. (This parameters are NOT necessary for all 
      ' hardware types) .
      '
      Select Case cbbHws.SelectedIndex
         Case HardwareType.DNG
            bShowIO = True
         Case HardwareType.DNP
            bShowIO = True
         Case HardwareType.ISA_1CH
            bShowIO = True
         Case HardwareType.ISA_2CH
            bShowIO = True
         Case Else
            bShowIO = False
      End Select

      cbbIO.Enabled = bShowIO
      cbbInterrupt.Enabled = bShowIO

   End Sub

   Private Sub cbbBaudrates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbBaudrates.SelectedIndexChanged
      ' We save the corresponding Baudrate enumeration 
      ' type value for every selected Baudrate from the 
      ' list.
      '
      Select Case cbbBaudrates.SelectedIndex
         Case 0
            cbbBaudrates.Tag = Baudrates.BAUD_1M
         Case 1
            cbbBaudrates.Tag = Baudrates.BAUD_500K
         Case 2
            cbbBaudrates.Tag = Baudrates.BAUD_250K
         Case 3
            cbbBaudrates.Tag = Baudrates.BAUD_125K
         Case 4
            cbbBaudrates.Tag = Baudrates.BAUD_100K
         Case 5
            cbbBaudrates.Tag = Baudrates.BAUD_50K
         Case 6
            cbbBaudrates.Tag = Baudrates.BAUD_20K
         Case 7
            cbbBaudrates.Tag = Baudrates.BAUD_10K
         Case 8
            cbbBaudrates.Tag = Baudrates.BAUD_5K
         Case Else
            cbbBaudrates.Tag = 0
      End Select

   End Sub



    Private Sub btnForceFixtureUnlock_Click(sender As Object, e As EventArgs) Handles btnForceFixtureUnlock.Click
      If RelayBoardInstalled = True Then
         'Wait For Operator To Hit The Nest Lock Relase Button
         lblTestResult.Text = "Waiting For Fixture Nest Lock Release Switch"
         lblTestResult.BackColor = Color.Yellow
         Do
            Application.DoEvents()
            btnOperatorAbort.Refresh()
            If AbortTesting = True Then Exit Do
            delay(250)
         Loop Until NestLockRelease()

         If NestLockRelease() Then
            'Fire Relay 
            RelayBoard.Turn_On_Relay(NestLockRelay)
         End If

         'Wait For Operator To Rotate The Nest
         lblTestResult.Text = "Waiting For The Fixture Nest To Be Rotated"
         lblTestResult.BackColor = Color.Yellow
         Do
            Application.DoEvents()
            btnOperatorAbort.Refresh()
            If AbortTesting = True Then Exit Do
            delay(250)
         Loop Until NestAtHomePosition()
      End If
      If NestAtHomePosition() Then
         'Turn Off Relay's
         RelayBoard.Turn_Off_Relay(EngageConnectorPogoPinsRelay)
         RelayBoard.Turn_Off_Relay(ConnectorLockRelay)
         RelayBoard.Turn_Off_Relay(NestLockRelay)
         lblTestResult.Text = "Waiting For New Test (Start Button)"
      End If
   End Sub

#End Region

#Region "TBOX"
   Private Sub btnStartTBOX_6_0_Measurement_Click(sender As Object, e As EventArgs) Handles btnStartTBOX_6_0_Measurement.Click
      Dim Result As Boolean
      Dim Results As String = ""
      Dim ReturnedBytes(18) As Byte
      btnStartTBOX_6_0_Measurement.Enabled = False

      Result = DirectEchoesRequest(Results)

      Select Case Result
         Case True
            lblTbox_6_0_RXStatus.Text = "Command Sent Complete"
         Case Else
            lblTbox_6_0_RXStatus.Text = "Error Command Not Sent"
      End Select

      lblTBOX_6_0_ResponseBytes.Text = ReturnedBytes.ToString

      btnStartTBOX_6_0_Measurement.Enabled = True
   End Sub

   Private Sub btnReadFrimwareVersion_Click(sender As Object, e As EventArgs) Handles btnReadFrimwareVersion.Click
      'Reads The Bosch Box Firmware Version
      Dim Result As Boolean
      Dim ReturnedBytes(7) As Byte

      lblTBOX_6_0_ResponseBytes.Text = ""
      Result = ReadFirmwareVersion(ReturnedBytes)
      Select Case Result
         Case True
            lblFirmwareVersion.Text = Trim(Str(ReturnedBytes(5))) & "." & Trim(Str(ReturnedBytes(6)))
         Case False
            lblFirmwareVersion.Text = "Error"
      End Select
   End Sub

   Private Sub btnReadBumperSetup_Click(sender As Object, e As EventArgs) Handles btnReadBumperSetup.Click
      'Reads The Bosch Box Firmware Version
      Dim Result As Boolean
      Dim ReturnedBytes(30) As Byte

      lblTBOX_6_0_ResponseBytes.Text = ""
      Result = ReadBumperSetup(ReturnedBytes)
      Select Case Result
         Case True
            lblFirmwareVersion.Text = Trim(Str(ReturnedBytes(5))) & "." & Trim(Str(ReturnedBytes(6)))
         Case False
            lblFirmwareVersion.Text = "Error"
      End Select

   End Sub

   Private Sub btnWriteBumperSetup_Click(sender As Object, e As EventArgs) Handles btnWriteBumperSetup.Click
      Dim Result As Byte = Nothing
      Dim ReturnString As String = ""

      lblTbox_6_0_RXStatus.Text = ""

      Result = TBOX.WriteBumperSetup(ReturnString)
      Select Case Result
         Case True
            lblTbox_6_0_RXStatus.Text = "Command Sent Complete"
         Case Else
            lblTbox_6_0_RXStatus.Text = "Error Command Not Sent"
      End Select

      lblTBOX_6_0_ResponseBytes.Text = ReturnString

   End Sub

   Private Sub btnReadSensorProductionData_Click(sender As Object, e As EventArgs) Handles btnReadSensorProductionData.Click
        'Reads The Bosch Box Sensor Production Data

        Dim Result As Boolean

        Dim ReturnString As String = ""


        Result = EchoeProductionData(ReturnString)
        lblTBOX_6_0_ResponseBytes.Text = ""



        Select Case Result
                Case True
                    lblTbox_6_0_RXStatus.Text = Result
                Case False
                lblTBOX_6_0_ResponseBytes.Text = "Error: " & Result
        End Select

    End Sub

   Private Sub btnStopTBOX_6_0_Measurement_Click(sender As Object, e As EventArgs) Handles btnStopTBOX_6_0_Measurement.Click
      'Stops The Measurment Mode IOn The TBOX

   End Sub

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      '  RFID.DecodeRFIDBits("000000000000010000000000100001000000000000000000000010100000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000")

   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnReadTboxConfiguration.Click
      Dim Counter As Short = 0
      Dim SensorArray(100) As String

      TBOX.LoadTBOXCommunicationProtocall()

      tbTBOXBumperConfiguration.Text = TBOXBumperConfiguration

      Dim SensorString As String = LookUpTBOXInfo("SensorMask_Configuration", TBOXBumperConfiguration)

      If SensorString <> "" Then
         Counter = 0                                                         'Reset Counter
         Do                                                                  'Do
            Application.DoEvents()
            SensorArray(Counter) = SplitString(SensorString, ",")    'Split out string into indvidual section Names
            If SensorArray(Counter) = "" Or SensorArray(Counter) = Nothing Then
               Exit Do
            End If
            Counter = Counter + 1                                           '
         Loop
         ReDim Preserve SensorArray(Counter - 1)
      End If

      If SensorArray.Length = 0 Then
         Exit Sub
      Else
         txtSensor1Distance.Text = Val("&H" & SensorArray(11)).ToString
         txtSensor2Distance.Text = Val("&H" & SensorArray(12)).ToString
         txtSensor3Distance.Text = Val("&H" & SensorArray(13)).ToString
         txtSensor4Distance.Text = Val("&H" & SensorArray(14)).ToString
         txtSensor5Distance.Text = Val("&H" & SensorArray(15)).ToString
         txtSensor6Distance.Text = Val("&H" & SensorArray(16)).ToString
      End If
   End Sub

   Private Sub BtnWriteTboxConfiguration_Click(sender As Object, e As EventArgs) Handles btnWriteTboxConfiguration.Click
      'Saves The TBOX Configuration to the .ini File
      Dim Counter As Short = 0
      Dim SensorArray(100) As String
      Dim ConfigurationString As String = ""

      'Load Current Protocal Before Saving New One
      TBOX.LoadTBOXCommunicationProtocall()

      If tbTBOXBumperConfiguration.Text = "" Then
         MsgBox("You Must Read The Configuration Before Saving It!", MsgBoxStyle.Critical, "Bosch Box Write Configuration Error")
         Exit Sub
      End If

      Dim SensorString As String = LookUpTBOXInfo("SensorMask_Configuration", TBOXBumperConfiguration)

      If SensorString <> "" Then
         Counter = 0                                                         'Reset Counter
         Do                                                                  'Do
            Application.DoEvents()
            SensorArray(Counter) = SplitString(SensorString, ",")    'Split out string into indvidual section Names
            If SensorArray(Counter) = "" Or SensorArray(Counter) = Nothing Then
               Exit Do
            End If
            Counter = Counter + 1                                           '
         Loop
         ReDim Preserve SensorArray(Counter - 1)
      End If

      If SensorArray.Length = 0 Then
         Exit Sub
      Else
         SensorArray(11) = Hex(txtSensor1Distance.Text)
         SensorArray(12) = Hex(txtSensor2Distance.Text)
         SensorArray(13) = Hex(txtSensor3Distance.Text)
         SensorArray(14) = Hex(txtSensor4Distance.Text)
         SensorArray(15) = Hex(txtSensor5Distance.Text)
         SensorArray(16) = Hex(txtSensor6Distance.Text)

         For Loops As Short = 0 To 16
            If Loops = 16 Then
               ConfigurationString = ConfigurationString & SensorArray(Loops)
            Else
               ConfigurationString = ConfigurationString & SensorArray(Loops) & ","
            End If
         Next
      End If

      WriteINIFile("SensorMask_Configuration", TBOXBumperConfiguration, ConfigurationString, DefaultIniPath & DefaultTBOXCommunicationINIFileName)

   End Sub

    Private Sub lblTBOX_6_0_SensorEchoes_1_Click(sender As Object, e As EventArgs) Handles lblTBOX_6_0_SensorEchoes_1.Click

    End Sub





#End Region

    '

End Class

