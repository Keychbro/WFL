using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.UI;
using YG;

public class RewardAd : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GetReward;
    }
    public void GetReward(int rewardID)
    {
        if (!YandexGame.SDKEnabled) { return; }

        if (rewardID == 0) { return; }
        else if (rewardID == 1)
        {
            FindObjectOfType<EndGameScreenPopup>().IncreaseAndAdjustChachedReward();
        }
        else if ( rewardID == 2)
        {
            StopAllCoroutines();
            StartCoroutine(speedRoutine());
        }

    }

    public void ShowAd(int rewardID)
    {
        if (DataSaveManager.Instance.MyData.IsAdsRemoved && rewardID != 0)
        {
            GetReward(rewardID);
            return;
        }
        YandexGame.RewVideoShow(rewardID);
    }

    private IEnumerator speedRoutine()
    {
        Time.timeScale = 3;
        yield return new WaitForSeconds(300);
        Time.timeScale = 1;
    }
}
