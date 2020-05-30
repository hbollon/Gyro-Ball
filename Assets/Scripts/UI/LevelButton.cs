using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerDownHandler {
    public int levelIndex;

    private void Start() {
        if(LevelManager.Instance.LevelIsUnlocked(levelIndex)){
            gameObject.GetComponentInChildren<Text>().text = LevelManager.Instance.GetLevel(levelIndex).levelNumber.ToString();
        } else {
            gameObject.GetComponentInChildren<Text>().gameObject.SetActive(false);
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/button_locked_hover");;
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        if(LevelManager.Instance.LevelIsUnlocked(levelIndex))
            LevelManager.Instance.LoadLevel(levelIndex);
    }  
}