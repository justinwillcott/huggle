﻿Imports System

Namespace Huggle

    'Represents deletion/restoration of a page

    Public Class Deletion : Inherits LogItem

        Private ReadOnly _Page As Page

        Public Sub New(ByVal time As Date, ByVal action As String, _
            ByVal page As Page, ByVal user As User, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = action
            Me.Comment = comment
            Me.Time = time
            Me.User = user

            _Page = page
            _Page.Exists = False
            _Page.DeletedEditsKnown = False
            _Page.HasDeletedEdits = True
        End Sub

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_delete
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _Page.Name
            End Get
        End Property

    End Class

End Namespace