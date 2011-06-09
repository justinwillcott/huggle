﻿Imports Huggle.Net
Imports System
Imports System.Text

Namespace Huggle.Queries

    Friend Class MediaQuery : Inherits Query

        Private _Media As File
        Private _ThumbSize As Integer

        Public Sub New(ByVal session As Session, ByVal media As File)
            MyBase.New(session, Msg("media-desc", media))
            _Media = media
        End Sub

        Public Sub New(ByVal session As Session, ByVal media As File, ByVal thumbSize As Integer)
            MyBase.New(session, Msg("mediathumb-desc", media, thumbSize))
            _Media = media
            _ThumbSize = thumbSize
        End Sub

        Public ReadOnly Property Media() As File
            Get
                Return _Media
            End Get
        End Property

        Public ReadOnly Property ThumbSize() As Integer
            Get
                Return _ThumbSize
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("media-progress", Media))
            OnStarted()

            If Wiki.FileUrl IsNot Nothing Then
                'Upload location is determined by MD5 hash of file name
                Dim hash As String = HexString(MD5Hash(UTF8Encode(Media.Name)))
                Dim url As Uri

                If ThumbSize > 0 Then url = New Uri(Wiki.FileUrl.ToString & "thumb/" & hash(0) & "/" & _
                    hash(0) & hash(1) & "/" & Media.Name & "/" & Media.ThumbName(ThumbSize)) _
                    Else url = New Uri(Wiki.FileUrl.ToString & hash(0) & "/" & hash(0) & hash(1) & "/" & Media.Name)

                Dim req As New FileRequest(url)
                req.Start()
                If req.IsFailed Then OnFail(req.Result)

                If req.Response IsNot Nothing Then
                    If ThumbSize = 0 Then Media.Content = req.Response Else Media.Thumb(ThumbSize) = req.Response
                End If
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
