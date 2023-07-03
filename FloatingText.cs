
using UnityEngine;
using UnityEngine.UI;

public class FloatingText 
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShow;

    public void Show()
    {
        active = true;
        lastShow = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        //      10   -     7     >    2
        if (Time.time - lastShow > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;


    }
}
