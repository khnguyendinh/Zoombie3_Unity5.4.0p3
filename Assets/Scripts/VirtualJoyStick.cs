using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class VirtualJoyStick : MonoBehaviour ,IDragHandler,
    IPointerUpHandler,IPointerDownHandler{
    private Image bkgImage;
    private Image joystickImage;
    public Vector3 InputDirection { set; get; }
    private void Start()
    {
        bkgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bkgImage.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = (pos.x / bkgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bkgImage.rectTransform.sizeDelta.y);
            float x = (bkgImage.rectTransform.pivot.x == 1) ?
                pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bkgImage.rectTransform.pivot.y == 1) ?
               pos.y * 2 + 1 : pos.y * 2 - 1;
            InputDirection = new Vector3(x, y, 0);
            InputDirection = (InputDirection.magnitude > 1) ?
                InputDirection.normalized : InputDirection;
            joystickImage.rectTransform.anchoredPosition =
                new Vector3(InputDirection.x * (bkgImage.rectTransform.sizeDelta.x / 3),
                InputDirection.y * (bkgImage.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }
}
