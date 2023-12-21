using UnityEngine;

public class RollDiceState : IPlayerInputState
{
    private float _timer = 0f;
    public void OnInitialize(Player player)
    {
        _timer = 10f;
    }

    public void OnExecute(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.MoveWithDice();
            player.PlayerInput.SetState(new EmptyState());
        }
        _timer -= Time.deltaTime;
    }

    public void OnExit(Player player)
    {

    }

}
