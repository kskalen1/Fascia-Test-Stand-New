Public Class frmSensorTestInformationDatabase

   Private Sub RadarSensorTestsBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles RadarSensorTestsBindingNavigatorSaveItem.Click
      Me.Validate()
      Me.RadarSensorTestsBindingSource.EndEdit()
      Me.TableAdapterManager.UpdateAll(Me.Test_InformationDataSet1)

   End Sub

   Private Sub frmSensorTestInformationDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.RadarSensorTestsTableAdapter.Fill(Me.Test_InformationDataSet1.RadarSensorTests)

   End Sub
End Class