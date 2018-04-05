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

    //int handId = 2;
    
      int temp = 4000;


    int sensorValue = 0;
    /****************************************************************/
    /*                             INIT                             */
    /****************************************************************/
    void setup()
    {
      Serial.begin(9600);
      
      ESP8266.begin(115200);
      envoieAuESP8266("AT+CIOBAUD=9600");
      recoitDuESP8266(temp);
      
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
          /*analogWrite(vibIndex, 0);
          delay(1000);
          Serial.print("\nVIB INDEX\n");
          analogWrite(vibIndex, 0);*/ 
          recoitDuESP8266(0); 
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
      recoitDuESP8266(temp);
      Serial.println("**********************************************************");
      envoieAuESP8266("AT+CWMODE=3");
      recoitDuESP8266(temp);
      Serial.println("**********************************************************");
      envoieAuESP8266("AT+CWJAP=\""+ NomduReseauWifi + "\",\"" + MotDePasse +"\"");
      recoitDuESP8266(temp);
      Serial.println("**********************************************************");
      envoieAuESP8266("AT+CIFSR");
      recoitDuESP8266(temp);
      Serial.println("**********************************************************");
      envoieAuESP8266("AT+CIPMUX=1");   
      recoitDuESP8266(temp);
      Serial.println("**********************************************************");
      envoieAuESP8266("AT+CIPSERVER=1,80");
      recoitDuESP8266(temp);
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
      long int timePlus = time + timeout;
       
      while(ESP8266.available())
      {
        char c = ESP8266.read();
        reponse+=c;
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

    void recoitDuESP8266Wait(const int timeout)
    {
      String reponse = "";
      long int time = millis();
      long int timePlus = time + timeout;

      while(time+timeout > millis())
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
