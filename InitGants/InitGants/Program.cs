using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InitGants
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] fingers = { 0, 0, 0, 0, 0};

            int val = 255;

            WebRequest request;
            WebResponse response;

            int mode = 1;

            int delay = 400;

            if(mode==2)
            {
                delay = 300;
            }

            for(int i=0;i< fingers.Length;i++)
            {
                if (mode == 2)
                {
                    for (int j = 0; j < fingers.Length; j++)
                        fingers[j] = 0;
                }
                
                fingers[i] = val;
                Console.WriteLine("Send right finger " + i);
                request = WebRequest.Create("http://192.168.0.101/"+fingers[0]+"?"+fingers[1]+"?"+fingers[2]+"?"+fingers[3]+"?"+fingers[4]);
                request.Proxy = null;
                request.Timeout = 50;
                try
                {
                    response = request.GetResponse();
                }
                catch(Exception e)
                {

                }
                Thread.Sleep(delay);
            }
            for (int j = 0; j < fingers.Length; j++)
                fingers[j] = 0;
            request = WebRequest.Create("http://192.168.0.101/" + fingers[0] + "?" + fingers[1] + "?" + fingers[2] + "?" + fingers[3] + "?" + fingers[4]);
            request.Proxy = null;
            request.Timeout = 50;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception e)
            {

            }
            //Thread.Sleep(delay);

            for (int i = 0; i < fingers.Length; i++)
            {
                if (mode == 2)
                {
                    for (int j = 0; j < fingers.Length; j++)
                        fingers[j] = 0;
                }
                fingers[i] = val;
                Console.WriteLine("Send left finger "+i);
                request = WebRequest.Create("http://192.168.0.102/" + fingers[0] + "?" + fingers[1] + "?" + fingers[2] + "?" + fingers[3] + "?" + fingers[4]);
                request.Proxy = null;
                request.Timeout = 50;
                try
                {
                    response = request.GetResponse();
                }
                catch (Exception e)
                {

                }
                Thread.Sleep(delay);
            }
            for (int j = 0; j < fingers.Length; j++)
                fingers[j] = 0;
            request = WebRequest.Create("http://192.168.0.102/" + fingers[0] + "?" + fingers[1] + "?" + fingers[2] + "?" + fingers[3] + "?" + fingers[4]);
            try
            {
                response = request.GetResponse();
            }
            catch (Exception e)
            {

            }
            Thread.Sleep(delay);




        }
    }
}
