using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Views")]
    [SerializeField] private MainScreenView _mainScreenView;
    [SerializeField] private PanelView _panelView;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;

    private void Awake()
    {
        var fadeService = new FadeService();
        var soundPlayer = new SoundPlayer(_audioSource, _openSound, _closeSound, null, null);
        var score = new Score();
        var saver = new PlayerPrefsSaver(score);
        var serviceLocator = new ServiceLocator(fadeService, soundPlayer, saver);

        var switcher = new UISwitcher();
        var mainState = new MainScreenState(_mainScreenView, switcher);
        var panelState = new PanelState(_panelView, switcher, soundPlayer, fadeService, saver, score);

        switcher.Register("Main", mainState);
        switcher.Register("Panel", panelState);
        switcher.SwitchTo("Main");
    }
}