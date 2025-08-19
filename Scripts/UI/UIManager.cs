using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class UIManager : MonoBehaviour
{
	public InputField newIpField;
	public GameObject mainPanel, devicesPanel;
	public Text messageAfterSave;
    void Start()
    {
	    mainPanel.SetActive(true);
	    devicesPanel.SetActive(false);
	    messageAfterSave.text = "";
    }
    
	public void openSimulator()
	{
		SceneManager.LoadScene("Simulator");	
	}
	
	public void openDevicesPanel()
	{
		mainPanel.SetActive(false);
		devicesPanel.SetActive(true);
	}
	
	public void openMainPanel()
	{
		mainPanel.SetActive(true);
		devicesPanel.SetActive(false);
	}
	
	public void addNewIp()
	{
		string ip = newIpField.text;
		if(IPAddress.TryParse(ip, out _))
		{
			saveIp(ip);
		}
		else{
			messageAfterSave.text = "Please Enter Valid IP!!!";
		}
	}
	
	private void saveIp(string ip)
	{
		try{
			PlayerPrefs.SetString("ActiveIp", ip );
			messageAfterSave.text = "IP Saved Successfully!";
		}catch
		{
			messageAfterSave.text = "IP could not save";
		}
	}
	
}
