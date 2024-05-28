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
        TextSet(gameStat.playerLevelText, gameStat.playerLevel.ToString());
    }

    public void OnLateUpdate()
    {
        
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.expSliderMaxValue);
        

        SliderValueSet(gameStat.playerExpSlider, gameStat.expSliderProgressValue);
        TextSet(gameStat.playerLevelText, gameStat.playerLevel.ToString());
    }

    private void SliderMaxValueSet(Slider _slider, int _maxValue)
    {
        _slider.maxValue = _maxValue;
    }

    private void SliderValueSet(Slider _slider, int _value)
    {
        _slider.value = _value;

        if (_slider.maxValue == _value)
        {
            _slider.value = 0;
        }
    }

    private void TextSet(TextMeshProUGUI _textMeshPro, string _text)
    {
        _textMeshPro.text = _text;
    }
}
