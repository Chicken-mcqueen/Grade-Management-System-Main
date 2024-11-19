﻿Imports MySql.Data.MySqlClient

Public Class ManageStudentAdmin
    Private addStudentForm As New AddStudentForm
    Private modifyStudentForm As New ModifyStudentForm
    Private connector As New DatabaseConnector
    Private Sub addStudent_Click(sender As Object, e As EventArgs) Handles addStudent.Click
        addStudentForm.Visible = True
    End Sub

    Private Sub ManageStudentAdmin_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Sub refreshButton_Click(sender As Object, e As EventArgs) Handles refreshButton.Click
        Try
            connector.connect.Open()
            connector.dataTable.Clear()
            connector.query = "SELECT * FROM student_info;"
            connector.command.Connection = connector.connect
            connector.command.CommandText = connector.query
            connector.dataAdapter.SelectCommand = connector.command
            connector.dataAdapter.Fill(connector.dataTable)
            dataView.DataSource = connector.dataTable
            connector.connect.Close()
            connector.command.Parameters.Clear()
        Catch ex As MySqlException
            connector.connect.Close()
            connector.command.Parameters.Clear()
            MessageBox.Show("Database Error")
        End Try
    End Sub

    Private Sub modifyStudent_Click(sender As Object, e As EventArgs) Handles modifyStudent.Click
        modifyStudentForm.Visible = True
    End Sub

    Private Sub searchStudent_Click(sender As Object, e As EventArgs) Handles searchStudent.Click
        Try
            Dim studentDetail = searchStudentField.Text
            connector.connect.Open()
            connector.dataTable.Clear()
            connector.query = "SELECT * FROM student_info WHERE CONCAT(fname,' ',mname,' ',lname) = ?fullname OR CONCAT(fname,' ',mname) = ?fullname OR CONCAT(fname,' ',lname) = ?fullname;"
            connector.command.Connection = connector.connect
            connector.command.CommandText = connector.query
            connector.command.Parameters.AddWithValue("?fullname", studentDetail)
            connector.dataAdapter.SelectCommand = connector.command
            connector.dataAdapter.Fill(connector.dataTable)
            dataView.DataSource = connector.dataTable
            connector.connect.Close()
            connector.command.Parameters.Clear()
        Catch ex As MySqlException
            connector.connect.Close()
            connector.command.Parameters.Clear()
            MessageBox.Show("Database Error")
        End Try
    End Sub

End Class