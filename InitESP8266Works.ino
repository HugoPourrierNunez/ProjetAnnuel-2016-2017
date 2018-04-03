    #include <SoftwareSerial.h>
    #include <WiFi.h>

    SoftwareSerial ESP8266(10, 12); // 10 TX 12 RX

    String NomduReseauWifi = ""; // Garder les guillements
    String MotDePasse      = ""; // Garder les guillements
    
    int vibThumb = 3;
    int vibIndex = 5;
    int vibMiddle = 6;
    int vibRing = 9;
    int vibPinky = 11;

    int handId = 2;


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
      
      pinMode(vibThumb, OUTPUT);
      pinMode(vibIndex, OUTPUT);
      pinMode(vibMiddle, OUTPUT);
      pinMode(vibRing, OUTPUT);
      pinMode(vibPinky, OUTPUT);
      
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
          recoitDuESP8266(1000); 
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
        Serial.print(valueStr); 

        int handIndex = valueStr.substring(0, valueStr.indexOf("?")).toInt();
        Serial.print("\nHand Index : ");
        Serial.print(handIndex);
        if(handIndex == handId)
        {
          String rest = valueStr.substring(valueStr.indexOf("?") + 1);
          int fingerIndex = rest.substring(0, rest.indexOf("?")).toInt();
          rest = rest.substring(rest.indexOf("?") + 1);
          int vibIntensity = rest.substring(0, rest.indexOf("?")).toInt();
          rest = rest.substring(rest.indexOf("?") + 1);
          int timeVib = rest.toInt();
          
          Serial.print("\nFinger Index : ");
          Serial.print(fingerIndex);
          Serial.print("\nVib Intensity : ");
          Serial.print(vibIntensity);
          Serial.print("\nTime vib : ");
          Serial.print(timeVib);

          switch(fingerIndex)
          {
            case 1:
              Serial.print("\nVIB THUMB\n");
              analogWrite(vibThumb, vibIntensity); 
              delay(timeVib);
              analogWrite(vibThumb, 0);
              break;
            case 2:
              analogWrite(vibIndex, vibIntensity);
              delay(timeVib);
              Serial.print("\nVIB INDEX\n");
              analogWrite(vibIndex, 0); 
              break;
            case 3:
            Serial.print("\nVIB MIDDLE\n");
              analogWrite(vibMiddle, vibIntensity); 
              delay(timeVib);
              analogWrite(vibMiddle, 0);
              break;
            case 4:
              Serial.print("\nVIB RING\n");
              analogWrite(vibRing, vibIntensity); 
              delay(timeVib);
              analogWrite(vibRing, 0);
              break;
            case 5:
              Serial.print("\nVIB PINKY\n");
              analogWrite(vibPinky, vibIntensity); 
              delay(timeVib);
              analogWrite(vibPinky, 0);
              break;
          }
        }
      }
      
      Serial.print("Leaving from recoitDuESP8266\n"); 
      Serial.print(reponse);   
    }
