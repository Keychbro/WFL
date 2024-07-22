using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestHolder : MonoBehaviour
{
    public static event Action<ChestData> OnChestInvoke;

    [SerializeField] private ChestData _data;
    [SerializeField] private ChestHodlerView view;

    private void Start()
    {
        view.Init();
        view.OnOpenEvent += OpenChest;
    }

    public void OpenChest()
    {
        OnChestInvoke?.Invoke(_data);
        Debug.Log("Open chest");
    }

    private void OnValidate()
    {
        try
        {
            view.UpdateView(_data);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}

[Serializable]
public class ChestHodlerView
{
    public event Action OnOpenEvent;

    [SerializeField] private TextMeshProUGUI toolsText;
    [SerializeField] private TextMeshProUGUI diamondsText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI marksText;

    [Space(10)]
    [SerializeField] private Button _openButton;
    [SerializeField] private Image _chestImage;

    public void Init()
    {
        _openButton.onClick.AddListener(OnOpen);
    }

    public void OnOpen()
    {
        OnOpenEvent?.Invoke();
    }

    public void UpdateView(ChestData Model)
    {
        _chestImage.sprite = Model.ChestSprite;
        toolsText.text = $"{Model.ToolsRange.x} - {Model.ToolsRange.y} | {Model.ToolsChance}%";
        diamondsText.text = $"{Model.DiamondsRange.x} - {Model.DiamondsRange.y} | {Model.DiamondsChance}%";
        coinsText.text = $"{Model.GoldRange.x} - {Model.GoldRange.y} | {Model.GoldChance}%";
        marksText.text = $"{Model.MarkRange.x} - {Model.MarkRange.y} | {Model.MarkChance}%";
    }
}
