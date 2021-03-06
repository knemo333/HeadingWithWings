using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeHandler : MonoBehaviour
{
    public Slider slider;
    public Slider sliderInside;
    public TMPro.TextMeshProUGUI coolTimeText;
    public float _coolTime;

    private void Start() {
        //slider = GetComponentInChildren<Slider>();
        //coolTimeText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0];
    }

    public void StartCoolTime()
    {
        slider.value = 0;
        sliderInside.value = 0;
        StartCoroutine(FilltheCircle());
    }

    public void SetCoolTime(float coolTime)
    {
        slider.maxValue = coolTime;
        sliderInside.maxValue = coolTime;
        _coolTime = coolTime;
    }


    IEnumerator FilltheCircle()
    {
        while(slider.value < slider.maxValue)
        {
            _coolTime -= Time.deltaTime;
            slider.value += Time.deltaTime;
            sliderInside.value += Time.deltaTime;
            coolTimeText.text = $"{(int)_coolTime}s";
            yield return null;
        }
        coolTimeText.text = "";
    }

    public void ResetCool()
    {
        coolTimeText.text = "";
        slider.value = slider.maxValue;
        sliderInside.value = sliderInside.maxValue;
    }
}
