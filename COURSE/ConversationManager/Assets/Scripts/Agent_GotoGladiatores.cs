/**
 * @file Agent_GotoGladiatores.cs
 * @author NDark
 * @date 20140330 . file started.
 */
using UnityEngine;

public class Agent_GotoGladiatores : AgentBase 
{
	private string oName = "MainCharacter" ;
	private string aName = "MainCharacter" ;
	private string aCategory = "CHARACTER_MainCharacter" ;

	private GameObject m_CarObject = null ;


	private GameObject m_GameObject = null ;
	private float carDistance = 0.07f ;
	private float closeDistance = 0.01f ;
	private int m_TargetObjectIndex = 0 ;
	private string []m_TargetObjectNames = 
	{
		"ChangePicture:DBLogo" ,
		"WaitSec:1" ,
		"ChangePicture:pedestrian" ,
		"Anchor01" ,
		"ChangePicture:SBahn" ,
		"Anchor02" ,
		"Anchor03" ,
		"Anchor04" ,
		"Anchor05" ,
		"Anchor06" ,
		"Anchor07" ,
		"ChangePicture:pedestrian" ,
		"Anchor08" ,
		"ChangePicture:SBahn" ,
		"Anchor09" ,
		"Anchor10" ,
		"Anchor11" ,
		"Anchor12" ,
		"Anchor13" ,
		"Anchor14" ,
		"Anchor15" ,
		"Anchor16" ,
		"Anchor17" ,
		"Anchor18" ,
		"Anchor19" ,
		"Anchor20" ,
		"Anchor21" ,
		"ChangePicture:pedestrian" ,
		"Anchor22" ,
		"WaitCar:CarObj" ,
		"Anchor23" ,
		"Anchor24" ,
		"Anchor25" ,
		"Anchor26" ,
	} ;

	// Use this for initialization
	void Start () 
	{
		this.AgentName = aName ;

		AgentStart() ;

		// MainCharacter
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( aCategory , "OBJECT_NAME" , oName ) ;
		infoDataCenter.WriteProperty( aCategory , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		AgentUpdate() ;
	}

	protected override void DoCondition()
	{
		Conditon_DoGoToGladiatores() ;
	}
	
	protected override void DoAction()
	{
		Action_DoGoToGladiatores() ;
	}

	
	private void Conditon_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject )
		{
			string currentObjectName = infoDataCenter.ReadProperty( aCategory , "OBJECT_NAME" ) ;
			m_GameObject = GameObject.Find( currentObjectName ) ;
		}
		
		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;

		if( "Wait" == assignmentStr )
		{
			// do nothing.
		}
		else if( "WaitSec" == assignmentStr )
		{

		}
		else if( "WaitCar" == assignmentStr )
		{
			string targetObjectNameStr = infoDataCenter.ReadProperty( aCategory , 
			                                                         "TARGET_OBJECT_NAME" ) ;

			if( null == m_CarObject )
			{
				m_CarObject = GameObject.Find( targetObjectNameStr ) ;
			}

			if( null != m_CarObject )
			{
				Vector3 targetPositon = m_CarObject.transform.position ;
				Vector3 currentPosistion = m_GameObject.transform.position ;
				Vector3 distanceVec = targetPositon - currentPosistion ;
				distanceVec.z = 0 ;
				// Debug.Log( "Conditon_DoGoToGladiatores():distanceVec.magnitude=" + distanceVec.magnitude ) ;
				if( distanceVec.magnitude < carDistance )
				{
					// replace correct action object
					infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
				}
			}
		}
		else if( "ChangePicture" == assignmentStr )
		{
			WriteAgentState( AgentState.Action ) ;
		}
		else if( "GoToTarget" == assignmentStr )
		{
			// Debug.Log( "Conditon_DoGoToGladiatores():GoToTarget" ) ;

			bool FindNextTargetObject = false ;

			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			if( 0 == targetPositionStr.Length )
			{
				string targetObjectNameStr = infoDataCenter.ReadProperty( aCategory , "TARGET_OBJECT_NAME" ) ;
				if( 0 == targetObjectNameStr.Length )
				{
					FindNextTargetObject = true ;
				}
				else
				{
					GameObject obj = GameObject.Find( targetObjectNameStr ) ;
					if( null != obj )
					{
						targetPositionStr = string.Format( "{0},{1},{2}" , 
						                                  obj.transform.position.x , obj.transform.position.y , obj.transform.position.z ) ;
						// Debug.Log( "Conditon_DoGoToGladiatores():targetPositionStr=" + targetPositionStr ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , targetPositionStr ) ;
					}
				}
			}

			// check distance
			if( 0 != targetPositionStr.Length )
			{
				Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
				Vector3 currentPosistion = m_GameObject.transform.position ;
				targetPositon.z = currentPosistion.z ;
				float distanceToTarget = Vector3.Distance( targetPositon , currentPosistion ) ;
				if( distanceToTarget > closeDistance )
				{
					// replace correct action object
					WriteAgentState( AgentState.Action ) ;
				}
				else
				{
					FindNextTargetObject = true ;
				}
			}

			if( true == FindNextTargetObject )
			{
				// next target position
				if( m_TargetObjectIndex >= m_TargetObjectNames.Length )
				{
					Debug.Log( "Conditon_DoGoToGladiatores():m_TargetObjectIndex >= m_TargetObjectNames.Length" ) ;
					infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "Wait" ) ;
				}
				else 
				{
					string targetName = m_TargetObjectNames[ m_TargetObjectIndex ] ;

					// check special word
					if( 0 == targetName.IndexOf( "ChangePicture" ) )
					{
						int index = targetName.IndexOf( ":" ) ;
						string targetPicture = "" ;
						if( 0 != index )
						{
							targetPicture = targetName.Substring( index +  1 ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "ChangePicture" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_PICTURE" , targetPicture ) ;
						WriteAgentState( AgentState.Action ) ;
					}
					else if( 0 == targetName.IndexOf( "WaitSec" ) )
					{
						int index = targetName.IndexOf( ":" ) ;
						string targetSecStr = "" ;
						if( 0 != index )
						{
							targetSecStr = targetName.Substring( index +  1 ) ;
							float elapsedSec = 0 ;
							float.TryParse( targetSecStr , out elapsedSec ) ;
							float stopTime = Time.timeSinceLevelLoad + elapsedSec ;
							targetSecStr = stopTime.ToString() ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "WaitSec" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_TIME" , targetSecStr ) ;
						WriteAgentState( AgentState.Action ) ;
					}
					else if( 0 == targetName.IndexOf( "WaitCar" ) )
					{

						int index = targetName.IndexOf( ":" ) ;
						string targetCarStr = "" ;
						if( 0 != index )
						{
							targetCarStr = targetName.Substring( index +  1 ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "WaitCar" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , 
						                             targetCarStr ) ;
					}
					else
					{
						Debug.Log( "Conditon_DoGoToGladiatores():targetName=" + targetName ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , targetName ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , "" ) ;// clear TARGET_POSITION
					}

					++m_TargetObjectIndex ;	
				}
			}
		}
	}
	
	private void Action_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject )
		{
			Debug.Log( "null == m_GameObject") ;
			return;
		}

		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;

		if( "Wait" == assignmentStr )
		{
			// do nothing.
		}
		else if( "WaitSec" == assignmentStr )
		{
			// do nothing.
			string targetTimeStr = infoDataCenter.ReadProperty( aCategory , "TARGET_TIME" ) ;
			float targetTime = 0 ;
			float.TryParse( targetTimeStr , out targetTime ) ;
			if( Time.timeSinceLevelLoad > targetTime )
			{
				infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
				WriteAgentState( AgentState.Condition ) ;
			}
		}
		else if( "ChangePicture" == assignmentStr )
		{
			string targetPicture = infoDataCenter.ReadProperty( aCategory , "TARGET_PICTURE" ) ;
			Texture targetTexture = ResourceLoad.LoadTexture( targetPicture ) ;
			if( null != m_GameObject.renderer )
			{
				Debug.Log( "ChangePicture" ) ;
				m_GameObject.renderer.material.mainTexture = targetTexture ;
			}

			infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
			WriteAgentState( AgentState.Condition ) ;
		}
		else if( "GoToTarget" == assignmentStr )
		{

			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;


			Vector3 currentPosistion = m_GameObject.transform.position ;
			targetPositon.z = currentPosistion.z ;

			Vector3 distanceVec = targetPositon - currentPosistion ;
			float distanceToTarget = distanceVec.magnitude ;
//			Debug.Log( "Action_DoGoToGladiatores()::targetPositionStr=" + targetPositionStr ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::currentPosistion=" + currentPosistion ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::distanceVec=" + distanceVec ) ;
			if( distanceToTarget > closeDistance )
			{
				// keep going
				Rigidbody2D r2d = m_GameObject.rigidbody2D ;
				if( null != r2d )
				{
					distanceVec.Normalize() ;
					if( r2d.velocity.magnitude < 0.2f )
						r2d.AddForce( distanceVec * 0.1f ) ;
				}
			}
			else
			{
				Rigidbody2D r2d = m_GameObject.rigidbody2D ;
				r2d.velocity = Vector2.zero ;
				WriteAgentState( AgentState.Condition ) ;
			}
		}
	}

}
