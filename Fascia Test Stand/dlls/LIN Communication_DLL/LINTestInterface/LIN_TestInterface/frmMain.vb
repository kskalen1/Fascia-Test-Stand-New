Public Class frmMain
   Dim LIN_Interface As NI_LIN.NI_LIN_COM

   Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
      End
   End Sub

   Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      LIN_Interface = New NI_LIN.NI_LIN_COM

   End Sub

   Private Sub btnInitLIN_Click(sender As Object, e As EventArgs) Handles btnInitLIN.Click
      Try
         LIN_Interface.Intitalize_LIN_Interface(CInt(Val(txtBuadRate.Text)), txtLIN_DeviceName.Text)
      Catch ex As Exception
         Stop
      End Try
   End Sub


   Private Sub btnWriteLINData_Click(sender As Object, e As EventArgs) Handles btnWriteReadLINData.Click
      Dim Bytes_To_Send(7) As Byte
      Dim Bytes_Received(99) As Byte
      Dim FirstArray(7) As Byte
      Dim Array21(7) As Byte
      Dim Array22(7) As Byte
      Dim ByteMessages As String = ""
      Dim ECUSerialNumber As String = ""

      Bytes_To_Send(0) = CByte(Val("&H" & txtMSGTxData_0.Text))
      Bytes_To_Send(1) = CByte(Val("&H" & txtMSGTxData_1.Text))
      Bytes_To_Send(2) = CByte(Val("&H" & txtMSGTxData_2.Text))
      Bytes_To_Send(3) = CByte(Val("&H" & txtMSGTxData_3.Text))
      Bytes_To_Send(4) = CByte(Val("&H" & txtMSGTxData_4.Text))
      Bytes_To_Send(5) = CByte(Val("&H" & txtMSGTxData_5.Text))
      Bytes_To_Send(6) = CByte(Val("&H" & txtMSGTxData_6.Text))
      Bytes_To_Send(7) = CByte(Val("&H" & txtMSGTxData_7.Text))
      Label2.Text = ""
      Try
         LIN_Interface.WriteReadBytes(True, Bytes_To_Send, CInt(Val("&H" & txtSendArbID.Text)), 8, CInt(Val("&H" & txtReceiveArbID.Text)), false, Bytes_Received, Label2.Text)
         LIN_Interface.WriteReadBytes(False, Bytes_To_Send, CInt(Val("&H" & txtSendArbID.Text)), 8, CInt(Val("&H" & txtReceiveArbID.Text)), True, Bytes_Received, Label2.Text)


         '     If Proper Response Is Received Then 
         Array.Copy(Bytes_Received, FirstArray, 8)

            'Send Header to get next set of Bytes
            LIN_Interface.WriteReadBytes(True, Bytes_To_Send, CInt(Val("&H" & txtSendArbID.Text)), 8, CInt(Val("&H" & txtReceiveArbID.Text)), True, Bytes_Received, ByteMessages)
            If Bytes_Received(0) <> 0 Then
               Array.Copy(Bytes_Received, Array21, 8)
            End If

         ''Send Header to get next set of Bytes
         'LIN_Interface.WriteReadBytes(True, Bytes_To_Send, CInt(Val("&H" & txtSendArbID.Text)), 8, CInt(Val("&H" & txtReceiveArbID.Text)), True, Bytes_Received, ByteMessages)
         'If Bytes_Received(0) <> 0 Then
         '   Array.Copy(Bytes_Received, Array22, 8)
         'End If

         'ECUSerialNumber = Chr(FirstArray(6)) & Chr(FirstArray(7)) & Chr(Array21(2)) & Chr(Array21(3)) & Chr(Array21(4)) & Chr(Array21(5)) & Chr(Array21(6)) & Chr(Array21(7)) _
         '                   & Chr(Array22(2)) & Chr(Array22(3))

      Catch ex As Exception
         Stop
      End Try

      txtMSGRxData_0.Text = Hex(Bytes_Received(0)).ToString
      txtMSGRxData_1.Text = Hex(Bytes_Received(1)).ToString
      txtMSGRxData_2.Text = Hex(Bytes_Received(2)).ToString
      txtMSGRxData_3.Text = Hex(Bytes_Received(3)).ToString
      txtMSGRxData_4.Text = Hex(Bytes_Received(4)).ToString
      txtMSGRxData_5.Text = Hex(Bytes_Received(5)).ToString
      txtMSGRxData_6.Text = Hex(Bytes_Received(6)).ToString
      txtMSGRxData_7.Text = Hex(Bytes_Received(7)).ToString
   End Sub

   Private Sub txtMSGTxData_1_TextChanged(sender As Object, e As EventArgs) Handles txtMSGTxData_1.TextChanged

   End Sub
End Class
