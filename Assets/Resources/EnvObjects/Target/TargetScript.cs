using UnityEngine;

public class TargetScript : HittableObject
{

    [SerializeField] private GameObject hitDecalPrefab;


    public override void hit(int hittingTypeID, float damage)
    {
        
        Debug.Log(gameObject.name + "hitted by: " + hittingTypeID.ToString() + "; damage: " + damage.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        //Instantiate(hitDecalPrefab, collision.contacts[0].point, );

    }


}
