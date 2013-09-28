/*
@file DatabaseTester02.cs
@author NDark
@date 20130928 file started
*/
using UnityEngine;
using System.Collections;

public class DatabaseTester02 : MonoBehaviour 
{
	public dbAccess m_DB = null ;
	public string m_DataBaseFileName = "TestDB2.sqdb" ;
	public string m_DataBaseTable1Name = "UnitProperty" ;
	public string [] m_ColumnLabels = { "ID" , "Name" , "HP" , "Att" , "Def" } ;
	public string [] m_ValuesType = { "INTEGER" , "TEXT" , "INTEGER" , "INTEGER" , "INTEGER" } ;
	
	// Use this for initialization
	void Start () 
	{
		if( m_ColumnLabels.Length != m_ValuesType.Length )
			return ;
		m_DB = new dbAccess() ;
		m_DB.OpenDB( m_DataBaseFileName ) ;
		m_DB.CreateTable( m_DataBaseTable1Name , m_ColumnLabels , m_ValuesType ) ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
/*
        GUILayout.Label("Database Contents");
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100));
            for (var line : ArrayList in databaseData){
                GUILayout.BeginHorizontal();
                for (var s in line){
                    GUILayout.Label(s.ToString(), GUILayout.Width(DatabaseEntryStringWidth));
                }
                GUILayout.EndHorizontal();
            }
*/
	}
}
