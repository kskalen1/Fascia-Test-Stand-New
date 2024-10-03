Public Class OperatorLoginForm
    Dim CurrentID As String = ""

    Private Sub OperatorLoginForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtoperatorId.Focus()
    End Sub

    Private Sub OperatorLoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Save Current ID In case user cancels
        CurrentID = txtoperatorId.Text
        'Clear Out Old ID
        frmMain.txtOperatorID.BackColor = Color.Empty
        txtoperatorId.Text = ""
        frmMain.txtOperatorID.Text = ""
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        'Restore Saved ID
        txtoperatorId.Text = CurrentID
        Me.Close()
    End Sub

End Class
