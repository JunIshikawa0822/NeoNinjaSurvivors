using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI;
using System;

public class UISystem : SystemBase, IOnLateUpdate
{
    public override void SetUp()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerPrimeDemandExp);
        SliderMaxValueSet(gameStat.playerHpSlider, gameStat.playerObjectData.playerMaxHp);

        EnterButtonInit(gameStat.levelUpEnterButton);
        DebugButtonInit(gameStat.DebugButton);

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

        UIDisplay(gameStat.levelUpPanel, gameStat.isLevelUp);
    }

    private void SliderMaxValueSet(UnityEngine.UI.Slider _slider, int _value)
    {
        _slider.maxValue = _value;
    }

    private void SliderValueSet(UnityEngine.UI.Slider _slider, int _value)
    {
        _slider.value = _value;
    }

    private void UIDisplay(GameObject _UI, bool _UIDisplayTrigger)
    {
        if (_UIDisplayTrigger)
        {
            _UI.SetActive(true);
        }
        else
        {
            _UI.SetActive(false);
        }
    }

    private void TextSet(TextMeshProUGUI _TMPText, string _text)
    {
        _TMPText.text = _text;
    }

    //levelUpPanelのEnterButton初期化
    private void EnterButtonInit(GameObject _buttonUI)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerEnter).callback.AddListener((eventDate) => { button.PointerOverEvent(); });
        TriggerInsert(button, EventTriggerType.PointerExit).callback.AddListener((eventDate) => { button.PointerExitEvent(); });
        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerOverEvent += () => button.buttonImage.color = Color.red;
        button.pointerExitEvent += () => button.buttonImage.color = Color.white;

        button.pointerDownEvent += () => {
            if (gameStat.isPanelSelected == true)
                gameStat.isLevelUp = false;
                gameStat.isPanelSelected = false;
                //選んだRewardのスキルレベルを１上昇させる
                //Debug.Log(gameStat.currentRewardsSet[gameStat.selectedPanelNumber]);
                switch(gameStat.currentRewardsSet[gameStat.selectedPanelNumber])
                {
                    case "Aura":
                        gameStat.isAuraUsing = true;
                        gameStat.auraSkillLevel++;
                        gameStat.auraObjectData.SetLevel(gameStat.auraSkillLevel);
                        break;
                    case "Bullet":
                        gameStat.bulletSkillLevel++;
                        gameStat.bulletObjectData.SetLevel(gameStat.bulletSkillLevel);
                        break;
                    case "Shuriken":
                        gameStat.shurikenSkillLevel++;
                        break;
                    case "Thunder":
                        gameStat.isThunderAttack = true;
                        gameStat.thunderSkillLevel++;
                        break;
                    default:
                        Debug.LogWarning("異常なSkillLevel検知");
                        break;
                }
        };
    }

    //levelUpPanelのPanels初期化
    private void RewardSelectPanelInit(GameObject _buttonUI, int _panelNum)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerEnter).callback.AddListener((eventDate) => { button.PointerOverEvent(); });
        TriggerInsert(button, EventTriggerType.PointerExit).callback.AddListener((eventDate) => { button.PointerExitEvent(); });
        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerOverEvent += () => button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 1f); // マウスオーバーで半透明の黒
        button.pointerExitEvent += () => {
            if(!gameStat.isPanelSelected || gameStat.selectedPanelNumber != _panelNum) 
                button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 0.5f); // パネルが選択されていない場合または選択パネルが異なる場合は不透明の黒に戻す
            else
                button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 1f); // 選択パネルが同じ場合は半透明の黒
        };
        button.pointerDownEvent += () => {
            if(gameStat.isPanelSelected && gameStat.selectedPanelNumber == _panelNum)
            {
                button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 0.5f);
                gameStat.isPanelSelected = false;
            } else if(gameStat.isPanelSelected && gameStat.selectedPanelNumber != _panelNum) {
                button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 1f);
                gameStat.selectPanelsList[gameStat.selectedPanelNumber].GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 0.5f);
                gameStat.isPanelSelected = true;
                gameStat.selectedPanelNumber = _panelNum;
            } else {
                gameStat.isPanelSelected = true;
                gameStat.selectedPanelNumber = _panelNum;
                button.GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 1f);
            }
        };
    }

    private void DebugButtonInit(GameObject _buttonUI)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerDownEvent += () => gameStat.playerTotalExp++;
    }

    private Entry TriggerInsert(ButtonBase button, EventTriggerType _eventTriggerType)
    {
        Entry entryTrigger = new Entry();
        entryTrigger.eventID = _eventTriggerType;

        button.buttonEventTrigger.triggers.Add(entryTrigger);

        return entryTrigger;
    }
}
