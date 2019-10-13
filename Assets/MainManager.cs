using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public GameObject canvas;

    public string themeSound;
    private AudioManager theAudio;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        canvas = GameObject.Find("Unicon").transform.Find("UniconCanvas").gameObject;
        canvas.gameObject.SetActive(true);
        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);

        CanvasManager.instance.PopupChange("Idle");
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}
