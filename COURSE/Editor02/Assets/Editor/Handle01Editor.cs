/**
@file Handle01Editor.cs
@author NDark
@date 20130819 . file started
*/
using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof(Handle01) ) ]
public class Handle01Editor : Editor 
{
	
	// change layout of Inspector
    public override void OnInspectorGUI() 
	{
		// target is the class of ExecuteInEditMode01
		Handle01 targetWithType = (Handle01)target ;
		
        if( GUI.changed )
		{
			EditorUtility.SetDirty( targetWithType );
		}
    }	
	
    public void OnSceneGUI () 
	{
		Handle01 targetWithType = (Handle01)target ;
		
        if (GUI.changed)
		{
            EditorUtility.SetDirty (targetWithType);
		}
    }		
}

