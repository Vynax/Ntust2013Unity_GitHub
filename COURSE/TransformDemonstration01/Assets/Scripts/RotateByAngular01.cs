/*
 * @file RotateByAngular01.cs
 * @author NDark
 * 
 * Demonstration a rotate of 90 degrees by X-Axis,
 * and then a rotation of 90 degrees by Y-Axis.
 * 
 * Amd it will the same with RotateByAngular02.cs
 * 
 * It shows the problem of using angle of 3 axis to define a rotation.
 * 
 * For furthur information, please check this link:
 * http://blog.roodo.com/sayaku/archives/19544672.html
 * and this video:
 * http://www.youtube.com/watch?v=zc8b2Jo7mno
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateByAngular01 : MonoBehaviour 
{
	
	public float x = 0 ;
	public float y = 0 ;
	public float z = 0 ;
	

	public enum RotateByAngular01State
	{
		UnActive = 0 ,
		Initialization,
		X,
		Y,
		End ,
	}
	
	// we show the rotation by slow motion
	// and seperate by 2 rotation : by x axis and y axis.	
	public RotateByAngular01State m_State = RotateByAngular01State.UnActive ;
	
	// we use this rotation to keep the rotation after x rotation.
	Quaternion m_PreviousRotation = Quaternion.identity ;
	
	
	public void ReStart()
	{
		m_State = RotateByAngular01State.Initialization ;
	}
	
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case RotateByAngular01State.UnActive :
			break ; 
			
		case RotateByAngular01State.Initialization :
			x = 0 ;
			y = 0 ;
			m_State = RotateByAngular01State.X ;
			break ; 
			
		case RotateByAngular01State.X  :
			x += 0.1f ;
			this.transform.rotation = Quaternion.identity ;
			this.transform.Rotate( x , 0 , 0 , Space.Self ) ;
			if( x > 90 )
			{
				m_PreviousRotation = this.transform.rotation ;
				m_State = RotateByAngular01State.Y ;
			}
			
			break ;
		case RotateByAngular01State.Y  :
			y += 0.1f ;
			this.transform.rotation = m_PreviousRotation ;
			this.transform.Rotate( 0 , y , 0 , Space.Self  ) ;
			if( y > 90 ) 
				m_State = RotateByAngular01State.End ;			
			break ;

			
		case RotateByAngular01State.End :
			break ;
		}
	}
	
}

