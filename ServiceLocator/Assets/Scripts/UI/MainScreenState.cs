public class MainScreenState : IUIState
{
    private readonly MainScreenView _view;
    private readonly UISwitcher _switcher;
    private readonly System.Action _onOpen;

    public MainScreenState(MainScreenView view, UISwitcher switcher)
    {
        _view = view;
        _switcher = switcher;
        _onOpen = () => _switcher.SwitchTo("Panel");
    }

    public void Enter()
    {
        _view.gameObject.SetActive(true);
        _view.SubscribeOpen(_onOpen);
    }

    public void Exit()
    {
        _view.UnsubscribeOpen(_onOpen);
    }
}