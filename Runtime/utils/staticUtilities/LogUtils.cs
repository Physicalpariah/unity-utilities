//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

public static class LogUtils
{
    // Properties
    public static bool m_doesBasicLog = true;
    public static bool m_doesPriorityLog = true;
    public static bool m_doesWarningLog = true;
    public static bool m_doesIssueLog = true;
    public static bool m_doesTODOLog = true;

    // Initalisation Functions

    // Public Functions
    public static void ForceLog(object log)
    {
        Debug.Log(log);
    }

    public static void Log(object log, bool check = true)
    {
        if (!m_doesBasicLog) { return; }
        if (!check) { return; }
        Debug.Log("-LOG:" + log);
    }

    public static void LogPriority(object log, bool check = true)
    {
        if (!m_doesPriorityLog) { return; }
        if (!check) { return; }
        Debug.Log("-LOG:Priority: <color=#00BDF7>" + log + "</color>");

    }

    public static void LogIssue(object log, bool check = true)
    {
        if (!m_doesIssueLog) { return; }
        if (!check) { return; }
        Debug.Log("-LOG:Issue: <color=#C13F3F>" + log + "</color>");

    }

    public static void LogTodo(object log, bool check = true)
    {
        if (!m_doesTODOLog) { return; }
        if (!check) { return; }
        Debug.Log("-LOG:Todo <color=#FF00B1>" + log + "</color>");

    }

    public static void LogWarning(object log, bool check = true)
    {
        if (!m_doesWarningLog) { return; }
        if (!check) { return; }
        Debug.LogWarning(log);

    }

    public static void LogError(object log, bool check = true)
    {
        if (!check) { return; }
        Debug.LogError(log);

    }

    public static void LogNotImplemented(object log = null, bool check = true)
    {
        if (!check) { return; }
        Debug.LogError("Function not yet Implemented: " + log);
    }
    // Private Functions

}