<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class OperatorLoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.lblOperatorIdMessage = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.txtoperatorId = New System.Windows.Forms.MaskedTextBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(209, 46)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(220, 23)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Operator ID"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(209, 113)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(220, 23)
        Me.Cancel.TabIndex = 2
        Me.Cancel.Text = "&Cancel"
        '
        'lblOperatorIdMessage
        '
        Me.lblOperatorIdMessage.Location = New System.Drawing.Point(209, 6)
        Me.lblOperatorIdMessage.Name = "lblOperatorIdMessage"
        Me.lblOperatorIdMessage.Size = New System.Drawing.Size(220, 23)
        Me.lblOperatorIdMessage.TabIndex = 6
        Me.lblOperatorIdMessage.Text = "Please Scan Your Badge ID Number"
        Me.lblOperatorIdMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Fascia_Test_Stand.My.Resources.Rehau_Logo
        Me.PictureBox1.Location = New System.Drawing.Point(3, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 80)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'txtoperatorId
        '
        Me.txtoperatorId.Location = New System.Drawing.Point(209, 72)
        Me.txtoperatorId.Mask = "00000"
        Me.txtoperatorId.Name = "txtoperatorId"
        Me.txtoperatorId.PromptChar = Global.Microsoft.VisualBasic.ChrW(45)
        Me.txtoperatorId.Size = New System.Drawing.Size(220, 20)
        Me.txtoperatorId.TabIndex = 1
        Me.txtoperatorId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtoperatorId.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.txtoperatorId.ValidatingType = GetType(Integer)
        '
        'OperatorLoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(444, 158)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtoperatorId)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblOperatorIdMessage)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.PasswordLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OperatorLoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Operator ID Login"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblOperatorIdMessage As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtoperatorId As System.Windows.Forms.MaskedTextBox

End Class
