using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level1UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        Debug.Log("Se presionó botón Restart");
        SceneManager.LoadScene("Level_1");
    }

    public void OnClickMain()
    {
        Debug.Log("Se presionó botón Main Menu");
        SceneManager.LoadScene("Main");
    }

    public void OnClickNext()
    {
        Debug.Log("Se presionó botón Main Menu");
        SceneManager.LoadScene("Level_2");
    }
}
