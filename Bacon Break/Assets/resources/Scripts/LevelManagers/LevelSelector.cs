using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/*to make this work, in the button which you want to load the level, set the level as a prefab and put it in myLevel through the inspector
  make sure the level has a unique name otherwise this will probably glitch. the best way would be as follows: Level*thenumberofthelevel*. E.G: Level3 */
public class LevelSelector : MonoBehaviour {
    public GameObject myLevel;
    public string myScene;
	
    // Use this for initialization
    public void SelectLevel()
    {
        PlayerPrefs.SetString("level", myLevel.name);
        SceneManager.LoadScene(myScene);
    }

    public void SetLevelObject(GameObject level)
    {
        myLevel = level;
    }
}
