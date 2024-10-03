Public Class frmProductionNumber

    Private Sub btnProductionNumberOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductionNumberOk.Click
        frmMain.txtProductionNumber.ReadOnly = False
        Select Case Val(txtUserInputProductionNumber.Text)
            Case Is = 0
                MsgBox("Please Enter A Value Before Pressing OK", MsgBoxStyle.Critical, "Input Error")
            Case 1 To 999999999999
                frmMain.txtProductionNumber.Text = txtUserInputProductionNumber.Text
                frmMain.txtProductionNumber.Refresh()
                Me.Close()
            Case Else
                MsgBox("Value Does Not Appear To Be Correct", MsgBoxStyle.Critical, "Input Error")
        End Select
        frmMain.txtProductionNumber.ReadOnly = True
    End Sub
End Class