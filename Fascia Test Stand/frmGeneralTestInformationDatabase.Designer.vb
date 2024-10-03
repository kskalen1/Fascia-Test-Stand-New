<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralTestInformationDatabase
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
        Dim IDLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeneralTestInformationDatabase))
        Me.GeneralTestsBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.GeneralTestsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Test_InformationDataSet = New Fascia_Test_Stand.Test_InformationDataSet()
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
        Me.GeneralTestsBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.IDTextBox = New System.Windows.Forms.TextBox()
        Me.TestNameTextBox = New System.Windows.Forms.TextBox()
        Me.TestDescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.TestTypeTextBox = New System.Windows.Forms.TextBox()
        Me.MaxLimitTextBox = New System.Windows.Forms.TextBox()
        Me.TestUnitTextBox = New System.Windows.Forms.TextBox()
        Me.MeasurementTypeTextBox = New System.Windows.Forms.TextBox()
        Me.MeterRangeTextBox = New System.Windows.Forms.TextBox()
        Me.DelayBeforeMeasurementTextBox = New System.Windows.Forms.TextBox()
        Me.RelayNumberForLowSideConnectTextBox = New System.Windows.Forms.TextBox()
        Me.RelayNumberForHighSideConnectTextBox = New System.Windows.Forms.TextBox()
        Me.NumberOfSensorsTextBox = New System.Windows.Forms.TextBox()
        Me.gbTestName = New System.Windows.Forms.GroupBox()
        Me.lblTestName = New System.Windows.Forms.Label()
        Me.gbTestDescription = New System.Windows.Forms.GroupBox()
        Me.lblTestDescription = New System.Windows.Forms.Label()
        Me.gbTestType = New System.Windows.Forms.GroupBox()
        Me.lblTestType = New System.Windows.Forms.Label()
        Me.gbMinimumLimit = New System.Windows.Forms.GroupBox()
        Me.lblMinimumLimit = New System.Windows.Forms.Label()
        Me.MinLimitTextBox = New System.Windows.Forms.TextBox()
        Me.gbMaximumLimit = New System.Windows.Forms.GroupBox()
        Me.lblMaximumLimit = New System.Windows.Forms.Label()
        Me.gbTestUnit = New System.Windows.Forms.GroupBox()
        Me.lblTestUnit = New System.Windows.Forms.Label()
        Me.gbMeasurementType = New System.Windows.Forms.GroupBox()
        Me.lblMeasurementType = New System.Windows.Forms.Label()
        Me.gbMeterRange = New System.Windows.Forms.GroupBox()
        Me.lblMeterRange = New System.Windows.Forms.Label()
        Me.gbDelayBeforeMeasurement = New System.Windows.Forms.GroupBox()
        Me.lblDelayBeforeMeasurement = New System.Windows.Forms.Label()
        Me.gbNumberOfPTSSensors = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbRelayNumberLowSideConnect = New System.Windows.Forms.GroupBox()
        Me.lblRelayNumberForLowSideConnect = New System.Windows.Forms.Label()
        Me.gbRelayNumberHighSideConnect = New System.Windows.Forms.GroupBox()
        Me.lblRelayNumberForHighSideConnect = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GeneralTestsTableAdapter = New Fascia_Test_Stand.Test_InformationDataSetTableAdapters.GeneralTestsTableAdapter()
        Me.GeneralTestTableAdapterManager = New Fascia_Test_Stand.Test_InformationDataSetTableAdapters.TableAdapterManager()
        IDLabel = New System.Windows.Forms.Label()
        CType(Me.GeneralTestsBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GeneralTestsBindingNavigator.SuspendLayout()
        CType(Me.GeneralTestsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Test_InformationDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTestName.SuspendLayout()
        Me.gbTestDescription.SuspendLayout()
        Me.gbTestType.SuspendLayout()
        Me.gbMinimumLimit.SuspendLayout()
        Me.gbMaximumLimit.SuspendLayout()
        Me.gbTestUnit.SuspendLayout()
        Me.gbMeasurementType.SuspendLayout()
        Me.gbMeterRange.SuspendLayout()
        Me.gbDelayBeforeMeasurement.SuspendLayout()
        Me.gbNumberOfPTSSensors.SuspendLayout()
        Me.gbRelayNumberLowSideConnect.SuspendLayout()
        Me.gbRelayNumberHighSideConnect.SuspendLayout()
        Me.SuspendLayout()
        '
        'IDLabel
        '
        IDLabel.AutoSize = True
        IDLabel.Location = New System.Drawing.Point(562, 467)
        IDLabel.Name = "IDLabel"
        IDLabel.Size = New System.Drawing.Size(21, 13)
        IDLabel.TabIndex = 1
        IDLabel.Text = "ID:"
        IDLabel.Visible = False
        '
        'GeneralTestsBindingNavigator
        '
        Me.GeneralTestsBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.GeneralTestsBindingNavigator.BindingSource = Me.GeneralTestsBindingSource
        Me.GeneralTestsBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.GeneralTestsBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.GeneralTestsBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GeneralTestsBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.GeneralTestsBindingNavigatorSaveItem})
        Me.GeneralTestsBindingNavigator.Location = New System.Drawing.Point(0, 516)
        Me.GeneralTestsBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.GeneralTestsBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.GeneralTestsBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.GeneralTestsBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.GeneralTestsBindingNavigator.Name = "GeneralTestsBindingNavigator"
        Me.GeneralTestsBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.GeneralTestsBindingNavigator.Size = New System.Drawing.Size(1030, 25)
        Me.GeneralTestsBindingNavigator.TabIndex = 0
        Me.GeneralTestsBindingNavigator.Text = "BindingNavigator1"
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
        'GeneralTestsBindingSource
        '
        Me.GeneralTestsBindingSource.DataMember = "GeneralTests"
        Me.GeneralTestsBindingSource.DataSource = Me.Test_InformationDataSet
        '
        'Test_InformationDataSet
        '
        Me.Test_InformationDataSet.DataSetName = "Test_InformationDataSet"
        Me.Test_InformationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
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
        'GeneralTestsBindingNavigatorSaveItem
        '
        Me.GeneralTestsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.GeneralTestsBindingNavigatorSaveItem.Image = CType(resources.GetObject("GeneralTestsBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.GeneralTestsBindingNavigatorSaveItem.Name = "GeneralTestsBindingNavigatorSaveItem"
        Me.GeneralTestsBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.GeneralTestsBindingNavigatorSaveItem.Text = "Save Data"
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "ID", True))
        Me.IDTextBox.Location = New System.Drawing.Point(584, 464)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.Size = New System.Drawing.Size(442, 20)
        Me.IDTextBox.TabIndex = 2
        Me.IDTextBox.Visible = False
        '
        'TestNameTextBox
        '
        Me.TestNameTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TestNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "TestName", True))
        Me.TestNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TestNameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestNameTextBox.Location = New System.Drawing.Point(3, 77)
        Me.TestNameTextBox.Name = "TestNameTextBox"
        Me.TestNameTextBox.Size = New System.Drawing.Size(450, 20)
        Me.TestNameTextBox.TabIndex = 4
        '
        'TestDescriptionTextBox
        '
        Me.TestDescriptionTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TestDescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "TestDescription", True))
        Me.TestDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TestDescriptionTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestDescriptionTextBox.Location = New System.Drawing.Point(3, 77)
        Me.TestDescriptionTextBox.Name = "TestDescriptionTextBox"
        Me.TestDescriptionTextBox.Size = New System.Drawing.Size(514, 20)
        Me.TestDescriptionTextBox.TabIndex = 6
        '
        'TestTypeTextBox
        '
        Me.TestTypeTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TestTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "TestType", True))
        Me.TestTypeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TestTypeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestTypeTextBox.Location = New System.Drawing.Point(3, 203)
        Me.TestTypeTextBox.Name = "TestTypeTextBox"
        Me.TestTypeTextBox.Size = New System.Drawing.Size(220, 20)
        Me.TestTypeTextBox.TabIndex = 8
        '
        'MaxLimitTextBox
        '
        Me.MaxLimitTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MaxLimitTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "MaxLimit", True))
        Me.MaxLimitTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MaxLimitTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaxLimitTextBox.Location = New System.Drawing.Point(3, 39)
        Me.MaxLimitTextBox.Name = "MaxLimitTextBox"
        Me.MaxLimitTextBox.Size = New System.Drawing.Size(220, 20)
        Me.MaxLimitTextBox.TabIndex = 12
        '
        'TestUnitTextBox
        '
        Me.TestUnitTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TestUnitTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "TestUnit", True))
        Me.TestUnitTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TestUnitTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestUnitTextBox.Location = New System.Drawing.Point(3, 71)
        Me.TestUnitTextBox.Name = "TestUnitTextBox"
        Me.TestUnitTextBox.Size = New System.Drawing.Size(220, 20)
        Me.TestUnitTextBox.TabIndex = 14
        '
        'MeasurementTypeTextBox
        '
        Me.MeasurementTypeTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MeasurementTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "MeasurementType", True))
        Me.MeasurementTypeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MeasurementTypeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MeasurementTypeTextBox.Location = New System.Drawing.Point(3, 71)
        Me.MeasurementTypeTextBox.Name = "MeasurementTypeTextBox"
        Me.MeasurementTypeTextBox.Size = New System.Drawing.Size(250, 20)
        Me.MeasurementTypeTextBox.TabIndex = 16
        '
        'MeterRangeTextBox
        '
        Me.MeterRangeTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MeterRangeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "MeterRange", True))
        Me.MeterRangeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MeterRangeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MeterRangeTextBox.Location = New System.Drawing.Point(3, 71)
        Me.MeterRangeTextBox.Name = "MeterRangeTextBox"
        Me.MeterRangeTextBox.Size = New System.Drawing.Size(252, 20)
        Me.MeterRangeTextBox.TabIndex = 18
        '
        'DelayBeforeMeasurementTextBox
        '
        Me.DelayBeforeMeasurementTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.DelayBeforeMeasurementTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "DelayBeforeMeasurement", True))
        Me.DelayBeforeMeasurementTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DelayBeforeMeasurementTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DelayBeforeMeasurementTextBox.Location = New System.Drawing.Point(3, 105)
        Me.DelayBeforeMeasurementTextBox.Name = "DelayBeforeMeasurementTextBox"
        Me.DelayBeforeMeasurementTextBox.Size = New System.Drawing.Size(250, 20)
        Me.DelayBeforeMeasurementTextBox.TabIndex = 20
        '
        'RelayNumberForLowSideConnectTextBox
        '
        Me.RelayNumberForLowSideConnectTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.RelayNumberForLowSideConnectTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "RelayNumberForLowSideConnect", True))
        Me.RelayNumberForLowSideConnectTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RelayNumberForLowSideConnectTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RelayNumberForLowSideConnectTextBox.Location = New System.Drawing.Point(3, 39)
        Me.RelayNumberForLowSideConnectTextBox.Name = "RelayNumberForLowSideConnectTextBox"
        Me.RelayNumberForLowSideConnectTextBox.Size = New System.Drawing.Size(290, 20)
        Me.RelayNumberForLowSideConnectTextBox.TabIndex = 22
        '
        'RelayNumberForHighSideConnectTextBox
        '
        Me.RelayNumberForHighSideConnectTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.RelayNumberForHighSideConnectTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "RelayNumberForHighSideConnect", True))
        Me.RelayNumberForHighSideConnectTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RelayNumberForHighSideConnectTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RelayNumberForHighSideConnectTextBox.Location = New System.Drawing.Point(3, 39)
        Me.RelayNumberForHighSideConnectTextBox.Name = "RelayNumberForHighSideConnectTextBox"
        Me.RelayNumberForHighSideConnectTextBox.Size = New System.Drawing.Size(290, 20)
        Me.RelayNumberForHighSideConnectTextBox.TabIndex = 24
        '
        'NumberOfSensorsTextBox
        '
        Me.NumberOfSensorsTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.NumberOfSensorsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "NumberOfSensors", True))
        Me.NumberOfSensorsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.NumberOfSensorsTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumberOfSensorsTextBox.Location = New System.Drawing.Point(3, 105)
        Me.NumberOfSensorsTextBox.Name = "NumberOfSensorsTextBox"
        Me.NumberOfSensorsTextBox.Size = New System.Drawing.Size(252, 20)
        Me.NumberOfSensorsTextBox.TabIndex = 26
        '
        'gbTestName
        '
        Me.gbTestName.Controls.Add(Me.lblTestName)
        Me.gbTestName.Controls.Add(Me.TestNameTextBox)
        Me.gbTestName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTestName.Location = New System.Drawing.Point(44, 14)
        Me.gbTestName.Name = "gbTestName"
        Me.gbTestName.Size = New System.Drawing.Size(456, 100)
        Me.gbTestName.TabIndex = 27
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
        Me.lblTestName.Size = New System.Drawing.Size(232, 52)
        Me.lblTestName.TabIndex = 5
        Me.lblTestName.Text = "The Test Name Must Be Formated As Follows:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ModelID_FasciaType_SensorType_Generat" &
    "ion:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For Example:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "X294_Front_PTS_GEN1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'gbTestDescription
        '
        Me.gbTestDescription.Controls.Add(Me.lblTestDescription)
        Me.gbTestDescription.Controls.Add(Me.TestDescriptionTextBox)
        Me.gbTestDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTestDescription.Location = New System.Drawing.Point(506, 14)
        Me.gbTestDescription.Name = "gbTestDescription"
        Me.gbTestDescription.Size = New System.Drawing.Size(520, 100)
        Me.gbTestDescription.TabIndex = 28
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
        'gbTestType
        '
        Me.gbTestType.Controls.Add(Me.lblTestType)
        Me.gbTestType.Controls.Add(Me.TestTypeTextBox)
        Me.gbTestType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTestType.Location = New System.Drawing.Point(44, 120)
        Me.gbTestType.Name = "gbTestType"
        Me.gbTestType.Size = New System.Drawing.Size(226, 226)
        Me.gbTestType.TabIndex = 29
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
        Me.lblTestType.Size = New System.Drawing.Size(214, 52)
        Me.lblTestType.TabIndex = 5
        Me.lblTestType.Text = "Enter The Test Type:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Avalable Test Types Include The Following:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PTS"
        '
        'gbMinimumLimit
        '
        Me.gbMinimumLimit.Controls.Add(Me.lblMinimumLimit)
        Me.gbMinimumLimit.Controls.Add(Me.MinLimitTextBox)
        Me.gbMinimumLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMinimumLimit.Location = New System.Drawing.Point(274, 120)
        Me.gbMinimumLimit.Name = "gbMinimumLimit"
        Me.gbMinimumLimit.Size = New System.Drawing.Size(226, 62)
        Me.gbMinimumLimit.TabIndex = 30
        Me.gbMinimumLimit.TabStop = False
        Me.gbMinimumLimit.Text = "Test Minimum Limit"
        '
        'lblMinimumLimit
        '
        Me.lblMinimumLimit.AutoSize = True
        Me.lblMinimumLimit.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMinimumLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinimumLimit.Location = New System.Drawing.Point(3, 16)
        Me.lblMinimumLimit.Name = "lblMinimumLimit"
        Me.lblMinimumLimit.Size = New System.Drawing.Size(187, 13)
        Me.lblMinimumLimit.TabIndex = 5
        Me.lblMinimumLimit.Text = "Enter The Minimum Limit For This Test"
        '
        'MinLimitTextBox
        '
        Me.MinLimitTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MinLimitTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GeneralTestsBindingSource, "MinLimit", True))
        Me.MinLimitTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MinLimitTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinLimitTextBox.Location = New System.Drawing.Point(3, 39)
        Me.MinLimitTextBox.Name = "MinLimitTextBox"
        Me.MinLimitTextBox.Size = New System.Drawing.Size(220, 20)
        Me.MinLimitTextBox.TabIndex = 10
        '
        'gbMaximumLimit
        '
        Me.gbMaximumLimit.Controls.Add(Me.lblMaximumLimit)
        Me.gbMaximumLimit.Controls.Add(Me.MaxLimitTextBox)
        Me.gbMaximumLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMaximumLimit.Location = New System.Drawing.Point(274, 186)
        Me.gbMaximumLimit.Name = "gbMaximumLimit"
        Me.gbMaximumLimit.Size = New System.Drawing.Size(226, 62)
        Me.gbMaximumLimit.TabIndex = 31
        Me.gbMaximumLimit.TabStop = False
        Me.gbMaximumLimit.Text = "Test Maximum Limit"
        '
        'lblMaximumLimit
        '
        Me.lblMaximumLimit.AutoSize = True
        Me.lblMaximumLimit.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMaximumLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaximumLimit.Location = New System.Drawing.Point(3, 16)
        Me.lblMaximumLimit.Name = "lblMaximumLimit"
        Me.lblMaximumLimit.Size = New System.Drawing.Size(190, 13)
        Me.lblMaximumLimit.TabIndex = 5
        Me.lblMaximumLimit.Text = "Enter The Maximum Limit For This Test"
        '
        'gbTestUnit
        '
        Me.gbTestUnit.Controls.Add(Me.lblTestUnit)
        Me.gbTestUnit.Controls.Add(Me.TestUnitTextBox)
        Me.gbTestUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTestUnit.Location = New System.Drawing.Point(274, 252)
        Me.gbTestUnit.Name = "gbTestUnit"
        Me.gbTestUnit.Size = New System.Drawing.Size(226, 94)
        Me.gbTestUnit.TabIndex = 32
        Me.gbTestUnit.TabStop = False
        Me.gbTestUnit.Text = "Test Unit"
        '
        'lblTestUnit
        '
        Me.lblTestUnit.AutoSize = True
        Me.lblTestUnit.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTestUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestUnit.Location = New System.Drawing.Point(3, 16)
        Me.lblTestUnit.Name = "lblTestUnit"
        Me.lblTestUnit.Size = New System.Drawing.Size(222, 39)
        Me.lblTestUnit.TabIndex = 5
        Me.lblTestUnit.Text = "Enter The Unit Of Measurement For This Test" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For Example:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Volts    (Can Leave Bl" &
    "ank)"
        '
        'gbMeasurementType
        '
        Me.gbMeasurementType.Controls.Add(Me.MeasurementTypeTextBox)
        Me.gbMeasurementType.Controls.Add(Me.lblMeasurementType)
        Me.gbMeasurementType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMeasurementType.Location = New System.Drawing.Point(506, 120)
        Me.gbMeasurementType.Name = "gbMeasurementType"
        Me.gbMeasurementType.Size = New System.Drawing.Size(256, 94)
        Me.gbMeasurementType.TabIndex = 33
        Me.gbMeasurementType.TabStop = False
        Me.gbMeasurementType.Text = "Measurement Type"
        '
        'lblMeasurementType
        '
        Me.lblMeasurementType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMeasurementType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeasurementType.Location = New System.Drawing.Point(3, 16)
        Me.lblMeasurementType.Name = "lblMeasurementType"
        Me.lblMeasurementType.Size = New System.Drawing.Size(250, 75)
        Me.lblMeasurementType.TabIndex = 5
        Me.lblMeasurementType.Text = "Enter The Meter Measurement Type For This Test" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Avalable Types:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "OHMS,VDC,ADC,FRE" &
    "Q,CONT,DIODE,VAC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'gbMeterRange
        '
        Me.gbMeterRange.Controls.Add(Me.MeterRangeTextBox)
        Me.gbMeterRange.Controls.Add(Me.lblMeterRange)
        Me.gbMeterRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMeterRange.Location = New System.Drawing.Point(766, 120)
        Me.gbMeterRange.Name = "gbMeterRange"
        Me.gbMeterRange.Size = New System.Drawing.Size(258, 94)
        Me.gbMeterRange.TabIndex = 34
        Me.gbMeterRange.TabStop = False
        Me.gbMeterRange.Text = "Meter Range"
        '
        'lblMeterRange
        '
        Me.lblMeterRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMeterRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterRange.Location = New System.Drawing.Point(3, 16)
        Me.lblMeterRange.Name = "lblMeterRange"
        Me.lblMeterRange.Size = New System.Drawing.Size(252, 75)
        Me.lblMeterRange.TabIndex = 5
        Me.lblMeterRange.Text = "Enter The Meter Range For This Test" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Avalable Ranges" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0(Auto Range) to 6:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'gbDelayBeforeMeasurement
        '
        Me.gbDelayBeforeMeasurement.Controls.Add(Me.DelayBeforeMeasurementTextBox)
        Me.gbDelayBeforeMeasurement.Controls.Add(Me.lblDelayBeforeMeasurement)
        Me.gbDelayBeforeMeasurement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbDelayBeforeMeasurement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gbDelayBeforeMeasurement.Location = New System.Drawing.Point(506, 218)
        Me.gbDelayBeforeMeasurement.Name = "gbDelayBeforeMeasurement"
        Me.gbDelayBeforeMeasurement.Size = New System.Drawing.Size(256, 128)
        Me.gbDelayBeforeMeasurement.TabIndex = 35
        Me.gbDelayBeforeMeasurement.TabStop = False
        Me.gbDelayBeforeMeasurement.Text = "Delay Before Measurment"
        '
        'lblDelayBeforeMeasurement
        '
        Me.lblDelayBeforeMeasurement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDelayBeforeMeasurement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDelayBeforeMeasurement.Location = New System.Drawing.Point(3, 16)
        Me.lblDelayBeforeMeasurement.Name = "lblDelayBeforeMeasurement"
        Me.lblDelayBeforeMeasurement.Size = New System.Drawing.Size(250, 109)
        Me.lblDelayBeforeMeasurement.TabIndex = 5
        Me.lblDelayBeforeMeasurement.Text = "Enter The Amount Of Time You Wish To Delay" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Before The Final Measurement Is Taken" &
    " " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(milli seconds)"
        '
        'gbNumberOfPTSSensors
        '
        Me.gbNumberOfPTSSensors.Controls.Add(Me.NumberOfSensorsTextBox)
        Me.gbNumberOfPTSSensors.Controls.Add(Me.Label1)
        Me.gbNumberOfPTSSensors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbNumberOfPTSSensors.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gbNumberOfPTSSensors.Location = New System.Drawing.Point(766, 218)
        Me.gbNumberOfPTSSensors.Name = "gbNumberOfPTSSensors"
        Me.gbNumberOfPTSSensors.Size = New System.Drawing.Size(258, 128)
        Me.gbNumberOfPTSSensors.TabIndex = 36
        Me.gbNumberOfPTSSensors.TabStop = False
        Me.gbNumberOfPTSSensors.Text = "Enter The Number Of Sensors"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(252, 109)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Only Used For PTS Tests" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please specify one of the following" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2,4,6"
        '
        'gbRelayNumberLowSideConnect
        '
        Me.gbRelayNumberLowSideConnect.Controls.Add(Me.lblRelayNumberForLowSideConnect)
        Me.gbRelayNumberLowSideConnect.Controls.Add(Me.RelayNumberForLowSideConnectTextBox)
        Me.gbRelayNumberLowSideConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbRelayNumberLowSideConnect.Location = New System.Drawing.Point(44, 356)
        Me.gbRelayNumberLowSideConnect.Name = "gbRelayNumberLowSideConnect"
        Me.gbRelayNumberLowSideConnect.Size = New System.Drawing.Size(296, 62)
        Me.gbRelayNumberLowSideConnect.TabIndex = 37
        Me.gbRelayNumberLowSideConnect.TabStop = False
        Me.gbRelayNumberLowSideConnect.Text = "Relay Number For Connection"
        '
        'lblRelayNumberForLowSideConnect
        '
        Me.lblRelayNumberForLowSideConnect.AutoSize = True
        Me.lblRelayNumberForLowSideConnect.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRelayNumberForLowSideConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelayNumberForLowSideConnect.Location = New System.Drawing.Point(3, 16)
        Me.lblRelayNumberForLowSideConnect.Name = "lblRelayNumberForLowSideConnect"
        Me.lblRelayNumberForLowSideConnect.Size = New System.Drawing.Size(210, 13)
        Me.lblRelayNumberForLowSideConnect.TabIndex = 5
        Me.lblRelayNumberForLowSideConnect.Text = "Relay Number For Connecting The  Sensor"
        '
        'gbRelayNumberHighSideConnect
        '
        Me.gbRelayNumberHighSideConnect.Controls.Add(Me.lblRelayNumberForHighSideConnect)
        Me.gbRelayNumberHighSideConnect.Controls.Add(Me.RelayNumberForHighSideConnectTextBox)
        Me.gbRelayNumberHighSideConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbRelayNumberHighSideConnect.Location = New System.Drawing.Point(44, 426)
        Me.gbRelayNumberHighSideConnect.Name = "gbRelayNumberHighSideConnect"
        Me.gbRelayNumberHighSideConnect.Size = New System.Drawing.Size(296, 62)
        Me.gbRelayNumberHighSideConnect.TabIndex = 38
        Me.gbRelayNumberHighSideConnect.TabStop = False
        Me.gbRelayNumberHighSideConnect.Text = "Relay Number For High Side Connect"
        Me.gbRelayNumberHighSideConnect.Visible = False
        '
        'lblRelayNumberForHighSideConnect
        '
        Me.lblRelayNumberForHighSideConnect.AutoSize = True
        Me.lblRelayNumberForHighSideConnect.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRelayNumberForHighSideConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelayNumberForHighSideConnect.Location = New System.Drawing.Point(3, 16)
        Me.lblRelayNumberForHighSideConnect.Name = "lblRelayNumberForHighSideConnect"
        Me.lblRelayNumberForHighSideConnect.Size = New System.Drawing.Size(280, 13)
        Me.lblRelayNumberForHighSideConnect.TabIndex = 5
        Me.lblRelayNumberForHighSideConnect.Text = "Relay Number For Connecting The High Side Of A Sensor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Yellow
        Me.Label2.Location = New System.Drawing.Point(254, 500)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(425, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "NOTE:You Must Click On The Save Icon Below To Save Any Changes You Have Made"
        '
        'GeneralTestsTableAdapter
        '
        Me.GeneralTestsTableAdapter.ClearBeforeFill = True
        '
        'GeneralTestTableAdapterManager
        '
        Me.GeneralTestTableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.GeneralTestTableAdapterManager.GeneralTestsTableAdapter = Me.GeneralTestsTableAdapter
        Me.GeneralTestTableAdapterManager.UpdateOrder = Fascia_Test_Stand.Test_InformationDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'frmGeneralTestInformationDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 541)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.gbRelayNumberHighSideConnect)
        Me.Controls.Add(Me.gbRelayNumberLowSideConnect)
        Me.Controls.Add(Me.gbNumberOfPTSSensors)
        Me.Controls.Add(Me.gbDelayBeforeMeasurement)
        Me.Controls.Add(Me.gbMeterRange)
        Me.Controls.Add(Me.gbMeasurementType)
        Me.Controls.Add(Me.gbTestUnit)
        Me.Controls.Add(Me.gbMaximumLimit)
        Me.Controls.Add(Me.gbMinimumLimit)
        Me.Controls.Add(Me.gbTestType)
        Me.Controls.Add(Me.gbTestDescription)
        Me.Controls.Add(Me.gbTestName)
        Me.Controls.Add(IDLabel)
        Me.Controls.Add(Me.IDTextBox)
        Me.Controls.Add(Me.GeneralTestsBindingNavigator)
        Me.Name = "frmGeneralTestInformationDatabase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "General Test Information Database Editor"
        CType(Me.GeneralTestsBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GeneralTestsBindingNavigator.ResumeLayout(False)
        Me.GeneralTestsBindingNavigator.PerformLayout()
        CType(Me.GeneralTestsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Test_InformationDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTestName.ResumeLayout(False)
        Me.gbTestName.PerformLayout()
        Me.gbTestDescription.ResumeLayout(False)
        Me.gbTestDescription.PerformLayout()
        Me.gbTestType.ResumeLayout(False)
        Me.gbTestType.PerformLayout()
        Me.gbMinimumLimit.ResumeLayout(False)
        Me.gbMinimumLimit.PerformLayout()
        Me.gbMaximumLimit.ResumeLayout(False)
        Me.gbMaximumLimit.PerformLayout()
        Me.gbTestUnit.ResumeLayout(False)
        Me.gbTestUnit.PerformLayout()
        Me.gbMeasurementType.ResumeLayout(False)
        Me.gbMeasurementType.PerformLayout()
        Me.gbMeterRange.ResumeLayout(False)
        Me.gbMeterRange.PerformLayout()
        Me.gbDelayBeforeMeasurement.ResumeLayout(False)
        Me.gbDelayBeforeMeasurement.PerformLayout()
        Me.gbNumberOfPTSSensors.ResumeLayout(False)
        Me.gbNumberOfPTSSensors.PerformLayout()
        Me.gbRelayNumberLowSideConnect.ResumeLayout(False)
        Me.gbRelayNumberLowSideConnect.PerformLayout()
        Me.gbRelayNumberHighSideConnect.ResumeLayout(False)
        Me.gbRelayNumberHighSideConnect.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Test_InformationDataSet As Fascia_Test_Stand.Test_InformationDataSet
   Friend WithEvents GeneralTestsBindingSource As System.Windows.Forms.BindingSource
   Friend WithEvents GeneralTestsTableAdapter As Fascia_Test_Stand.Test_InformationDataSetTableAdapters.GeneralTestsTableAdapter
   Friend WithEvents GeneralTestTableAdapterManager As Fascia_Test_Stand.Test_InformationDataSetTableAdapters.TableAdapterManager
   Friend WithEvents GeneralTestsBindingNavigator As System.Windows.Forms.BindingNavigator
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
   Friend WithEvents GeneralTestsBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestNameTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestDescriptionTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestTypeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents MaxLimitTextBox As System.Windows.Forms.TextBox
   Friend WithEvents TestUnitTextBox As System.Windows.Forms.TextBox
   Friend WithEvents MeasurementTypeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents MeterRangeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents DelayBeforeMeasurementTextBox As System.Windows.Forms.TextBox
   Friend WithEvents RelayNumberForLowSideConnectTextBox As System.Windows.Forms.TextBox
   Friend WithEvents RelayNumberForHighSideConnectTextBox As System.Windows.Forms.TextBox
   Friend WithEvents NumberOfSensorsTextBox As System.Windows.Forms.TextBox
   Friend WithEvents gbTestName As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestName As System.Windows.Forms.Label
   Friend WithEvents gbTestDescription As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestDescription As System.Windows.Forms.Label
   Friend WithEvents gbTestType As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestType As System.Windows.Forms.Label
   Friend WithEvents gbMinimumLimit As System.Windows.Forms.GroupBox
   Friend WithEvents lblMinimumLimit As System.Windows.Forms.Label
   Friend WithEvents MinLimitTextBox As System.Windows.Forms.TextBox
   Friend WithEvents gbMaximumLimit As System.Windows.Forms.GroupBox
   Friend WithEvents lblMaximumLimit As System.Windows.Forms.Label
   Friend WithEvents gbTestUnit As System.Windows.Forms.GroupBox
   Friend WithEvents lblTestUnit As System.Windows.Forms.Label
   Friend WithEvents gbMeasurementType As System.Windows.Forms.GroupBox
   Friend WithEvents lblMeasurementType As System.Windows.Forms.Label
   Friend WithEvents gbMeterRange As System.Windows.Forms.GroupBox
   Friend WithEvents lblMeterRange As System.Windows.Forms.Label
   Friend WithEvents gbDelayBeforeMeasurement As System.Windows.Forms.GroupBox
   Friend WithEvents lblDelayBeforeMeasurement As System.Windows.Forms.Label
   Friend WithEvents gbNumberOfPTSSensors As System.Windows.Forms.GroupBox
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents gbRelayNumberLowSideConnect As System.Windows.Forms.GroupBox
   Friend WithEvents lblRelayNumberForLowSideConnect As System.Windows.Forms.Label
   Friend WithEvents gbRelayNumberHighSideConnect As System.Windows.Forms.GroupBox
   Friend WithEvents lblRelayNumberForHighSideConnect As System.Windows.Forms.Label
   Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
