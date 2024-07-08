using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SDKLoader : MonoBehaviour
{
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {

        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }
    public void GetData()
    {
        Debug.Log("SuccesLoad");
        SceneManager.LoadScene("Game");
    }
}
