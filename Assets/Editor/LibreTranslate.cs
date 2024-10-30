using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LibreTranslate 
{
    private const string TranslateUrl = "https://api.mymemory.translated.net/get";

    public async Task<string> TranslateText(string text, string sourceLanguage, string targetLanguage)
    {
        string url = $"{TranslateUrl}?q={UnityWebRequest.EscapeURL(text)}&langpair={sourceLanguage}|{targetLanguage}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield(); 
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = request.downloadHandler.text;
                MyMemoryResponse response = JsonUtility.FromJson<MyMemoryResponse>(jsonResult);
                return response.responseData.translatedText;
            }
            else
            {
                Debug.LogError($"Error: {request.error}"); 
                Debug.LogError($"Response Code: {request.responseCode}"); 
                Debug.LogError($"Response Body: {request.downloadHandler.text}"); 
                return null;
            }
        }
    }
}
[System.Serializable]
public class MyMemoryResponse
{
    public ResponseData responseData;
}

[System.Serializable]
public class ResponseData
{
    public string translatedText;
}
