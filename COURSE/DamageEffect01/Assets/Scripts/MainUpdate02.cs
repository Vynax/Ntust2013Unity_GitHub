/**
 * @file MainUpdate02.cs
 * @author NDark
 * @date 20130714 . file started.
 */
using UnityEngine;
using System.Collections.Generic;

public class MainUpdate02 : MonoBehaviour 
{
	public Dictionary< string , GameObject> m_Units = new Dictionary< string , GameObject>() ;
	
	bool m_PauseThisFrame = false ;
	
	public void RegisterGameUnit( GameObject _unitObj )
	{
		if( null == _unitObj )
			return ;
		
		if( false == m_Units.ContainsKey( _unitObj.name ) )
		{
			m_Units.Add( _unitObj.name , _unitObj ) ;
			Debug.Log( "MainUpdate02:RegisterGameUnit() m_Units.Add=" + _unitObj.name ) ;
		}
		
		
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
			

		UpdateUnitPosition() ;
	}
	
	private void UpdateUnitPosition()
	{
		Dictionary< string , GameObject>.Enumerator iMap = m_Units.GetEnumerator() ;
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
						obj.transform.Translate( velocity , Space.World ) ;
					}
					
					unitData.m_Velocity = Vector3.zero ;
				}
			}
		}
	}
	
}
