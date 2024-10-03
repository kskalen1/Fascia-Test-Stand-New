Public Class frmRFIDDatabase

   Private Sub frmRFIDDatabase_Load(sender As Object, e As EventArgs) Handles Me.Load

      Me.BITSTableAdapter.Fill(Me.RFID_BITSDataSet.BITS)

   End Sub

   Private Sub BITSBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BITSBindingNavigatorSaveItem.Click

      Me.Validate()
      Me.BITSBindingSource.EndEdit()
      Me.RFID_BITS_TableAdapterManager.UpdateAll(Me.RFID_BITSDataSet)

   End Sub
End Class