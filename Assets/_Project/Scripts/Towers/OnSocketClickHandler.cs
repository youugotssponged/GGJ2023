using UnityEngine;

public class OnSocketClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name.Contains("SpawnPoint") && !EngineHelper.isMousePointerOverUI())
                {
                    var re = hit.collider.gameObject.GetComponentInParent<IInteractable>();
                    re?.Interact();
                }   
            }
        }
    }
}
