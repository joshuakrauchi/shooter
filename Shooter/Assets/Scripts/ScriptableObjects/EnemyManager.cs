using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Manager")]
public class EnemyManager : ScriptableObject
{
    private readonly List<Enemy> _enemies;

    private EnemyManager()
    {
        _enemies = new List<Enemy>();
    }

    public void UpdateEnemies()
    {
        foreach (Enemy enemy in _enemies.ToArray())
        {
            enemy.UpdateUpdatable();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public void SetMinionAnimatorSpeed(float speed)
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy is Minion minion)
            {
                minion.Animator.speed = speed;
            }
        }
    }
}