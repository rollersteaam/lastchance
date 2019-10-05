using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableProperties {
    public GameObject quarkEnemy;

    /// <summary>
    /// TODO: IMPLEMENT
    /// Chooses an enemy to spawn at random.
    /// </summary>
    /// <returns></returns>
    public GameObject Choose() {
        return quarkEnemy;
    }

    /// <summary>
    /// TODO: IMPLEMENT
    /// Chooses an enemy based on player's evolution.
    /// </summary>
    /// <returns></returns>
    public GameObject ChooseForPlayerEvo() {
        return quarkEnemy;
    }
}

/// <summary>
/// Spawns enemies in the game.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// The distance before mass-based modification to spawn enemies outside
    /// of the player's view.
    /// </summary>
    [SerializeField] float baseSpawnDistance;
    /// <summary>
    /// The maximum number of enemies before we should stop spawning more.
    /// </summary>
    [SerializeField] int maxConcurrentEnemies;
    [SerializeField] SpawnableProperties spawnableProperties;
    Transform dynamicObjects;
    List<GameObject> spawnedEnemies = new List<GameObject>();
    Character player;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        dynamicObjects = GameObject.FindWithTag("DynamicObjects").transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (spawnedEnemies.Count < maxConcurrentEnemies) {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// Spawns an enemy.
    /// </summary>
    void SpawnEnemy() {
        var position = GenerateRandomPositionFromPlayer();

        var gameObj = Instantiate(
            spawnableProperties.quarkEnemy,
            position,
            Quaternion.identity,
            dynamicObjects.transform
        );
        var combat = gameObj.GetComponent<CharacterCombat>();
        combat.OnDeath += (o, e) => {
            spawnedEnemies.Remove(gameObj);
        };
        spawnedEnemies.Add(gameObj);
    }

    /// <summary>
    /// Generates a random position outside of the player's view.
    /// </summary>
    /// <returns></returns>
    Vector3 GenerateRandomPositionFromPlayer() {
        var statMul = player
            .evolutionProperties
            .CurrentEvolution
            .CalculateStatMul();
        var radius = baseSpawnDistance * statMul;

        var position = GenerateRandomCircularPosition(radius);
        
        return player.transform.position + position;
    }

    /// <summary>
    /// Generates a random position within a circle radius.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    Vector3 GenerateRandomCircularPosition(float radius)
        => new Vector3(
            UnityEngine.Random.Range(-1, 1),
            UnityEngine.Random.Range(-1, 1),
            0
        ) * radius;
}
