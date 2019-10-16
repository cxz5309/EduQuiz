using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    private GameObject unicon;
    private GameObject uniconCanvas;
    private GameObject uniconObject;

    public string themeSound;
    private AudioManager theAudio;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        unicon = GameObject.Find("Unicon");
        uniconCanvas = unicon.transform.Find("UniconCanvas").gameObject;
        uniconObject = unicon.transform.Find("UniconObject").gameObject;
        uniconCanvas.gameObject.SetActive(true);
        uniconObject.gameObject.SetActive(true);
        UniconText();

        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);
        CanvasManager.instance.PopupChange("Idle");

    }

    public void UniconText()
    {
        UniconTextStart();
        UniconTextEnd();
    }
    
    public void UniconTextStart()
    {
        CanvasManager.instance.PopupPosition(-10, -20);
    }
    public void UniconTextEnd()
    {
        CanvasManager.instance.PopupPosition(0, -10);
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}
