using System.Collections.Generic;
using UnityEngine;

public abstract class HittableObject : MonoBehaviour
{

    protected float _HP = 100.0f;
    protected Dictionary<int, float> _hittingMultiplayers = new Dictionary<int, float>();

    
    public virtual void hit(int hittingTypeID, float damage, Collision collision)
    {
        hit(hittingTypeID, damage);
    }
    public virtual void hit(int hittingTypeID, float damage)
    {
        if (_hittingMultiplayers.ContainsKey(hittingTypeID))
            damage *= _hittingMultiplayers[hittingTypeID];

        _HP -= damage;

        Debug.Log(_HP);

        if (_HP <= 0) onZeroHP();
    }
    public virtual void onZeroHP()
    {
        Destroy(gameObject);
    }

    
}
