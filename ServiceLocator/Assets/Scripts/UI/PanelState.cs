using DG.Tweening;
using UnityEngine;

public class PanelState : IUIState
{
    private readonly PanelView _view;
    private readonly UISwitcher _switcher;
    private readonly IService _serviceLocator;
    private readonly Score _score;

    private readonly System.Action _onClose;
    private readonly System.Action _onCollect;
    private const float FADE_DURATION = 0.3f;

    public PanelState(PanelView view, UISwitcher switcher, IService serviceLocator, Score score)
    {
        _view = view;
        _switcher = switcher;
        _serviceLocator = serviceLocator;
        _score = score;

        _onClose = () => _switcher.SwitchTo("Main");
        _onCollect = OnCollect;
    }

    public void Enter()
    {
        _view.gameObject.SetActive(true);
        _view.PanelBackground.gameObject.SetActive(true);

        _view.CanvasGroup.alpha = 0f;
        _view.CanvasGroup.interactable = true;

        var c = _view.PanelBackground.color;
        c.a = 0f;
        _view.PanelBackground.color = c;

        _view.SubscribeClose(_onClose);
        _view.SubscribeCollect(_onCollect);

        _view.CanvasGroup.DOFade(1f, FADE_DURATION).SetEase(Ease.Linear);

        _serviceLocator.GetService < IFadeService > ()?.FadeIn(_view.PanelBackground, FADE_DURATION);
        _serviceLocator.GetService<ISoundPlayer>()?.PlayOpenSound();

        UpdateDisplay();
    }

    public void Exit()
    {
        _serviceLocator.GetService<ISaver>()?.SaveScore();
        _serviceLocator.GetService<ISoundPlayer>()?.PlayCloseSound();

        _view.UnsubscribeClose(_onClose);
        _view.UnsubscribeCollect(_onCollect);
        _view.CanvasGroup.interactable = false;

        _view.CanvasGroup.DOFade(0f, FADE_DURATION)
             .SetEase(Ease.Linear)
             .OnComplete(() => _view.gameObject.SetActive(false));

        _serviceLocator.GetService < IFadeService > ()?.FadeOut(_view.PanelBackground, FADE_DURATION);
    }

    private void OnCollect()
    {
        _score.Add();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _view.UpdateScoreDisplay(_score.Value);
    }
}