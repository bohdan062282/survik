using UnityEngine;

public class TargetScript : HittableObject
{

    [SerializeField] private GameObject hitDecalPrefab;

    [SerializeField]
    [Range(0.0f, 100.0f)] private float decalLifetime;




    public override void hit(int hittingTypeID, float damage, Collision collision)
    {
        hit(hittingTypeID, damage);

        foreach (ContactPoint contactPoint in collision.contacts)
        {
            GameObject decalObject = Instantiate(hitDecalPrefab, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            Destroy(decalObject, decalLifetime);
        }


    }
    public override void hit(int hittingTypeID, float damage)
    {
        Debug.Log(gameObject.name + " hitted by: " + hittingTypeID.ToString() + "; damage: " + damage.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
