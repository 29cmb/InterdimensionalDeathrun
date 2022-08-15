using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class RealityFlipper : MonoBehaviour
{
    // Start is called before the first frame update
    string currentScene;
    public BoxCollider2D playerCollision;
    public BoxCollider2D boxCollision;
    public GameObject player;
    public int FlipperVal;
    UnityEngine.Vector3 playerPos;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    async void Update()
    {
        
        currentScene = SceneManager.GetActiveScene().name; // Swiftly update the scene string
        if(playerCollision.IsTouching(boxCollision)) // Check if the player is touching it
        {
            if(currentScene.Contains("Flipped")) // Check if the scene is currently flipped
            {
                string str = currentScene.Substring(0,6); // Make the new scene name
                string n = this.name;
                SceneManager.LoadScene(str); // Load the new scene
                await Task.Delay(100);
                GameObject.Find("Player").transform.position = (GameObject.Find(n).transform.position - new Vector3(0, 3, 0));
            } else {
                string str = currentScene + "Flipped";
                string n = this.name;
                SceneManager.LoadScene(str);
                await Task.Delay(100);
                GameObject.Find("Player").transform.position = (GameObject.Find(n).transform.position - new Vector3(0, 3, 0));
            }
        }
    }
}
