#include <SoftwareSerial.h>
#include <WiFi.h>

SoftwareSerial ESP8266(10, 12); // 10 TX 12 RX

String NomduReseauWifi = "TP-LINK_BACA"; // Garder les guillements
String MotDePasse      = "EasyGlove"; // Garder les guillements

int vibThumb = 3;
int vibIndex = 5;
int vibMiddle = 9;
int vibRing = 11;
int vibPinky = 6;

int fingers[] = {vibThumb,vibIndex,vibMiddle,vibRing,vibPinky};

int handId = 2;

String getString = ":GET /";
String httpString = " HTTP/";


bool findGet = false, findHttp=false;
String reponse = "";
int sensorValue = 0;

/****************************************************************/
/*                             INIT                             */
/****************************************************************/
void setup()
{
  Serial.begin(9600);
  
  ESP8266.begin(115200);
  envoieAuESP8266("AT+CIOBAUD=9600");
  recoitDuESP8266(4000);

  for(int i=0;i<5;i++)
  {
    pinMode(fingers[i], OUTPUT);
  }
  
  ESP8266.begin(9600);  
  initESP8266();
}
/****************************************************************/
/*                        BOUCLE INFINIE                        */
/****************************************************************/
void loop()
{
   while(ESP8266.available())
   {   
    recoitDuESP8266Short(); 
   }   
}
/****************************************************************/
/*                Fonction qui initialise l'ESP8266             */
/****************************************************************/
void initESP8266()
{  
  Serial.println("**********************************************************");  
  Serial.println("**************** DEBUT DE L'INITIALISATION ***************");
  Serial.println("**********************************************************");  
  envoieAuESP8266("AT");
  recoitDuESP8266(2000);
  Serial.println("**********************************************************");
  envoieAuESP8266("AT+CWMODE=3");
  recoitDuESP8266(5000);
  Serial.println("**********************************************************");
  envoieAuESP8266("AT+CWJAP=\""+ NomduReseauWifi + "\",\"" + MotDePasse +"\"");
  recoitDuESP8266(10000);
  Serial.println("**********************************************************");
  envoieAuESP8266("AT+CIFSR");
  recoitDuESP8266(1000);
  Serial.println("**********************************************************");
  envoieAuESP8266("AT+CIPMUX=1");   
  recoitDuESP8266(1000);
  Serial.println("**********************************************************");
  envoieAuESP8266("AT+CIPSERVER=1,80");
  recoitDuESP8266(1000);
  Serial.println("**********************************************************");
  Serial.println("***************** INITIALISATION TERMINEE ****************");
  Serial.println("**********************************************************");
  Serial.println("");  
}

/****************************************************************/
/*        Fonction qui envoie une commande à l'ESP8266          */
/****************************************************************/
void envoieAuESP8266(String commande)
{  
  ESP8266.println(commande);
}
/****************************************************************/
/*Fonction qui lit et affiche les messages envoyés par l'ESP8266*/
/****************************************************************/
void recoitDuESP8266(const int timeout)
{
  String reponse = "";
  long int time = millis();
  while( (time+timeout) > millis())
  {
    while(ESP8266.available())
    {
      char c = ESP8266.read();
      reponse+=c;
    }
  }

  int index1 = reponse.indexOf(":GET");
      
  if(index1 != -1)
  {
    String valueStr = reponse.substring(index1 + 6, reponse.indexOf("HTTP/") - 1);
    //Serial.print(valueStr); 
    
    int fingerIndex = valueStr.substring(0, valueStr.indexOf("?")).toInt();
    String rest = valueStr.substring(valueStr.indexOf("?") + 1);
    int vibIntensity = rest.substring(0, rest.indexOf("?")).toInt();
    rest = rest.substring(rest.indexOf("?") + 1);
    int timeVib = rest.toInt();
    
    analogWrite(fingerIndex, vibIntensity); 
    delay(timeVib);
    analogWrite(fingerIndex, 0);
  }
  
  //Serial.print("Leaving from recoitDuESP8266\n"); 
  Serial.print(reponse);   
}

void recoitDuESP8266Short()
{
    while(ESP8266.available())
    {
      char c = ESP8266.read();
      reponse+=c;
    }
    if(!findGet)
    {
      findGet = reponse.indexOf(getString)!=-1;
    }
    if(!findHttp)
    {
      findHttp = reponse.indexOf(httpString)!=-1;
    }
    if(findGet && findHttp)
    {
      
      String value = reponse.substring(reponse.indexOf(getString)+getString.length(), reponse.indexOf(httpString));
      //Serial.print("'"+value+"'");
      
      /*+IPD,0,76:GET /3?150?250?0?0 HTTP/1.1
      Connection: keep-alive
      Host: 192.168.0.101*/
      for(int i=0;i<5;i++)
      {
            int index = value.indexOf("?");
            int val = 0;
            if(index!=-1)
            {
              val = value.substring(0, index).toInt();
              value = value.substring(index+1);
            }
            else
            {
              val=value.toInt();
            }
           Serial.print("finger");
           Serial.print(i);
           Serial.print("=");
           Serial.println(val);
           analogWrite(fingers[i], val);
      }
      reponse = "";
      findGet=false;
      findHttp=false;
    }
  
  
}
  

