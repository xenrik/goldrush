using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine;
using UnityEditor;

public class DynamicConsole : EditorWindow {
    [MenuItem("Window/Dynamic Console")]
    public static void Open() {
        GetWindow(typeof(DynamicConsole), false, "Dynamic Console");
    }

    private Queue<LogFrame> logMessages = new Queue<LogFrame>();
    private int maxLogSize = 1000;

    private Mutex mutex = new Mutex();
    private volatile bool isDirty;
    private string fullMessage;

    private Texture2D consoleBackground;

    private string command;

    private void OnEnable() {
        InitialiseTextures();

        Application.logMessageReceivedThreaded += HandleLog;
        Clear();
    }

    private void InitialiseTextures() {
        // Textures seem to get lost when flipping between play and edit mode. If we
        // detect that we will re-initialise during the call to OnGUI

        if (consoleBackground == null) {
            consoleBackground = new Texture2D(1, 1);
            consoleBackground.SetPixels(new Color[] { new Color(0.85f, 0.85f, 0.85f, 1f) });
            consoleBackground.Apply();
        }
    }

    // Could be called on any thread!
    private void HandleLog(string logString, string stackTrace, LogType type) {
        if (mutex.WaitOne(1000)) {
            try {
                logMessages.Enqueue(new LogFrame(logString, stackTrace, type));
                while (logMessages.Count > maxLogSize) {
                    logMessages.Dequeue();
                }

                isDirty = true;
            } finally {
                mutex.ReleaseMutex();
            }
        } 
    }

    private void Clear() {
        logMessages.Clear();
        isDirty = true;
    }

    private string getFullMessage() {
        if (!isDirty) {
            return fullMessage;
        }

        if (mutex.WaitOne(1000)) {
            try {
                StringBuilder buffer = new StringBuilder();
                foreach (LogFrame frame in logMessages) {
                    switch (frame.type) {
                    case LogType.Assert:
                        buffer.Append("[ASSRT] ");
                        break;

                    case LogType.Error:
                        buffer.Append("<color=#cc0000ff>[ERROR]</color> ");
                        break;

                    case LogType.Exception:
                        buffer.Append("<color=#cc0000ff>[EXCEP]</color> ");
                        break;

                    case LogType.Log:
                        buffer.Append("[DEBUG] ");
                        break;

                    case LogType.Warning:
                        buffer.Append("<color=#ffff00ff>[ WARN]</color> ");
                        break;
                    }
                    buffer.AppendLine(frame.message);
                }

                fullMessage = buffer.ToString();
                isDirty = false;
            } finally {
                mutex.ReleaseMutex();
            }
        }

        return fullMessage;
    }

    private void OnGUI() {
        InitialiseTextures();
 
        GUILayout.BeginHorizontal(EditorStyles.toolbar);

        GUILayout.Button("Clear", EditorStyles.toolbarButton);
        GUILayout.Space(6);
        GUILayout.Button("Collapse", EditorStyles.toolbarButton);
        GUILayout.Button("Clear on Play", EditorStyles.toolbarButton);
        GUILayout.Button("Error Pause", EditorStyles.toolbarButton);
        GUILayout.FlexibleSpace();

        GUIContent content = EditorGUIUtility.IconContent("console.infoicon.sml");
        content.text = " 0";
        GUILayout.Button(content, EditorStyles.toolbarButton);

        content = EditorGUIUtility.IconContent("console.warnicon.inactive.sml");
        content.text = " 0";
        GUILayout.Button(content, EditorStyles.toolbarButton);

        content = EditorGUIUtility.IconContent("console.erroricon.inactive.sml");
        content.text = " 0";
        GUILayout.Button(content, EditorStyles.toolbarButton);

        GUILayout.EndHorizontal();

        GUIStyle style = new GUIStyle(GUI.skin.label) {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(4, 4, 4, 4),
            stretchWidth = true,
            stretchHeight = true,
            richText = true
        };
        style.normal.background = consoleBackground;
        style.active.background = consoleBackground;

        GUIContent consoleText = new GUIContent(getFullMessage());
        Rect rect = GUILayoutUtility.GetRect(consoleText, style);
        GUI.Label(rect, consoleText, style);

        style = new GUIStyle {
            stretchWidth = true
        };
        rect = GUILayoutUtility.GetRect(1, 1, style);
        GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);

        style = new GUIStyle(GUI.skin.label) {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(4, 4, 4, 4),
            stretchWidth = true,
            fixedHeight = 100,
            richText = true
};
        rect = GUILayoutUtility.GetRect(new GUIContent("Detail Goes Here"), style);
        GUI.Label(rect, "Detail Goes Here", style);

        style = new GUIStyle {
            stretchWidth = true
        };
        rect = GUILayoutUtility.GetRect(1, 1, style);
        GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);

        style = new GUIStyle(GUI.skin.textField) {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(4, 4, 4, 4),
            stretchWidth = true
        };
        rect = GUILayoutUtility.GetRect(new GUIContent(command), style);
        command = GUI.TextField(rect, command, style);
    }

    private struct LogFrame {
        public readonly string message;
        public readonly string stackTrace;

        public readonly LogType type;

        public LogFrame(string message, string stackTrace, LogType type) {
            this.message = message;
            this.stackTrace = stackTrace;
            this.type = type;
        }
    }
}
