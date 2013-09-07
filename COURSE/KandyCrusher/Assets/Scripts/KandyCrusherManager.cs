/*
@file KandyCrusherManager.cs
@author NDark
@date 20130906 file started.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class KandyCrusherManager : MonoBehaviour 
{
	
	public enum KandyCrusherState
	{
		UnActive ,
		CheckDrop ,
		Droping ,
		JudgeDestroy ,
		Destroy ,
		WaitingPress , 
		WaitingRelease ,
		Swap, 
	}
	
	public KandyCrusherState m_State = KandyCrusherState.UnActive ;
	public List<GameObject> m_EmptyBoards = new List<GameObject>() ;
	public Dictionary<int , GameObject> m_Units = new Dictionary<int, GameObject>() ;
	public int m_WidthNum = 1 ;
	public int m_HeightNum = 1 ;
	public int m_Iter = 0 ;
	
	public GameObject m_UnitCollector = null ;
	
	// Use this for initialization
	void Start () 
	{
		
		if( null == m_UnitCollector )
		{
			m_UnitCollector = GameObject.Find( "UnitCollector" ) ;
		}
		
		
		
		InitializeAllUnits() ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case KandyCrusherState.UnActive :
			m_State = KandyCrusherState.CheckDrop ;
			break ;
		case KandyCrusherState.CheckDrop :
			CheckDrop() ;
			break ;
		case KandyCrusherState.Droping :
			CheckDroping() ;
			break ;
		case KandyCrusherState.JudgeDestroy :
			// 檢查是否有需要消滅
			JudgeDestroy() ;
			break ;	
		case KandyCrusherState.Destroy :
			// 消滅動畫檢查結束,回到 CheckDrop
			break ;	
		case KandyCrusherState.WaitingPress :
			// 等待按下
			break ;	
		case KandyCrusherState.WaitingRelease :
			// 等待放開 
			break ;				
		case KandyCrusherState.Swap :
			// 交換動畫
			break ;				
		}
	
	}
	
	void InitializeAllUnits()
	{
		for( int j = 0 ; j < m_HeightNum ; ++j )
		{
			for( int i = 0 ; i < m_WidthNum ; ++i )
			{
				GameObject newObj = GenerateUnit( i , j ) ;
				
				if( null != newObj )
				{
					m_Units.Add( j * m_HeightNum + i , newObj ) ;
				}
			}
		}
	}
	
	GameObject GenerateUnit( int _i , int _j )
	{
		GameObject ret = null ;
		
		int index = Random.Range( 1 , 5 ) ;
		string prefabName = string.Format( "AlienUnit{0:00}" , index ) ;
		Object prefab = Resources.Load( prefabName ) ;
		if( null == prefab )
			Debug.LogError( "null == prefab" + prefabName ) ;
		else
		{
			ret = (GameObject) GameObject.Instantiate( prefab ) ;
			ret.name = "Unit" + m_Iter.ToString() ;
			++m_Iter ;
			
			// 設定unit data
			UnitData script = ret.AddComponent<UnitData>() ;
			script.m_IndexI = _i ;
			script.m_IndexJ = _j ;
			script.m_UnitType = index ;
			
			
			// 設定parent
			if( null != m_UnitCollector )
			{
				ret.transform.parent = m_UnitCollector.transform ;
			}
	
			// 設定位置
			ret.transform.position = m_EmptyBoards[ _j * m_HeightNum + _i ].transform.position ;
		}
		return ret ;
	}
	
	private void CheckDrop()
	{
		HashSet<int> dropIndices = new HashSet<int>() ;
		
		int currentIndex = 0 ;
		int downIndex = 0 ;
		// 由下而上
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			currentIndex = iU.Current.Key ;
			downIndex = currentIndex - m_WidthNum ;// 越下面index越少
			
			if( true == IsValidIndex( downIndex ) &&
				false == m_Units.ContainsKey( downIndex ) )
			{
				// drop index
				dropIndices.Add( currentIndex ) ;
				Debug.Log( "CheckDrop() currentIndex=" + currentIndex ) ;
			}
		}
		
		foreach( int index in dropIndices )
		{
			GameObject dropObj = m_Units[ index ] ;
			// 修改index
			UnitData unitData = dropObj.GetComponent<UnitData>() ;
			if( null == unitData )
				break ;
			unitData.m_IndexJ -= m_WidthNum ;
			// 掉落動畫 設定目標
			CloseTarget moveAnim = dropObj.GetComponent<CloseTarget>() ;
			if( null != moveAnim )
			{
				moveAnim = dropObj.AddComponent<CloseTarget>() ;
			}
			int newIndex = unitData.m_IndexJ * m_WidthNum + unitData.m_IndexI ;
			moveAnim.m_TargetPos = m_EmptyBoards[ newIndex ].transform.position ;			
		}
		
		if( dropIndices.Count > 0 )
		{
			this.m_State = KandyCrusherState.Droping ;
		}
		else
		{
			this.m_State = KandyCrusherState.JudgeDestroy ;
		}
	}
	
	private void CheckDroping()
	{
		// 檢查掉落結束了沒有, 回到 CheckDrop
		
		// 由下而上
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			GameObject unit = iU.Current.Value ;
			CloseTarget script = unit.GetComponent<CloseTarget>() ;
			if( null != script )// 有一個單位還在掉
			{
				return ;
			}
		}
		
		Debug.Log( "CheckDroping() end droping" ) ;
		this.m_State = KandyCrusherState.CheckDrop ;
	}
	
	private void JudgeDestroy()
	{
		// Debug.Log( "JudgeDestroy()" + m_EmptyBoards.Count ) ;
		List<bool> isChecked = new List<bool>() ;
		for( int i = 0 ; i < m_EmptyBoards.Count ; ++i )
		{
			isChecked.Add( false ) ;
		}
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		HashSet<int> destroyList = new HashSet<int>() ;
		while( iU.MoveNext() )
		{
			GameObject unit = iU.Current.Value ;
			UnitData unitData = unit.GetComponent<UnitData>() ;
			if( null != unitData )
			{
				destroyList.Clear() ;
				for( int i = 0 ; i < isChecked.Count ; ++i )
				{
					isChecked[ i ] = false ;
				}
				// Debug.Log( "isChecked.Count= " + isChecked.Count ) ;
				RecursiveFindDestroy( unitData.m_IndexI ,
					unitData.m_IndexJ ,
					unitData.m_UnitType ,
					ref isChecked , 
					ref destroyList ) ;
				
				if( destroyList.Count > 2 )
				{
					// 新增 清除動畫
				}
				
					
			}
		}
	}
	
	void RecursiveFindDestroy( int _i , 
								int _j , 
								int _Type , 
								ref List<bool> _IsChecked , 
								ref HashSet<int> _DestroyList ) 
	{
		Debug.Log( "RecursiveFindDestroy() " + _i + "," + _j + "," + _Type ) ;
		
		if( false == IsValidIndex( _i , _j ) )
		{
			Debug.Log( "false == IsValidIndex" ) ;
			return ;
		}
		
		int index = _j * m_WidthNum + _i ;
		// Debug.Log( "index= " + index + " " + _IsChecked.Count ) ;
		if( true == _IsChecked[ index ] )
		{
			Debug.Log( "true == _IsChecked[ index ]" ) ;
			return ;
		}
		
		_IsChecked[ index ] = true ;
		
		if( false == m_Units.ContainsKey( index ) )
		{
			Debug.Log( "false == m_Units.ContainsKey" ) ;
			return ;
		}
		
		GameObject unit = m_Units[ index ] ;
		UnitData unitData = unit.GetComponent<UnitData>() ;
		if( null == unitData )
			return ;
		if( unitData.m_UnitType != _Type )
		{
			Debug.Log( "unitData.m_UnitType != _Type" + unitData.m_UnitType ) ;
			return ;
		}
		
		Debug.Log( "_DestroyList.Add " + index ) ;
		_DestroyList.Add( index ) ;
		
		RecursiveFindDestroy( _i + 1 , _j , _Type , ref _IsChecked , ref _DestroyList ) ;
		RecursiveFindDestroy( _i - 1 , _j , _Type , ref _IsChecked , ref _DestroyList ) ;
		RecursiveFindDestroy( _i , _j + 1 , _Type , ref _IsChecked , ref _DestroyList ) ;
		RecursiveFindDestroy( _i , _j - 1 , _Type , ref _IsChecked , ref _DestroyList ) ;
	}
	
	private bool IsValidIndex( int _index )
	{
		if( _index < 0 || _index >= m_HeightNum * m_WidthNum )
			return false ;
		return true ;
	}
	private bool IsValidIndex( int _i , int _j )
	{
		if( _i < 0 || 
			_i >= m_WidthNum ||
			_j < 0 || 
			_j >= m_HeightNum )
			return false ;
		return true ;
	}
}
