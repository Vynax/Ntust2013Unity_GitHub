/*
@file CheckMainCharacterFall01.cs
@author NDark
@date 20130815 file started.
*/
using UnityEngine;

public class CheckMainCharacterFall01 : MonoBehaviour 
{
	public GameObject m_MainCharacterObject = null ;
	public bool m_StartFall = false ;
	public Camera m_CutSceneCamera = null ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_MainCharacterObject )
		{
			m_MainCharacterObject = GameObject.FindGameObjectWithTag( "Player" ) ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_MainCharacterObject &&
			null != m_MainCharacterObject.GetComponent<Rigidbody>() &&
			false == m_StartFall )
		{
			if( m_MainCharacterObject.GetComponent<Rigidbody>().velocity.y < -1.0f )
			{
				Debug.Log( "Fall" + m_MainCharacterObject.GetComponent<Rigidbody>().velocity.y ) ;
				StartCutSceneCamera() ;
				CloseMainCharacterController() ;
				StartSlowMotion() ;
				m_StartFall = true ;
			}
		}
	}
	
	private void StartCutSceneCamera()
	{
		if( null != m_CutSceneCamera )
		{
			m_CutSceneCamera.enabled = true ;
		}
	}
	
	private void CloseMainCharacterController()
	{
		if( null != m_MainCharacterObject )
		{
			MainCharacterController01 script = m_MainCharacterObject.GetComponent<MainCharacterController01>() ;
			if( null != script )
			{
				script.enabled = false ;
			}
		}
	}

	private void StartSlowMotion()
	{
		Time.timeScale = 0.2f ;
	}
	
}
