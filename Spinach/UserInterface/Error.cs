﻿//////////////////////////////////////////////////////////////////////////////////
//  Error.cs - Error handling class                                             //
//  ver 1.0                                                                     //
//                                                                              //
//  Language:      C#                                                           //
//  Platform:      Windows 7                                                    //
//  Application:   SPINACH                                                      //
//  Author:        Abhay Ketkar (asketkar@syr.edu)                              //
//                 (315) 439 7224                                               //
//////////////////////////////////////////////////////////////////////////////////
/*
 * Maintenance History:
 * ====================
 * version 1.0 : 4 Nov 09
 * - the initial version of the Error module
 */

using System;
using System.Collections.Generic;
using Spinach;

namespace Spinach
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void ErrorNotification(string Msg);
    public class ErrorModule
    {
        //FEO fo;
        public event ErrorNotification ConnError;
        public event ErrorNotification ProgConfError;
        public event ErrorNotification ProgWinError;
        private exec FEND;
        //private InterpreterVisitor CORE;
        //private PlotReceiver PLOT;

        private Dictionary<int, string> ErrorDict = new Dictionary<int, string>();

        //----< Create the Dictionary of Errors >----
        public ErrorModule()
        {
            // FrontEnd Error Messages
            ErrorDict.Add(101, "Syntax Error: ");
            ErrorDict.Add(102, "Exception: ");
            // CoreTeam Error Messages
            ErrorDict.Add(110, "");
            ErrorDict.Add(111, "");
            // PlotTeam Error Messages
            ErrorDict.Add(120, "");
            ErrorDict.Add(121, "");

        }

        //----< Sends Error Message to appropriate window >----
        public void ErrorMsg(int Code, string Msg)
        {
            string ErrMsg = ErrorDict[Code] + Msg;
            if (Code < 50 && ConnError != null)
              ConnError(ErrMsg);
            else if (Code < 100 && ProgConfError != null)
              ProgConfError(ErrMsg);
            else if (Code < 150 && ProgWinError != null)
              ProgWinError(ErrMsg);
        }

        public void SetFrontEndObject(exec fe)
        {
            FEND = fe;
            FEND.error_ += new exec.errorreport(ErrorMsg);
        }
/*        public void SetCoreObject(InterpreterVisitor c)
        {
            CORE = c;
            CORE.error_ += new InterpreterVisitor.errorreport(ErrorMsg);
        }
        public void SetPlotObject(PlotReceiver p)
        {
            PLOT = p;
            PLOT.error_ += new PlotReceiver.errorreport(ErrorMsg);
        }
        */
    }
}