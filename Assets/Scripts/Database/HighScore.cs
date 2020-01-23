using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
using Mono.Data.Sqlite;

public class HighScore : SqliteHelper {
    private const String Tag = "Riz: HighScoreDb:\t";

    private const String TABLE_NAME = "Highscores";
    private const String HS_MAP = "map";
    private const String HS_HIGHSCORE = "highscore";
    private const String HS_RESPECTSCORE = "respectscore";
    private const String HS_ISUNLOCKED = "isunlocked";
    private const String HS_LASTPLAYED = "lastplayed";

    /*
    public HighScore() : base()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            HS_MAP + " TEXT PRIMARY KEY, " +
            HS_HIGHSCORE + " INTEGER, " +
            HS_RESPECTSCORE + " INTEGER, " +
            HS_ISUNLOCKED + " TEXT, " +
            HS_LASTPLAYED + " TEXT ) " +
        dbcmd.ExecuteNonQuery();
    }*/
    public HighScore hs;
    void Strart()
    {
        
        

    }
    public HighScore() : base()
    {
        
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            HS_MAP + " TEXT PRIMARY KEY, " +
            HS_HIGHSCORE + " INTEGER, " +
            HS_RESPECTSCORE + " INTEGER, " +
            HS_ISUNLOCKED + " BOOLEAN, " +
            HS_LASTPLAYED + " TEXT) ";
        dbcmd.ExecuteNonQuery();
        //UpdateHighscoreByMap(1000, "Burn Out");
    }

    public void addData(HighScoreEntity highscore)
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "INSERT INTO " + TABLE_NAME
            + " ( "
            + HS_MAP + ", "
            + HS_HIGHSCORE + ", "
            + HS_RESPECTSCORE + ", "
            + HS_ISUNLOCKED + ", "
            + HS_LASTPLAYED + " ) "

            + "VALUES ( '"
            + highscore._map + "', '"
            + highscore._highScore + "', '"
            + highscore._respectScore + "', '"
            + highscore._isUnlocked + "', '"
            + highscore._lastPlayed + "' )";
        dbcmd.ExecuteNonQuery();
    }

    public void UpdateHighscoreByMap(int highScore, string map)
    {
        /*
        db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
        IDbConnection db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();*/

        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =

             "UPDATE " + TABLE_NAME
             + " SET " + HS_HIGHSCORE
             + " = " + highScore
             + " WHERE " + HS_MAP + " = " +"'"+map+"';";
        
        dbcmd.ExecuteNonQuery();
        //close();
    }
    public override IDataReader getDataById(int id)
    {
        return base.getDataById(id);
    }

    public override IDataReader getDataByString(string map)
    {
        Debug.Log(Tag + "Getting Location: " + map);

        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "SELECT * FROM " + TABLE_NAME + " WHERE " + HS_MAP + " = '" + map + "';";
        return dbcmd.ExecuteReader();
    }

    public override void deleteDataByString(string map)
    {
        Debug.Log(Tag + "Deleting Location: " + map);

        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "DELETE FROM " + TABLE_NAME + " WHERE " + HS_MAP + " = '" + map + "'";
        dbcmd.ExecuteNonQuery();
    }

    public override void deleteDataById(int id)
    {
        base.deleteDataById(id);
    }

    public override void deleteAllData()
    {
        Debug.Log(Tag + "Deleting Table");

        base.deleteAllData(TABLE_NAME);

    }

    public override IDataReader getAllData()
    {
        return base.getAllData(TABLE_NAME);

    }

    public IDataReader getLatestTimeStamp()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "SELECT * FROM " + TABLE_NAME + " ORDER BY " + HS_LASTPLAYED + " DESC LIMIT 1";
        return dbcmd.ExecuteReader();
    }

    public void deleteHighScoreTable()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "DROP TABLE" + TABLE_NAME;
    }
}
