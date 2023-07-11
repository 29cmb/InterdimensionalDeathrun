using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    public Configuration config;
    public AudioSource winSound;
    bool debounce = false;
    DataManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<DataManager>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (GetComponent<CircleCollider2D>().IsTouching(config.player.GetComponent<BoxCollider2D>()) && debounce == false)
        {
            debounce = true;
            winSound.Play();
      
            manager.SaveData(SceneManager.GetActiveScene().name + "_completed", true.ToString());

            await Task.Delay(1500);
            debounce = false;
            SceneManager.LoadScene("LevelSelectMenu");
        }
    }


}