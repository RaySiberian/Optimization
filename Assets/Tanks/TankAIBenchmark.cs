using UnityEngine;

public class TankAIBenchmark : MonoBehaviour
{
    Transform[] tanks;
    public int numberOfTanks;
    public GameObject tankPrefab;
    [SerializeField] private Transform player;

    void Start()
    {
        tanks = new Transform[numberOfTanks];
        for (int i = 0; i < numberOfTanks; i++)
        {
            tanks[i] = Instantiate(tankPrefab, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)),
                Quaternion.identity).transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        foreach (Transform t in tanks)
        {
            t.LookAt(player);
            t.Translate(0, 0, 0.05f);
        }
    }
}