using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankSpawner : MonoBehaviour
{
    public int numberOfTanks;
    public TankAI tankPrefab;
    private List<TankAI> tankAis = new List<TankAI>();
    [SerializeField] private Transform player;
    [SerializeField] private UpdateManager manager;

    void Start()
    {
        for (int i = 0; i < numberOfTanks; i++)
        {
            var tank = Instantiate(tankPrefab, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)),
                Quaternion.identity);
            tank.player = player;
            tankAis.Add(tank);
            tank.OnCreate();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < tankAis.Count / 2; i++)
            {
                tankAis.Remove(tankAis[i]);
                tankAis[i].OnDestroy();
                Destroy(tankAis[i].gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var tank = Instantiate(tankPrefab, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)),
                Quaternion.identity);
            tank.player = player;
            tank.OnCreate();
            tankAis.Add(tank);
        }
    }
}