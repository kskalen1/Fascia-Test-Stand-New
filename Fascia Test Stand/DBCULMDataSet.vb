Partial Class DBCULMDataSet
    Partial Public Class Fascia_TestDataTable
        Private Sub Fascia_TestDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.Fascia_NumberColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class
End Class
