using UnityEngine;

public class SmallCannedFoodActiveScript : ConsumingActiveScript
{

    [SerializeField] private GameObject openedObject;
    [SerializeField] private GameObject closedObject;

    private bool isOpened = false;

    public override void interract()
    {
        
        if (isOpened) animator.SetBool("IsConsuming", true);
        else openCannedFood();

    }
    private void openCannedFood()
    {

        closedObject.SetActive(false);
        openedObject.SetActive(true);

        isOpened = true;

    }


}
