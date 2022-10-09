using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStory()
    {
        Debug.Log("Se presionó botón Story mode");
        SceneManager.LoadScene("Level_1");
    }

    public void OnClickSurvival()
    {
        Debug.Log("Se presionó botón Survival mode");
        SceneManager.LoadScene("SurvivalMode");
    }

    public void OnClickExit()
    {
        Debug.Log("Se presionó el botón para cerrar los créditos");
        creditsPanel.SetActive(false);

    }

    public void OnClickCredits()
    {
        Debug.Log("Se presionó el botón para acceder a los créditos");
        creditsPanel.SetActive(true);
    }
}
