using UnityEngine;

public class TabForList : MonoBehaviour
{
    public RectTransform panel;
    public float slideSpeed = 500f;
    public float slideSmoothness = 5f;

    private Vector2 originalPosition;
    private bool isPanelVisible = false;

    private void Start()
    {
        originalPosition = panel.anchoredPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPanelVisible = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isPanelVisible = false;
        }

        float targetX = isPanelVisible ? 0f : originalPosition.x;
        float step = slideSpeed * Time.deltaTime;
        float smoothStep = slideSmoothness * Time.deltaTime;
        Vector2 targetPosition = new Vector2(targetX, panel.anchoredPosition.y);
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPosition, smoothStep);
    }
}
