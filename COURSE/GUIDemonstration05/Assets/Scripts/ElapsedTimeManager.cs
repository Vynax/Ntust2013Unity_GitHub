/*
IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING. 

By downloading, copying, installing or using the software you agree to this license.
If you do not agree to this license, do not download, install, copy or use the software.

    License Agreement For Kobayashi Maru Commander Open Source

Copyright (C) 2013, Chih-Jen Teng(NDark) and Koguyue Entertainment, 
all rights reserved. Third party copyrights are property of their respective owners. 

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

  * Redistribution's of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.

  * Redistribution's in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.

  * The name of Koguyue or all authors may not be used to endorse or promote products
    derived from this software without specific prior written permission.

This software is provided by the copyright holders and contributors "as is" and
any express or implied warranties, including, but not limited to, the implied
warranties of merchantability and fitness for a particular purpose are disclaimed.
In no event shall the Koguyue or all authors or contributors be liable for any direct,
indirect, incidental, special, exemplary, or consequential damages
(including, but not limited to, procurement of substitute goods or services;
loss of use, data, or profits; or business interruption) however caused
and on any theory of liability, whether in contract, strict liability,
or tort (including negligence or otherwise) arising in any way out of
the use of this software, even if advised of the possibility of such damage.  
*/
/*
@file ElapsedTimeManager.cs
@author NDark

# 預設沒有掛上物件需要特別呼叫掛上
# Setup() 啟動開始記數
# 顯示時使用 00:00 的格式
# 使用的GUI物件 GUI_CountDownTimeText

@date 20121224 by NDark
*/
using UnityEngine;

public class ElapsedTimeManager : MonoBehaviour 
{
	public string m_GUITextName = "GUI_TimeText" ;
	protected GameObject m_GuiTextObj = null ;
	
	protected bool m_IsActive = false ;
	protected float m_ChangeTime = 0 ;
	
	// Use this for initialization
	void Start () 
	{
		Setup() ;
	}
	
	public void Setup()
	{
		m_IsActive = true ;
		m_ChangeTime = Time.timeSinceLevelLoad ;
		m_GuiTextObj = GameObject.Find( m_GUITextName ) ;
		if( null != m_GuiTextObj &&
			null != m_GuiTextObj.guiText )
			m_GuiTextObj.guiText.enabled = true ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_IsActive &&
			null != m_GuiTextObj &&
			null != m_GuiTextObj.guiText )
		{
			GUIText guiText = m_GuiTextObj.guiText ;
			if( null != guiText )
			{
				UpdateDisplayText( guiText , 
								   ElapsedFromLast() ) ;
			}
		}
	}
	
	protected void UpdateDisplayText( GUIText _guiText , float _DisplayTime )
	{
		string minStr = "" ;
		string secStr = "" ;						 
		float remainingSec = _DisplayTime ;
		
		// consider negative color
		bool positive = ( remainingSec > 0 ) ;
		if( false == positive )
		{
			remainingSec *= -1 ;
		}
		
		
		// "xyz {0} , {1} ... " , arg0 , arg1 , ... 
		minStr = string.Format( "{0:00}" , 
			Mathf.Floor( remainingSec / 60 ) ) ;
		
		secStr = string.Format( "{0:00}" , 
			Mathf.Floor( remainingSec % 60 ) ) ;
		
		
		_guiText.text = minStr + ":" + secStr ;		
		if( false == positive )
		{
			_guiText.material.color = Color.red ;
		}	
		else
		{
			_guiText.material.color = Color.green ;
		}
		
//		string totalStr = 
//			string.Format( "{0}...{1}" , 
//				remainingSec / 60 , 
//				remainingSec % 60 ) ;
//		
//		_guiText.text = totalStr ;		
	}
	
	protected float ElapsedFromLast()
	{
		float ret = 0.0f ;
		
		ret = Time.timeSinceLevelLoad - m_ChangeTime ;// reset time

		return ret;
	}	
}
