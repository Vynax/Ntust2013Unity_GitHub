/*
@file ClickOnGUI_CloseThisObjOnly.cs
@author NDark

點選就關閉自己

@date 20130813 file started and copy from ClickOnGUI_Close of Kobayashi Maru Commander Open Project
*/
using UnityEngine;

public class ClickOnGUI_CloseThisObjOnly : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnMouseDown()
	{
		GUITexture guiTexture = this.gameObject.guiTexture ;
		if( null != guiTexture )
		{
			guiTexture.enabled = false ;
		}
	}		
}
