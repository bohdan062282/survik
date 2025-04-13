using System;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    [SerializeField] private PlayerController _player;
    [SerializeField][Range(0.0f, 10.0f)] private float punchDistance;

    private int _hittingID = 0;
    private float _damage = 5.0f;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void hit()
    {
        GameObject gameObject =
            _player.getFocusObject(punchDistance, _player.hittableLayerMask);

        if (gameObject != null)
        {
            Debug.Log(gameObject.name);

            HittableObject hittableObject = gameObject.GetComponent<HittableObject>();
            if (hittableObject != null) hittableObject.hit(_hittingID, _damage);
        }
    }
    public void startPunch() => CanPunch = false;
    public void onPunchHit()
    {
        if (_player.stateMachine2.CurrentState.Type == gameCore.StateType.CombatState) hit();
    }
    public void onPunchEnd()
    {
        CanPunch = true;
        _player.animator.SetBool(PlayerAnimationParams.isPunch, false);
    }
    public bool CanPunch { get; private set; } = true;
}
