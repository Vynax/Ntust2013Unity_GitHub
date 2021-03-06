/*
@file OnGUI02.cs
@author NDark
@date 20130722 . file started.
*/
using UnityEngine;

public class OnGUI02 : MonoBehaviour 
{
	public Texture m_Texture1 = null ;
	public GUIStyle m_Style1 = new GUIStyle() ;
	public int m_DisplayPage = 0 ;
	 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_DisplayPage )
		{
		case 0 :
			if( true == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 1 ;
			break ;
		case 1 :
			if( false == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 2 ;
			break ;			
		case 2 :
			if( true == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 3 ;			
			break ;
		case 3 :
			if( false == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 0 ;			
			break ;
		}	
	}
	
	void OnGUI()
	{
		
		switch( m_DisplayPage )
		{
		case 0 :
			DrawGUIBox() ;
			break ;
		case 2 :
			DrawGUIButton() ;
			break ;	
			
		}		

	}
	
	private void DrawGUIBox()
	{
		GUILayout.Box( "Box1" ) ;	
		GUILayout.Box( "Box2" ) ;		
		GUILayout.Box( "" ) ;
		GUILayout.Box( "box4box4box4box4" ) ;
		GUILayout.Box( new GUIContent ( "box8" , "box tooltip" )) ;
		// GUILayout.Label( GUI.tooltip ) ;
		GUILayout.Box( "box9box9box9" , m_Style1 ) ;	
		
		
		Rect area = new Rect( 200 , 0 , 500 , 760 ) ;
		GUILayout.BeginArea( area ) ;
		GUILayout.Box( m_Texture1 ) ;
		GUILayout.Box( new GUIContent ( "box7" , m_Texture1 ) ) ;
		GUILayout.Box( m_Texture1 , m_Style1 ) ;	
		GUILayout.EndArea() ;
	}
	
	private void DrawGUIButton()
	{
		GUILayout.BeginHorizontal() ;
			GUILayout.Button( "Button1" ) ;
			GUILayout.Button( "Button2" ) ;
			
			GUILayout.BeginVertical() ;
				GUILayout.Button( "" ) ;
				GUILayout.Button( "button4button4" ) ;
			GUILayout.EndVertical() ;
			
			GUILayout.Button( new GUIContent ( "button8" , "button8 tooltip" ) ) ;
		
		GUILayout.EndHorizontal() ;
		
		GUILayout.Button( "box9box9box9" , m_Style1 ) ;	
		
		
		Rect area = new Rect( 0 , 200 , 1024 , 760 ) ;
		GUILayout.BeginArea( area ) ;
		
		GUILayout.BeginVertical() ;
			GUILayout.Button( m_Texture1 ) ;
			
			GUILayout.BeginHorizontal() ;
				GUILayout.Button( new GUIContent ( "button7" , m_Texture1 ) ) ;
				GUILayout.Button( m_Texture1 , m_Style1 ) ;
			GUILayout.EndHorizontal() ;
		GUILayout.EndVertical() ;
		
		GUILayout.EndArea() ;
		
	}	
	
}
