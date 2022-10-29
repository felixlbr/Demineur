Public Class Theme

    Private backColor_Box, borderColor_Box, fontColor, flagColor As Color

    Public Sub New(backC_b As Color, borderC_b As Color, fontC As Color, flagC As Color)
        backColor_Box = backC_b
        borderColor_Box = borderC_b
        fontColor = fontC
        flagColor = flagC
    End Sub
    Public Function get_backColor_Box()
        Return backColor_Box
    End Function
    Public Function get_borderColor_Box()
        Return borderColor_Box
    End Function
    Public Function get_fontColor()
        Return fontColor
    End Function
    Public Function get_flagColor()
        Return flagColor
    End Function
End Class
