Partial Class Test_InformationDataSet1
   Partial Public Class RadarSensorTestsDataTable
      Private Sub RadarSensorTestsDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
         If (e.Column.ColumnName = Me.AutolivdllToUseColumn.ColumnName) Then
            'Add user code here
         End If

      End Sub

      Private Sub RadarSensorTestsDataTable_RadarSensorTestsRowChanging(sender As Object, e As RadarSensorTestsRowChangeEvent) Handles Me.RadarSensorTestsRowChanging

      End Sub

   End Class
End Class
