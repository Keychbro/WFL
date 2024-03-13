using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    [SerializeField] private DataTest newData = new DataTest();
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://193.42.113.235:8001/api/"));
        StartCoroutine(Upload());
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        //7b867119-c1a7-40da-b51e-294d8f66b7f1
        //fb988152-8f01-4f9a-b436-3691d2ffe806
        // form.AddField("server_uuid", "fb988152-8f01-4f9a-b436-3691d2ffe806");
        // form.AddField("player_uuid", "7b867119-c1a7-40da-b51e-294d8f66b7f1");
        // form.AddField("fraction", "Human");
        // form.AddField("text", "Hello World!");
        //        [
        //            { "player_uuid":"7b867119-c1a7-40da-b51e-294d8f66b7f1",
        //                "player_name":"Nikita"
        //                ,"player_icon":"1",
        //                "text":"Hello World!","created_at":"2024-03-13T21:23:16.465696Z"},
        //{ "player_uuid":"7b867119-c1a7-40da-b51e-294d8f66b7f1",
        //                "player_name":"Nikita",
        //                "player_icon":"1",
        //                "text":"Hello World!",
        //                "created_at":"2024-03-13T21:26:08.729634Z"}
        //        ]
        //
        //        //using UnityWebRequest www = UnityWebRequest.Get("http://193.42.113.235:8001/api/get_messages/?server_uuid=fb988152-8f01-4f9a-b436-3691d2ffe806&fraction=Human");

        form.AddField("server_uuid", "fb988152-8f01-4f9a-b436-3691d2ffe806");
        form.AddField("player_uuid", "7b867119-c1a7-40da-b51e-294d8f66b7f1");
        form.AddField("new_data", JsonUtility.ToJson(DataSaveManager.Instance.MyData));


        //using UnityWebRequest www = UnityWebRequest.Post("http://193.42.113.235:8001/api/update_player_data/", form);
        using UnityWebRequest www = UnityWebRequest.Get("http://193.42.113.235:8001/api/get_player_data/?server_uuid=fb988152-8f01-4f9a-b436-3691d2ffe806&player_uuid=7b867119-c1a7-40da-b51e-294d8f66b7f1");
        yield return www.SendWebRequest();

       if (www.result != UnityWebRequest.Result.Success)
       {
           Debug.LogError(www.error);
       }
       else
       {
       
           string cleanedJsonString = www.downloadHandler.text;
           Debug.Log(cleanedJsonString);
           newData = JsonUtility.FromJson<DataTest>(cleanedJsonString);
          //Debug.Log(newData.data.UnitsDatas);
          //DataSaveManager.Instance.MyData = newData;
          //DataSaveManager.Instance.SaveData();
       
         string path = Application.persistentDataPath + "/" + "test.json";
        
         try
         {
             if (!File.Exists(path))
             {
                 using FileStream stream = File.Create(path);
             }
        
             File.WriteAllText(path, cleanedJsonString);
         }
         catch (Exception e)
         {
        
         }
       }
    }

}
[Serializable] public class DataTest
{
    public Data data;
    public string created_at;
}
