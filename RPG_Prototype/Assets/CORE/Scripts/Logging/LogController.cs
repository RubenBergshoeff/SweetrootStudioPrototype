using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.Networking;

public static class LogController {
    private const bool DEBUG = true;

    public static void LogAction(Instigator instigator, string action) {
        switch (Application.platform) {
            case RuntimePlatform.Android:
                if (File.Exists(GetAndroidLogPath()) == false) {
                    TextWriter tw = new StreamWriter(GetAndroidLogPath());
                    tw.WriteLine(DateTime.Now.ToShortDateString());
                    tw.Close();
                }
                File.AppendAllText(GetAndroidLogPath(),
                (DateTime.Now.ToLongTimeString() + " " + instigator + " " + action) + Environment.NewLine);
                if (DEBUG) Debug.Log("Logged action: " + instigator + action);
                break;
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsEditor:
                if (File.Exists(GetEditorLogPath()) == false) {
                    TextWriter tw = new StreamWriter(GetEditorLogPath());
                    tw.WriteLine(DateTime.Now.ToShortDateString());
                    tw.Close();
                }
                File.AppendAllText(GetEditorLogPath(),
                (DateTime.Now.ToLongTimeString() + " " + instigator + " " + action) + Environment.NewLine);
                if (DEBUG) Debug.Log("Logged action: " + instigator + " " + action);
                break;
            default:
                throw new NotImplementedException("Logging is not implemented for this platform");
        }
    }

    private static string GetEditorLogPath() {
        return Application.dataPath + "/Logs/Log" + DateTime.Now.ToShortDateString().Replace('/', '-') + ".log";
    }

    private static string GetAndroidLogPath() {
        return Application.persistentDataPath + "/Log" + DateTime.Now.ToShortDateString().Replace('/', '-') + ".log";
    }
}
