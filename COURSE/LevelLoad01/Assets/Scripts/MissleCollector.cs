/*
 * @file MissleCollector.cs
 * @author NDark
 * @date 20130714 . file created.
 */
using UnityEngine;
using System.Collections;

public class MissleCollector : MonoBehaviour 
{
	static GameObject gChessPieceParentObj = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == gChessPieceParentObj )
		{
			gChessPieceParentObj = GameObject.Find( "MissleCollector" ) ;
		}
		
		if( null != gChessPieceParentObj )
		{
			this.gameObject.transform.parent = gChessPieceParentObj.transform ;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
