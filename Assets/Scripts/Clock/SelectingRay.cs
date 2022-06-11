using UnityEngine;

public class SelectingRay : MonoBehaviour
{
    private OnHandSelectListener currentSelectable;
    private Ray ray;
    private RaycastHit hit;

    private void LateUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            OnHandSelectListener selectable = hit.collider.gameObject.
                GetComponent<OnHandSelectListener>();
            HandsRotation handsRotation = selectable.GetComponent<HandsRotation>();
            if (selectable)
            {
                if (!currentSelectable)
                {
                    currentSelectable = selectable;
                    currentSelectable.Select();
                    EventManager.SendSelectHand(currentSelectable.gameObject.
                        GetComponent<HandsRotation>());
                }
            }
        }else if(Input.GetMouseButtonUp(0))
        {
            if (currentSelectable)
            {
                currentSelectable.Deselect();
                currentSelectable = null;
                EventManager.SendDeselectHand();
            }
        }
    }
}
