using DG.Tweening;
using UnityEngine.UI;

public class FadeService : IFadeService
{
    public void FadeIn(Image image, float duration)
    {
        image.gameObject.SetActive(true);
        image.DOFade(1f, duration).From(0f).SetEase(Ease.Linear);
    }

    public void FadeOut(Image image, float duration)
    {
        image.DOFade(0f, duration).SetEase(Ease.Linear);
    }
}