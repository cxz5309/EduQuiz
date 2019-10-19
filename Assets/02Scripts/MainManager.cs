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
    private GameObject Player;

    private DataSave dataSave;

    public Transform animalSpwanTr;
    public GameObject[] animals = new GameObject[10];

    public GameObject inventory;

    public static bool UniconConversationOneTime = false;

    public string themeSound;
    private AudioManager theAudio;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        dataSave = Player.GetComponent<DataSave>();
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
            SetAnimals();
        }
    }

    public void SetAnimals()
    {
        StartCoroutine(coSetAnimals());
    }

    IEnumerator coSetAnimals()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(i + "번째 동물이 떨어진다");
            if (DataSave.instance.data.availableAnimal[i] == 1)
            {
                Instantiate(animals[i], animalSpwanTr.position + new Vector3(Random.Range(-3,3),0,Random.Range(-5,5)), Quaternion.Euler(new Vector3(0,Random.Range(0,180),0)));
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void InventoryOn()
    {
        if (inventory.activeSelf == true)
        {
            inventory.SetActive(false);
        }
        else if (inventory.activeSelf == false)
        {
            inventory.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}
