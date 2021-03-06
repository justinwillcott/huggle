﻿//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Petrb

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3
{
    public partial class main : Form
    {
        /// <summary>
        /// Current page
        /// </summary>
        public static Page _CurrentPage;
        /// <summary>
        /// Current browser
        /// </summary>
        public static Controls.SpecialBrowser _CurrentBrowser;
        /// <summary>
        /// Current user
        /// </summary>
        public static user _CurrentUser;
        /// <summary>
        /// Queue
        /// </summary>
        public static Queue _CurrentQueue;
        /// <summary>
        /// Current edit
        /// </summary>
        public static Edit _CurrentEdit;
        /// <summary>
        /// Shows if we are currently rendering a diff
        /// </summary>
        public static bool DisplayingEdit;
        /// <summary>
        /// Config form
        /// </summary>
        private static ConfigForm config_form;
        public static bool DisplayingLast = false;
        
        public bool Localize()
        {
            Core.History("Main.Localize()");
            this.newTabToolStripMenuItem.Text = Languages.Get("main-browser");
            this.showNewMessagesToolStripMenuItem.Text = Languages.Get("main-system-messages");
            this.userToolStripMenuItem.Text = Languages.Get("main-user");
            this.showHistoryPageToolStripMenuItem.Text = Languages.Get("main-history");
            this.LContribs.Text = Languages.Get("main-contribs");
            this.systemToolStripMenuItem.Text = Languages.Get("main-system");
            this.lHistory.Text = Languages.Get("main-history");
            this.retrieveContributionsToolStripMenuItem.Text = Languages.Get("main-contribs");
            this.clearCurrentToolStripMenuItem.Text = Languages.Get("main-queue-clear");
            this.LUser.Text = Languages.Get("main-user");
            this.LabelP.Text = Languages.Get("main-page");
           
            return false;
        }

        public void Refresh_Interface()
        {
            // make me
            this.CurrentPage.ForeColor = System.Drawing.Color.Black;
            
            StatusBar("Ready");
        }

        public main()
        {
            InitializeComponent();
            systemWindowToolStripMenuItem.Visible = false;
            if (Config.devs)
            {
                systemWindowToolStripMenuItem.Visible = true;
            }
        }

        public int AlignForm()
        {
            Core.History("main.AlignForm()");
            contribsPanel.Width = this.Width - historyStrip.Left - 20;
            historyStrip.Width = this.Width - contribsPanel.Left - 20;
            lsLog.Columns[0].Width = lsLog.Width;
            return 1;
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Close(object sender, EventArgs e)
        {
            if (login.LoggedIn)
            {
                Core.ShutdownSystem();
            }
        }

        public void Browser_DisplayPage(Page _Page)
        {
            try
            {
                if (_Page != null && this.Visible)
                {
                    if (_Page !=  _CurrentPage)
                    {
                        _CurrentEdit = _Page.LastEdit;
                        if (_Page.LastEdit == null)
                        {
                            _CurrentEdit = new Edit();
                            
                            _CurrentEdit._Page = _Page;
                        }

                        Processing.DisplayEdit(_CurrentEdit);

                        if (_Page.LastEdit == Core.NullEdit)
                        {
                            
                        }
                        Refresh_Interface();

                    }
                }
            }
            catch(Exception B)
            {
                Core.ExceptionHandler(B);
            }
        }

        private void NotAvailable()
        {
            Log("Requested function is not yet available in this revision");
        }

        public void Log(string text)
        {
            try
            {
                lsLog.Items.Add(text);
                lsLog.Items[lsLog.Items.Count - 1].EnsureVisible();
            }
            catch (Exception ex)
            {
                Core.ExceptionHandler(ex);
            }
        }

        public void Draw_Contributions()
        {
            Core.History("Draw_Contributions()");
            contribsPanel.ThisUser = _CurrentUser;
            contribsPanel.Refresh();

            Edit Edit = _CurrentUser.LastEdit;
            int offset = contribsPanel.Width - 20 + (contribsPanel.Offset * 17);
        }

        private void main_Resize(object sender, EventArgs e)
        {
            AlignForm();
        }

        /// <summary>
        /// Set a text to the status bar
        /// </summary>
        /// <param name="text"></param>
        public void StatusBar(string text)
        {
            toolStripStatus.Text = text;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {
            Core.History("main.main_Load()");
            //init
            AlignForm();
            toolStripStatus.Text = "Loading";
            toolStripSInfo.Text = "Wiki: " + Config.Project + " edit rate: <waiting>";
            toolStripSUser.Text = Config.Username;
            _CurrentBrowser = webBrowser;
            OpenInfo();
            Localize();
            lsLog.Columns.Add("");
            Config.Initialised = true;
            DisplayingLast = true;
            lock (Core.SystemLog)
            {
                foreach (string message in Core.SystemLog)
                {
                    Log(message);
                }
            }
            
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Program.feed = new IRC(Config.IrcServer, Config.IrcPort, "huggle3", "#en.wikipedia");
        }

        /// <summary>
        /// Called when needed to render stuff
        /// </summary>
        public void RetrievePageContent()
        {
            Core.History("RetrievePageContent()");
            if (_CurrentPage.LastEdit != null)
            {
                _CurrentEdit = _CurrentPage.LastEdit;
                Processing.DisplayEdit(_CurrentPage.LastEdit);
            }
        }

        private void OpenInfo()
        {
            // startup page
            DisplayingLast = true;
            this.webBrowser.Navigate(Config.Projects[Config.Project] + Config.WikiPath + "index.php?title=" + Config.StartupPage + "&action=render");
            StatusBar("Done");
        }

        public bool Set_Current_Page(Page _page)
        {
            Core.History("Set_Current_Page()");
            if (_page != null)
            {
                if ((_page != _CurrentPage) && DisplayingLast)
                {
                    if (_page.LastEdit == null)
                    {
                        _CurrentEdit = new Edit();
                        _CurrentEdit._Page = _page;
                        _CurrentPage = _page;

                        Requests.request_read.history History = new Requests.request_read.history();
                        History.Page = _page;
                        History.Start(RetrievePageContent);
                    }
                }
                if (_page.LastEdit == Core.NullEdit)
                {
                    CurrentPage.ForeColor = Color.Red;
                    Refresh_Interface();
                }
                if (_CurrentPage == _page && _CurrentEdit.Id != null)
                {
                    if (Edit.All.ContainsKey(_CurrentEdit.Id))
                    {
                        historyStrip.NewerEdit = Edit.All[_CurrentEdit.Id];
                    }
                }
                if  ( ! CurrentPage.Items.Contains(_page.Name))
                {
                    CurrentPage.Items.Add(_page.Name);
                    if (CurrentPage.Items.Count > 20) // this needs to be in config
                    {
                        CurrentPage.Items.RemoveAt(0);
                    }
                }
            }
            return true;
        }

        public bool Set_Current_User(user _user)
        {
            if (_user == null)
            {
                return true;
            }
            _CurrentUser = _user;

            return true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Core.ShutdownSystem();
        }

        /// <summary>
        /// About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            huggle3.Forms.AboutForm about = new huggle3.Forms.AboutForm();
            about.Show();
        }

        private void CurrentUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Contribs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LContribs_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void CurrentUser_KeyPressed(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void LUser_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        /// <summary>
        /// Configuration form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (config_form == null)
            {
                config_form = new ConfigForm();
            }
            else if (config_form.IsDisposed)
            {
                config_form = new ConfigForm();
            }
            config_form.Show();
        }

        public void DisplayPage(string name)
        {
            Core.History("DisplayPage()");
            Page Page = new Page(CurrentPage.Text);
            Browser_DisplayPage(Page);
        }

        /// <summary>
        /// Draw
        /// </summary>
        public void Draw_History()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(Draw_History));
                return;
            }

            Core.History("Draw_History()");
            try
            {
                if (_CurrentPage != null)
                {
                    historyStrip._Page = _CurrentPage;
                    historyStrip.Refresh();

                    Edit Edit;
                    Edit = _CurrentPage.LastEdit;
                    int x = historyStrip.Width - 18 + (historyStrip.Offset * Config.ItemSize);
                    bool Enable_Scroll = false;
                    while (Edit != null && Edit != Core.NullEdit)
                    {
                        if (x < 0)
                        {
                            Enable_Scroll = true;
                            break;
                        }
                        x -= 17;
                        Edit = Edit.Prev;
                    }
                    if (_CurrentPage.FirstEdit == null)
                    { 
                        
                    }
                }
            }
            catch (Exception B)
            {
                Core.ExceptionHandler(B);
            }
        }

        private void tmQueueUpdt_Tick(object sender, EventArgs e)
        {
            // update queue
            queuePanel1.Redraw();
        }

        /// <summary>
        /// Someone press enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentPage_Trigger(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                 Set_Current_Page (Core.GetPage(CurrentPage.Text));
            }
        }

        private void CurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Current_Page(Core.GetPage(CurrentPage.Text));
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login.LoggedIn = false;
            Program._LoginForm.PrepareForm();
            Program._LoginForm.Visible = true;
            Core.InitConfig();
            Close();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void manageQueuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void myTalkPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable(); 
        }

        private void myContributionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void aIVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void revertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void goodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void sightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void nextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void latestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void acceptPendingRevisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void rejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void switchToTalkPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void viewLatestRevisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void retrieveHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void showHistoryPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void tagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void showUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void ignoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void retrieveContributionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void viewTalkPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void messageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void warnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void blockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void closeOtherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void viewThisInExternalBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void showNewEditsToPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void showNewContributionsByUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotAvailable();
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            int rpm = 0;
            int epm = 0;
            if (Config.devs)
            {
                toolStripSInfo.Text = "Wiki: " + Config.Project + " edit rate: <waiting> threadc: " + Core.Threading.ThCount.ToString();
            }
            else
            {
                toolStripSInfo.Text = "Wiki: " + Config.Project + " edit rate: " + rpm.ToString() + ", reverts: " + epm.ToString() +" per minute";
            }
        }

        private void systemWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Syslog form = new Forms.Syslog();
            form.Show();
        }

    }
}
