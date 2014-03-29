/*
@file LevelGenerator.cs
@author NDark
@date 20140329 file started.
*/
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class LevelGenerator : MonoBehaviour 
{
	public string LevelFilepath 
	{
		get { return m_LevelFilepath ; } 
		set { m_LevelFilepath = value ;}
	}
	private string m_LevelFilepath = "" ;


	// Use this for initialization
	void Start () 
	{
		RetrieveLevelFilepath () ;
		LoadLevel( m_LevelFilepath ) ;
		GenerateObjectFromLevelData() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private void RetrieveLevelFilepath()
	{
		// m_LevelFilepath
	}

	public void LoadLevel( string _LevelFilepath )
	{
	}

	private void GenerateObjectFromLevelData()
	{

	}

}
