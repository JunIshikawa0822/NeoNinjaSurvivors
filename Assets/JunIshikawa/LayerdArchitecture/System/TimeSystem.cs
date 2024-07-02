using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeSystem : SystemBase, IOnUpdate
{
    public void OnUpdate() {
        TimeControl(ref gameStat.seconds,ref gameStat.minutes,ref gameStat.oldSeconds, gameStat.timerText);
    }

    private void TimeControl(ref float seconds, ref int minutes, ref float oldSeconds, TextMeshProUGUI timerText)
    {
        seconds += Time.deltaTime;
        if (seconds >= 60f)//経過秒数が６０秒を超えていたら分に繰り上げ
        {
            minutes++;
            seconds -= 60f;
        }

        int currentSeconds = (int)seconds;
        //前フレームまでの経過時間から秒に繰り上がりが起きていたら更新
        if (currentSeconds != (int)oldSeconds)
        {
            timerText.text = $"{minutes:00}:{currentSeconds:00}";
            oldSeconds = seconds;
        }
    }

}
