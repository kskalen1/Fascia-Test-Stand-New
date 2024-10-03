<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
      Me.GroupBox1 = New System.Windows.Forms.GroupBox()
      Me.gbReceiveArbID = New System.Windows.Forms.GroupBox()
      Me.txtReceiveArbID = New System.Windows.Forms.TextBox()
      Me.gbSendArbID = New System.Windows.Forms.GroupBox()
      Me.txtSendArbID = New System.Windows.Forms.TextBox()
      Me.GroupBox2 = New System.Windows.Forms.GroupBox()
      Me.txtBuadRate = New System.Windows.Forms.TextBox()
      Me.gbLIN_Device = New System.Windows.Forms.GroupBox()
      Me.txtLIN_DeviceName = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_0 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_1 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_2 = New System.Windows.Forms.TextBox()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.txtMSGRxData_3 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_7 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_4 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_6 = New System.Windows.Forms.TextBox()
      Me.txtMSGRxData_5 = New System.Windows.Forms.TextBox()
      Me.Label3 = New System.Windows.Forms.Label()
      Me.txtMSGTxData_0 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_1 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_2 = New System.Windows.Forms.TextBox()
      Me.lblData = New System.Windows.Forms.Label()
      Me.txtMSGTxData_3 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_7 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_4 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_6 = New System.Windows.Forms.TextBox()
      Me.txtMSGTxData_5 = New System.Windows.Forms.TextBox()
      Me.Label6 = New System.Windows.Forms.Label()
      Me.btnWriteReadLINData = New System.Windows.Forms.Button()
      Me.btnInitLIN = New System.Windows.Forms.Button()
      Me.Label2 = New System.Windows.Forms.Label()
      Me.GroupBox1.SuspendLayout()
      Me.gbReceiveArbID.SuspendLayout()
      Me.gbSendArbID.SuspendLayout()
      Me.GroupBox2.SuspendLayout()
      Me.gbLIN_Device.SuspendLayout()
      Me.SuspendLayout()
      '
      'GroupBox1
      '
      Me.GroupBox1.Controls.Add(Me.gbReceiveArbID)
      Me.GroupBox1.Controls.Add(Me.gbSendArbID)
      Me.GroupBox1.Controls.Add(Me.GroupBox2)
      Me.GroupBox1.Controls.Add(Me.gbLIN_Device)
      Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
      Me.GroupBox1.Name = "GroupBox1"
      Me.GroupBox1.Size = New System.Drawing.Size(189, 256)
      Me.GroupBox1.TabIndex = 0
      Me.GroupBox1.TabStop = False
      Me.GroupBox1.Text = "Initialization Settings"
      '
      'gbReceiveArbID
      '
      Me.gbReceiveArbID.Controls.Add(Me.txtReceiveArbID)
      Me.gbReceiveArbID.Location = New System.Drawing.Point(6, 184)
      Me.gbReceiveArbID.Name = "gbReceiveArbID"
      Me.gbReceiveArbID.Size = New System.Drawing.Size(173, 49)
      Me.gbReceiveArbID.TabIndex = 3
      Me.gbReceiveArbID.TabStop = False
      Me.gbReceiveArbID.Text = "Arb ID (Receive)"
      '
      'txtReceiveArbID
      '
      Me.txtReceiveArbID.Dock = System.Windows.Forms.DockStyle.Fill
      Me.txtReceiveArbID.Location = New System.Drawing.Point(3, 16)
      Me.txtReceiveArbID.Name = "txtReceiveArbID"
      Me.txtReceiveArbID.Size = New System.Drawing.Size(167, 20)
      Me.txtReceiveArbID.TabIndex = 0
      Me.txtReceiveArbID.Text = "3D"
      '
      'gbSendArbID
      '
      Me.gbSendArbID.Controls.Add(Me.txtSendArbID)
      Me.gbSendArbID.Location = New System.Drawing.Point(6, 129)
      Me.gbSendArbID.Name = "gbSendArbID"
      Me.gbSendArbID.Size = New System.Drawing.Size(173, 49)
      Me.gbSendArbID.TabIndex = 2
      Me.gbSendArbID.TabStop = False
      Me.gbSendArbID.Text = "Arb ID (Send)"
      '
      'txtSendArbID
      '
      Me.txtSendArbID.Dock = System.Windows.Forms.DockStyle.Fill
      Me.txtSendArbID.Location = New System.Drawing.Point(3, 16)
      Me.txtSendArbID.Name = "txtSendArbID"
      Me.txtSendArbID.Size = New System.Drawing.Size(167, 20)
      Me.txtSendArbID.TabIndex = 0
      Me.txtSendArbID.Text = "3C"
      '
      'GroupBox2
      '
      Me.GroupBox2.Controls.Add(Me.txtBuadRate)
      Me.GroupBox2.Location = New System.Drawing.Point(6, 74)
      Me.GroupBox2.Name = "GroupBox2"
      Me.GroupBox2.Size = New System.Drawing.Size(173, 49)
      Me.GroupBox2.TabIndex = 1
      Me.GroupBox2.TabStop = False
      Me.GroupBox2.Text = "Buad Rate"
      '
      'txtBuadRate
      '
      Me.txtBuadRate.Dock = System.Windows.Forms.DockStyle.Fill
      Me.txtBuadRate.Location = New System.Drawing.Point(3, 16)
      Me.txtBuadRate.Name = "txtBuadRate"
      Me.txtBuadRate.Size = New System.Drawing.Size(167, 20)
      Me.txtBuadRate.TabIndex = 0
      Me.txtBuadRate.Text = "19200"
      '
      'gbLIN_Device
      '
      Me.gbLIN_Device.Controls.Add(Me.txtLIN_DeviceName)
      Me.gbLIN_Device.Location = New System.Drawing.Point(6, 19)
      Me.gbLIN_Device.Name = "gbLIN_Device"
      Me.gbLIN_Device.Size = New System.Drawing.Size(173, 49)
      Me.gbLIN_Device.TabIndex = 0
      Me.gbLIN_Device.TabStop = False
      Me.gbLIN_Device.Text = "LIN Device Name"
      '
      'txtLIN_DeviceName
      '
      Me.txtLIN_DeviceName.Dock = System.Windows.Forms.DockStyle.Fill
      Me.txtLIN_DeviceName.Location = New System.Drawing.Point(3, 16)
      Me.txtLIN_DeviceName.Name = "txtLIN_DeviceName"
      Me.txtLIN_DeviceName.Size = New System.Drawing.Size(167, 20)
      Me.txtLIN_DeviceName.TabIndex = 0
      Me.txtLIN_DeviceName.Text = "LIN0"
      '
      'txtMSGRxData_0
      '
      Me.txtMSGRxData_0.AcceptsReturn = True
      Me.txtMSGRxData_0.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_0.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_0.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_0.Location = New System.Drawing.Point(386, 118)
      Me.txtMSGRxData_0.MaxLength = 0
      Me.txtMSGRxData_0.Name = "txtMSGRxData_0"
      Me.txtMSGRxData_0.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_0.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_0.TabIndex = 148
      Me.txtMSGRxData_0.Text = "00"
      Me.txtMSGRxData_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_1
      '
      Me.txtMSGRxData_1.AcceptsReturn = True
      Me.txtMSGRxData_1.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_1.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_1.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_1.Location = New System.Drawing.Point(418, 118)
      Me.txtMSGRxData_1.MaxLength = 0
      Me.txtMSGRxData_1.Name = "txtMSGRxData_1"
      Me.txtMSGRxData_1.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_1.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_1.TabIndex = 147
      Me.txtMSGRxData_1.Text = "00"
      Me.txtMSGRxData_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_2
      '
      Me.txtMSGRxData_2.AcceptsReturn = True
      Me.txtMSGRxData_2.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_2.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_2.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_2.Location = New System.Drawing.Point(450, 118)
      Me.txtMSGRxData_2.MaxLength = 0
      Me.txtMSGRxData_2.Name = "txtMSGRxData_2"
      Me.txtMSGRxData_2.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_2.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_2.TabIndex = 146
      Me.txtMSGRxData_2.Text = "00"
      Me.txtMSGRxData_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'Label1
      '
      Me.Label1.BackColor = System.Drawing.SystemColors.Control
      Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
      Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
      Me.Label1.Location = New System.Drawing.Point(354, 118)
      Me.Label1.Name = "Label1"
      Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.Label1.Size = New System.Drawing.Size(33, 22)
      Me.Label1.TabIndex = 149
      Me.Label1.Text = "Data"
      '
      'txtMSGRxData_3
      '
      Me.txtMSGRxData_3.AcceptsReturn = True
      Me.txtMSGRxData_3.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_3.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_3.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_3.Location = New System.Drawing.Point(482, 118)
      Me.txtMSGRxData_3.MaxLength = 0
      Me.txtMSGRxData_3.Name = "txtMSGRxData_3"
      Me.txtMSGRxData_3.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_3.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_3.TabIndex = 145
      Me.txtMSGRxData_3.Text = "00"
      Me.txtMSGRxData_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_7
      '
      Me.txtMSGRxData_7.AcceptsReturn = True
      Me.txtMSGRxData_7.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_7.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_7.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_7.Location = New System.Drawing.Point(610, 118)
      Me.txtMSGRxData_7.MaxLength = 0
      Me.txtMSGRxData_7.Name = "txtMSGRxData_7"
      Me.txtMSGRxData_7.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_7.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_7.TabIndex = 141
      Me.txtMSGRxData_7.Text = "00"
      Me.txtMSGRxData_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_4
      '
      Me.txtMSGRxData_4.AcceptsReturn = True
      Me.txtMSGRxData_4.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_4.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_4.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_4.Location = New System.Drawing.Point(514, 118)
      Me.txtMSGRxData_4.MaxLength = 0
      Me.txtMSGRxData_4.Name = "txtMSGRxData_4"
      Me.txtMSGRxData_4.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_4.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_4.TabIndex = 144
      Me.txtMSGRxData_4.Text = "00"
      Me.txtMSGRxData_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_6
      '
      Me.txtMSGRxData_6.AcceptsReturn = True
      Me.txtMSGRxData_6.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_6.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_6.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_6.Location = New System.Drawing.Point(578, 118)
      Me.txtMSGRxData_6.MaxLength = 0
      Me.txtMSGRxData_6.Name = "txtMSGRxData_6"
      Me.txtMSGRxData_6.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_6.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_6.TabIndex = 142
      Me.txtMSGRxData_6.Text = "00"
      Me.txtMSGRxData_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGRxData_5
      '
      Me.txtMSGRxData_5.AcceptsReturn = True
      Me.txtMSGRxData_5.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGRxData_5.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGRxData_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGRxData_5.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGRxData_5.Location = New System.Drawing.Point(546, 118)
      Me.txtMSGRxData_5.MaxLength = 0
      Me.txtMSGRxData_5.Name = "txtMSGRxData_5"
      Me.txtMSGRxData_5.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGRxData_5.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGRxData_5.TabIndex = 143
      Me.txtMSGRxData_5.Text = "00"
      Me.txtMSGRxData_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.Location = New System.Drawing.Point(393, 102)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(97, 13)
      Me.Label3.TabIndex = 140
      Me.Label3.Text = "Message Returned"
      '
      'txtMSGTxData_0
      '
      Me.txtMSGTxData_0.AcceptsReturn = True
      Me.txtMSGTxData_0.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_0.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_0.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_0.Location = New System.Drawing.Point(386, 70)
      Me.txtMSGTxData_0.MaxLength = 0
      Me.txtMSGTxData_0.Name = "txtMSGTxData_0"
      Me.txtMSGTxData_0.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_0.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_0.TabIndex = 138
      Me.txtMSGTxData_0.Text = "21"
      Me.txtMSGTxData_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_1
      '
      Me.txtMSGTxData_1.AcceptsReturn = True
      Me.txtMSGTxData_1.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_1.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_1.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_1.Location = New System.Drawing.Point(418, 70)
      Me.txtMSGTxData_1.MaxLength = 0
      Me.txtMSGTxData_1.Name = "txtMSGTxData_1"
      Me.txtMSGTxData_1.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_1.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_1.TabIndex = 137
      Me.txtMSGTxData_1.Text = "03"
      Me.txtMSGTxData_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_2
      '
      Me.txtMSGTxData_2.AcceptsReturn = True
      Me.txtMSGTxData_2.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_2.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_2.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_2.Location = New System.Drawing.Point(450, 70)
      Me.txtMSGTxData_2.MaxLength = 0
      Me.txtMSGTxData_2.Name = "txtMSGTxData_2"
      Me.txtMSGTxData_2.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_2.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_2.TabIndex = 136
      Me.txtMSGTxData_2.Text = "22"
      Me.txtMSGTxData_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'lblData
      '
      Me.lblData.BackColor = System.Drawing.SystemColors.Control
      Me.lblData.Cursor = System.Windows.Forms.Cursors.Default
      Me.lblData.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblData.ForeColor = System.Drawing.SystemColors.ControlText
      Me.lblData.Location = New System.Drawing.Point(354, 70)
      Me.lblData.Name = "lblData"
      Me.lblData.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.lblData.Size = New System.Drawing.Size(33, 22)
      Me.lblData.TabIndex = 139
      Me.lblData.Text = "Data"
      '
      'txtMSGTxData_3
      '
      Me.txtMSGTxData_3.AcceptsReturn = True
      Me.txtMSGTxData_3.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_3.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_3.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_3.Location = New System.Drawing.Point(482, 70)
      Me.txtMSGTxData_3.MaxLength = 0
      Me.txtMSGTxData_3.Name = "txtMSGTxData_3"
      Me.txtMSGTxData_3.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_3.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_3.TabIndex = 135
      Me.txtMSGTxData_3.Text = "F1"
      Me.txtMSGTxData_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_7
      '
      Me.txtMSGTxData_7.AcceptsReturn = True
      Me.txtMSGTxData_7.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_7.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_7.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_7.Location = New System.Drawing.Point(610, 70)
      Me.txtMSGTxData_7.MaxLength = 0
      Me.txtMSGTxData_7.Name = "txtMSGTxData_7"
      Me.txtMSGTxData_7.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_7.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_7.TabIndex = 131
      Me.txtMSGTxData_7.Text = "00"
      Me.txtMSGTxData_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_4
      '
      Me.txtMSGTxData_4.AcceptsReturn = True
      Me.txtMSGTxData_4.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_4.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_4.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_4.Location = New System.Drawing.Point(514, 70)
      Me.txtMSGTxData_4.MaxLength = 0
      Me.txtMSGTxData_4.Name = "txtMSGTxData_4"
      Me.txtMSGTxData_4.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_4.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_4.TabIndex = 134
      Me.txtMSGTxData_4.Text = "8C"
      Me.txtMSGTxData_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_6
      '
      Me.txtMSGTxData_6.AcceptsReturn = True
      Me.txtMSGTxData_6.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_6.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_6.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_6.Location = New System.Drawing.Point(578, 70)
      Me.txtMSGTxData_6.MaxLength = 0
      Me.txtMSGTxData_6.Name = "txtMSGTxData_6"
      Me.txtMSGTxData_6.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_6.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_6.TabIndex = 132
      Me.txtMSGTxData_6.Text = "00"
      Me.txtMSGTxData_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'txtMSGTxData_5
      '
      Me.txtMSGTxData_5.AcceptsReturn = True
      Me.txtMSGTxData_5.BackColor = System.Drawing.SystemColors.Window
      Me.txtMSGTxData_5.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.txtMSGTxData_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtMSGTxData_5.ForeColor = System.Drawing.SystemColors.WindowText
      Me.txtMSGTxData_5.Location = New System.Drawing.Point(546, 70)
      Me.txtMSGTxData_5.MaxLength = 0
      Me.txtMSGTxData_5.Name = "txtMSGTxData_5"
      Me.txtMSGTxData_5.RightToLeft = System.Windows.Forms.RightToLeft.No
      Me.txtMSGTxData_5.Size = New System.Drawing.Size(33, 20)
      Me.txtMSGTxData_5.TabIndex = 133
      Me.txtMSGTxData_5.Text = "00"
      Me.txtMSGTxData_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      '
      'Label6
      '
      Me.Label6.AutoSize = True
      Me.Label6.Location = New System.Drawing.Point(393, 54)
      Me.Label6.Name = "Label6"
      Me.Label6.Size = New System.Drawing.Size(94, 13)
      Me.Label6.TabIndex = 130
      Me.Label6.Text = "Message To Write"
      '
      'btnWriteReadLINData
      '
      Me.btnWriteReadLINData.Location = New System.Drawing.Point(207, 57)
      Me.btnWriteReadLINData.Name = "btnWriteReadLINData"
      Me.btnWriteReadLINData.Size = New System.Drawing.Size(141, 28)
      Me.btnWriteReadLINData.TabIndex = 152
      Me.btnWriteReadLINData.Text = "Write Read LIN Message"
      Me.btnWriteReadLINData.UseVisualStyleBackColor = True
      '
      'btnInitLIN
      '
      Me.btnInitLIN.Location = New System.Drawing.Point(207, 22)
      Me.btnInitLIN.Name = "btnInitLIN"
      Me.btnInitLIN.Size = New System.Drawing.Size(141, 28)
      Me.btnInitLIN.TabIndex = 150
      Me.btnInitLIN.Text = "Init LIN Device"
      Me.btnInitLIN.UseVisualStyleBackColor = True
      '
      'Label2
      '
      Me.Label2.Location = New System.Drawing.Point(228, 189)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(414, 78)
      Me.Label2.TabIndex = 153
      Me.Label2.Text = "Label2"
      '
      'frmMain
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(664, 276)
      Me.Controls.Add(Me.Label2)
      Me.Controls.Add(Me.btnWriteReadLINData)
      Me.Controls.Add(Me.btnInitLIN)
      Me.Controls.Add(Me.txtMSGRxData_0)
      Me.Controls.Add(Me.txtMSGRxData_1)
      Me.Controls.Add(Me.txtMSGRxData_2)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.txtMSGRxData_3)
      Me.Controls.Add(Me.txtMSGRxData_7)
      Me.Controls.Add(Me.txtMSGRxData_4)
      Me.Controls.Add(Me.txtMSGRxData_6)
      Me.Controls.Add(Me.txtMSGRxData_5)
      Me.Controls.Add(Me.Label3)
      Me.Controls.Add(Me.txtMSGTxData_0)
      Me.Controls.Add(Me.txtMSGTxData_1)
      Me.Controls.Add(Me.txtMSGTxData_2)
      Me.Controls.Add(Me.lblData)
      Me.Controls.Add(Me.txtMSGTxData_3)
      Me.Controls.Add(Me.txtMSGTxData_7)
      Me.Controls.Add(Me.txtMSGTxData_4)
      Me.Controls.Add(Me.txtMSGTxData_6)
      Me.Controls.Add(Me.txtMSGTxData_5)
      Me.Controls.Add(Me.Label6)
      Me.Controls.Add(Me.GroupBox1)
      Me.Name = "frmMain"
      Me.Text = "NI LIN Test Interface"
      Me.GroupBox1.ResumeLayout(False)
      Me.gbReceiveArbID.ResumeLayout(False)
      Me.gbReceiveArbID.PerformLayout()
      Me.gbSendArbID.ResumeLayout(False)
      Me.gbSendArbID.PerformLayout()
      Me.GroupBox2.ResumeLayout(False)
      Me.GroupBox2.PerformLayout()
      Me.gbLIN_Device.ResumeLayout(False)
      Me.gbLIN_Device.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
   Friend WithEvents gbReceiveArbID As System.Windows.Forms.GroupBox
   Friend WithEvents txtReceiveArbID As System.Windows.Forms.TextBox
   Friend WithEvents gbSendArbID As System.Windows.Forms.GroupBox
   Friend WithEvents txtSendArbID As System.Windows.Forms.TextBox
   Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
   Friend WithEvents txtBuadRate As System.Windows.Forms.TextBox
   Friend WithEvents gbLIN_Device As System.Windows.Forms.GroupBox
   Friend WithEvents txtLIN_DeviceName As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_0 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_1 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_2 As System.Windows.Forms.TextBox
   Public WithEvents Label1 As System.Windows.Forms.Label
   Public WithEvents txtMSGRxData_3 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_7 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_4 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_6 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGRxData_5 As System.Windows.Forms.TextBox
   Friend WithEvents Label3 As System.Windows.Forms.Label
   Public WithEvents txtMSGTxData_0 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_1 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_2 As System.Windows.Forms.TextBox
   Public WithEvents lblData As System.Windows.Forms.Label
   Public WithEvents txtMSGTxData_3 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_7 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_4 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_6 As System.Windows.Forms.TextBox
   Public WithEvents txtMSGTxData_5 As System.Windows.Forms.TextBox
   Friend WithEvents Label6 As System.Windows.Forms.Label
   Friend WithEvents btnWriteReadLINData As System.Windows.Forms.Button
   Friend WithEvents btnInitLIN As System.Windows.Forms.Button
   Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
