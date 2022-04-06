using System;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance;
    [SerializeField] private ITickAble[] TickAbles;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        TickAbles = new ITickAble[150];
    }

    private void Update()
    {
        foreach (var tickAble in TickAbles)
        {
            tickAble?.Tick();
        }
    }

    public void AddToManager(ITickAble tickAble)
    {
        TickAbles[FindFreeSlotIndex()] = tickAble;
    }

    public void RemoveFromManager(ITickAble tickAble)
    {
        for (int i = 0; i < TickAbles.Length; i++)
        {
            if (tickAble == TickAbles[i])
            {
                TickAbles[i] = null;
            }
        }
    }

    private int FindFreeSlotIndex()
    {
        for (int i = 0; i < TickAbles.Length; i++)
        {
            if (TickAbles[i] == null)
            {
                return i;
            }
        }

        return -1;
    }
}