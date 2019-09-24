#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections;

public class WebGLBuilder : MonoBehaviour
{
    [MenuItem("File/Fucking Build")]
    static void Start()
    {
        //string[] env = System.Environment.GetCommandLineArgs();

        //string commitHash = env[7];

        string commitHash = "b4bd8e1e382b1b703f24e6247d55f2fb1f803011";

        string[] scenes = { "Assets\\Scenes\\MainMenu.unity", "Assets\\Scenes\\Grid.unity" };
        BuildPipeline.BuildPlayer(
            scenes,
            string.Format("C:\\Users\\Tyler Hekman\\Documents\\code\\suspense-ci\\builds\\{0}", commitHash),
            BuildTarget.WebGL,
            BuildOptions.None
        );
    }
}

#endif