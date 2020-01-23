using UnityEngine;
using System.Collections;
using System;

public class Portal : Collidable {

    public string[] sceneNames;

    protected bool loadScene = false;

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

            //Reset the Objectives Text's in the Menu object
            GameObject.Find("Objective1Text").GetComponent<UnityEngine.UI.Text>().text = "";
            GameObject.Find("Objective 2").GetComponent<UnityEngine.UI.Text>().text = "";
            GameObject.Find("Objective2Text").GetComponent<UnityEngine.UI.Text>().text = "";
        }
        else if (loadScene) {
            try
            {
                GameObject.Find("EndOfMapAnimation").GetComponent<Animator>().SetTrigger("shading");
            } catch (NullReferenceException)
            {
                Debug.Log("Add the EndOfMap gameObject to the scene.");
            }
            
        }
    }

    protected virtual IEnumerator LoadNewScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
        
    }
}
