using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using JsonData;
using TMPro;

public class DialogflowAPIScript : MonoBehaviour {

	public TextMeshPro dialogTextbox;
	public string dialogflowURL;
	private string googleTokenUrl = "http://ec2-3-16-154-51.us-east-2.compute.amazonaws.com/access-token";
	private string googleToken;

	// Use this for initialization
	void Start () {
		// TODO: Make access-token handling and dialogflow communication done on a server so we don't need to have keys here.
		StartCoroutine(RefreshAccessToken());
	}

	IEnumerator RefreshAccessToken() {

		using (UnityWebRequest webRequest = UnityWebRequest.Get(googleTokenUrl))
		{
			yield return webRequest.SendWebRequest();
			if (webRequest.isNetworkError)
			{
				UnityEngine.Debug.LogError("Web Request Error: " + webRequest.error);
			}
			else
			{
				googleToken = webRequest.downloadHandler.text.Trim();
				UnityEngine.Debug.Log("Got Key! " + googleToken);
			}
		}

		// // gcloud requires credentials to be set in file at the path set by env_var GOOGLE_APPLICATION_CREDENTIALS, so we do that first, run gcloud auth to get our access token, and write it to a local file to be read later.
		// // TODO: Find a way to get the output directly into unity rather than in a file.
		// Process.Start("cmd.exe", "/C " + "set GOOGLE_APPLICATION_CREDENTIALS=" + gacCredsPath + " && " + gcloudPath + " auth application-default print-access-token > " + keyPath).WaitForExit();
		// LoadAccessToken();
		yield return null;
	}

	// void LoadAccessToken() {
	// 	// Gets the key we wrote into the file and loads it into Unity.
	// 	googleToken = File.ReadAllText(Path.Combine(Application.dataPath, "StreamingAssets/CloudSDK/key.txt"));
	// 	// Need to get rid of newline.
	// 	googleToken = googleToken.Substring(0, googleToken.Length - 2);
	// }

	IEnumerator PostRequest(String url, String AccessToken, String message){
		UnityWebRequest postRequest = new UnityWebRequest(url, "POST");
		RequestBody requestBody = new RequestBody();
		requestBody.queryInput = new QueryInput();
		requestBody.queryInput.text = new TextInput();
		requestBody.queryInput.text.text = message;
		requestBody.queryInput.text.languageCode = "en";

		string jsonRequestBody = JsonUtility.ToJson(requestBody,true);
		UnityEngine.Debug.Log(jsonRequestBody);

		byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
		//UnityEngine.Debug.Log(bodyRaw);
		postRequest.SetRequestHeader("Authorization", "Bearer " + AccessToken);
		postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		//postRequest.SetRequestHeader("Content-Type", "application/json");

		yield return postRequest.SendWebRequest();

		if(postRequest.isNetworkError || postRequest.isHttpError)
		{
			UnityEngine.Debug.Log(postRequest.responseCode);
			UnityEngine.Debug.Log(postRequest.error);
		}
		else
		{
			// Show results as text
			UnityEngine.Debug.Log("Response: " + postRequest.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] resultbyte = postRequest.downloadHandler.data;
			string result = System.Text.Encoding.UTF8.GetString(resultbyte);
			ProcessResult(result);
			ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
			string finalText = content.queryResult.fulfillmentText;
			ResolveText(finalText);
		}
	}

	public virtual void SendMessage(String message) {
			//https://stackoverflow.com/questions/51272889/unable-to-send-post-request-to-dialogflow-404
			//first param is the dialogflow API call, second param is Json web token
			int openParenIndex = message.IndexOf("(", 0);
			if (openParenIndex > 0) {
				message = message.Substring(0, openParenIndex);
			}

			UserInputDisplay uid = FindObjectOfType<UserInputDisplay>();
			uid.SetText(message);

			StartCoroutine(PostRequest(dialogflowURL,
				googleToken,
				message));
	}

	public virtual void ResolveText(String text) {
			dialogTextbox.text = text;
			SynthesizeSpeech(text);
	}

	public virtual void ProcessResult(string result) {

	}

	public virtual void SynthesizeSpeech(String text) {
			ExampleTextToSpeech tts = GameObject.FindWithTag("TTS").GetComponent<ExampleTextToSpeech>();
			StartCoroutine(tts.Convert(text));
	}

	IEnumerator GetAgent(String AccessToken)
	{

		UnityWebRequest www = UnityWebRequest.Get("https://dialogflow.googleapis.com/v2/projects/sandwich-shop-558e7/agent");

		www.SetRequestHeader("Authorization", "Bearer " + AccessToken);

		yield return www.SendWebRequest();
		//myHttpWebRequest.PreAuthenticate = true;
		//myHttpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);
		//myHttpWebRequest.Accept = "application/json";

		if (www.isNetworkError || www.isHttpError)
		{
			UnityEngine.Debug.Log(www.error);
		}
		else
		{
			// Show results as text
			UnityEngine.Debug.Log(www.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
		}
	}
}
