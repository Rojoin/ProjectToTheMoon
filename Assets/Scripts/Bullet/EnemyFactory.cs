using UnityEngine;

public class EnemyFactory
{
    public EnemyMovement CreateEnemy(EnemyMovement enemy,Transform holder)
    {
        var aux = GameObject.Instantiate(enemy,holder);
        return aux;
    }
}