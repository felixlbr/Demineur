Module stockJoueur

    Private tab As ArrayList

    Public Sub enregistrementJoueur(nom As String, nbCasePerf As Integer, tempsPerf As Integer, partieJouees As Integer, tempsJoué As Integer)
        Dim j As Joueur = New Joueur(nom, nbCasePerf, tempsPerf, partieJouees, tempsJoué)
        tab.Add(j)
    End Sub

    Public Sub debut(path As String)
        tab = New ArrayList
        Dim readFile As System.IO.StreamReader
        readFile = New System.IO.StreamReader(reglages.getPath)
        Dim tmp As String = "test test test"
        Dim line() As String
        line = Split(tmp)
        While line(0) <> ""
            line = Split(readFile.ReadLine())
            If line(0) <> "" Then
                enregistrementJoueur(line(0), line(1), line(2), line(3), line(4))
                accueil.ComboBox1.Items.Add(line(0))
            End If
        End While
        readFile.Close()
        tab.Sort(New JoueurComparer())
    End Sub

    Public Sub fin(path As String)
        Dim writeFile As System.IO.StreamWriter = New System.IO.StreamWriter(reglages.getPath)
        For Each j In tab
            writeFile.WriteLine(j.getNom() & " " & j.getNbCasePerf() & " " & j.getTempsPerf() & " " & j.getPartieJouee() & " " & j.getTempsJoue())
        Next
        writeFile.Close()
    End Sub

    Public Sub refreshList()
        scores.ListBox1.Items.Clear()
        scores.ListBox2.Items.Clear()
        scores.ListBox3.Items.Clear()
        tab.Sort(New JoueurComparer())
        For Each j In tab
            scores.ListBox1.Items.Add(j.getNom())
            scores.ListBox2.Items.Add(j.getNbCasePerf)
            scores.ListBox3.Items.Add(j.getTempsPerf & " secondes")

            scores.ComboBox1.Items.Add(j.getNom())
        Next
    End Sub

    Public Function getJoueur(nom As String) As Joueur
        For Each j In tab
            If j.getNom() = nom Then
                Return j
            End If
        Next
        Return Nothing
    End Function

    Public Function verifJoueur(nom As String) As Boolean
        For Each j In tab
            If j.getNom() = nom Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub updateAvecTemps(nbCaseDecouverte As Integer, temps As Integer)
        Dim j As Joueur = getJoueur(accueil.ComboBox1.Text)
        j.setTempsJoue(j.getTempsJoue + temps)
        j.setPartieJouee(j.getPartieJouee + 1)
        If j.getNbCasePerf < nbCaseDecouverte Then
            j.setNbCasePerf(nbCaseDecouverte)
            j.setTempsPerf(temps)
        ElseIf j.getNbCasePerf = nbCaseDecouverte And temps < j.getTempsPerf Then
            j.setNbCasePerf(nbCaseDecouverte)
            j.setTempsPerf(temps)
        End If
    End Sub

End Module
