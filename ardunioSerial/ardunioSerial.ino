const byte numChars = 32;
char receivedChars[numChars]; // an array to store the received data
const int led = 2;
boolean newData = false;

void setup() {  
  Serial.begin(9600);
  while(!Serial){}
  pinMode(led, OUTPUT);
  Serial.println("Ardunio Serial is on");
}

void loop() {
  recvWithEndMarker();
   if (newData == true) {
     Serial.print("Recived serial message: ");
     Serial.println(receivedChars);
     if (strcmp(receivedChars,"on") == 0 ){
      Serial.println("Turning on led");
      digitalWrite(led, LOW);
     }else if (strcmp(receivedChars,"off") == 0 ){
      Serial.println("Turning off led");
      digitalWrite(led, HIGH);
     } else {
      Serial.println("Unkown Keyword");
     }
     
     newData = false;
   }
}

String getNextSerialMessage() {
  String readString;
  String Q;
  while(!Serial.available()){}
  while (Serial.available()) {
    delay(1);  //delay to allow buffer to fill 
    if (Serial.available() >0) {
      char c = Serial.read();  //gets one byte from serial buffer
      if (isControl(c)) {
        //'Serial.println("it's a control character");
        break;
      }
      readString += c; //makes the string readString    
     }
  }
  return readString;
}
void recvWithEndMarker() {
 static byte ndx = 0;
 char endMarker = '>';
 char rc;
 
 // if (Serial.available() > 0) {
 while (Serial.available() > 0 && newData == false) {
   rc = Serial.read();
  
   if (rc != endMarker) {
   receivedChars[ndx] = rc;
   ndx++;
   if (ndx >= numChars) {
   ndx = numChars - 1;
   }
   }
   else {
   receivedChars[ndx] = '\0'; // terminate the string
   ndx = 0;
   newData = true;
   }
 }
}
