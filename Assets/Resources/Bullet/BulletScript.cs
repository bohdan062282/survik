using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{

    private LayerMask _excludeLayers;

    private Rigidbody _rigidbody;

    private float _damage = 0.0f;
    private float _startTime;
    private float _lifetime = 4.0f;

    private int _hittingID = 0;


    void Start()
    {

        _excludeLayers = LayerMask.GetMask("Ground");
        _startTime = Time.time;
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        gameObject.SetActive(false);


    }

    void Update()
    {

        if (_startTime + _lifetime < Time.time)
            gameObject.SetActive(false);


    }

    public void release(Vector3 position, Vector3 direction, float speed, float damage, int hittingID)
    {
        gameObject.SetActive(true);

        _startTime = Time.time;

        _damage = damage;
        _hittingID = hittingID;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = position;
        transform.forward = direction;

        _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if ((_excludeLayers.value & (1 << collision.collider.gameObject.layer)) != 0)
        {
            Debug.Log("GroundHit!");
        }
        else
        {
            HittableObject hittableObject = collision.collider.gameObject.GetComponent<HittableObject>();

            hittableObject.hit(_hittingID, _damage);
        }

        gameObject.SetActive(false);

    }

}
