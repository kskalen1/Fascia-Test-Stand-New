Public Class frmPassword

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Me.PasswordTextBox.Text = Password Then
            PasswordOk = True
            Me.Close()
        Else
            MsgBox("Password Does Not Match", MsgBoxStyle.Exclamation, "Password Error")
            Me.PasswordTextBox.Text = ""
            PasswordOk = False
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        PasswordOk = False
        Me.Close()
    End Sub

    Private Sub frmPassword_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.PasswordTextBox.Text = ""
    End Sub

    Private Sub frmPassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      Me.PasswordTextBox.Text = ""
   End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
   End Sub
End Class
