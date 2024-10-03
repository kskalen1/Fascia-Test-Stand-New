<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSensorTestInformationDatabase
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
      Me.components = New System.ComponentModel.Container()
      Dim IDLabel1 As System.Windows.Forms.Label
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSensorTestInformationDatabase))
      Me.Label2 = New System.Windows.Forms.Label()
      Me.gbTestName = New System.Windows.Forms.GroupBox()
      Me.lblTestName = New System.Windows.Forms.Label()
      Me.TestNameTextBox = New System.Windows.Forms.TextBox()
      Me.RadarSensorTestsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
      Me.Test_InformationDataSet1 = New Fascia_Test_Stand.Test_InformationDataSet1()
      Me.gbTestDescription = New System.Windows.Forms.GroupBox()
      Me.lblTestDescription = New System.Windows.Forms.Label()
      Me.TestDescriptionTextBox = New System.Windows.Forms.TextBox()
      Me.gbTestType = New System.Windows.Forms.GroupBox()
      Me.lblTestType = New System.Windows.Forms.Label()
      Me.TestTypeTextBox = New System.Windows.Forms.TextBox()
      Me.gbSensorAddress1 = New System.Windows.Forms.GroupBox()
      Me.lblSensorAddress1 = New System.Windows.Forms.Label()
      Me.SensorAddress1TextBox = New System.Windows.Forms.TextBox()
      Me.gbSensorAddress2 = New System.Windows.Forms.GroupBox()
      Me.lblSensorAddress2 = New System.Windows.Forms.Label()
      Me.SensorAddress2TextBox = New System.Windows.Forms.TextBox()
      Me.gbUseMXbetdll = New System.Windows.Forms.GroupBox()
      Me.lblUseMXbetdll = New System.Windows.Forms.Label()
      Me.UseMXbetdllTextBox = New System.Windows.Forms.TextBox()
      Me.gbChassisCode = New System.Windows.Forms.GroupBox()
      Me.lblChassisCode = New System.Windows.Forms.Label()
      Me.ChassisCodeTextBox = New System.Windows.Forms.TextBox()
      Me.gbPlatformID = New System.Windows.Forms.GroupBox()
      Me.lblPlatformID = New System.Windows.Forms.Label()
      Me.PlatformIDTextBox = New System.Windows.Forms.TextBox()
      Me.gbPlatformIDName = New System.Windows.Forms.GroupBox()
      Me.lblPlatformIDName = New System.Windows.Forms.Label()
      Me.PlatFormNameTextBox = New System.Windows.Forms.TextBox()
      Me.RadarSensorTestsBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
      Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
      Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
      Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
      Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
      Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
      Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
      Me.RadarSensorTestsBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
      Me.IDTextBox = New System.Windows.Forms.TextBox()
      Me.TbetConfigurationFileNameTextBox = New System.Windows.Forms.TextBox()
      Me.gbConfigurationFileName = New System.Windows.Forms.GroupBox()
      Me.lblConfigurationFileName = New System.Windows.Forms.Label()
      Me.RadarSensorTestsTableAdapter = New Fascia_Test_Stand.Test_InformationDataSet1TableAdapters.RadarSensorTestsTableAdapter()
      Me.TableAdapterManager = New Fascia_Test_Stand.Test_InformationDataSet1TableAdapters.TableAdapterManager()
      Me.gbSensorAddress3 = New System.Windows.Forms.GroupBox()
      Me.lblSensorAddress3 = New System.Windows.Forms.Label()
      Me.SensorAddress3TextBox = New System.Windows.Forms.TextBox()
      IDLabel1 = New System.Windows.Forms.Label()
      Me.gbTestName.SuspendLayout()
      CType(Me.RadarSensorTestsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.Test_InformationDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.gbTestDescription.SuspendLayout()
      Me.gbTestType.SuspendLayout()
      Me.gbSensorAddress1.SuspendLayout()
      Me.gbSensorAddress2.SuspendLayout()
      Me.gbUseMXbetdll.SuspendLayout()
      Me.gbChassisCode.SuspendLayout()
      Me.gbPlatformID.SuspendLayout()
      Me.gbPlatformIDName.SuspendLayout()
      CType(Me.RadarSensorTestsBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.RadarSensorTestsBindingNavigator.SuspendLayout()
      Me.gbConfigurationFileName.SuspendLayout()
      Me.gbSensorAddress3.SuspendLayout()
      Me.SuspendLayout()
      '
      'IDLabel1
      '
      IDLabel1.AutoSize = True
      IDLabel1.Location = New System.Drawing.Point(808, 487)
      IDLabel1.Name = "IDLabel1"
      IDLabel1.Size = New System.Drawing.Size(21, 13)
      IDLabel1.TabIndex = 50
      IDLabel1.Text = "ID:"
      IDLabel1.Visible = False
      '
      'Label2
      '
      Me.Label2.AutoSize = True
      Me.Label2.BackColor = System.Drawing.Color.Yellow
      Me.Label2.Location = New System.Drawing.Point(286, 504)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(425, 13)
      Me.Label2.TabIndex = 40
      Me.Label2.Text = "NOTE:You Must Click On The Save Icon Below To Save Any Changes You Have Made"
      '
      'gbTestName
      '
      Me.gbTestName.Controls.Add(Me.lblTestName)
      Me.gbTestName.Controls.Add(Me.TestNameTextBox)
      Me.gbTestName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbTestName.Location = New System.Drawing.Point(2, 6)
      Me.gbTestName.Name = "gbTestName"
      Me.gbTestName.Size = New System.Drawing.Size(476, 100)
      Me.gbTestName.TabIndex = 41
      Me.gbTestName.TabStop = False
      Me.gbTestName.Text = "Test Name"
      '
      'lblTestName
      '
      Me.lblTestName.AutoSize = True
      Me.lblTestName.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblTestName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTestName.Location = New System.Drawing.Point(3, 16)
      Me.lblTestName.Name = "lblTestName"
      Me.lblTestName.Size = New System.Drawing.Size(303, 52)
      Me.lblTestName.TabIndex = 5
      Me.lblTestName.Text = "The Test Name Must Be Formated As Follows:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ModelID_SensorType_FasciaType_BumperS" &
    "eries_Generation:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For Example:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "W205_DTR_Front_S_GEN1"
      '
      'TestNameTextBox
      '
      Me.TestNameTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.TestNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "TestName", True))
      Me.TestNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.TestNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.TestNameTextBox.Location = New System.Drawing.Point(3, 77)
      Me.TestNameTextBox.Name = "TestNameTextBox"
      Me.TestNameTextBox.Size = New System.Drawing.Size(470, 20)
      Me.TestNameTextBox.TabIndex = 53
      '
      'RadarSensorTestsBindingSource
      '
      Me.RadarSensorTestsBindingSource.DataMember = "RadarSensorTests"
      Me.RadarSensorTestsBindingSource.DataSource = Me.Test_InformationDataSet1
      '
      'Test_InformationDataSet1
      '
      Me.Test_InformationDataSet1.DataSetName = "Test_InformationDataSet1"
      Me.Test_InformationDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
      '
      'gbTestDescription
      '
      Me.gbTestDescription.Controls.Add(Me.lblTestDescription)
      Me.gbTestDescription.Controls.Add(Me.TestDescriptionTextBox)
      Me.gbTestDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbTestDescription.Location = New System.Drawing.Point(481, 6)
      Me.gbTestDescription.Name = "gbTestDescription"
      Me.gbTestDescription.Size = New System.Drawing.Size(547, 100)
      Me.gbTestDescription.TabIndex = 42
      Me.gbTestDescription.TabStop = False
      Me.gbTestDescription.Text = "Test Description"
      '
      'lblTestDescription
      '
      Me.lblTestDescription.AutoSize = True
      Me.lblTestDescription.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblTestDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTestDescription.Location = New System.Drawing.Point(3, 16)
      Me.lblTestDescription.Name = "lblTestDescription"
      Me.lblTestDescription.Size = New System.Drawing.Size(167, 39)
      Me.lblTestDescription.TabIndex = 5
      Me.lblTestDescription.Text = "Enter The Test Description" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Displayed In The Grid During Test" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "And Saved In Log F" &
    "iles" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
      '
      'TestDescriptionTextBox
      '
      Me.TestDescriptionTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.TestDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "TestDescription", True))
      Me.TestDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.TestDescriptionTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.TestDescriptionTextBox.Location = New System.Drawing.Point(3, 77)
      Me.TestDescriptionTextBox.Name = "TestDescriptionTextBox"
      Me.TestDescriptionTextBox.Size = New System.Drawing.Size(541, 20)
      Me.TestDescriptionTextBox.TabIndex = 55
      '
      'gbTestType
      '
      Me.gbTestType.Controls.Add(Me.lblTestType)
      Me.gbTestType.Controls.Add(Me.TestTypeTextBox)
      Me.gbTestType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbTestType.Location = New System.Drawing.Point(4, 106)
      Me.gbTestType.Name = "gbTestType"
      Me.gbTestType.Size = New System.Drawing.Size(226, 136)
      Me.gbTestType.TabIndex = 43
      Me.gbTestType.TabStop = False
      Me.gbTestType.Text = "Test Type"
      '
      'lblTestType
      '
      Me.lblTestType.AutoSize = True
      Me.lblTestType.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblTestType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTestType.Location = New System.Drawing.Point(3, 16)
      Me.lblTestType.Name = "lblTestType"
      Me.lblTestType.Size = New System.Drawing.Size(214, 91)
      Me.lblTestType.TabIndex = 5
      Me.lblTestType.Text = "Enter The Test Type:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Avalable Test Types Include The Following:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "BSM" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "iBSM" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DTR " &
    "(DP)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FCW" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "RCW"
      '
      'TestTypeTextBox
      '
      Me.TestTypeTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.TestTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "TestType", True))
      Me.TestTypeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.TestTypeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.TestTypeTextBox.Location = New System.Drawing.Point(3, 113)
      Me.TestTypeTextBox.Name = "TestTypeTextBox"
      Me.TestTypeTextBox.Size = New System.Drawing.Size(220, 20)
      Me.TestTypeTextBox.TabIndex = 57
      '
      'gbSensorAddress1
      '
      Me.gbSensorAddress1.Controls.Add(Me.lblSensorAddress1)
      Me.gbSensorAddress1.Controls.Add(Me.SensorAddress1TextBox)
      Me.gbSensorAddress1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbSensorAddress1.Location = New System.Drawing.Point(236, 106)
      Me.gbSensorAddress1.Name = "gbSensorAddress1"
      Me.gbSensorAddress1.Size = New System.Drawing.Size(242, 62)
      Me.gbSensorAddress1.TabIndex = 44
      Me.gbSensorAddress1.TabStop = False
      Me.gbSensorAddress1.Text = "Sensor Address 1"
      '
      'lblSensorAddress1
      '
      Me.lblSensorAddress1.AutoSize = True
      Me.lblSensorAddress1.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblSensorAddress1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblSensorAddress1.Location = New System.Drawing.Point(3, 16)
      Me.lblSensorAddress1.Name = "lblSensorAddress1"
      Me.lblSensorAddress1.Size = New System.Drawing.Size(153, 13)
      Me.lblSensorAddress1.TabIndex = 5
      Me.lblSensorAddress1.Text = "Enter The First Sensor Address"
      '
      'SensorAddress1TextBox
      '
      Me.SensorAddress1TextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.SensorAddress1TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "SensorAddress1", True))
      Me.SensorAddress1TextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.SensorAddress1TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.SensorAddress1TextBox.Location = New System.Drawing.Point(3, 39)
      Me.SensorAddress1TextBox.Name = "SensorAddress1TextBox"
      Me.SensorAddress1TextBox.Size = New System.Drawing.Size(236, 20)
      Me.SensorAddress1TextBox.TabIndex = 59
      '
      'gbSensorAddress2
      '
      Me.gbSensorAddress2.Controls.Add(Me.lblSensorAddress2)
      Me.gbSensorAddress2.Controls.Add(Me.SensorAddress2TextBox)
      Me.gbSensorAddress2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbSensorAddress2.Location = New System.Drawing.Point(234, 180)
      Me.gbSensorAddress2.Name = "gbSensorAddress2"
      Me.gbSensorAddress2.Size = New System.Drawing.Size(242, 62)
      Me.gbSensorAddress2.TabIndex = 45
      Me.gbSensorAddress2.TabStop = False
      Me.gbSensorAddress2.Text = "Sensor Address 2"
      '
      'lblSensorAddress2
      '
      Me.lblSensorAddress2.AutoSize = True
      Me.lblSensorAddress2.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblSensorAddress2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblSensorAddress2.Location = New System.Drawing.Point(3, 16)
      Me.lblSensorAddress2.Name = "lblSensorAddress2"
      Me.lblSensorAddress2.Size = New System.Drawing.Size(238, 13)
      Me.lblSensorAddress2.TabIndex = 5
      Me.lblSensorAddress2.Text = "Enter The Second Sensor Address (If Applicable)"
      '
      'SensorAddress2TextBox
      '
      Me.SensorAddress2TextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.SensorAddress2TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "SensorAddress2", True))
      Me.SensorAddress2TextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.SensorAddress2TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.SensorAddress2TextBox.Location = New System.Drawing.Point(3, 39)
      Me.SensorAddress2TextBox.Name = "SensorAddress2TextBox"
      Me.SensorAddress2TextBox.Size = New System.Drawing.Size(236, 20)
      Me.SensorAddress2TextBox.TabIndex = 61
      '
      'gbUseMXbetdll
      '
      Me.gbUseMXbetdll.Controls.Add(Me.lblUseMXbetdll)
      Me.gbUseMXbetdll.Controls.Add(Me.UseMXbetdllTextBox)
      Me.gbUseMXbetdll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbUseMXbetdll.Location = New System.Drawing.Point(481, 106)
      Me.gbUseMXbetdll.Name = "gbUseMXbetdll"
      Me.gbUseMXbetdll.Size = New System.Drawing.Size(226, 136)
      Me.gbUseMXbetdll.TabIndex = 46
      Me.gbUseMXbetdll.TabStop = False
      Me.gbUseMXbetdll.Text = "Which Dll To Use"
      '
      'lblUseMXbetdll
      '
      Me.lblUseMXbetdll.AutoSize = True
      Me.lblUseMXbetdll.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblUseMXbetdll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblUseMXbetdll.Location = New System.Drawing.Point(3, 16)
      Me.lblUseMXbetdll.Name = "lblUseMXbetdll"
      Me.lblUseMXbetdll.Size = New System.Drawing.Size(124, 39)
      Me.lblUseMXbetdll.TabIndex = 5
      Me.lblUseMXbetdll.Text = "Does The Sensor Use " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The Avalable Types Are " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "MXBET or MRBET" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
      '
      'UseMXbetdllTextBox
      '
      Me.UseMXbetdllTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.UseMXbetdllTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "AutolivdllToUse", True))
      Me.UseMXbetdllTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.UseMXbetdllTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.UseMXbetdllTextBox.Location = New System.Drawing.Point(3, 113)
      Me.UseMXbetdllTextBox.Name = "UseMXbetdllTextBox"
      Me.UseMXbetdllTextBox.Size = New System.Drawing.Size(220, 20)
      Me.UseMXbetdllTextBox.TabIndex = 63
      '
      'gbChassisCode
      '
      Me.gbChassisCode.Controls.Add(Me.lblChassisCode)
      Me.gbChassisCode.Controls.Add(Me.ChassisCodeTextBox)
      Me.gbChassisCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbChassisCode.Location = New System.Drawing.Point(710, 106)
      Me.gbChassisCode.Name = "gbChassisCode"
      Me.gbChassisCode.Size = New System.Drawing.Size(316, 136)
      Me.gbChassisCode.TabIndex = 47
      Me.gbChassisCode.TabStop = False
      Me.gbChassisCode.Text = "Chassis Code"
      '
      'lblChassisCode
      '
      Me.lblChassisCode.AutoSize = True
      Me.lblChassisCode.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblChassisCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblChassisCode.Location = New System.Drawing.Point(3, 16)
      Me.lblChassisCode.Name = "lblChassisCode"
      Me.lblChassisCode.Size = New System.Drawing.Size(266, 13)
      Me.lblChassisCode.TabIndex = 5
      Me.lblChassisCode.Text = "Enter The Chassis Code For This Sensor/Bumper Type"
      '
      'ChassisCodeTextBox
      '
      Me.ChassisCodeTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.ChassisCodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "ChassisCode", True))
      Me.ChassisCodeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.ChassisCodeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.ChassisCodeTextBox.Location = New System.Drawing.Point(3, 113)
      Me.ChassisCodeTextBox.Name = "ChassisCodeTextBox"
      Me.ChassisCodeTextBox.Size = New System.Drawing.Size(310, 20)
      Me.ChassisCodeTextBox.TabIndex = 65
      '
      'gbPlatformID
      '
      Me.gbPlatformID.Controls.Add(Me.lblPlatformID)
      Me.gbPlatformID.Controls.Add(Me.PlatformIDTextBox)
      Me.gbPlatformID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbPlatformID.Location = New System.Drawing.Point(4, 310)
      Me.gbPlatformID.Name = "gbPlatformID"
      Me.gbPlatformID.Size = New System.Drawing.Size(474, 70)
      Me.gbPlatformID.TabIndex = 48
      Me.gbPlatformID.TabStop = False
      Me.gbPlatformID.Text = "Platform ID"
      '
      'lblPlatformID
      '
      Me.lblPlatformID.AutoSize = True
      Me.lblPlatformID.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblPlatformID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblPlatformID.Location = New System.Drawing.Point(3, 16)
      Me.lblPlatformID.Name = "lblPlatformID"
      Me.lblPlatformID.Size = New System.Drawing.Size(254, 13)
      Me.lblPlatformID.TabIndex = 5
      Me.lblPlatformID.Text = "Enter The Platform ID For This Sensor/Bumper Type"
      '
      'PlatformIDTextBox
      '
      Me.PlatformIDTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.PlatformIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "PlatformID", True))
      Me.PlatformIDTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.PlatformIDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.PlatformIDTextBox.Location = New System.Drawing.Point(3, 47)
      Me.PlatformIDTextBox.Name = "PlatformIDTextBox"
      Me.PlatformIDTextBox.Size = New System.Drawing.Size(468, 20)
      Me.PlatformIDTextBox.TabIndex = 67
      '
      'gbPlatformIDName
      '
      Me.gbPlatformIDName.Controls.Add(Me.lblPlatformIDName)
      Me.gbPlatformIDName.Controls.Add(Me.PlatFormNameTextBox)
      Me.gbPlatformIDName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbPlatformIDName.Location = New System.Drawing.Point(484, 310)
      Me.gbPlatformIDName.Name = "gbPlatformIDName"
      Me.gbPlatformIDName.Size = New System.Drawing.Size(540, 70)
      Me.gbPlatformIDName.TabIndex = 49
      Me.gbPlatformIDName.TabStop = False
      Me.gbPlatformIDName.Text = "Platform Name"
      '
      'lblPlatformIDName
      '
      Me.lblPlatformIDName.AutoSize = True
      Me.lblPlatformIDName.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblPlatformIDName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblPlatformIDName.Location = New System.Drawing.Point(3, 16)
      Me.lblPlatformIDName.Name = "lblPlatformIDName"
      Me.lblPlatformIDName.Size = New System.Drawing.Size(271, 13)
      Me.lblPlatformIDName.TabIndex = 5
      Me.lblPlatformIDName.Text = "Enter The Platform Name For This Sensor/Bumper Type"
      '
      'PlatFormNameTextBox
      '
      Me.PlatFormNameTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.PlatFormNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "PlatFormName", True))
      Me.PlatFormNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.PlatFormNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.PlatFormNameTextBox.Location = New System.Drawing.Point(3, 47)
      Me.PlatFormNameTextBox.Name = "PlatFormNameTextBox"
      Me.PlatFormNameTextBox.Size = New System.Drawing.Size(534, 20)
      Me.PlatFormNameTextBox.TabIndex = 69
      '
      'RadarSensorTestsBindingNavigator
      '
      Me.RadarSensorTestsBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
      Me.RadarSensorTestsBindingNavigator.BindingSource = Me.RadarSensorTestsBindingSource
      Me.RadarSensorTestsBindingNavigator.CountItem = Me.BindingNavigatorCountItem
      Me.RadarSensorTestsBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
      Me.RadarSensorTestsBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.RadarSensorTestsBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.RadarSensorTestsBindingNavigatorSaveItem})
      Me.RadarSensorTestsBindingNavigator.Location = New System.Drawing.Point(0, 537)
      Me.RadarSensorTestsBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
      Me.RadarSensorTestsBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
      Me.RadarSensorTestsBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
      Me.RadarSensorTestsBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
      Me.RadarSensorTestsBindingNavigator.Name = "RadarSensorTestsBindingNavigator"
      Me.RadarSensorTestsBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
      Me.RadarSensorTestsBindingNavigator.Size = New System.Drawing.Size(1048, 25)
      Me.RadarSensorTestsBindingNavigator.TabIndex = 50
      Me.RadarSensorTestsBindingNavigator.Text = "BindingNavigator1"
      '
      'BindingNavigatorAddNewItem
      '
      Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
      Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorAddNewItem.Text = "Add new"
      '
      'BindingNavigatorCountItem
      '
      Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
      Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
      Me.BindingNavigatorCountItem.Text = "of {0}"
      Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
      '
      'BindingNavigatorDeleteItem
      '
      Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
      Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorDeleteItem.Text = "Delete"
      '
      'BindingNavigatorMoveFirstItem
      '
      Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
      Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorMoveFirstItem.Text = "Move first"
      '
      'BindingNavigatorMovePreviousItem
      '
      Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
      Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
      '
      'BindingNavigatorSeparator
      '
      Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
      Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
      '
      'BindingNavigatorPositionItem
      '
      Me.BindingNavigatorPositionItem.AccessibleName = "Position"
      Me.BindingNavigatorPositionItem.AutoSize = False
      Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
      Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
      Me.BindingNavigatorPositionItem.Text = "0"
      Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
      '
      'BindingNavigatorSeparator1
      '
      Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
      Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
      '
      'BindingNavigatorMoveNextItem
      '
      Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
      Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorMoveNextItem.Text = "Move next"
      '
      'BindingNavigatorMoveLastItem
      '
      Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
      Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
      Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
      Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
      Me.BindingNavigatorMoveLastItem.Text = "Move last"
      '
      'BindingNavigatorSeparator2
      '
      Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
      Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
      '
      'RadarSensorTestsBindingNavigatorSaveItem
      '
      Me.RadarSensorTestsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.RadarSensorTestsBindingNavigatorSaveItem.Image = CType(resources.GetObject("RadarSensorTestsBindingNavigatorSaveItem.Image"), System.Drawing.Image)
      Me.RadarSensorTestsBindingNavigatorSaveItem.Name = "RadarSensorTestsBindingNavigatorSaveItem"
      Me.RadarSensorTestsBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
      Me.RadarSensorTestsBindingNavigatorSaveItem.Text = "Save Data"
      '
      'IDTextBox
      '
      Me.IDTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "ID", True))
      Me.IDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.IDTextBox.Location = New System.Drawing.Point(904, 484)
      Me.IDTextBox.Name = "IDTextBox"
      Me.IDTextBox.Size = New System.Drawing.Size(100, 20)
      Me.IDTextBox.TabIndex = 51
      Me.IDTextBox.Visible = False
      '
      'TbetConfigurationFileNameTextBox
      '
      Me.TbetConfigurationFileNameTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.TbetConfigurationFileNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "TbetConfigurationFileName", True))
      Me.TbetConfigurationFileNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.TbetConfigurationFileNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.TbetConfigurationFileNameTextBox.Location = New System.Drawing.Point(3, 47)
      Me.TbetConfigurationFileNameTextBox.Name = "TbetConfigurationFileNameTextBox"
      Me.TbetConfigurationFileNameTextBox.Size = New System.Drawing.Size(1014, 20)
      Me.TbetConfigurationFileNameTextBox.TabIndex = 52
      '
      'gbConfigurationFileName
      '
      Me.gbConfigurationFileName.Controls.Add(Me.lblConfigurationFileName)
      Me.gbConfigurationFileName.Controls.Add(Me.TbetConfigurationFileNameTextBox)
      Me.gbConfigurationFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbConfigurationFileName.Location = New System.Drawing.Point(4, 386)
      Me.gbConfigurationFileName.Name = "gbConfigurationFileName"
      Me.gbConfigurationFileName.Size = New System.Drawing.Size(1020, 70)
      Me.gbConfigurationFileName.TabIndex = 53
      Me.gbConfigurationFileName.TabStop = False
      Me.gbConfigurationFileName.Text = "Configuration File Name"
      '
      'lblConfigurationFileName
      '
      Me.lblConfigurationFileName.AutoSize = True
      Me.lblConfigurationFileName.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblConfigurationFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblConfigurationFileName.Location = New System.Drawing.Point(3, 16)
      Me.lblConfigurationFileName.Name = "lblConfigurationFileName"
      Me.lblConfigurationFileName.Size = New System.Drawing.Size(323, 13)
      Me.lblConfigurationFileName.TabIndex = 5
      Me.lblConfigurationFileName.Text = "Enter The File Name To Use For This Platform ID And Sensor Type"
      '
      'RadarSensorTestsTableAdapter
      '
      Me.RadarSensorTestsTableAdapter.ClearBeforeFill = True
      '
      'TableAdapterManager
      '
      Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
      Me.TableAdapterManager.RadarSensorTestsTableAdapter = Me.RadarSensorTestsTableAdapter
      Me.TableAdapterManager.UpdateOrder = Fascia_Test_Stand.Test_InformationDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
      '
      'gbSensorAddress3
      '
      Me.gbSensorAddress3.Controls.Add(Me.lblSensorAddress3)
      Me.gbSensorAddress3.Controls.Add(Me.SensorAddress3TextBox)
      Me.gbSensorAddress3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.gbSensorAddress3.Location = New System.Drawing.Point(234, 248)
      Me.gbSensorAddress3.Name = "gbSensorAddress3"
      Me.gbSensorAddress3.Size = New System.Drawing.Size(242, 62)
      Me.gbSensorAddress3.TabIndex = 54
      Me.gbSensorAddress3.TabStop = False
      Me.gbSensorAddress3.Text = "Sensor Address 3"
      '
      'lblSensorAddress3
      '
      Me.lblSensorAddress3.AutoSize = True
      Me.lblSensorAddress3.Dock = System.Windows.Forms.DockStyle.Left
      Me.lblSensorAddress3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblSensorAddress3.Location = New System.Drawing.Point(3, 16)
      Me.lblSensorAddress3.Name = "lblSensorAddress3"
      Me.lblSensorAddress3.Size = New System.Drawing.Size(225, 13)
      Me.lblSensorAddress3.TabIndex = 5
      Me.lblSensorAddress3.Text = "Enter The Third Sensor Address (If Applicable)"
      '
      'SensorAddress3TextBox
      '
      Me.SensorAddress3TextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption
      Me.SensorAddress3TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RadarSensorTestsBindingSource, "SensorAddress3", True))
      Me.SensorAddress3TextBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.SensorAddress3TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.SensorAddress3TextBox.Location = New System.Drawing.Point(3, 39)
      Me.SensorAddress3TextBox.Name = "SensorAddress3TextBox"
      Me.SensorAddress3TextBox.Size = New System.Drawing.Size(236, 20)
      Me.SensorAddress3TextBox.TabIndex = 61
      '
      'frmSensorTestInformationDatabase
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1048, 562)
      Me.Controls.Add(Me.gbSensorAddress3)
      Me.Controls.Add(Me.gbConfigurationFileName)
      Me.Controls.Add(IDLabel1)
      Me.Controls.Add(Me.IDTextBox)
      Me.Controls.Add(Me.RadarSensorTestsBindingNavigator)
      Me.Controls.Add(Me.gbPlatformIDName)
      Me.Controls.Add(Me.gbPlatformID)
      Me.Controls.Add(Me.gbChassisCode)
      Me.Controls.Add(Me.gbUseMXbetdll)
      Me.Controls.Add(Me.gbSensorAddress2)
      Me.Controls.Add(Me.gbSensorAddress1)
      Me.Controls.Add(Me.gbTestType)
      Me.Controls.Add(Me.gbTestDescription)
      Me.Controls.Add(Me.gbTestName)
      Me.Controls.Add(Me.Label2)
      Me.Name = "frmSensorTestInformationDatabase"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "Sensor Test Information Database Editor"
      Me.gbTestName.ResumeLayout(False)
      Me.gbTestName.PerformLayout()
      CType(Me.RadarSensorTestsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.Test_InformationDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.gbTestDescription.ResumeLayout(False)
      Me.gbTestDescription.PerformLayout()
      Me.gbTestType.ResumeLayout(False)
      Me.gbTestType.PerformLayout()
      Me.gbSensorAddress1.ResumeLayout(False)
      Me.gbSensorAddress1.PerformLayout()
      Me.gbSensorAddress2.ResumeLayout(False)
      Me.gbSensorAddress2.PerformLayout()
      Me.gbUseMXbetdll.ResumeLayout(False)
      Me.gbUseMXbetdll.PerformLayout()
      Me.gbChassisCode.ResumeLayout(False)
      Me.gbChassisCode.PerformLayout()
      Me.gbPlatformID.ResumeLayout(False)
      Me.gbPlatformID.PerformLayout()
      Me.gbPlatformIDName.ResumeLayout(False)
      Me.gbPlatformIDName.PerformLayout()
      CType(Me.RadarSensorTestsBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
      Me.RadarSensorTestsBindingNavigator.ResumeLayout(False)
      Me.RadarSensorTestsBindingNavigator.PerformLayout()
      Me.gbConfigurationFileName.ResumeLayout(False)
      Me.gbConfigurationFileName.PerformLayout()
      Me.gbSensorAddress3.ResumeLayout(False)
      Me.gbSensorAddress3.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents gbTestName As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestName As System.Windows.Forms.Label
   Friend WithEvents gbTestDescription As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestDescription As System.Windows.Forms.Label
   Friend WithEvents gbTestType As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestType As System.Windows.Forms.Label
   Friend WithEvents gbSensorAddress1 As System.Windows.Forms.GroupBox
   Friend WithEvents lblSensorAddress1 As System.Windows.Forms.Label
   Friend WithEvents gbSensorAddress2 As System.Windows.Forms.GroupBox
   Friend WithEvents lblSensorAddress2 As System.Windows.Forms.Label
   Friend WithEvents gbUseMXbetdll As System.Windows.Forms.GroupBox
   Friend WithEvents lblUseMXbetdll As System.Windows.Forms.Label
   Friend WithEvents gbChassisCode As System.Windows.Forms.GroupBox
   Friend WithEvents lblChassisCode As System.Windows.Forms.Label
   Friend WithEvents gbPlatformID As System.Windows.Forms.GroupBox
   Friend WithEvents lblPlatformID As System.Windows.Forms.Label
   Friend WithEvents gbPlatformIDName As System.Windows.Forms.GroupBox
   Friend WithEvents lblPlatformIDName As System.Windows.Forms.Label
   Friend WithEvents Test_InformationDataSet1 As Fascia_Test_Stand.Test_InformationDataSet1
   Friend WithEvents RadarSensorTestsBindingSource As System.Windows.Forms.BindingSource
   Friend WithEvents RadarSensorTestsTableAdapter As Fascia_Test_Stand.Test_InformationDataSet1TableAdapters.RadarSensorTestsTableAdapter
   Friend WithEvents TableAdapterManager As Fascia_Test_Stand.Test_InformationDataSet1TableAdapters.TableAdapterManager
   Friend WithEvents RadarSensorTestsBindingNavigator As System.Windows.Forms.BindingNavigator
   Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
   Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
   Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents RadarSensorTestsBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestNameTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestDescriptionTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestTypeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents SensorAddress1TextBox As System.Windows.Forms.TextBox
   Friend WithEvents SensorAddress2TextBox As System.Windows.Forms.TextBox
   Friend WithEvents UseMXbetdllTextBox As System.Windows.Forms.TextBox
   Friend WithEvents ChassisCodeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents PlatformIDTextBox As System.Windows.Forms.TextBox
   Friend WithEvents PlatFormNameTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TbetConfigurationFileNameTextBox As System.Windows.Forms.TextBox
   Friend WithEvents gbConfigurationFileName As System.Windows.Forms.GroupBox
   Friend WithEvents lblConfigurationFileName As System.Windows.Forms.Label
   Friend WithEvents gbSensorAddress3 As System.Windows.Forms.GroupBox
   Friend WithEvents lblSensorAddress3 As System.Windows.Forms.Label
   Friend WithEvents SensorAddress3TextBox As System.Windows.Forms.TextBox
End Class
