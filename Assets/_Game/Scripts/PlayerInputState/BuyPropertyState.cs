using UnityEngine;

public class BuyPropertyState : IPlayerInputState
{
    private float _timer = 0f;
    private Player _thisPlayer;

    public void OnEnter(Player player)
    {
        _timer = 20f;
        _thisPlayer = player;
        BuyPropertyUI.Instance.onPurchase += OnPurchase;
        BuyPropertyUI.Instance.Show();
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
        BuyPropertyUI.Instance.onPurchase -= OnPurchase;
        BuyPropertyUI.Instance.Hide();
    }

    private void OnPurchase(int buildingLevel)
    {
        _thisPlayer.BuyProperty(buildingLevel);
        _thisPlayer.PlayerInput.SetState(new EmptyState());
    }
}
