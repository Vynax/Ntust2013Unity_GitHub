/*
 * @file LevelGeneration01.cs
 * @author NDark
 * @date 20130616 . file created.
 */
using UnityEngine;
using System.Collections;

public class LevelGeneration01 : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		InitializeLevel() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	private void InitializeLevel()
	{
		Debug.Log( "LevelGeneration01:InitializeLevel() start." ) ;
		
		InitializeChessBoard() ;
		Debug.Log( "LevelGeneration01:InitializeLevel() InitializeChessBoard() done." ) ;
		
		InitializeChessPawn() ;
		Debug.Log( "LevelGeneration01:InitializeLevel() InitializeChessPawn() done." ) ;
		
		Debug.Log( "LevelGeneration01:InitializeLevel() end." ) ;
	}
	
	private void InitializeChessBoard() 
	{
		Object chessBoardPrefab = Resources.Load( "Common/Prefabs/ChessBoard" ) ;
		if( null != chessBoardPrefab )
		{
			GameObject chessBoardObj = (GameObject) GameObject.Instantiate( chessBoardPrefab ) ;
			if( null != chessBoardObj )
			{
				chessBoardObj.name = "ChessBoard" ;
				chessBoardObj.transform.position = new Vector3( 5 , 0 , 5 ) ;
			}
		}
	}
	
	private void InitializeChessPawn() 
	{
		Object chessPawnPrefab = Resources.Load( "Common/Prefabs/ChessPawn" ) ;
		if( null != chessPawnPrefab )
		{
			GameObject chessPawnObj = (GameObject) GameObject.Instantiate( chessPawnPrefab ) ;
			if( null != chessPawnObj )
			{
				chessPawnObj.name = "ChessPawn" ;
				chessPawnObj.transform.position = new Vector3( 0.65f , 1 , 0.65f ) ;
			}
		}
	}	
}
