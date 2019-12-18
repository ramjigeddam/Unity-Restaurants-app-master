using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DownloadHandler : MonoBehaviour
{
	[SerializeField]
	private TextAsset jsonText;
	private JsonData jsonData;

	private string modelThumbnail, modelUrl;
	private Texture2D downloadedTexture;
	[SerializeField]
	private Image downloadedImage, loadingBar, downloadingCompleted;
	[SerializeField]
	private Text loadingText, downLoadingText;

	[SerializeField]
	private float LValue, PValue;

	void Start()
	{
		downLoadingText.text = "Loading Menu Please wait";
		loadingBar.gameObject.SetActive(false);
		loadingText.gameObject.SetActive(false);
		downloadingCompleted.gameObject.SetActive(false);

		jsonData = JsonMapper.ToObject(jsonText.text);


		for (int i = 0; i < jsonData["models"].Count; i++)
		{
			modelThumbnail = jsonData["models"][i]["modelThumbnail"].ToString();
			modelUrl = jsonData["models"][i]["modelsUrl"].ToString();
			StartCoroutine("DownloadData");

		}
	}

	IEnumerator DownloadData()
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(modelThumbnail);
		yield return www.SendWebRequest();
		downloadedTexture = DownloadHandlerTexture.GetContent(www);
		downloadedImage.sprite = Sprite.Create(downloadedTexture, new Rect(Vector2.zero, new Vector2(downloadedTexture.width, downloadedTexture.height)), new Vector2(0.5f, 0.5f), 100.0f);
		downloadedImage.GetComponent<RectTransform>().sizeDelta = new Vector2(downloadedTexture.width, downloadedTexture.height);
		downloadedImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(88, -180, 0);
		downloadedImage.GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);

		if (www.isDone)
		{
			downLoadingText.text = "";
			Button btn = downloadedImage.GetComponent<Button>();
			btn.onClick.AddListener(DownLoadModel);
		}
	}


	void DownLoadModel()
	{
		loadingBar.gameObject.SetActive(true);
		loadingText.gameObject.SetActive(true);
		loadingBar.GetComponent<RectTransform>().localScale = new Vector3(6.0f, 6.0f, 6.0f);
		GameObject.FindObjectOfType<UnityGLTF.GLTFComponent>().GLTFUri = modelUrl;
		GameObject.FindObjectOfType<UnityGLTF.GLTFComponent>().LoadGltfModel();
	}

	public void ModelDownloadingCompleted()
	{
		loadingBar.gameObject.SetActive(false);
		loadingText.gameObject.SetActive(false);
		downloadingCompleted.gameObject.SetActive(true);
		downloadingCompleted.GetComponent<RectTransform>().anchoredPosition = new Vector3(250, -500, 0);
		downloadingCompleted.GetComponent<RectTransform>().localScale = Vector3.one;
		Invoke("LoadARScene", 1.5f);
	}

	void LoadARScene()
	{
		SceneManager.LoadScene("AR");
	}
}
