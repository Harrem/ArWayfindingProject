using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListController : MonoBehaviour
{
    public GameObject itemPrefab; // the prefab for each item in the list
    public NavigationController navController;
    public List<string> items; // the list of items to display
    public TextAsset locationData;
    public float itemHeight = 50f;
    private Color grey = new Color(110, 110, 110);
    void Start()
    {
        RectTransform contentTransform = GetComponent<RectTransform>();

        var locations = deserializeLocationData();

        // generate the list items
        items = new List<string>();
        int i = 0;
        foreach (var location in locations)
        {
            // instantiate a new item prefab and set its parent to the content transform
            GameObject newItem = Instantiate(itemPrefab, transform);

            // set the text of the item
            newItem.GetComponentInChildren<Text>().text = location.Name;
            RectTransform itemTransform = newItem.GetComponent<RectTransform>();
            itemTransform.anchoredPosition = new Vector2(0, -i * itemHeight);
            itemTransform.anchorMin = new Vector2(0, 1);
            itemTransform.anchorMax = new Vector2(1, 1);

            // set the pivot to the top-center
            itemTransform.pivot = new Vector2(0.5f, 1);

            // set the left and right offsets to 10 pixels
            itemTransform.offsetMin = new Vector2(0, itemTransform.offsetMin.y);
            itemTransform.offsetMax = new Vector2(0, itemTransform.offsetMax.y);
            // add an event listener to the item


            newItem.GetComponentsInChildren<Image>()[1].enabled = false;

            if (i % 2 == 1)
            {
                newItem.GetComponent<Image>().color = Color.grey;
            }
            newItem.GetComponent<Button>().onClick.AddListener(() => OnItemClick(location));
            i++;
        }
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, items.Count * itemHeight);
    }

    void OnItemClick(Target item)
    {
        Debug.Log("User selected item: " + item.Name);
        navController.TargetPosition = item.Position;
    }
    private IEnumerable<Target> deserializeLocationData()
    {
        return JsonUtility.FromJson<TargetWrapper>(locationData.text).TargetList;
    }
}