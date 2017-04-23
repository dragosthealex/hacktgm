using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;


public class camera : MonoBehaviour {

    WebCamTexture webCamTex;

    byte[] image;

    // Use this for initialization
    void Start () {
        webCamTex = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTex;
        webCamTex.Play();
        StartCoroutine(CaptureTextureAsPNG());
    }

    IEnumerator CaptureTextureAsPNG()
    {
        while (true) //captures images
        {
            print("image captured");
            Texture2D TextureFromCamera = new Texture2D(GetComponent<Renderer>().material.mainTexture.width,
            GetComponent<Renderer>().material.mainTexture.height);
            TextureFromCamera.SetPixels((GetComponent<Renderer>().material.mainTexture as WebCamTexture).GetPixels());
            TextureFromCamera.Apply();
            image = TextureFromCamera.EncodeToPNG(); //save image 

            StartCoroutine(GetEmotion()); //call to api

            yield return new WaitForSeconds(3f); //every 3 seconds
        }
    }

    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        return binaryReader.ReadBytes((int)fileStream.Length);
    }

    IEnumerator GetEmotion()
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string, string>(form.headers);
        headers.Add("Ocp-Apim-Subscription-Key", "d09fe9c732914d5da2aa4dcb5835cdba");
        headers["Content-Type"] = "application/octet-stream";
        string url = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";

        WWW www = new WWW(url, image, headers);

        yield return www;

        Debug.Log(www.text); //microsoft emotion answer

        ParseResponse(www.text);
    }

    void ParseResponse(string response)
    {
        double value = GetJsonValue(response, "happiness");
        Debug.Log("parsed value:" + value);
    }

    //gets value for the first face for the given attribute
    double GetJsonValue(string json, string key)
    {
        if (json == "[]" || json.Contains("error"))
        {
            return 0;
        }
        string[] values = json.Split(new string[] { key + "\":" }, StringSplitOptions.None);
        return Double.Parse(values[1].Split(',')[0]);

    }
}
