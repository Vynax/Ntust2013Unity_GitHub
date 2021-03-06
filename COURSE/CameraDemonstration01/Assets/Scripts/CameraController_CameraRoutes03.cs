/**
 * @file CameraController_CameraRoutes03.cs
 * @author NDark
 * @date 20130601 . file started.
 */
using UnityEngine;
using System.Collections;

public class CameraController_CameraRoutes03 : MonoBehaviour 
{
	
	public enum CameraRoutesState
	{
		UnActive = 0 ,
		MoveToTarget ,
		CheckNextTarget ,
		Pause ,	
		Closed ,
	}
	
	public class RouteStruct
	{
		public Vector3 Position = Vector3.zero ;
		public Vector3 TargetPosition = Vector3.zero ;
	}
	
	public int m_RouteSize = 4 ;
	public int m_RouteIndex = 0 ; 
	public RouteStruct[] m_Routes = null ;
	public Vector3 m_PreviousTarget = Vector3.zero ;
	
	public CameraRoutesState m_CameraState = CameraRoutesState.UnActive ;
	public Camera m_CameraPtr = null ;
	
	public float m_CloseDistanceThreasholdSquare = 1.0f ;
	public float m_InterpolateSpeed = 1.0f ;
	
	public void StartMove()
	{
		if( m_RouteIndex < m_Routes.Length )
		{
			m_CameraState = CameraRoutesState.MoveToTarget ;
		}
	}

	public void StopMove()
	{
		m_CameraState = CameraRoutesState.Pause ;
	}

	public void SetupRoutes( RouteStruct [] _Routes )
	{
		m_Routes = _Routes ;
		m_RouteIndex = 0 ;
	}
		
	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
		
	}
	
	void OnGUI()
	{
		if( true == GUILayout.Button( "ReStart" ) )
		{
			GameObject routesObj = null ;

			RouteStruct [] routes = new RouteStruct[ m_RouteSize ] ;
			
			for( int i = 0 ; i < m_RouteSize ; ++i )
			{
				routes[ i ] = new RouteStruct() ;
				routesObj = GameObject.Find( "CameraRoute" + i.ToString() ) ;
				if( null != routesObj )
				{
					routes[ i ].Position = routesObj.transform.position ;
				}
				
				routesObj = GameObject.Find( "CameraRouteTarget" + i.ToString() ) ;
				if( null != routesObj )
				{
					routes[ i ].TargetPosition = routesObj.transform.position ;
				}
				
			}			
			SetupRoutes( routes ) ;
			StartMove() ;			
			
		}
		if( true == GUILayout.Button( "Pause/Resume" ) )
		{
			if( m_CameraState != CameraRoutesState.MoveToTarget )
				StartMove() ;
			else
				StopMove() ;
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_CameraState )
		{
		case CameraRoutesState.UnActive :
			break ;
		case CameraRoutesState.MoveToTarget :
			MoveToTarget() ;
			break ;			
		case CameraRoutesState.CheckNextTarget :
			CheckNextTarget() ;
			break ;			
		case CameraRoutesState.Pause :
			break ;
		case CameraRoutesState.Closed :
			break ;
		}

	}
	
	private void MoveToTarget()
	{
		if( m_RouteIndex >= m_Routes.Length ) 
		{
			Debug.LogError( "CameraController_CameraRoutes01:MoveToTarget() m_RouteIndex >= m_Routes.Length" ) ;
			return ;
		}
		
		Vector3 TargetPos = m_Routes[ m_RouteIndex ].Position ;
		Vector3 CurrentPos = m_CameraPtr.transform.position ;
		Vector3 DistanceVecNow = TargetPos - CurrentPos ;
		
		if( DistanceVecNow.sqrMagnitude < m_CloseDistanceThreasholdSquare )
		{
			// 立即抵達
			m_CameraPtr.transform.position = m_Routes[ m_RouteIndex ].Position ;
			m_CameraPtr.transform.LookAt( m_Routes[ m_RouteIndex ].TargetPosition ) ;						
			m_CameraState = CameraRoutesState.CheckNextTarget ;
		}
		else
		{
			// 繼續推進
			Vector3 NextPosition = Vector3.Lerp( m_CameraPtr.transform.position , 
												 m_Routes[ m_RouteIndex ].Position , 
												 Time.deltaTime * m_InterpolateSpeed ) ;			
			// Debug.Log( m_CameraPtr.transform.position + " " + m_Routes[ m_RouteIndex ].Position + " "  + Time.deltaTime * m_InterpolateSpeed + "=>" + NextPosition ) ;
			m_CameraPtr.transform.position = NextPosition ;
			
			m_PreviousTarget = Vector3.Lerp( m_PreviousTarget , 
											 m_Routes[ m_RouteIndex ].TargetPosition , 
											 Time.deltaTime * m_InterpolateSpeed ) ;			
			m_CameraPtr.transform.LookAt( m_PreviousTarget ) ;
		}
	}
	
	private void CheckNextTarget()
	{
		++m_RouteIndex ;
		if( m_RouteIndex >= m_Routes.Length ) 
		{
			m_CameraState = CameraRoutesState.Closed ;
		}
		else
		{
			m_CameraState = CameraRoutesState.MoveToTarget ;
		}
		
	}	
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.main ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_CameraRoutes01:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			m_PreviousTarget = m_CameraPtr.transform.position + m_CameraPtr.transform.forward * 100.0f ;
			Debug.Log( "CameraController_CameraRoutes01:InitializeCameraPtr() end." ) ;
		}
	}	
}
