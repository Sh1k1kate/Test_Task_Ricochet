using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public GameObject Player, Enemy, tmpP, tmpE;
    public Text score;
    public static int playerScr, enemyScr;
   
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        tmpP.transform.position = Player.transform.position;
        tmpE.transform.position = Enemy.transform.position;
    }
   
    void Update()
    {
        score.text = playerScr.ToString() + " : " + enemyScr.ToString();
    }

    public void RestartScene()
    {
            Player.transform.position = tmpP.transform.position;
            Enemy.transform.position = tmpE.transform.position;
    }
}
