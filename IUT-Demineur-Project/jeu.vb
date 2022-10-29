Imports System.ComponentModel

Public Class jeu
    Private flag As Boolean
    Private cptCase As Integer
    Public Sub init(l As Integer, c As Integer, p As String, n As Integer, tmp As String, th As Theme)
        cptCase = l * c - n
        enregistrement(l, c, n, tmp, th)
    End Sub

    Private Sub Formulaire_Jeu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        accueil.curseur()
        Me.Text = accueil.ComboBox1.Text
        Dim th As Theme = Donnees.get_Theme
        If (reglages.getTimerActif) Then
            Label1.Text = Donnees.get_Temps * 60 + 1
        Else
            Label1.Visible = False
        End If
        Timer1.Interval = 1000
        tlp.AutoSize = True
        tlp.Margin = New Padding(0, 100, 0, 0)
        tlp.SuspendLayout()
        tlp.ColumnCount = Donnees.get_Colonne
        tlp.RowCount = Donnees.get_Ligne
        For j As Integer = 0 To Donnees.get_Colonne - 1
            For i As Integer = 0 To Donnees.get_Ligne - 1
                tlp.Controls.Add(Donnees.get_BoxBtn(i, j), j, i)
            Next
        Next
        Me.Controls.Add(tlp)
        tlp.ResumeLayout()
        For Each btn As Control In tlp.Controls
            If TypeOf btn Is Button Then
                AddHandler btn.Click, AddressOf First_Click
            End If
        Next
        afficheHeure(sender, e)
    End Sub

    Private Sub afficheHeure(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If Label1.Visible = True Then
            Label1.Text = Label1.Text - 1
            If Label1.Text = 0 Then
                Partie_Perdue()
            End If
        End If
    End Sub

    Private Sub First_Click(sender As Object, e As EventArgs)
        Timer1.Start()
        Drapeau.Enabled = True
        Aide.Enabled = True
        Drapeau.FlatStyle = FlatStyle.Flat
        Drapeau.FlatAppearance.BorderColor = Donnees.get_Theme().get_backColor_Box()
        flag = False
        For Each btn As Control In tlp.Controls
            If TypeOf btn Is Button Then
                RemoveHandler btn.Click, AddressOf First_Click
            End If
        Next
        placementBombes(tlp.GetRow(sender), tlp.GetColumn(sender))
        placementNombres()
        Btn_Clicked(sender, e)
        For Each btn As Control In tlp.Controls
            If TypeOf btn Is Button Then
                AddHandler btn.Click, AddressOf Btn_Clicked
                AddHandler btn.MouseDown, AddressOf Click_Droit
            End If
        Next
    End Sub

    Private Sub Btn_Clicked(sender As Object, e As EventArgs)
        If isBomb(tlp.GetRow(sender), tlp.GetColumn(sender)) Then
            Partie_Perdue()
        Else
            If affichageText(tlp.GetRow(sender), tlp.GetColumn(sender)).Text = "" Then
                Donnees.caseVide(tlp.GetRow(sender), tlp.GetColumn(sender))
                For Each l As Integer In Donnees.get_ListeVide
                    Dim numero As Control = tlp.Controls.Item((l Mod Donnees.get_Colonne) * Donnees.get_Ligne + (l \ Donnees.get_Colonne))
                    numero.Visible = False
                    tlp.Controls.Add(Donnees.affichageText(tlp.GetRow(numero), tlp.GetColumn(numero)), tlp.GetColumn(numero), tlp.GetRow(numero))
                    cptCase -= 1
                Next
                Donnees.clear_ListVide()
            Else
                Dim lbl As Label = Donnees.reveler(tlp.GetRow(sender), tlp.GetColumn(sender))
                sender.Visible = False
                tlp.Controls.Add(lbl, tlp.GetColumn(sender), tlp.GetRow(sender))
                cptCase -= 1
            End If
            If cptCase = 0 Then
                Partie_Gagnee()
            End If
        End If
    End Sub
    Private Sub Flag_Click(sender As Object, e As EventArgs)
        If Donnees.retourne(tlp.GetRow(sender), tlp.GetColumn(sender)) Then
            If Donnees.drapeau(tlp.GetRow(sender), tlp.GetColumn(sender)) Then
                sender.BackColor = Donnees.get_Theme().get_flagColor()
            Else
                sender.BackColor = Donnees.get_Theme().get_backColor_Box()
            End If
        End If
    End Sub
    Private Sub Click_Droit(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            Flag_Click(sender, e)
        End If
    End Sub
    Private Sub Partie_Gagnee()
        Timer1.Stop()
        Enabled = False
        MsgBox("Vous avez gagné. Bravo !", vbOKOnly, "Fin de la partie")
        Close()
        accueil.Show()
    End Sub
    Private Sub Partie_Perdue()
        Timer1.Stop()
        Enabled = False
        MsgBox("Vous avez perdu.", vbOKOnly, "Fin de la partie")
        Close()
        accueil.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Timer1.Stop()
        Enabled = False
        If MsgBox("Vous êtes sur le point de perdre votre partie.", vbOKCancel, "Attention") = vbOK Then
            Close()
            accueil.Visible = True
        Else
            Timer1.Start()
            Enabled = True
        End If
    End Sub

    Private Sub jeu_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If reglages.getTimerActif Then
            stockJoueur.updateAvecTemps((reglages.getNbLigne * reglages.getNbColonne - reglages.getNbBombe) - cptCase, reglages.getTemps * 60 - Label1.Text)
            MsgBox("Vous avez dévoilé " & (reglages.getNbLigne * reglages.getNbColonne - reglages.getNbBombe) - cptCase & " case(s) en " & (reglages.getTemps * 60 - Label1.Text) \ 60 & " minute(s) et " & (reglages.getTemps * 60 - Label1.Text) Mod 60 & " seconde(s).", vbOKOnly, "Informations")
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        PictureBox3.Visible = False
        PictureBox2.Visible = True
        tlp.Enabled = False
        Label1.Enabled = False
        PictureBox1.Enabled = False
        Drapeau.Enabled = False

        Timer1.Stop()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.Visible = False
        PictureBox3.Visible = True
        tlp.Enabled = True
        Label1.Enabled = True
        PictureBox1.Enabled = True
        Drapeau.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub Drapeau_Click(sender As Object, e As EventArgs) Handles Drapeau.Click
        If flag Then
            flag = False
            Drapeau.FlatAppearance.BorderColor = Donnees.get_Theme().get_backColor_Box()
            For Each btn As Control In tlp.Controls
                If TypeOf btn Is Button Then
                    RemoveHandler btn.Click, AddressOf Flag_Click
                End If
            Next
            For Each btn As Control In tlp.Controls
                If TypeOf btn Is Button Then
                    AddHandler btn.Click, AddressOf Btn_Clicked
                End If
            Next
        Else
            flag = True
            Drapeau.FlatAppearance.BorderColor = Donnees.get_Theme().get_flagColor()
            For Each btn As Control In tlp.Controls
                If TypeOf btn Is Button Then
                    RemoveHandler btn.Click, AddressOf Btn_Clicked
                End If
            Next
            For Each btn As Control In tlp.Controls
                If TypeOf btn Is Button Then
                    AddHandler btn.Click, AddressOf Flag_Click
                End If
            Next
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Dim coordonnees As Integer
        If keyData = Keys.Up Or keyData = Keys.Down Or keyData = Keys.Right Or keyData = Keys.Left Then
            For Each ctr As Control In tlp.Controls
                If TypeOf ctr Is Button And ctr.Focused = True Then
                    coordonnees = Donnees.Next_Box(tlp.GetRow(ctr), tlp.GetColumn(ctr), keyData)
                    Dim numero As Button = tlp.Controls.Item((coordonnees Mod Donnees.get_Colonne) * Donnees.get_Ligne + (coordonnees \ Donnees.get_Colonne))
                    numero.Focus()
                    Return True
                End If
            Next
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Aide.Click
        Dim coordonnees_Drapeau As Integer = Donnees.Marque_Drapeau()
        Dim btn As Button
        If coordonnees_Drapeau > -1 Then
            btn = tlp.Controls.Item((coordonnees_Drapeau Mod Donnees.get_Colonne) * Donnees.get_Ligne + (coordonnees_Drapeau \ Donnees.get_Colonne))
            btn.BackColor = Donnees.get_Theme().get_flagColor()
        Else
            Dim coordonnees_Demasque As Integer = Donnees.Demasque()
            If coordonnees_Demasque > -1 Then
                btn = tlp.Controls.Item((coordonnees_Demasque Mod Donnees.get_Colonne) * Donnees.get_Ligne + (coordonnees_Demasque \ Donnees.get_Colonne))
                Btn_Clicked(btn, e)
            Else
                MsgBox("L'aide n'est pas disponible sur les coups qui relèvent du hasard...")
            End If
        End If
    End Sub

    Private Sub jeu_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        accueil.curseur()
    End Sub
End Class