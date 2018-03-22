using UnityEngine;
using UnityEditor;

using System.Collections;

public class BuildBatch : MonoBehaviour {

    // build iOS app
    [UnityEditor.MenuItem("Tools/Build Project AllScene iOS")]
    private static void BuildiOS(){
        Debug.Log("##########iOS Build Start#########");

        //　全てのシーンを取得する
        EditorUserBuildSettings.SwitchActiveBuildTarget( BuildTargetGroup.iOS, BuildTarget.iOS );
        string[] allScene = new string[EditorBuildSettings.scenes.Length];
        int i = 0;
        foreach( EditorBuildSettingsScene scene in EditorBuildSettings.scenes ){
            Debug.Log ("scenePath(" + i + ")" + scene.path);
            allScene[i] = scene.path;
            i++;
        }

        // オプションの設定
        BuildOptions opt = BuildOptions.SymlinkLibraries |
        BuildOptions.AllowDebugging |
        BuildOptions.ConnectWithProfiler |
        BuildOptions.Development;

        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
        // Identifierは個人の環境に合わせる
        PlayerSettings.applicationIdentifier = "jp.co.AnimeMiru";
        // バージョンも必要に合わせて変更する
        PlayerSettings.bundleVersion = "1.0.0";
        PlayerSettings.statusBarHidden = true;
        string errorMsg_Device = BuildPipeline.BuildPlayer( 
            allScene,
            "iOSDevice",
            BuildTarget.iOS,
            opt
        );

        if (string.IsNullOrEmpty(errorMsg_Device)){
            Debug.Log ("##########Success iOS Device Build#########");
        } else {
            Debug.Log ("##########Failed iOS Device Build#########");
            Debug.Log (errorMsg_Device);
        }

        //シュミレーター用
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.SimulatorSDK;
        string errorMsg_Simulator = BuildPipeline.BuildPlayer(
            allScene, 
            "Simulator", 
            BuildTarget.iOS, 
            opt
        );
        if (string.IsNullOrEmpty(errorMsg_Simulator)){
            Debug.Log ("##########Success Simulator Build#########");
        } else {
            Debug.Log ("##########Failed Simulator Build#########");
            Debug.Log (errorMsg_Device);
        }

    }
}