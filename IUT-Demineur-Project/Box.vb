Public Class Box
    Private val As Valeur
    Private form As Etat
    Private cptBombe As Integer
    Public btn As Button
    Public lbl As Label

    Public Sub New(taille As Integer, backColor_Box As Color, borderColor_Box As Color, fontColor As Color)
        btn = New Button
        taille = 50 - (5) * (taille \ 10)
        btn.Size = New Size(taille, taille)
        btn.BackColor = backColor_Box
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderColor = borderColor_Box
        btn.FlatAppearance.BorderSize = 2
        lbl = New Label
        lbl.Size = New Size(taille, taille)
        lbl.ForeColor = fontColor
        lbl.Text = ""
        lbl.TextAlign = ContentAlignment.MiddleCenter
        cptBombe = 0
        val = Valeur.Vide
        form = Etat.Inconnu
    End Sub

    Public Sub set_Color(backColor_Box As Color)
        btn.BackColor = backColor_Box
    End Sub
    Public Sub set_Valeur(valeur As Valeur) ' OU UN BOOL : true = bomb et false = vide
        val = valeur
    End Sub
    Public Function get_Valeur()
        Return val
    End Function
    Public Function get_Etat()
        Return form
    End Function
    Public Sub set_Etat(etat As Etat)
        form = etat
    End Sub
    Public Function get_Button()
        Return btn
    End Function
    Public Function get_Label()
        Return lbl
    End Function
    Public Sub add_CptBombe()
        cptBombe += 1
    End Sub
    Public Enum Valeur
        Bomb
        Nombre
        Vide
    End Enum
    Public Enum Etat
        Inconnu
        Connu
        Drapeau
    End Enum
    Public Sub numerotation()
        If val = Valeur.Nombre Then
            lbl.Text = cptBombe
        End If
    End Sub
End Class