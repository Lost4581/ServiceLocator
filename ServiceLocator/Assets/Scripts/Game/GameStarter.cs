using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    private UISwitcher _switcher;

    [Inject]
    public void Construct(UISwitcher switcher)
    {
        _switcher = switcher;
    }

    void Start()
    {
        _switcher.SwitchTo("Main");
    }
}