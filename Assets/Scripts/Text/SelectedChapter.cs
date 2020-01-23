using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedChapter : TMPro.TMP_TextEventHandler {

    protected override void Awake()
    {
        base.Awake();
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ActivateChapter()
    {
        if (unlocked)
        {
            
            int index = transform.parent.GetSiblingIndex();
            MenuManager.instance.SelectedChapterIndex = index;
            foreach (var c in MenuManager.instance.Chapters)
            {
                c.Selected = false;
            }
            selected = true;

        }
            
    }

    
}
