using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public static SliderController instance;

    public bool sliderWait;
    private Slider slider;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slider = GetComponent<Slider>();
        sliderWait = false;
        SliderStart();

    }

    public void SliderInit()
    {
    }


    public void SliderStart()
    {
        StartCoroutine(coSliderStart());
    }

    IEnumerator coSliderStart()
    {
        slider.maxValue = 100f;
        slider.value = 100f;  // 제한시간
        float time = 5;// WaveManager.instance.waveTime();
        while (time > 0)
        {
            if (!sliderWait)
            {
                time -= Time.fixedDeltaTime;
                slider.value -= Time.fixedDeltaTime * (slider.maxValue / 5);
            }
            yield return new WaitForFixedUpdate();
        }
        Debug.Log(5);// WaveManager.instance.waveTime());
        Debug.Log(time);
    }

    void Update()
    {
        //if (!sliderWait)
        //{
        //    if (slider.value > 0)
        //    {

        //        slider.value -= Time.deltaTime * (slider.maxValue / WaveManager.instance.waveTime());
        //    }
        //    else
        //    {

        //        Debug.Log(WaveManager.instance.WaveDelaying);
        //        if (!WaveManager.instance.WaveDelaying)
        //        {
        //            GameManager.instance.FailEffect();
        //            HPManager.instance.HP -= 10;
        //            HPManager.instance.HeartCheck();  // 플레이어 체력 감소 후 업데이트
        //            WaveManager.instance.WaveDelayStart();
        //        }

        //    }
        //}
    }
    public void StartSlider()
    {
        Debug.Log("스타트슬라이더");
        slider.maxValue = 100f;
        slider.value = 100f;  // 제한시간
    }
    public void WaitSlider()
    {
        sliderWait = true;
    }
    public void ResumeSlider()
    {
        sliderWait = false;
    }
    public void ChangeSliderValue(float value)
    {
        slider.value += value;        
    }

    public void WaitSliderForSeconds(float time)
    {
        StartCoroutine(coWaitSlider(time));
    }
    IEnumerator coWaitSlider(float time)
    {
        sliderWait = true;
        yield return new WaitForSeconds(time);
        sliderWait = false;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
