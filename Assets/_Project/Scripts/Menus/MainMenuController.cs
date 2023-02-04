using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(BaseEventData data)
    {
        Debug.Log("pointer enter");
        var pointerEvent = data as PointerEventData;
        var gameObject = pointerEvent.hovered.First();
        gameObject.GetComponent<RawImage>().color = new Color(255, 245, 161);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(BaseEventData data)
    {
        Debug.Log("pointer exit");
        var pointerEvent = data as PointerEventData;
        var gameObject = pointerEvent.hovered.First();
        gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
    }
}
