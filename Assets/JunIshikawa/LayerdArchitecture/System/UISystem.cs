using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

public class UISystem : SystemBase, IOnLateUpdate 
{
    public override void SetUp()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerPrimeDemandExp);
        SliderMaxValueSet(gameStat.playerHpSlider, gameStat.playerDataList[0].playerMaxHp);

        EnterButtonInit(gameStat.levelUpEnterButton);

        for(int panelNum = 0; panelNum < gameStat.selectPanelsList.Count; panelNum++)
        {
            RewardSelectPanelInit(gameStat.selectPanelsList[panelNum], panelNum);
        }
    }

    public void OnLateUpdate()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderMaxValue);
        SliderValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderProgressValue);

        SliderValueSet(gameStat.playerHpSlider, gameStat.player.GetEntityHp);
        TextSet(gameStat.playerLevelText, gameStat.playerLevel.ToString());
    }

    private void SliderMaxValueSet(Slider _slider, int _value)
    {
        _slider.maxValue = _value;
    }

    private void SliderValueSet(Slider _slider, int _value)
    {
        _slider.value = _value;

        //if(_slider.value >= _slider.maxValue)
        //{
        //    _slider.value = 0;
        //}
    }

    private void TextSet(TextMeshProUGUI _TMPText, string _text)
    {
        _TMPText.text = _text;
    }

    private void EnterButtonInit(GameObject _button)
    {
        ButtonBase button = _button.GetComponent<ButtonBase>();
        if (button == null) return;

        button.buttonOutline = _button.GetComponent<Outline>();

        button.ButtonInit();

        button.pointerDownEvent += () => Debug.Log(gameStat.selectedPanelNumber);
        button.pointerOverEvent += () => button.buttonImage.color = Color.red;
        button.pointerExitEvent += () => button.buttonImage.color = Color.white;
    }

    private void RewardSelectPanelInit(GameObject _panel, int _panelNum)
    {
        ButtonBase panel = _panel.GetComponent<ButtonBase>();
        if (panel == null) return;
        panel.ButtonInit();

        panel.buttonOutline = _panel.GetComponent<Outline>();

        TriggerInsert(EventTriggerType.PointerEnter).callback.AddListener((eventDate) => { panel.PointerOverEvent(); });
        TriggerInsert(EventTriggerType.PointerExit).callback.AddListener((eventDate) => { panel.PointerExitEvent(); });

        panel.pointerOverEvent += () => panel.buttonImage.color = Color.black;
        panel.pointerExitEvent += () => panel.buttonImage.color = Color.white;

        Entry TriggerInsert(EventTriggerType _eventTriggerType)
        {
            Entry entryTrigger = new Entry();
            entryTrigger.eventID = _eventTriggerType;

            panel.buttonEventTrigger.triggers.Add(entryTrigger);

            return entryTrigger;
        }
    }
}
