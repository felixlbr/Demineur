Public Class JoueurComparer

    Implements IComparer

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        If x.getNbCasePerf < y.getNbCasePerf Then
            Return 1
        ElseIf x.getNbCasePerf > y.getNbCasePerf Then
            Return -1
        Else
            If x.getTempsPerf > y.getTempsPerf Then
                Return 1
            ElseIf x.getTempsPerf < y.getTempsPerf Then
                Return -1
            Else
                Return 0
            End If
        End If
    End Function

End Class
