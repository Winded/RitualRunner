using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif




//============================//////////////
//||         TETRIS!        ||//////////////
//============================//////////////
//||        ;;;;            ||//-=======-///
//||      ;;;;;;            ||//-|SCORE|-///
//||          ;;;;          ||//-|99879|-///
//||          ;;;;          ||//-=======-///
//||          ;;;;          ||//////////////
//||            ;;          ||//-========-//
//||       GAME OVER!       ||//-  NEXT  -//
//||        ;;;;;;          ||//-========-//
//||            ;;;;        ||//-  ;;    -// 
//||          ;;;;;;        ||//-  ;;;;  -//
//||        ;;;;            ||//-    ;;  -//
//||        ;;              ||//-        -//
//||        ;;;;            ||//-========-//
//||      ;;;;;;            ||//////////////
//||    ;;;;        ;;      ||//////////////
//||  ::  %%%%      ;;;;    ||//////////////
//||::::&&%%%%::######::    ||//////////////
//============================//////////////




public class SceneAsset : ScriptableObject 
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/SceneAsset")]
	static void CreateSceneAsset()
	{
		SceneAsset asset = ScriptableObject.CreateInstance<SceneAsset> ();

		AssetDatabase.CreateAsset (asset, "Assets/MyScene.asset");
	}
	#endif

	public enum SceneObjectType
	{
		Block,
		Obstacle,
		Hole,
		Enemy,
		Custom,
	}

	public enum SceneObjectPropertyType
	{
		None,
		Scale,
		Offset,
	}

	[System.Serializable]
	public struct SceneObjectProperty
	{
		public SceneObjectPropertyType type;
		public Vector2 data;
	}

	[System.Serializable]
	public class DataObject
	{
		public int time;
		public int range;

		public SceneObjectType type;
		public int customID;

		public SceneObjectProperty[] properties;
	}

	[System.Serializable]
	public struct SceneObject
	{ 
		public GameObject prefab;
		public SceneObjectType type;
		
		public int customID;
	}

	public GameObject ground;

	public float magicNumber = 1;
	public AudioClip clip;

	public SceneObject[] sceneObjects;
	public DataObject[] dataObjects;
}