using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestoy : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
