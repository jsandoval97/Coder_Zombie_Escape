using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    private static HUDManager instance;
    public static HUDManager Instance { get => instance; }

    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Text Coins;

    [SerializeField] private Text Lives;

    int score = 0;
    int lives = 2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log(instance);
            PlayerCollision.OnDead += GameOver;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Coins.text = "Coins " + score;
        Lives.text = "Lives " + lives;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetProgressBar(int newValue)
    {
        instance.progressBar.value = newValue;
    }

    public void AddPoint()
    {
        score += 1;
        Coins.text = "Coins " + score;
    }

    public void RestarVida()
    {
        lives = -1;
        Lives.text = "Lives " + lives;
    }

    private void GameOver()
    {
        Debug.Log("Respuesta desde otro script");
        gameOverPanel.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerCollision.OnDead -= GameOver;
    }

}