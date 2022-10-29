Imports System.ComponentModel

Public Class accueil

    Private Const maxLettre As Integer = 50

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Voulez-vous vraiment quitter l'application ?", vbOKCancel, "Quitter") = vbOK Then
            Close()
        End If
    End Sub

    Private Sub accueil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        curseur()
        FormBorderStyle = FormBorderStyle.FixedSingle
        jeu.FormBorderStyle = FormBorderStyle.FixedSingle
        reglages.FormBorderStyle = FormBorderStyle.FixedSingle
        scores.FormBorderStyle = FormBorderStyle.FixedSingle
        themes.FormBorderStyle = FormBorderStyle.FixedSingle
        'à optimiser mais indispensable pour charger les réglages
        reglages.Show()
        reglages.Hide()
    End Sub

    Public Sub curseur()
        Dim cursor As Icon = My.Resources.cursor
        Me.Cursor = New Cursor(cursor.Handle)
        scores.Cursor = New Cursor(cursor.Handle)
        reglages.Cursor = New Cursor(cursor.Handle)
        themes.Cursor = New Cursor(cursor.Handle)
        jeu.Cursor = New Cursor(cursor.Handle)
        informations.Cursor = New Cursor(cursor.Handle)
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        reglages.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem <> "" Then
            Hide()
            jeu.init(reglages.getNbLigne, reglages.getNbColonne, reglages.getPath, reglages.getNbBombe, reglages.getTemps, themes.getTheme)
            jeu.Show()
        ElseIf ComboBox1.Text.Length = 0 Then
            Label2.Visible = True
            ComboBox1.Focus()
        ElseIf ComboBox1.Text.Length < 3 Then
            ComboBox1.Text = ""
            Label4.Visible = True
            ComboBox1.Focus()
        Else
            If verifJoueur(ComboBox1.Text) Then
                Hide()
                jeu.init(reglages.getNbLigne, reglages.getNbColonne, reglages.getPath, reglages.getNbBombe, reglages.getTemps, themes.getTheme)
                jeu.Show()
            Else
                stockJoueur.enregistrementJoueur(ComboBox1.Text, 0, 0, 0, 0)
                Hide()
                jeu.init(reglages.getNbLigne, reglages.getNbColonne, reglages.getPath, reglages.getNbBombe, reglages.getTemps, themes.getTheme)
                jeu.Show()
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        Label4.Visible = False
        If e.KeyChar = vbBack Then Exit Sub
        If Not Char.IsLetter(e.KeyChar) Or ComboBox1.Text.Length > maxLettre - 1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Label2.Visible = False
        Label3.Visible = False
    End Sub

    Private Sub accueil_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        curseur()
        If reglages.aChange() = True Then
            If reglages.verifFichier() = True Then
                stockJoueur.debut(reglages.getPath())
                Button1.Enabled = True
                Button2.Enabled = True
                ComboBox1.Enabled = True
                Label3.Visible = False
            Else
                Label3.Visible = True
                Button1.Enabled = False
                Button2.Enabled = False
                ComboBox1.Enabled = False
            End If
            reglages.Change()
        End If
    End Sub

    Private Sub accueil_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If (reglages.verifFichier()) Then
            fin(reglages.getPath)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Enabled = False
        scores.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Enabled = False
        informations.Show()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class
