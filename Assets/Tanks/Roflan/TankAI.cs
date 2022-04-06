using UnityEngine;

public class TankAI : MonoBehaviour, ITickAble
{
    public Transform player;

    public void Tick()
    {
        if (player == null) return;

        transform.LookAt(player);
        transform.Translate(0, 0, 0.05f);
    }

    public void OnDestroy()
    {
        UpdateManager.Instance.RemoveFromManager(this);
    }

    public void OnCreate()
    {
        UpdateManager.Instance.AddToManager(this);
    }
}