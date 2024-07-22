using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChestSystem : MonoBehaviour
{

    public GameObject ShopViewport;
    public GameObject InteractionViewport;

    [Header("Animation")]
    [SerializeField] private Image ChestAnimImage;
    [SerializeField] private Button endButton;
    public ParticleSystem fallParticle;

    private void OnEnable()
    {
        ChestHolder.OnChestInvoke += ExecuteChestOpenProcces;
    }

    private void OnDisable()
    {
        ChestHolder.OnChestInvoke -= ExecuteChestOpenProcces;
    }

    public void ExecuteChestOpenProcces(ChestData data)
    {
        ShopViewport.SetActive(false);
        InteractionViewport.SetActive(true);
        ChestAnimImage.gameObject.SetActive(true);
        ChestAnimImage.sprite = data.ChestSprite;
        Vector3 chestScale = ChestAnimImage.transform.localScale;
        Quaternion chestRotation = ChestAnimImage.transform.rotation;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(ChestAnimImage.transform.DOMoveY(0, 0.5f)).SetEase(Ease.InOutBack);
        mySequence.Append(ChestAnimImage.transform.DOShakeScale(0.2f, 0.3f));

        mySequence.Append(ChestAnimImage.transform.DOScale(Vector3.one, 2f));
        mySequence.Append(ChestAnimImage.transform.DOShakeScale(0.4f, 0.4f));
        
        mySequence.Append(ChestAnimImage.transform.DOScale(Vector3.one, 1f));
        
        mySequence.Append(ChestAnimImage.transform.DOShakeScale(1f, 0.4f));
        mySequence.Join(ChestAnimImage.transform.DOShakeRotation(1f, 0.5f));
        
        mySequence.Append(ChestAnimImage.transform.DOScale(Vector3.one, 1f));
        
        mySequence.Append(ChestAnimImage.transform.DOShakeScale(1f, 0.4f));
        mySequence.Join(ChestAnimImage.transform.DOShakeRotation(1, 0.5f));
        
        mySequence.Append(ChestAnimImage.transform.DOScale(Vector3.one, 1f));
        
        mySequence.Join(ChestAnimImage.transform.DOShakeRotation(0.3f, 0.5f));
        mySequence.Append(ChestAnimImage.transform.DOShakeScale(6f, 0.1f));

        mySequence.Append(ChestAnimImage.transform.DOScale(Vector3.one, 2f));
        mySequence.Append(ChestAnimImage.DOFade(0, 0.3f));
        mySequence.Join(ChestAnimImage.transform.DOScale(Vector3.zero, 0.3f));

        mySequence.Play().onComplete = () =>
        {
            ChestAnimImage.DOFade(1, 0);
            ChestAnimImage.transform.localScale = chestScale;
            ChestAnimImage.transform.rotation = chestRotation;
            ChestAnimImage.transform.DOMoveY(1000, 0f);

            ChestAnimImage.gameObject.SetActive(false);
            endButton.gameObject.SetActive(true);
            fallParticle.Play();
        };
    }
}
