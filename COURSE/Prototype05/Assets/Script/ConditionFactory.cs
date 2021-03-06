/*
@file ConditionFactory.cs
@author NDark
@date 20130816 file started 
*/
using UnityEngine;

[System.Serializable]
public static class ConditionFactory
{
	public static Condition GetByString( string _ConditionName )
	{
		if( _ConditionName == "Condition_Collision" )
			return new Condition_Collision() ;
		else if( _ConditionName == "Condition_Time" )
			return new Condition_Time() ;		
		else
			return null ;
	}

}
