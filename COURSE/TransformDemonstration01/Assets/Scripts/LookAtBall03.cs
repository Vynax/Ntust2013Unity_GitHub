/**
 * @file LookAtBall03.cs
 * @author NDark
 * 
 * Demonstration of up vector of LookAt().
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class LookAtBall03 : MonoBehaviour 
{
	public GameObject m_Ball = null ;
	public Vector3 m_Up = new Vector3( 0 , 1 , 0 ) ;// define up vector of this object
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_Ball )
		{
			InitializeBall() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_Ball )
			return ;
		
		this.gameObject.transform.LookAt( m_Ball.transform , m_Up ) ;
	}
	
	private void InitializeBall()
	{
		m_Ball = GameObject.Find( "Ball" ) ;
		if( null == m_Ball )
		{
			Debug.LogError( "LookAtBall03::InitializeBall() null == m_Ball" ) ;
		}
		else
		{
			Debug.Log( "LookAtBall03::InitializeBall() end." ) ;
		}		
	}
}
