Attribute VB_Name = "Support"
Public Declare Function timeGetTime Lib "winmm.dll" () As Long
Public Bytes_Received(500, 30) As Byte

Sub delay(delayTime)

' This is a delay routine.  This routine will wait the amount of time
' requested by the variable named "DelayTime".

' Note: For this subroutine to work, the following code must be inserted in
'      the GLOBALS file:
'               Declare Function timeGetTime Lib "winmm.dll" () As Long


' The delay time is expressed in increments of milliseconds


Dim EndTime As Long

If timeGetTime + delayTime > (2 ^ 32) Then
    EndTime = (timeGetTime + delayTime) - (2 ^ 32)
Else
    EndTime = delayTime + timeGetTime
End If

Do Until timeGetTime >= EndTime
    
    DoEvents                'Allows other multitasking events to occurr
Loop

End Sub

Function PrintByte(Data As Byte) As String
    Dim Value As Byte
    Dim ReturnValue As String
    
    ReturnValue = Hex(Data)
    'Check to see if Byte is = 0 or > then 16 (hex F)
    Select Case Data
        Case Is = 0
            ReturnValue = "00"
        Case Is < 16
            ReturnValue = "0" & ReturnValue
    End Select
    PrintByte = ReturnValue

End Function
