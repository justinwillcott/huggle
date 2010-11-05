﻿Imports System.Collections.Generic
Imports System.Globalization

Namespace Huggle

    'Represents an entry in the log generated by MediaWiki's AbuseFilter extension
    'This is not integrated with other MediaWiki logs, but is treated as such here

    Friend Class Abuse : Inherits LogItem

        Private ReadOnly _Diff As String
        Private ReadOnly _Filter As AbuseFilter
        Private ReadOnly _Page As Page
        Private ReadOnly _Result As String
        Private ReadOnly _UserAction As String

        Friend Sub New(ByVal time As Date, ByVal user As User,
            ByVal filter As AbuseFilter, ByVal page As Page, ByVal result As String,
            ByVal userAction As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = "abuse"
            Me.Comment = ""
            Me.Time = time
            Me.User = user

            _Filter = filter
            _Filter.Hits.Add(Me)
            _Page = page
            _Result = result
            _UserAction = userAction

            user.RefreshState()

            'Find the associated edit, if any
            If userAction = "edit" AndAlso (result = "none" OrElse result = "tag") Then
                For Each item As Revision In user.Edits
                    If item.Page Is page Then
                        Rev = item
                        Rev.Abuse = Me
                        Exit For
                    End If
                Next item
            End If
        End Sub

        Friend Property Diff() As String
            Get
                Return _Diff
            End Get
            Set(ByVal value As String)
                Dim HeaderColor As String

                Select Case Page.Space
                    Case Wiki.Spaces.Article : HeaderColor = "ffffff"
                    Case Wiki.Spaces.User, Wiki.Spaces.UserTalk : HeaderColor = "d7d7ff"
                    Case Wiki.Spaces.Project, Wiki.Spaces.ProjectTalk : HeaderColor = "ffd2ff"
                    Case Wiki.Spaces.Talk : HeaderColor = "cdffcd"
                    Case Else : HeaderColor = "ffe6d2"
                End Select

                '_Diff = Resources.AbusePage _
                '    .Replace("$IMAGE", If(Page.IsBlp, BlpIconHtml, "")) _
                '    .Replace("$PAGETITLE", Page.Name) _
                '    .Replace("$DIFF", value) _
                '    .Replace("$HEADERCOLOR", HeaderColor) _
                '    .Replace("$FILTERINFO", Msg("diff-filterinfo", Filter.Id, Filter.Description)) _
                '    .Replace("$FILTERACTION", If(Stopped, _
                '        "<span class='huggle-filterstopped'>" & Msg("diff-filterstopped", Result) & "</span>", _
                '        Msg("diff-filteraction", Result)))
            End Set
        End Property

        Friend Property Rev() As Revision

        Friend ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Friend ReadOnly Property IsStopped() As Boolean
            Get
                Return (Result = "Disallow" OrElse Result = "Warn")
            End Get
        End Property

        Friend Overrides ReadOnly Property Label() As String
            Get
                Return "[" & Result & "] " & Filter.Id.ToStringForUser & " (" & Filter.Description & ")"
            End Get
        End Property

        Friend ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Friend ReadOnly Property Result() As String
            Get
                Return _Result
            End Get
        End Property

        Friend Overrides ReadOnly Property Target() As String
            Get
                Return User.Name
            End Get
        End Property

        Friend ReadOnly Property UserAction() As String
            Get
                Return _UserAction
            End Get
        End Property

    End Class

End Namespace
