using System.Collections.Generic;

public class EnemyManager
{
    private static EnemyManager _instance;
    public static EnemyManager Instance => _instance ??= new EnemyManager();
    private List<Enemy> _enemies;

    private EnemyManager()
    {
        _enemies = new List<Enemy>();
    }

    public void UpdateEnemies()
    {
        foreach (var enemy in _enemies.ToArray())
        {
            enemy.UpdateEnemy();
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
}