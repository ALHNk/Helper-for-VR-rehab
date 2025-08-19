using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.SceneManagement;

public class LegController : MonoBehaviour
{
	public float rotationZ;
	
	private bool isStarted;
	private string ip;
	
	private UdpClient udpClient;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    ip = PlayerPrefs.GetString("ActiveIp");
	    udpClient = new UdpClient();
    }
	public void changeIp(string newIp)
	{
		ip = newIp;
	}

    // Update is called once per frame
    void Update()
    {
	    rotationZ = Input.acceleration.z;
	    if(isStarted)
	    {
	    	sendValues(rotationZ);
	    }
    }
    
	public void sendValues(float rotation)
	{
		string msg = rotation.ToString("F2");
		byte[] data = Encoding.ASCII.GetBytes(msg);
		udpClient.Send(data, data.Length, ip, 9050);
	}
    
	public void setStarted()
	{
		isStarted = true;
	}
	
	public void exit()
	{
		SceneManager.LoadScene("Menu");
	}
}
