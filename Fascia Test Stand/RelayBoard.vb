Module RelayBoard
   Public WithEvents RelayBoardPort As New NCD.NCDComponent()
    Public Enum Relays As Byte
        Relay_1 = 0
        Relay_2 = 1
        Relay_3 = 2
        Relay_4 = 3
        Relay_5 = 4
        Relay_6 = 5
        Relay_7 = 6
        Relay_8 = 7
        Relay_9 = 8
        Relay_10 = 9
        Relay_11 = 10
        Relay_12 = 11
        Relay_13 = 12
        Relay_14 = 13
        Relay_15 = 14
        Relay_16 = 15
    End Enum
   Public BoschSensorPowerRelay As Byte
   Public NestLockRelay As Byte
   Public ConnectorLockRelay As Byte
   Public EngageConnectorPogoPinsRelay As Byte
   Public RFIDTriggerRelay As Byte
   Public LINSensorPowerRelay As Byte
   Public ValeoSensorPowerRelay As Byte
   Public AutolivSensorPowerRelay As Byte
   Public CANHighRelay As Byte
   Public CANLowRelay As Byte

   Sub Initialize()
      'line Below Opens Dialog Box, settings have been selected in control so no need to open dialog.
      'frmMain.RelayBoardPort.SettingPort()

      RelayBoardPort.PortName = RelayBoardcomPort
      RelayBoardPort.BaudRate = 115200

      ' setting the port

      RelayBoardPort.OpenPort()
      ' open the port
      If (Not RelayBoardPort.IsOpen) Then
         MessageBox.Show("Could Not Open The Port For The Relay Board", "RelayBoard Port Communication Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop)
         Application.Exit()
      End If
      RelayBoard.Turn_Off_All_Relays()    'Make Sure All Relays Are Off Before New Test Starts

   End Sub
   Sub Turn_On_Relay(ByVal Relay_To_Turn_On As Relays)
      'Sub Turns On The Passed Relay
      Select Case Relay_To_Turn_On
         Case Relays.Relay_1
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_1.Checked = True

         Case Relays.Relay_2
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_2.Checked = True

         Case Relays.Relay_3
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_3.Checked = True

         Case Relays.Relay_4
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_4.Checked = True

         Case Relays.Relay_5
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_5.Checked = True

         Case Relays.Relay_6
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_6.Checked = True

         Case Relays.Relay_7
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_7.Checked = True

         Case Relays.Relay_8
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On, 1)
            frmMain.cbRelay_8.Checked = True

         Case Relays.Relay_9
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_9.Checked = True

         Case Relays.Relay_10
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_10.Checked = True

         Case Relays.Relay_11
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_11.Checked = True

         Case Relays.Relay_12
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_12.Checked = True

         Case Relays.Relay_13
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_13.Checked = True

         Case Relays.Relay_14
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_14.Checked = True

         Case Relays.Relay_15
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_15.Checked = True

         Case Relays.Relay_16
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOnRelayInBank(Relay_To_Turn_On - 8, 2)
            frmMain.cbRelay_16.Checked = True

      End Select
   End Sub

   Sub Turn_Off_Relay(ByVal Relay_To_Turn_Off As Relays)
      'Sub Turns Off The Passed Relay
      Select Case Relay_To_Turn_Off
         Case Relays.Relay_1
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_1.Checked = False

         Case Relays.Relay_2
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_2.Checked = False

         Case Relays.Relay_3
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_3.Checked = False

         Case Relays.Relay_4
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_4.Checked = False

         Case Relays.Relay_5
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_5.Checked = False

         Case Relays.Relay_6
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_6.Checked = False

         Case Relays.Relay_7
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_7.Checked = False

         Case Relays.Relay_8
            RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off, 1)
            frmMain.cbRelay_8.Checked = False

         Case Relays.Relay_9
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_9.Checked = False

         Case Relays.Relay_10
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_10.Checked = False

         Case Relays.Relay_11
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_11.Checked = False

         Case Relays.Relay_12
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_12.Checked = False

         Case Relays.Relay_13
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_13.Checked = False

         Case Relays.Relay_14
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_14.Checked = False

         Case Relays.Relay_15
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_15.Checked = False

         Case Relays.Relay_16
            RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
            RelayBoardPort.ProXR.RelayBanks.TurnOffRelayInBank(Relay_To_Turn_Off - 8, 2)
            frmMain.cbRelay_16.Checked = False

      End Select
   End Sub
   Sub Turn_Off_All_Relays()
      'Turns Off All Relays In All Banks
      Turn_Off_Relay(Relays.Relay_1)

      Turn_Off_Relay(Relays.Relay_2)

      Turn_Off_Relay(Relays.Relay_3)

      Turn_Off_Relay(Relays.Relay_4)

      Turn_Off_Relay(Relays.Relay_5)

      Turn_Off_Relay(Relays.Relay_6)

      Turn_Off_Relay(Relays.Relay_7)

      Turn_Off_Relay(Relays.Relay_8)

      Turn_Off_Relay(Relays.Relay_9)

      Turn_Off_Relay(Relays.Relay_10)

      Turn_Off_Relay(Relays.Relay_11)

      Turn_Off_Relay(Relays.Relay_12)

      Turn_Off_Relay(Relays.Relay_13)

      Turn_Off_Relay(Relays.Relay_14)

      Turn_Off_Relay(Relays.Relay_15)

      Turn_Off_Relay(Relays.Relay_16)

   End Sub

   Sub Turn_Off_All_Relays_Fast()
      RelayBoardPort.ProXR.RelayBanks.SelectBank(1)
      RelayBoardPort.ProXR.RelayBanks.TurnOffAllRelays()
      RelayBoardPort.ProXR.RelayBanks.SelectBank(2)
      RelayBoardPort.ProXR.RelayBanks.TurnOffAllRelays()
      frmMain.cbRelay_1.Checked = False
      frmMain.cbRelay_2.Checked = False
      frmMain.cbRelay_3.Checked = False
      frmMain.cbRelay_4.Checked = False
      frmMain.cbRelay_5.Checked = False
      frmMain.cbRelay_6.Checked = False
      frmMain.cbRelay_7.Checked = False
      frmMain.cbRelay_8.Checked = False
      frmMain.cbRelay_9.Checked = False
      frmMain.cbRelay_10.Checked = False
      frmMain.cbRelay_11.Checked = False
      frmMain.cbRelay_12.Checked = False
      frmMain.cbRelay_13.Checked = False
      frmMain.cbRelay_14.Checked = False
      frmMain.cbRelay_15.Checked = False
      frmMain.cbRelay_16.Checked = False
   End Sub

   Sub ScanSwitchValues()
      'Scans The Switch Input Board Attached To The Relay Board
      Application.DoEvents()
      Dim Result As Boolean = False

      If RelayBoardInstalled = False Then Exit Sub 'If No Board Is Installed We Do Not Want To Try Reading Switches

      Try
         'Scan Bank 1
         Dim valuebank1 As Byte = RelayBoardPort.ProXR.Scan.ScanValue(CByte(0))

         frmMain.osBank1_1.BackColor = IIf((valuebank1 And 1) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_2.BackColor = IIf((valuebank1 And 2) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_3.BackColor = IIf((valuebank1 And 4) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_4.BackColor = IIf((valuebank1 And 8) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_5.BackColor = IIf((valuebank1 And 16) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_6.BackColor = IIf((valuebank1 And 32) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_7.BackColor = IIf((valuebank1 And 64) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osBank1_8.BackColor = IIf((valuebank1 And 128) > 0, Color.Lime, Color.WhiteSmoke)

         'Update Main Screen Indicatiors
         frmMain.osStartRestartSwitch.BackColor = IIf((valuebank1 And StartRestartSwitchInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osNestLockRelease.BackColor = IIf((valuebank1 And NestLockReleaseInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osArtPlateHarnessConnected.BackColor = IIf((valuebank1 And ArtPlateHarnessConnectedInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osCameraConnected.BackColor = IIf((valuebank1 And CameraHarnessInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osNestRotated.BackColor = IIf((valuebank1 And NestInRotatedPositionInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osNestAtHome.BackColor = IIf((valuebank1 And NestAtHomePositionInputPort) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osInput7.BackColor = IIf((valuebank1 And 64) > 0, Color.Lime, Color.WhiteSmoke)
         frmMain.osPartInPlace.BackColor = IIf((valuebank1 And PartInPlaceInputPort) > 0, Color.Lime, Color.WhiteSmoke)

      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
      End Try

   End Sub

   Function StartRestartSwitch() As Boolean
      'Function Returns The State Of The Start Restart Switch
      Try
         Select Case frmMain.osStartRestartSwitch.BackColor
            Case Color.Lime
               Return True
            Case Else
               Return False
         End Select

      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
      End Try

   End Function

   Function NestInRotatedPosition() As Boolean
      'Function Returns The State Of The Nest In Rotated Position Switch
      Try
         Select Case frmMain.osNestRotated.BackColor
            Case Color.Lime
               Return True
            Case Else
               Return False
         End Select
      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function

   Function NestAtHomePosition() As Boolean
      'Function Returns The State Of The Part In Nest Switch
      Try
         Select Case frmMain.osNestAtHome.BackColor
            Case Color.Lime
               frmMain.osNestAtHome.BackColor = Color.Lime
               Return True
            Case Else
               frmMain.osNestAtHome.BackColor = Color.WhiteSmoke
               Return False
         End Select
      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function

   Function NestLockRelease() As Boolean
      'Function Returns The State Of The Nest Lock Release Switch
      Try
         Select Case frmMain.osNestLockRelease.BackColor
            Case Color.Lime
               Return True
            Case Else
               Return False
         End Select

      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function
   Function PartInPlace() As Boolean
      'Function Returns The State Of The PartInPlace
      Try
         Select Case frmMain.osPartInPlace.BackColor
            Case Color.Lime
               Return True
            Case Else
               Return False
         End Select

      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function
   Sub TriggerRFIDRead()
      'Triggers An RFID Read (Activates "Buzzer")
      RelayBoard.Turn_On_Relay(RFIDTriggerRelay)
      delay(250)
      RelayBoard.Turn_Off_Relay(RFIDTriggerRelay)
   End Sub
   Function CameraHarnessPresent() As Boolean
      'Function Returns The State Of The Camera Harness Present (Harness Is Shorted When Plugged In)
      Try
         Dim BankValue As Byte = 0
         BankValue = RelayBoardPort.ProXR.Scan.ScanValue(CameraHarnessInputBank)

         Select Case BankValue And CameraHarnessInputPort
            Case Is > 0
               Return True
            Case Else
               Return False
         End Select
      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function
   Function ARTPlateHarnessPresent() As Boolean
      'Function Returns The State Of The ART Plate Harness Present (Harness Is Shorted When Plugged In)
      Try
         Dim BankValue As Byte = 0
         BankValue = RelayBoardPort.ProXR.Scan.ScanValue(ArtPlateHarnessConnectedInputBank)

         Select Case BankValue And ArtPlateHarnessConnectedInputPort
            Case Is > 0
               Return True
            Case Else
               Return False
         End Select
      Catch ex As TimeoutException
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The Switches : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      End Try

   End Function
End Module
