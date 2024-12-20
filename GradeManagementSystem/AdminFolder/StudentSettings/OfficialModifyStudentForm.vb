﻿Imports MySql.Data.MySqlClient
Public Class OfficialModifyStudentForm
    Private connector As New DatabaseConnector
    Private Sub OfficialModifyStudentForm_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Function getProgramID() As String
        Dim programID As String = ""
        Try
            connector.query = "SELECT * FROM program;"
            connector.command.Connection = connector.connect
            connector.command.CommandText = connector.query
            connector.dataAdapter.SelectCommand = connector.command
            connector.dataAdapter.Fill(connector.dataTable)
            ManageProgramAdmin.dataView.DataSource = connector.dataTable
            For Each row As DataGridViewRow In ManageProgramAdmin.dataView.Rows
                If (row.Cells("program_name").Value IsNot Nothing AndAlso row.Cells("program_name").Value.ToString.Equals(programComboBox.Text)) Then
                    programID = row.Cells("program_id").Value.ToString
                    Return programID
                End If
            Next
        Catch ex As MySqlException
            connector.connect.Close()
            MessageBox.Show("Database Error")
        End Try
        Return programID
    End Function

    Private Function birthDate() As String
        Dim birthday As String = birthCalendar.SelectionStart.Year.ToString & "-" & birthCalendar.SelectionStart.Month.ToString & "-" & birthCalendar.SelectionStart.Day.ToString
        Return birthday
    End Function

    Private Sub modifyStudentButton_Click(sender As Object, e As EventArgs) Handles modifyStudentButton.Click
        Try
            connector.connect.Open()
            connector.query = "UPDATE student SET lname = '" & lastnameTextBox.Text & "', fname = '" & firstnameTextBox.Text & "', mname = '" & middlenameTextBox.Text & "', birthdate = '" & birthDate() & "', program_id = '" & getProgramID() & "', year = " & yearComboBox.Text & ", section = '" & sectionComboBox.Text & "', email = '" & emailTextBox.Text & "'  WHERE id = " & sidTextBox.Text & ";"
            connector.command.Connection = connector.connect
            connector.command.CommandText = connector.query
            connector.command.ExecuteNonQuery()
            MessageBox.Show("Modified Successfully!")
            connector.connect.Close()
        Catch ex As MySqlException
            connector.connect.Close()
            MessageBox.Show("Database Error")
        End Try
        Me.Visible = False
    End Sub

End Class