using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class MovingPlatform : MonoBehaviour
{
	public float maxZ, minZ;
	public float speed;
	private Vector3 lastPosition;
	private float lastTime;
	private Vector3 touchStart;
	private string ip;
	public float rotationAmount, deadzone;
	 
	 UdpClient udpClient;
	 
	 void Start()
	 {
		 lastPosition = transform.position;
		 lastTime = Time.time;
		 udpClient = new UdpClient();
		 ip = PlayerPrefs.GetString("ActiveIp");
	 }
	 
	 void Update()
	 {
		 if (Input.touchCount > 0)
		 {
			 Touch t = Input.GetTouch(0);
			 Ray ray = Camera.main.ScreenPointToRay(t.position);
			 RaycastHit hit;
			
			 rotationAmount = Input.acceleration.x;
			 
			 rotationAmount = Mathf.Abs(rotationAmount) > deadzone ? rotationAmount : 0f;
	 
			 if (Physics.Raycast(ray, out hit))
			 {
				 Vector3 newPos = new Vector3(transform.position.x, transform.position.y, hit.point.z);
				 float deltaTime = Time.time - lastTime;
				 speed = Vector3.Distance(lastPosition, newPos) / deltaTime;
	 
				 transform.position = newPos;
				 lastPosition = newPos;
				 lastTime = Time.time;
	 
				 if (transform.localPosition.z > maxZ)
					 transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
				 else if (transform.localPosition.z < minZ)
					 transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
	 
				 SendValues(speed, rotationAmount);
			 }
		 }
		 else
		 {
			 speed = 0;
			 SendValues(speed, rotationAmount);
		 }
	 }
	 
	public void changeIp(string newIp)
	{
		ip = newIp;
	}
	 
	void SendValues(float speed, float rotation)
	 {
		 string msg = speed.ToString("F2") + "," + rotation.ToString("F2");
		 byte[] data = Encoding.ASCII.GetBytes(msg);
		 Debug.Log(msg);
		 Debug.Log("IP is" + ip);
		 udpClient.Send(data, data.Length, ip, 9050); 
	 }
	 
	 void OnApplicationQuit()
	 {
		 udpClient.Close();
	 }
}
