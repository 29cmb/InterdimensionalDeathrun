using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audio;
    bool debounce = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    async public void LaunchLevel(int lvl)
    {
        if(debounce == false)
        {
            debounce = true;
            audio.Play();
            await Task.Delay(500);
            SceneManager.LoadScene("Level" + lvl);
            debounce = false;
        }
        
    }
}
