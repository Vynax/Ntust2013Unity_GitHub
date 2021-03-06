/**
@file FightSystem.cs
@author NDark
@date 20140405 file started.
@date 20140413 by NDark . add return when there is no unitData at RefreshOptions()

 */
#define TESTSTARTBATTLE
using UnityEngine;
using System.Collections.Generic;

public enum FightSystemState
{
	UnActive = 0 ,
	Initialize ,
	Ready ,
	BattleInitialize ,
	BattleStartAnimation ,
	RefreshOptions ,
	WaitCharacterLeft ,
	WaitCharacterRight ,
	WaitAnimationAndJudgeVictory ,
	BattleEndAnimation ,
}

public class FightSystem : MonoBehaviour 
{
	private GameObject m_CharacterLeft = null ;
	private GameObject m_CharacterRight = null ;
	private GameObject m_DisplayCharacterLeft = null ; // 左顯示圖像
	private GameObject m_DisplayCharacterRight = null ; // 右顯示圖像

	private GameObject m_StatusBackground = null ;// 狀態框背景
	private bool m_ShowEndStatus = true ;
	private int m_StatusStringIndex = 0 ; // 目前顯示的狀態索引
	private List<string> m_StatusStringList = new List<string>() ; // 所有狀態
	private List<GameObject> m_DisplayStatusList = new List<GameObject>() ; // 狀態文字物件


	private GameObject m_OperationBackground = null ;// 操作框背景
	private int m_OperationStringIndex = 0 ; // 目前操作字串顯示所引注意是 0 到 m_OperationOptionList.Count
	private List<Operation> m_OperationOptionList = new List<Operation>() ; // 所有的操作
	private List<GameObject> m_DispalyOperationList = new List<GameObject>() ; // 顯示的操作字串物件

	private int m_SelectedOperationIndex = 0 ; // 目前選擇的操作(注意是0~5)
	private GameObject m_IndicateOperationSymbol = null ; // 標示目前選擇的操作的符號

	// private List<SkillAnimation> m_SkillAnimationList = new List<SkillAnimation>() ;

	private FightSystemState m_FightSystemState = FightSystemState.UnActive ;

	public void StartABattle( GameObject _LeftCharacter , GameObject _RightCharacter )
	{
		m_CharacterLeft = _LeftCharacter ;
		m_CharacterRight = _RightCharacter ;
		m_FightSystemState = FightSystemState.BattleInitialize ;
	}

	public void StartABattle( GameObject _LeftCharacter , UnitDataStruct _LeftUnitDataStruct, 
	                         GameObject _RightCharacter , UnitDataStruct _RightUnitDataStruct )
	{
		if( null == _LeftCharacter ||
		   null == _RightCharacter ||
		   null == _LeftUnitDataStruct ||
		   null == _RightUnitDataStruct )
		{
			Debug.LogWarning( "StartABattle() null" ) ;
			return ;
		}

		UnitData leftUnitData = _LeftCharacter.AddComponent<UnitData>() ;
		leftUnitData.m_UnitDataStruct = _LeftUnitDataStruct ;

		UnitData rightUnitData = _RightCharacter.AddComponent<UnitData>() ;
		rightUnitData.m_UnitDataStruct = _RightUnitDataStruct ;

		m_CharacterLeft = _LeftCharacter ;
		m_CharacterRight = _RightCharacter ;
		m_FightSystemState = FightSystemState.BattleInitialize ;
	}

	public void AddStatus( string _Str )
	{
		m_StatusStringList.Add( _Str ) ;
		RefreshStatus() ;
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_FightSystemState )
		{
		case FightSystemState.UnActive :
			m_FightSystemState = FightSystemState.Initialize ;
			break ;
		case FightSystemState.Initialize :
			RetrieveUIObjects() ;
			break ;
		case FightSystemState.Ready :
#if TESTSTARTBATTLE
			m_CharacterLeft = m_DisplayCharacterLeft ;
			m_CharacterRight = m_DisplayCharacterRight ;
			Dictionary<string,UnitDataStruct> table = GlobalSingleton.GetUnitDataStructTable() ;
			if( true == table.ContainsKey( "FightSystemCharacterLeftObject" ) &&
			   true == table.ContainsKey( "FightSystemCharacterRightObject" ) )
			{
				UnitDataStruct leftUnitDataStruct = table[ "FightSystemCharacterLeftObject" ] ;
				UnitDataStruct rightUnitDataStruct = table[ "FightSystemCharacterRightObject" ] ;
				StartABattle( m_CharacterLeft , leftUnitDataStruct ,
				             m_CharacterRight , rightUnitDataStruct ) ;
			}

#endif
			break ;
		case FightSystemState.BattleInitialize :
			InitBattle() ;
			break ;
		case FightSystemState.BattleStartAnimation :
			m_FightSystemState = FightSystemState.RefreshOptions ;
			break ;
		case FightSystemState.RefreshOptions :
			RefreshOptions() ;
			break ;
		case FightSystemState.WaitCharacterLeft :
			WaitCharacter( m_CharacterLeft , m_DisplayCharacterLeft , m_CharacterRight , true ) ;
			break ;
		case FightSystemState.WaitCharacterRight :
			WaitCharacter( m_CharacterRight , m_DisplayCharacterRight , m_CharacterLeft , false ) ;
			break ;
		case FightSystemState.WaitAnimationAndJudgeVictory :
			m_FightSystemState = FightSystemState.RefreshOptions ;
			break ;
		case FightSystemState.BattleEndAnimation :
			break ;
		}
	}

	static int m_Iter = 0 ;
	private void WaitCharacter( GameObject _CharacterObj , 
	                           GameObject _DisplayObj , 
	                           GameObject _OppnentObj ,
	                           bool _FaceRight ) 
	{
		if( null == _CharacterObj )
		{
			Debug.LogWarning( "null == _CharacterObj" + _CharacterObj.name ) ;
			return ;
		}

		UnitData unitData = _CharacterObj.GetComponent<UnitData>() ;
		if( null == unitData )
		{
			Debug.LogWarning( "null == unitData" + _CharacterObj.name ) ;
			return ;
		}
		if( null == unitData.m_UnitDataStruct )
		{
			Debug.LogWarning( "null == unitData.m_UnitDataStruct" ) ;
			return ;
		}

		
		UnitType unitType = unitData.m_UnitDataStruct.GetUnitType() ;

		if( unitType == UnitType.Player )
		{
			Dictionary< string , StandardParameter > standardParamTable = unitData.m_UnitDataStruct.GetStandardParameterTable() ;

			// 真的等待
			if( true == Input.GetKeyDown(KeyCode.DownArrow) )
			{
				int now = m_SelectedOperationIndex ;
				++now ;
				// @todo 判斷是否到底
				if( now < m_DispalyOperationList.Count &&
				   now < m_OperationOptionList.Count )
				{
					Debug.Log("true == Input.GetKeyDown(KeyCode.DownArrow" ) ;
					m_SelectedOperationIndex = now ;
					RefreshOperationSymbol() ;
				}

			}
			else if( true == Input.GetKeyDown(KeyCode.UpArrow) )
			{
				int now = m_SelectedOperationIndex ;
				--now ;
				// @todo 判斷是否可以再往上
				if( now >= 0 )
				{
					Debug.Log("true == Input.GetKeyDown(KeyCode.UpArrow" ) ;
					m_SelectedOperationIndex = now ;
					RefreshOperationSymbol() ;
				}
			}
			else if( true == Input.GetMouseButtonDown( 0 ) ||
			        true == Input.GetKeyDown(KeyCode.Return) )
			{
				Operation operationNow = RetrieveCurrentSelectOperation() ;
				Debug.Log( "unitType=" + unitType ) ;
				// Debug.Log( "operationNow.DisplayString=" + operationNow.DisplayString ) ;
				Debug.Log( "operationNow.CommandString=" + operationNow.CommandString ) ;
				
				string skillCommand = operationNow.CommandString ;
				float animationSpeed = 0 ;
				string animationPrefabName = "" ;
				SkillSetting skillSetting = null ;
				Dictionary<string,SkillSetting> skillTable = GlobalSingleton.GetSkillSettingTable() ;
				if( true == skillTable.ContainsKey( skillCommand ) )
				{
					skillSetting = skillTable[ skillCommand ] ;
					animationPrefabName = skillSetting.AnimationPrefab ;
					string animationSpeedStr = skillSetting.FyingSpeed ;
					float.TryParse( animationSpeedStr , out animationSpeed ) ;
				}
				Debug.Log( "animationSpeed=" + animationSpeed ) ;

				if( -1 != operationNow.CommandString.IndexOf( "SaveSP" ) )
				{
					if( true == standardParamTable.ContainsKey( "SP" ) )
					{
						float nowSP = standardParamTable[ "SP" ].now ;
						nowSP += 1 ;
						standardParamTable[ "SP" ].now = nowSP ;

						string addStatus = string.Format( "{0} 開始集氣" , _DisplayObj.name ) ;
						AddStatus( addStatus ) ;
						addStatus = string.Format( "{0} 目前SP到達了 {1}" , _DisplayObj.name , standardParamTable[ "SP" ].now ) ;
						AddStatus( addStatus ) ;
					}
					else
					{
						Debug.Log( "false == standardParamTable.ContainsKey( SP" ) ;
					}
				}
				else if( -1 != operationNow.CommandString.IndexOf( "Hadoken" ) ||
				         -1 != operationNow.CommandString.IndexOf( "FireBall" ) )
				{
					if( true == standardParamTable.ContainsKey( "SP" ) )
					{
						float nowSP = standardParamTable[ "SP" ].now ;
						if( nowSP > 0 )
						{
							// 產生一個特效
							GameObject animObj = PrefabInstantiate.Create( animationPrefabName , 
								"animObj" + (m_Iter++) ) ;
							if( null == animObj )
							{
								
							}
							else
							{
								animObj.transform.position = _DisplayObj.transform.position ;
								if( true == _FaceRight )
								{
									Vector2 forceVec = new Vector2( 1 , 0 ) ;
									forceVec *= animationSpeed ;
									animObj.GetComponent<Rigidbody2D>().AddForce( forceVec ) ;
								}
								else
								{
									Vector3 scale = animObj.transform.localScale ;
									animObj.transform.localScale = new Vector3( -1 * scale.x , scale.y , scale.z ) ; 
									
									Vector2 forceVec = new Vector2( -1 , 0 ) ;
									forceVec *= animationSpeed ;
									animObj.GetComponent<Rigidbody2D>().AddForce( forceVec ) ;
								}
								
								SkillAnimation skillAnim = animObj.GetComponent<SkillAnimation>() ;
								if( null == skillAnim )
								{
									skillAnim = animObj.AddComponent<SkillAnimation>() ;
									if( null == skillAnim.m_SkillVariable )
									{
										skillAnim.m_SkillVariable = new SkillVariable() ;
									}
									skillAnim.m_ParentName = _DisplayObj.name ;
									skillAnim.m_SkillVariable.m_SkillSetting = skillSetting ;
									skillAnim.m_SkillVariable.m_SkillPropertyNow.AttackProperty.Import( skillSetting.AttackProperty ) ;
									skillAnim.m_SkillVariable.m_SkillPropertyNow.DefenseProperty.Import( skillSetting.DefenseProperty ) ;
								}
								
								AnimationCollisionOnEnter collisionObj = animObj.GetComponent<AnimationCollisionOnEnter>() ;
								if( null != collisionObj )
								{
									collisionObj.m_ParentName = _DisplayObj.name ;
								}
								
							}
							string addStatus = string.Format( "{0} 發射了一個 {1}!" , _DisplayObj.name , skillSetting.DisplayString ) ;
							AddStatus( addStatus ) ;

							nowSP -= 1 ;
							standardParamTable[ "SP" ].now = nowSP ;
							addStatus = string.Format( "{0} 目前SP到達了 {1}" , _DisplayObj.name , standardParamTable[ "SP" ].now ) ;
							AddStatus( addStatus ) ;
						}
					}
				}
				int k = (int) m_FightSystemState ;
				++k ;
				m_FightSystemState = (FightSystemState) k ;
			}

		}
		else 
		{
			// 電腦 直接下令
		}


	}

	private void RefreshOperationSymbol()
	{
		if( m_SelectedOperationIndex >= 0 &&
		   m_SelectedOperationIndex < m_DispalyOperationList.Count )
		{
			Vector3 operationPos = m_DispalyOperationList[ m_SelectedOperationIndex ].transform.localPosition ;
			operationPos.x += 0.1f ;
			m_IndicateOperationSymbol.transform.localPosition = operationPos ;
		}
 	}

	private void RefreshStatus()
	{
		int startDisplayIndex = 0 ;
		if( true == m_ShowEndStatus )
		{
			int startIndexOfStrList = m_StatusStringList.Count - m_DisplayStatusList.Count ;

			if( startIndexOfStrList >= 0 )
			{
				m_StatusStringIndex = startIndexOfStrList ;
			}
			else
			{
				// Display List 反而比較多
				startDisplayIndex = 0 - startIndexOfStrList;
				m_StatusStringIndex = 0 ;
			}

		}


		for( int i = 0 ; i < m_DisplayStatusList.Count ; ++i )
		{

			TextMesh tm = m_DisplayStatusList[ i ].GetComponent<TextMesh>() ;
			if( null != tm )
			{
				if( i < startDisplayIndex )
				{
					tm.text = "" ;
				}
				else
				{
					int stringIndexNow = m_StatusStringIndex + ( i - startDisplayIndex ) ;
					if( stringIndexNow < m_StatusStringList.Count )
					{
						string str = m_StatusStringList[ stringIndexNow ] ;
						tm.text = str ;
					}
				}
			}
		}
	}

	private void RefreshOptions()
	{
		// 取得左玩家的招式資料
		Dictionary<string , SkillSetting> skillSettingTable = GlobalSingleton.GetSkillSettingTable() ;
		UnitData unitData = m_CharacterLeft.GetComponent<UnitData>() ;
		if( null == unitData )
		{
			Debug.LogError( "RefreshOptions() null == unitData " ) ;
			return ;
		}
		else
		{
			// Debug.Log( "RefreshOptions() unitData.m_UnitDataStruct.m_Skills.Count=" + unitData.m_UnitDataStruct.m_Skills.Count ) ;
			Dictionary< string , SkillLabel > skillLabelTable = unitData.m_UnitDataStruct.GetSkillLabelTable() ;
			string [] skillArray = new string[ skillLabelTable.Count ] ;
			skillLabelTable.Keys.CopyTo( skillArray , 0 ) ;
			for( int i = 0 ; i < 6 ; ++i )
			{

				int index = i + m_OperationStringIndex ;
				if( index < skillLabelTable.Count )
				{
					if( i >= m_OperationOptionList.Count )
						m_OperationOptionList.Add( new Operation() ) ; 
					string skillLabelKey = skillArray[ index ] ;
					string displayString = "(displayString)" ;
					if( true == skillSettingTable.ContainsKey( skillLabelKey ) )
					{
						displayString = skillSettingTable[skillLabelKey].DisplayString ;
					}
					m_OperationOptionList[ i ].DisplayString = displayString ;
					m_OperationOptionList[ i ].CommandString = skillLabelKey ;

				}
			}
		}

		for( int i = 0 ; i < m_DispalyOperationList.Count ; ++i )
		{
			GameObject displayOperation = m_DispalyOperationList[ i ] ;
			TextMesh tm = displayOperation.GetComponent<TextMesh>() ;

			if( i < m_OperationOptionList.Count )
			{
				tm.text = m_OperationOptionList[ i ].DisplayString ;
			}
			else
			{
				tm.text = "" ;
			}
		}

		// 依照玩家資料更新玩家的選項
		m_FightSystemState = FightSystemState.WaitCharacterLeft ;
	}

	private void InitBattle()
	{
		// 取得雙方的資料 傳入並建立角色的貼圖。取得角色資料，角色的招式資料。
		// 依照玩家資料更新玩家的選項
		m_FightSystemState = FightSystemState.BattleStartAnimation ;
	}

	private void RetrieveUIObjects() 
	{
		RetrieveGameObject( ref m_DisplayCharacterLeft , "FightSystemCharacterLeftObject" ) ;
		RetrieveGameObject( ref m_DisplayCharacterRight , "FightSystemCharacterRightObject" ) ;
		RetrieveGameObject( ref m_StatusBackground , "FightSystemStatusBackground" ) ;
		RetrieveGameObject( ref m_OperationBackground , "FightSystemOperationBackground" ) ;
		RetrieveOperationStringList( m_OperationBackground ) ;
		RetrieveStatusStringList( m_StatusBackground ) ;
		RetrieveGameObject( ref m_IndicateOperationSymbol , "IndicateOperationSymbol" ) ;
		m_FightSystemState = FightSystemState.Ready ;
	}

	private void RetrieveStatusStringList( GameObject _ParentObj ) 
	{
		if( null == _ParentObj )
			return ;
		
		for( int i = 0 ; i < 10 ; ++i )
		{
			string objectName = string.Format( "StatusText{0:00}" , i ) ;
			Transform trans = _ParentObj.transform.FindChild( objectName ) ;
			if( null != trans )
			{
				TextMesh tm = trans.gameObject.GetComponent<TextMesh>() ;
				tm.text = "" ;
				// Debug.Log( "objectName" + objectName ) ;
				m_DisplayStatusList.Add( trans.gameObject ) ;
			}
		}
	}

	private void RetrieveOperationStringList( GameObject _ParentObj ) 
	{
		if( null == _ParentObj )
			return ;

		for( int i = 0 ; i < 10 ; ++i )
		{
			string objectName = string.Format( "OperationText{0:00}" , i ) ;
			Transform trans = _ParentObj.transform.FindChild( objectName ) ;
			if( null != trans )
			{
				TextMesh tm = trans.gameObject.GetComponent<TextMesh>() ;
				tm.text = "" ;
				// Debug.Log( "objectName" + objectName ) ;
				m_DispalyOperationList.Add( trans.gameObject ) ;
			}
		}
	}

	private void RetrieveGameObject( ref GameObject _Obj , string _ObjectName ) 
	{
		_Obj = GameObject.Find( _ObjectName ) ;
		if( null == _Obj )
		{
			Debug.LogError( "RetrieveGameObject():" + _ObjectName + "is not found." ) ;
		}
	}

	private Operation RetrieveCurrentSelectOperation()
	{
		Operation ret = null ;
		for( int i = 0 ; i < m_OperationOptionList.Count ; ++i )
		{
			if( m_SelectedOperationIndex == m_OperationStringIndex + i )
			{
				ret = m_OperationOptionList[ i ] ;
				break ;
			}
		}
		return ret ;
	}
}
