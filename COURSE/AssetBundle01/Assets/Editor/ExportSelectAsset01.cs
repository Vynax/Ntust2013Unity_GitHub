/*
@file ExportSelectAsset01.cs
@author NDark
@date 20130821 file started.
*/
using UnityEngine;
using UnityEditor;

public class ExportSelectAsset01 : MonoBehaviour {

	[MenuItem ("Build/BuildSelectAssets")]
	static void BuildSelectAssets()
	{
		Debug.Log( "BuildSelectAssets()" + Selection.activeObject.name ) ;
		for( int i = 0 ; i < Selection.objects.Length ; ++i )
		{
			Debug.Log( Selection.objects[ i ] ) ;
		}
		// Build the resource file from the active selection.
		BuildPipeline.BuildAssetBundle(
			Selection.activeObject, 
			Selection.objects, 
			"BuildSelectedAssets_"+Selection.activeObject.name+".unity3d" ) ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
