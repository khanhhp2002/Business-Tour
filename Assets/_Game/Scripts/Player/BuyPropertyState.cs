using UnityEngine;

public class BuyPropertyState : IPlayerInputState
{
    private float _timer = 0f;
    public void OnInitialize(Player player)
    {
        _timer = 20f;
    }

    public void OnExecute(Player player)
    {
        if (_timer <= 0f) // Cancel buy property by force
        {
            player.PlayerInput.SetState(new EmptyState());
        }
        _timer -= Time.deltaTime;
    }

    public void OnExit(Player player)
    {

    }
}
