/*
@file UnitData.cs
@author NDark
@date 20140329 file started.
*/
#define TESTSKILL
using UnityEngine;
using System.Collections.Generic;

public enum UnitState
{
	UnActive = 0 ,
	Initialized ,
	Borning ,
	Active ,
	Idle ,
	Dying ,
	Dead ,
} ;

public enum UnitType
{
	UnDefine = 0 ,
	StaticObject ,
	Unit ,
	Player ,
} ;

public class UnitData : MonoBehaviour 
{
	public Vector3 GetCurrentPos() 
	{
		return this.gameObject.transform.position ;
	}

	public bool IsVisible()
	{
		return this.gameObject.renderer.enabled ;
	}

	public UnitState GetUnitState()
	{
		return m_UnitState ;
	}
	private UnitState m_UnitState = UnitState.UnActive ;

	public UnitType GetUnitType()
	{
		return m_UnitType ;
	}
	private UnitType m_UnitType = UnitType.UnDefine ;

	public string ProfileTextureName
	{
		get { return m_ProfileTextureName ; }
		set 
		{ 
			string set = value ;
			if( set != m_ProfileTextureName )
			{
				//reload?
				m_ProfileTextureName = set ;
			}
		}
	}
	private string m_ProfileTextureName = "" ;

	public Texture ProfileTexture
	{
		get { return m_ProfileTexture ; }
		set 
		{
			if( null != m_ProfileTexture )
			{

			}
			m_ProfileTexture = value ;
		}
	}
	private Texture m_ProfileTexture = null ;


	public Dictionary< string , Skill > m_Skills = new Dictionary<string, Skill>() ;

	// Use this for initialization
	void Start () 
	{
#if TESTSKILL
		Skill addSkill = new Skill() ;
		addSkill.DisplayString = "SaveSP" ;
		Debug.Log( "SaveSP" ) ;
		m_Skills.Add( "SaveSP" , addSkill ) ; 
#endif
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
