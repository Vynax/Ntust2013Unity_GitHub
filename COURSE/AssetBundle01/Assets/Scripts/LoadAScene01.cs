/*
@file LoadAScene01.cs
@author NDark
@date 20130821 file started.
*/
using UnityEngine;

public class LoadAScene01 : MonoBehaviour 
{
	public string m_Unity3DURL = "file:///"
		+ "D:/Workspace/Project/Ntust2013Unity/COURSE/AssetBundle01/" 
		+ "TargetScenes_TestScene.unity3d" ;
	public string m_SceneNameInBundle = "Test" ;
	// Use this for initialization
	void Start () 
	{
		Debug.Log( m_Unity3DURL ) ;
		WWW download = WWW.LoadFromCacheOrDownload( m_Unity3DURL , 0 ) ;
		if( null == download )
		{
			Debug.LogError( "null == download" ) ;
		}
		else
		{
			
            AssetBundle assetBundle = null ;
			
			// Try mark this line, you will find you can't load the scene.
			assetBundle = download.assetBundle;
			
			if( null == assetBundle )
			{
				Debug.Log( "null == assetBundle" ) ;
			}
			else
			{
				Debug.Log( "null != assetBundle" ) ;
				// assetBundle.Load
				Application.LoadLevel( m_SceneNameInBundle ) ;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
