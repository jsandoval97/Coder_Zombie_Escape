using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
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
        Debug.Log("Se presionó botón Story mode");
        SceneManager.LoadScene("SurvivalMode");
    }
}
