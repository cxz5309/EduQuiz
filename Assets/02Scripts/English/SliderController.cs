using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public static SliderController instance;

    public bool sliderWait = false;
    public bool coSliderMoving = false;
    Coroutine coSliderMove;
    private Slider slider;
    float time;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SliderStart()
    {
        if(coSliderMoving)
        {
            sliderWait = false;
            coSliderMoving = false;
            StopCoroutine(coSliderMove);
            coSliderMove = null;
        }
        
        coSliderMove = StartCoroutine(coSliderStart());
    }

    public void SliderEnd()
    {
        sliderWait = false;
        StopCoroutine(coSliderStart());
    }
    IEnumerator coSliderStart()
    {
        coSliderMoving = true;
        slider.maxValue = 100f;
        slider.value = 100f;  // 제한시간
        time = WaveManager.instance.waveTime();

        while (time > 0)
        {

            if (!sliderWait)
            {
                time -= Time.fixedDeltaTime;
                slider.value -= Time.fixedDeltaTime * (slider.maxValue / WaveManager.instance.waveTime());
            }
            yield return new WaitForFixedUpdate();
        }
        if (!WaveManager.instance.WaveDelaying)
        {
            GameManager.instance.FailEffect();
            HPManager.instance.HP -= 10;
            HPManager.instance.HeartCheck();  // 플레이어 체력 감소 후 업데이트
            WaveManager.instance.WaveDelayStart();
        }
        yield break;
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
    public void WaitSlider()
    {
        sliderWait = true;
    }
    public void ResumeSlider()
    {
        sliderWait = false;
    }
    public void ChangeSliderValue(float _time)
    {
        slider.value -= _time * (slider.maxValue / WaveManager.instance.waveTime());
        this.time -= _time;
    }

    public void WaitSliderForSeconds(float time)
    {
        StartCoroutine(coWaitSliderForSeconds(time));
    }

    IEnumerator coWaitSliderForSeconds(float time)
    {
        WaitSlider();
        yield return new WaitForSeconds(time);
        ResumeSlider();
    }

    private void OnDisable()
    {
        StopCoroutine(coSliderStart());
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
