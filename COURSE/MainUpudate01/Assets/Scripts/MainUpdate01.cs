/**
 * @file MainUpdate01.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections.Generic;

public class MainUpdate01 : MonoBehaviour 
{
	public Dictionary< string , GameObject> m_Units = new Dictionary< string , GameObject>() ;
	public PauseController02 m_PauseScript = null ;
	bool m_PauseThisFrame = false ;
	
	public void RegisterGameUnit( GameObject _unitObj )
	{
		if( null == _unitObj )
			return ;
		
		if( false == m_Units.ContainsKey( _unitObj.name ) )
		{
			m_Units.Add( _unitObj.name , _unitObj ) ;
			Debug.Log( "MainUpdate01:RegisterGameUnit() m_Units.Add=" + _unitObj.name ) ;
		}
		
		
	}
	
	// Use this for initialization
	void Start () 
	{
		InitializePauseScriptPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_PauseThisFrame = ( null != m_PauseScript &&
			m_PauseScript.m_PauseState == 
			PauseController02.PauseState.OnPause ) ;
			

		UpdateUnitPosition() ;
	}
	
	private void UpdateUnitPosition()
	{
		Dictionary< string , GameObject>.Enumerator iMap = 
			m_Units.GetEnumerator() ;
		
		while( iMap.MoveNext() )
		{
			GameObject obj = iMap.Current.Value ;
			if( null != obj )
			{
				GameUnitData01 unitData = obj.GetComponent<GameUnitData01>() ;
				if( null != unitData )
				{
					Vector3 velocity = unitData.m_Velocity ;
					
					if( false == m_PauseThisFrame )
					{
						obj.transform.Translate( velocity ) ;
					}
					
					unitData.m_Velocity = Vector3.zero ;
				}
			}
		}
	}
	
	private void InitializePauseScriptPtr()
	{
		m_PauseScript = this.gameObject.GetComponent<PauseController02>() ;
		
		if( null == m_PauseScript )
		{
			Debug.LogError( "MainUpdate01:InitializePauseScriptPtr() null == m_PauseScript" ) ;
		}
		else
		{
			Debug.Log( "MainUpdate01:InitializePauseScriptPtr() end." ) ;
		}		
	}
}
