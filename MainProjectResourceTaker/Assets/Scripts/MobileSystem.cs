using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileSystem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image joystickBG;
    [SerializeField]
    private Image joystick;
    private Vector2 inputVector;
    public GameObject Effect;
    public GameObject EffectTwo;

    private void Start()
    {
        Effect.SetActive(false);
        EffectTwo.SetActive(false);
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
        Effect.SetActive(true);
        EffectTwo.SetActive(true);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
        Effect.SetActive(false);
        EffectTwo.SetActive(false);
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform,ped.position,ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 3f, pos.y * 3f);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 4), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 4));
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}
