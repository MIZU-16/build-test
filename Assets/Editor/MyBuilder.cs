using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MyBuilder {
	// ビルド実行でAndroidのapkを作成する例
	[UnityEditor.MenuItem("Tools/Build Project AllScene Android")]
	public static void BuildProjectAllSceneAndroid() {
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
		List<string> allScene = new List<string>();
		foreach( EditorBuildSettingsScene scene in EditorBuildSettings.scenes ){
			if (scene.enabled) {
				allScene.Add (scene.path);
			}
		}
		PlayerSettings.applicationIdentifier = "jp.co.altplus.sakura";
		PlayerSettings.companyName = "jp.co.altplus";
		PlayerSettings.productName = "sakura";
		PlayerSettings.bundleVersion = "1.0.0";
		PlayerSettings.statusBarHidden = true;
		BuildPipeline.BuildPlayer( 
				allScene.ToArray(),
				"newgame.apk",
				BuildTarget.Android,
				BuildOptions.None
		);
	}
	
	// ビルド実行でiOS用のXcodeプロジェクトを作成する例
	[UnityEditor.MenuItem("Tools/Build Project AllScene iOS")]
	public static void BuildProjectAllSceneiOS() {	
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.iOS, BuildTarget.iOS);
		List<string> allScene = new List<string>();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (scene.enabled) {
				allScene.Add (scene.path);
			}
		}

		BuildOptions opt = BuildOptions.SymlinkLibraries |
		                   BuildOptions.AllowDebugging |
		                   BuildOptions.ConnectWithProfiler |
		                   BuildOptions.Development;

		//BUILD for Device
		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
		PlayerSettings.applicationIdentifier = "jp.co.altplus.sakura";
		PlayerSettings.companyName = "jp.co.altplus";
		PlayerSettings.productName = "sakura";
		PlayerSettings.bundleVersion = "1.0.0";
		PlayerSettings.statusBarHidden = true;
		PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "jp.co.altplus.sakura");
		string errorMsg_Device = BuildPipeline.BuildPlayer (
							allScene.ToArray(),
							"iOS",
							BuildTarget.iOS,
							opt
		                         );

		if (string.IsNullOrEmpty (errorMsg_Device)) {
		} else {
			// エラー処理
		}
	}
}
