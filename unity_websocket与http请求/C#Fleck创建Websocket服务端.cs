using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
  
            Console.WriteLine(DateTime.Now.ToString() + " 使用说明:SDmoni项目");
            Console.WriteLine("1.先启动本程序，再打开兵力客户端");
            Console.WriteLine("2.正在监听3D主端，转发2D端");

            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://127.0.0.1:6009");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("与客户端已经连接上 Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("与客户端已经断开连接 Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine("########");
                    Console.WriteLine("接受到数据"+message);
                
                    Console.WriteLine("##########");
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send("服务端: " + input);
                }
                input = Console.ReadLine();
            }

           


         

        }
    }
}
