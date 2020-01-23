using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour {

    
    private string connectionString;
    private string connection;

    // Use this for initialization
    void Start () {
        connection = "URI=file:" + Application.persistentDataPath + "/" + "DataStorage";
        CreateTable("my_table");
        //InsertData("my_table");
        ReadScore();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateTable(string tableName)
    {
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS " + tableName + " (id INTEGER PRIMARY KEY, val INTEGER )";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
    }

    private void InsertData(string tableName)
    {
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO " + tableName + " (id, val) VALUES (0, 5)";
        cmnd.ExecuteNonQuery();

    }

    private void ReadScore()
    {
        //string connection = "URI=file:" + Application.persistentDataPath + "/" + "DataStorage";

        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
        }

        // Close connection
        dbcon.Close();
    }

    private void GetScores()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM HighScores";

                dbCmd.CommandText = sqlQuery;

                //Executes the sqlQuery and returns the result to the reader
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetString(1) + " - " + reader.GetInt32(2));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

}


//Create Table
/*
// Create database
connection = "URI=file:" + Application.persistentDataPath + "/" + "HighScoreDB";

// Open connection
IDbConnection dbcon = new SqliteConnection(connection);
dbcon.Open();
// Create table
IDbCommand dbcmd;
dbcmd = dbcon.CreateCommand();
string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";

dbcmd.CommandText = q_createTable;
dbcmd.ExecuteReader();

// Insert values in table
IDbCommand cmnd = dbcon.CreateCommand();
cmnd.CommandText = "INSERT INTO my_table (id, val) VALUES (0, 5)";
cmnd.ExecuteNonQuery();

// Read and print all values in table
IDbCommand cmnd_read = dbcon.CreateCommand();
IDataReader reader;
string query = "SELECT * FROM my_table";
cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
        }

        // Close connection
        dbcon.Close();
        */
