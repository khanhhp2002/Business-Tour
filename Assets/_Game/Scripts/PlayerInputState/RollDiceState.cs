using UnityEngine;

public class RollDiceState : IPlayerInputState
{
    private float _timer = 0f;
    public void OnEnter(Player player)
    {
        _timer = 10f;
    }

    public void OnExecute(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space) || _timer <= 0)
        {
            player.MoveWithDice();
            player.PlayerInput.SetState(new EmptyState());
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.PlayerMovement.MoveToTile(TileManager.Instance.Airport, player);

        }
        _timer -= Time.deltaTime;
    }

    public void OnExit(Player player)
    {

    }

}
