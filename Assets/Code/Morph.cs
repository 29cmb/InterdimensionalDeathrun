using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Morph : MonoBehaviour
{

    [Header("Configuration")]
    public Configuration config;
    public int MorphId = 1;

    private Dictionary<int,string> morphIds;

    private GameObject Player;

    bool debounce = false;

    // Start is called before the first frame update
    void Start()
    {
        morphIds = new Dictionary<int, string>();
        morphIds.Add(1, "Green_Gerald");
        morphIds.Add(2, "Butter_Man");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    async void Update()
    {
        Player = config.player;
        if (debounce == false && IsPlayerTouching() && config.morphKey != MorphId) {
            debounce = true;
            

            var ResetPoint = GameObject.Find(morphIds[config.morphKey] + "_reset");
            var CurrentPlayerPosition = Player.transform.position;

            Player.GetComponent<PlayerController>().enabled = false;
            Destroy(Player.GetComponent<Rigidbody2D>());
            Player.transform.DOMove(ResetPoint.transform.position, 3f).SetEase(Ease.InOutSine);
            Player.name = morphIds[config.morphKey];
           
            var NewPlayer = GameObject.Find(morphIds[MorphId]);
            NewPlayer.transform.DOMove(CurrentPlayerPosition, 3f).SetEase(Ease.InOutSine);
            config.player = NewPlayer;
            await Task.Delay(3000);
            NewPlayer.AddComponent<Rigidbody2D>();
            NewPlayer.GetComponent<PlayerController>().rb = NewPlayer.GetComponent<Rigidbody2D>();
            NewPlayer.GetComponent<PlayerController>().enabled = true;
            NewPlayer.name = "Player";

            config.morphKey = MorphId;
            debounce = false;
            
        }
    }

    private bool IsPlayerTouching()
    {
        BoxCollider2D playerCollider = Player.GetComponent<BoxCollider2D>();
        BoxCollider2D MorphCollider = GetComponent<BoxCollider2D>();

        return playerCollider.IsTouching(MorphCollider);
    }
}
