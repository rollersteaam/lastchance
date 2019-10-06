﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableProperties
{
    public GameObject quarkEnemy;

    /// <summary>
    /// TODO: IMPLEMENT
    /// Chooses an enemy to spawn at random.
    /// </summary>
    /// <returns></returns>
    public GameObject Choose()
    {
        return quarkEnemy;
    }

    /// <summary>
    /// TODO: IMPLEMENT
    /// Chooses an enemy based on player's evolution.
    /// </summary>
    /// <returns></returns>
    public GameObject ChooseForPlayerEvo()
    {
        return quarkEnemy;
    }
}

/// <summary>
/// Spawns enemies in the game.
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    public event System.EventHandler OnBossKill;

    /// <summary>
    /// The distance before mass-based modification to spawn enemies outside
    /// of the player's view.
    /// </summary>
    [SerializeField] float baseSpawnDistance = 10;
    /// <summary>
    /// The maximum number of enemies before we should stop spawning more.
    /// </summary>
    [SerializeField] int maxConcurrentEnemies = 2;
    [SerializeField]
    SpawnableProperties spawnableProperties
        = new SpawnableProperties();
    int concurrentEnemies;
    Transform dynamicObjects;
    List<GameObject> spawnedEnemies = new List<GameObject>();
    Character player;
    int deathThreshold = 10;
    int waveDeaths;
    bool bossRound;
    bool underlingCooldown;
    int concurrentUnderling;
    int underlingLimit = 20;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = ReferenceManager.Instance.player.GetComponent<Character>();
        dynamicObjects = ReferenceManager.Instance.dynamicObjects.transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!ProgressionManager.Instance.gameStarted) return;

        if (concurrentEnemies < maxConcurrentEnemies)
        {
            SpawnEnemy();
        }

        SpawnUnderlings();
    }

    /// <summary>
    /// How much action is going on right now
    /// </summary>
    /// <returns></returns>
    public int CalculateActionValue()
    {
        if (bossRound)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    void SpawnUnderlings()
    {
        if (underlingCooldown) return;
        if (concurrentUnderling >= underlingLimit) return;
        if (player.evolutionProperties.CurrentEvolution.type == EvolutionType.Force) return;

        underlingCooldown = true;

        var position = GenerateRandomPositionFromPlayer();

        var gameObj = Instantiate(
            spawnableProperties.quarkEnemy,
            position,
            Quaternion.identity,
            dynamicObjects.transform
        );
        var combat = gameObj.GetComponent<CharacterCombat>();
        combat.OnDeath += (o, e) =>
        {
            Destroy(gameObj, 4f);
            concurrentUnderling--;
        };
        var evo = gameObj.GetComponent<EvolvingBody>();
        spawnedEnemies.Add(gameObj);

        Chrono.Instance.After(0.1f, () =>
        {
            var playerType = player.evolutionProperties.CurrentEvolution.type;

            if (playerType == EvolutionType.Quark)
            {
                evo.Evolve(EvolutionType.Force);
            }
            else if (playerType == EvolutionType.Particle)
            {
                evo.Evolve(EvolutionType.Quark);
            }
        });

        concurrentUnderling++;

        Chrono.Instance.After(UnityEngine.Random.Range(1f, 7f), () =>
        {
            underlingCooldown = false;
        });
    }

    /// <summary>
    /// Spawns an enemy.
    /// </summary>
    void SpawnEnemy()
    {
        if (waveDeaths >= deathThreshold)
        {
            return;
        }

        if (bossRound) return;

        Chrono.Instance.After(UnityEngine.Random.Range(2f, 4f), () =>
        {
            var position = GenerateRandomPositionFromPlayer();

            var gameObj = Instantiate(
                spawnableProperties.quarkEnemy,
                position,
                Quaternion.identity,
                dynamicObjects.transform
            );
            var combat = gameObj.GetComponent<CharacterCombat>();
            combat.OnDeath += (o, e) =>
            {
                Destroy(gameObj, 4f);
                spawnedEnemies.Remove(gameObj);
                concurrentEnemies--;
                waveDeaths++;
                bossRound = false;

                if (concurrentEnemies == 0 && waveDeaths >= deathThreshold)
                {
                    bossRound = true;
                    waveDeaths = 0;
                    SpawnBoss();
                }
            };
            var evo = gameObj.GetComponent<EvolvingBody>();
            spawnedEnemies.Add(gameObj);

            Chrono.Instance.After(0.1f, () =>
            {
                evo.Evolve(player.evolutionProperties.CurrentEvolution.type);
            });
        });

        concurrentEnemies++;
    }

    void SpawnBoss()
    {
        var position = GenerateRandomPositionFromPlayer();

        var gameObj = Instantiate(
            spawnableProperties.quarkEnemy,
            position,
            Quaternion.identity,
            dynamicObjects.transform
        );
        var combat = gameObj.GetComponent<CharacterCombat>();
        combat.OnDeath += (o, e) =>
        {
            spawnedEnemies.Remove(gameObj);
            bossRound = false;
            waveDeaths = 0;
            
            OnBossKill?.Invoke(this, System.EventArgs.Empty);
        };
        var evo = gameObj.GetComponent<EvolvingBody>();
        spawnedEnemies.Add(gameObj);

        Chrono.Instance.After(0.1f, () =>
        {
            evo.Evolve(player.evolutionProperties.CurrentEvolution.nextEvolution);
        });
    }

    /// <summary>
    /// Generates a random position outside of the player's view.
    /// </summary>
    /// <returns></returns>
    Vector3 GenerateRandomPositionFromPlayer()
    {
        var statMul = player
            .evolutionProperties
            .CurrentEvolution
            .CalculateStatMul();
        var radius = baseSpawnDistance * statMul;

        var position = GenerateRandomCircularPosition(radius);

        return player.transform.position + position;
    }

    /// <summary>
    /// Generates a random position on a circle with the provided radius.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    Vector3 GenerateRandomCircularPosition(float radius)
    {
        var rads = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Sin(rads),
            Mathf.Cos(rads),
            0
        ) * radius;
    }
}
