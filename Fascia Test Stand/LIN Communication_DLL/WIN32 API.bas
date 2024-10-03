Attribute VB_Name = "API_Declare"
Option Base 0
Option Explicit


Public Type SystemTime
        wYear As Integer
        wMonth As Integer
        wDayOfWeek As Integer
        wDay As Integer
        wHour As Integer
        wMinute As Integer
        wSecond As Integer
        wMilliseconds As Integer
End Type


Public Declare Function TimeToLocalTime Lib "kernel32" (lpTime As FileTime, lpLocalTime As FileTime) As Long
Public Declare Function TimeToSystemTime Lib "kernel32" (lpTime As FileTime, lpSystemTime As SystemTime) As Long
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Public Declare Sub RtlMoveMemory Lib "kernel32" (ByRef hpvDest As Any, ByRef hpvSource As Any, ByVal cbCopy As Long)
Public Declare Function FileTimeToLocalFileTime Lib "kernel32" (lpFileTime As FileTime, _
                                                                lpLocalFileTime As FileTime) As Long
Public Declare Function FileTimeToSystemTime Lib "kernel32" (lpFileTime As FileTime, _
                                                        lpSystemTime As SystemTime) As Long


