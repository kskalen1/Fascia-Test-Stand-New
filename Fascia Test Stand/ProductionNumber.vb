Imports System.Windows.Forms

Public Class frmProductionNumber

   Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
      Me.DialogResult = System.Windows.Forms.DialogResult.OK
      ProductionSequenceNumber = txtProductionNumber.Text
      Me.Close()
   End Sub

   Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
      Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.Close()
   End Sub

    Private Sub frmProductionNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.Show()
      Application.DoEvents()
        txtProductionNumber.Text = ""
        txtProductionNumber.Focus()
    End Sub
End Class
