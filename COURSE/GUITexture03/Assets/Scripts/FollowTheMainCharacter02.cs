/**
 * @file FollowTheMainCharacter02.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections;

public class FollowTheMainCharacter02 : MonoBehaviour 
{
	public float m_Speed = 1.0f ;
	GameObject m_MainCharacter = null ;
	GameUnitData01 m_UnitData = null ;

	// Use this for initialization
	void Start () 
	{
		InitializeMainCharacterObjectPtr() ;
		InitializeUnitDataPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindTheVectorToMainCharacterAndGoToIt() ;
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "FollowTheMainCharacter02:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "FollowTheMainCharacter02:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void InitializeUnitDataPtr()
	{
		
		m_UnitData = this.GetComponent<GameUnitData01>() ;
		
		if( null == m_UnitData )
		{
			Debug.LogError( "FollowTheMainCharacter02:InitializeUnitDataPtr() null == m_UnitData" ) ;
		}
		else
		{
			Debug.Log( "FollowTheMainCharacter02:InitializeUnitDataPtr() end." ) ;
		}
	}		
	
	private void FindTheVectorToMainCharacterAndGoToIt()
	{
		if( null == m_MainCharacter )
			return ;
		
		Vector3 toMainCharacter = m_MainCharacter.transform.position - this.transform.position ;
		toMainCharacter.Normalize() ;
		Vector3 moveVec = toMainCharacter * ( m_Speed * Time.deltaTime ) ;
		if( null != m_UnitData )
		{
			m_UnitData.m_Velocity += moveVec ;
		}
		
	}
	

}
