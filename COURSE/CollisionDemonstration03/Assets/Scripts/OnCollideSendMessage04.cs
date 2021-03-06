/**
@file OnCollideSendMessage04.cs
@author NDark
@date 20130801 file started.
*/
using UnityEngine;

public class OnCollideSendMessage04 : MonoBehaviour 
{
	MessageQueueManager01 m_MessageQueueManagerPtr = null ;
	public bool m_OnTriggerEnter = true ;
	public bool m_OnTriggerStay = false ;
	public bool m_OnTriggerExit = true ;
	public bool m_OnCollisionEnter = true ;
	public bool m_OnCollisionStay = false ;
	public bool m_OnCollisionExit = true ;
	public AudioClip m_Audio = null ;
	// Use this for initialization
	void Start () 
	{
		InitMessageQueueManager() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		if( false == m_OnTriggerEnter )
			return ;
		string reportLog = other.gameObject.name + " is OnTriggerEnter on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		SendAMessage( reportLog ) ;
	}
	
	void OnTriggerStay( Collider other )
	{
		if( false == m_OnTriggerStay )
			return ;		
		string reportLog = other.gameObject.name + "is OnTriggerStay on " + this.gameObject.name ;
		SendAMessage( reportLog ) ;
	}
	
	void OnTriggerExit( Collider other )
	{
		if( false == m_OnTriggerExit )
			return ;		
		string reportLog = other.gameObject.name + " is OnTriggerExit on " + this.gameObject.name ;
		SendAMessage( reportLog ) ;
	}
	
	void OnCollisionEnter( Collision other )
	{
		if( false == m_OnCollisionEnter )
			return ;		
		string reportLog = other.gameObject.name + " is OnCollisionEnter on " + this.gameObject.name ;
		SendAMessage( reportLog ) ;
	}
	
	void OnCollisionStay( Collision other )
	{
		if( false == m_OnCollisionStay )
			return ;		
		string reportLog = other.gameObject.name + "is OnCollisionStay on " + this.gameObject.name ;
		SendAMessage( reportLog ) ;
	}
	
	void OnCollisionExit( Collision other )
	{
		if( false == m_OnCollisionExit )
			return ;		
		string reportLog = other.gameObject.name + " is OnCollisionExit on " + this.gameObject.name ;
		
		SendAMessage( reportLog ) ;
	}
	
	private void InitMessageQueueManager()
	{
		if( false == m_OnTriggerEnter )
			return ;		
		GameObject messageQueueManagerObj = GameObject.Find( "MessageQueueManagerObj" ) ;
		if( null != messageQueueManagerObj )
		{
			m_MessageQueueManagerPtr = messageQueueManagerObj.GetComponent<MessageQueueManager01>() ;
		}
		
		if( null == m_MessageQueueManagerPtr )
		{
			Debug.LogError( "OnCollideSendMessage04:InitMessageQueueManager() null == m_MessageQueueManagerPtr" ) ;
		}
		else
		{
			Debug.Log( "OnCollideSendMessage04:InitMessageQueueManager() end." ) ;
		}		
	}
	
	private void SendAMessage( string _Message )
	{
		if( null != m_Audio )
		{
			this.GetComponent<AudioSource>().PlayOneShot( m_Audio ) ;
		}
		
		Debug.Log( _Message ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( _Message ) ;
		}
	}
}
