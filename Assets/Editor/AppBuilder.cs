using UnityEditor;
using UnityEngine;

public class AppBuilder{

    [MenuItem ("Build/Android向けビルド")]
    static void buildForAndroid(){
        bumpBuildNumberForAndroid ();
        string errorMessage = BuildPipeline.BuildPlayer(GetAllScenePaths (), "/Users/plasticstraw/Desktop/game.apk", BuildTarget.Android, BuildOptions.None);
        if( !string.IsNullOrEmpty( errorMessage ) )
            Debug.LogError( "[Error!] " + errorMessage );
        else
            Debug.Log( "[Success!]" );
    }

    [MenuItem ("Build/iOS向けビルド")]
    static void buildForIOS(){
        bumpBuildNumberForIOS ();
        // 出力パス。絶対パスで指定すること。また、最後にスラッシュを入れないこと。PostBuildProcess に渡る path が通常ビルドと異なってしまい、思わぬバグを引き起こすことがあります。
        string path = "/Users/plasticstraw/Desktop/game";
        string errorMessage = BuildPipeline.BuildPlayer(GetAllScenePaths(), path, BuildTarget.iOS, BuildOptions.Il2CPP);
        if( !string.IsNullOrEmpty( errorMessage ) )
            Debug.LogError( "[Error!] " + errorMessage );
        else
            Debug.Log( "[Success!]" );
    }

    static void bumpBuildNumberForIOS(){
        string str = PlayerSettings.iOS.buildNumber;
        int num = int.Parse (str);
        num++;
        PlayerSettings.iOS.buildNumber = num + "";
    }

    static void bumpBuildNumberForAndroid(){
        PlayerSettings.Android.bundleVersionCode += 1;
    }

    static string[] GetAllScenePaths(){
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for( int i = 0; i < EditorBuildSettings.scenes.Length ; i++ ) {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}