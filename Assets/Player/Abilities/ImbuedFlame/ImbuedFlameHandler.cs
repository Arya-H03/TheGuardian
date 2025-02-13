using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImbuedFlameHandler : ActiveAbilityHandler
{
    [SerializeField] GameObject firstFlameSlash;
    [SerializeField] GameObject secondFlameSlash;

    PlayerSwordAttackState swordAttackState;

    private void Start()
    {
        swordAttackState = GameManager.Instance.Player.GetComponentInChildren<PlayerSwordAttackState>();
        swordAttackState.OnFirstSwordSwingEvent += SpawnFirstSwingFlame;
        swordAttackState.OnSecondSwordSwingEvent += SpawnSecondSwingFlame;
    }

    private void SpawnFirstSwingFlame()
    {
        GameObject obj = Instantiate(firstFlameSlash, swordAttackState.FirstSwingCenter.position, Quaternion.Euler(35, 0, 0));

        Vector3 scale = GameManager.Instance.Player.transform.localScale;

        Vector2 launchVec = Vector2.zero;
        if (scale.x == 1)
        {
            launchVec = new Vector2(1, 0);
        }
        if (scale.x == -1)
        {
            launchVec = new Vector2(-1, 0);
            obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
        }
        obj.GetComponent<Rigidbody2D>().velocity = launchVec * 3;
        Destroy(obj,0.5f);

    }

    private void SpawnSecondSwingFlame()
    {
        GameObject obj = Instantiate(secondFlameSlash, swordAttackState.SecondSwingCenter.position, Quaternion.Euler(-70,0,0));

        Vector3 scale = GameManager.Instance.Player.transform.localScale;

        Vector2 launchVec = Vector2.zero;
        if (scale.x == 1)
        {
            launchVec = new Vector2(1, 0);
        }
        if (scale.x == -1)
        {
            launchVec = new Vector2(-1, 0);
            obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
        }
        obj.GetComponent<Rigidbody2D>().velocity = launchVec * 3;
        Destroy(obj, 0.5f);

    }
}
