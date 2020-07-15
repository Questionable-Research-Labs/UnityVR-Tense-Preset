using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text.RegularExpressions;
namespace UnityVRScripts {

    
    public class ArdCom : MonoBehaviour {
        // Start is called before the first frame update
        public String arduinoPort;
        public bool useArdunio;
        private SerialPort serialPortStream;

        void Start() {
            if (useArdunio)
            {
                serialPortStream = new SerialPort(arduinoPort, 9600);
                serialPortStream.Open();
                sendToArduino("off");
            }
            else {
                Debug.Log("Ard is not enabled");
            }
        }

        public void sendToArduino (String message) {
            
            // // register the event
            // port.DataReceived += Port_DataReceived;
            //open the port
            if (useArdunio) {
                try {
                    // start the communication
                    Debug.Log("Sending message to Ard: "+Regex.Replace(
                        message + ">",
                        @"\p{Cc}",
                        a => string.Format("[{0:X2}]", (byte) a.Value[0])
                    ));
                    serialPortStream.Write(message + ">");
                }
                catch (Exception ex) {
                    Debug.Log("Writing failed! \nError: " + ex.Message);
                }
            }
        }
    }
}
