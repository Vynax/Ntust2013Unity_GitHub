/*
@file SelectionSystem03.cs
@author NDark
@date 20130805 file started.
*/
using UnityEngine;

public class SelectionSystem03 : MonoBehaviour 
{
	public GUITexture m_GUISelection = null ;
	public GUITexture m_GUICursor = null ;
	
	public bool m_IsSelect = false ;
	public bool m_IsShowCursor = false ;
	public GameObject m_SelectObject = null ;
	public Vector3 m_SelectionPos = Vector3.zero ;
	public Vector3 m_CursorPos = Vector3.zero ;
	
	public void ResizeCursor( bool _Enable) 
	{
		GUI_ResizeTexture script = 
			m_GUICursor.gameObject.GetComponent<GUI_ResizeTexture>() ;
		if( null == script )
		{
			script = m_GUICursor.gameObject.AddComponent<GUI_ResizeTexture>() ;			
			script.m_MaximumSize = 78 ;
			script.m_StandardSize = 64 ;
			script.m_Direction = 0.6f ;
		}
		script.enabled = _Enable ;
	}
	
	public void ShowCursor( bool _Select ) 
	{
		EnableCursor( _Select ) ;
		m_IsShowCursor = _Select ;
	}
	
	public void Select( bool _Select , GameObject _TargetObject ) 
	{
		// case 0 無到有
		if( _Select != m_IsSelect && false == m_IsSelect )
		{
			EnableSelect( true ) ;
		}
		else if( _Select != m_IsSelect && true == m_IsSelect )
		{
			// 取消點選
			EnableSelect( false ) ;
		}
		else
		{
			// 改變對象
		}
		m_IsSelect = _Select ;
		m_SelectObject = _TargetObject ;
	}
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_GUISelection )
			InitGUISelection() ; 
		
		if( null == m_GUICursor )
		{
			InitGUICursor() ;
			ShowCursor( true ) ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSelectionPos() ;
		UpdateCursorPos() ;
	}
	
	private void UpdateSelectionPos()
	{
		float height = 0.5f ;
		if( null != m_GUISelection && 
			null != m_SelectObject &&
			true == m_IsSelect )
		{
			Vector3 viewportPos = 
				Camera.mainCamera.WorldToViewportPoint( m_SelectObject.transform.position + 
														m_SelectObject.transform.up * height ) ;
			// Debug.Log( viewportPos ) ;
			m_SelectionPos.Set( viewportPos.x , viewportPos.y , 0 ) ;
			m_GUISelection.transform.position = m_SelectionPos ;
		}
	}
	
	private void UpdateCursorPos()
	{
		if( null != m_GUISelection && 
			true == m_IsShowCursor )
		{
			Vector3 viewportPos = 
				Camera.mainCamera.ScreenToViewportPoint( Input.mousePosition ) ;
			
			// Debug.Log( Input.mousePosition ) ;
			m_CursorPos.Set( viewportPos.x , viewportPos.y , 0 ) ;
			m_GUICursor.transform.position = m_CursorPos ;			
		}
	}
	
	private void InitGUISelection()
	{
		GameObject guiSelectionObj = GameObject.Find( "GUI_Selection" ) ;
		if( null != guiSelectionObj )
		{
			m_GUISelection = guiSelectionObj.guiTexture ;
		}
	}
	
	private void InitGUICursor()
	{
		GameObject guiCursorObj = GameObject.Find( "GUI_Cursor" ) ;
		if( null != guiCursorObj )
		{
			m_GUICursor = guiCursorObj.guiTexture ;
		}
	}
	
	
	private void EnableSelect( bool _Enable )
	{
		if( null == m_GUISelection )
			return ;
		
		m_GUISelection.enabled = _Enable ;
	}	
	
	private void EnableCursor( bool _Enable )
	{
		if( null == m_GUICursor )
			return ;
		
		m_GUICursor.enabled = _Enable ;
	}
}
