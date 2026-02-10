using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapContent : MonoBehaviour
{
    public Button map;
    public Button close_map;
    public GameObject mapPanel;

    void Start()
    {
        mapPanel.SetActive(false);

        if (map != null)
        {
            map.onClick.AddListener(OpenMapPanel);
        }
        if (close_map != null)
        {
            close_map.onClick.AddListener(CloseMapPanel);
        }   
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "map")
        {
            map.gameObject.SetActive(false);
        }
        else
        {
            map.gameObject.SetActive(true);
        }
    }

    public void OpenMapPanel()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMapPanel()
    {
        mapPanel.SetActive(false);
    }
}
