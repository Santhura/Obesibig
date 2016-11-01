using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeShopItem
{

    [MenuItem("Assets/Create/Shop Item")]
    public static void Create()
    {
        ShopItem asset = ScriptableObject.CreateInstance<ShopItem>();
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Shop/Shop Items/NewShopItem.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
