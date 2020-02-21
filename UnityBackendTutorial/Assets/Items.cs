using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallback;

    private void Start()
    {
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString) {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;

                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };
            StartCoroutine(Main.Instance.Web.GetItem(itemId, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            item.transform.Find("name").GetComponent<Text>().text = itemInfoJson["name"];
            item.transform.Find("price").GetComponent<Text>().text = itemInfoJson["price"];
            item.transform.Find("description").GetComponent<Text>().text = itemInfoJson["description"];
            item.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string iId = itemId;
                string uId = Main.Instance.UserInfo.UserID;

                StartCoroutine(Main.Instance.Web.SellItem(iId, uId));
            });
        }

        yield return null;
    }
}
