using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighscoreDBBehaviuor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //1
        //HighScore mHighScoreWriter = new HighScore();
        //mHighScoreWriter.deleteHighScoreTable();
        //Add data
        /*
        mHighScoreWriter.addData(new HighScoreEntity("Burn Out", 0, 1000, true));
        mHighScoreWriter.addData(new HighScoreEntity("Lights Off", 0, 1000, true));
        mHighScoreWriter.addData(new HighScoreEntity("Raise Again", 0, 1000, true));
        mHighScoreWriter.addData(new HighScoreEntity("Sold Out", 0, 1000, true));
        mHighScoreWriter.addData(new HighScoreEntity("Burning Man", 0, 1000, false));
        */
        //Fetch All Data
        // 2
        /*
        HighScore mHighScoreReader = new HighScore();
        System.Data.IDataReader reader = mHighScoreReader.getAllData();
        
        int fieldCount = reader.FieldCount;
        List<HighScoreEntity> highScoreList = new List<HighScoreEntity>();
        while (reader.Read())
        {
            HighScoreEntity highscore = new HighScoreEntity(reader[0].ToString(),
                                        int.Parse(reader[1].ToString()),
                                        int.Parse(reader[2].ToString()),
                                        bool.Parse(reader[3].ToString()),
                                        reader[4].ToString()
                                        );
            Debug.Log(highscore._highScore);
            highScoreList.Add(highscore);
        }*/
        //mHighScoreReader.UpdateHighscoreByMap(1000, "Burn Out");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
