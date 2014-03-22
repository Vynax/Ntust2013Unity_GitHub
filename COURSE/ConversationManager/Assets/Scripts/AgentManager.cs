/**
 * @file AgentManager.cs
 * @author NDark
 * @date 20140322 . file started.
 */
using UnityEngine;
using System.Collections.Generic;

public class AgentManager : MonoBehaviour 
{
	private List<AgentBase> m_Agents = new List<AgentBase>() ;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		foreach( AgentBase agent in m_Agents )
		{
			agent.DoUpdate() ;
		}
	}
}
