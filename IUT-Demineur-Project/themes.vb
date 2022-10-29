Public Class themes

    Private theme As Theme

    Public Function getTheme() As Theme
        If RadioButton1.Checked = True Then 'Espagne
            'Ukraine
            theme = New Theme(Color.RoyalBlue, Color.Yellow, Color.Snow, Color.Yellow)
        ElseIf RadioButton2.Checked = True Then 'Vietnam
            'Espagne
            theme = New Theme(Color.Yellow, Color.Red, Color.Snow, Color.Red)
        ElseIf RadioButton7.Checked = True Then 'Orignel
            theme = New Theme(Color.Blue, Color.Snow, Color.Snow, Color.Red)
        ElseIf RadioButton4.Checked = True Then 'Pologne
            'Vietnam
            theme = New Theme(Color.Red, Color.Yellow, Color.Snow, Color.Yellow)
        ElseIf RadioButton5.Checked = True Then 'Portugal
            'Pologne
            theme = New Theme(Color.White, Color.Red, Color.Snow, Color.Red)
        ElseIf RadioButton6.Checked = True Then 'France
            'Portugal
            theme = New Theme(Color.ForestGreen, Color.DarkRed, Color.Snow, Color.DarkRed)
        Else
            'Original
            theme = New Theme(Color.Gainsboro, Color.Gray, Color.Snow, Color.DarkGray)
        End If
        Return theme

    End Function

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = True
            PictureBox8.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            PictureBox1.Visible = True
            PictureBox2.Visible = False
            PictureBox8.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox8.Visible = True
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox8.Visible = False
            PictureBox4.Visible = True
            PictureBox5.Visible = False
            PictureBox6.Visible = False
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox8.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = True
            PictureBox6.Visible = False
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox8.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = True
            PictureBox7.Visible = False
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox8.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
            PictureBox7.Visible = True
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        reglages.Enabled = True
        Hide()
    End Sub

    Private Sub themes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        accueil.curseur()
    End Sub

    Private Sub themes_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        accueil.curseur()
    End Sub
End Class