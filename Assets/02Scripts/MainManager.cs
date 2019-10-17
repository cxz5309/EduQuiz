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

    public static bool UniconConversationOneTime = false;

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

        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);

        if (!UniconConversationOneTime)
        {
            UniconConversationOneTime = true;
            CanvasManager.instance.UniconConversation();
        }
        else
        {
            CanvasManager.instance.SetIdlePopup();
        }
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}
