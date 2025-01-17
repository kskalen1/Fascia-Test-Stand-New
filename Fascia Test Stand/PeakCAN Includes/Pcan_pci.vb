'//////////////////////////////////////////////////////////////////////////////
'  PCAN-Light
'  PCAN-PCI.vb
'
'  Version 1.5
'
'  ~~~~~~~~~~
'
'  Basic Idea:
'
'  ~~~~~~~~~~
'
'  Definition of the PCAN-Light API. 
'  The Driver support a Hardware and a Software who want to communicate with CAN-busses 
'
'  ~~~~~~~~~~~~
'
'  PCAN-Light -API
'
'  ~~~~~~~~~~~~
'
'  - Init(ByVal BTR0BTR1 As Int16, ByVal CANMsgtype As Integer)
'  - Close()  
'  - Status() 
'  - Write(ByRef msg As TPCANMsg) 
'  - Read(ByRef msg As TPCANMsg)  
'  - VersionInfo(ByVal buffer As StringBuilder) 
'  - SpecialFunktion(ByVal distributorcode As Long, ByVal codenumber As Integer)
'  - ResetClient()
'  - MsgFilter(ByVal FromID As UInt32, ByVal ToID As UInt32, ByVal Type As Integer)
'  - ResetFilter()
'
'  ------------------------------------------------------------------
'  Author : Hoppe, Wilhelm
'  Modified By: Wagner (17.03.2006)
'
'  Sprache: Visual Basic .Net
'  ------------------------------------------------------------------
'
'  Copyright (C) 1999-2006  PEAK-System Technik GmbH, Darmstadt
'
Imports System
Imports System.Text
Imports System.Runtime.InteropServices

Public Class PCAN_PCI
#Region "Frames, ID's und CAN message types"

    ' Constants definitions - Frame Type
    Public Const CAN_INIT_TYPE_EX As Integer = 1 ' Extended Frames
    Public Const CAN_INIT_TYPE_ST As Integer = 0 ' Standard Frames

    ' Constants definitions - ID
    Public Const CAN_MAX_STANDARD_ID As Integer = &H7FF
    Public Const CAN_MAX_EXTENDED_ID As Integer = &H1FFFFFFF

    ' Constants definitions  - CAN message types
    Public Const MSGTYPE_STANDARD As Integer = &H0  ' Standard Frame (11 bit ID)() As 0x00
    Public Const MSGTYPE_RTR As Integer = &H1       ' 1, if Remote Request frame 
    Public Const MSGTYPE_EXTENDED As Integer = &H2  ' 1, if Extended Data frame (CAN 2.0B, 29-bit ID)
    Public Const MSGTYPE_STATUS As Integer = &H80   ' 1, if Status information
#End Region

#Region "Baudrate Codes"
    ' BTR0BTR1 register
    ' Baudrate code = register value BTR0/BTR1
    Public Const CAN_BAUD_1M As Integer = &H14      '   1 MBit/sec
    Public Const CAN_BAUD_500K As Integer = &H1C    ' 500 KBit/sec
    Public Const CAN_BAUD_250K As Integer = &H11C   ' 250 KBit/sec
    Public Const CAN_BAUD_125K As Integer = &H31C   ' 125 KBit/sec
    Public Const CAN_BAUD_100K As Integer = &H432F  ' 100 KBit/sec 
    Public Const CAN_BAUD_50K As Integer = &H472F   '  50 KBit/sec
    Public Const CAN_BAUD_20K As Integer = &H532F   '  20 KBit/sec
    Public Const CAN_BAUD_10K As Integer = &H672F   '  10 KBit/sec
    Public Const CAN_BAUD_5K As Integer = &H7F7F    '   5 KBit/sec

    ' You can define your own Baudrate for the BTROBTR1 register.
    ' Take a look at www.peak-system.com for our software BAUDTOOL to
    ' calculate the BTROBTR1 register for every baudrate and sample point.
#End Region

#Region "Error Codes"
    ' Error codes (bit code)
    Public Const CAN_ERR_OK As Integer = &H0               ' No error
    Public Const CAN_ERR_XMTFULL As Integer = &H1          ' Transmit buffer in CAN controller is full
    Public Const CAN_ERR_OVERRUN As Integer = &H2          ' CAN controller was read too late
    Public Const CAN_ERR_BUSLIGHT As Integer = &H4         ' Bus error: an error counter reached the 'light' limit
    Public Const CAN_ERR_BUSHEAVY As Integer = &H8         ' Bus error: an error counter reached the 'heavy' limit
    Public Const CAN_ERR_BUSOFF As Integer = &H10          ' Bus error: the CAN controller is in bus-off state
    Public Const CAN_ERR_QRCVEMPTY As Integer = &H20       ' Receive queue is empty
    Public Const CAN_ERR_QOVERRUN As Integer = &H40        ' Receive queue was read too late
    Public Const CAN_ERR_QXMTFULL As Integer = &H80        ' Transmit queue is full			        
    Public Const CAN_ERR_REGTEST As Integer = &H100        ' Test of the CAN controller hardware registers failed (no hardware found)
    Public Const CAN_ERR_NOVXD As Integer = &H200          ' Driver not loaded 
    Public Const CAN_ERR_RESOURCE As Integer = &H2000      ' Resource (FIFO, Client, timeout) cannot be create
    Public Const CAN_ERR_ILLPARAMTYPE As Integer = &H4000  ' Invalid parameter
    Public Const CAN_ERR_ILLPARAMVAL As Integer = &H8000   ' Invalid parameter value
    Public Const CAN_ERRMASK_ILLHANDLE As Integer = &H1C00 ' Mask for all handle errors
    Public Const CAN_ERR_ANYBUSERR As Integer = (CAN_ERR_BUSLIGHT Or CAN_ERR_BUSHEAVY Or CAN_ERR_BUSOFF)
    ' All further error conditions <> 0 please ask PEAK when required.......internal driver failure ........
#End Region

    ' CAN message
    '
    <StructLayout(LayoutKind.Sequential, Pack:=1)> Public Structure TPCANMsg
        Public ID As Integer   ' 11/29 bit identifier
        Public MSGTYPE As Byte ' Bits from MSGTYPE_*
        Public LEN As Byte     ' Data Length Code of the Msg (0..8)
        <MarshalAs(UnmanagedType.ByValArray, sizeconst:=8)> _
        Public DATA As Byte()  ' Data 0 .. 7
    End Structure

    '/////////////////////////////////////////////////////////////////////////////
    '  Init()
    '  This function make the following:
    '   - Activate a Hardware
    '   - Make a Register Test of 82C200/SJA1000
    '   - Allocate a Send buffer and a Hardware handle
    '   - Programs the configuration of the transmit/receive driver
    '   - Set the Baudrate register
    '   - Set the Controller in RESET condition
    '
    '  If CANMsgType=0  ---> ID 11Bit 
    '  If CANMsgType=1  ---> ID 11/29Bit
    '
    '  Possible Errors: NOVXD ILLHW REGTEST RESOURCE
    '
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_Init")> _
    Public Shared Function init(ByVal BTR0BTR1 As Int16, ByVal CANMsgtype As Integer) As Int32
    End Function

    '/////////////////////////////////////////////////////////////////////////////
    '  Close()
	'  This function terminate and release the configured hardware and all 
	'  allocated resources 
    '
    '  Possible Errors: NOVXD
    '
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_Close")> _
    Public Shared Function Close() As Integer
    End Function

    '/////////////////////////////////////////////////////////////////////////////
    '  Status()
	'  This function request the current status of the hardware (b.e. BUS-OFF)
	'
	'  Possible Errors: NOVXD BUSOFF BUSHEAVY OVERRUN
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_Status")> _
    Public Shared Function Status() As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  Write()
	'  This function Place a CAN message into the Transmit Queue of the CAN Hardware
	'
	'  Possible Errors: NOVXD RESOURCE BUSOFF QXMTFULL
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_Write")> _
    Public Shared Function Write(ByRef msg As TPCANMsg) As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  Read()
	'  This function get the next message or the next error from the Receive Queue of 
	'  the CAN Hardware.  
	'  REMARK:
	'		- Check always the type of the received Message (MSGTYPE_STANDARD,MSGTYPE_RTR,
	'		  MSGTYPE_EXTENDED,MSGTYPE_STATUS)
	'		- The function will return ERR_OK always that you receive a CAN message successfully 
	'		  although if the messages is a MSGTYPE_STATUS message.  
	'		- When a MSGTYPE_STATUS mesasge is got, the ID and Length information of the message 
	'		  will be treated as indefined values. Actually information of the received message
	'		  should be interpreted using the first 4 data bytes as follow:
	'			*	Data0	Data1	Data2	Data3	Kind of Error
	'				0x00	0x00	0x00	0x02	CAN_ERR_OVERRUN		0x0002	CAN Controller was read to late
	'				0x00	0x00	0x00	0x04	CAN_ERR_BUSLIGHT	0x0004  Bus Error: An error counter limit reached (96)
	'				0x00	0x00	0x00	0x08	CAN_ERR_BUSHEAVY	0x0008	Bus Error: An error counter limit reached (128)
	'				0x00	0x00	0x00	0x10	CAN_ERR_BUSOFF		0x0010	Bus Error: Can Controller went "Bus-Off"
	'		- If a CAN_ERR_BUSOFF status message is received, the CAN Controller must to be 
	'		  initialized again using the Init() function.  Otherwise, will be not possible 
	'		  to send/receive more messages. 
	'		- The message will be written to 'msgbuff'.
	'
	'  Possible Errors: NOVXD  QRCVEMPTY
	'
    <DllImport("PCAN_PCI.dll", EnTryPoint:="CAN_Read")> _
    Public Shared Function Read(ByRef msg As TPCANMsg) As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  VersionInfo()
	'  This function get the Version and copyright of the hardware as text 
	'  (max. 255 characters)
	'
	'  Possible Errors:  NOVXD
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_VersionInfo")> _
    Public Shared Function VersionInfo(ByVal buffer As StringBuilder) As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  SpecialFunktion()
	'  This function is an special function to be used "ONLY" for distributors
	'  Return: 1 - the given parameters and the parameters in the hardware agree 
	'   	   0 - otherwise
	'
	'  Possible Errors:  NOVXD
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_SpecialFunktion")> _
    Public Shared Function SpecialFunktion(ByVal distributorcode As Long, ByVal codenumber As Integer) As Integer
    End Function

	'/////////////////////////////////////////////////////////////////////////////
	'  ResetClient()
	'  This function delete the both queues (Transmit,Receive) of the CAN Controller 
	'  using a RESET
	'
	'  Possible Errors: ERR_ILLCLIENT ERR_NOVXD
	' 
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_ResetClient")> _
    Public Shared Function ResetClient() As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  MsgFilter(FromID, ToID, int Type)
	'  This function set the receive message filter of the CAN Controller.
	'  REMARK:
	'		- A quick register of all messages is possible using the parameters FromID and ToID = 0
	'		- Every call of this function maybe cause an extention of the receive filter of the 
	'		  CAN controller, which one can go briefly to RESET
	'		- New in Ver 2.x:
	'			* Standard frames will be put it down in the acc_mask/code as Bits 28..13
	'			* Hardware driver for 82C200 must to be moved to Bits 10..0 again!
	'	WARNING: 
	'		It is not guaranteed to receive ONLY the registered messages.
	'
	'  Possible Errors: NOVXD ILLCLIENT ILLNET REGTEST
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_MsgFilter")> _
    Public Shared Function MsgFilter(ByVal FromID As UInt32, ByVal ToID As UInt32, ByVal Type As Integer) As Integer
    End Function

	'//////////////////////////////////////////////////////////////////////////////
	'  ResetFilter()
	'  This function close completely the Message Filter of the Hardware.
	'  They will be no more messages received.
	'
	'  Possible Errors: NOVXD
	'
    <DllImport("PCAN_PCI.dll", EntryPoint:="CAN_ResetFilter")> _
    Public Shared Function ResetFilter() As Integer
    End Function
End Class
