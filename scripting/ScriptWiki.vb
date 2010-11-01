﻿Imports Huggle.Actions
Imports Huggle.Wikitext
Imports System
Imports System.Collections
Imports System.Collections.Generic

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function WikiFunc(ByVal context As Object, ByVal func As Token, _
            ByVal arg As Token(), ByVal original As Token()) As Token

            If TypeOf arg(0).Value Is Revision Then Return New Token(GetRevisionProperty(func.String.ToLower, arg))

            Select Case func.String
                'Infobox lookup operator
                'Case "::"
                '    Dim Page As Page

                '    If TypeOf Arg(0).Value Is Page Then Page = Arg(0).Page Else Page = Wiki.Pages.FromString(Arg(0).String)
                '    If Not Page.Exists Then Return New Token("<page does not exist>")

                '    If Not Page.InfoboxKnown Then
                '        InfoNeeded.Add(Page, "text")
                '        Return Undefined
                '    End If

                '    If Not Page.HasInfobox Then Return New Token("<no infobox>")

                '    Dim Key As String = Arg(1).String.ToLower
                '    If Page.Infobox.Params.ContainsKey(Key) Then Return New Token(Page.Infobox.Params(Key))

                '    For Each Param As KeyValuePair(Of String, List(Of String)) In Wiki.Config.ParamNorm
                '        If Param.Key = Key OrElse Param.Value.Contains(Key) Then
                '            If Page.Infobox.Params.ContainsKey(Param.Key) _
                '                Then Return New Token(Page.Infobox.Params(Param.Key))

                '            For Each Name As String In Param.Value
                '                If Page.Infobox.Params.ContainsKey(Name) _
                '                    Then Return New Token(Page.Infobox.Params(Name))
                '            Next Name
                '        End If
                '    Next Param

                '    Return New Token("<no param>")

                'Category properties
                Case "hidden"
                    Dim Cat As Category = CType(arg(0).Value, Category)

                    If Cat.Count = -1 Then
                        RequestInfo(Cat.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(Cat.IsHidden)
                    End If

                Case "subcatcount"
                    Dim Cat As Category = CType(arg(0).Value, Category)

                    If Cat.Count = -1 Then
                        RequestInfo(Cat.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(Cat.SubcatCount)
                    End If

                    'Media properties
                Case "isshared"
                    If arg(0).Media.Exists AndAlso Not arg(0).Media.DetailsKnown Then
                        RequestInfo(arg(0).Media.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Media.IsShared)
                    End If

                Case "firstrevision"
                    If arg(0).Media.Exists AndAlso Not arg(0).Media.DetailsKnown Then
                        RequestInfo(arg(0).Media.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Media.FirstRevision)
                    End If

                Case "lastrevision"
                    If arg(0).Media.Exists AndAlso Not arg(0).Media.DetailsKnown Then
                        RequestInfo(arg(0).Media.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Media.LastRevision)
                    End If

                Case "revisions"
                    If arg(0).Media.Exists AndAlso Not arg(0).Media.DetailsKnown Then
                        RequestInfo(arg(0).Media.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Media.Revisions))
                    End If

                Case "uploader"
                    If arg(0).Media.Exists AndAlso Not arg(0).Media.DetailsKnown Then
                        RequestInfo(arg(0).Media.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Media.Uploader)
                    End If

                    'Media revision properties
                Case "comment" : Return New Token(CType(arg(0).Value, FileRevision).Comment)
                Case "depth" : Return New Token(CType(arg(0).Value, FileRevision).Depth)
                Case "hash" : Return New Token(CType(arg(0).Value, FileRevision).Hash)
                Case "height" : Return New Token(CType(arg(0).Value, FileRevision).Height)
                Case "type" : Return New Token(CType(arg(0).Value, FileRevision).Type)
                Case "uploader" : Return New Token(CType(arg(0).Value, FileRevision).User)
                Case "url" : Return New Token(CType(arg(0).Value, FileRevision).Url)
                Case "width" : Return New Token(CType(arg(0).Value, FileRevision).Width)

                Case "metadata"
                    Dim Rev As FileRevision = CType(arg(0).Value, FileRevision)
                    Dim Result As New ScriptTable
                    Result.Columns = New List(Of String)
                    Result.Columns.Add("Name")
                    Result.Columns.Add("Value")
                    Result.ColumnTypes = New List(Of String)
                    Result.ColumnTypes.Add("String")
                    Result.ColumnTypes.Add("String")
                    Result.Rows = New List(Of ScriptTableRow)

                    For Each Item As KeyValuePair(Of String, String) In Rev.Metadata
                        Dim Row As New ScriptTableRow
                        Row.Columns = Result.Columns
                        Row.Items = New ArrayList
                        Row.Items.Add(Item.Key)
                        Row.Items.Add(Item.Value)
                        Result.Rows.Add(Row)
                    Next Item

                    Return New Token(Result)

                    'User properties
                Case "anonymous" : Return New Token(arg(0).User.IsAnonymous)

                Case "created"
                    If arg(0).User.Created = Date.MinValue Then
                        RequestInfo(arg(0).User, "created")
                        Return Undefined
                    Else
                        Return New Token(arg(0).User.Created)
                    End If

                Case "editcount"
                    If arg(0).User.Contributions = -1 Then
                        RequestInfo(arg(0).User, "editcount")
                        Return Undefined
                    Else
                        Return New Token(arg(0).User.Contributions)
                    End If

                Case "ignored" : Return New Token(arg(0).User.IsIgnored)
                Case "userpage" : Return New Token(arg(0).User.Userpage)

                    'Page properties
                Case "assessment"
                    If arg(0).Page.TalkPage.Exists AndAlso Not arg(0).Page.TalkPage.CategoriesKnown Then
                        RequestInfo(arg(0).Page, "assessment")
                        Return Undefined
                    Else
                        Return New Token(If(arg(0).Page.Assessment, ""))
                    End If

                Case "title" : Return New Token(arg(0).Page.Name)

                Case "namespace"
                    If arg(0).ValueType = "Page" Then
                        Return New Token(arg(0).Page.Space)
                    Else
                        Return New Token(Wiki.Spaces(CInt(arg(0).Number)))
                    End If

                Case "basename" : Return New Token(arg(0).Page.Name)

                Case "categories"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.CategoriesKnown Then
                        If Immediate Then
                            Dim req As New PageInfoQuery(Session, List(arg(0).Page), Categories:=True)
                            req.Start()
                            If req.Result.IsError Then Throw New ScriptException(req.Result.Message)
                            Return New Token(New ArrayList(arg(0).Page.Categories))
                        Else
                            RequestInfo(arg(0).Page, "categories")
                            Return Undefined
                        End If
                    Else
                        Return New Token(New ArrayList(arg(0).Page.Categories))
                    End If

                Case "creator"
                    If arg(0).Page.Exists AndAlso arg(0).Page.Creator Is Nothing Then
                        RequestInfo(arg(0).Page, "creation")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Page.Creator)
                    End If

                Case "externallinks"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.ExternalLinksKnown Then
                        RequestInfo(arg(0).Page, "externallinks")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.ExternalLinks))
                    End If

                Case "firstedit"
                    If arg(0).Page.Exists AndAlso arg(0).Page.FirstRev Is Nothing Then
                        RequestInfo(arg(0).Page, "creation")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Page.FirstRev)
                    End If

                Case "isarticle" : Return New Token(arg(0).Page.IsArticle)
                Case "isarticletalkpage" : Return New Token(arg(0).Page.IsArticleTalkPage)
                Case "isblp" : Return New Token(arg(0).Page.IsPriority)
                Case "isblptalk" : Return New Token(arg(0).Page.IsPriorityTalk)
                Case "isrootpage" : Return New Token(arg(0).Page.IsRootPage)
                Case "issubjectpage" : Return New Token(arg(0).Page.IsSubjectPage)
                Case "issubpage" : Return New Token(arg(0).Page.IsSubpage)
                Case "istalkpage" : Return New Token(arg(0).Page.IsTalkPage)

                Case "isredirect"
                    If arg(0).Page.LastRev Is Nothing Then
                        RequestInfo(arg(0).Page, "redirect")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Page.IsRedirect)
                    End If

                    'Case "hasinfobox"
                    '    If Arg(0).Page.Exists AndAlso Arg(0).Page.Infobox Is Nothing Then
                    '        RequestInfo(Arg(0).Page, "text")
                    '        Return Undefined
                    '    Else
                    '        Return New Token(Arg(0).Page.HasInfobox)
                    '    End If

                    'Case "infobox"
                    '    If Not Arg(0).Page.InfoboxKnown Then
                    '        InfoNeeded.Merge(Arg(0).Page, "text")
                    '        Return Undefined
                    '    Else
                    '        If Arg(0).Page.Infobox Is Nothing Then Return New Token("<no infobox>") _
                    '            Else Return New Token(Arg(0).Page.Infobox)
                    '    End If

                Case "langlinks"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.LangLinksKnown Then
                        RequestInfo(arg(0).Page, "langlinks")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.LangLinks))
                    End If

                Case "lastrev"
                    If arg(0).Page.Exists AndAlso arg(0).Page.LastRev Is Nothing Then
                        RequestInfo(arg(0).Page, "latest")
                        Return Undefined
                    Else
                        Return New Token(arg(0).Page.LastRev)
                    End If

                Case "links"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.LinksKnown Then
                        RequestInfo(arg(0).Page, "links")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.Links))
                    End If

                Case "mediaused"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.MediaKnown Then
                        RequestInfo(arg(0).Page, "media")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.Media))
                    End If

                Case "sections"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.SectionsKnown Then
                        RequestInfo(arg(0).Page, "sections")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.Sections))
                    End If

                Case "subject" : Return New Token(arg(0).Page.SubjectPage)

                Case "talk"
                    If arg(0).ValueType = "Page" Then Return New Token(arg(0).Page.TalkPage) _
                        Else Return New Token(arg(0).User.Talkpage)

                Case "target"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.TargetKnown Then
                        RequestInfo(arg(0).Page, "target")
                        Return Undefined
                    Else
                        If arg(0).Page.Target Is Nothing Then Return New Token(arg(0).Page) _
                            Else Return New Token(arg(0).Page.Target)
                    End If

                Case "templates"
                    If arg(0).Page.Exists AndAlso Not arg(0).Page.TranscludesKnown Then
                        RequestInfo(arg(0).Page, "templates")
                        Return Undefined
                    Else
                        Return New Token(New ArrayList(arg(0).Page.Transcludes))
                    End If

                Case "text"
                    If TypeOf arg(0).Value Is Revision Then
                        Dim Rev As Revision = arg(0).Revision

                        If Rev.Text Is Nothing Then
                            RequestInfo(Rev, "text")
                            Return Undefined
                        Else
                            Return New Token(Rev.Text)
                        End If

                    Else
                        Dim Page As Page = arg(0).Page

                        If Page.Exists AndAlso Page.Text Is Nothing Then
                            If Immediate Then
                                Dim req As New PageDetailQuery(Session, Page)
                                req.Start()
                                If req.Result.IsError Then Throw New ScriptException(req.Result.Message)
                                Return New Token(Page.Text)
                            Else
                                RequestInfo(Page, "text")
                                Return Undefined
                            End If
                        Else
                            Return New Token(Page.Text)
                        End If
                    End If

                    'Namespace properties
                Case "issubjectspace" : Return New Token(arg(0).Space.IsSubjectSpace)
                Case "istalkspace" : Return New Token(arg(0).Space.IsTalkSpace)
                Case "number" : Return New Token(arg(0).Space.Number)
                Case "subpages" : Return New Token(arg(0).Space.HasSubpages)

                    'Time properties
                Case "fullday" : Return New Token(arg(0).Time.Day.ToString & " " & _
                    Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames(arg(0).Time.Month - 1) & _
                    " " & arg(0).Time.Year.ToString)
                Case "fullweek" : Return New Token("Week " & (arg(0).Time.DayOfYear \ 7).ToString & " " & _
                    arg(0).Time.Year.ToString)
                Case "fullmonth" : Return New Token(Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames _
                    (arg(0).Time.Month - 1) & " " & arg(0).Time.Year.ToString)

                Case "second" : Return New Token(arg(0).Time.Second.ToString)
                Case "minute" : Return New Token(arg(0).Time.Minute.ToString)
                Case "hour" : Return New Token(arg(0).Time.Hour.ToString)
                Case "dayofweek" : Return New Token(CInt(arg(0).Time.DayOfWeek).ToString)
                Case "dayofmonth" : Return New Token(arg(0).Time.Day.ToString)
                Case "dayofyear" : Return New Token(arg(0).Time.DayOfYear.ToString)
                Case "dayname" : Return New Token(arg(0).Time.DayOfWeek.ToString)
                Case "week" : Return New Token((arg(0).Time.DayOfYear \ 7).ToString)
                Case "month" : Return New Token(arg(0).Time.Month.ToString)
                Case "monthname" : Return New Token(Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames(arg(0).Time.Month - 1))
                Case "year" : Return New Token(arg(0).Time.Year.ToString)

                    'Template instance properties
                    'Case "params"
                    '    Dim Result As New ScriptTable
                    '    Result.Rows = New List(Of ScriptTableRow)
                    '    Result.Columns = New List(Of String)
                    '    Result.Columns.Add("Parameter")
                    '    Result.Columns.Add("Value")
                    '    Result.ColumnTypes = New List(Of String)
                    '    Result.ColumnTypes.Add("String")
                    '    Result.ColumnTypes.Add("String")

                    '    Dim Infobox As Transclusion = CType(Arg(0).Value, Transclusion)

                    '    If Infobox.Params Is Nothing Then Return New Token(Result)

                    '    For Each Param As KeyValuePair(Of String, String) In Infobox.Params
                    '        If Param.Key <> "__type" Then
                    '            Dim Row As New TableRow
                    '            Row.Items = New ArrayList
                    '            Row.Columns = Result.Columns

                    '            Row.Items.Add(Param.Key)
                    '            Row.Items.Add(Param.Value)
                    '            Result.Rows.Add(Row)
                    '        End If
                    '    Next Param

                    '    Return New Token(Result)

                    'List requests
                Case "allpages" : Return ListQuery(context, New Lists.AllPagesQuery(Session, Wiki.Spaces(arg(0).String)), original)
                Case "backlinks" : Return ListQuery(context, New Lists.BacklinksQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "blps" : Return New Token(New ArrayList(Wiki.Pages.Priority))
                Case "category" : Return ListQuery(context, New Lists.CategoryQuery(Session, Wiki.Categories.FromString(arg(0).String)), original)
                Case "contribs" : Return ListQuery(context, New Lists.ContribsQuery(Session, Wiki.Users.FromString(arg(0).String)), original)
                Case "deletedcontribs" : Return ListQuery(context, New Lists.DeletedContribsQuery(Session, Wiki.Users.FromString(arg(0).String)), original)
                Case "deletedhistory" : Return ListQuery(context, New Lists.DeletedHistoryQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "externallinkusage" : Return ListQuery(context, New Lists.ExternalLinkUsageQuery(Session, arg(0).String), original)
                Case "history" : Return ListQuery(context, New Lists.HistoryQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "logs" : Return ListQuery(context, New Lists.LogsQuery(Session), original)
                Case "mediausage" : Return ListQuery(context, New Lists.MediaUsageQuery(Session, Wiki.Files(arg(0).String)), original)
                Case "links" : Return ListQuery(context, New Lists.LinksQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "prefix" : Return ListQuery(context, New Lists.PrefixQuery(Session, Wiki.Pages.FromString(arg(0).String).Space, Wiki.Pages.FromString(arg(0).String).Name), original)
                Case "redirects" : Return ListQuery(context, New Lists.RedirectsQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "subcats" : Return ListQuery(context, New Lists.SubcatsQuery(Session, Wiki.Categories.FromString(arg(0).String)), original)
                Case "search" : Return ListQuery(context, New Lists.SearchQuery(Session, arg(0).String), original)
                Case "templates" : Return ListQuery(context, New Lists.TemplatesQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "transclusions" : Return ListQuery(context, New Lists.TransclusionsQuery(Session, Wiki.Pages.FromString(arg(0).String)), original)
                Case "unreviewed" : Return ListQuery(context, New Lists.UnreviewedQuery(Session), original)
                Case "watchlist" : Return ListQuery(context, New Lists.WatchlistQuery(Session, Wiki.Users.FromString(arg(0).String)), original)

                    'Other
                Case "revs_allknown" : Return New Token(New ArrayList(Wiki.Revisions.All.Values))
                Case "users_allknown" : Return New Token(New ArrayList(Wiki.Users.All.ToArray))
                Case "pages_allknown" : Return New Token(New ArrayList(Wiki.Pages.All.Values))
                Case "ignoredusers" : Return New Token(New ArrayList(Wiki.Users.Ignored))

                Case "pagecount"
                    If Wiki.Pages.Count = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.Pages.Count)
                    End If

                Case "articlecount"
                    If Wiki.ContentPages = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.ContentPages)
                    End If

                Case "usercount"
                    If Wiki.Users.Count = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.Users.Count)
                    End If

                Case "admincount"
                    If Wiki.Administrators = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.Administrators)
                    End If

                Case "activeusercount"
                    If Wiki.ActiveUsers = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.ActiveUsers)
                    End If

                Case "mediacount"
                    If Wiki.Files.Count = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.Files.Count)
                    End If

                Case "revisioncount"
                    If Wiki.Revisions.Count = -1 Then
                        RequestInfo("stats")
                        Return Undefined
                    Else
                        Return New Token(Wiki.Revisions.Count)
                    End If

                Case "categorycount"
                    Dim cat As Category = Wiki.Categories.FromString(arg(0).String)

                    If cat.Count = -1 Then
                        RequestInfo(cat.Page, "info")
                        Return Undefined
                    Else
                        Return New Token(cat.Count)
                    End If

                    'Wikitext functions
                Case "wikitext" : Return New Token((arg(0).Value))


                Case "expand"
                    'Dim Str As String = cache.Get(Arg(0).String)

                    'If Str Is Nothing Then
                    '    InfoNeeded.Merge(Arg(0).String, "expand")
                    '    Return Undefined
                    'Else
                    '    Return New Token(Str)
                    'End If

                Case "strip" : Return New Token(WikiStripMarkup(arg(0).String))

                Case "paramnorm"
                    Dim Param As String = arg(0).String

                    For Each Item As KeyValuePair(Of String, List(Of String)) In Wiki.Config.ParamNorm
                        If Item.Key = Param OrElse Item.Value.Contains(Param) Then Return New Token(Item.Key)
                    Next Item

                    Return New Token(Param)

                    'Case "getparam" : Return New Token(GetParam(Arg(0).String, Wiki.Templates(Arg(1).String), Arg(2).String))

                    'Case "setparam"
                    '    Dim Template As Template = Project.Templates(Arg(1).String)

                    '    'Also check redirects to the template
                    '    If Template.Page.Exists AndAlso Not Template.Page.RedirectsKnown Then
                    '        If Not Immediate Then
                    '            InfoNeeded.Add(Template.Page, "redirects")
                    '            Return Undefined
                    '        End If

                    '        ListQuery(Context, New RedirectsQuery(Template.Page))
                    '    End If

                    '    Dim Text As String = Arg(0).String
                    '    Dim Result As String = SetParam(Text, Template, Arg(2).String, Arg(3).String)

                    '    If Result = Text Then
                    '        For Each Item As Page In Template.Page.Redirects
                    '            If Item.Space Is Project.Spaces.Template Then
                    '                Result = SetParam(Arg(0).String, Project.Templates(Item), Arg(2).String, Arg(3).String)
                    '                If Result <> Text Then Exit For
                    '            End If
                    '        Next Item
                    '    End If

                    '    Return New Token(Result)

                    'Case "addtemplate"
                    '    Dim Template As Template = Project.Templates(Arg(1).String)
                    '    Dim Params As New Dictionary(Of String, String)

                    '    For i As Integer = 2 To Arg.Length - 2 Step 2
                    '        Params.Merge(Arg(i).String, Arg(i + 1).String)
                    '    Next i

                    '    Dim TemplateCall As New TemplateCall(Template)
                    '    TemplateCall.Params = Params

                    '    Return New Token(AddTemplate(Arg(0).String, TemplateCall, False))

                    'Case "removetemplate"
                    '    Dim Template As Template = Project.Templates(Arg(1).String)

                    '    'Also check redirects to the template
                    '    If Template.Page.Exists AndAlso Not Template.Page.RedirectsKnown Then
                    '        If Not Immediate Then
                    '            InfoNeeded.Add(Template.Page, "redirects")
                    '            Return Undefined
                    '        End If

                    '        ListQuery(Context, New RedirectsQuery(Template.Page))
                    '    End If

                    '    Dim Text As String = Arg(0).String
                    '    Dim Result As String = RemoveTemplate(Text, Template)

                    '    If Result = Text Then
                    '        For Each Item As Page In Template.Page.Redirects
                    '            If Item.Space Is Project.Spaces.Template Then
                    '                Result = RemoveTemplate(Text, Project.Templates(Item))
                    '                If Result <> Text Then Exit For
                    '            End If
                    '        Next Item
                    '    End If

                    '    Return New Token(Result)

                    'Case "addcategory" : Return New Token(AddCategory(Arg(0).String, Project.Categories.FromString(Arg(1).String)))
                    'Case "removecategory" : Return New Token(RemoveCategory(Arg(0).String, Project.Categories.FromString(Arg(1).String)))
                    'Case "replacecategory" : Return New Token(ReplaceCategory(Arg(0).String, _
                    '    Project.Categories.FromString(Arg(1).String), Project.Categories(Arg(2).String)))
            End Select

            Return Nothing
        End Function

        Private Function GetRevisionProperty(ByVal prop As String, ByVal arg As Token()) As Token

            Dim rev As Revision = arg(0).Revision

            If rev Is Nothing Then Throw New ScriptException("Not a valid revision")
            If prop <> "exists" AndAlso rev.Exists.IsUndefined _
                Then Throw New ScriptException("Revision " & rev.Id & " does not exist")

            Select Case prop
                Case "author" : Return New Token(arg(0).Revision.User)
                Case "bytes" : Return MaybeLoadInfo(rev.Bytes, rev.Bytes < 0, rev, "bytes")
                Case "change" : Return MaybeLoadInfo(rev.Change, rev.Change = Integer.MinValue, rev, "change")
                Case "exists" : Return MaybeLoadInfo(rev.Exists, rev.Exists.IsUndefined, rev, "info")
                Case "id" : Return New Token(rev.Id)
                Case "issection" : Return MaybeLoadInfo(rev.IsSection, Not rev.DetailsKnown, rev, "info")
                Case "length" : Return MaybeLoadInfo(rev.Length, rev.Length < 0, rev, "text")
                Case "minor" : Return MaybeLoadInfo(rev.IsMinor, Not rev.DetailsKnown, rev, "info")
                Case "new" : Return MaybeLoadInfo(rev.IsCreation, Not rev.DetailsKnown, rev, "info")
                Case "page" : Return MaybeLoadInfo(rev.Page, rev.Page Is Nothing, rev, "info")
                Case "section" : Return MaybeLoadInfo(rev.Section, Not rev.DetailsKnown, rev, "info")
                Case "summary" : Return MaybeLoadInfo(rev.Summary, Not rev.DetailsKnown, rev, "info")
                Case "time" : Return MaybeLoadInfo(rev.Time, Not rev.DetailsKnown, rev, "info")
                Case "tool" : Return MaybeLoadInfo(If(rev.Tool Is Nothing, Nothing, rev.Tool.Name), Not rev.DetailsKnown, rev, "info")
            End Select

            Throw New ScriptException(prop & " is not a property of Revision")
        End Function

        Private Function MaybeLoadInfo(ByVal result As Object, ByVal condition As Boolean, _
            ByVal item As QueueItem, ByVal type As String) As Token

            If condition Then
                NextBatch.Add(New BatchInfo(Wiki, item, type))
                Return Undefined
            Else
                Return New Token(result)
            End If
        End Function

    End Class

End Namespace
