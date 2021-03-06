/*
@file ExportSelectAsset02.cs
@author NDark
@date 20130821 file started.
*/
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class ExportSelectAsset02 : MonoBehaviour {
	

	[MenuItem ("Build/BuildWithExplicitAssetNames")]
	static void BuildWithExplicitAssetNames()
	{
		Debug.Log( "Selection.activeObject=" + Selection.activeObject.name ) ;
		
		for( int i = 0 ; i < Selection.objects.Length ; ++i )
		{
			Debug.Log( "Selection.objects[ i ]=" + Selection.objects[ i ] ) ;
			Debug.Log( "Selection.objects[ i ].GetType()=" + Selection.objects[ i ].GetType() ) ;
		}
		
		string [] pathes ;
		RetrieveFilePath( Selection.objects , out pathes ) ;
		
		for( int i = 0 ; i < pathes.Length ; ++i )
		{
			Debug.Log( "pathes[ i ]=" + pathes[ i ] ) ;
		}
		
		
		BuildPipeline.BuildAssetBundleExplicitAssetNames( 
			Selection.objects , 
			pathes ,
			"BuildWithExplicitAssetNames.unity3d" ) ;
		
		Debug.Log( "BuildWithExplicitAssetNames() completed." ) ;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	static private void RetrieveFilePath( object[] _Objects , out string [] _Pathes )
	{
		string [] pathes = new string[ _Objects.Length ] ;
		Debug.Log( _Objects.Length ) ;
		
		Queue<string> triversePathes = new Queue<string>() ;
		triversePathes.Enqueue( "Assets" ) ;// 加速
		string projectFullPath = Path.GetFullPath( triversePathes.Peek() ) + "\\" ;
		
		Debug.Log( "projectFullPath=" + projectFullPath ) ;
		
		while( triversePathes.Count > 0 )
		{
			string thisPath = triversePathes.Dequeue() ;
			// Debug.Log( "dequeue " + thisPath ) ;
			string fullPath = Path.GetFullPath( thisPath ) ;
			string []files = Directory.GetFiles( fullPath ) ;
			
			for( int i = 0 ; i < files.Length ; ++i )
			{
				FileInfo fi = new FileInfo( files[ i ] ) ;
				// 只檢查
				
				CheckFileExist( fi.Name , 
							    fi.FullName , 
							    _Objects , 
								ref pathes ) ;
			}
			
			
			
			string []dirs = Directory.GetDirectories( fullPath ) ;
			
			for( int j = 0 ; j < dirs.Length ; ++j )
			{
				// DirectoryInfo di = new DirectoryInfo( dirs[ j ] ) ;
				// Debug.Log( "push " + dirs[ j ] ) ;
				triversePathes.Enqueue( dirs[ j ] ) ;
			}
		}
		
		
		for( int k = 0 ; k < pathes.Length ; ++k )
		{
			// Debug.Log( "pathes[k] " + pathes[ k ] ) ;
			if( null == pathes[ k ] )
				continue ;
			
			// Debug.Log( k ) ; 
			int index = pathes[ k ].IndexOf( projectFullPath ) ;
			if( -1 != index )
			{
				pathes[ k ] = pathes[ k ].Remove( index , projectFullPath.Length ) ;
				
				// 去掉副檔名
				index = pathes[ k ].LastIndexOf( "." ) ;
				if( -1 != index )
				{
					pathes[ k ] = pathes[ k ].Substring( 0 , index ) ;
				}
				
			}
		}
		
		_Pathes = pathes ;
	}
				
	static void CheckFileExist( string _FileNameWithExt , string _Path , 
							    object[] _Objects , 
								ref string [] _Pathes )
	{
		string fileNameWithoutExt = Path.GetFileNameWithoutExtension( _FileNameWithExt ) ;
		string extention = Path.GetExtension( _FileNameWithExt ) ;
		
		// Debug.Log( "CheckFileExist() fileNameWithoutExt=" + fileNameWithoutExt ) ;
		// Debug.Log( "CheckFileExist() extention=" + extention ) ;
		
		bool match = false ;
		for( int i = 0 ; i < _Objects.Length ; ++i )
		{
			// Debug.Log( "CheckFileExist() _Objects[ i ] = " + _Objects[ i ].ToString() ) ;
			
			if( -1 != _Objects[ i ].ToString().IndexOf( fileNameWithoutExt ) )
			{
				if( typeof( UnityEngine.Texture2D ) == _Objects[ i ].GetType() &&
					( ".png" == extention || ".jpg" == extention ) )
				{
					match = true ;
				}
				else if( typeof( UnityEngine.GameObject ) == _Objects[ i ].GetType() &&
						 ( ".prefab" == extention ) )
				{
					match = true ;
				}
			}
				
			if( true == match )
			{
				_Pathes[ i ] = _Path ;
				Debug.Log( "checked!!! =" + _Path ) ;
				return ;
			}
			
		}
	}
}
