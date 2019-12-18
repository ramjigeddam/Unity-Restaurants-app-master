using System;
using System.Collections;
using System.IO;
using GLTF;
using GLTF.Schema;
using UnityEngine;
using UnityGLTF.Loader;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace UnityGLTF
{

	/// <summary>
	/// Component to load a GLTF scene with
	/// </summary>
	public class GLTFComponent : MonoBehaviour
	{

		public string GLTFUri = null;
		public bool Multithreaded = true;

		[SerializeField]
		private bool loadOnStart = true;

		public IEnumerable<Animation> Animations { get; set; }

		public int MaximumLod = 300;
		public int Timeout = 8;
		public GLTFSceneImporter.ColliderType Collider = GLTFSceneImporter.ColliderType.None;

		[SerializeField]
		private Shader shaderOverride = null;

		#region RayCasst Private varabiles
		/*
		 * Ray cast
		 * Hit Objectname
		 */

		private Ray rayCastingMeshFiles;
		private RaycastHit raycastHitObject;

		#endregion

		private void Start()
		{
			//StartCoroutine("Load");
		}

		public void LoadGltfModel()
		{
			StartCoroutine("Load");

		}
		Animation anim;

		public IEnumerator Load()
		{

			GLTFSceneImporter sceneImporter = null;
			ILoader loader = null;
			try
			{

				string directoryPath = URIHelper.GetDirectoryName(GLTFUri);
				loader = new WebRequestLoader(directoryPath);
				sceneImporter = new GLTFSceneImporter(
					URIHelper.GetFileFromUri(new Uri(GLTFUri)),
					loader
					);

				sceneImporter.SceneParent = gameObject.transform;
				sceneImporter.Collider = Collider;
				sceneImporter.MaximumLod = MaximumLod;
				sceneImporter.Timeout = Timeout;
				sceneImporter.isMultithreaded = Multithreaded;
				sceneImporter.CustomShaderName = shaderOverride ? shaderOverride.name : null;
				yield return sceneImporter.LoadScene(-1);

				// Override the shaders on all materials if a shader is provided
				if (shaderOverride != null)
				{
					Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
					foreach (Renderer renderer in renderers)
					{
						renderer.sharedMaterial.shader = shaderOverride;
					}
				}

				if (gameObject.GetComponentInChildren<Animation>() != null)
					anim = gameObject.GetComponentInChildren<Animation>();
				if (anim != null)
				{
					anim.Play();
				}
			}
			finally
			{
				if (loader != null)
				{
					sceneImporter.Dispose();
					sceneImporter = null;
					loader = null;
				}
			}
		}
	}
}
