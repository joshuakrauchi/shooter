using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObjects/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    private List<Enemy> Enemies { get; set; }

    public void Initialize()
    {
        Enemies = new List<Enemy>();
    }

    public void UpdateEnemies()
    {
        foreach (Enemy enemy in Enemies.ToArray())
        {
            enemy.UpdateUpdateable();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    public void SetMinionAnimatorSpeed(float speed)
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy is Minion minion)
            {
                minion.Animator.speed = speed;
            }
        }
    }
}