using UnityEngine;

public class HealthSystem
{
    



    public HealthSystem()
    {
        HP = 100.0f;
        Food = 100.0f;
        Water = 100.0f;
    }
    public HealthSystem(float hP, float food, float water)
    {
        HP = hP;
        Food = food;
        Water = water;
    }
    public void affectConsumable(ConsumingActiveScript.ConsumingParams consumingParams)
    {
        HP += consumingParams.HP;
        Water += consumingParams.Water;
        Food += consumingParams.Food;

        Debug.Log("HP: " + HP.ToString() + "; Food: " + Food.ToString() + "; Water: " + Water.ToString());
    }

    public float HP { get; private set; }
    public float Food { get; private set; }
    public float Water { get; private set; }

}
