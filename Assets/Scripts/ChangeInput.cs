using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChangeInput : MonoBehaviour
{

    EventSystem system;

    public Selectable firstInput;   // ô email
    public Button submitButton;     // nút login

    void Start()
    {
        system = EventSystem.current;

        // focus ô đầu tiên khi mở game
        if (firstInput != null)
        {
            firstInput.Select();
        }
    }

    void Update()
    {
        if (system.currentSelectedGameObject == null) return;

        Selectable current = system.currentSelectedGameObject.GetComponent<Selectable>();
        if (current == null) return;

        // SHIFT + TAB → đi lên
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = current.FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        // TAB → đi xuống
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = current.FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        // ENTER → bấm nút Login
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (submitButton != null)
            {
                submitButton.onClick.Invoke();
                Debug.Log("Login button pressed!");
            }
        }
    }

    //  GỌI SAU KHI REGISTER
    public void ResetInput()
    {
        // clear selection cũ
        system.SetSelectedGameObject(null);

        // focus lại ô đầu tiên
        if (firstInput != null)
        {
            firstInput.Select();
        }
    }
}