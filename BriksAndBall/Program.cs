using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace BreakAndBalls
{

    class Program
    {
        static public Ball ball;
        static public Map map;
        static void Main(string[] args)
        {
            ball = new Ball();
            map = new Map();
            map.ShowMap();
            Console.CursorVisible = false;
            //Thread thread = new Thread(map.Move);
            //thread.Start();
            while (true)
            {
                ball.Move();
                if (map.GetElementFromMap(ball.x, ball.y) == '#' || map.GetElementFromMap(ball.x, ball.y) == '=')
                {
                    ball.ChangeCourse();
                }
                map.BallPosition(ball.x, ball.y, ball.prevX, ball.prevY);
                Thread.Sleep(80);
                ConsoleKey key = ConsoleKey.Enter;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }
                if (key == ConsoleKey.LeftArrow && map.x > 1)
                {
                    map.x -= 1;
                    map.AddPlayerToMap(map.x, map.y, map.prevX);
                    map.prevX = map.x;
                }
                if (key == ConsoleKey.RightArrow && map.x <= 40 - 5)
                {
                    map.x += 1;
                    map.AddPlayerToMap(map.x, map.y, map.prevX);
                    map.prevX = map.x;
                }
            }

        }
    }

    public class Map
    {
        private string[] map;

        public int x { get; set; } = 18;
        public int y { get; private set; } = 11;
        public int prevX { get; set; } = 18;
        public int prevY { get; private set; } = 11;
        public string sprite { get; private set; } = "=====";

        public Map()
        {
            map = null;
            map = File.ReadAllLines("map.txt");
        }

        public void ShowMap()
        {
            //Console.Clear();
            for (int i = 0; i < map.Length; i++)
            {
                Console.WriteLine(map[i]);
            }
        }

        public void BallPosition(int x, int y, int prevx, int prevy)
        {
            map[y] = map[y].Remove(x, 1).Insert(x, "o");
            map[prevy] = map[prevy].Remove(prevx, 1).Insert(prevx, " ");
            Console.SetCursorPosition(0, 0);
            ShowMap();
        }

        public char GetElementFromMap(int x, int y)
        {
            return map[y].ElementAt(x);
        }

        public void AddPlayerToMap(int x, int y, int PrevX)
        {
            map[y] = map[y].Remove(PrevX, 5).Insert(x, "=====");
            Console.SetCursorPosition(0, 0);
            ShowMap();
        }

        public void Move()
        {
            while (true)
            {

                ConsoleKey key = ConsoleKey.Enter;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }
                if (key == ConsoleKey.LeftArrow && x > 1)
                {
                    x -= 1;
                    AddPlayerToMap(x, y, prevX);
                    prevX = x;
                }
                if (key == ConsoleKey.RightArrow && x <= 40 - 5)
                {
                    x += 1;
                    AddPlayerToMap(x, y, prevX);
                    prevX = x;
                }
            }
        }
    }
    public class Controll
    {

    }

    public class Ball
    {
        public int x { get; private set; } = 20;
        public int y { get; private set; } = 10;
        public int movex { get; private set; } = 1;
        public int movey { get; private set; } = -1;
        public int prevX { get; private set; } = 20;
        public int prevY { get; private set; } = 10;

        public void Move()
        {
            prevX = x;
            prevY = y;
            x += movex;
            y += movey;
            if (x <= 1 || x >= 40)
            {
                movex = -movex;
            }
            if (y < 1 || y >= 12)
            {
                movey = -movey;
            }
        }

        public void ChangeCourse()
        {
            //movex = -movex;
            movey = -movey;
        }

    }


}
