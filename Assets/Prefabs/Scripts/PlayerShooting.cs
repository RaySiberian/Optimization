using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool spreadShot = false;

    [Header("General")] public Transform gunBarrel;
    public ParticleSystem shotVFX;
    public AudioSource shotAudio;
    public float fireRate = .1f;
    public int spreadAmount = 20;

    [Header("Bullets")] public ProjectileBehaviour bulletPrefab;

    float timer;

    private List<ProjectileBehaviour> bulletsPool = new List<ProjectileBehaviour>();

    // Мне кажется, что нижние два метода можно в один засунуть
    private bool CheckNeedCreateNewSlotInPool()
    {
        return bulletsPool.All(bullet => bullet.gameObject.activeSelf != false);
    }

    private ProjectileBehaviour GetInactiveBulletFromPool()
    {
        return bulletsPool.FirstOrDefault(bullet => bullet.gameObject.activeSelf == false);
    }

    private void InstantiateNewObjectToPool(Vector3 spawnPos, Vector3 rotation)
    {
        var bullet = Instantiate(bulletPrefab);

        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.Euler(rotation);
        bulletsPool.Add(bullet);
    }

    private void InstantiateFromPool(Vector3 spawnPos, Vector3 rotation)
    {
        if (CheckNeedCreateNewSlotInPool())
        {
            InstantiateNewObjectToPool(spawnPos, rotation);
        }
        else
        {
            var bullet = GetInactiveBulletFromPool();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = spawnPos;
            bullet.transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= fireRate)
        {
            Vector3 rotation = gunBarrel.rotation.eulerAngles;
            rotation.x = 0f;

            if (spreadShot)
            	SpawnBulletSpread(rotation);
            else
                SpawnBullet(rotation);


            timer = 0f;

            if (shotVFX)
                shotVFX.Play();

            if (shotAudio)
                shotAudio.Play();
        }
    }

    void SpawnBullet(Vector3 rotation)
    {
        InstantiateFromPool(gunBarrel.position, rotation);
    }

    void SpawnBulletSpread(Vector3 rotation)
    {
        int max = spreadAmount / 2;
        int min = -max;

        Vector3 tempRot = rotation;
        for (int x = min; x < max; x++)
        {
            tempRot.x = (rotation.x + 3 * x) % 360;

            for (int y = min; y < max; y++)
            {
                tempRot.y = (rotation.y + 3 * y) % 360;

                InstantiateFromPool(gunBarrel.position, tempRot);
            }
        }
    }
}