Attribute VB_Name = "nican"
'/*****************************************************************************/
'/******************** N I - C A N   F R A M E    A P I ***********************/
'/*****************************************************************************/

'/***********************************************************************
'                            D A T A   T Y P E S
'***********************************************************************/
Public Const NC_SUCCESS = 0

'/* This two-part declaration is required for compilers which do not
'   provide support for native 64-bit integers.  */
Public Type NCTYPE_UINT64
   LowPart As Long
   HighPart As Long
End Type

'/* Type for ncWrite of CAN Network Interface Object */
Public Type NCTYPE_CAN_FRAME
   ArbitrationId As Long
   IsRemote As Byte
   DataLength As Byte
   Data(7) As Byte
End Type

'/* Type for ncRead of CAN Network Interface Object */
Public Type NCTYPE_CAN_FRAME_TIMED
   Timestamp As NCTYPE_UINT64
   ArbitrationId As Long
   IsRemote As Byte
   DataLength As Byte
   Data(7) As Byte
End Type

'/* Type for ncRead of CAN Network Interface Object (using FrameType instead of IsRemote).
'   Type for ncWrite of CAN Network Interface Object when timed transmission is enabled. */
Public Type NCTYPE_CAN_STRUCT
   Timestamp As NCTYPE_UINT64
   ArbitrationId As Long
   FrameType As Byte
   DataLength As Byte
   Data(7) As Byte
End Type

'/* Type for ncWrite of CAN Object */
Public Type NCTYPE_CAN_DATA
   Data(7) As Byte
End Type

'/* Type for ncRead of CAN Object */
Public Type NCTYPE_CAN_DATA_TIMED
   Timestamp As NCTYPE_UINT64
   Data(7) As Byte
End Type

'/***********************************************************************
'                              S T A T U S
'***********************************************************************/

'/* NCTYPE_STATUS

'   NI-CAN and NI-DNET use the standard NI status format.
'   This status format does not use bit-fields, but rather simple
'   codes in the lower byte, and a common base for the upper bits.
'   This standard NI status format ensures that all NI-CAN errors are located
'   in a specific range of codes.  Since this range does not overlap with
'   errors reported from other NI products, NI-CAN is supported within
'   environments such as LabVIEW and MeasurementStudio.

'   If your application currently uses the NI-CAN legacy error codes,
'   you must change to the standard NI error codes.  For instructions on updating
'   your code, refer to KnowledgeBase article # 2BBD8JHR on www.ni.com.

'   If you shipped an executable to your customers that uses the legacy
'   NI-CAN error codes, and you must upgrade those customers to the
'   newest version of NI-CAN, contact National Instruments Technical
'   Support to obtain instructions for re-enabling the legacy status format.
'   */
Public Const NICAN_WARNING_BASE = &H3FF62000
Public Const NICAN_ERROR_BASE = &HBFF62000

'/* Success values (you can simply use zero as well)  */
Public Const CanSuccess = 0
Public Const DnetSuccess = 0

'/* Numbers 0x001 to 0x0FF are used for status codes defined prior to
'   NI-CAN v1.5.  These codes can be mapped to/from the legacy status format.  */
Public Const CanErrFunctionTimeout = NICAN_ERROR_BASE Or &H1
Public Const CanErrWatchdogTimeout = NICAN_ERROR_BASE Or &H21
Public Const DnetErrConnectionTimeout = NICAN_ERROR_BASE Or &H41
Public Const DnetWarnConnectionTimeout = NICAN_WARNING_BASE Or &H41
Public Const CanErrScheduleTimeout = NICAN_ERROR_BASE Or &HA1
Public Const CanErrDriver = NICAN_ERROR_BASE Or &H2
Public Const CanWarnDriver = NICAN_WARNING_BASE Or &H2
Public Const CanErrBadNameSyntax = NICAN_ERROR_BASE Or &H3
Public Const CanErrBadIntfName = NICAN_ERROR_BASE Or &H23
Public Const CanErrBadCanObjName = NICAN_ERROR_BASE Or &H43
Public Const CanErrBadParam = NICAN_ERROR_BASE Or &H4
Public Const CanErrBadHandle = NICAN_ERROR_BASE Or &H24
Public Const CanErrBadAttributeValue = NICAN_ERROR_BASE Or &H5
Public Const CanErrAlreadyOpen = NICAN_ERROR_BASE Or &H6
Public Const CanWarnAlreadyOpen = NICAN_WARNING_BASE Or &H6
Public Const DnetErrOpenIntfMode = NICAN_ERROR_BASE Or &H26
Public Const DnetErrOpenConnType = NICAN_ERROR_BASE Or &H46
Public Const CanErrNotStopped = NICAN_ERROR_BASE Or &H7
Public Const CanErrOverflowWrite = NICAN_ERROR_BASE Or &H8
Public Const CanErrOverflowCard = NICAN_ERROR_BASE Or &H28
Public Const CanErrOverflowChip = NICAN_ERROR_BASE Or &H48
Public Const CanErrOverflowRxQueue = NICAN_ERROR_BASE Or &H68
Public Const CanWarnOldData = NICAN_WARNING_BASE Or &H9
Public Const CanErrNotSupported = NICAN_ERROR_BASE Or &HA
Public Const CanWarnComm = NICAN_WARNING_BASE Or &HB
Public Const CanErrComm = NICAN_ERROR_BASE Or &HB
Public Const CanWarnCommStuff = NICAN_WARNING_BASE Or &H2B
Public Const CanErrCommStuff = NICAN_ERROR_BASE Or &H2B
Public Const CanWarnCommFormat = NICAN_WARNING_BASE Or &H4B
Public Const CanErrCommFormat = NICAN_ERROR_BASE Or &H4B
Public Const CanWarnCommNoAck = NICAN_WARNING_BASE Or &H6B
Public Const CanErrCommNoAck = NICAN_ERROR_BASE Or &H6B
Public Const CanWarnCommTx1Rx0 = NICAN_WARNING_BASE Or &H8B
Public Const CanErrCommTx1Rx0 = NICAN_ERROR_BASE Or &H8B
Public Const CanWarnCommTx0Rx1 = NICAN_WARNING_BASE Or &HAB
Public Const CanErrCommTx0Rx1 = NICAN_ERROR_BASE Or &HAB
Public Const CanWarnCommBadCRC = NICAN_WARNING_BASE Or &HCB
Public Const CanErrCommBadCRC = NICAN_ERROR_BASE Or &HCB
Public Const CanWarnCommUnknown = NICAN_WARNING_BASE Or &HEB
Public Const CanErrCommUnknown = NICAN_ERROR_BASE Or &HEB
Public Const CanWarnTransceiver = NICAN_WARNING_BASE Or &HC
Public Const CanWarnRsrcLimitQueues = NICAN_WARNING_BASE Or &H2D
Public Const CanErrRsrcLimitQueues = NICAN_ERROR_BASE Or &H2D
Public Const DnetErrRsrcLimitIO = NICAN_ERROR_BASE Or &H4D
Public Const DnetErrRsrcLimitWriteSrvc = NICAN_ERROR_BASE Or &H6D
Public Const DnetErrRsrcLimitReadSrvc = NICAN_ERROR_BASE Or &H8D
Public Const DnetErrRsrcLimitRespPending = NICAN_ERROR_BASE Or &HAD
Public Const DnetWarnRsrcLimitRespPending = NICAN_WARNING_BASE Or &HAD
Public Const CanErrRsrcLimitRtsi = NICAN_ERROR_BASE Or &HCD
Public Const DnetErrNoReadAvail = NICAN_ERROR_BASE Or &HE
Public Const DnetErrBadMacId = NICAN_ERROR_BASE Or &HF
Public Const DnetErrDevInitOther = NICAN_ERROR_BASE Or &H10
Public Const DnetErrDevInitIoConn = NICAN_ERROR_BASE Or &H30
Public Const DnetErrDevInitInputLen = NICAN_ERROR_BASE Or &H50
Public Const DnetErrDevInitOutputLen = NICAN_ERROR_BASE Or &H70
Public Const DnetErrDevInitEPR = NICAN_ERROR_BASE Or &H90
Public Const DnetErrDevInitVendor = NICAN_ERROR_BASE Or &HB0
Public Const DnetErrDevInitDevType = NICAN_ERROR_BASE Or &HD0
Public Const DnetErrDevInitProdCode = NICAN_ERROR_BASE Or &HF0
Public Const DnetErrDeviceMissing = NICAN_ERROR_BASE Or &H11
Public Const DnetWarnDeviceMissing = NICAN_WARNING_BASE Or &H11
Public Const DnetErrFragmentation = NICAN_ERROR_BASE Or &H12
Public Const DnetErrIntfNotOpen = NICAN_ERROR_BASE Or &H33
Public Const DnetErrErrorResponse = NICAN_ERROR_BASE Or &H14
Public Const CanWarnNotificationPending = NICAN_WARNING_BASE Or &H15
Public Const CanErrConfigOnly = NICAN_ERROR_BASE Or &H17
Public Const CanErrPowerOnSelfTest = NICAN_ERROR_BASE Or &H18

Public Const LinErrCommBit = NICAN_ERROR_BASE Or &H1A0
Public Const LinErrCommFraming = NICAN_ERROR_BASE Or &H1A1
Public Const LinErrCommResponseTimout = NICAN_ERROR_BASE Or &H1A2
Public Const LinErrCommWakeup = NICAN_ERROR_BASE Or &H1A3
Public Const LinErrCommForm = NICAN_ERROR_BASE Or &H1A4
Public Const LinErrCommBusNoPowered = NICAN_ERROR_BASE Or &H1A5

'//The percent difference between the passed in baud rate and the actual baud rate was greater than or equal to 0.5%.  LIN 2.0 specifies a clock tolerance of less than 0.5% for a master and less than 1.5% for a slave.
Public Const LinWarnBaudRateOutOfTolerance = NICAN_WARNING_BASE Or &H1A6

'// Numbers 0x100 to 0x1FF are used for the NI-CAN Frame API, the NI-DNET API, and LIN.
'// Numbers 0x1A0 to 0x1DF are used for the NI-CAN Frame API for LIN .
'// Numbers 0x200 to 0x2FF are used for the NI-CAN Channel API.
'// Numbers 0x300 to 0x3FF are reserved for future use.
Public Const CanErrMaxObjects = NICAN_ERROR_BASE Or &H100
Public Const CanErrMaxChipSlots = NICAN_ERROR_BASE Or &H101
Public Const CanErrBadDuration = NICAN_ERROR_BASE Or &H102
Public Const CanErrFirmwareNoResponse = NICAN_ERROR_BASE Or &H103
Public Const CanErrBadIdOrOpcode = NICAN_ERROR_BASE Or &H104
Public Const CanWarnBadSizeOrLength = NICAN_WARNING_BASE Or &H105
Public Const CanErrBadSizeOrLength = NICAN_ERROR_BASE Or &H105
Public Const CanErrNotifAlreadyInUse = NICAN_ERROR_BASE Or &H107
Public Const CanErrOneProtocolPerCard = NICAN_ERROR_BASE Or &H108
Public Const CanWarnPeriodsTooFast = NICAN_WARNING_BASE Or &H109
Public Const CanErrDllNotFound = NICAN_ERROR_BASE Or &H10A
Public Const CanErrFunctionNotFound = NICAN_ERROR_BASE Or &H10B
Public Const CanErrLangIntfRsrcUnavail = NICAN_ERROR_BASE Or &H10C
Public Const CanErrRequiresNewHwSeries = NICAN_ERROR_BASE Or &H10D
Public Const CanErrHardwareNotSupported = NICAN_ERROR_BASE Or &H10D
Public Const CanErrSeriesOneOnly = NICAN_ERROR_BASE Or &H10E
Public Const CanErrSetAbsTime = NICAN_ERROR_BASE Or &H10F
Public Const CanErrBothApiSameIntf = NICAN_ERROR_BASE Or &H110
Public Const CanErrWaitOverlapsSameObj = NICAN_ERROR_BASE Or &H111
Public Const CanErrNotStarted = NICAN_ERROR_BASE Or &H112
Public Const CanErrConnectTwice = NICAN_ERROR_BASE Or &H113
Public Const CanErrConnectUnsupported = NICAN_ERROR_BASE Or &H114
Public Const CanErrStartTrigBeforeFunc = NICAN_ERROR_BASE Or &H115
Public Const CanErrStringSizeTooLarge = NICAN_ERROR_BASE Or &H116
Public Const CanErrQueueReqdForReadMult = NICAN_ERROR_BASE Or &H117
Public Const CanErrHardwareInitFailed = NICAN_ERROR_BASE Or &H118
Public Const CanErrOldDataLost = NICAN_ERROR_BASE Or &H119
Public Const CanErrOverflowChannel = NICAN_ERROR_BASE Or &H11A
Public Const CanErrUnsupportedModeMix = NICAN_ERROR_BASE Or &H11C
Public Const CanErrNoNetIntfConfig = NICAN_ERROR_BASE Or &H11D
Public Const CanErrBadTransceiverMode = NICAN_ERROR_BASE Or &H11E
Public Const CanErrWrongTransceiverAttr = NICAN_ERROR_BASE Or &H11F
Public Const CanErrRequiresXS = NICAN_ERROR_BASE Or &H120
Public Const CanErrDisconnected = NICAN_ERROR_BASE Or &H121
Public Const CanErrNoTxForListenOnly = NICAN_ERROR_BASE Or &H122
Public Const CanErrSetOnly = NICAN_ERROR_BASE Or &H123
Public Const CanErrBadBaudRate = NICAN_ERROR_BASE Or &H124
Public Const CanErrOverflowFrame = NICAN_ERROR_BASE Or &H125
Public Const CanWarnRTSITooFast = NICAN_WARNING_BASE Or &H126
Public Const CanErrNoTimebase = NICAN_ERROR_BASE Or &H127
Public Const CanErrTimerRunning = NICAN_ERROR_BASE Or &H128
Public Const DnetErrUnsupportedHardware = NICAN_ERROR_BASE Or &H129
Public Const CanErrInvalidLogfile = NICAN_ERROR_BASE Or &H12A
Public Const CanErrMaxPeriodicObjects = NICAN_ERROR_BASE Or &H130
Public Const CanErrUnknownHardwareAttribute = NICAN_ERROR_BASE Or &H131
Public Const CanErrDelayFrameNotSupported = NICAN_ERROR_BASE Or &H132
Public Const CanErrVirtualBusTimingOnly = NICAN_ERROR_BASE Or &H133

Public Const CanErrVirtualNotSupported = NICAN_ERROR_BASE Or &H135
Public Const CanErrWriteMultLimit = NICAN_ERROR_BASE Or &H136
Public Const CanErrObsoletedHardware = NICAN_ERROR_BASE Or &H137
Public Const CanErrVirtualBusTimingMismatch = NICAN_ERROR_BASE Or &H138
Public Const CanErrVirtualBusOnly = NICAN_ERROR_BASE Or &H139
Public Const CanErrConversionTimeRollback = NICAN_ERROR_BASE Or &H13A
Public Const CanErrInterFrameDelayExceeded = NICAN_ERROR_BASE Or &H140
Public Const CanErrLogConflict = NICAN_ERROR_BASE Or &H141
Public Const CanErrBootLoaderUpdated = NICAN_ERROR_BASE Or &H142

'/* Included for backward compatibility with older versions of NI-CAN */
Public Const CanWarnLowSpeedXcvr = NICAN_WARNING_BASE Or &HC
Public Const CanErrOverflowRead = NICAN_ERROR_BASE Or &H28

'/***********************************************************************
'                          A T T R I B U T E   I D S
'***********************************************************************/

'/* Attributes of the NI driver (NCTYPE_ATTRID values)
'      For every attribute ID, its full name, datatype,
'      permissions, and applicable objects are listed in the comment.
'   */

'/* Current State, NCTYPE_STATE, Get, CAN Interface/Object */
Public Const NC_ATTR_STATE = &H80000009
'/* Status, NCTYPE_STATUS, Get, CAN Interface/Object */
Public Const NC_ATTR_STATUS = &H8000000A
'/* Baud Rate, Set, CAN Interface
'            Note that in addition to standard baud rates like 125000,
'            this attribute also allows you to program non-standard
'            or otherwise uncommon baud rates.  If bit 31 (0x80000000)
'            is set, the low 16 bits of this attribute are programmed
'            directly into the bit timing registers of the CAN
'            communications controller.  The low byte is programmed as
'            BTR0 of the Intel 82527 chip (8MHz clock), and the high byte
'            as BTR1, resulting in the following bit map:
'               15  14  13  12  11  10   9   8   7   6   5   4   3   2   1   0
'               sam (tseg2 - 1) (  tseg1 - 1   ) (sjw-1) (     presc - 1     )
'            For example, baud rate 0x80001C03 programs the CAN communications
'            controller for 125000 baud (same baud rate 125000 decimal).
'            For more information, refer to the reference manual for
'            any CAN communications controller chip.  */
Public Const NC_ATTR_BAUD_RATE = &H80000007
'/* Start On Open, NCTYPE_BOOL, Set, CAN Interface */
Public Const NC_ATTR_START_ON_OPEN = &H80000006
'/* Absolute Time, NCTYPE_ABS_TIME, Get, CAN Interface */
Public Const NC_ATTR_ABS_TIME = &H80000008
'/* Period, NCTYPE_DURATION, Set, CAN Object */
Public Const NC_ATTR_PERIOD = &H8000000F
'/* Timestamping, NCTYPE_BOOL, Set, CAN Interface */
Public Const NC_ATTR_TIMESTAMPING = &H80000010
'/* Read Pending, NCTYPE_UINT32, Get, CAN Interface/Object */
Public Const NC_ATTR_READ_PENDING = &H80000011
'/* Write Pending, NCTYPE_UINT32, Get, CAN Interface/Object */
Public Const NC_ATTR_WRITE_PENDING = &H80000012
'/* Read Queue Length, NCTYPE_UINT32, Set, CAN Interface/Object */
Public Const NC_ATTR_READ_Q_LEN = &H80000013
'/* Write Queue Length, NCTYPE_UINT32, Set, CAN Interface/Object */
Public Const NC_ATTR_WRITE_Q_LEN = &H80000014
'/* Receive Changes Only, NCTYPE_BOOL, Set, CAN Object */
Public Const NC_ATTR_RX_CHANGES_ONLY = &H80000015
'/* Communication Type, NCTYPE_COMM_TYPE, Set, CAN Object */
Public Const NC_ATTR_COMM_TYPE = &H80000016
'/* RTSI Mode, NCTYPE_RTSI_MODE, Set, CAN Interface/Object */
Public Const NC_ATTR_RTSI_MODE = &H80000017
'/* RTSI Signal, NCTYPE_UINT8, Set, CAN Interface/Object */
Public Const NC_ATTR_RTSI_SIGNAL = &H80000018
'/* RTSI Signal Behavior, NCTYPE_RTSI_SIG_BEHAV,
'         Set, CAN Interface/Object */
Public Const NC_ATTR_RTSI_SIG_BEHAV = &H80000019
'/* RTSI Frame for CAN Object, NCTYPE_UINT32, Get/Set, CAN Object */
Public Const NC_ATTR_RTSI_FRAME = &H80000020
'/* RTSI Skip Count, NCTYPE_UINT32, Set, CAN Interface/Object */
Public Const NC_ATTR_RTSI_SKIP = &H80000021

'/* Standard Comparator, NCTYPE_CAN_ARBID, Set, CAN Interface */
Public Const NC_ATTR_COMP_STD = &H80010001
'/* Standard Mask (11 bits), NCTYPE_UINT32, Set, CAN Interface */
Public Const NC_ATTR_MASK_STD = &H80010002
'/* Extended Comparator (29 bits), NCTYPE_CAN_ARBID, Set, CAN Interface */
Public Const NC_ATTR_COMP_XTD = &H80010003
'/* Extended Mask (29 bits), NCTYPE_UINT32, Set, CAN Interface */
Public Const NC_ATTR_MASK_XTD = &H80010004
'/* Transmit By Response, NCTYPE_BOOL, Set, CAN Object */
Public Const NC_ATTR_TX_RESPONSE = &H80010006
'/* Data Frame Length, NCTYPE_UINT32, Set, CAN Object */
Public Const NC_ATTR_DATA_LEN = &H80010007
'/* Log Comm Errors, NCTYPE_BOOL, Get/Set,
'         CAN Interface (Low-speed boards only) */
Public Const NC_ATTR_LOG_COMM_ERRS = &H8001000A
'/* Notify Multiple Length, NCTYPE_UINT32, Get/Set, CAN Interface/Object */
Public Const NC_ATTR_NOTIFY_MULT_LEN = &H8001000B
'/* Receive Queue Length, NCTYPE_UINT32, Set, CAN Interface */
Public Const NC_ATTR_RX_Q_LEN = &H8001000C
'/* Is bus timing enabled on virtual hardware,
'            NC_FALSE: Used in case of frame-channel conversion
'            NC_TRUE: NI-CAN will simluate the bus timing on TX, set timestamp
'            of the frame when it is received,default behavior,NCTYPE_UINT32,Get/Set,
'            CAN Interface   */

Public Const NC_ATTR_VIRTUAL_BUS_TIMING = &HA0000031
'/* Transmit mode,NCTYPE_UINT32,Get/Set,
'             0 (immediate) [default], 1 (timestamped)
'            CAN Interface */
Public Const NC_ATTR_TRANSMIT_MODE = &H80020029
'/* Log start trigger in the read queue,NCTYPE_UINT32,Get/Set,
'            CAN Interface*/
Public Const NC_ATTR_LOG_START_TRIGGER = &H80020031
'/* Timestamp format in absolute or relative mode, NCTYPE_UINT32, Get/Set,
'            CAN Interface .*/
Public Const NC_ATTR_TIMESTAMP_FORMAT = &H80020032
'/* Rate of the incoming clock pulses.Rate can either be
'            10(M Series DAQ) or 20(E series DAQ) Mhz,NCTYPE_UINT32,Get/Set,
'            CAN Interface */
Public Const NC_ATTR_MASTER_TIMEBASE_RATE = &H80020033
'/* Number of frames that can be written without overflow,NCTYPE_UINT32, Get,CAN Interface*/
Public Const NC_ATTR_WRITE_ENTRIES_FREE = &H80020034
'/* Timeline recovery,NCTYPE_UINT32,Get/Set,
'             Valid only in timestamped transmit mode (NC_ATTR_TRANSMIT_MODE = 1)
'             0 (false) [default], 1 (true)
'             CAN Interface .*/

Public Const NC_ATTR_TIMELINE_RECOVERY = &H80020035
'/* Log Bus Errors, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             0 (false) [default], 1 (true)*/
Public Const NC_ATTR_LOG_BUS_ERROR = &H80020037
'/* Log Transceiver Faults, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             Log NERR into NI read queue
'             0 (false) [default], 1 (true)*/
Public Const NC_ATTR_LOG_TRANSCEIVER_FAULT = &H80020038
'/* Termination Select, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             CAN HS - Not Selectable
'             CAN LS - 0 (1k ohm) [default], 1 (5k ohm)
'             LIN - 0 (disabled) [default], 1 (enabled) */
Public Const NC_ATTR_TERMINATION = &H80020041
'/*** LIN Attributes **/
'/* LIN - Sleep, NCTYPE_UINT32, Get/Set,
'             0 (False) [default], 1 (true) */
Public Const NC_ATTR_LIN_SLEEP = &H80020042
'/* LIN - Check Sum Type, NCTYPE_UINT32, Get/Set,
'             0 (classic) [default], 1 (enhanced) */
Public Const NC_ATTR_LIN_CHECKSUM_TYPE = &H80020043
'/* LIN - Response Timeout, NCTYPE_UINT32, Get/Set,
'             0 [default], 1 - 65535 (in 50 us increments for LIN specific frame response timing) */
Public Const NC_ATTR_LIN_RESPONSE_TIMEOUT = &H80020044
'/* LIN - Enable DLC Check, NCTYPE_UINT32, Get/Set,
'             0 (false) [default], 1 (true) */
Public Const NC_ATTR_LIN_ENABLE_DLC_CHECK = &H80020045
'/* LIN - Log Wakeup, NCTYPE_UINT32, Get/Set,
'             0 (false) [default], 1 (true) */
Public Const NC_ATTR_LIN_LOG_WAKEUP = &H80020046

'/* Attributes specific to Series 2 hardware (not supported on Series 1). */

'/* Enable Listen Only on SJA1000, NCTYPE_UINT32, Get/Set
'             0 (false) [default], 1 (enable)*/
Public Const NC_ATTR_LISTEN_ONLY = &H80010010
'/* Returns Receive Error Counter from SJA1000, NCTYPE_UINT32, Get, CAN Interface/Object */
Public Const NC_ATTR_RX_ERROR_COUNTER = &H80010011
'/* Returns Send Error Counter from SJA1000, NCTYPE_UINT32, Get, CAN Interface/Object */
Public Const NC_ATTR_TX_ERROR_COUNTER = &H80010012
'/* Series 2 Comparator, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             11 bits or 29 bits depending on NC_ATTR_SERIES2_FILTER_MODE
'             0 [default] */
Public Const NC_ATTR_SERIES2_COMP = &H80010013
'/* Series 2 Mask, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             11 bits or 29 bits depending on NC_ATTR_SERIES2_FILTER_MODE
'             0xFFFFFFFF [default] */
Public Const NC_ATTR_SERIES2_MASK = &H80010014
'/* Series 2 Filter Mode, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             NC_FILTER_SINGLE_STANDARD [default], NC_FILTER_SINGLE_EXTENDED,
'             NC_FILTER_DUAL_EXTENDED, NC_FILTER_SINGLE_STANDARD*/
Public Const NC_ATTR_SERIES2_FILTER_MODE = &H80010015
'/* Self Reception, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             Echo transmitted frames in read queue
'             0 (false) [default], 1 (true) */
Public Const NC_ATTR_SELF_RECEPTION = &H80010016
'/* Single Shot Transmit, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             Single Shot = No retry on error transmissions
'             0 (false) [default]. 1 (true) */
Public Const NC_ATTR_SINGLE_SHOT_TX = &H80010017
Public Const NC_ATTR_BEHAV_FINAL_OUT = &H80010018
'/* Transceiver Mode, NCTYPE_UINT32, Get/Set, CAN Interface/Object
'             NC_TRANSCEIVER_MODE_NORMAL [default], NC_TRANSCEIVER_MODE_SLEEP,
'             NC_TRANSCEIVER_MODE_SW_HIGHSPEED, NC_TRANSCEIVER_MODE_SW_WAKEUP*/
Public Const NC_ATTR_TRANSCEIVER_MODE = &H80010019
'/* Transceiver External Out, NCTYPE_UINT32, Bitmask, Get/Set, CAN Interface
'             on XS cards and external transceivers, it sets MODE0 and MODE1 pins on CAN port,
'             and sleep of CAN controller chip
'             NC_TRANSCEIVER_OUT_MODE0 | NC_TRANSCEIVER_OUT_MODE1 [default]
'             NC_TRANSCEIVER_OUT_MODE0, NC_TRANSCEIVER_OUT_MODE1, NC_TRANSCEIVER_OUT_SLEEP*/

Public Const NC_ATTR_TRANSCEIVER_EXTERNAL_OUT = &H8001001A
'/* Transceiver External In, NCTYPE_UINT32, Get, CAN Interface
'             on XS cards, reads STATUS pin on CAN port*/
Public Const NC_ATTR_TRANSCEIVER_EXTERNAL_IN = &H8001001B
'/* Error Code Capture and Arbitration Lost Capture, NCTYPE_UINT32, Get, CAN Interface/Object
'             Returns Error Code Capture and Arbitration Lost Capture registers */
Public Const NC_ATTR_SERIES2_ERR_ARB_CAPTURE = &H8001001C
'/* Transceiver Type, NCTYPE_UINT32, Get/Set, CAN Interface
'             NC_TRANSCEIVER_TYPE_DISC (disconnect), NC_TRANSCEIVER_TYPE_EXT (external),
'             NC_TRANSCEIVER_TYPE_HS (high speed), NC_TRANSCEIVER_TYPE_LS (low speed),
'             NC_TRANSCEIVER_TYPE_SW (single wire)*/
Public Const NC_ATTR_TRANSCEIVER_TYPE = &H80020007

'/* Informational attributes (hardware and version info).  Get, CAN Interface only
'   These attribute IDs can be used with ncGetHardwareInfo and ncGetAttribute.  */
Public Const NC_ATTR_NUM_CARDS = &H80020002
Public Const NC_ATTR_HW_SERIAL_NUM = &H80020003
Public Const NC_ATTR_HW_FORMFACTOR = &H80020004
Public Const NC_ATTR_HW_SERIES = &H80020005
Public Const NC_ATTR_NUM_PORTS = &H80020006
Public Const NC_ATTR_HW_TRANSCEIVER = &H80020007
Public Const NC_ATTR_INTERFACE_NUM = &H80020008
Public Const NC_ATTR_VERSION_MAJOR = &H80020009
Public Const NC_ATTR_VERSION_MINOR = &H8002000A
Public Const NC_ATTR_VERSION_UPDATE = &H8002000B
Public Const NC_ATTR_VERSION_PHASE = &H8002000C
Public Const NC_ATTR_VERSION_BUILD = &H8002000D
Public Const NC_ATTR_VERSION_COMMENT = &H8002000E

'/* Included for backward compatibility with older versions of NI-CAN */
'/* NCTYPE_ATTRID values, CAN Interface/Object */
Public Const NC_ATTR_PROTOCOL = &H80000001
Public Const NC_ATTR_PROTOCOL_VERSION = &H80000002
Public Const NC_ATTR_SOFTWARE_VERSION = &H80000003
Public Const NC_ATTR_BKD_READ_SIZE = &H8000000B
Public Const NC_ATTR_BKD_WRITE_SIZE = &H8000000C
Public Const NC_ATTR_BKD_TYPE = &H8000000D
Public Const NC_ATTR_BKD_WHEN_USED = &H8000000E
Public Const NC_ATTR_BKD_PERIOD = &H8000000F
Public Const NC_ATTR_BKD_CHANGES_ONLY = &H80000015
Public Const NC_ATTR_SERIAL_NUMBER = &H800000A0
Public Const NC_ATTR_CAN_BIT_TIMINGS = &H80010005
Public Const NC_ATTR_BKD_CAN_RESPONSE = &H80010006
Public Const NC_ATTR_CAN_DATA_LENGTH = &H80010007
Public Const NC_ATTR_CAN_COMP_STD = &H80010001
Public Const NC_ATTR_CAN_MASK_STD = &H80010002
Public Const NC_ATTR_CAN_COMP_XTD = &H80010003
Public Const NC_ATTR_CAN_MASK_XTD = &H80010004
Public Const NC_ATTR_CAN_TX_RESPONSE = &H80010006
Public Const NC_ATTR_NOTIFY_MULT_SIZE = &H8001000B
Public Const NC_ATTR_RESET_ON_START = &H80010008
Public Const NC_ATTR_NET_SYNC_COUNT = &H8001000D
Public Const NC_ATTR_IS_NET_SYNC = &H8001000E
Public Const NC_ATTR_START_TRIG_BEHAVIOR = &H80010023
'/* NCTYPE_BKD_TYPE values */
Public Const NC_BKD_TYPE_PEER2PEER = &H1
Public Const NC_BKD_TYPE_REQUEST = &H2
Public Const NC_BKD_TYPE_RESPONSE = &H3
'/* NCTYPE_BKD_WHEN values */
Public Const NC_BKD_WHEN_PERIODIC = &H1
Public Const NC_BKD_WHEN_UNSOLICITED = &H2
'/* Special values for background read/write data
'      sizes (NC_ATTR_BKD_READ_SIZE and NC_ATTR_BKD_WRITE_SIZE). */
Public Const NC_BKD_CAN_ZERO_SIZE = &H8000

'/***********************************************************************
'                    O T H E R   C O N S T A N T S
'***********************************************************************/

'/* NCTYPE_BOOL (true/false values) */
Public Const NC_TRUE = 1
Public Const NC_FALSE = 0

'/* NCTYPE_DURATION (values in one millisecond ticks) */
Public Const NC_DURATION_NONE = 0
Public Const NC_DURATION_INFINITE = &HFFFFFFFF
Public Const NC_DURATION_1MS = 1
Public Const NC_DURATION_10MS = 10
Public Const NC_DURATION_100MS = 100
Public Const NC_DURATION_1SEC = 1000
Public Const NC_DURATION_10SEC = 10000
Public Const NC_DURATION_100SEC = 100000
Public Const NC_DURATION_1MIN = 60000

'/* NCTYPE_PROTOCOL (values for supported protocols) */
Public Const NC_PROTOCOL_CAN = 1
Public Const NC_PROTOCOL_DNET = 2
Public Const NC_PROTOCOL_LIN = 3

'/* NCTYPE_STATE (bit masks for states).
'   Refer to other NC_ST values below for backward compatibility. */
Public Enum NC_STATE_ENUM
'/* Any object */
   NC_ST_READ_AVAIL = &H1
'/* Any object */
   NC_ST_WRITE_SUCCESS = &H2

'/* Expl Msg object only */
   NC_ST_ESTABLISHED = &H8
'/* Included for backward compatibility with older versions of NI-CAN */
'/* NCTYPE_STATE (bit masks for states): Prior to NI-CAN 2.0,
'      the Stopped state emabled detection of the Bus Off condition,
'      which stopped communication independent of NI-CAN functions such as ncAction(Stop).
'      Since the Bus Off condition is an error, and errors are detected automatically
'      in v2.0 and later, this state is now obsolete. For compatibility, it may be
'      returned as the DetectedState of ncWaitForState, but this bit should be
'      ignored by new NI-CAN applications. */
   NC_ST_STOPPED = &H4
'/* NCTYPE_STATE (bit masks for states): Prior to NI-CAN 2.0,
'      the Error state emabled detection of background errors (such as the Bus Off condition).
'      For v2.0 and later, the ncWaitForState function returns automatically when any
'      error occurs, so this state is now obsolete. For compatibility, it may be
'      returned as the DetectedState of ncWaitForState, but this bit should be
'      ignored by new NI-CAN applications. */
   NC_ST_ERROR = &H10
'/* NCTYPE_STATE (bit masks for states): Prior to NI-CAN 2.0,
'      the Warning state emabled detection of background warnings (such as Error Passive).
'      For v2.0 and later, the ncWaitForState function will not abort when a warning occurs,
'      but it will return the warning, so this state is now obsolete. For compatibility,
'      it may be returned as the DetectedState of ncWaitForState, but this bit should be
'      ignored by new NI-CAN applications. */
   NC_ST_WARNING = &H20
End Enum

'/* Any object */
Public Const NC_ST_READ_MULT = &H8
'/* State to detect when a CAN port has been woken up remotely.*/
Public Const NC_ST_REMOTE_WAKEUP = &H40

'/* NI only */
Public Const NC_ST_WRITE_MULT = &H80

'/* NCTYPE_OPCODE values */
Public Enum NC_OPCODE_ENUM
'/* Interface object */
   NC_OP_START = &H80000001
'/* Interface object */
   NC_OP_STOP = &H80000002
'/* Interface object */
   NC_OP_RESET = &H80000003

'/* Interface object only */
   NC_OP_ACTIVE = &H80000004
'/* Interface object only */
   NC_OP_IDLE = &H80000005
End Enum

'/* Interface object, Param is used */
Public Const NC_OP_RTSI_OUT = &H80000004

'/* NCTYPE_BAUD_RATE (values for baud rates) */
Public Const NC_BAUD_10K = 10000
Public Const NC_BAUD_100K = 100000
Public Const NC_BAUD_125K = 125000
Public Const NC_BAUD_250K = 250000
Public Const NC_BAUD_500K = 500000
Public Const NC_BAUD_1000K = 1000000

'/* NCTYPE_COMM_TYPE values */
Public Const NC_CAN_COMM_RX_UNSOL = &H0
Public Const NC_CAN_COMM_TX_BY_CALL = &H1
Public Const NC_CAN_COMM_RX_PERIODIC = &H2
Public Const NC_CAN_COMM_TX_PERIODIC = &H3
Public Const NC_CAN_COMM_RX_BY_CALL = &H4
Public Const NC_CAN_COMM_TX_RESP_ONLY = &H5
Public Const NC_CAN_COMM_TX_WAVEFORM = &H6

'/* NCTYPE_RTSI_MODE values */
Public Const NC_RTSI_NONE = 0
Public Const NC_RTSI_TX_ON_IN = 1
Public Const NC_RTSI_TIME_ON_IN = 2
Public Const NC_RTSI_OUT_ON_RX = 3
Public Const NC_RTSI_OUT_ON_TX = 4
Public Const NC_RTSI_OUT_ACTION_ONLY = 5

'/* NCTYPE_RTSI_SIG_BEHAV values */
Public Const NC_RTSISIG_PULSE = 0
Public Const NC_RTSISIG_TOGGLE = 1

'/* NC_ATTR_START_TRIG_BEHAVIOUR values */
Public Const NC_START_TRIG_NONE = 0
Public Const NC_RESET_TIMESTAMP_ON_START = 1
Public Const NC_LOG_START_TRIG = 2

'/* NCTYPE_CAN_ARBID (bit masks) */
'/* When frame type is data (NC_FRMTYPE_DATA) or remote (NC_FRMTYPE_REMOTE),
'         this bit in ArbitrationId is interpreted as follows:
'         If this bit is clear, the ArbitrationId is standard (11-bit).
'         If this bit is set, the ArbitrationId is extended (29-bit).  */
Public Const NC_FL_CAN_ARBID_XTD = &H20000000

'/* NCTYPE_CAN_ARBID (special values) */
'/* Special value used to disable comparators. */

Public Const NC_CAN_ARBID_NONE = &HCFFFFFFF

'/* Values for the FrameType (IsRemote) field of CAN frames.  */
Public Const NC_FRMTYPE_DATA = 0
Public Const NC_FRMTYPE_REMOTE = &H1
'/* NI only */
Public Const NC_FRMTYPE_COMM_ERR = &H2
Public Const NC_FRMTYPE_RTSI = &H3

'/* Status for Driver NetIntf (and Driver CanObjs):
'            ArbID=0
'            DataLength=0
'            Timestamp=<time of start trigger>
'  */
Public Const NC_FRMTYPE_TRIG_START = &H4
Public Const NC_FRMTYPE_DELAY = &H5

'/* CAN Frame to indicate Bus Error. Format: Byte 0:CommState,Byte1:Tx Err Ctr,
'     Byte2: Rx Err Ctr, Byte 3: ECC register. Byte 4-7: Don't care.*/
Public Const NC_FRMTYPE_BUS_ERR = &H6
'/* Frame to indicate status of Nerr. Byte 0: 1 = NERR , 0 = No Fault.*/
Public Const NC_FRMTYPE_TRANSCEIVER_ERR = &H7

'//Response frame for LIN communication
Public Const NC_FRMTYPE_LIN_RESPONSE_ENTRY = &H10
'//Header frame for LIN communication
Public Const NC_FRMTYPE_LIN_HEADER = &H11
'//Full frame for LIN communication
Public Const NC_FRMTYPE_LIN_FULL = &H12
'//Wakeup frame for LIN communication
Public Const NC_FRMTYPE_LIN_WAKEUP_RECEIVED = &H13
'//Sleep frame for LIN communication
Public Const NC_FRMTYPE_LIN_BUS_INACTIVE = &H14
'//Bus error frame for LIN communication
Public Const NC_FRMTYPE_LIN_BUS_ERR = &H15

'/* Special values for CAN mask attributes (NC_ATTR_MASK_STD/XTD) */
Public Const NC_MASK_STD_MUSTMATCH = &H7FF
Public Const NC_MASK_XTD_MUSTMATCH = &H1FFFFFFF
Public Const NC_MASK_STD_DONTCARE = &H0
Public Const NC_MASK_XTD_DONTCARE = &H0
Public Const NC_SERIES2_MASK_MUSTMATCH = &H0
Public Const NC_SERIES2_MASK_DONTCARE = &HFFFFFFFF

'// Values for NC_ATTR_HW_SERIES attribute
Public Const NC_HW_SERIES_1 = 0
Public Const NC_HW_SERIES_2 = 1
Public Const NC_HW_SERIES_847X = 2
Public Const NC_HW_SERIES_847X_SYNC = 3

'// Values for SourceTerminal of ncConnectTerminals.
Public Const NC_SRC_TERM_RTSI0 = 0
Public Const NC_SRC_TERM_RTSI1 = 1
Public Const NC_SRC_TERM_RTSI2 = 2
Public Const NC_SRC_TERM_RTSI3 = 3
Public Const NC_SRC_TERM_RTSI4 = 4
Public Const NC_SRC_TERM_RTSI5 = 5
Public Const NC_SRC_TERM_RTSI6 = 6
Public Const NC_SRC_TERM_RTSI_CLOCK = 7
Public Const NC_SRC_TERM_PXI_STAR = 8
Public Const NC_SRC_TERM_INTF_RECEIVE_EVENT = 9
Public Const NC_SRC_TERM_INTF_TRANSCEIVER_EVENT = 10
Public Const NC_SRC_TERM_PXI_CLK10 = 11
Public Const NC_SRC_TERM_20MHZ_TIMEBASE = 12
Public Const NC_SRC_TERM_10HZ_RESYNC_CLOCK = 13
Public Const NC_SRC_TERM_START_TRIGGER = 14

'// Values for DestinationTerminal of ncConnectTerminals.
Public Const NC_DEST_TERM_RTSI0 = 0
Public Const NC_DEST_TERM_RTSI1 = 1
Public Const NC_DEST_TERM_RTSI2 = 2
Public Const NC_DEST_TERM_RTSI3 = 3
Public Const NC_DEST_TERM_RTSI4 = 4
Public Const NC_DEST_TERM_RTSI5 = 5
Public Const NC_DEST_TERM_RTSI6 = 6
Public Const NC_DEST_TERM_RTSI_CLOCK = 7
Public Const NC_DEST_TERM_MASTER_TIMEBASE = 8
Public Const NC_DEST_TERM_10HZ_RESYNC_CLOCK = 9
Public Const NC_DEST_TERM_START_TRIGGER = 10

'// Values for NC_ATTR_HW_FORMFACTOR attribute
Public Const NC_HW_FORMFACTOR_PCI = 0
Public Const NC_HW_FORMFACTOR_PXI = 1
Public Const NC_HW_FORMFACTOR_PCMCIA = 2
Public Const NC_HW_FORMFACTOR_AT = 3
Public Const NC_HW_FORMFACTOR_USB = 4

'// Values for NC_ATTR_TRANSCEIVER_TYPE attribute
Public Const NC_TRANSCEIVER_TYPE_HS = 0
Public Const NC_TRANSCEIVER_TYPE_LS = 1
Public Const NC_TRANSCEIVER_TYPE_SW = 2
Public Const NC_TRANSCEIVER_TYPE_EXT = 3
Public Const NC_TRANSCEIVER_TYPE_DISC = 4
Public Const NC_TRANSCEIVER_TYPE_LIN = 5
Public Const NC_TRANSCEIVER_TYPE_UNKNOWN = &HFF

'// Values for legacy NC_ATTR_HW_TRANSCEIVER attribute
Public Const NC_HW_TRANSCEIVER_HS = 0
Public Const NC_HW_TRANSCEIVER_LS = 1
Public Const NC_HW_TRANSCEIVER_SW = 2
Public Const NC_HW_TRANSCEIVER_EXT = 3
Public Const NC_HW_TRANSCEIVER_DISC = 4

'// Values for NC_ATTR_TRANSCEIVER_MODE attribute.
Public Const NC_TRANSCEIVER_MODE_NORMAL = 0
Public Const NC_TRANSCEIVER_MODE_SLEEP = 1
Public Const NC_TRANSCEIVER_MODE_SW_WAKEUP = 2
Public Const NC_TRANSCEIVER_MODE_SW_HIGHSPEED = 3

'// Values for NC_ATTR_BEHAV_FINAL_OUT attribute (CAN Objs of type NC_CAN_COMM_TX_PERIODIC)
Public Const NC_OUT_BEHAV_REPEAT_FINAL = 0
Public Const NC_OUT_BEHAV_CEASE_TRANSMIT = 1

'// Values for NC_ATTR_SERIES2_FILTER_MODE
Public Const NC_FILTER_SINGLE_STANDARD = 0
Public Const NC_FILTER_SINGLE_EXTENDED = 1
Public Const NC_FILTER_DUAL_STANDARD = 2
Public Const NC_FILTER_DUAL_EXTENDED = 3

'// Values for SourceTerminal of ncConnectTerminals.
Public Const NC_SRC_TERM_10HZ_RESYNC_EVENT = 13
Public Const NC_SRC_TERM_START_TRIG_EVENT = 14
'// Values for DestinationTerminal of ncConnectTerminals.
Public Const NC_DEST_TERM_10HZ_RESYNC = 9
Public Const NC_DEST_TERM_START_TRIG = 10
'/* NCTYPE_VERSION (NC_ATTR_SOFTWARE_VERSION); ncGetHardwareInfo preferable */
Public Const NC_MK_VER_MAJOR = &HFF000000
Public Const NC_MK_VER_MINOR = &HFF0000
Public Const NC_MK_VER_SUBMINOR = &HFF00
Public Const NC_MK_VER_BETA = &HFF
'/* ArbitrationId; use IsRemote or FrameType to determine RTSI frame. */
Public Const NC_FL_CAN_ARBID_INFO = &H40000000
Public Const NC_ARBID_INFO_RTSI_INPUT = &H1
'/* NC_ATTR_STD_MASK and NC_ATTR_XTD_MASK */
Public Const NC_CAN_MASK_STD_MUSTMATCH = &H7FF
Public Const NC_CAN_MASK_XTD_MUSTMATCH = &H1FFFFFFF
Public Const NC_CAN_MASK_STD_DONTCARE = &H0
Public Const NC_CAN_MASK_XTD_DONTCARE = &H0

'/* Values for NC_ATTR_TRANSMIT_MODE(Immediate or timestamped).*/
Public Const NC_TX_MODE_IMMEDIATE = 0
Public Const NC_TX_MODE_TIMESTAMPED = 1

'/* Values for NC_ATTR_TIMESTAMP_FORMAT.*/
Public Const NC_TIME_FORMAT_ABSOLUTE = 0
Public Const NC_TIME_FORMAT_RELATIVE = 1
'/* Values for NC_ATTR_MASTER_TIMEBASE_RATE.Rate can either be
'         10(M Series DAQ) or 20(E series DAQ) Mhz.
'         This attribute is applicable only to PCI/PXI.*/
Public Const NC_TIMEBASE_RATE_10 = 10
Public Const NC_TIMEBASE_RATE_20 = 20

'/***********************************************************************
'                F U N C T I O N   P R O T O T Y P E S
'***********************************************************************/

'/* Naming conventions for sizes:
'   Sizeof?        Indicates size of buffer passed in, and has no relation to
'                  the number of bytes sent/received on the network (C only).
'   ?Length        Indicates number of bytes to send on network.
'   Actual?Length  Indicates number of bytes received from network.
'*/

Public Declare Function ncAction Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal Opcode As Long, _
   ByVal Param As Long) As Long

Public Declare Function ncCloseObject Lib "nican" ( _
   ByVal ObjHandle As Long) As Long

Public Declare Function ncConfig Lib "nican" ( _
   ByVal ObjName As String, _
   ByVal NumAttrs As Long, _
   ByRef AttrIdList As Long, _
   ByRef AttrValueList As Long) As Long

Public Declare Function ncConnectTerminals Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SourceTerminal As Long, _
   ByVal DestinationTerminal As Long, _
   ByVal Modifiers As Long) As Long

Public Declare Function ncDisconnectTerminals Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SourceTerminal As Long, _
   ByVal DestinationTerminal As Long, _
   ByVal Modifiers As Long) As Long

Public Declare Function ncGetAttribute Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal AttrId As Long, _
   ByVal SizeofAttr As Long, _
   ByRef Attr As Any) As Long

Public Declare Function ncGetHardwareInfo Lib "nican" ( _
   ByVal CardIndex As Long, _
   ByVal PortIndex As Long, _
   ByVal AttrId As Long, _
   ByVal AttrSize As Long, _
   ByRef Attr As Any) As Long

Public Declare Function ncOpenObject Lib "nican" ( _
   ByVal ObjName As String, _
   ByRef ObjHandle As Long) As Long

Public Declare Function ncRead Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SizeofData As Long, _
   ByRef Data As Any) As Long

Public Declare Function ncReadMult Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SizeofData As Long, _
   ByRef Data As Any, _
   ByRef ActualDataSize As Long) As Long

Public Declare Function ncReset Lib "nican" ( _
   ByVal IntfName As String, _
   ByVal Param As Long) As Long

Public Declare Function ncSetAttribute Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal AttrId As Long, _
   ByVal SizeofAttr As Long, _
   ByRef Attr As Any) As Long

Public Declare Function ncStatusToString Lib "nican" ( _
   ByVal Status As Long, _
   ByVal SizeofString As Long, _
   ByRef ErrorString As Byte) As Long

Public Declare Function ncWaitForState Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal DesiredState As Long, _
   ByVal Timeout As Long, _
   ByRef CurrentState As Long) As Long

Public Declare Function ncWrite Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SizeofData As Long, _
   ByRef Data As Any) As Long

Public Declare Function ncWriteMult Lib "nican" ( _
   ByVal ObjHandle As Long, _
   ByVal SizeofData As Long, _
   ByRef FrameArray As Any) As Long





'*******************************************************
'** Some additional functions used in the examples.   **
'*******************************************************

Public ErrString As String

Public Type FileTime
   dwLowDateTime As Long
   dwHighDateTime As Long
End Type

Function ncReadMultiple( _
   ByVal ObjHandle As Long, _
   ByVal SizeofData As Long, _
   ByRef Data() As NCTYPE_CAN_STRUCT, _
   ByRef ActualDataSize As Long) As Long

   Dim FramesToRead As Long
   Dim BytesToRead As Long
   Dim BytesRead As Long
   Dim FramesRead As Long
   Dim Buffer() As Byte
   Dim NumFrame As Long
   Dim Status As Long

   FramesToRead = SizeofData / 24
   BytesToRead = FramesToRead * 22
   ReDim Buffer(BytesToRead) As Byte
   Status = ncReadMult(ObjHandle, BytesToRead, Buffer(0), BytesRead)

   If BytesRead <> 0 Then
       FramesRead = (BytesRead / 22) - 1 ' Array starts counting at 0
   'Loop to handle each of the can frames
       For NumFrame = 0 To FramesRead
          Call RtlMoveMemory(Data(FramesRead - NumFrame), Buffer((FramesRead - NumFrame) * 22), 22)
       Next
       ActualDataSize = (FramesRead + 1) * 24
   End If
   ncReadMultiple = Status
End Function

Function ncWriteMultiple( _
   ByVal ObjHandle As Long, _
   ByVal FramesToWrite As Long, _
   ByRef Data() As NCTYPE_CAN_STRUCT _
   ) As Long

   Dim BytesToWrite As Long
   Dim Buffer() As Byte
   Dim NumFrame As Long
   Dim Status As Long

   BytesToWrite = FramesToWrite * 22
   ReDim Buffer(BytesToWrite) As Byte

   For NumFrame = 0 To FramesToWrite - 1
      Call RtlMoveMemory(Buffer((NumFrame) * 22), Data(NumFrame), 22)
   Next
   Status = ncWriteMult(ObjHandle, BytesToWrite, Buffer(0))
   ncWriteMultiple = Status
End Function

'**************************************************************

'This function is used to print the absolute time obtained from ncRead.
Public Function ConvAbsTime(time As NCTYPE_UINT64) As String

Dim stime As SystemTime
Dim localftime As FileTime
Dim localtime As FileTime
Dim res As Long

      ' This Win32 function converts from UTC (international) time
      'to the local time zone.  The card keeps time in UTC
      'format (refer to the description of NCTYPE_ABS_TIME in
      'the section of the NI-CAN reference titled Common Host Data Types).  */
    localtime.dwLowDateTime = time.LowPart
    localtime.dwHighDateTime = time.HighPart
    res = FileTimeToLocalFileTime(localtime, localftime)

      ' This Win32 function converts an absolute time (FILETIME)
      'into SYSTEMTIME, a structure with fields for year, month, day,
      'and so on.  */
   res = FileTimeToSystemTime(localftime, stime)

   ConvAbsTime = stime.wMonth & "/" & stime.wDay & "/" & _
                 stime.wYear & " " & stime.wHour & ":" & _
                 stime.wMinute & ":" & stime.wSecond & ":" & _
                 stime.wMilliseconds
End Function

Function ncStatToStr(ByVal Status As Long, ByRef ErrString As String)

' This function wraps ncStatusToStr function and makes available to the
' user the error string in string format

Dim str(1024) As Byte
Dim i As Integer

        i = 1
        ncStatusToString Status, 1024, str(0)
        ErrString = Chr(str(0))
        While ((i < 1024) And (Chr(str(i)) <> "\0"))
            ErrString = ErrString + Chr(str(i))
            i = i + 1
        Wend
End Function

' This is a utility function used in the examples and refrences frmMain. If you do not
' have frmMain, this function can be deleted

Function CheckStatus(ByVal Status As Long, ByVal FuncName As String) As Boolean

If (Status <> NC_SUCCESS) Then
    ErrCode = Hex(Status)
    ErrSource = FuncName
    ncStatToStr Status, ErrString
    Debug.Print ErrString
End If

If (Status < 0) Then
    CheckStatus = True
Else
    CheckStatus = False
End If
End Function

' This is a utility function used in the examples and refrences frmMain. If you do not
' have frmMain, this function can be deleted

Sub ClearErrors()
    frmmain.ErrCode = vbNullString
    frmmain.ErrSource = vbNullString
    frmmain.ErrString = vbNullString
End Sub

