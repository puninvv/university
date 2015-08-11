using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    class Server
    {
        private static int port = 1488;
        private static int timeOfGame = 10;
        private static TcpClient Waiter = null;
        private static List<Game> Games = new List<Game>();
        private static String[] gen_words_base;

        static void update_bases()
        {
            timeOfGame = Convert.ToInt32(System.IO.File.ReadAllText("time.txt"));
            Console.WriteLine("Время игры:"+timeOfGame);
            gen_words_base = System.IO.File.ReadAllLines("gen_words_base.txt");
            Console.WriteLine("Базы слов загружены!");
        }
        static string GenerateWord()
        {
            Random rnd = new Random();
            string strout = gen_words_base[rnd.Next(gen_words_base.Length)];
            Console.WriteLine(strout);
            return strout;
        }
        static void Main(string[] args)
        {
            update_bases();
            TcpListener Listen = new TcpListener(IPAddress.Any, 1488);
            Listen.Start();
            Console.WriteLine("[Server is online]");
            int games = 0;
            while (true)
            {
                //кто-то подсоединился                    
                TcpClient Client = Listen.AcceptTcpClient();
                Console.Write("[Client connected!]");
                NetworkStream ns = Client.GetStream();

                //Проверяем, есть ли кто-то в очереди
                if (Waiter != null)
                {
                    //в очереди кто-то есть, создаем новую игру
                    Games.Add(new Game(games++, Waiter, Client, timeOfGame, GenerateWord()));
                    Waiter = null;
                }
                else
                {
                    //очеред пуста, закидываем туда клиента
                    Waiter = Client;
                }
            }

        }

        private static Boolean SendMessage(TcpClient client, String message) 
        {
            try { client.GetStream().Write(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length); return true; }
            catch { return false; }
        }
        private static String GetMessage(TcpClient client) 
        {
            try
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int data = client.GetStream().Read(buffer, 0, client.ReceiveBufferSize);
                return Encoding.UTF8.GetString(buffer, 0, data);
            }
            catch { return "DC"; }
        }
        

        private class Game { 
            public int gameid;
            private TcpClient fclient;
            private TcpClient sclient;
            private int timeOfGame;
            private int timeOfStart;
            private String words;
            public Game(int id, TcpClient FC, TcpClient SC, int TOG, String W)
            {
                this.words = "";
                gameid = id;
                fclient = FC;
                sclient = SC;
                timeOfGame = TOG;
                timeOfStart = DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Hour * 3600;
                SendMessage(fclient, "1:"+ TOG + ":" + W);
                SendMessage(sclient, "2:" + TOG + ":" + W);
                Thread check = new Thread(Checker);
                check.Start();
            }

            private void Checker() 
            { 
                String word = "";
                while (true)
                {
                    Thread.Sleep((timeOfGame - 2) * 1000);
                    word = GetMessage(fclient);
                    if (word == "DC")
                    {
                        Console.WriteLine("Game "+gameid+": first client has been disconnected!");

                        if (SendMessage(sclient,"WIN") == false)
                            Console.WriteLine("Game "+gameid+": second client has been disconnected!");
                        else
                            Console.WriteLine("Game "+gameid+": second client WON");

                        break;
                    }
                    else {
                        words += word+"\n";

                        if (SendMessage(sclient,word) == false) {
                            Console.WriteLine("Game "+gameid+": second client has been disconnected!");

                            if (SendMessage(fclient, "WIN") == false)
                                Console.WriteLine("Game " + gameid + ": first client has been disconnected!");
                            else
                                Console.WriteLine("Game " + gameid + ": first client WON");

                            break;
                        }
                    }

                    Thread.Sleep((timeOfGame - 2) * 1000);
                    word = GetMessage(sclient);
                    if (word == "DC"){
                        Console.WriteLine("Game "+gameid+": second client has been disconnected!");

                        if (SendMessage(fclient,"WIN") == false)
                            Console.WriteLine("Game "+gameid+": first client has been disconnected!");
                        else
                            Console.WriteLine("Game "+gameid+": first client WON");

                        break;
                    }
                    else {
                        words += word+"\n";

                        if (SendMessage(fclient,word) == false) {
                            Console.WriteLine("Game "+gameid+": first client has been disconnected!");

                            if (SendMessage(sclient, "WIN") == false)
                                Console.WriteLine("Game " + gameid + ": second client has been disconnected!");
                            else
                                Console.WriteLine("Game " + gameid + ": second client WON");

                            break;
                        }
                    }
                }

                Games.Remove(this);
            }
        }
    }
}
