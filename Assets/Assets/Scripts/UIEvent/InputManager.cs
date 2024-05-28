using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField]  public Vector3 mousePosition;

    private void Awake()
    {
        InputManager.instance = this;
    }

    private void FixedUpdate()
    {
        this.GetMouse();
    }

    protected virtual void GetMouse()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        // Cập nhật vị trí của chuột
        this.mousePosition = new Vector3(mouseX, mouseY, 0f);
    }
}
