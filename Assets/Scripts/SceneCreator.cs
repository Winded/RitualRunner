using UnityEngine;
using System.Collections;

public class SceneCreator : MonoBehaviour 
{

#if UNITY_EDITOR
	[UnityEditor.Callbacks.PostProcessScene]
	private static void EditorLoadScene()
	{
		SceneCreator sceneCreator = GameObject.FindObjectOfType<SceneCreator> ();

		if (sceneCreator) 
		{
			sceneCreator.LoadScene();
		}
	}
#endif

	public int intro = 10;

	public float randomOffet = 0.03f;

	public int scale = 1;

	public int length = 60;
	public SceneAsset asset;

#if UNITY_EDITOR
	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.RightControl)) 
		{
			AudioSource source = GameObject.FindObjectOfType<AudioSource>();

			if(source)
			{
				Debug.Log(source.time + " : " + source.time * asset.magicNumber);
			}
		}
	}
#endif
		
	private void LoadScene()
	{
		Vector2 position = transform.position;
		int time = -intro;

		while (time < (asset != null ? (asset.clip != null ? asset.clip.length * asset.magicNumber : length) : length) ) 
		{
			time++;
			
			GameObject prefab = null;

			SceneAsset.DataObject obj = null;

			for (int i = 0; i < asset.dataObjects.Length; i++) 
			{
				obj = asset.dataObjects[i];

				if( time >= obj.time * asset.magicNumber && time <= obj.time * asset.magicNumber + obj.range)
				{
					prefab = FindObject(obj.type, obj.customID);
					break;
				}
				else
				{
					prefab = FindObject(SceneAsset.SceneObjectType.Block, obj.customID);
				}
			}
			
			if(prefab != null)
			{
				for(int i = 0; i < scale; i++)
				{ 
					//Debug.Log(prefab.name + " " + scale + ": " + time);
					GameObject gobj = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;

					gobj.transform.parent = transform;

					SpriteRenderer sprite = gobj.GetComponent<SpriteRenderer>();

					float offset = gobj.transform.localScale.x;

					if(sprite != null)
					{
						offset = sprite.bounds.size.x;
					}

					if(obj != null)
					{
						for(int j = 0; j < obj.properties.Length; j++)
						{
							if(obj.properties[j].type == SceneAsset.SceneObjectPropertyType.Scale)
							{			
								gobj.transform.localScale += new Vector3(obj.properties[j].data.x, obj.properties[j].data.y);

								float oldOffset = offset;
								offset = gobj.transform.localScale.x;
								
								if(sprite != null)
								{
									offset = sprite.bounds.size.x;
								}

								gobj.transform.Translate(oldOffset,0,0);
							}
							else if(obj.properties[j].type == SceneAsset.SceneObjectPropertyType.Offset)
							{
								gobj.transform.Translate(obj.properties[j].data.x, obj.properties[j].data.y, 0);
							}
						}
					}

					position.x += offset;

					if(Random.Range(0,100) > 50)
					{
						position.y += Random.Range(-randomOffet,randomOffet);
					}
				}

				for(int i = 0; i < obj.properties.Length; i++)
				{
					if(obj.properties[i].type == SceneAsset.SceneObjectPropertyType.Offset) 
					{
						position += obj.properties[i].data;
					}
				}
			}
		}
	}

	private GameObject FindObject(SceneAsset.SceneObjectType type, int customID)
	{
		for(int i = 0; i < asset.sceneObjects.Length; i++)
		{
			if(asset.sceneObjects[i].type == type || (type == SceneAsset.SceneObjectType.Custom && asset.sceneObjects[i].customID == customID))
			{
				return asset.sceneObjects[i].prefab;
			}
		}

		return null;
	}
}