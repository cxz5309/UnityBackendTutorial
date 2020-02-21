using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate());

        //StartCoroutine(GetUsers());

        //StartCoroutine(Login("testuser", "123456"));

        //StartCoroutine(RegisterUser("testuser3", "1111"));


    }

    IEnumerator GetDate()
    {
        string uri = "http://localhost/UnityBackendTutorial/GetDate.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                byte[] rewults = webRequest.downloadHandler.data;
            }
        }
    }

    IEnumerator GetUsers()
    {
        string uri = "http://localhost/UnityBackendTutorial/GetUsers.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                byte[] rewults = webRequest.downloadHandler.data;
            }
        }
    }


    public IEnumerator Login(string username, string password)
    {
        string uri = "http://localhost/UnityBackendTutorial/Login.php";

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                Main.Instance.UserInfo.SetCredential(username, password);
                Main.Instance.UserInfo.SetID(webRequest.downloadHandler.text);

                if (webRequest.downloadHandler.text.Contains("Wrong Credentials") || webRequest.downloadHandler.text.Contains("Username dose not exist"))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login.gameObject.SetActive(false);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        string uri = "http://localhost/UnityBackendTutorial/RegisterUser.php";
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                Main.Instance.RegisterUserUI.SetActive(false);
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        string uri = "http://localhost/UnityBackendTutorial/GetItemsIDs.php";

        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                string jsonArray = webRequest.downloadHandler.text;

                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        string uri = "http://localhost/UnityBackendTutorial/GetItem.php";

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                string jsonArray = webRequest.downloadHandler.text;

                callback(jsonArray);
            }
        }
    }

    public IEnumerator SellItem(string itemID, string userID)
    {
        string uri = "http://localhost/UnityBackendTutorial/SellItem.php";

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}