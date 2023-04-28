using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListGenerator : MonoBehaviour
{
    public GameObject itemPrefab; // the prefab for each item in the list
    public List<string> items; // the list of items to display
    public float itemHeight = 50f;
    private Color grey = new Color(110,110,110);

    void Start()
    {
        RectTransform contentTransform = GetComponent<RectTransform>();

        for (int i = 0; i<30; i++)
        {
            items.Add("item: "+ i);
        }
        // generate the list items
        for (int i = 0; i < items.Count; i++)
        {
            // create a new item GameObject
            GameObject newItem = Instantiate(itemPrefab, transform);

            // set the text of the item
            newItem.GetComponentInChildren<Text>().text = items[i];
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
            int index = i;


            newItem.GetComponentsInChildren<Image>()[1].enabled = false;

            if(i % 2 == 1)
            {
                newItem.GetComponent<Image>().color = Color.grey;
            }
            newItem.GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[index]));
        }
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, items.Count * itemHeight);
    }

    void OnItemClick(string item)
    {
        // execute code in response to the user's selection
        Debug.Log("User selected item: " + item);
        
    }
}