/*
@file ClickOnGUI_FindParentAndClose.cs
@author NDark
@date 20130817 file started.
*/
using UnityEngine;

public class ClickOnGUI_FindParentAndClose : MonoBehaviour 
{
	public GameObject m_ParentObject = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_ParentObject )
		{
			GameObject tmpObj = this.gameObject ; 
			
			while( null != tmpObj.transform.parent )
			{
				tmpObj = tmpObj.transform.parent.gameObject ;
			}
			
			m_ParentObject = tmpObj ;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnMouseDown()
	{
		ShowGUITexture.Show( m_ParentObject , false , true , true ) ;
	}		
}
