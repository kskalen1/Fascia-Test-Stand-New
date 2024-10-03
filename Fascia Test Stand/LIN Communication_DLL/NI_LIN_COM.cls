VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "NI_LIN_COM"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Public LINTxRx As Long
Public Status  As Long
Public ErrCode As String
Public ErrSource As String
Public ErrString As String

Public Function Intitalize_LIN_Interface(ByVal BaudRate As Long, ByVal Device_Name As String) As Boolean
   'This sub will be used to initialize the LIN device
   
   'This sub will open up a new lin object and configure it
   
   'This sub will perform the following operations:
       'Dim the variables used (note that many variables must be LONG or the software will crash)
       'Define the configuration variables
       'Set the interface to be used as Lin0
       'Do not log bus errors
       'Set the Lin to use the enhanced checksum
       'Set the baud rate to be 10417
       'Enable termination
       'Enable self reception so the lin device can receive data
       'Start the new Lin object
       'Close the object if an error occurred......
   Dim Term As Long
   Dim BusEr As Long
   Dim SelfRec As Long
   
   'Open and Configure the LIN Network Interface Object
   
   'Enable Termination
   Term = 1
   'Log Buss Errors
   BusEr = 1
   'Enable self reception so the lin device can receive data
   SelfRec = 1
   
   Status = ncOpenObject(Device_Name, LINTxRx)
   If (CheckStatus(Status, "ncOpen " & Device_Name) = True) Then GoTo Error
   
   Status = ncSetAttribute(LINTxRx, NC_ATTR_BAUD_RATE, 4, BaudRate)
   If (CheckStatus(Status, "ncSetAttribute" & Device_Name) = True) Then GoTo Error
   
   Status = ncSetAttribute(LINTxRx, NC_ATTR_TERMINATION, 4, Term)
   If (CheckStatus(Status, "ncSetAttribute" & Device_Name) = True) Then GoTo Error
   
   Status = ncSetAttribute(LINTxRx, NC_ATTR_SELF_RECEPTION, 4, SelfRec)
   If (CheckStatus(Status, "ncSetAttribute" & Device_Name) = True) Then GoTo Error
   
   Status = ncSetAttribute(LINTxRx, NC_ATTR_LOG_BUS_ERROR, 4, BusEr)
   If (CheckStatus(Status, "ncSetAttribute" & Device_Name) = True) Then GoTo Error
   
   Status = ncAction(LINTxRx, NC_OP_START, 0)
   If (CheckStatus(Status, "ncAction" & Device_Name) = True) Then GoTo Error
   
   Intitalize_LIN_Interface = True
   
   Exit Function
    
Error:
   Status = ncCloseObject(LINTxRx)
   Intitalize_LIN_Interface = False
End Function

Public Sub ReadBytes(ByVal ReceiveArbID As Long, ByRef DataBytesRead() As Byte, ByRef DataString As String)

Dim ReceiveBuf(150) As NCTYPE_CAN_STRUCT
Dim ActualDataSize As Long
Dim i, j As Integer
Dim Transmit As NCTYPE_CAN_FRAME
Dim LinTxRxHandle As Long

Transmit.ArbitrationId = ReceiveArbID
Transmit.DataLength = 8
Transmit.IsRemote = NC_FRMTYPE_LIN_FULL

delay 100

Status = ncReadMultiple(LINTxRx, 150 * LenB(ReceiveBuf(0)), ReceiveBuf, ActualDataSize)
If (CheckStatus(Status, "ncRead ") = True) Then GoTo Error

'if frames were received, display them
If (ActualDataSize >= 1) Then
    For i = 0 To (ActualDataSize / 24) - 1
        'check if frame is a LIN Full Frame
        If ReceiveBuf(i).FrameType = NC_FRMTYPE_LIN_FULL Then
            If ReceiveBuf(i).ArbitrationId = ReceiveArbID Then
            DataString = DataString & str$(timeGetTime) & "     [RX]  ID:" & Format(Hex(ReceiveBuf(i).ArbitrationId), "00")
               For j = 0 To (ReceiveBuf(i).DataLength) - 1
                  DataBytesRead(j) = ReceiveBuf(i).Data(j)
                  DataString = DataString & " " & PrintByte(ReceiveBuf(i).Data(j))
               Next j
               DataString = DataString & vbCrLf
               Debug.Print DataString
               DoEvents
            End If
        End If
    Next i
End If

Exit Sub
Error:

'Status = ncCloseObject(LINTxRx)

End Sub
Public Sub WriteReadBytes(ByVal HeaderFrameOnly As Boolean, BytesToWrite() As Byte, ByVal SendArbID As Long, ByVal DataLength As Long, ByVal ReceiveArbID As Long, ByVal SendHeaderFrame As Boolean, ByRef Data_Bytes_Received() As Byte, ByRef DataString As String)
     
   Dim Transmit As NCTYPE_CAN_FRAME
   
   If HeaderFrameOnly = False Then
      Transmit.ArbitrationId = SendArbID
      Transmit.DataLength = DataLength
      Transmit.IsRemote = NC_FRMTYPE_LIN_FULL
      
      Transmit.Data(0) = BytesToWrite(0)
      Transmit.Data(1) = BytesToWrite(1)
      Transmit.Data(2) = BytesToWrite(2)
      Transmit.Data(3) = BytesToWrite(3)
      Transmit.Data(4) = BytesToWrite(4)
      Transmit.Data(5) = BytesToWrite(5)
      Transmit.Data(6) = BytesToWrite(6)
      Transmit.Data(7) = BytesToWrite(7)
         
      Status = ncWrite(LINTxRx, Len(Transmit), Transmit)
      If (CheckStatus(Status, "ncWrite") = True) Then GoTo Error
      
      Debug.Print str$(timeGetTime) & "     [TX]  ID:" & Format(Hex(Transmit.ArbitrationId), "00") & " " & PrintByte(Transmit.Data(0)) & " " & PrintByte(Transmit.Data(1)) & " " & PrintByte(Transmit.Data(2)) & " " & PrintByte(Transmit.Data(3)) & " " & PrintByte(Transmit.Data(4)) & " " & PrintByte(Transmit.Data(5)) & " " & PrintByte(Transmit.Data(6)) & " " & PrintByte(Transmit.Data(7))
   
      delay 75
   End If
   
   If HeaderFrameOnly = True Or SendHeaderFrame = True Then
      Transmit.ArbitrationId = ReceiveArbID
      Transmit.DataLength = 0
      Transmit.IsRemote = NC_FRMTYPE_LIN_HEADER
'      Transmit.Data(0) = &H0
'      Transmit.Data(1) = &H0
'      Transmit.Data(2) = &H0
'      Transmit.Data(3) = &H0
'      Transmit.Data(4) = &H0
'      Transmit.Data(5) = &H0
'      Transmit.Data(6) = &H0
'      Transmit.Data(7) = &H0
      Status = ncWrite(LINTxRx, Len(Transmit), Transmit)
      If (CheckStatus(Status, "ncWrite") = True) Then GoTo Error
       Debug.Print str$(timeGetTime) & "     [TX]  HEADER ONLY"
   End If
   
   delay 75
   ReadBytes ReceiveArbID, Data_Bytes_Received, DataString
   
   Exit Sub
   
Error:
   Status = ncCloseObject(LINTxRx)

End Sub

Sub Send_Header_Frame(ByVal ReceiveArbID)
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'Send a header using ID #6 to receive the returned info that the part processed the ENTER CALIBRATION

'Put the descriptive message on the Lin Communication Form text box
'frmMain.txtDataSentandReceived_LIN = frmMain.txtDataSentandReceived_LIN & "Data Received After Enter Calibration Command: (Should be 2 50 2 FF FF FF FF FF)" & Chr$(13) + Chr$(10)

'Select ID 6, header frame, with a data length of 0
'Determine the transmit LIN arbitration ID based on what type of LIN communication is being run when receiving data
Dim Transmit As NCTYPE_CAN_FRAME
 
Transmit.ArbitrationId = ReceiveArbID
Transmit.IsRemote = NC_FRMTYPE_LIN_HEADER            'Send header frame
Transmit.DataLength = 0           'Data length is 0

Debug.Print str$(timeGetTime) & "     [TX]  ID:" & Format(Hex(Transmit.ArbitrationId), "00") & " Len: " & Trim(str(Transmit.DataLength)) & " " & Format(Hex(Transmit.Data(0)), "00") & " " & Format(Hex(Transmit.Data(1)), "00") & " " & Format(Hex(Transmit.Data(2)), "00") & " " & Format(Hex(Transmit.Data(3)), "00") & " " & Format(Hex(Transmit.Data(4)), "00") & " " & Format(Hex(Transmit.Data(5)), "00") & " " & Format(Hex(Transmit.Data(6)), "00") & " " & Format(Hex(Transmit.Data(7)), "00")
Status = ncWrite(LINTxRx, Len(Transmit), Transmit)

'Verify there weren't any errors
If (CheckStatus(Status, "ncWrite") = True) Then GoTo Error

Exit Sub
 'The software will go here upon an error or if the part did not power up.....
Error:
    Select Case Status
        'No Error
        Case 0
            SendHeaderFrame = True
        'This error is caused when an attempt is made to start a running CAN object (no harm done)
        Case -1074388985
        
        Case Else
            Status = ncCloseObject(LINTxRx)
    End Select
End Sub
