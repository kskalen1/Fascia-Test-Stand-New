Public Class frmGeneralTestInformationDatabase

   Private Sub GeneralTestsBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles GeneralTestsBindingNavigatorSaveItem.Click
      Me.Validate()
      Me.GeneralTestsBindingSource.EndEdit()
      Me.GeneralTestTableAdapterManager.UpdateAll(Me.Test_InformationDataSet)

   End Sub

   Private Sub frmGeneralTestInformationDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

      Me.GeneralTestsTableAdapter.Fill(Me.Test_InformationDataSet.GeneralTests)

   End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click

    End Sub
End Class