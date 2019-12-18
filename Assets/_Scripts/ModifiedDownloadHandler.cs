using UnityEngine;
using LitJson;

public class ModifiedDownloadHandler : MonoBehaviour
{
	#region Private variables
	[SerializeField]
	private TextAsset jsonText;
	[SerializeField]
	private bool isOnce = false;

	private JsonData jsonData;
	private string modelThumbnail, modelUrl;
	#endregion

	#region Public variables
	public GameObject completedDownloadImage;
	#endregion


	// Start is called before the first frame update
	void Start()
	{
		isOnce = false;
		completedDownloadImage.SetActive(false);
		jsonData = JsonMapper.ToObject(jsonText.text);

		for (int i = 0; i < jsonData["models"].Count; i++)
		{
			modelThumbnail = jsonData["models"][i]["modelThumbnail"].ToString();
			modelUrl = jsonData["models"][i]["modelsUrl"].ToString();
		}
	}

	public void DownLoadModel()
	{
		if (!isOnce)
		{
			GameObject.FindObjectOfType<UnityGLTF.GLTFComponent>().GLTFUri = modelUrl;
			GameObject.FindObjectOfType<UnityGLTF.GLTFComponent>().LoadGltfModel();
			isOnce = true;
		}
	}
}
