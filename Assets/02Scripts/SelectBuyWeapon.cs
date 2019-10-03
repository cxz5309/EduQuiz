using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectBuyWeapon : MonoBehaviour
{
    public GameObject Chapters;
    public GameObject [] ChapterArr;

    Vector2 nextPos;

    private int distance;
    private int size = 400;
    private int speed = 5;
    private int chapIndex;
    

    void Start()
    {
        chapIndex = 0;
        UpdateStage(chapIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rignt();
        }

        Chapters.transform.localPosition = Vector2.Lerp(Chapters.transform.localPosition, nextPos, Time.deltaTime * speed);
    }

    public void UpdateStage(int index)
    {
        nextPos = new Vector2(distance, Chapters.transform.localPosition.y);
        StartCoroutine(ChapterSizeUp(index, true));
    }

    public void Left()
    {
        chapIndex -= 1;
        if (chapIndex < 0)
        {
            chapIndex = 0;
            return;
        }
        
        StartCoroutine(ChapterSizeUp(chapIndex + 1, false));
        distance += size;
        UpdateStage(chapIndex);
    }

    public void Rignt()
    {
        chapIndex += 1;
        if (chapIndex >= ChapterArr.Length)
        {
            chapIndex = ChapterArr.Length - 1;
            return;
        }
        
        StartCoroutine(ChapterSizeUp(chapIndex - 1, false));
        distance -= size;
        UpdateStage(chapIndex);
    }

    IEnumerator ChapterSizeUp(int index, bool flag)
    {
        if (flag)
        {
            for (int i = 0; i < 15; i++)
            {
                ChapterArr[index].transform.localScale = new Vector3(ChapterArr[index].transform.localScale.x + 0.02f,
                    ChapterArr[index].transform.localScale.y + 0.02f, ChapterArr[index].transform.localScale.z + 0.02f);
                yield return new WaitForFixedUpdate();
            }
        }
        else if (!flag)
        {
            for (int i = 15; i > 0; i--)
            {
                ChapterArr[index].transform.localScale = new Vector3(ChapterArr[index].transform.localScale.x - 0.02f,
                    ChapterArr[index].transform.localScale.y - 0.02f, ChapterArr[index].transform.localScale.z - 0.02f);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}