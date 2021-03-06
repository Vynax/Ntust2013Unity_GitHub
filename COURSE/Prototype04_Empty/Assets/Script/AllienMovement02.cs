/**
@file AllienMovement02.cs
@author NDark
@date 20130717 . file started.
*/
using UnityEngine;

public class AllienMovement02 : MonoBehaviour 
{
	
	public enum AllienMovement02State
	{
		UnActive ,
		Forward1 ,
		Left , 
		Forward2 ,
		Right ,
	}
	
	public GameUnitData02 m_UnitDataPtr = null ;
	public float m_Speed = 0.005f ;
	public float m_MoveDistanceNow = 0.0f ;
	public float m_MoveDistanceThreashold = 0.5f ;
	public AllienMovement02State m_State = AllienMovement02State.UnActive ;
	
	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case AllienMovement02State.UnActive :
			InitializeGameUnitDataPtr() ;
			m_MoveDistanceNow = 0 ;
			this.m_State = AllienMovement02State.Forward1 ;
			break ;
			
		case AllienMovement02State.Forward1 :
		case AllienMovement02State.Forward2 :
			if( null == m_UnitDataPtr )
				break ;
			
			m_UnitDataPtr.m_Velocity += -1 * m_Speed * this.transform.forward ;
			m_MoveDistanceNow += m_UnitDataPtr.m_Velocity.magnitude ;
			if( m_MoveDistanceNow > m_MoveDistanceThreashold )
			{
				m_MoveDistanceNow = 0 ;
				this.m_State = (AllienMovement02State) (((int)this.m_State)+1) ;
			}
			break ;
			
		case AllienMovement02State.Left :
			m_UnitDataPtr.m_Velocity += -1 * m_Speed * this.transform.right ;
			m_MoveDistanceNow += m_UnitDataPtr.m_Velocity.magnitude ;
			if( m_MoveDistanceNow > m_MoveDistanceThreashold )
			{
				m_MoveDistanceNow = 0 ;
				this.m_State = AllienMovement02State.Forward2 ;
			}
			break ;
			
		case AllienMovement02State.Right :
			m_UnitDataPtr.m_Velocity += m_Speed * this.transform.right ;
			m_MoveDistanceNow += m_UnitDataPtr.m_Velocity.magnitude ;
			if( m_MoveDistanceNow > m_MoveDistanceThreashold )
			{
				m_MoveDistanceNow = 0 ;
				this.m_State = AllienMovement02State.Forward1 ;
			}
			break ;
		}
	}
	
	private void InitializeGameUnitDataPtr()
	{
		m_UnitDataPtr = this.gameObject.GetComponent<GameUnitData02>() ;
		if( null == m_UnitDataPtr )
		{
			Debug.LogError( "AllienMovement02::InitializeGameUnitDataPtr() ,null == m_UnitDataPtr" ) ;
		}
		else
		{
			Debug.Log( "AllienMovement02::InitializeGameUnitDataPtr() end." ) ;
		}
	}
}
