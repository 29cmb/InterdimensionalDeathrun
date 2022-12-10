using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public BoxCollider2D collider;
    public CircleCollider2D collider2;
    public AudioSource winSound;
    bool debounce = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    async void Update()
    {
        if (collider.IsTouching(collider2) && debounce == false)
        {
            debounce = true;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Completed", 1);
            winSound.Play();
            await Task.Delay(1500);
            debounce = false;
            SceneManager.LoadScene("LevelSelectMenu");
        }
    }
}
