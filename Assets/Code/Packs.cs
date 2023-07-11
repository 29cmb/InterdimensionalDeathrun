using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Packs : MonoBehaviour
{
    public TMP_Text packText;

    public int TotalPackCount;
    private int CurrentPackNumber = 1;

    public Button Next;
    public Button Previous;

    private Dictionary<GameObject, string> packs;
    private GameObject[] packObjects;
    private GameObject currentPack;
    private string[] packNames;

    private void Start()
    {
        packs = new Dictionary<GameObject, string>();
        packObjects = new GameObject[TotalPackCount];
        packNames = new string[TotalPackCount];

        // Assign pack objects and names
        for (int i = 1; i <= TotalPackCount; i++)
        {
            var pack = GameObject.Find("Pack" + i);
            packObjects[i - 1] = pack;
            if (i != 1) {
                pack.SetActive(false);
            }

            // Assign pack names based on the pack index
            switch (i)
            {
                case 1:
                    packNames[i - 1] = "Tutorial Pack";
                    break;
                case 2:
                    packNames[i - 1] = "Beginner Pack";
                    break;
                // Add more cases for additional packs and assign their names accordingly
                default:
                    packNames[i - 1] = "Unnamed Pack";
                    break;
            }

            packs.Add(pack, packNames[i - 1]);
        }

        currentPack = packObjects[0];
        UpdateUI();
    }


    private void UpdateUI()
    {
        packText.text = packs[currentPack];

        Previous.GetComponent<Image>().color = CurrentPackNumber == 1 ? new Color32(96, 96, 96, 255) : new Color32(255, 255, 255, 255);
        Next.GetComponent<Image>().color = CurrentPackNumber == TotalPackCount ? new Color32(96, 96, 96, 255) : new Color32(255, 255, 255, 255);
    }

    public void NextButtonClick()
    {
        if (CurrentPackNumber < TotalPackCount)
        {
            currentPack.SetActive(false);
            CurrentPackNumber += 1;
            currentPack = packObjects[CurrentPackNumber - 1];
            currentPack.SetActive(true);
            UpdateUI();
        }
    }

    public void PreviousButtonClick()
    {
        if (CurrentPackNumber > 1)
        {
            currentPack.SetActive(false);
            CurrentPackNumber -= 1;
            currentPack = packObjects[CurrentPackNumber - 1];
            currentPack.SetActive(true);
            UpdateUI();
        }
    }
}
