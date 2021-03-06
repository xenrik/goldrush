﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using System.Linq;

public class ImmediateConsole : EditorWindow {
    [MenuItem("Window/Immediate Console")]
    public static void Open() {
        GetWindow(typeof(ImmediateConsole), false, "Immediate");
    }

    private Queue<LogFrame> logMessages = new Queue<LogFrame>();
    private int maxLogSize = 1000;

    private Mutex mutex = new Mutex();
    private volatile bool isDirty;
    private string fullMessage;

    private Texture2D consoleBackground;

    private string command;
    private UnityELEvaluator evaluator;

    private void OnEnable() {
        InitialiseTextures();

        //Application.logMessageReceivedThreaded += HandleLog;
        Clear();

        evaluator = new UnityELEvaluator();
        evaluator.DefaultFunctionResolver = new UnityFunctionResolver(this);
        evaluator.ArgumentGroupEvaluator = new UnityArgumentGroupEvaluator();
    }

    private GameObject FindByName(string name) {
        return GameObject.Find(name);
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

    private void Update() {
        if (isDirty) {
            Repaint();
        }
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
                        buffer.Append("<color=#ffff00ff>[WARN]</color>  ");
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

        if (GUILayout.Button("Clear", EditorStyles.toolbarButton)) {
            Clear();
        }

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
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return) {
            try {
                if (command.Equals("cls")) {
                    Clear();
                } else {
                    object result = evaluator.Evaluate<object>(command);
                    if (result != null) {
                        result = ConvertToString(result);
                        Debug.Log(command + " => " + result);
                    } else {
                        Debug.Log(command);
                    }
                }
            } catch (System.Exception ex) {
                Debug.Log(ex);
            }
        }
    }

    private string ConvertToString(object o) {
        if (o is Quaternion) {
            return ((Quaternion)o).eulerAngles.ToString();
        } else {
            return o.ToString();
        }
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

    private class UnityFunctionResolver : StaticMethodsFunctionResolver {
        private static ImmediateConsole console;

        public UnityFunctionResolver(ImmediateConsole dynamicConsole) {
            UnityFunctionResolver.console = dynamicConsole;
        }

        public static GameObject FindByName(string name) {
            // Quick Search
            GameObject result = GameObject.Find(name);
            if (result != null) {
                return result;
            }

            // Try all objects including inactive ones
            Stack<GameObject> objectsToCheck = new Stack<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            foreach (GameObject go in scene.GetRootGameObjects()) {
                objectsToCheck.Push(go);
            }
            while (objectsToCheck.Count > 0) {
                GameObject go = objectsToCheck.Pop();
                if (go.name.Equals(name)) {
                    return go;
                }

                for (int i = 0; i < go.transform.childCount; ++i) {
                    objectsToCheck.Push(go.transform.GetChild(i).gameObject);
                }
            }

            return null;
        }

        public static Quaternion QuaternionFromEuler(float x, float y, float z) {
            return Quaternion.Euler(x, y, z);
        }
    }

    private class UnityArgumentGroupEvaluator: ArgumentGroupEvaluator {
        public object Evaluate(UnityELEvaluator context, ArgumentGroupToken group) {
            // Assume its a vector if there are three arguments
            if (group.Children.Count == 3) {
                float x = TypeCoercer.CoerceToType<float>(group, group.Children[0].Evaluate(context));
                float y = TypeCoercer.CoerceToType<float>(group, group.Children[1].Evaluate(context));
                float z = TypeCoercer.CoerceToType<float>(group, group.Children[2].Evaluate(context));

                return new Vector3(x, y, z);
            } else {
                return null;
            }
        }

        public object EvaluateForArgument(UnityELEvaluator context, string functionname, int argumentIndex, ArgumentGroupToken group) {
            // Just delegate to the normal Evaluate method for now
            return Evaluate(context, group);
        }
    }
}
