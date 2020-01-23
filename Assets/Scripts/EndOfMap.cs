using UnityEngine;
using System;
using System.Collections;

public class EndOfMap : Portal
{

    /*
    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        SaveStats();
    }*/

    private void SaveStats()
    {
        //HighScore newHighScore = new HighScore();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PlayerHitBox" && !loadScene)
        {
            loadScene = true;
            //Teleport the player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[UnityEngine.Random.Range(0, sceneNames.Length)];
            StartCoroutine(LoadNewScene(sceneName));
            GameManager.instance.ObjectivesRemove();
            GameManager.instance.AllObjectivesCompleted = false;

            //Set the total killed enemies count
            GameManager.instance.TotalKilledEnemiesCount += GameManager.instance.KilledZombiesCount;
            GameManager.instance.KilledZombiesCount = 0;
            GameManager.instance.Pause(true);

            //Delete GameObjects
            //Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("Menu"));
            Destroy(GameObject.Find("HUD"));
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("FloatingTextManager"));

            GameManager.instance.levelCompleted = true;
            //SaveStats();

        }
        else if (loadScene)
        {
            try
            {
                GameObject.Find("EndOfMapAnimation").GetComponent<Animator>().SetTrigger("shading");

            }
            catch (NullReferenceException)
            {
                Debug.Log("Add the EndOfMap gameObject to the scene.");
            }

        }
        
    }

    
    /*
    protected override IEnumerator LoadNewScene(string sceneName)
    {
        yield return new WaitForSeconds(1);

        AsyncOperation[] async = new AsyncOperation[]
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName),
            
        };
    }*/
    protected override IEnumerator LoadNewScene(string sceneName)
    {
        yield return new WaitForSeconds(1);

            

        
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }

    }







    //newHighScore.UpdateHighscoreByMap(100, "Burn Out");

    //System.Data.IDataReader
    /*
    HighScore highScoreReader = new HighScore();
    System.Data.IDataReader reader = highScoreReader.getAllData();

    while (reader.Read())
    {
        HighScoreEntity highScore = new HighScoreEntity(reader[0].ToString(),
                                    int.Parse(reader[1].ToString()),
                                    int.Parse(reader[2].ToString()),
                                    bool.Parse(reader[3].ToString()),
                                    reader[4].ToString()
                                    );
        Debug.Log(highScore._map + " highScore: " + highScore._highScore);
    }*/
    //newHighScore.updateHighscoreByMap(GameManager.instance.TotalKilledEnemiesCount * 10, "Burn Out");

    //Debug.Log();

    //Debug.Log("Burn out highscore has been Updated to " + new HighScore().getDataByString("Burn Out"));
}

