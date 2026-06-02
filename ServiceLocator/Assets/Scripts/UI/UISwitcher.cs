using System.Collections.Generic;

public class UISwitcher
{
    private readonly Dictionary<string, IUIState> _states = new();
    private IUIState _current;

    public void Register(string key, IUIState state)
    {
        _states[key] = state;
    }

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