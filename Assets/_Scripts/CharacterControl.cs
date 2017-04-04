//using UnityEngine;
//using System.Collections;

// MQTT
using System.Net;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

public class CharacterControl : UnityEngine.MonoBehaviour
{
    [UnityEngine.SerializeField]    
    private string serverIP = "203.66.68.65";

    // MQTT
    private MqttClient client;

    //
    public float characterWalkSpeed;
    public float characterPositionX;

    private float movePositionX = (float)0.0;
    private bool moveScaleX = true;

    // Use this for initialization
    void Start ()
    {
        // MQTT
        client = new MqttClient(IPAddress.Parse(serverIP), 1883, false, null);
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        string clientId = System.Guid.NewGuid().ToString();
        client.Connect(clientId);
        client.Subscribe(new string[] { "taipei/unity3d" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        
        //
        this.transform.position = new UnityEngine.Vector3(characterPositionX, (float)0.0, (float)0.0);
	}
	
	// Update is called once per frame
	void Update ()
    {

        UnityEngine.Vector3 moveXData = new UnityEngine.Vector3(movePositionX,(float)0.0,(float)0.0);
        this.transform.position = moveXData;
        this.transform.localScale = new UnityEngine.Vector3((float)((moveScaleX == true)?(1.0):(-1.0)), (float)1.0, (float)1.0);

        /*moveHorizontal = UnityEngine.Input.GetAxis("Horizontal");
        {

            if (moveHorizontal != 0)
            {
                //UnityEngine.Debug.Log(moveHorizontal);
                UnityEngine.Vector3 movement = new UnityEngine.Vector3(moveHorizontal * this.characterWalkSpeed * UnityEngine.Mathf.Sign(moveHorizontal) * UnityEngine.Time.deltaTime, (float)0.0, (float)0.0);
                this.transform.Translate(movement);
                //this.transform.position = new UnityEngine.Vector3(moveHorizontal * this.characterWalkSpeed * UnityEngine.Mathf.Sign(moveHorizontal) * UnityEngine.Time.deltaTime, (float)0.0, (float)0.0);

                this.transform.localScale = new UnityEngine.Vector3(UnityEngine.Mathf.Sign(moveHorizontal), (float)1.0, (float)1.0);
                GetComponent<CharacterAnimator>().boolWalk = true;
            }
            else
            {
                GetComponent<CharacterAnimator>().boolWalk = false;
            }
        }*/
    }

    //MQTT
    string msg = "";
    public static int Num = -1;
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        try
        {
            msg = System.Text.Encoding.UTF8.GetString(e.Message).Trim();

            if (msg.Equals("Right"))
            {
                moveScaleX = true;
                movePositionX = movePositionX + (float)0.5;
            }
            else if (msg.Equals("Left"))
            {
                moveScaleX = false;
                movePositionX = movePositionX - (float)0.5;
            }
        }
        catch
        {

        }
        
        UnityEngine.Debug.Log("Received: " + msg + " Num=" + Num + "movePositionX=" + movePositionX);
    }
}
