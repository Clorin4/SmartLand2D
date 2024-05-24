using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bananaPrefab;
    public GameObject orangePrefab;

    public Transform[] appleSpawnPoints;
    public Transform[] bananaSpawnPoints;
    public Transform[] orangeSpawnPoints;

    public float respawnTime = 2f;

    private void Start()
    {
        // Hacer spawn de las frutas al comienzo del juego
        SpawnAllFruits();
    }

    private void SpawnAllFruits()
    {
        // Spawn de manzanas
        SpawnFruit(applePrefab, appleSpawnPoints);

        // Spawn de plátanos
        SpawnFruit(bananaPrefab, bananaSpawnPoints);

        // Spawn de naranjas
        SpawnFruit(orangePrefab, orangeSpawnPoints);
    }

    private void SpawnFruit(GameObject prefab, Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Verificar si ya hay una fruta en este spawn point
            if (spawnPoint.childCount == 0)
            {
                Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
            }
        }
    }

    // Método para respawnear una fruta después de un tiempo
    private IEnumerator RespawnFruit(GameObject fruitPrefab, Transform[] spawnPoints)
    {
        yield return new WaitForSeconds(respawnTime);

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Verificar si no hay una fruta en este spawn point
            if (spawnPoint.childCount == 0)
            {
                Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
            }
        }
    }

    // Método que se llama cuando una fruta es recolectada
    public void FruitCollected(string fruitType)
    {
        switch (fruitType)
        {
            case "Manzanas":
                StartCoroutine(RespawnFruit(applePrefab, appleSpawnPoints));
                break;
            case "Platanos":
                StartCoroutine(RespawnFruit(bananaPrefab, bananaSpawnPoints));
                break;
            case "Naranjas":
                StartCoroutine(RespawnFruit(orangePrefab, orangeSpawnPoints));
                break;
            default:
                Debug.LogError("Tipo de fruta no reconocido: " + fruitType);
                break;
        }
    }
}
