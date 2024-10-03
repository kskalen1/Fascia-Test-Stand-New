<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionNumber
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtUserInputProductionNumber = New System.Windows.Forms.TextBox
        Me.btnProductionNumberOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(85, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please Enter A Production Number For This Fascia"
        '
        'txtUserInputProductionNumber
        '
        Me.txtUserInputProductionNumber.Location = New System.Drawing.Point(88, 45)
        Me.txtUserInputProductionNumber.Name = "txtUserInputProductionNumber"
        Me.txtUserInputProductionNumber.Size = New System.Drawing.Size(243, 20)
        Me.txtUserInputProductionNumber.TabIndex = 1
        '
        'btnProductionNumberOk
        '
        Me.btnProductionNumberOk.Location = New System.Drawing.Point(167, 91)
        Me.btnProductionNumberOk.Name = "btnProductionNumberOk"
        Me.btnProductionNumberOk.Size = New System.Drawing.Size(75, 23)
        Me.btnProductionNumberOk.TabIndex = 2
        Me.btnProductionNumberOk.Text = "OK"
        Me.btnProductionNumberOk.UseVisualStyleBackColor = True
        '
        'frmProductionNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 137)
        Me.Controls.Add(Me.btnProductionNumberOk)
        Me.Controls.Add(Me.txtUserInputProductionNumber)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmProductionNumber"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Production Number Input"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUserInputProductionNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnProductionNumberOk As System.Windows.Forms.Button
End Class
