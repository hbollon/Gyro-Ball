using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private GameObject[] pauseObjects;
	private GameObject[] finishObjects;
	private GameObject[] ballsController;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");			//gets all objects with tag ShowOnPause
		finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");			//gets all objects with tag ShowOnFinish

		HidePaused();
		HideFinished();

		//Checks to make sure MainLevel is the loaded level
		if(GameObject.FindWithTag("Player"))
			ballsController = GameObject.FindGameObjectsWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		//uses the p button to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				HidePaused();
			}
		}
	}


	//Reloads the Level
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//controls the pausing of the scene
	public void PauseControl(){
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				HidePaused();
			}
	}

	//shows objects with ShowOnPause tag
	public void ShowPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void HidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	//shows objects with ShowOnFinish tag
	public void ShowFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnFinish tag
	public void HideFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level){
		SceneManager.LoadScene(level);
	}
}