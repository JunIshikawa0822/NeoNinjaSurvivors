using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISystem : SystemBase, IOnLateUpdate 
{
    public override void SetUp()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerPrimeDemandExp);
        //Debug.Log("ha");
    }

    public void OnLateUpdate()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderMaxValue);
        SliderValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderProgressValue);
        TextSet(gameStat.playerLevelText, gameStat.playerLevel.ToString());
    }

    private void SliderMaxValueSet(Slider _slider, int _value)
    {
        _slider.maxValue = _value;
    }

    private void SliderValueSet(Slider _slider, int _value)
    {
        _slider.value = _value;

        if(_slider.value >= _slider.maxValue)
        {
            _slider.value = 0;
        }
    }

    private void TextSet(TextMeshProUGUI _TMPText, string _text)
    {
        _TMPText.text = _text;
    }
}
