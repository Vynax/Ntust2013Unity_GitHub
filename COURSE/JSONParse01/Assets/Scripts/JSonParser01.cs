/*
 * @file JSonParser01.cs
 * @author NDark
 * @date 20130623 . file started.
 */
using UnityEngine;
using System.Collections;

public class JSonParser01 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level001" ;

	// Use this for initialization
	void Start () 
	{
		LoadLevelFile( m_LevelFilePath ) ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{

		Debug.Log( "JSonParser01:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( XmlDocument _Doc )
	{

		Debug.Log( "JSonParser01:ParseLevel() end." ) ;
	}
}
