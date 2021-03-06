/*
@file ScoreTextDisplay01.cs
@author NDark
@date 20130825 file started.
*/
using UnityEngine;

public class ScoreTextDisplay01 : MonoBehaviour 
{
	public bool m_Active = false ;
	public GUIText m_GUIText = null ;
	public float m_CurrentScore = 0.0f ;
	public int m_TargetScore = 0 ;
	public float m_ScoreSpeed = 10.0f ;
	
	public float m_ScorllingInSec = 0.0f ;
	public void SetScore( int _TargetScore )
	{
		if( _TargetScore == m_CurrentScore )
			return ;
		
		if( 0.0 != m_ScorllingInSec )
		{
			m_ScoreSpeed = 
				( _TargetScore - m_CurrentScore ) / m_ScorllingInSec ;
		}
		
		if( _TargetScore < m_CurrentScore && 
			m_ScoreSpeed > 0 )
			m_ScoreSpeed *= -1 ;
		else if( _TargetScore > m_CurrentScore && 
			m_ScoreSpeed < 0 )
			m_ScoreSpeed *= -1 ;
		
		m_TargetScore = _TargetScore ;
		m_Active = true ;
	}

	// Use this for initialization
	void Start () 
	{
		m_GUIText = this.gameObject.guiText ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_GUIText )
			return ;
		if( true == m_Active )
		{
			UpdateScore() ;
		}
	}
	
	private void UpdateScore()
	{
		float tmpScore = 
			m_CurrentScore + m_ScoreSpeed * Time.deltaTime ;
		
		if( m_ScoreSpeed > 0 && 
			tmpScore > m_TargetScore )
		{
			m_CurrentScore = m_TargetScore ;
			m_Active = false ;
		}
		else if( m_ScoreSpeed < 0 && 
				 tmpScore < m_TargetScore )
		{
			m_CurrentScore = m_TargetScore ;
			m_Active = false ;
		}
		else
		{
			m_CurrentScore = tmpScore ;
		}
		
		int displayScore = (int)m_CurrentScore ;		
		m_GUIText.text = displayScore.ToString() ;
	}
}
