﻿//This is a source code or part of Huggle project
//
//This file contains code for reading

/// <DOCUMENTATION>
/// There is no documentation for this
/// </DOCUMENTATION>

//Copyright (C) 2011-2012 Huggle team
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.Text;

namespace huggle3.Requests
{
    public static class request_read 
    {
        public class diff : request_core.Request
        {
            /// <summary>
            /// Current handle
            /// </summary>
            public Controls.SpecialBrowser browsertab;
            public int Request_Count;
            public int Preload_Count;
            public string Diff;
            public int MaxSimultaneousR = 20;
            public Edit _Edit;


            public override void Process()
            {
                Core.History("request.diff()");
                Request_Count--;

                if (Request_Count >= MaxSimultaneousR)
                {
                    ThreadDone();
                    return;
                }

                if (browsertab == null)
                {
                    browsertab = main._CurrentBrowser;
                }
                _Edit.DiffCacheState = Edit.CacheState.Caching;
                string Old;
                Old = "prev";
                if (_Edit.Oldid != "-1")
                {
                    Old = _Edit.Oldid;
                }

                string QueryString;

                QueryString = Core.SitePath() + "index.php?title=" + System.Web.HttpUtility.UrlEncode(_Edit._Page.Name) + "&diff=" + _Edit.Id+ "&oldid=" + Old + "&uselang=en";

                if (Config.QuickSight != true || _Edit.Sighted)
                {
                    QueryString = QueryString + "&diffonly=1";
                }

                Diff = request_core.Request.RequestURL(QueryString);

                Complete();
            }

            public override void EndRequest()
            {
                Preload_Count--;
                Processing.Process_Diff(_Edit, Diff, browsertab);
                base.EndRequest();
            }
        }

        /// <summary>
        /// History of page
        /// </summary>
        public class history : request_core.Request
        { 
            public int FullTotal, FullLimit = 5000, BlockSize = Config.HistoryBlockSize;
            /// <summary>
            /// Page
            /// </summary>
            public Page Page;
            /// <summary>
            /// Full history or just a part
            /// </summary>
            public bool _full;
            public bool GetContent;
            

            public override void  EndRequest()
            {
                 Processing.Process_History(result.text, Page);
 	             base.EndRequest();
            }

            public override void  Process()
            {
                Core.History("history.Process()");
                ApiResult Result = new ApiResult();
                string Offset;
                Offset = Page.HistoryOffset;
                if (_full)
                {
                    Program.MainForm.Log(Languages.Get("history"));
                }

                while ( (_full || FullTotal < FullLimit) && Offset != "" )
                {
                    if (GetContent)
                    {
                        BlockSize = Math.Min(50, BlockSize);
                    }

                    string query_string = "action=query&prop=revisions&titles=" + System.Web.HttpUtility.UrlEncode(Page.Name) + "&rvlimit=" + BlockSize.ToString() + "&rvprops=ids|timestamp|user|comment";

                    if (GetContent)
                    {
                        query_string = query_string + "|content";
                    }
                    if (Offset != null)
                    {
                        query_string = query_string + "&rvstartid=" + Offset;
                    }
                    Result = ApiRequest(query_string);
                    if (Result.ResultInError)
                    {
                        Fail(Languages.Get("history-fail"), Page.Name);
                    }
                    if (_full)
                    {
                        FullTotal = FullTotal + BlockSize;
                        result = new request_core.Request_Result(result.text);
                        Program.MainForm.Log(Languages.Get("history-progress"));
                    }
                    Offset = GetParameter(Result.Result_Text, "rvstartid");
                    if (Offset == null)
                    {
                        Offset = "";
                    }
                }
                Complete(Text: Result.Result_Text);
            }
        }

        public class blocklog : request_core.Request
        {
            public user User;
            public override void Process()
            {
                Core.History("request_read");
            }
        }

        /// <summary>
        /// Browser request
        /// </summary>
        public class browser_html_data : request_core.Request
        {
            public Controls.SpecialBrowser browser;
            public Core.HistoryItem HistoryItem;
            public string address;

            public override void EndRequest()
            {
                Core.History("browser_html_data.Done");
                try
                {
                    browser.DocumentText = result.text;
                    browser.CurrentUrl = address;
                }
                catch (Exception A)
                {
                    Core.ExceptionHandler(A);
                }

                base.EndRequest();    
            }

            public override void Process()
            {
                Core.History("request_read.browser_html_data.Process()");
                string Result = "";
                try
                {
                browser = main._CurrentBrowser;
                if (Config.devs)
                {
                    Program.MainForm.Log(address);
                }
                Result = RequestURL(address);
                }catch (Exception ex)
                    {
                        Fail(ex.Source);
                    }

                if (Result == "")
                {
                    Fail();
                }
                
                Complete(null, Result);
            }
        }
    }
}
