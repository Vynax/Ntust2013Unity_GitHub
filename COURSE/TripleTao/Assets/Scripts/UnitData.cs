using UnityEngine;
using System.Collections;

public enum UnitType
{
	Grass = 0 ,
	Bush ,
}

public class UnitData : MonoBehaviour 
{
	public int m_IndexI = 0 ;
	public int m_IndexJ = 0 ;
	public UnitType m_UnitType = UnitType.Grass ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
}
