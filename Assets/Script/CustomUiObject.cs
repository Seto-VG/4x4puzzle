using UnityEngine;
using DG.Tweening;

public class CustomObject : InteractableObject
{

    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    float onPointerEnterScale = 1;
    [SerializeField]
    float onPointerDownScale = 1;
    [SerializeField]
    float duration = 0.1f;
    [SerializeField]
    Ease ease = Ease.OutQuad;

    protected override void AdditionalOnPointerEnterProcess()
    {
        ChangeObjectScale(onPointerEnterScale);
    }

    protected override void AdditionalOnPointerExitProcess()
    {
        ChangeObjectScale(1);
    }

    protected override void AdditionalOnPointerDownProcess()
    {
        ChangeObjectScale(onPointerDownScale);
    }

    protected override void AdditionalOnPointerUpProcess()
    {
        ChangeObjectScale(1);
    }

    void ChangeObjectScale(float scale)
    {
        rectTransform.DOScale(Vector3.one * scale, duration).SetLink(gameObject).SetEase(ease).SetUpdate(true);
    }

}