using System.Collections;
using UnityEngine;

public class RollDiceState : IPlayerInputState
{
    private float _waitTimer = 0f;
    private Player _thisPlayer;
    public void OnEnter(Player player)
    {
        _waitTimer = 10f;
        _thisPlayer = player;
    }

    public void OnExecute(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space) || _waitTimer <= 0)
        {
            DiceManager.Instance.RollAndMove(player);
            player.PlayerInput.SetState(new EmptyState());
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.PlayerMovement.MoveToTile(TileManager.Instance.Airport, player);

        }
        _waitTimer -= Time.deltaTime;
    }

    public void OnExit(Player player)
    {

    }

}
