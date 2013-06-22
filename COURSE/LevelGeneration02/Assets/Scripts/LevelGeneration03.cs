/*
 * @file LevelGeneration02.cs
 * @author NDark
 * @date 20130616 . file created.
 */
using UnityEngine;
using System.Collections;

public class LevelGeneration02 : MonoBehaviour 
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
		Debug.Log( "LevelGeneration02:InitializeLevel() start." ) ;
		
		InitializeChessBoard() ;
		Debug.Log( "LevelGeneration02:InitializeLevel() InitializeChessBoard() done." ) ;
		
		InitializeChessPieces() ;
		Debug.Log( "LevelGeneration02:InitializeLevel() InitializeChessPieces() done." ) ;
		
		Debug.Log( "LevelGeneration02:InitializeLevel() end." ) ;
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
	
	private void InitializeChessPieces() 
	{
		float distanceOfABlock = 1.25f;
		float startX = 0.65f ;
		float startY = 0.65f ;
		
		int index = 0 ; 
		Vector3 pos = Vector3.zero ;
		Quaternion r180inY = Quaternion.AngleAxis( 180.0f , Vector3.up ) ;
		Quaternion rotation = Quaternion.identity ;
		for( int j = 0 ; j < 8 ; ++j )
		{
			if( j > 1 && j < 6 )
				continue ;
			if( j > 4 )
				rotation = r180inY ;
			else
				rotation = Quaternion.identity ;
				
			for( int i = 0 ; i < 8 ; ++i )
			{
				index = j * 8 + i ;
				pos = new Vector3( startX + i * distanceOfABlock , 1.0f , startY + j * distanceOfABlock ) ;
				InitializeChessPawn( index , pos , rotation ) ;
			}
		}
	}
	
	private void InitializeChessPawn( int index , Vector3 _Pos , Quaternion _Rotation ) 
	{
		Object chessPawnPrefab = Resources.Load( "Common/Prefabs/ChessPawn" ) ;
		if( null != chessPawnPrefab )
		{
			GameObject chessPawnObj = (GameObject) GameObject.Instantiate( chessPawnPrefab ) ;
			if( null != chessPawnObj )
			{
				chessPawnObj.name = "ChessPawn"  + index.ToString() ;
				chessPawnObj.transform.position = _Pos  ;
				chessPawnObj.transform.rotation = _Rotation ;
			}
		}
	}	
}
