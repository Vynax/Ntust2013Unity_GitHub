/*
@file AnimationPlayTrigger01.cs
@author NDark
@date 20130913 file started.
*/
using UnityEngine;

public class AnimationPlayTrigger01 : MonoBehaviour 
{
	public void PlayAnimationShowTexture( string _GUITextureObj )
	{
		ShowGUITexture.Show( _GUITextureObj , true , true , true ) ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
