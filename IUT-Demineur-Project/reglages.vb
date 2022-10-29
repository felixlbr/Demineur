Public Class reglages

    Private Const nbLigneDefaut As Integer = 8
    Private Const nbMin As Integer = 6
    Private Const nbMax As Integer = 16
    Private Const nbColonneDefaut As Integer = 8
    Private Const tempsDefaut As Integer = 1
    Private Const nbBombeDefaut As Integer = 10
    Private Const nbBombeMax As Integer = 100

    Private ChangePath As Boolean = False
    Private nbLigne As Integer
    Private nbColonne As Integer
    Private path As String
    Private temps As Integer
    Private nbBombe As Integer
    Private timerActif As Boolean = True

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim estOk As Boolean = True
        If TextBox1.Text.Length() = 0 Then
            Label11.Visible = True
            estOk = False
        End If
        If TextBox2.Text.Length() = 0 Then
            Label10.Visible = True
            estOk = False
        End If
        If estOk Then
            nbLigne = HScrollBar1.Value
            nbColonne = HScrollBar2.Value
            path = TextBox1.Text
            temps = TextBox2.Text
            nbBombe = ListBox1.SelectedIndex + 1
            TextBox1.Enabled = False
            PictureBox4.Visible = False
            PictureBox2.Visible = False
            PictureBox1.Visible = True
            Hide()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Label11.Visible = False
        ChangePath = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Label10.Visible = False
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = vbBack Then Exit Sub
        If Not Char.IsDigit(e.KeyChar) Or TextBox2.Text.Length() > 2 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        accueil.curseur()
        accueil.Enabled = False
        For i = 1 To (nbColonneDefaut * nbLigneDefaut) \ 2
            If i < nbBombeMax Then
                ListBox1.Items.Add(i & " bombes")
            End If
        Next
        ListBox1.SelectedIndex = nbBombeDefaut - 1
        TextBox1.Text = Application.StartupPath & "\config.txt"
        TextBox2.Text = tempsDefaut
        HScrollBar1.Minimum = nbMin
        HScrollBar1.Maximum = nbMax
        HScrollBar1.Value = nbMax / 2
        HScrollBar2.Minimum = nbMin
        HScrollBar2.Maximum = nbMax
        HScrollBar2.Value = nbMax / 2
        nbLigne = nbLigneDefaut
        nbColonne = nbColonneDefaut
        path = Application.StartupPath & "\config.txt"
        temps = tempsDefaut
        nbBombe = nbBombeDefaut
    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        Dim nbCase As Integer
        Label8.Text = HScrollBar1.Value
        nbCase = HScrollBar1.Value * HScrollBar2.Value
        ListBox1.Items.Clear()
        For i = 1 To nbCase \ 2
            ListBox1.Items.Add(i & " bombes")
        Next
        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub HScrollBar2_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar2.Scroll
        Dim nbCase As Integer
        Label4.Text = HScrollBar2.Value
        nbCase = HScrollBar1.Value * HScrollBar2.Value
        ListBox1.Items.Clear()
        For i = 1 To nbCase \ 2
            ListBox1.Items.Add(i & " bombes")
        Next
        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox1.Items.Clear()
        For i = 1 To (nbColonneDefaut * nbLigneDefaut) \ 2
            ListBox1.Items.Add(i & " bombes")
        Next
        ListBox1.SelectedIndex = nbBombeDefaut - 1
        TextBox2.Text = tempsDefaut
        TextBox1.Text = Application.StartupPath & "\config.txt"
        TextBox1.Enabled = False
        PictureBox4.Visible = False
        PictureBox2.Visible = False
        PictureBox1.Visible = True
        HScrollBar1.Value = nbMax / 2
        HScrollBar2.Value = nbMax / 2
        Label8.Text = HScrollBar1.Value
        Label4.Text = HScrollBar2.Value
        CheckBox1.CheckState = CheckState.Checked
        themes.RadioButton3.Checked = True
    End Sub

    Private Sub reglages_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Visible = True Then
            accueil.Enabled = False
        Else
            accueil.Enabled = True
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        TextBox1.Enabled = False
        PictureBox4.Visible = False
        PictureBox2.Visible = False
        PictureBox1.Visible = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        TextBox1.Enabled = True
        PictureBox4.Visible = True
        PictureBox1.Visible = False
        PictureBox2.Visible = True
    End Sub

    Public Function getNbLigne() As Integer
        Return nbLigne
    End Function

    Public Function getNbColonne() As Integer
        Return nbColonne
    End Function

    Public Function getPath() As String
        Return path
    End Function

    Public Function getTemps() As Integer
        Return temps
    End Function

    Public Function getNbBombe() As String
        Return nbBombe
    End Function

    Public Function aChange() As Boolean
        Return ChangePath
    End Function

    Public Sub Change()
        ChangePath = False
    End Sub

    Public Function getTimerActif() As Boolean
        If timerActif Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function verifFichier() As Boolean
        Try
            Dim readFile As System.IO.StreamReader
            readFile = New System.IO.StreamReader(path)
            readFile.ReadLine()
            readFile.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            TextBox2.Enabled = True
            timerActif = True
        Else
    MsgBox("Si vous désactivez le timer, vos parties ne seront plus sauvegardées.", vbOKOnly, "Attention")
            TextBox2.Enabled = False
            timerActif = False
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If MsgBox("Aucun changement ne sera enregistré", vbOKCancel, "Attention") = vbOK Then
            Hide()
            accueil.Enabled = True
            themes.RadioButton3.Checked = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Enabled = False
        themes.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        OpenFileDialog1.Filter = "Fichiers texte (*.txt)|*.txt"

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub reglages_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        accueil.curseur()
    End Sub
End Class

