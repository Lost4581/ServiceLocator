using System.Collections.Generic;
using Zenject;

public class UISwitcher
{
    private readonly Dictionary<string, IUIState> _states = new();
    private IUIState _current;

    [Inject]
    public void Construct(
        [Inject(Id = "Main")] IUIState mainState,
        [Inject(Id = "Panel")] IUIState panelState)
    {
        Register("Main", mainState);
        Register("Panel", panelState);
    }

    public void Register(string key, IUIState state) => _states[key] = state;

    public void SwitchTo(string key)
    {
        _current?.Exit();
        if (_states.TryGetValue(key, out var next))
        {
            _current = next;
            _current.Enter();
        }
    }
}