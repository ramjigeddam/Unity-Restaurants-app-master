using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsHandler : MonoBehaviour
{
	#region Private variables
	private bool isShow = false, // To control the Show/Hide of Ingredients
		isShowCategory = true; // To control the Show/Hide of Category
	[SerializeField]
	private RectTransform ingredientsPanel, menuPanel, categoryMenu;
	#endregion

	// Start is called before the first frame update
	private void Start()
	{
		isShow = false;
		iTween.MoveTo(menuPanel.gameObject, iTween.Hash("position", new Vector3(0, -600, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
		iTween.MoveTo(ingredientsPanel.gameObject, iTween.Hash("position", new Vector3(300, 0, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			HideMenuButton();
		}
	}

	public void IngredientsButton()
	{
		isShow = !isShow;
		if (isShow)
		{
			iTween.MoveTo(ingredientsPanel.gameObject, iTween.Hash("position", new Vector3(4, 0, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
		}
		else
		{
			iTween.MoveTo(ingredientsPanel.gameObject, iTween.Hash("position", new Vector3(300, 0, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
		}
	}

	/* Main menu panel
	 * Move menu panel -600 to 0 position
	 */

	public void ShowMenuButton()
	{
		iTween.MoveTo(menuPanel.gameObject, iTween.Hash("position", new Vector3(0, 0, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
	}

	/* Main menu panel
	 * Move menu panel 0 to-600 position
	 */
	public void HideMenuButton()
	{
		iTween.MoveTo(menuPanel.gameObject, iTween.Hash("position", new Vector3(0, -600, 0), "islocal", true, "time", 1.0f, "easyType", iTween.EaseType.linear));
	}

	/* Category menu 
	 * Show and hide category menu with bool value
	 */
	public void ShowCategoryMenu()
	{
		isShowCategory = !isShowCategory;
		if (isShowCategory)
		{
			categoryMenu.gameObject.SetActive(true);
		}
		else
		{
			categoryMenu.gameObject.SetActive(false);
		}
	}
}
