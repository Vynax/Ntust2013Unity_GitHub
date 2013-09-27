/*
@file ModelAnimationDuplicate01.cs
@author NDark
@date 20130927 file started
*/
using UnityEngine;
using System.Collections.Generic ;

public class ModelAnimationDuplicate01 : MonoBehaviour 
{
	public int m_ModelDuplicatedNum = 2 ;
	public List<GameObject> m_ModelDuplicated = new List<GameObject>() ;
	public Material m_DuplicatedMaterial = null ;
	
	public int m_PositionQueueMaxNum = 30 ;
	public List<Vector3> m_PositionQueue = new List<Vector3>() ;
	public int m_PositionIntervalNum = 10 ;
	
	public string m_PlayAnimationString = "" ;
	public float m_AnimationSpeed = 0.1f ;
	
	public float m_AnimationIntervalRatio = 0.1f ;
	
	
	// Use this for initialization
	void Start () 
	{
		
		for( int i = 0 ; i < m_ModelDuplicatedNum ; ++i )
		{
			GameObject newObj = (GameObject) GameObject.Instantiate( 
				this.gameObject 
				/*, this.gameObject.transform.position + new Vector3( 1 , 0 , 0 ) , this.gameObject.transform.rotation */
				) ;
			
			ModelAnimationDuplicate01 removingScript = newObj.GetComponent<ModelAnimationDuplicate01>() ;
			Component.DestroyImmediate( removingScript ) ;
			
			newObj.AddComponent<PlayAnimationAtTime01>() ;
			
			newObj.name = this.gameObject.name + i.ToString() ;
			Renderer[] renderers = newObj.GetComponentsInChildren<Renderer>() ;
			for( int j = 0 ; j < renderers.Length ; ++j )
			{
				renderers[ j ].material = m_DuplicatedMaterial ;
			}
			
			m_ModelDuplicated.Add( newObj ) ;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != this.animation )
		{
			if( null != this.animation[ m_PlayAnimationString ] )
			{
				AnimationState animState = this.animation[ m_PlayAnimationString ] ;
				if( false == this.animation.IsPlaying( m_PlayAnimationString ) )
				{
					animState.speed = m_AnimationSpeed ;
					this.animation.Play( m_PlayAnimationString ) ;
				}
				else
				{
					UpdateDuplicatedModelAnimation( animState.time  , 
													animState.length * m_AnimationIntervalRatio ) ;
				}
			}
		}
		
		UpdateDuplicatedModelPosition() ;
		
		if( true == Input.GetKey( KeyCode.Space ) )
		{
			m_PlayAnimationString = "jump" ;
		}
	}
	
	private void UpdateDuplicatedModelAnimation( float _currentTime , float _Interval )
	{
		Debug.Log( "UpdateDuplicatedModelAnimation() " + _currentTime ) ;
		for( int i = 0 ; i < m_ModelDuplicated.Count ; ++i )
		{
			GameObject duplicatedObj = m_ModelDuplicated[ i ] ;
			
			PlayAnimationAtTime01 script = duplicatedObj.GetComponent<PlayAnimationAtTime01>() ;
			script.m_AnimationStr = m_PlayAnimationString ;
			float tryAnimTime = _currentTime - _Interval * ( i + 1 ) ;
			// Debug.Log( tryAnimTime ) ;
			if( tryAnimTime > 0 )
			{
				script.m_AnimationTime = tryAnimTime ;
			}
			else
				script.m_AnimationTime = tryAnimTime ;
			
			script.PlayAnimationAtSpecifiedTime() ;
		}
	}
	
	private void UpdateDuplicatedModelPosition()
	{
		// 先將自己目前位置推到queue裡面
		
		m_PositionQueue.Add( this.transform.position ) ;
		if( m_PositionQueue.Count >= m_PositionQueueMaxNum )
		{
			m_PositionQueue.RemoveAt( 0 ) ;
		}
		
		for( int i = 0 ; i < m_ModelDuplicated.Count ; ++i )
		{
			GameObject duplicatedObj = m_ModelDuplicated[ i ] ;
			
			int index = m_PositionQueue.Count - m_PositionIntervalNum * ( i + 1 ) ;
			// Debug.Log( index ) ;
			if( index < m_PositionQueue.Count )
			{
				duplicatedObj.transform.position = m_PositionQueue[ index ] ;
			}

		}
		
	}
}
