﻿//This is a source code or part of Huggle project
//
//This file contains code for

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

namespace huggle3
{
    public class LoginRequest : request_core.Request
    {
        public LoginForm Login_Fr;
        public override void Process()
        {
            try {
                Core.History("Login-Request.Process()");

                Core.WriteLog("Logging in as " + Config.Username);

                login.LoggedIn = false;

                LoginResult result;
                result = request_api.DoLogin();

                //If the login is not a success then try and find out what went wrong and give the relevant error message

                Core.WriteLog("Login resulted as " + result.ToString());
                if (result != LoginResult.Success)
                {
                    switch (result)
                    {
                        case LoginResult.Cancelled:
                            login.Error = Languages.Get("login-error-cancelled");
                            break;
                        case LoginResult.NotExists:
                            login.Error = Languages.Get("login-error-notexists");
                            break;
                        case LoginResult.WrongPass:
                            login.Error = Languages.Get("login-error-password");
                            break;
                        case LoginResult.Throttled:
                            login.Error = Languages.Get("login-error-throttled");
                            break;
                        case LoginResult.Blocked:
                            login.Error = Languages.Get("login-error-blocked");
                            break;
                        case LoginResult.NeedToken:
                            login.Error = Languages.Get("login-error-needtoken");
                            break;
                        case LoginResult.NoName:
                            login.Error = Languages.Get("login-error-noname");
                            break;
                        case LoginResult.EmptyPass:
                            login.Error = Languages.Get("login-error-emptypass");
                            break;
                            //There are still some cases this doesnt account for
                        default: // If it doesnt match any of the above give the default error message
                            login.Error = Languages.Get("login-error-unknown");
                            break;
                    }
                    login.Status = result;
                    login.LoggingOn = false;
                    return;
                }

                login.phase = login.LoginState.LoggedIn;
                login.LoggedIn = true;
                Complete();
            }
            catch (Exception B)
            {
                Core.ExceptionHandler(B);
                this.Fail();
            }
        }
    }
    
    static class login
    {
        public enum LoginState
        {
            LoggingIn,
            LoadingGlobal,
            LoadingLocal,
            Whitelist,
            LoggedIn,
            LoadedLocal,
            LoadedGlobal,
            Successful,
            Error
        }

        public static string Token;
        public static bool LoggedIn;
        public static bool LoggingOn;
        public static string Error = "";
        public static LoginState phase;
        public static request_core.Request.LoginResult Status;
    }
}
