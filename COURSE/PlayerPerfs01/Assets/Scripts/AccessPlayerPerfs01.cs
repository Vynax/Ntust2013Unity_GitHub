/*
 * @file AccessPlayerPerfs01.cs
 * @author NDark
 * @date 20130623 . file started.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccessPlayerPerfs01 : MonoBehaviour 
{
	public Dictionary<string,string> m_Map = new Dictionary<string,string>() ;
	public Rect m_WindowLeft = new Rect( 100 , 100 , 200 , 600 ) ;
	
	private string [] m_TempStringVector = null ;
	// Use this for initialization
	void Start () 
	{
		InitilaizaeKeys() ;
		RetrievePlayerPerfs() ;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea( m_WindowLeft , "" ) ;
		GUILayout.BeginHorizontal() ;
		
		GUILayout.BeginVertical() ;
		Dictionary<string,string>.Enumerator iMap = m_Map.GetEnumerator() ;
		while( iMap.MoveNext() ) 
		{
			GUILayout.Label( iMap.Current.Key ) ;			
		}
		GUILayout.EndVertical() ;

		GUILayout.BeginVertical() ;
		

		m_Map.Keys.CopyTo( m_TempStringVector , 0 ) ;
		
		string mapKey = "" ;
		string mapValue = "" ;
		string resultValue = "" ;			
		
		for( int i = 0 ; i < m_TempStringVector.Length ; ++i )
		{
			mapKey = m_TempStringVector[ i ] ;
			mapValue = m_Map[ mapKey ] ;
			resultValue = GUILayout.TextArea( mapValue ) ;			
			if( resultValue != mapValue )
			{
				m_Map[ mapKey ] = resultValue ;
			}
		}
		if( true == GUILayout.Button( "Apply" ) )
		{
			SetPlayerPerfs() ;
		}
		GUILayout.EndVertical() ;

		
		GUILayout.EndHorizontal() ;
		GUILayout.EndArea() ;
	}
	
	private void InitilaizaeKeys()
	{
		m_Map.Clear() ;
		m_Map.Add( "PlayerName" , "" ) ;
		m_Map.Add( "PlayerRace" , "" ) ;
		m_Map.Add( "PlayerLv" , "" ) ;
		m_Map.Add( "PlayerScore" , "" ) ;
		
		m_TempStringVector = new string[ m_Map.Count ] ;
	}
	
	private void RetrievePlayerPerfs()
	{
		Debug.Log( "AccessPlayerPerfs01::RetrievePlayerPerfs() start." ) ;						
		
		m_Map.Keys.CopyTo( m_TempStringVector , 0 ) ;
		
		string mapKey = "" ;
		string mapValue = "" ;
				
		for( int i = 0 ; i < m_TempStringVector.Length ; ++i )
		{
			mapKey = m_TempStringVector[i] ;
			if( true == PlayerPrefs.HasKey( mapKey ) )
			{
				mapValue = PlayerPrefs.GetString( mapKey ) ; 
				m_Map[ mapKey ] = mapValue ;
				Debug.Log( "AccessPlayerPerfs01::RetrievePlayerPerfs() " + 
					"key=" + mapKey + " value=" + mapValue ) ;
			}
		}
	}

	private void SetPlayerPerfs()
	{
		Dictionary<string,string>.Enumerator iMap = m_Map.GetEnumerator() ;
		while( iMap.MoveNext() ) 		
		{
			PlayerPrefs.SetString( iMap.Current.Key , iMap.Current.Value ) ;
			Debug.Log( "AccessPlayerPerfs01::SetPlayerPerfs() " + 
				"key=" + iMap.Current.Key + " value=" + iMap.Current.Value ) ;
		}
	}	
}

