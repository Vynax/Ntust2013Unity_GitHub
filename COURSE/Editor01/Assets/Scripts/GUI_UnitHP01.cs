/*
@file GUI_UnitHP01.cs
@brief 單位血條的GUI更新
@author NDark

@date 20130805 file started and copy from KobayashiMaruCommander
*/
using UnityEngine;

public class GUI_UnitHP01 : MonoBehaviour 
{
	public enum GUI_UnitHP01State
	{
		UnActive ,			// 未啟動
		Initialization ,	// 初始化 創造血條
		Active ,			// 啟動中
		Closed ,			// 已關閉
	}

	GUI_UnitHP01State m_State ;
	GameObject m_GUI_UnitHPObj = null ;
	private string m_GUI_UnitHPPrefabName = "Common/Prefabs/GUI_UnitHP" ;
	public Vector3 m_GUIPos = Vector3.zero ;
	
	
	public void Active_GUI_Unit()
	{
		if( m_State == GUI_UnitHP01State.UnActive )
		{
			m_State = GUI_UnitHP01State.Initialization ;
		}		
	}
	
	
	// Use this for initialization
	void Start () 
	{
		Active_GUI_Unit() ;
	}
	
	void OnDestroy()
	{
		if( null != m_GUI_UnitHPObj )
			GameObject.Destroy( m_GUI_UnitHPObj ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case GUI_UnitHP01State.UnActive :
			break ;
			
		case GUI_UnitHP01State.Initialization :
			CreateGUI_Unit_UnitIntagratyObject() ;// create gui ship ship intagraty object 
			UpdateGUI_UnitHP() ;
			
			m_State = GUI_UnitHP01State.Active ;
			break ;
		case GUI_UnitHP01State.Active :
			UpdateGUI_UnitHP() ;
			break ;
		case GUI_UnitHP01State.Closed :
			break ;			
		}
	}

	void CreateGUI_Unit_UnitIntagratyObject()
	{
		Object prefab = Resources.Load( m_GUI_UnitHPPrefabName ) ;
		if( null == prefab )
			return ;
		
		m_GUI_UnitHPObj = (GameObject) GameObject.Instantiate( prefab , 
															   Vector3.zero , 
															   Quaternion.identity ) ;
		if( null != m_GUI_UnitHPObj )
		{
			m_GUI_UnitHPObj.name = this.gameObject.name + ":" + "HP" ;
		}
	}
	
	
	void UpdateGUI_UnitHP()
	{
		if( null == m_GUI_UnitHPObj )
		{
			Debug.Log( " null == m_GUI_UnitHPObj " ) ;
			return ;
		}
		
		// update position follow unit object
		Vector3 screenPosition = Camera.mainCamera.WorldToViewportPoint( this.gameObject.transform.position ) ;
		
		m_GUIPos.Set( screenPosition.x , 
					  screenPosition.y , 
					  1.0f ) ;
		
		m_GUI_UnitHPObj.transform.position = m_GUIPos ;
	}
		
}
