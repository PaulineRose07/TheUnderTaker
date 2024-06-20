using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightReveal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase component))
        {
            component.ShowYourself();
            component.OnTriggerReaction();
        }
        if(collision.gameObject.TryGetComponent<GraveBehavior>(out GraveBehavior behavior))
        {
            behavior.MaterialChangeToUnlit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase component))
        {
            component.HideYourself();
            component.OnTriggerExitReaction();
        }
        if (collision.gameObject.TryGetComponent<GraveBehavior>(out GraveBehavior behavior))
        {
            behavior.MaterialBackToDark();
        }
    }
}
