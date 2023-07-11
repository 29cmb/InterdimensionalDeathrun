using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public DataManager manager;
    public Sprite level1Completed;
    public Sprite level2Completed;
    public Sprite level3Completed;
    public Sprite level4Completed;
    public Sprite level5Completed;

    private Dictionary<string, Sprite> levelImageSprites;
    private string[] DataManagerKeys = {
        "Level1_completed",
        "Level2_completed",
        "Level3_completed",
        "Level4_completed",
        "Level5_completed",
    };

    void Start()
    {
        levelImageSprites = new Dictionary<string, Sprite>();

        levelImageSprites.Add("Level1", level1Completed);
        levelImageSprites.Add("Level2", level2Completed);
        levelImageSprites.Add("Level3", level3Completed);
        levelImageSprites.Add("Level4", level4Completed);
        levelImageSprites.Add("Level5", level5Completed);
    }

    void Update()
    {
        foreach (string key in DataManagerKeys)
        {
            string completed = manager.LoadData(key);

            if (completed == "True")
            {
                var LevelKey = key.Substring(0, 6);
                GameObject levelButton = GameObject.Find(LevelKey);
                if (levelButton != null)
                {
                    Image imageComponent = levelButton.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        if (levelImageSprites.ContainsKey(LevelKey))
                        {
                            imageComponent.sprite = levelImageSprites[LevelKey];
                        }
                    }
                }
            }
        }
    }
}
