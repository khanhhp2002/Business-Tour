using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IPlayerInputState _currentState;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(_player);
        }
    }
    public void SetState(IPlayerInputState state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(_player);
        }
        _currentState = state;
        _currentState.OnEnter(_player);
    }
}
