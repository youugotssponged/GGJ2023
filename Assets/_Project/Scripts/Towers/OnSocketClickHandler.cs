using UnityEngine;

public class OnSocketClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name.Contains("SpawnPoint"))
                {
                    var re = hit.collider.gameObject.GetComponentInParent<IInteractable>();
                    re.Interact();
                }   
            }
        }
    }
}
