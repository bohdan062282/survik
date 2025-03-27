using UnityEngine;

public class TargetScript : HittableObject
{

    [SerializeField] private GameObject hitDecalPrefab;

    [SerializeField]
    [Range(0.0f, 10.0f)] private float decalLifetime;




    public override void hit(int hittingTypeID, float damage, Collision collision)
    {
        hit(hittingTypeID, damage);

        GameObject decalObject = Instantiate(hitDecalPrefab, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].point));
        Destroy(decalObject, decalLifetime);
    }
    public override void hit(int hittingTypeID, float damage)
    {
        Debug.Log(gameObject.name + " hitted by: " + hittingTypeID.ToString() + "; damage: " + damage.ToString());
    }


}
