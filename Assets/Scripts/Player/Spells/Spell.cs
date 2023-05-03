using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    public SpellScriptableObject SpellToCast;

    public StatController stats;

    private SphereCollider myCollider;
    private Rigidbody myRigidbody;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = SpellToCast.SpellRadius;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = true;

        //Destroy(this.gameObject, SpellToCast.Lifetime);
    }

    private void Update()
    {
        if (SpellToCast.Speed > 0)
        {
            transform.Translate(Vector3.forward * SpellToCast.Speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Apply spell effects to whatever we hit
        // Apply hit particle effects
        // Apply sound effects

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            //Destruir enemigo
            enemy.TakeDamage(stats.CalculateDmg(stats.mainHand.damage, enemy.gameObject.GetComponent<StatController>().defense));
        }

        deactivateObject();
    }

    public void setLifeTime()
    {
        Invoke(nameof(deactivateObject), SpellToCast.Lifetime);
    }

    void deactivateObject()
    {
        gameObject.SetActive(false);
    }
}
