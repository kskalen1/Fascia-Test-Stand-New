Imports System
Imports System.IO
Imports Fascia_Test_Stand.TBOX
Module TBOX
    Dim WithEvents spTBOX_6_0 As New IO.Ports.SerialPort

    Public Structure TBOXCommunicationStructure
        Friend TX() As String
        Friend RX() As String
        Friend RX_Text As String
        Friend RX_NEG_1 As String
        Friend RX_NEG_2 As String
        Friend RX_NEG_3 As String
        Friend RX_NEG_4 As String
        Friend RX_NEG_5 As String
        Friend RX_NEG_6 As String
        Friend RX_NEG_1_Text As String
        Friend RX_NEG_2_Text As String
        Friend RX_NEG_3_Text As String
        Friend RX_NEG_4_Text As String
        Friend RX_NEG_5_Text As String
        Friend RX_NEG_6_Text As String
        Friend CommandName As String
    End Structure
    Friend TBOXCommunicationProtocall(50) As TBOXCommunicationStructure

    Public Structure SensorConfiguration
        Friend Sensor1 As String
        Friend Sensor2 As String
        Friend Sensor3 As String
        Friend Sensor4 As String
        Friend Sensor5 As String
        Friend Sensor6 As String
        Friend Sensor_1_Year As String
        Friend Sensor_2_Year As String
        Friend Sensor_3_Year As String
        Friend Sensor_4_Year As String
        Friend Sensor_5_Year As String
        Friend Sensor_6_Year As String
        Friend Sensor_1_Month As String
        Friend Sensor_2_Month As String
        Friend Sensor_3_Month As String
        Friend Sensor_4_Month As String
        Friend Sensor_5_Month As String
        Friend Sensor_6_Month As String
        Friend Sensor_1_Day As String
        Friend Sensor_2_Day As String
        Friend Sensor_3_Day As String
        Friend Sensor_4_Day As String
        Friend Sensor_5_Day As String
        Friend Sensor_6_Day As String
        Friend Sensor_1_Line As String
        Friend Sensor_2_Line As String
        Friend Sensor_3_Line As String
        Friend Sensor_4_Line As String
        Friend Sensor_5_Line As String
        Friend Sensor_6_Line As String
        Friend Sensor_1_ProNum As String
        Friend Sensor_2_ProNum As String
        Friend Sensor_3_ProNum As String
        Friend Sensor_4_ProNum As String
        Friend Sensor_5_ProNum As String
        Friend Sensor_6_ProNum As String
        Friend Sensor_1_ROM As String
        Friend Sensor_2_ROM As String
        Friend Sensor_3_ROM As String
        Friend Sensor_4_ROM As String
        Friend Sensor_5_ROM As String
        Friend Sensor_6_ROM As String
    End Structure
    Friend ConfigurationRequired As SensorConfiguration
    Friend ConfigurationStored As SensorConfiguration

    Public NumberOfBytesReturned As Integer



    Friend Enum TBOXCommunicationCommandIndex
        Read_Firmware_Request = 0
        Read_Bumper_Setup = 1
        SensorMask_Configuration = 2
        Start_Measurement_Request = 3
        Read_Sensor_Production_Data = 4
    End Enum
    Friend Enum MaskConfigurations
        Mask_All_6_0_Sensors
        Mask_All_6_1_Sensors
        Mask_6_0_And_6_5_Sensors
    End Enum
    Friend Enum TBOXLocation
        RAM = 1
        EEPROM = 2
    End Enum

    Friend Enum NumberOfSensors
        Sensors_2 = 2
        Sensors_4 = 4
        Sensors_6 = 6
    End Enum
    Friend Enum TBOXs
        Version_6_0
    End Enum

    Public Structure TBOXEchoResponse
        Friend Sensor_1 As String
        Friend Sensor_2 As String
        Friend Sensor_3 As String
        Friend Sensor_4 As String
        Friend Sensor_5 As String
        Friend Sensor_6 As String
    End Structure
    Friend TBOXEchoResponses As TBOXEchoResponse
    Dim NumberOfRetrys As Integer = 3

    Public Structure TBOXEchoProductionDataResponse
        Friend Sensor_1_Year As String
        Friend Sensor_2_Year As String
        Friend Sensor_3_Year As String
        Friend Sensor_4_Year As String
        Friend Sensor_5_Year As String
        Friend Sensor_6_Year As String
        Friend Sensor_1_Month As String
        Friend Sensor_2_Month As String
        Friend Sensor_3_Month As String
        Friend Sensor_4_Month As String
        Friend Sensor_5_Month As String
        Friend Sensor_6_Month As String
        Friend Sensor_1_Day As String
        Friend Sensor_2_Day As String
        Friend Sensor_3_Day As String
        Friend Sensor_4_Day As String
        Friend Sensor_5_Day As String
        Friend Sensor_6_Day As String
        Friend Sensor_1_Line As String
        Friend Sensor_2_Line As String
        Friend Sensor_3_Line As String
        Friend Sensor_4_Line As String
        Friend Sensor_5_Line As String
        Friend Sensor_6_Line As String
        Friend Sensor_1_ProdNum As String
        Friend Sensor_2_ProdNum As String
        Friend Sensor_3_ProdNum As String
        Friend Sensor_4_ProdNum As String
        Friend Sensor_5_ProdNum As String
        Friend Sensor_6_ProdNum As String
        Friend Sensor_1_ROM As String
        Friend Sensor_2_ROM As String
        Friend Sensor_3_ROM As String
        Friend Sensor_4_ROM As String
        Friend Sensor_5_ROM As String
        Friend Sensor_6_ROM As String
    End Structure
    Friend TBOXEchoProductionDataResponses As TBOXEchoProductionDataResponse

    Sub LoadTBOXCommunicationProtocall()
        'Searches For All Avalable Communication Commands And & Loads All Communication Protocalls
        Dim SectionNames(50) As String
        Dim TBOXcommands(50) As String

        Dim FileName As String = ""
        Dim DefaultValue As String = ""
        Dim NumberOfLoops As Integer = 0
        Dim Temporay_String As String = ""
        Dim Counter As Integer = 0
        Dim dummy As Long = 0
        Dim Temporary_Holder As String = ""
        Dim NumberOfBytes As Integer = 0
        Const strDelimiter = ","

        'Check To Make Sure File Is Avalable To Open

        FileName = DefaultIniPath & DefaultTBOXCommunicationINIFileName     'Set name Of File To Search For Info.
        If File.Exists(FileName) = False Then
            MsgBox("Default.ini file not found" & FileName, MsgBoxStyle.Critical, "Load TBOX Communication Commands .ini ERROR")
            End
        End If

        DefaultValue = ""                                                   'Set Default Value
        Temporay_String = Space$(10000)                                     'Make Room For All Section Names
        dummy = GetPrivateProfileSectionNames(Temporay_String, Len(Temporay_String), FileName) 'Find All Section Names In .ini file

        Counter = 0                                                         'Reset Counter
        Do                                                                  'Do
            Application.DoEvents()
            TBOXcommands(Counter) = SplitString(Temporay_String, Chr(0))    'Split out string into indvidual section Names
            If TBOXcommands(Counter) = "" Then
                Exit Do
            End If
            TBOXCommunicationProtocall(Counter).CommandName = TBOXcommands(Counter)

            Counter = Counter + 1                                           'Increment counter
        Loop
        ReDim Preserve TBOXcommands(Counter - 1)                            'Redmiminsion Array To number of Commands
        ReDim Preserve TBOXCommunicationProtocall(Counter - 1)

        'Loads All Of The Communication Protocall Parameters From The .ini File
        For NumberOfLoops = 0 To UBound(TBOXcommands)
            'Load TX Bytes
            ReDim TBOXCommunicationProtocall(NumberOfLoops).TX(50)
            Section = TBOXcommands(NumberOfLoops)
            If Section = "SensorMask_Configuration" Then
                Key = TBOXBumperConfiguration
                Debug.Print(TBOXBumperConfiguration)
            Else
                Key = "TX"                  'Key String To Look For
            End If
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                TBOXCommunicationProtocall(NumberOfLoops).TX(NumberOfBytes) = SplitString(Temporary_Holder, strDelimiter)
                If TBOXCommunicationProtocall(NumberOfLoops).TX(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop
            ReDim Preserve TBOXCommunicationProtocall(NumberOfLoops).TX(NumberOfBytes - 1)

            'Load RX Bytes
            ReDim TBOXCommunicationProtocall(NumberOfLoops).RX(50)
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX"                  'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                TBOXCommunicationProtocall(NumberOfLoops).RX(NumberOfBytes) = SplitString(Temporary_Holder, strDelimiter)
                If TBOXCommunicationProtocall(NumberOfLoops).RX(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop
            ReDim Preserve TBOXCommunicationProtocall(NumberOfLoops).RX(NumberOfBytes - 1)

            'Load RX_Neg_1 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_1"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_1_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_1 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_1 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

            'Load RX_Neg_2 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_2"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_2_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_2 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_2 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

            'Load RX_Neg_3 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_3"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_3_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_3 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_3 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                If TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_3(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

            'Load RX_Neg_4 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_4"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_4_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_4 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_4 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                If TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_4(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

            'Load RX_Neg_5 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_5"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_5_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_5 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_5 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                If TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_5(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

            'Load RX_Neg_6 Negitive Response Bytes
            Section = TBOXcommands(NumberOfLoops)
            Key = "RX_Neg_6"            'Key String To Look For
            DefaultValue = ""           'Set DefaultValue Value in case variable key is not found
            Temporary_Holder = ReadINIFile(Section, Key, DefaultValue, FileName)
            NumberOfBytes = 0                                                         'Reset Counter
            Do                                                                  'Do
                If NumberOfBytes = 0 Then
                    TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_6_Text = SplitString(Temporary_Holder, strDelimiter)
                End If
                TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_6 = TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_6 & " " & SplitString(Temporary_Holder, strDelimiter)
                If Temporary_Holder = Nothing Then Exit Do
                If TBOXCommunicationProtocall(NumberOfLoops).RX_NEG_6(NumberOfBytes) = Nothing Then Exit Do
                NumberOfBytes = NumberOfBytes + 1
            Loop

        Next NumberOfLoops

    End Sub

    Sub TBOX_6_0_SerialPortOpen()
        'Sets The Communication Port Parameters & Opens the Serial Port To The TBOX Version 5.0

        Select Case spTBOX_6_0.IsOpen
            Case Is = True
                'If Port Is Open Don't Re-open
                spTBOX_6_0.Close()
                'Re-Open 
                Try
                    With spTBOX_6_0
                        .PortName = TBOX_Ver_6_0_comPort
                        .BaudRate = 9600
                        .Parity = IO.Ports.Parity.None
                        .DataBits = 8
                        .StopBits = IO.Ports.StopBits.One
                        .Handshake = Ports.Handshake.None
                        .ReadTimeout = 2000
                    End With
                    spTBOX_6_0.Open()
                    frmMain.lblTBOX_6_0_comPort.Text = TBOX_Ver_6_0_comPort
                    frmMain.indTBOX_6_0_ComPortOpen.BackColor = Color.Green
                Catch ex As Exception
                    MsgBox("Attempt Open Com Port For TBOX6 Version 1.1 Failed, In Tbox 6.0 SerialPortOpen Routine: See Error Log For Details", MsgBoxStyle.Critical, "Bosch Box Error")
                    WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, False, "Attempt Open Com Port For TBOX6 Version 1.1 Failed, In Tbox 6.0 SerialPortOpen Routine: An Exception Was Thrown See Error Log For Details")
                End Try

            Case Is = False
                'If Port Is Closed Open It.
                Try
                    With spTBOX_6_0
                        .PortName = TBOX_Ver_6_0_comPort
                        .BaudRate = 9600
                        .Parity = IO.Ports.Parity.None
                        .DataBits = 8
                        .StopBits = IO.Ports.StopBits.One
                        .Handshake = Ports.Handshake.None
                        .DiscardNull = False
                        .ReadTimeout = 2000
                    End With
                    spTBOX_6_0.Open()
                    frmMain.lblTBOX_6_0_comPort.Text = TBOX_Ver_6_0_comPort
                    frmMain.indTBOX_6_0_ComPortOpen.BackColor = Color.Green
                Catch ex As Exception
                    MsgBox("Attempt Open Com Port For TBOX6 Version 1.1 Failed, In Tbox 6.0 SerialPortOpen Routine: See Error Log For Details", MsgBoxStyle.Critical, "Bosch Box Error")
                    WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, False, "Attempt Open Com Port For TBOX6 Version 1.1 Failed, In Tbox 6.0 SerialPortOpen Routine: An Exception Was Thrown See Error Log For Details")

                End Try
        End Select
    End Sub

    Public Function TBOXCommunication(ByVal TBOXCommand As Object, ByVal BytesReturned() As Byte, ByVal MeasurmentCommand As Boolean) As Boolean  'Communicates With TBOX Via COM Port
        Dim MessagePacket(150) As Byte
        Dim NumberOfBytesToWrite As Integer
        Dim ReturnString As String = ""
        Dim Loops As Integer
        Dim ChecksumString As String = ""
        Dim TXMessage As String = ""

        For Loops = 0 To UBound(TBOXCommand)
            MessagePacket(Loops) = Val("&H" & TBOXCommand(Loops))
            TXMessage = TXMessage & "0x" & Hex(MessagePacket(Loops)) & " "
        Next Loops
        ChecksumString = CheckSum(MessagePacket)
        MessagePacket(Loops) = Val(ChecksumString)
        TXMessage = TXMessage & "0x" & Hex(MessagePacket(Loops))
        NumberOfBytesToWrite = (Loops + 1)
        'ReDim Preserve MessagePacket(Loops - 1)
        ReDim Preserve MessagePacket(Loops)
        Debug.Print(TXMessage)

        Try
            'Clear Out Buffer before write & read
            'spTBOX_6_0.DiscardInBuffer()
            'Write Commands To The TBOX Via Com Port
            spTBOX_6_0.Write(MessagePacket, 0, NumberOfBytesToWrite)  'Send Out Command

            'Allow time for TBOX to Respond
            Select Case MeasurmentCommand
                Case True
                    delay(1250)
                Case False
                    delay(500)
            End Select

            Dim i As Integer = 0
            Dim ArraySize As Short = spTBOX_6_0.BytesToRead - 1
            For i = 0 To (spTBOX_6_0.BytesToRead - 1)
                BytesReturned(i) = spTBOX_6_0.ReadByte
                ReturnString = ReturnString & "0x" & Hex$(BytesReturned(i)) & " "
            Next i
            ReDim Preserve BytesReturned(ArraySize)

            Debug.Print(ReturnString)
            NumberOfBytesReturned = (i - 1)
            ReDim Preserve BytesReturned(i - 1)
            TBOXCommunication = True

        Catch ex As Exception
            TBOXCommunication = False
            Exit Function
        End Try

    End Function

    Friend Function StartMeasurementRequest(ByRef ReturnedBytes() As Byte) As Boolean

        'This Function Starts Communication with the Bosch TBOX and returns the Result
        Dim CommandIndex As TBOXCommunicationCommandIndex = TBOXCommunicationCommandIndex.Start_Measurement_Request
        Dim Result As Boolean
        Dim BoschBoxToUse As Integer = 0

        Try
            Result = TBOXCommunication(TBOXCommunicationProtocall(CommandIndex).TX, ReturnedBytes, True)
            Select Case Result
                Case True
                    StartMeasurementRequest = True
                Case Else
                    StartMeasurementRequest = False
            End Select
        Catch ex As Exception
            WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Starting The Measurement Request (Bosch TBOX COMMAND) : An Exception Was Thrown See Error Log For Details")
            StartMeasurementRequest = False
        End Try

    End Function

    Function ReadFirmwareVersion(ByRef ReturnedBytes() As Byte) As Boolean
        'This Function Starts Communication with the Bosch TBOX and returns the Result
        Dim CommandIndex As TBOXCommunicationCommandIndex = TBOXCommunicationCommandIndex.Read_Firmware_Request
        Dim Result As Byte


        Result = TBOXCommunication(TBOXCommunicationProtocall(CommandIndex).TX, ReturnedBytes, False)

        Select Case Result
            Case True
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Green
                frmMain.lblTbox_6_0_RXStatus.Text = TBOXCommunicationProtocall(CommandIndex).RX_Text
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadFirmwareVersion = True
            Case Else
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Red
                frmMain.lblTbox_6_0_RXStatus.Text = "Undefined Error"
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadFirmwareVersion = False
        End Select

    End Function

    Function ReadBumperSetup(ByRef ReturnedBytes() As Byte) As Boolean
        'This Function Starts Communication with the Bosch TBOX and returns the Result
        Dim CommandIndex As TBOXCommunicationCommandIndex = TBOXCommunicationCommandIndex.Read_Bumper_Setup
        Dim ReturnMessage(17) As Byte
        Dim Result As Boolean
        Result = TBOXCommunication(TBOXCommunicationProtocall(CommandIndex).TX, ReturnMessage, False)
        ReturnedBytes = ReturnMessage
        Select Case Result
            Case True
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Green
                frmMain.lblTbox_6_0_RXStatus.Text = TBOXCommunicationProtocall(CommandIndex).RX_Text
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadBumperSetup = True

            Case Else
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Red
                frmMain.lblTbox_6_0_RXStatus.Text = "Undefined Error"
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadBumperSetup = False
        End Select

    End Function

    Function WriteBumperSetup(ByRef ReturnString As String)
        'This Writes The Sensor Configuration To The Box
        'The Configuration Is Stored In The .ini file and is explained there.
        Dim Result As Boolean
        Dim CommandIndex As TBOXCommunicationCommandIndex = TBOXCommunicationCommandIndex.SensorMask_Configuration
        Dim ReturnMessage(7) As Byte
        Dim BoschBoxToUse As Integer = 0

        Result = TBOXCommunication(TBOXCommunicationProtocall(CommandIndex).TX, ReturnMessage, False)

        If ReturnMessage(0).ToString = Val("&H" & TBOXCommunicationProtocall(CommandIndex).RX(0)).ToString AndAlso ReturnMessage(1).ToString = Val("&H" & TBOXCommunicationProtocall(CommandIndex).RX(1)).ToString _
          AndAlso ReturnMessage(2).ToString = Val("&H" & TBOXCommunicationProtocall(CommandIndex).RX(2)).ToString AndAlso ReturnMessage(3).ToString = Val("&H" & TBOXCommunicationProtocall(CommandIndex).RX(3)).ToString Then
            WriteBumperSetup = True
        Else
            frmMain.lblTbox_6_0_RXStatus.Text = "Could Not Write Bumper Setup: " & Result
            WriteBumperSetup = False
        End If
    End Function

    Function ReadSensorProductionData(ByRef ReturnedBytes() As Byte) As Boolean
        'This Function Starts Communication with the Bosch TBOX and returns the Result
        Dim CommandIndex As TBOXCommunicationCommandIndex = TBOXCommunicationCommandIndex.Read_Sensor_Production_Data
        Dim ReturnMessage(48) As Byte
        Dim Result As Boolean
        Dim BoschBoxToUse As Integer = 0
        Result = TBOXCommunication(TBOXCommunicationProtocall(CommandIndex).TX, ReturnMessage, False)
        ReturnedBytes = ReturnMessage
        Select Case Result
            Case True
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Green
                frmMain.lblTbox_6_0_RXStatus.Text = TBOXCommunicationProtocall(CommandIndex).RX_Text
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadSensorProductionData = True

            Case Else
                frmMain.imgTBOX_6_0_CommunicationStarted.BackColor = Color.Red
                frmMain.lblTbox_6_0_RXStatus.Text = "Undefined Error"
                frmMain.lblTBOX_6_0_ResponseBytes.Text = Result
                ReadSensorProductionData = False
        End Select
    End Function
    Function DirectEchoesRequest(ByRef ReturnString As String) As Boolean
        'This Function Reads The Number Of Sensors Currently Configured in the Bosch TBOX and returns the Result
        'Results are returned in TBOXEchoResponses.Sensor_X

        Dim Result As String = ""
        TBOXEchoResponses = Nothing
        Dim RetryNeeded As Boolean = False
        Dim RetryCount As Short = 0
        Dim ReturnedBytes(18) As Byte

        For Retrys As Short = 1 To 3
            'Take The Measurement
            Result = StartMeasurementRequest(ReturnedBytes)

            Select Case Result
                Case True
                    'Sensor 1 Values
                    Select Case ReturnedBytes(12)
                        Case 0
                            TBOXEchoResponses.Sensor_1 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_1 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_1 = CInt(ReturnedBytes(12)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_1 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_1 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_1 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_1 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_1 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_1 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_1 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_1 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_1 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_1 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_1 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_1 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_1 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_1 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_1.Text = TBOXEchoResponses.Sensor_1

                    'Sensor 2 Values
                    Select Case ReturnedBytes(13)
                        Case 0
                            TBOXEchoResponses.Sensor_2 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_2 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_2 = CInt(ReturnedBytes(13)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_2 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_2 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_2 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_2 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_2 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_2 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_2 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_2 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_2 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_2 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_2 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_2 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_2 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_2 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_2.Text = TBOXEchoResponses.Sensor_2

                    'Sensor 3 Values
                    Select Case ReturnedBytes(14)
                        Case 0
                            TBOXEchoResponses.Sensor_3 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_3 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_3 = CInt(ReturnedBytes(14)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_3 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_3 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_3 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_3 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_3 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_3 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_3 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_3 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_3 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_3 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_3 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_3 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_3 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_3 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_3.Text = TBOXEchoResponses.Sensor_3

                    'Sensor 4 Values
                    Select Case ReturnedBytes(15)
                        Case 0
                            TBOXEchoResponses.Sensor_4 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_4 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_4 = CInt(ReturnedBytes(15)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_4 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_4 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_4 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_4 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_4 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_4 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_4 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_4 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_4 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_4 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_4 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_4 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_4 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_4 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_4.Text = TBOXEchoResponses.Sensor_4

                    'Sensor 5 Values
                    Select Case ReturnedBytes(16)
                        Case 0
                            TBOXEchoResponses.Sensor_5 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_5 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_5 = CInt(ReturnedBytes(16)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_5 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_5 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_5 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_5 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_5 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_5 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_5 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_5 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_5 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_5 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_5 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_5 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_5 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_5 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_5.Text = TBOXEchoResponses.Sensor_5

                    'Sensor 6 Values
                    Select Case ReturnedBytes(17)
                        Case 0
                            TBOXEchoResponses.Sensor_6 = "ERROR:No Sensor Connected"

                        Case 1 To 29
                            TBOXEchoResponses.Sensor_6 = "ERROR:Unexpected value cannot define error"

                        Case 30 To 200 'Normal Range Of Distance Values Expected
                            TBOXEchoResponses.Sensor_6 = CInt(ReturnedBytes(17)).ToString

                        Case 201 To 242
                            TBOXEchoResponses.Sensor_6 = "ERROR:Unexpected value cannot define error"

                        Case Is = 243
                            TBOXEchoResponses.Sensor_6 = "ERROR:Distance values outside the distance definition"

                        Case Is = 244
                            TBOXEchoResponses.Sensor_6 = "ERROR:Sensor data transmission error"

                        Case Is = 245
                            TBOXEchoResponses.Sensor_6 = "ERROR:Sensor Hardware error"

                        Case Is = 246
                            TBOXEchoResponses.Sensor_6 = "ERROR:Sensor error: Ringing to short"

                        Case Is = 247
                            TBOXEchoResponses.Sensor_6 = "ERROR:Sensor error: Ringing to long"

                        Case Is = 248
                            TBOXEchoResponses.Sensor_6 = "ERROR:Sensor impedance error"

                        Case Is = 249
                            TBOXEchoResponses.Sensor_6 = "ERROR:Wrong sensortype"

                        Case Is = 250
                            TBOXEchoResponses.Sensor_6 = "ERROR:Cable, sensor harness poor connection"

                        Case Is = 251
                            TBOXEchoResponses.Sensor_6 = "ERROR:Broken harness wire data Sensor"

                        Case Is = 252
                            TBOXEchoResponses.Sensor_6 = "ERROR:Broken harness wire U+ or GND"

                        Case Is = 253
                            TBOXEchoResponses.Sensor_6 = "ERROR:Short circuit: data wire with GND"

                        Case Is = 254
                            TBOXEchoResponses.Sensor_6 = "ERROR:Short circuit: data wire with U+"

                        Case Is = 255
                            TBOXEchoResponses.Sensor_6 = "ERROR:Short circuit: U+ with GND"

                    End Select
                    frmMain.lblTBOX_6_0_SensorEchoes_6.Text = TBOXEchoResponses.Sensor_6


                    '//////////////////////////////////////////////////////////////////////////////////////////
                Case Else
                    RetryNeeded = True
            End Select

            If RetryNeeded = False Then
                'Still Check To See If There Is An Error
                If TBOXEchoResponses.Sensor_1.Contains("ERROR:") Or TBOXEchoResponses.Sensor_2.Contains("ERROR:") Or TBOXEchoResponses.Sensor_3.Contains("ERROR:") Or TBOXEchoResponses.Sensor_4.Contains("ERROR:") Or TBOXEchoResponses.Sensor_5.Contains("ERROR:") Or TBOXEchoResponses.Sensor_6.Contains("ERROR:") Then
                    RetryNeeded = True
                End If
            End If

            If RetryNeeded = False Then
                Exit For
            End If
            RetryCount = RetryCount + 1
            delay(500)
        Next
        'If Retiries Didnt Work Then Fail
        DirectEchoesRequest = True


    End Function


    Function EchoeProductionData(ByRef ReturnString As String) As Boolean
        'This Function Reads The Number Of Sensors Currently Configured in the Bosch TBOX and returns the Result
        'Results are returned in TBOXEchoResponses.Sensor_X

        Dim RetryNeeded As Boolean = False
        Dim RetryCount As Short = 0
        Dim Result As Boolean
        TBOXEchoProductionDataResponses = Nothing
        Dim ReturnedBytes As Byte() = Nothing


        For Retrys As Short = 1 To 3
            'Take The Measurement
            Result = ReadSensorProductionData(ReturnedBytes)
            Select Case Result
                Case True
                    'Sensor 1 Year
                    Select Case ReturnedBytes(5)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_Year = "ERROR:No Production Year Data or Sensor"

                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_Year = CInt(ReturnedBytes(5)).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_Year.Text = TBOXEchoProductionDataResponses.Sensor_1_Year
                    Select Case ReturnedBytes(6)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_Month = "ERROR:No Production Month Data or Sensor"
                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_Month = CInt(ReturnedBytes(6)).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_Month.Text = TBOXEchoProductionDataResponses.Sensor_1_Month

                    Select Case ReturnedBytes(7)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_Day = "ERROR:No Production Day Data or Sensor"

                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_Day = CInt(ReturnedBytes(7)).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_Day.Text = TBOXEchoProductionDataResponses.Sensor_1_Day
                    Select Case ReturnedBytes(8)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_Line = "ERROR:No Production Line Data or Sensor"
                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_Line = CInt(ReturnedBytes(8)).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_Line.Text = TBOXEchoProductionDataResponses.Sensor_1_Line
                    Select Case ReturnedBytes(11)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_ROM = "ERROR:No Production ROM Data or Sensor"
                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_ROM = CInt(ReturnedBytes(11)).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_ROM.Text = TBOXEchoProductionDataResponses.Sensor_1_ROM
                    Select Case ReturnedBytes(9)
                        Case 0
                            TBOXEchoProductionDataResponses.Sensor_1_ProdNum = "ERROR:No Production Production # Data or Sensor"
                        Case 1 To 255
                            TBOXEchoProductionDataResponses.Sensor_1_ProdNum = ReturnedBytes(9) & ReturnedBytes(10).ToString
                    End Select
                    frmMain.lblTBOX_6_0_SensorDataEchoes_1_ProdNum.Text = TBOXEchoProductionDataResponses.Sensor_1_ProdNum


                    '//////////////////////////////////////////////////////////////////////////////////////////
                Case Else
                    RetryNeeded = True
            End Select

            If RetryNeeded = False Then
                'Still Check To See If There Is An Error
                If TBOXEchoProductionDataResponses.Sensor_1_Year.Contains("ERROR:") Or TBOXEchoProductionDataResponses.Sensor_1_Month.Contains("ERROR:") Or TBOXEchoProductionDataResponses.Sensor_1_Day.Contains("ERROR:") Or TBOXEchoProductionDataResponses.Sensor_1_Line.Contains("ERROR:") Or TBOXEchoProductionDataResponses.Sensor_1_ROM.Contains("ERROR:") Or TBOXEchoProductionDataResponses.Sensor_1_ProdNum.Contains("ERROR:") Then
                    RetryNeeded = True
                End If
            End If

            If RetryNeeded = False Then
                Exit For
            End If
            RetryCount = RetryCount + 1
            delay(500)
        Next

        'If Retiries Didnt Work Then Fail
        EchoeProductionData = True



    End Function
    Friend Function CheckSum(MessagePackets As Object) As String
        Dim sum As Integer = 0
        'Dim bytes() As Byte = {&H82, &H68, &HF3, &H21, &H60}
        Dim bytes() As Byte = MessagePackets
        For i As Integer = 0 To bytes.Length - 1
            sum = (CInt(sum) + bytes(i)) Mod 256
        Next
        If sum = 0 Then
            Return 0
        Else
            Return CByte(sum)
        End If

    End Function

End Module