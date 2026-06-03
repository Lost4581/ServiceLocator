using DG.Tweening;
using UnityEngine;
using Zenject;

public class PanelState : IUIState
{
    private readonly PanelView _view;
    private readonly UISwitcher _switcher;
    private readonly ISoundPlayer _soundPlayer;
    private readonly IFadeService _fadeService;
    private readonly ISaver _saver;
    private readonly Score _score;

    private readonly System.Action _onClose;
    private readonly System.Action _onCollect;
    private const float FADE_DURATION = 0.3f;

    public PanelState(PanelView view, UISwitcher switcher, ISoundPlayer soundPlayer, IFadeService fadeService, ISaver saver, Score score)
    {
        _view = view;
        _switcher = switcher;
        _soundPlayer = soundPlayer;
        _fadeService = fadeService;
        _saver = saver;
        _score = score;

        _onClose = () => _switcher.SwitchTo("Main");
        _onCollect = OnCollect;
    }

    public void Enter()
    {
        _view.gameObject.SetActive(true);
        _view.CanvasGroup.alpha = 0f;
        _view.CanvasGroup.interactable = true;

        _view.SubscribeClose(_onClose);
        _view.SubscribeCollect(_onCollect);

        _view.CanvasGroup.DOFade(1f, FADE_DURATION).SetEase(Ease.Linear);
        _fadeService.FadeIn(_view.PanelBackground, FADE_DURATION);
        _soundPlayer.PlayOpenSound();

        UpdateDisplay();
    }

    public void Exit()
    {
        _saver.SaveScore();
        _soundPlayer.PlayCloseSound();

        _view.UnsubscribeClose(_onClose);
        _view.UnsubscribeCollect(_onCollect);
        _view.CanvasGroup.interactable = false;

        _view.CanvasGroup.DOFade(0f, FADE_DURATION)
             .SetEase(Ease.Linear)
             .OnComplete(() => _view.gameObject.SetActive(false));

        _fadeService.FadeOut(_view.PanelBackground, FADE_DURATION);
    }

    private void OnCollect()
    {
        _score.Add();
        UpdateDisplay();
    }

    private void UpdateDisplay() => _view.UpdateScoreDisplay(_score.Value);
}