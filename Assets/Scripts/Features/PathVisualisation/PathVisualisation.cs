using UnityEngine;

public class PathVisualisation : MonoBehaviour {

    [SerializeField]
    private PathLineVisualisation pathLineVis;

    private GameObject activeVisualisation;

    private void Start() {
        activeVisualisation = pathLineVis.gameObject;
    }

    public void NextLineVisualisation() {

        pathLineVis.gameObject.SetActive(false);
        activeVisualisation.SetActive(true);
    }

    public void ToggleVisualVisibility() {
        activeVisualisation.SetActive(!activeVisualisation.activeSelf);
    }
}
