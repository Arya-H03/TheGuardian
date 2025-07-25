using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    GameObject enemyTarget;
    private float damage ;
    public GameObject EnemyTarget { get => enemyTarget; set => enemyTarget = value; }
    public float Damage { get => damage; set => damage = value; }

    public void DestroyClone()
    {
        Destroy(gameObject);
    }

    public void AttackEnemy()
    {
        enemyTarget.GetComponent<EnemyController>().OnEnemyHit(Damage, enemyTarget.transform.position, HitSfxType.sword,0.5f);
    }


}
