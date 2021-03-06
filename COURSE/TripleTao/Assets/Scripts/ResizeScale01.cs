/*
@file ResizeScale01.cs
@author NDark
@date 20130823 file started.
*/
using UnityEngine;

public class ResizeScale01 : MonoBehaviour {
	
	public bool m_Active = false ;
	public Vector3 m_InitScale = Vector3.zero ;
	public Vector3 m_TargetScale = new Vector3( 0.6f , 0.06f , 0.6f ) ;
	public float m_ScaleSpeed = -2.0f ;
	public float m_Threashold = 0.1f ;
	public Vector3 m_ScaleNow = Vector3.zero ;
	
	void Awake () 
	{
		m_InitScale = this.gameObject.transform.localScale ;
		m_ScaleNow = m_InitScale ;		
	}
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		ResizeObject() ;
	}
	
	protected void ResizeObject()
	{
		float positiveScaleSpeed = m_ScaleSpeed ;
		if( m_ScaleSpeed < 0 )
		{
			positiveScaleSpeed = -1 * m_ScaleSpeed ;
			m_ScaleNow = Vector3.Lerp( m_ScaleNow , m_InitScale , positiveScaleSpeed * Time.deltaTime ) ;
			if( Vector3.Distance( m_ScaleNow , m_InitScale ) < m_Threashold )
			{
				m_ScaleSpeed *= -1 ;
			}
		}
		else
		{
			m_ScaleNow = Vector3.Lerp( m_ScaleNow , m_TargetScale , positiveScaleSpeed * Time.deltaTime ) ;
			if( Vector3.Distance( m_ScaleNow , m_TargetScale ) < m_Threashold )
			{
				m_ScaleSpeed *= -1 ;
			}
		}
		
		this.transform.localScale = m_ScaleNow ;
	}	
}
