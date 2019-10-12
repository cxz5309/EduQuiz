using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public static SliderController instance;

    private bool sliderWait;
    private Slider slider;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slider = GetComponent<Slider>();
        sliderWait = true;
        StartSlider();
    }
    void Update()
    {
        if (slider.value > 0)
        {
            if (!sliderWait)
            {
                slider.value -= Time.deltaTime * (slider.maxValue / WaveManager.instance.waveTime());
            }
        }
        else
        {
            if (!sliderWait)
            {
                if (!WaveManager.instance.WaveDelaying)
                {
                    GameManager.instance.FailEffect();
                    HPManager.instance.HP -= 10;
                    HPManager.instance.HeartCheck();  // 플레이어 체력 감소 후 업데이트
                    WaveManager.instance.WaveDelayStart();
                }
            }
        }
    }
    public void StartSlider()
    {
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
