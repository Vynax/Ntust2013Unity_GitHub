/*
@file VectorProject02.cs
@author NDark

http://docs.unity3d.com/Documentation/ScriptReference/Vector3.Project.html

@date 20130831 . file started.
*/
using UnityEngine;

public class VectorProject02 : MonoBehaviour 
{
	public Vector3 m_ThisVec = Vector3.zero ;
	public GameObject m_Sundial = null ;
	public GameObject m_ProjectSphere = null ;
	// Use this for initialization
	void Start () 
	{
		m_Sundial = GameObject.Find("Sundial") ;
		m_ProjectSphere = GameObject.Find("ProjectSphere") ;		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_ProjectSphere && 
			null != m_Sundial )
		{
			m_ThisVec = this.gameObject.transform.position - m_Sundial.transform.position ;
			
			//         O sphere
			//        / m_ThisVec
			//normal /
			// ^    / 
			// |   /
			// |  /
			// origin			
			Vector3 normal = m_Sundial.transform.up ;
			Vector3 subVec = Vector3.Project( m_ThisVec , normal ) ;
			// wrong answer
			// m_ProjectSphere.transform.position = subVec ;
			
			// subVec
			//   projectOnPlaneVec 
			// ^ ---->  
			// |     / m_ThisVec
			// |    /
			// |   / 
			// |  /
			// | /
			// origin
			Vector3 projectOnPlaneVec = m_ThisVec - subVec ;
			Vector3 projectOnPlanePos = m_Sundial.transform.position + projectOnPlaneVec ;
			
			m_ProjectSphere.transform.position = projectOnPlanePos ;
			
		}
	
	}
}
