using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimulatorUI : MonoBehaviour
{
	public InputField ipField;
	public MovingPlatform mp;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void openMenu()
	{
		SceneManager.LoadScene("Menu");
	}
	
	public void changeIp()
	{
		mp.changeIp(ipField.text);
	}
}
