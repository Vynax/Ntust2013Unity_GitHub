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
@file GUI_ResizeTexture.cs
@author NDark

# 注意圖片一定要對準物件的座標中心
# m_StandardSize 標準大小
# m_SizeNow 目前大小
# m_MaximumSize 最大大小
# m_Direction 縮放速度

@date 20121226 by NDark
@date 20130113 by NDark . comment.
@date 20130121 by NDark . add checking of guitexture enable at Update()
@date 20130126 by NDark . add class method ResizeGUITexture()

*/
using UnityEngine;

public class GUI_ResizeTexture : MonoBehaviour 
{

	public float m_StandardSize = 32 ;
	public float m_MaximumSize = 36 ;
	public float m_Direction = 0.2f ;
	
	protected float m_SizeNow = 32 ;
	
	// Use this for initialization
	void Start () 
	{
		m_SizeNow = m_StandardSize ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ResizeGUITexture() ;
	}
	
	protected void ResizeGUITexture()
	{
		if( null != this.gameObject.GetComponent<GUITexture>() &&
			true == this.gameObject.GetComponent<GUITexture>().enabled )
		{
			AnimationResize( this.gameObject.GetComponent<GUITexture>() ) ;
			Rect rect = this.gameObject.GetComponent<GUITexture>().pixelInset ;
			rect.Set( -0.5f * rect.width ,
				-0.5f * rect.height ,
				rect.width ,
				rect.height ) ;
			this.gameObject.GetComponent<GUITexture>().pixelInset = rect ;
		}
	}
	
	void AnimationResize( GUITexture _guiTexture )
	{
		Rect size = _guiTexture.pixelInset ;
		size.width = m_SizeNow ;
		size.height = m_SizeNow ;
		_guiTexture.pixelInset = size ;
		if( m_SizeNow > m_MaximumSize )
		{
			m_Direction *= -1 ;
		}
		else if( m_SizeNow < m_StandardSize )
		{
			m_Direction *= -1 ;
		}
		m_SizeNow += m_Direction ;
		
	}	
}
