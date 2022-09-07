using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    private static HUDManager instance;
    public static HUDManager Instance { get => instance; }

   [SerializeField] private Slider progressBar;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetProgressBar(int newValue)
    {
        instance.progressBar.value = newValue;
    }


}
