/**
@file RegisterToMainUpdate03.cs
@author NDark
@date 20130805 . file started.
*/
using UnityEngine;

public class RegisterToMainUpdate03 : MonoBehaviour 
{
	static public GameObject sSingletonObject = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == sSingletonObject )
		{
			sSingletonObject = GameObject.Find( "GlobalSingleton" ) ;
		}

		if( null != sSingletonObject )
		{		
			MainUpdate03 mainUpdate = sSingletonObject.GetComponent<MainUpdate03>() ;
			if( null != mainUpdate )
			{
				mainUpdate.RegisterGameUnit( this.gameObject ) ;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
