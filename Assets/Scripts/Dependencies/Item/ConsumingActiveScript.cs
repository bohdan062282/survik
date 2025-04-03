using UnityEngine;
using gameCore;

public class ConsumingActiveScript : ActiveItemScript
{

    [SerializeField] private float HP;
    [SerializeField] private float Food;
    [SerializeField] private float Water;

    [SerializeField] private Animator animator;


    private ConsumingParams _consumingParams;


    void Start()
    {

        _consumingParams = new ConsumingParams(HP, Food, Water);

    }
    public override void interract()
    {
        base.interract();

        animator.SetBool("IsConsuming", true);
    }
    public void onConsumed()
    {
        _playerController.useConsumable(_consumingParams);
    }


    public struct ConsumingParams
    {

        public float HP;
        public float Food;
        public float Water;

        public ConsumingParams(float hp, float food, float water)
        {
            HP = hp;
            Food = food;
            Water = water;
        }

    }
    
}
