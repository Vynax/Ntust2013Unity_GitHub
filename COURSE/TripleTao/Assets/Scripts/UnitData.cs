/*
@file UnitData.cs
@author NDark
@date 20130827 file started.
*/
using UnityEngine;
using System.Collections.Generic ;

public enum UnitType
{
	Grass = 0 ,
	Bush ,
}

public class UnitData : MonoBehaviour 
{
	static public Dictionary<UnitType,Material> s_AllMaterial = new Dictionary<UnitType,Material>() ;
	
	public int m_IndexI = 0 ;
	public int m_IndexJ = 0 ;
	public UnitType m_UnitType = UnitType.Grass ;
	
	public void Upgrade() 
	{
		Debug.Log( "UnitData::Upgrade()" ) ;
		if( m_UnitType == UnitType.Bush )
			return ;
		
		++m_UnitType ;
		SetMaterial( this.gameObject.renderer ) ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetMaterial( Renderer _Render )
	{
		if( false == s_AllMaterial.ContainsKey( m_UnitType ) )
		{
			Debug.LogError( "UnitData::SetMaterial() no material" ) ;
		}
		_Render.material = s_AllMaterial[ m_UnitType ] ;
	}	
	
	
}
