Public Class informations
    Private Sub informations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        accueil.curseur()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://felixliburski.fr")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://hugopereira.fr")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://github.com/tigrou23/IUT-Demineur2-Project")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        accueil.Enabled = True
        Hide()
    End Sub

    Private Sub informations_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        accueil.curseur()
    End Sub
End Class