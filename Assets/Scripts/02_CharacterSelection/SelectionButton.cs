using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private string orderName;
    private SelectionMenuManager m_selectionManager;

    public void Initialize(SelectionMenuManager manager, string orderName)
    {
        m_selectionManager = manager;
        this.orderName = orderName;
    }

    public void LoadLevel()
    {
        m_selectionManager.loadScene(orderName);
    }

    public void OnSelect(BaseEventData eventData)
    {
        m_selectionManager.changeText(orderName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_selectionManager.changeText(orderName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_selectionManager.changeText("");
    }
}
