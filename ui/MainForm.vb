﻿Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class MainForm

    Private ShutdownOnClose As Boolean = True

    Private Session As Session

    Public Sub New(ByVal session As Session)
        InitializeComponent()
        Me.Session = session
    End Sub

    Private Sub _FormClosed() Handles Me.FormClosed
        If ShutdownOnClose Then App.Shutdown()
    End Sub

    Private Sub _Load() Handles Me.Load
        Icon = Resources.Icon
        Text = Application.ProductName
        If Config.Local.WindowLocation <> Point.Empty Then Location = Config.Local.WindowLocation
        If Config.Local.WindowSize <> Size.Empty Then Size = Config.Local.WindowSize
        WindowState = If(Config.Local.WindowMaximized, FormWindowState.Maximized, FormWindowState.Normal)

        QueueSplit.Panel1.Controls.Add(New QueuePanel(Session.Wiki) With {.Dock = DockStyle.Fill})
        LogSplit.Panel2.Controls.Add(New LogPanel With {.Dock = DockStyle.Fill})
    End Sub

    Private Sub SystemExit_Click() Handles SystemExit.Click
        Close()
    End Sub

    Private Sub SystemLogout_Click() Handles SystemLogout.Click
        Dim logout As New Logout(Session)
        App.UserWaitForProcess(logout)
        If logout.IsFailed Then App.ShowError(logout.Result)

        Dim loginForm As New LoginForm
        loginForm.Show()
        ShutdownOnClose = False
        Close()
    End Sub

    Private Sub WikiProperties_Click() Handles WikiProperties.Click
        Dim form As New WikiPropertiesForm(Session.Wiki)
        form.Show()
    End Sub

    Private Sub AccountProperties_Click() Handles AccountProperties.Click
        Dim form As New AccountPropertiesForm(Session.User)
        form.Show()
    End Sub

    Private Sub HelpAbout_Click() Handles HelpAbout.Click
        Dim form As New AboutForm
        form.ShowDialog()
    End Sub

    Private Sub HelpManual_Click() Handles HelpManual.Click
        OpenWebBrowser(Config.Internal.ManualUrl)
    End Sub

    Private Sub AccountGlobalProperties_Click() Handles AccountGlobalProperties.Click
        Dim form As New GlobalUserPropertiesForm(Session.User.GlobalUser)
        form.Show()
    End Sub

    Private Sub WikiFamilyProperties_Click() Handles WikiFamilyProperties.Click
        Dim form As New FamilyPropertiesForm(Session.Wiki.Family)
        form.Show()
    End Sub

    Private Sub UserChangeGroups_Click() Handles UserChangeGroups.Click
        Dim form As New UserRightsForm(Session, Nothing)
        form.Show()
    End Sub

End Class