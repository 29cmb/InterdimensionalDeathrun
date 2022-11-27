using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Diagnostics;

public class ObjectManagerBlock : MonoBehaviour
{
    public bool Toggle_Enable;
    public GameObject InteractionItem;
    public BoxCollider2D PlayerCollision;
    public BoxCollider2D BoxCollision;

    bool Debounce = false;
    void Start()
    {
        if(Toggle_Enable == true)
        {
            InteractionItem.active = false;
        }
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
        if (PlayerCollision.IsTouching(BoxCollision) && Debounce == false)
        {
            InteractionItem.active = !InteractionItem.active;
            Debounce = true;
            await Task.Delay(500);
            Debounce = false;
        }
    }
}
