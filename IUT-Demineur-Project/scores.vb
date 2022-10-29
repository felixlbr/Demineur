Public Class scores

    Private Sub scores_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        stockJoueur.refreshList()
        If ListBox1.Items.Count <> 0 Then
            ListBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim tmp() As String = Split(ListBox1.SelectedItem)
        ComboBox1.Text = tmp(0)
        ListBox2.SelectedIndex = ListBox1.SelectedIndex
        ListBox3.SelectedIndex = ListBox1.SelectedIndex

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        accueil.Enabled = True
        Hide()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim nom As New List(Of String)
        Dim bombe As New List(Of String)
        Dim temps As New List(Of String)

        For Each item In ListBox1.Items
            nom.Add(item)
        Next

        For Each item In ListBox2.Items
            bombe.Add(item)
        Next

        For Each item In ListBox3.Items
            temps.Add(item)
        Next


        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()

        For i As Integer = nom.Count - 1 To 0 Step -1
            ListBox1.Items.Add(nom(i))
        Next
        For i As Integer = bombe.Count - 1 To 0 Step -1
            ListBox2.Items.Add(bombe(i))
        Next
        For i As Integer = temps.Count - 1 To 0 Step -1
            ListBox3.Items.Add(temps(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim j As Joueur = stockJoueur.getJoueur(ComboBox1.Text)
        If stockJoueur.verifJoueur(ComboBox1.Text) Then
            MsgBox("Nom : " & j.getNom & vbCrLf & "Meilleur nombre de cases révélées : " & j.getNbCasePerf & vbCrLf & "Temps de cette performance : " & j.getTempsPerf \ 60 & " minute(s) et " & j.getTempsPerf Mod 60 & " seconde(s)" & vbCrLf & "Nombre de partie(s) jouée(s) : " & j.getPartieJouee & vbCrLf & "Nombre de minute(s) en partie : " & j.getTempsJoue \ 60 & vbCrLf, vbOKOnly, "Statistiques du joueur")
        Else
            MsgBox("Le joueur n'est pas enregistré.", vbOKOnly, "Attention")
        End If
    End Sub

    Private Sub scores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        accueil.curseur()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox1.SelectedIndex = ListBox2.SelectedIndex
        ListBox3.SelectedIndex = ListBox2.SelectedIndex
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        ListBox2.SelectedIndex = ListBox3.SelectedIndex
        ListBox1.SelectedIndex = ListBox3.SelectedIndex
    End Sub

    Private Sub scores_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        accueil.curseur()
    End Sub

End Class