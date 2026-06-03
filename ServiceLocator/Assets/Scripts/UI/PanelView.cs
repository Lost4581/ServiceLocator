using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class PanelView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _collectButton;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _panelBackground;

    private CanvasGroup _canvasGroup;

    public Image PanelBackground => _panelBackground;
    public TextMeshProUGUI ScoreText => _scoreText;

    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent < CanvasGroup > ();
            return _canvasGroup;
        }
    }

    public void SubscribeClose(System.Action action) => _closeButton.onClick.AddListener(action.Invoke);
    public void UnsubscribeClose(System.Action action) => _closeButton.onClick.RemoveListener(action.Invoke);
    public void SubscribeCollect(System.Action action) => _collectButton.onClick.AddListener(action.Invoke);
    public void UnsubscribeCollect(System.Action action) => _collectButton.onClick.RemoveListener(action.Invoke);
    public void UpdateScoreDisplay(int value) => _scoreText.text = value.ToString();
}