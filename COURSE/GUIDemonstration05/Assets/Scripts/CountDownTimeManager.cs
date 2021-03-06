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
@file CountDownTimeManager.cs
@author NDark
@date 20130816 by NDark . file started and derieved from Kobayashi Maru Commander
*/
using UnityEngine;

public class CountDownTimeManager : ElapsedTimeManager 
{
	public float m_CountDownTime = 0 ;
	public bool m_ShowNegative = false ;
	
	// Use this for initialization
	void Start () 
	{
		if( 0 != m_CountDownTime )
			Setup( m_CountDownTime ) ;
	}
	
	public void Setup( float _TotalTime )
	{
		m_IsActive = true ;
		m_CountDownTime = _TotalTime ;
		m_ChangeTime = Time.time ;
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
			if( true == m_ShowNegative )
			{
				UpdateDisplayText( guiText , 
					m_CountDownTime - ElapsedFromLast() ) ;
			}
			else
			{
				if( true == IsCountDownToZero() )
				{
					guiText.text = "00:00" ;
				}
				else
				{
					UpdateDisplayText( guiText , 
						m_CountDownTime - ElapsedFromLast() ) ;	
				}
			}
		}
	}
	
	public bool IsCountDownToZero()
	{
		return ElapsedFromLast() > m_CountDownTime ;
	}
	
		
}
