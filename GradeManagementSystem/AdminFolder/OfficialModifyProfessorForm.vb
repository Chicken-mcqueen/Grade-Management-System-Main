﻿Public Class OfficialModifyProfessorForm
    Private Sub OfficialModifyProfessorForm_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub
End Class