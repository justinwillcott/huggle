﻿Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Huggle

    Friend MustInherit Class Config

        Private Shared _BaseLocation As String
        Private Shared _Global As GlobalConfig
        Private Shared _Local As LocalConfig

        Private _IsLoaded As Boolean

        Private LocalMissing As Boolean

        Private Shared ReadOnly UpdateInterval As New TimeSpan(12, 0, 0)

        Protected MustOverride ReadOnly Property Location() As String
        Protected MustOverride Sub ReadConfig(ByVal text As String)
        Friend MustOverride Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)

        Friend Property Updated As Date

        Friend Shared ReadOnly Property [Global] As GlobalConfig
            Get
                If _Global Is Nothing Then _Global = New GlobalConfig
                Return _Global
            End Get
        End Property

        Friend Shared ReadOnly Property Local As LocalConfig
            Get
                If _Local Is Nothing Then _Local = New LocalConfig
                Return _Local
            End Get
        End Property

        Friend Shared ReadOnly Property BaseLocation() As String
            Get
                _BaseLocation = DetermineBaseLocation()
                Return _BaseLocation
            End Get
        End Property

        Private Shared Function GetValidCloudKey(ByVal str As String) As String
            Const validKeyChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_"

            For i As Integer = 0 To str.Length - 1
                If Not validKeyChars.Contains(str(i)) Then str = str.Replace(str(i), "_")
            Next i

            Return str
        End Function

        Friend ReadOnly Property IsCurrent() As Boolean
            Get
                Return Updated.Add(UpdateInterval) > Date.UtcNow
            End Get
        End Property

        Friend ReadOnly Property IsLoaded As Boolean
            Get
                Return _IsLoaded
            End Get
        End Property

        Protected Shared Function MakeConfig(ByVal items As Dictionary(Of String, Object)) As String
            If items Is Nothing Then Return ""
            Dim result As New StringBuilder

            Dim sortedKeys As New List(Of String)(items.Keys)
            sortedKeys.Sort(Function(x As String, y As String) String.Compare(x, y, StringComparison.Ordinal))

            For Each key As String In sortedKeys
                Dim item As Object = items(key)
                If item Is Nothing Then Continue For

                result.Append(key & ":")
                Dim str As String

                If TypeOf item Is Dictionary(Of String, Object) Then
                    str = MakeConfig(DirectCast(item, Dictionary(Of String, Object)))
                ElseIf TypeOf item Is List(Of String) Then
                    str = DirectCast(item, List(Of String)).Join(", ")
                ElseIf TypeOf item Is Date Then
                    str = CDate(item).ToString("u", CultureInfo.InvariantCulture)
                ElseIf TypeOf item Is Boolean Then
                    str = CBool(item).ToString.ToLowerI
                Else
                    str = Escape(item.ToString)
                End If

                If str.Contains(CRLF) Then result.Append(CRLF & Tab)
                result.Append(str.Replace(CRLF, CRLF & Tab) & CRLF)
            Next key

            result.Append(CRLF & CRLF)
            Return result.ToString
        End Function

        Protected Shared Function EscapeKey(ByVal key As String) As String
            Return Escape(key).Replace(":", "_")
        End Function

        Private Shared Function Escape(ByVal value As String) As String
            value = value.Replace("\", "\\").Replace("#", "\#").Replace(LF, "\n").Remove(CR)
            Return value
        End Function

        Private Shared Function Unescape(ByVal value As String) As String
            value = value.Replace("\#", "#").Replace("\n", LF).Replace("\\", "\")
            Return value
        End Function

        Protected Shared Function EscapeWs(ByVal value As String) As String
            If value.StartsWithI(" "c) Then value = "\@" & value
            If value.EndsWithI(" "c) Then value = value & "\@"
            Return value
        End Function

        Protected Shared Function UnescapeWs(ByVal value As String) As String
            If value.StartsWithI("\@") Then value = value.Substring(2)
            If value.EndsWithI("\@") Then value = value.Substring(0, value.Length - 2)
            Return value
        End Function

        Protected Shared Function ParseConfig(ByVal source As String, ByVal context As String, ByVal text As String) _
            As Dictionary(Of String, String)

            Dim result As New Dictionary(Of String, String)
            If text Is Nothing Then Return result

            Try
                Using reader As New StringReader(text)
                    Dim line As String = reader.ReadLine
                    Dim currentItem As New StringBuilder
                    Dim items As New List(Of String)
                    Dim indent As Integer

                    While line IsNot Nothing
                        Dim firstNonSpace As Integer = line.Length

                        For j As Integer = 0 To line.Length - 1
                            If Not Char.IsWhiteSpace(line(j)) Then firstNonSpace = j : Exit For
                        Next j

                        If Not (firstNonSpace = line.Length OrElse line(firstNonSpace) = "#") Then
                            Dim commentTest As Integer = line.Remove("\#").IndexOfI("#")
                            If commentTest > -1 Then line = line.Substring(0, commentTest)

                            If Char.IsWhiteSpace(line(0)) AndAlso firstNonSpace >= indent Then
                                If indent = 0 Then indent = firstNonSpace
                                currentItem.Append(LF & line.Substring(indent))

                            ElseIf line.Contains(":") Then
                                indent = 0
                                If currentItem.Length > 0 Then items.Add(currentItem.ToString)
                                currentItem = New StringBuilder(line.Trim)
                            End If
                        End If

                        line = reader.ReadLine
                    End While

                    If currentItem.Length > 0 Then items.Add(currentItem.ToString)

                    For Each item As String In items
                        Dim name As String = item.ToFirst(":").Trim
                        Dim value As String = Unescape(item.FromFirst(":").Trim)

                        If result.ContainsKey(name) Then Log.Debug("Duplicate definition of '{0}' in {1} config" _
                            .FormatI(If(context Is Nothing, name, context & ":" & name), source))

                        result.Merge(name, value)
                    Next item
                End Using

            Catch ex As SystemException
                Throw New ConfigException(Msg("error-config", context, source), ex)
            End Try

            Return result
        End Function

        Private Shared Function DetermineBaseLocation() As String
            'Try the current directory first
            Dim currentDirPath As String = PathCombine(Directory.GetCurrentDirectory, "config")
            If Directory.Exists(currentDirPath) Then Return currentDirPath

            Try
                Directory.CreateDirectory(currentDirPath)
                Return currentDirPath

            Catch ex As SystemException
                'nom
            End Try

            'Then try AppData
            Dim appDataPath As String = PathCombine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "huggle", "config")

            If Directory.Exists(appDataPath) Then Return appDataPath

            Try
                Directory.CreateDirectory(appDataPath)
                Return appDataPath

            Catch ex As SystemException
                'nom
            End Try

            Throw New ConfigException("No suitable location for configuration data")
        End Function

        Friend Sub LoadCloud()
            Dim cloudQuery As New CloudQuery(Config.GetValidCloudKey(Location))
            cloudQuery.Start()

            If cloudQuery.IsFailed Then
                Log.Write(cloudQuery.Result.LogMessage)
            Else
                If cloudQuery.Value Is Nothing Then
                    Log.Debug("Not found in cloud: {0}".FormatI(Location))
                Else
                    Log.Debug("Load from cloud: {0}".FormatI(Location))
                    Load(cloudQuery.Value)
                End If
            End If
        End Sub

        Friend Overridable Sub LoadLocal()
            If Not LocalMissing Then Load(LoadFile(Location))
            If Not IsLoaded Then LocalMissing = True
        End Sub

        Friend Overridable Sub Load(ByVal text As String)
            If text Is Nothing Then Return

            ReadConfig(text)
            _IsLoaded = True
        End Sub

        Protected Shared Function LoadFile(ByVal location As String) As String
            Try
                Dim filePath As String = PathCombine(BaseLocation, location & ".txt")
                Dim contents As String = IO.File.ReadAllText(filePath, Encoding.UTF8)

                Log.Debug("Load from local: {0}".FormatI(location))
                Return contents

            Catch ex As FileNotFoundException
                Log.Debug("Not found in local: {0}".FormatI(location))

            Catch ex As DirectoryNotFoundException
                Log.Debug("Not found in local: {0}".FormatI(location))

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).Wrap(Msg("config-localloaderror", location)).LogMessage)
            End Try

            Return Nothing
        End Function

        Friend Sub SaveCloud()
            Dim cloudQuery As New CloudStore(Config.GetValidCloudKey(Location), Save(ConfigTarget.Cloud))
            cloudQuery.Start()

            If cloudQuery.IsFailed Then
                Log.Write(cloudQuery.Result.LogMessage)
            Else
                Log.Debug("Save to cloud: {0}".FormatI(Location))
            End If
        End Sub

        Friend Overridable Sub SaveLocal()
            Config.SaveFile(Location, Save(ConfigTarget.Local))
            LocalMissing = False
        End Sub

        Protected Function Save(ByVal target As ConfigTarget) As String
            Return MakeConfig(WriteConfig(target))
        End Function

        Protected Shared Sub SaveFile(ByVal location As String, ByVal contents As String)
            Try
                Dim filePath As String = PathCombine(Config.BaseLocation, location & ".txt")

                If Not Directory.Exists(Path.GetDirectoryName(filePath)) _
                    Then Directory.CreateDirectory(Path.GetDirectoryName(filePath))

                IO.File.WriteAllText(filePath, contents, Encoding.UTF8)
                Log.Debug("Save to local: {0}".FormatI(location))

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).Wrap(Msg("config-localsaveerror", location)).LogMessage)
            End Try
        End Sub

    End Class

    <Serializable()>
    Friend Class ConfigException : Inherits HuggleException

        Friend Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Friend Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

    Friend Enum ConfigTarget As Integer
        : Local : Cloud : Wiki
    End Enum

End Namespace
