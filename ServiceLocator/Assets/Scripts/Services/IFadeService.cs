using UnityEngine.UI;

public interface IFadeService
{
    void FadeIn(Image image, float duration);
    void FadeOut(Image image, float duration);
}