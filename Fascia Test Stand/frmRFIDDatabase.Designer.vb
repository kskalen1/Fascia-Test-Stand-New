<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRFIDDatabase
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
      Dim BITLabel As System.Windows.Forms.Label
      Dim Bit_DescriptionLabel As System.Windows.Forms.Label
      Dim BIT_ActiveLabel As System.Windows.Forms.Label
      Dim BIT_Test_IDLabel As System.Windows.Forms.Label
      Dim BIT_Fascia_TesterLabel As System.Windows.Forms.Label
      Dim BIT_CommentLabel As System.Windows.Forms.Label
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRFIDDatabase))
      Me.RFID_BITSDataSet = New Fascia_Test_Stand.RFID_BITSDataSet()
      Me.BITSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
      Me.BITSTableAdapter = New Fascia_Test_Stand.RFID_BITSDataSetTableAdapters.BITSTableAdapter()
      Me.RFID_BITS_TableAdapterManager = New Fascia_Test_Stand.RFID_BITSDataSetTableAdapters.TableAdapterManager()
      Me.BITSBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
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
      Me.BITSBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
      Me.IDTextBox = New System.Windows.Forms.TextBox()
      Me.BITTextBox = New System.Windows.Forms.TextBox()
      Me.Bit_DescriptionTextBox = New System.Windows.Forms.TextBox()
      Me.BIT_ActiveCheckBox = New System.Windows.Forms.CheckBox()
      Me.BIT_Test_IDTextBox = New System.Windows.Forms.TextBox()
      Me.BIT_Fascia_TesterCheckBox = New System.Windows.Forms.CheckBox()
      Me.BIT_CommentTextBox = New System.Windows.Forms.TextBox()
      Me.Label1 = New System.Windows.Forms.Label()
      IDLabel = New System.Windows.Forms.Label()
      BITLabel = New System.Windows.Forms.Label()
      Bit_DescriptionLabel = New System.Windows.Forms.Label()
      BIT_ActiveLabel = New System.Windows.Forms.Label()
      BIT_Test_IDLabel = New System.Windows.Forms.Label()
      BIT_Fascia_TesterLabel = New System.Windows.Forms.Label()
      BIT_CommentLabel = New System.Windows.Forms.Label()
      CType(Me.RFID_BITSDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.BITSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.BITSBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.BITSBindingNavigator.SuspendLayout()
      Me.SuspendLayout()
      '
      'IDLabel
      '
      IDLabel.AutoSize = True
      IDLabel.Location = New System.Drawing.Point(14, 264)
      IDLabel.Name = "IDLabel"
      IDLabel.Size = New System.Drawing.Size(21, 13)
      IDLabel.TabIndex = 1
      IDLabel.Text = "ID:"
      IDLabel.Visible = False
      '
      'BITLabel
      '
      BITLabel.AutoSize = True
      BITLabel.Location = New System.Drawing.Point(14, 15)
      BITLabel.Name = "BITLabel"
      BITLabel.Size = New System.Drawing.Size(27, 13)
      BITLabel.TabIndex = 3
      BITLabel.Text = "BIT:"
      '
      'Bit_DescriptionLabel
      '
      Bit_DescriptionLabel.AutoSize = True
      Bit_DescriptionLabel.Location = New System.Drawing.Point(14, 41)
      Bit_DescriptionLabel.Name = "Bit_DescriptionLabel"
      Bit_DescriptionLabel.Size = New System.Drawing.Size(78, 13)
      Bit_DescriptionLabel.TabIndex = 5
      Bit_DescriptionLabel.Text = "Bit Description:"
      '
      'BIT_ActiveLabel
      '
      BIT_ActiveLabel.AutoSize = True
      BIT_ActiveLabel.Location = New System.Drawing.Point(14, 69)
      BIT_ActiveLabel.Name = "BIT_ActiveLabel"
      BIT_ActiveLabel.Size = New System.Drawing.Size(60, 13)
      BIT_ActiveLabel.TabIndex = 7
      BIT_ActiveLabel.Text = "BIT Active:"
      BIT_ActiveLabel.Visible = False
      '
      'BIT_Test_IDLabel
      '
      BIT_Test_IDLabel.AutoSize = True
      BIT_Test_IDLabel.Location = New System.Drawing.Point(14, 97)
      BIT_Test_IDLabel.Name = "BIT_Test_IDLabel"
      BIT_Test_IDLabel.Size = New System.Drawing.Size(65, 13)
      BIT_Test_IDLabel.TabIndex = 9
      BIT_Test_IDLabel.Text = "BIT Test ID:"
      '
      'BIT_Fascia_TesterLabel
      '
      BIT_Fascia_TesterLabel.AutoSize = True
      BIT_Fascia_TesterLabel.Location = New System.Drawing.Point(14, 125)
      BIT_Fascia_TesterLabel.Name = "BIT_Fascia_TesterLabel"
      BIT_Fascia_TesterLabel.Size = New System.Drawing.Size(161, 13)
      BIT_Fascia_TesterLabel.TabIndex = 11
      BIT_Fascia_TesterLabel.Text = "BIT Fascia Tester (IA Relavient):"
      '
      'BIT_CommentLabel
      '
      BIT_CommentLabel.AutoSize = True
      BIT_CommentLabel.Location = New System.Drawing.Point(14, 153)
      BIT_CommentLabel.Name = "BIT_CommentLabel"
      BIT_CommentLabel.Size = New System.Drawing.Size(74, 13)
      BIT_CommentLabel.TabIndex = 13
      BIT_CommentLabel.Text = "BIT Comment:"
      '
      'RFID_BITSDataSet
      '
      Me.RFID_BITSDataSet.DataSetName = "RFID_BITSDataSet"
      Me.RFID_BITSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
      '
      'BITSBindingSource
      '
      Me.BITSBindingSource.DataMember = "BITS"
      Me.BITSBindingSource.DataSource = Me.RFID_BITSDataSet
      '
      'BITSTableAdapter
      '
      Me.BITSTableAdapter.ClearBeforeFill = True
      '
      'RFID_BITS_TableAdapterManager
      '
      Me.RFID_BITS_TableAdapterManager.BackupDataSetBeforeUpdate = False
      Me.RFID_BITS_TableAdapterManager.BITSTableAdapter = Me.BITSTableAdapter
      Me.RFID_BITS_TableAdapterManager.UpdateOrder = Fascia_Test_Stand.RFID_BITSDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
      '
      'BITSBindingNavigator
      '
      Me.BITSBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
      Me.BITSBindingNavigator.BindingSource = Me.BITSBindingSource
      Me.BITSBindingNavigator.CountItem = Me.BindingNavigatorCountItem
      Me.BITSBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
      Me.BITSBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.BITSBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.BITSBindingNavigatorSaveItem})
      Me.BITSBindingNavigator.Location = New System.Drawing.Point(0, 314)
      Me.BITSBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
      Me.BITSBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
      Me.BITSBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
      Me.BITSBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
      Me.BITSBindingNavigator.Name = "BITSBindingNavigator"
      Me.BITSBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
      Me.BITSBindingNavigator.Size = New System.Drawing.Size(534, 25)
      Me.BITSBindingNavigator.TabIndex = 0
      Me.BITSBindingNavigator.Text = "BindingNavigator1"
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
      'BITSBindingNavigatorSaveItem
      '
      Me.BITSBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
      Me.BITSBindingNavigatorSaveItem.Image = CType(resources.GetObject("BITSBindingNavigatorSaveItem.Image"), System.Drawing.Image)
      Me.BITSBindingNavigatorSaveItem.Name = "BITSBindingNavigatorSaveItem"
      Me.BITSBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
      Me.BITSBindingNavigatorSaveItem.Text = "Save Data"
      '
      'IDTextBox
      '
      Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BITSBindingSource, "ID", True))
      Me.IDTextBox.Location = New System.Drawing.Point(114, 261)
      Me.IDTextBox.Name = "IDTextBox"
      Me.IDTextBox.Size = New System.Drawing.Size(104, 20)
      Me.IDTextBox.TabIndex = 2
      Me.IDTextBox.Visible = False
      '
      'BITTextBox
      '
      Me.BITTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BITSBindingSource, "BIT", True))
      Me.BITTextBox.Location = New System.Drawing.Point(114, 12)
      Me.BITTextBox.Name = "BITTextBox"
      Me.BITTextBox.Size = New System.Drawing.Size(104, 20)
      Me.BITTextBox.TabIndex = 4
      '
      'Bit_DescriptionTextBox
      '
      Me.Bit_DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BITSBindingSource, "Bit_Description", True))
      Me.Bit_DescriptionTextBox.Location = New System.Drawing.Point(114, 38)
      Me.Bit_DescriptionTextBox.Name = "Bit_DescriptionTextBox"
      Me.Bit_DescriptionTextBox.Size = New System.Drawing.Size(340, 20)
      Me.Bit_DescriptionTextBox.TabIndex = 6
      '
      'BIT_ActiveCheckBox
      '
      Me.BIT_ActiveCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.BITSBindingSource, "BIT_Active", True))
      Me.BIT_ActiveCheckBox.Location = New System.Drawing.Point(114, 64)
      Me.BIT_ActiveCheckBox.Name = "BIT_ActiveCheckBox"
      Me.BIT_ActiveCheckBox.Size = New System.Drawing.Size(104, 24)
      Me.BIT_ActiveCheckBox.TabIndex = 8
      Me.BIT_ActiveCheckBox.UseVisualStyleBackColor = True
      Me.BIT_ActiveCheckBox.Visible = False
      '
      'BIT_Test_IDTextBox
      '
      Me.BIT_Test_IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BITSBindingSource, "BIT_Test_ID", True))
      Me.BIT_Test_IDTextBox.Location = New System.Drawing.Point(114, 94)
      Me.BIT_Test_IDTextBox.Name = "BIT_Test_IDTextBox"
      Me.BIT_Test_IDTextBox.Size = New System.Drawing.Size(104, 20)
      Me.BIT_Test_IDTextBox.TabIndex = 10
      '
      'BIT_Fascia_TesterCheckBox
      '
      Me.BIT_Fascia_TesterCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.BITSBindingSource, "BIT_Fascia_Tester", True))
      Me.BIT_Fascia_TesterCheckBox.Location = New System.Drawing.Point(190, 120)
      Me.BIT_Fascia_TesterCheckBox.Name = "BIT_Fascia_TesterCheckBox"
      Me.BIT_Fascia_TesterCheckBox.Size = New System.Drawing.Size(104, 24)
      Me.BIT_Fascia_TesterCheckBox.TabIndex = 12
      Me.BIT_Fascia_TesterCheckBox.UseVisualStyleBackColor = True
      '
      'BIT_CommentTextBox
      '
      Me.BIT_CommentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BITSBindingSource, "BIT_Comment", True))
      Me.BIT_CommentTextBox.Location = New System.Drawing.Point(114, 150)
      Me.BIT_CommentTextBox.Multiline = True
      Me.BIT_CommentTextBox.Name = "BIT_CommentTextBox"
      Me.BIT_CommentTextBox.Size = New System.Drawing.Size(340, 64)
      Me.BIT_CommentTextBox.TabIndex = 14
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.BackColor = System.Drawing.Color.Yellow
      Me.Label1.Location = New System.Drawing.Point(38, 284)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(425, 13)
      Me.Label1.TabIndex = 15
      Me.Label1.Text = "NOTE:You Must Click On The Save Icon Below To Save Any Changes You Have Made"
      '
      'frmRFIDDatabase
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(534, 339)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(IDLabel)
      Me.Controls.Add(Me.IDTextBox)
      Me.Controls.Add(BITLabel)
      Me.Controls.Add(Me.BITTextBox)
      Me.Controls.Add(Bit_DescriptionLabel)
      Me.Controls.Add(Me.Bit_DescriptionTextBox)
      Me.Controls.Add(BIT_ActiveLabel)
      Me.Controls.Add(Me.BIT_ActiveCheckBox)
      Me.Controls.Add(BIT_Test_IDLabel)
      Me.Controls.Add(Me.BIT_Test_IDTextBox)
      Me.Controls.Add(BIT_Fascia_TesterLabel)
      Me.Controls.Add(Me.BIT_Fascia_TesterCheckBox)
      Me.Controls.Add(BIT_CommentLabel)
      Me.Controls.Add(Me.BIT_CommentTextBox)
      Me.Controls.Add(Me.BITSBindingNavigator)
      Me.Name = "frmRFIDDatabase"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "RFID BIT DATABASE EDITOR"
      CType(Me.RFID_BITSDataSet, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.BITSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.BITSBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
      Me.BITSBindingNavigator.ResumeLayout(False)
      Me.BITSBindingNavigator.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents RFID_BITSDataSet As Fascia_Test_Stand.RFID_BITSDataSet
   Friend WithEvents BITSBindingSource As System.Windows.Forms.BindingSource
   Friend WithEvents BITSTableAdapter As Fascia_Test_Stand.RFID_BITSDataSetTableAdapters.BITSTableAdapter
   Friend WithEvents RFID_BITS_TableAdapterManager As Fascia_Test_Stand.RFID_BITSDataSetTableAdapters.TableAdapterManager
   Friend WithEvents BITSBindingNavigator As System.Windows.Forms.BindingNavigator
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
   Friend WithEvents BITSBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
   Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
   Friend WithEvents BITTextBox As System.Windows.Forms.TextBox
   Friend WithEvents Bit_DescriptionTextBox As System.Windows.Forms.TextBox
   Friend WithEvents BIT_ActiveCheckBox As System.Windows.Forms.CheckBox
   Friend WithEvents BIT_Test_IDTextBox As System.Windows.Forms.TextBox
   Friend WithEvents BIT_Fascia_TesterCheckBox As System.Windows.Forms.CheckBox
   Friend WithEvents BIT_CommentTextBox As System.Windows.Forms.TextBox
   Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
