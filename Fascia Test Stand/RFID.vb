Module RFID
   Sub GetRFIDBitsFromDatabase()
      'Reads The RFID Bit Information From The Database
      Dim DBConString As String = My.Settings.RFIDBitsConnectionString
      Dim DBConnection As OleDb.OleDbConnection = Nothing
      Dim DBCmdString As String = "SELECT * FROM " & "BITS"
      Dim DBCmd As OleDb.OleDbCommand = Nothing
      Dim DBRead As OleDb.OleDbDataReader = Nothing
      Dim index As Short = 1


      Try
         DBConnection = New OleDb.OleDbConnection(DBConString)
         DBConnection.Open()
         DBCmd = New OleDb.OleDbCommand(DBCmdString, DBConnection)
         DBRead = DBCmd.ExecuteReader()
         If DBRead.HasRows Then
            Do While DBRead.Read()
               'Loads The Tester Specific Offsets
               RFID_BIT_DATABASE(index).BIT_Number = DBRead("BIT")
               RFID_BIT_DATABASE(index).BIT_Description = DBRead("BIT_Description").ToString
               RFID_BIT_DATABASE(index).BIT_Active = DBRead("BIT_Active")
               RFID_BIT_DATABASE(index).BIT_Test_ID = DBRead("BIT_Test_ID")
               RFID_BIT_DATABASE(index).BIT_Fascia_Tester = DBRead("BIT_Fascia_Tester")
               RFID_BIT_DATABASE(index).BIT_Comment = DBRead("BIT_Comment")
               Select Case RFID_BIT_DATABASE(index).BIT_Fascia_Tester
                  Case True
                     RFID_InitString = RFID_InitString & "1"
                  Case False
                     RFID_InitString = RFID_InitString & "0"
               End Select
               index = index + 1
            Loop
         End If

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Reading The RFID Database : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
      Finally
         If DBRead IsNot Nothing Then DBCmd.Dispose()
         If DBRead IsNot Nothing Then DBRead.Close()
         DBConnection.Close()
      End Try
   End Sub

   Sub DecodeRFIDBits(ByVal RFID_BITS As String)
      'Decodes the RFID Bits passed into this routine
      Dim returnValue As Char() = ""
      Dim FasciaType As String = ""
      Dim SectionName As String = ""

      Try   'Try To Decode The RFID_BITS
         ' PUT BACK FOR NORMAL OPERATION!!!

         If RFID_BITS.Length <> 192 Then
            'There Must Be 192 Bits In Order To Decode Correctly If Not Error Out
            AllTestsPassed = False
            AbortTesting = True
            frmMain.lblTestResult.BackColor = Color.Crimson
            Throw New Exception("The RFID BIT STRING WAS NOT 192 BYTES IN LENGTH")
         End If

         'Select Default State To No Sensors Selected
         frmMain.rbNoRadarSensors.PerformClick()
         frmMain.rbNoneSelected.PerformClick()

         Dim NotMaybach As Boolean = False

         Application.DoEvents()

         returnValue = RFID_BITS.ToCharArray()

         For StringArrayIndex As Short = 1 To 192
            If returnValue(StringArrayIndex - 1) = "1" Then
               Select Case RFID_BIT_DATABASE(StringArrayIndex).BIT_Fascia_Tester
                  Case True
                     'If This BIT IS Used On The Fascia Tester Then Figure Out What The Bit Is And Then Decode The Proper Tests
                     Select Case RFID_BIT_DATABASE(StringArrayIndex).BIT_Test_ID.ToUpper
                        Case "FRONT"
                           frmMain.rbFrontSelected.Checked = True  'Set Button So That Front Tests Will Be Tested
                           frmMain.SetBumperScreen("Front")
                           Application.DoEvents()

                           'Set Bumper Location To Test
                           BumperLocationToTest = "Front"

                        Case "REAR"
                           frmMain.rbRearSelected.Checked = True  'Set Button So That Rear Tests Will Be Tested
                           frmMain.SetBumperScreen("Rear")
                           Application.DoEvents()

                           'Set Bumper Location To Test
                           BumperLocationToTest = "Rear"

                        Case "BASIC"
                           frmMain.rbBasic.Checked = True
                           'frmMain.lstTestList.Items.Add("BASIC")
                           Application.DoEvents()


                        Case "AMG"
                           frmMain.rbAMG.Checked = True
                           ' frmMain.lstTestList.Items.Add("AMG")
                           Application.DoEvents()


                        Case "MODEL ID"   'Decode And Display The Proper ModelID Selected
                           If frmMain.rbPlatform_1.Text = "Platform_1" Then
                              frmMain.rbPlatform_1.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description
                              frmMain.rbPlatform_1.Visible = True
                              frmMain.rbPlatform_1.Checked = False
                              frmMain.rbPlatform_1.Checked = True
                              Application.DoEvents()

                              ' frmMain.lstTestList.Items.Add(frmMain.rbPlatform_1.Text)
                              Exit Select
                           ElseIf frmMain.rbPlatform_1.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description Then
                              frmMain.rbPlatform_1.Checked = True
                              Application.DoEvents()
                              'frmMain.lstTestList.Items.Add(frmMain.rbPlatform_1.Text)
                              Exit Select
                           End If

                           If frmMain.rbPlatform_2.Text = "Platform_2" Then
                              frmMain.rbPlatform_2.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description
                              frmMain.rbPlatform_2.Visible = True
                              frmMain.rbPlatform_2.Checked = True
                              '      frmMain.lstTestList.Items.Add(frmMain.rbPlatform_2.Text)
                              Exit Select
                           ElseIf frmMain.rbPlatform_2.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description Then
                              frmMain.rbPlatform_2.Checked = True
                              '     frmMain.lstTestList.Items.Add(frmMain.rbPlatform_2.Text)
                              Exit Select
                           End If

                           If frmMain.rbPlatform_3.Text = "Platform_3" Then
                              frmMain.rbPlatform_3.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description
                              frmMain.rbPlatform_3.Visible = True
                              frmMain.rbPlatform_3.Checked = True
                              '    frmMain.lstTestList.Items.Add(frmMain.rbPlatform_3.Text)
                              Exit Select
                           ElseIf frmMain.rbPlatform_3.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description Then
                              frmMain.rbPlatform_3.Checked = True
                              '      frmMain.lstTestList.Items.Add(frmMain.rbPlatform_3.Text)
                              Exit Select
                           End If

                           If frmMain.rbPlatform_4.Text = "Platform_4" Then
                              frmMain.rbPlatform_4.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description
                              frmMain.rbPlatform_4.Visible = True
                              frmMain.rbPlatform_4.Checked = True
                              '      frmMain.lstTestList.Items.Add(frmMain.rbPlatform_4.Text)
                              Exit Select
                           ElseIf frmMain.rbPlatform_4.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description Then
                              frmMain.rbPlatform_4.Visible = True
                              frmMain.rbPlatform_4.Checked = True
                              '       frmMain.lstTestList.Items.Add(frmMain.rbPlatform_4.Text)
                              Exit Select
                           End If

                           If frmMain.rbPlatform_5.Text = "Platform_5" Then
                              frmMain.rbPlatform_5.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description
                              frmMain.rbPlatform_5.Visible = True
                              frmMain.rbPlatform_5.Checked = True
                              '         frmMain.lstTestList.Items.Add(frmMain.rbPlatform_5.Text)
                              Exit Select
                           ElseIf frmMain.rbPlatform_5.Text = RFID_BIT_DATABASE(StringArrayIndex).BIT_Description Then
                              frmMain.rbPlatform_5.Visible = True
                              frmMain.rbPlatform_5.Checked = True
                              '        frmMain.lstTestList.Items.Add(frmMain.rbPlatform_5.Text)
                              Exit Select
                           End If

                        Case "PTSGEN1"
                           frmMain.cb_PTS_TestSelected.Checked = True
                           frmMain.rbPTS_GEN_1.Checked = True
                           frmMain.lstTestList.Items.Add("PTS GEN 1")


                        Case "PTSGEN2"
                           frmMain.cb_PTS_TestSelected.Checked = True
                           frmMain.rbPTS_GEN_2.Checked = True
                           frmMain.lstTestList.Items.Add("PTS GEN 2")

                        Case "HFA"
                           'HFA Tests Selected
                           frmMain.cb_HFA_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("HFA Brose GEN 1")

                        Case "HFAGEN1"
                           'HFA Tests Selected
                           frmMain.cb_HFA_TestSelected.Checked = True


                        Case "HFAGEN2"
                           'HFA Tests Selected
                           frmMain.cb_HFA_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("HFA Brose GEN 2")


                        Case "HFAGEN3"
                           frmMain.cb_HFA_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("HFA Brose GEN 3")

                        Case "CAMERA"
                           'HFA Tests Selected
                           frmMain.cb_CAMERA_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("Camera")

                        Case "ART"
                           'HFA Tests Selected
                           frmMain.cb_ART_PLATE_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("Art Plate")

                        Case "TEMP"
                           'HFA Tests Selected
                           frmMain.cb_TEMP_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("Tempreature Sensor")

                        Case "LED"
                           frmMain.cb_LEDLamp_TestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("LED")


#Region "RADAR"

                        Case "FCWGEN1"
                           'FCW GEN1 Test Limits Selected
                           frmMain.rbRadar_Sensor_GEN_1.Checked = True
                           frmMain.rbFCWTestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("FCW GEN 1")

                        Case "FCWGEN2"
                           'FCW GEN2 Test Limits Selected
                           frmMain.rbRadar_Sensor_GEN_2.Checked = True
                           frmMain.rbFCWTestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("FCW GEN 2")

                        Case "FCWGEN3"
                           'FCW GEN3 Test Limits Selected
                           frmMain.rbRadar_Sensor_GEN_3.Checked = True
                           frmMain.rbFCWTestSelected.Checked = True
                           frmMain.lstTestList.Items.Add("FCW GEN 3")

                        Case "DTRGEN1" 'DP (DTR) GEN1 Test Limits Selected
                           frmMain.rbDTRTestSelected.Checked = True
                           frmMain.rbRadar_Sensor_GEN_1.Checked = True
                           frmMain.lstTestList.Items.Add("DP (DTR) GEN 1")

                        Case "DTRGEN2" 'DP (DTR) GEN2 Test Limits Selected
                           frmMain.rbDTRTestSelected.Checked = True
                           frmMain.rbRadar_Sensor_GEN_2.Checked = True
                           frmMain.lstTestList.Items.Add("DP (DTR) GEN 2")

                        Case "DTRGEN3" 'DP (DTR) GEN3 Test Limits Selected
                           frmMain.rbDTRTestSelected.Checked = True
                           frmMain.rbRadar_Sensor_GEN_3.Checked = True
                           frmMain.lstTestList.Items.Add("DP (DTR) GEN 3")
#End Region
                        Case Else
                           'Throw An Error There Were No Cases Found For This Enabled BIT (as Defined in the RFID_BIT Database)
                           Throw New Exception("RFID BIT Number: " & StringArrayIndex.ToString & " Has A Test ID (In The RFID_BIT Database that is not valid " & RFID_BIT_DATABASE(StringArrayIndex).BIT_Test_ID.ToUpper & " (no tests are avalable for this value)")

                     End Select

                     'Added More Cases Here

                  Case Else
                     'If Bit IS Not Active For The Fascia Tester Then Ignore It

               End Select

            End If 'End If Then For returnvalue = 1

         Next
         If frmMain.rbPlatform_1.Checked = True Then
            frmMain.lstTestList.Items.Add("Heating Element")
         End If
         Application.DoEvents()

      Catch ex As Exception
         WritetoErrorLog(ex, True, True, "Error Occured In: " & New StackTrace().GetFrame(0).GetMethod.ToString, True, "There Was An Error Decoding The RFID string : An Exception Was Thrown See Error Log For Details")
         AbortTesting = True
         Exit Sub
      End Try

   End Sub

   ''' <summary>
   ''' Container for the internal data of Order Confirmation
   ''' </summary>
   Friend Class OrderConfirm
      Private _iMID As Integer
      Private _iZuol As Integer
      Private _strBPtM As String
      Private _strAnr As String
      Private _iAid As Integer
      Private _strPnr As String
      Private _strMnr As String
      Private _strMbz As String
      Private _strGsb As String
      Private _strBPtV As String
      Private _strBPtIo As String
      Private _strBPtBio As String
      Private _strBPtNio As String

      '' 'Order confirmation. Contains all the relevant data for the rückzumeldende order
      '' '</ Summary> 
      '' '<Param name = "imide"> current machine ID, as it was specified in the init </ param> 
      '' '<Param name = "iZuol"> number of mapping logic </ param> 
      '' '<Param name = "strBPtM"> Machining Pattern of the machine, as it was specified in the init. </ Param> 
      '' '<Param name = "strAnr"> order number. Contains the RFID </ param> 
      '' '<Param name = "IAID"> Job ID. IMPORTANT !! Must be given back to back for the FEEDBACK </ param> 
      '' '<Param name = "strPnr"> production number (for JIT process. Otherwise empty) </ param> 
      '' '<Param name = "strMnr"> material number </ param> 
      '' '<Param name = "strMbz"> Materialbezeichung </ param> 
      '' '<Param name = "strGsb"> overall status in binary representation. (8 bits as 0 or 1 Characters) </ param> 
      '' '<Param name = "strBPtV"> Machining Pattern specification. (192 bits a, s is 0 or 1 Characters) </ param> 
      '' '<Param name = "strBPtIo"> Machining Pattern OK. (192 bit as 0 or 1 Characters) </ param> 
      '' '<Param name = "strBPtBio"> Machining Pattern BIO. (192 bit as 0 or 1 Characters) </ param> 
      '' '<Param name = "strBPtNio"> Machining Pattern NG. (192 bit as 0 or 1 Characters) </ param>  
      Public Sub New(ByVal iMID As Integer, ByVal iZuol As Integer, ByVal strBPtM As String, ByVal strAnr As String, ByVal iAid As Integer, ByVal strPnr As String, ByVal strMnr As String, ByVal strMbz As String, ByVal strGsb As String, ByVal strBPtV As String, ByVal strBPtIo As String, ByVal strBPtBio As String, ByVal strBPtNio As String)
         _iMID = iMID
         _iZuol = iZuol
         _strBPtM = strBPtM
         _strAnr = strAnr
         _iAid = iAid
         _strPnr = strPnr
         _strMnr = strMnr
         _strMbz = strMbz
         _strGsb = strGsb
         _strBPtV = strBPtV
         _strBPtIo = strBPtIo
         _strBPtBio = strBPtBio
         _strBPtNio = strBPtNio
      End Sub

      Public Sub Reset()
         _iMID = 0
         _iZuol = 0
         _strBPtM = "".PadRight(192, "0"c)
         _strAnr = ""
         _iAid = 0
         _strPnr = ""
         _strMnr = ""
         _strMbz = ""
         _strGsb = "".PadRight(8, "0"c)
         _strBPtV = "".PadRight(192, "0"c)
         _strBPtIo = "".PadRight(192, "0"c)
         _strBPtBio = "".PadRight(192, "0"c)
         _strBPtNio = "".PadRight(192, "0"c)
      End Sub

      Public Property iMID() As Integer
         Get
            Return (_iMID)
         End Get
         Set(ByVal value As Integer)
            _iMID = value
         End Set
      End Property
      Public Property iZuol() As Integer
         Get
            Return (_iZuol)
         End Get
         Set(ByVal value As Integer)
            _iZuol = value
         End Set
      End Property
      Public Property strBPtM() As String
         Get
            Return (_strBPtM)
         End Get
         Set(ByVal value As String)
            _strBPtM = value
         End Set
      End Property
      Public Property strAnr() As String
         Get
            Return (_strAnr)
         End Get
         Set(ByVal value As String)
            _strAnr = value
         End Set
      End Property
      Public Property iAid() As Integer
         Get
            Return (_iAid)
         End Get
         Set(ByVal value As Integer)
            _iAid = value
         End Set
      End Property
      Public Property strPnr() As String
         Get
            Return (_strPnr)
         End Get
         Set(ByVal value As String)
            _strPnr = value
         End Set
      End Property
      Public Property strMnr() As String
         Get
            Return (_strMnr)
         End Get
         Set(ByVal value As String)
            _strMnr = value
         End Set
      End Property
      Public Property strMbz() As String
         Get
            Return (_strMbz)
         End Get
         Set(ByVal value As String)
            _strMbz = value
         End Set
      End Property
      Public Property strGsb() As String
         Get
            Return (_strGsb)
         End Get
         Set(ByVal value As String)
            _strGsb = value
         End Set
      End Property
      Public Property strBPtV() As String
         Get
            Return (_strBPtV)
         End Get
         Set(ByVal value As String)
            _strBPtV = value
         End Set
      End Property
      Public Property strBPtIo() As String
         Get
            Return (_strBPtIo)
         End Get
         Set(ByVal value As String)
            _strBPtIo = value
         End Set
      End Property
      Public Property strBPtBio() As String
         Get
            Return (_strBPtBio)
         End Get
         Set(ByVal value As String)
            _strBPtBio = value
         End Set
      End Property
      Public Property strBPtNio() As String
         Get
            Return (_strBPtNio)
         End Get
         Set(ByVal value As String)
            _strBPtNio = value
         End Set
      End Property
   End Class

   ''' <summary>
   ''' Container class for the order data a new job
   ''' </summary>
   Friend Class Order
      Private _iMID As Integer
      Private _iZuol As Integer
      Private _strBPtM As String
      Private _strAnr As String
      Private _iAid As Integer
      Private _strPnr As String
      Private _strMnr As String
      Private _strMbz As String
      Private _strGsb As String
      Private _strBPtV As String

      '' 'New Job. Contains all the relevant data for the new job 
      '' '</ Summary> 
      '' '<Param name = "imide"> current machine ID, as it was specified in the init </ param> 
      '' '<Param name = "iZuol"> number of mapping logic </ param> 
      '' '<Param name = "strBPtM"> Machining Pattern of the machine, as it was specified in the init. </ Param> 
      '' '<Param name = "strAnr"> order number. Contains the RFID </ param> 
      '' '<Param name = "IAID"> Job ID. IMPORTANT !! Must be given back to back for the FEEDBACK </ param> 
      '' '<Param name = "strPnr"> production number (for JIT process. Otherwise empty) </ param> 
      '' '<Param name = "strMnr"> material number </ param> 
      '' '<Param name = "strMbz"> Materialbezeichung </ param> 
      '' '<Param name = "strGsb"> overall status in binary representation. (8 bits as 0 or 1 Characters) </ param> 
      '' '<Param name = "strBPtV"> Machining Pattern specification. (192 bits a, s is 0 or 1 Characters) </ param>
      Public Sub New(ByVal iMID As Integer, ByVal iZuol As Integer, ByVal strBPtM As String, ByVal strAnr As String, ByVal iAid As Integer, ByVal strPnr As String, ByVal strMnr As String, ByVal strMbz As String, ByVal strGsb As String, ByVal strBPtV As String)
         _iMID = iMID
         _iZuol = iZuol
         _strBPtM = strBPtM
         _strAnr = strAnr
         _iAid = iAid
         _strPnr = strPnr
         _strMnr = strMnr
         _strMbz = strMbz
         _strGsb = strGsb
         _strBPtV = strBPtV
      End Sub

      Public Sub Reset()
         _iMID = 0
         _iZuol = 0
         _strBPtM = "".PadRight(192, "0"c)
         _strAnr = ""
         _iAid = 0
         _strPnr = ""
         _strMnr = ""
         _strMbz = ""
         _strGsb = "".PadRight(8, "0"c)
         _strBPtV = "".PadRight(192, "0"c)
      End Sub

      Public Property iMID() As Integer
         Get
            Return (_iMID)
         End Get
         Set(ByVal value As Integer)
            _iMID = value
         End Set
      End Property
      Public Property iZuol() As Integer
         Get
            Return (_iZuol)
         End Get
         Set(ByVal value As Integer)
            _iZuol = value
         End Set
      End Property
      Public Property strBPtM() As String
         Get
            Return (_strBPtM)
         End Get
         Set(ByVal value As String)
            _strBPtM = value
         End Set
      End Property
      Public Property strAnr() As String
         Get
            Return (_strAnr)
         End Get
         Set(ByVal value As String)
            _strAnr = value
         End Set
      End Property
      Public Property iAid() As Integer
         Get
            Return (_iAid)
         End Get
         Set(ByVal value As Integer)
            _iAid = value
         End Set
      End Property
      Public Property strPnr() As String
         Get
            Return (_strPnr)
         End Get
         Set(ByVal value As String)
            _strPnr = value
         End Set
      End Property
      Public Property strMnr() As String
         Get
            Return (_strMnr)
         End Get
         Set(ByVal value As String)
            _strMnr = value
         End Set
      End Property
      Public Property strMbz() As String
         Get
            Return (_strMbz)
         End Get
         Set(ByVal value As String)
            _strMbz = value
         End Set
      End Property
      Public Property strGsb() As String
         Get
            Return (_strGsb)
         End Get
         Set(ByVal value As String)
            _strGsb = value
         End Set
      End Property
      Public Property strBPtV() As String
         Get
            Return (_strBPtV)
         End Get
         Set(ByVal value As String)
            _strBPtV = value
         End Set
      End Property
   End Class

 
  
End Module
