/*
@file MessageQueueManager01.cs
@author NDark
@date 20130612 file started, and derived from MessageQueueManager.cs of Kobayashi Maru Commander project.
*/
using UnityEngine;
using System.Collections.Generic;

public class MessageQueueManager01 : MonoBehaviour 
{
	public enum MessageQueueState
	{
		UnActive ,
		InDisplay ,
		InFadeOut ,
	}
	
	public MessageQueueState m_State = MessageQueueState.UnActive ;
	public Queue<string> m_MessageQueue = new Queue<string>() ;
	
	public float m_ShowSec = 1.0f ;
	public float m_FadeoutSec = 1.0f ;
	public float m_StartDisplayTime = 0.0f ;
	public float m_StartFadeoutTime = 0.0f ;
	public float m_StopTime = 0.0f ;
	
	public AudioClip m_Audio = null ;
	
	public GUIText m_GUITextPtr = null ;
	
	public void AddMessage( string _Message ) 
	{
		// Debug.Log( "AddMessage() _Message=" + _Message ) ;
		m_MessageQueue.Enqueue( _Message ) ;

	}
	
	public void CloseMessageNow( GUIText _guiText )
	{
		if( null == _guiText )
			_guiText = m_GUITextPtr ;
		
		if( null == _guiText )
			return ;		
		
		// Debug.Log( "CloseMessageNow" ) ;
		_guiText.enabled = false ;
		m_State = MessageQueueState.UnActive ;
	}
	
	// Use this for initialization
	void Start () 
	{
		m_State = MessageQueueState.UnActive ;				
		
		if( null == m_GUITextPtr )
			InitializeMessageText() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case MessageQueueState.UnActive :
			if( m_MessageQueue.Count > 0 )
			{
				// show another one
				string messageShow = m_MessageQueue.Dequeue() ;
				StartShow( m_GUITextPtr , messageShow ) ;
				m_State = MessageQueueState.InDisplay ;
			}			
			break ;
		case MessageQueueState.InDisplay :
			if( Time.timeSinceLevelLoad > m_StartFadeoutTime )
			{
				m_State = MessageQueueState.InFadeOut ;
			}
			break ;
		case MessageQueueState.InFadeOut :
			UpdateFadeOut( m_GUITextPtr ,
						   Time.timeSinceLevelLoad - m_StartFadeoutTime ,
						   m_FadeoutSec ) ;			
			if( Time.timeSinceLevelLoad > m_StopTime )
			{
				if( null != m_GUITextPtr )
					m_GUITextPtr.enabled = false ;
				m_State = MessageQueueState.UnActive ;
			}
			break ;			
		}
		
	}
	
	private void StartShow( GUIText _guiText , 
					   		string _ShowMessage )
	{
		if( null == _guiText )
			return ;
		
		// show another one
		_guiText.enabled = true ;
		Color color = _guiText.material.color ;
		color.a = 1.0f ;
		_guiText.material.color = color ;
		
		_guiText.text = _ShowMessage ;
		
		if( null != m_Audio )
			this.gameObject.GetComponent<AudioSource>().PlayOneShot( m_Audio ) ;
		
		// Debug.Log( "StartShow() _ShowMessage=" + _ShowMessage ) ;
		m_StartDisplayTime = Time.timeSinceLevelLoad ;
		m_StartFadeoutTime = m_StartDisplayTime + m_ShowSec ;
		m_StopTime = m_StartFadeoutTime + m_FadeoutSec ;		
	}
	
	private void UpdateFadeOut( GUIText _guiText , 
								float _ElapsedTime , 
								float _TotalFadeTime )
	{
		if( null == _guiText )
			return ;
		
		// fade out
		Color color = _guiText.material.color ;
		color.a = Mathf.Lerp( 1.0f , 0.0f , _ElapsedTime / _TotalFadeTime ) ;
		
		// Debug.Log( color ) ;
		_guiText.material.color = color ;
	}
	
	private void InitializeMessageText()
	{
		GameObject textObj = GameObject.Find( "MessageQueueManagerText" ) ;
		if( null != textObj )
		{
			m_GUITextPtr = textObj.GetComponent<GUIText>() ;
			m_GUITextPtr.text = "" ;
		}
		if( null == m_GUITextPtr )
		{
			Debug.LogError( "MessageQueueManager01:InitializeMessageText() null == m_GUITextPtr" ) ;
		}
		else
		{
			Debug.Log( "MessageQueueManager01:InitializeMessageText() end." ) ;
		}		
	}
}
