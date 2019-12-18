using UnityEngine;
using UnityEngine.UI;

public class MenuCategoryHandler : MonoBehaviour
{
	#region Private variables
	public bool isOnce = true;
	public GameObject categoryMenuList;
	public GameObject[] categoryListObj;
	public Text categoryName;
	#endregion

	#region Private variables
	[SerializeField]
	private string[] categoryName_String;
	#endregion

	// Start is called before the first frame update
	private void Start()
	{
		Category(0);
	}

	public void Category(int value)
	{
		for (int i = 0; i < categoryListObj.Length; i++)
		{
			categoryListObj[i].SetActive(false);
		}
		categoryListObj[value].SetActive(true);
		categoryName.text = categoryName_String[value];
	}
}
