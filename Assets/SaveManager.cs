using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(DataSave.instance.Gold);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
