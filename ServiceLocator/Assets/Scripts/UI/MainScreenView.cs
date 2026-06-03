using UnityEngine;
using UnityEngine.UI;

public class MainScreenView : MonoBehaviour
{
    [SerializeField] private Button _openButton;

    public void SubscribeOpen(System.Action action) => _openButton.onClick.AddListener(action.Invoke);
    public void UnsubscribeOpen(System.Action action) => _openButton.onClick.RemoveListener(action.Invoke);
}