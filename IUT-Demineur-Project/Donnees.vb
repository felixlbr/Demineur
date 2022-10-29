Module Donnees
    Private grille()() As Box
    Private nbLignes As Integer
    Private nbColonnes As Integer
    Private temps As Integer
    Private nbBombe As Integer
    Private placementBombe() As Integer
    Dim listeVide As ArrayList
    Private theme As Theme
    Public Sub enregistrement(nbL As Integer, nbC As Integer, nbB As Integer, tmp As Integer, th As Theme) 'ajouter le tempss
        nbBombe = nbB
        nbLignes = nbL
        nbColonnes = nbC
        theme = th
        temps = tmp
        placementBombe = New Integer(nbBombe - 1) {}
        listeVide = New ArrayList
        grille = New Box(nbLignes - 1)() {}
        Dim taille As Integer = nbLignes
        If nbColonnes > nbLignes Then
            taille = nbColonnes
        End If
        For i As Integer = 0 To nbLignes - 1
            grille(i) = New Box(nbColonnes - 1) {}
        Next
        For i As Integer = 0 To nbLignes - 1
            For j As Integer = 0 To nbColonnes - 1
                grille(i)(j) = New Box(taille, theme.get_backColor_Box, theme.get_borderColor_Box, theme.get_fontColor)
            Next
        Next
    End Sub
    'Placement aléatoire des bombes
    Public Sub placementBombes(ligne As Integer, colonne As Integer)
        Dim locLigne, locColonne As Integer
        Dim l1 As Integer, l2 As Integer, c1 As Integer, c2 As Integer
        Dim random As New Random()
        l1 = ligne - 1
        l2 = ligne + 1
        c1 = colonne - 1
        c2 = colonne + 1

        For i As Integer = 0 To nbBombe - 1
            Do
                locLigne = random.Next(0, nbLignes)
                locColonne = random.Next(0, nbColonnes)
            Loop Until grille(locLigne)(locColonne).get_Valeur <> Box.Valeur.Bomb And (locLigne <> ligne And locColonne <> colonne) And
                (locLigne <> l1 Or locColonne <> c1) And
                (locLigne <> l1 Or locColonne <> colonne) And
                (locLigne <> l1 Or locColonne <> c2) And
                (locLigne <> ligne Or locColonne <> c1) And
                (locLigne <> ligne Or locColonne <> c2) And
                (locLigne <> l2 Or locColonne <> c1) And
                (locLigne <> l2 Or locColonne <> colonne) And
                (locLigne <> l2 Or locColonne <> c2)
            'grille(locLigne)(locColonne).set_Color(Color.Red)
            grille(locLigne)(locColonne).set_Valeur(Box.Valeur.Bomb)
            placementBombe(i) = locLigne * nbColonnes + locColonne
        Next
    End Sub
    'Placement des valeurs des cases "Nombre"
    Public Sub placementNombres()
        For Each b As Integer In placementBombe
            For l As Integer = -1 To 1
                For c As Integer = -1 To 1
                    If ((b \ nbColonnes) + l >= 0) And ((b Mod nbColonnes) + c >= 0) And ((b \ nbColonnes) + l <= nbLignes - 1) And ((b Mod nbColonnes) + c <= nbColonnes - 1) Then
                        If grille((b \ nbColonnes) + l)((b Mod nbColonnes) + c).get_Valeur <> Box.Valeur.Bomb Then
                            grille((b \ nbColonnes) + l)((b Mod nbColonnes) + c).add_CptBombe()
                            grille((b \ nbColonnes) + l)((b Mod nbColonnes) + c).set_Valeur(Box.Valeur.Nombre)
                        End If
                    End If
                Next
            Next
        Next
        For i As Integer = 0 To nbLignes - 1
            For j As Integer = 0 To nbColonnes - 1
                grille(i)(j).numerotation()
            Next
        Next
    End Sub
    Public Sub caseVide(ligne As Integer, colonne As Integer)
        For l As Integer = -1 To 1
            For c As Integer = -1 To 1
                If (ligne + l >= 0) And (colonne + c >= 0) And (ligne + l <= nbLignes - 1) And (colonne + c <= nbColonnes - 1) Then
                    If grille(ligne + l)(colonne + c).get_Valeur <> Box.Valeur.Bomb And grille(ligne + l)(colonne + c).get_Etat = Box.Etat.Inconnu Then
                        listeVide.Add((ligne + l) * nbColonnes + (colonne + c))
                        reveler(ligne + l, colonne + c)
                        If grille(ligne + l)(colonne + c).get_Valeur = Box.Valeur.Vide Then
                            caseVide(ligne + l, colonne + c)
                        End If

                    End If
                End If
            Next
        Next
    End Sub
    Public Function isBomb(ligne As Integer, colonne As Integer)
        If grille(ligne)(colonne).get_Valeur = Box.Valeur.Bomb Then
            Return True
        Else Return False
        End If
    End Function
    Public Function affichageText(ligne As Integer, Colonne As Integer)
        Return grille(ligne)(Colonne).get_Label()
    End Function
    Public Function reveler(ligne As Integer, Colonne As Integer)
        grille(ligne)(Colonne).set_Etat(Box.Etat.Connu)
        Return affichageText(ligne, Colonne)
    End Function
    Public Function retourne(ligne As Integer, colonne As Integer)
        Return grille(ligne)(colonne).get_Etat <> Box.Etat.Connu
    End Function
    'Methode de marquage d'une case par un drapeau
    Public Function drapeau(ligne As Integer, colonne As Integer)
        If grille(ligne)(colonne).get_Etat = Box.Etat.Drapeau Then
            grille(ligne)(colonne).set_Etat(Box.Etat.Inconnu)
            Return False
        Else
            grille(ligne)(colonne).set_Etat(Box.Etat.Drapeau)
            Return True
        End If
    End Function
    Public Function get_Ligne()
        Return nbLignes
    End Function
    Public Function get_Colonne()
        Return nbColonnes
    End Function
    Public Function get_Theme()
        Return theme
    End Function
    Public Function get_Temps()
        Return temps
    End Function

    Public Function get_BoxBtn(i As Integer, j As Integer)
        Return grille(i)(j).btn
    End Function
    Public Function get_BoxLbl(i As Integer, j As Integer)
        Return grille(i)(j).lbl
    End Function
    Public Function get_Grille()
        Return grille
    End Function
    Public Function get_ListeVide()
        Return listeVide
    End Function
    Public Sub clear_ListVide()
        listeVide.Clear()
    End Sub
    'Methode de selection d'une case avec les fleches du clavier
    Public Function Next_Box(ligne As Integer, colonne As Integer, orientation As Keys)
        Dim locLigne As Integer = ligne
        Dim locColonne As Integer = colonne
        Dim i As Integer = 1
        If orientation = Keys.Up Then
            Do
                If locLigne - i >= 0 Then
                    locLigne -= i
                Else
                    Exit Do
                End If
            Loop Until grille(locLigne)(locColonne).get_Etat = Box.Etat.Inconnu
        ElseIf orientation = Keys.Right Then
            Do
                If locColonne + i <= nbColonnes - 1 Then
                    locColonne += i
                Else
                    Exit Do
                End If
            Loop Until grille(locLigne)(locColonne).get_Etat = Box.Etat.Inconnu
        ElseIf orientation = Keys.Down Then
            Do
                If locLigne + i <= nbLignes - 1 Then
                    locLigne += i
                Else
                    Exit Do
                End If
            Loop Until grille(locLigne)(locColonne).get_Etat = Box.Etat.Inconnu
        Else
            Do
                If locColonne - i >= 0 Then
                    locColonne -= i
                Else
                    Exit Do
                End If
            Loop Until grille(locLigne)(locColonne).get_Etat = Box.Etat.Inconnu
        End If
        Return locLigne * nbColonnes + locColonne
    End Function
    'Methode de découvre automatique d'une case approprié
    Public Function Demasque()
        For l As Integer = 0 To nbLignes - 1
            For c As Integer = 0 To nbColonnes - 1
                Dim cpt As Integer = 0
                If grille(l)(c).get_Etat = Box.Etat.Connu And grille(l)(c).get_Valeur = Box.Valeur.Nombre Then
                    For i As Integer = -1 To 1
                        For j As Integer = -1 To 1
                            If (i + l >= 0) And (j + c >= 0) And (i + l <= nbLignes - 1) And (j + c <= nbColonnes - 1) Then
                                If grille(l + i)(j + c).get_Etat = Box.Etat.Drapeau Then
                                    cpt += 1
                                End If
                            End If
                        Next
                    Next
                    If CInt(grille(l)(c).get_Label.Text) = cpt Then
                        For i As Integer = -1 To 1
                            For j As Integer = -1 To 1
                                If (i + l >= 0) And (j + c >= 0) And (i + l <= nbLignes - 1) And (j + c <= nbColonnes - 1) Then
                                    If grille(l + i)(c + j).get_Etat = Box.Etat.Inconnu Then
                                        'grille(l + i)(c + j).set_Etat(Box.Etat.Connu)
                                        Return (l + i) * nbColonnes + (c + j)
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If
            Next
        Next
        Return -1
    End Function
    Public Function Etat_Box(ligne As Integer, colonne As Integer)
        Return grille(ligne)(colonne).get_Etat
    End Function
    'Methode de marquage automatique par un drapeau d'une case approprié
    Public Function Marque_Drapeau()
        For l As Integer = 0 To nbLignes - 1
            For c As Integer = 0 To nbColonnes - 1
                Dim cpt As Integer = 0
                Dim tmp As Integer = 0
                If grille(l)(c).get_Etat = Box.Etat.Connu And grille(l)(c).get_Valeur = Box.Valeur.Nombre Then
                    For i As Integer = -1 To 1
                        For j As Integer = -1 To 1
                            If (i + l >= 0) And (j + c >= 0) And (i + l <= nbLignes - 1) And (j + c <= nbColonnes - 1) Then
                                If grille(l + i)(j + c).get_Etat <> Box.Etat.Connu Then
                                    If grille(l + i)(j + c).get_Etat = Box.Etat.Drapeau Then
                                        tmp += 1
                                    End If
                                    cpt += 1
                                End If
                            End If
                        Next
                    Next
                    If CInt(grille(l)(c).get_Label.Text) = cpt And tmp <> cpt Then
                        For i As Integer = -1 To 1
                            For j As Integer = -1 To 1
                                If (i + l >= 0) And (j + c >= 0) And (i + l <= nbLignes - 1) And (j + c <= nbColonnes - 1) Then
                                    If grille(l + i)(c + j).get_Etat = Box.Etat.Inconnu Then
                                        grille(l + i)(c + j).set_Etat(Box.Etat.Drapeau)
                                        Return (l + i) * nbColonnes + (c + j)
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If
            Next
        Next
        Return -1
    End Function
End Module
