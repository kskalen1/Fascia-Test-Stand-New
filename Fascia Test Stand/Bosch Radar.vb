Imports Fascia_Test_Stand.PCANLight
Module Bosch_Radar
   Public Structure CANmessages_Struct
      Friend TX_Message_ID_Left As Int32
      Friend TX_Message_ID_Right As Int32
      Friend Message_Descripion As String
      Friend TX_Extended_Message_ID As Boolean
      Friend TX_Type As String
      Friend TX_Length As Byte
      Friend DID As String
      Friend TX_TestIdentifier As String
      Friend RX_TestIdentifier As String
      Friend TX_Data_Byte_0 As Byte
      Friend TX_Data_Byte_1 As Byte
      Friend TX_Data_Byte_2 As Byte
      Friend TX_Data_Byte_3 As Byte
      Friend TX_Data_Byte_4 As Byte
      Friend TX_Data_Byte_5 As Byte
      Friend TX_Data_Byte_6 As Byte
      Friend TX_Data_Byte_7 As Byte
      Friend RX_Message_ID_Left As Int32
      Friend RX_Message_ID_Right As Int32
      Friend RX_Data_Byte_0 As Byte
      Friend RX_Data_Byte_1 As Byte
      Friend RX_Data_Byte_2 As Byte
      Friend RX_Data_Byte_3 As Byte
      Friend RX_Data_Byte_4 As Byte
      Friend RX_Data_Byte_5 As Byte
      Friend RX_Data_Byte_6 As Byte
      Friend RX_Data_Byte_7 As Byte
      Friend Conversion_Type As String
   End Structure
   Public CANmessages(25) As CANmessages_Struct
   Dim ActiveHardware As HardwareType

   Public Sub LoadCanMessages_Standard()
      'Loads All Of The CAN Message Information From The TestInformation.mdb File
      Dim TempString As String = ""
      Dim SectionName As String = ""

      'Reads The Test Information From The Database
      Dim DBConString As String = My.Settings.CANMessages_ConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "CANMessages"

      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 0

      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The CAN Messages Into Data Struct For Use During Tests
               CANmessages(index).TX_Message_ID_Left = Convert.ToInt32(Val("&H" & (Replace(DBRead("TX_Message_ID_Left"), "0x", ""))))
               CANmessages(index).TX_Message_ID_Right = Convert.ToInt32(Val("&H" & (Replace(DBRead("TX_Message_ID_Right"), "0x", ""))))
               CANmessages(index).Message_Descripion = DBRead("Message_Descripion").ToString
               CANmessages(index).DID = DBRead("DID").ToString
               CANmessages(index).TX_TestIdentifier = DBRead("TX_TestIdentifier").ToString
               CANmessages(index).RX_TestIdentifier = DBRead("RX_TestIdnetifier").ToString
               CANmessages(index).TX_Extended_Message_ID = Convert.ToBoolean(DBRead("TX_Extended_Message_ID"))
               CANmessages(index).TX_Type = DBRead("TX_Type").ToString
               CANmessages(index).TX_Length = Convert.ToUInt16(DBRead("TX_Length"))
               CANmessages(index).TX_Data_Byte_0 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_0"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_1 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_1"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_2 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_2"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_3 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_3"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_4 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_4"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_5 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_5"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_6 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_6"), "0x", ""))))
               CANmessages(index).TX_Data_Byte_7 = Convert.ToByte(Val("&H" & (Replace(DBRead("TX_Data_Byte_7"), "0x", ""))))
               CANmessages(index).RX_Message_ID_Left = Convert.ToInt32(Val("&H" & (Replace(DBRead("RX_Message_ID_Left"), "0x", ""))))
               CANmessages(index).RX_Message_ID_Right = Convert.ToInt32(Val("&H" & (Replace(DBRead("RX_Message_ID_Right"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_0 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_0"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_1 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_1"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_2 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_2"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_3 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_3"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_4 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_4"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_5 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_5"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_6 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_6"), "0x", ""))))
               CANmessages(index).RX_Data_Byte_7 = Convert.ToByte(Val("&H" & (Replace(DBRead("RX_Data_Byte_7"), "0x", ""))))
               CANmessages(index).Conversion_Type = DBRead("Conversion_Type").ToString
               index = index + 1
            Loop
         End If
         If index = 1 Then Throw New Exception("No CAN Message Data In Database File")
         ReDim Preserve CANmessages(index - 1)

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The LoadCanMessages_Standard Test Information Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try

   End Sub
   Sub Intialize_CAN()
      Dim Res As CANResult

      ' According with the active parameters/hardware, we
      ' use one of the two possible "Init" PCANLight functions.
      ' One is for Plug And Play hardware, and the other for
      ' Not P&P.
      '
      Try
         Res = CANLight.Init(HardwareType.USB, Baudrates.BAUD_500K, MsgTypes.MSGTYPE_STANDARD)

         ' The Hardware was successfully initiated
         '
         If (Res = CANResult.ERR_OK) Then
            ' We save the hardware type which is currently 
            ' initiated
            '
            ActiveHardware = HardwareType.USB
            delay(250)
         Else
            ' An error occurred.
            Throw New Exception("Error: " + Res.ToString())
         End If

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")

      End Try

   End Sub
   Function SendCANMessageAndWaitForResponse(ByVal MessageIndex As Short, ByVal SideToSend As String, ByRef SideResponse() As Byte, Optional LongDelay As Boolean = False) As Boolean
      'Sends The CAN Message Passed In And Waits For A Vailid Response
      Dim Result As Boolean = False
      Dim MessageSingleResponse() As Byte = {}
      Dim MultiFrameResponseMessage() As Byte = {}
      Dim NumberOFFramesToReceive As Short = 0
      Dim MessageSingleResponseLength As Short = 0
      Dim MultiFrameResponseMessageLength As Short = 0
      Dim Res As CANResult
      Dim MsgToSend As TCLightMsg
      MsgToSend = New TCLightMsg

      Try
         Select Case SideToSend
            Case "Left", "LEFT"
               'Send To Left
               Res = CANLight.MsgFilter(HardwareType.USB, CANmessages(MessageIndex).RX_Message_ID_Left, CANmessages(MessageIndex).RX_Message_ID_Left, MsgTypes.MSGTYPE_STANDARD)

               If (Res <> CANResult.ERR_OK) Then
                  Throw New Exception("Could Not Set Message Filter")
               End If
               delay(250)
               MsgToSend.ID = CANmessages(MessageIndex).TX_Message_ID_Left
               MsgToSend.Len = CANmessages(MessageIndex).TX_Length
               If (CANmessages(MessageIndex).TX_Extended_Message_ID) Then
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_EXTENDED
               Else
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_STANDARD
               End If

               MsgToSend.DATA(0) = CANmessages(MessageIndex).TX_Data_Byte_0
               MsgToSend.DATA(1) = CANmessages(MessageIndex).TX_Data_Byte_1
               MsgToSend.DATA(2) = CANmessages(MessageIndex).TX_Data_Byte_2
               MsgToSend.DATA(3) = CANmessages(MessageIndex).TX_Data_Byte_3
               MsgToSend.DATA(4) = CANmessages(MessageIndex).TX_Data_Byte_4
               MsgToSend.DATA(5) = CANmessages(MessageIndex).TX_Data_Byte_5
               MsgToSend.DATA(6) = CANmessages(MessageIndex).TX_Data_Byte_6
               MsgToSend.DATA(7) = CANmessages(MessageIndex).TX_Data_Byte_7

               ' The message is sent to the configured hardware
               '
               Res = CANLight.Write(ActiveHardware, MsgToSend)
               If (Res <> CANResult.ERR_OK) Then
                  Throw New Exception("Could Not Seend Data")
               End If

               If LongDelay = True Then
                  delay(2000)
               Else
                  delay(150)
               End If

               Select Case WaitForMessageResponse(CANmessages(MessageIndex).RX_Message_ID_Left, CANmessages(MessageIndex).TX_Message_ID_Left, MessageSingleResponse)
                  Case True
                     'IF Messages Were Received Then Test Results
                     If MessageSingleResponse(0) = &H10 Then
                        'MultiFrame Response So Send Flow Control Command And Wait For Response Back
                        'Calculate How Many Frames To Wait For
                        'Frame Can Return 7 Actual Bytes First Byte Is Flow Control ID (21,22,23....)
                        '2 Byte Of Return Message Is Number OF Bytes To Receive
                        NumberOFFramesToReceive = (MessageSingleResponse(1) - 3) / 7
                        delay(50)
                        Select Case SendCANFlowControlMessageAndWaitForResponse(MessageIndex, NumberOFFramesToReceive, "Left", MultiFrameResponseMessage)
                           Case True
                              'Combine Array Data With First Message Response
                              MessageSingleResponseLength = MessageSingleResponse.Length
                              MultiFrameResponseMessageLength = MultiFrameResponseMessage.Length

                              ReDim Preserve MessageSingleResponse(MessageSingleResponseLength + MultiFrameResponseMessageLength - 1)
                              Array.Copy(MultiFrameResponseMessage, 0, MessageSingleResponse, MessageSingleResponseLength, MultiFrameResponseMessageLength)
                              SideResponse = MessageSingleResponse
                              delay(10)
                              Return True
                           Case False
                              Return False
                        End Select

                     Else
                        'Single Frame Response So Return Message
                        SideResponse = MessageSingleResponse
                        Return True
                     End If

                  Case False
                     Return False
               End Select

            Case "Right", "RIGHT"
               'Send To Right Side
               Res = CANLight.MsgFilter(HardwareType.USB, CANmessages(MessageIndex).RX_Message_ID_Right, CANmessages(MessageIndex).RX_Message_ID_Right, MsgTypes.MSGTYPE_STANDARD)
               If (Res <> CANResult.ERR_OK) Then
                  Throw New Exception("Could Not Set Message Filter")
               End If
               delay(250)
               MsgToSend.ID = CANmessages(MessageIndex).TX_Message_ID_Right
               MsgToSend.Len = CANmessages(MessageIndex).TX_Length
               If (CANmessages(MessageIndex).TX_Extended_Message_ID) Then
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_EXTENDED
               Else
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_STANDARD
               End If

               MsgToSend.DATA(0) = CANmessages(MessageIndex).TX_Data_Byte_0
               MsgToSend.DATA(1) = CANmessages(MessageIndex).TX_Data_Byte_1
               MsgToSend.DATA(2) = CANmessages(MessageIndex).TX_Data_Byte_2
               MsgToSend.DATA(3) = CANmessages(MessageIndex).TX_Data_Byte_3
               MsgToSend.DATA(4) = CANmessages(MessageIndex).TX_Data_Byte_4
               MsgToSend.DATA(5) = CANmessages(MessageIndex).TX_Data_Byte_5
               MsgToSend.DATA(6) = CANmessages(MessageIndex).TX_Data_Byte_6
               MsgToSend.DATA(7) = CANmessages(MessageIndex).TX_Data_Byte_7

               ' The message is sent to the configured hardware
               '
               Res = CANLight.Write(ActiveHardware, MsgToSend)
               If (Res <> CANResult.ERR_OK) Then
                  Throw New Exception("Could Not Seend Data")
               End If

               If LongDelay = True Then
                  delay(2000)
               Else
                  delay(150)
               End If

               Select Case WaitForMessageResponse(CANmessages(MessageIndex).RX_Message_ID_Right, CANmessages(MessageIndex).TX_Message_ID_Right, MessageSingleResponse)
                  Case True
                     'IF Messages Were Received Then Test Results
                     If MessageSingleResponse(0) = &H10 Then
                        'MultiFrame Response So Send Flow Control Command And Wait For Response Back
                        'Calculate How Many Frames To Wait For
                        'Frame Can Return 7 Actual Bytes First Byte Is Flow Control ID (21,22,23....)
                        '2 Byte Of Return Message Is Number OF Bytes To Receive
                        NumberOFFramesToReceive = CInt((MessageSingleResponse(1) - 6) / 7)
                        delay(50)
                        Select Case SendCANFlowControlMessageAndWaitForResponse(MessageIndex, NumberOFFramesToReceive, "Right", MultiFrameResponseMessage)
                           Case True
                              'Combine Array Data With First Message Response
                              MessageSingleResponseLength = MessageSingleResponse.Length
                              MultiFrameResponseMessageLength = MultiFrameResponseMessage.Length

                              ReDim Preserve MessageSingleResponse(MessageSingleResponseLength + MultiFrameResponseMessageLength - 1)
                              Array.Copy(MultiFrameResponseMessage, 0, MessageSingleResponse, MessageSingleResponseLength, MultiFrameResponseMessageLength)
                              SideResponse = MessageSingleResponse
                              delay(10)
                              Return True
                           Case False
                              Return False
                        End Select

                     Else
                        'Single Frame Response So Return Message
                        SideResponse = MessageSingleResponse
                        Return True
                     End If

                  Case False
                     Return False
               End Select

            Case Else
               'Error

         End Select

         'Send To Right

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")
         Return False
      End Try

   End Function

   Function SendCANFlowControlMessageAndWaitForResponse(ByVal MessageIndex As Short, ByVal NumberOfFramesToReceive As Short, ByVal SideToSend As String, ByRef SideResponse() As Byte) As Boolean
      'Sends The CAN Message Passed In And Waits For A Vailid Response
      Dim Result As Boolean = False
      Dim Res As CANResult
      Dim MsgToSend As TCLightMsg
      MsgToSend = New TCLightMsg

      Try
         Select Case SideToSend
            Case "Left", "LEFT"
               'Send To Left
               MsgToSend.ID = CANmessages(MessageIndex).TX_Message_ID_Left
               MsgToSend.Len = 8
               'MsgToSend.Len = CANmessages(MessageIndex).TX_Length
               If (CANmessages(MessageIndex).TX_Extended_Message_ID) Then
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_EXTENDED
               Else
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_STANDARD
               End If

               MsgToSend.DATA(0) = &H30
               MsgToSend.DATA(1) = 0
               MsgToSend.DATA(2) = 0
               MsgToSend.DATA(3) = 0
               MsgToSend.DATA(4) = 0
               MsgToSend.DATA(5) = 0
               MsgToSend.DATA(6) = 0
               MsgToSend.DATA(7) = 0

               ' The message is sent to the configured hardware
               '
               Res = CANLight.Write(ActiveHardware, MsgToSend)

               delay(100)

               Result = WaitForMultiFrameMessageResponse(CANmessages(MessageIndex).RX_Message_ID_Left, CANmessages(MessageIndex).TX_Message_ID_Left, SideResponse, NumberOfFramesToReceive)

               If Result = False Then
                  Return False
               Else
                  Return True
               End If

            Case "Right", "RIGHT"
               'Send To Right Side
               Res = CANLight.MsgFilter(HardwareType.USB, CANmessages(MessageIndex).RX_Message_ID_Right, CANmessages(MessageIndex).RX_Message_ID_Right, MsgTypes.MSGTYPE_STANDARD)
               If (Res <> CANResult.ERR_OK) Then
                  Throw New Exception("Could Not Set Message Filter")
               End If

               MsgToSend.ID = CANmessages(MessageIndex).TX_Message_ID_Right
               MsgToSend.Len = 8
               'MsgToSend.Len = CANmessages(MessageIndex).TX_Length
               If (CANmessages(MessageIndex).TX_Extended_Message_ID) Then
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_EXTENDED
               Else
                  MsgToSend.MsgType = MsgTypes.MSGTYPE_STANDARD
               End If

               MsgToSend.DATA(0) = &H30
               MsgToSend.DATA(1) = 0
               MsgToSend.DATA(2) = 0
               MsgToSend.DATA(3) = 0
               MsgToSend.DATA(4) = 0
               MsgToSend.DATA(5) = 0
               MsgToSend.DATA(6) = 0
               MsgToSend.DATA(7) = 0

               ' The message is sent to the configured hardware
               '
               Res = CANLight.Write(ActiveHardware, MsgToSend)

               delay(100)

               Result = WaitForMultiFrameMessageResponse(CANmessages(MessageIndex).RX_Message_ID_Right, CANmessages(MessageIndex).TX_Message_ID_Right, SideResponse, NumberOfFramesToReceive)
               If Result = False Then
                  Return False
               Else
                  Return True
               End If

            Case Else
               'Error

         End Select

         'Send To Right

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")
         Return False
      End Try

   End Function

   Function WaitForMessageResponse(ByVal RxMessageResponseID As Integer, ByVal MessageTxID As UInt32, ByRef MessageResponse() As Byte) As Boolean
      'Waits For Message Or Aborts After TimeOut
      Dim MyMsg As TCLightMsg = Nothing
      Dim Res As CANResult

      ' We read at least one time the queue looking for messages.
      ' If a message is found, we look again trying to find more.
      ' If the queue is empty or an error occurr, we get out from
      ' the dowhile statement.
      '			
      Try
         Do
            Res = CANLight.Read(ActiveHardware, MyMsg)
            Debug.Print(MyMsg.ID.ToString)
            If (Res = CANResult.ERR_OK) Then
               If MyMsg.ID = RxMessageResponseID Then
                  MessageResponse = MyMsg.DATA
                  If MessageResponse(0) <> &H3 And MessageResponse(1) <> &H7F Then
                     Debug.Print(MyMsg.ID.ToString)
                     Return True
                  End If
               End If
            End If
         Loop While ((Res And CANResult.ERR_QRCVEMPTY) <> CANResult.ERR_QRCVEMPTY)

      Catch ex As Exception
         WritetoErrorLog(ex, False, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, False, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")
         Return False
      End Try

   End Function
   Function WaitForMultiFrameMessageResponse(ByVal RxMessageResponseID As UInteger, ByVal MessageTxID As UInt32, ByRef MessageResponse() As Byte, ByVal NumberOfFramesToReceive As Short) As Boolean
      Dim MyMsg As TCLightMsg = Nothing
      Dim Res As CANResult
      Dim MessageBytesReceived(256) As Byte
      Dim FrameIndex As Short = 0
      Dim Loops As Short = 0
      Dim FrameCounter As Short = 0

      ' We read at least one time the queue looking for messages.
      ' If a message is found, we look again trying to find more.
      ' If the queue is empty or an error occurr, we get out from
      ' the dowhile statement.
      '			
      Try
         Do
            Res = CANLight.Read(ActiveHardware, MyMsg)

            If (Res = CANResult.ERR_OK) Then
               If MyMsg.ID = RxMessageResponseID Then
                  MessageResponse = MyMsg.DATA

                  For Loops = (0 + FrameIndex) To (7 + FrameIndex)
                     MessageBytesReceived(Loops) = MessageResponse(Loops - FrameIndex)
                     Debug.Print(Hex(MessageBytesReceived(Loops)))
                  Next
                  FrameIndex = Loops
                  FrameCounter = FrameCounter + 1
                  If FrameCounter = NumberOfFramesToReceive Then
                     ReDim Preserve MessageBytesReceived(FrameIndex - 1)
                     MessageResponse = MessageBytesReceived
                     Return True
                  End If
               End If
            Else
            End If
         Loop While ((Res And CANResult.ERR_QRCVEMPTY) <> CANResult.ERR_QRCVEMPTY)

      Catch ex As Exception
         WritetoErrorLog(ex, False, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, False, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")
         Return False
      End Try

   End Function

   Sub ReleaseCANInterface()
      Dim Res As CANResult
      Try

         ' "Close" function of the PCANLight using 
         ' as parameter the Hardware type.
         '
         Res = CANLight.Close(ActiveHardware)

         If (Res <> CANResult.ERR_OK) Then
            Throw New Exception("Error: " + Res.ToString)
         End If
      Catch ex As Exception
         WritetoErrorLog(ex, False, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, False, "Exception Was Thrown In The SendCANMessageAndWaitForResponse Routine See Error Log For Details")
      End Try

   End Sub
End Module
