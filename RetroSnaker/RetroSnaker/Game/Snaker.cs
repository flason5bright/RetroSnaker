using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetroSnaker
{
    public class Snaker
    {

        public Node[,] Map { get; private set; }
        public List<Node> Body { get; private set; } = new List<Node>();

        public Direction Direction { get; set; } = Direction.Right;

        public delegate void LengthAddedHandler();

        public event LengthAddedHandler LengthAdded;

        public Snaker(int width, int height)
        {
            //初始化地图
            Map = new Node[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Map[i, j] = new Node(i, j);
                }
            }
        }

        public void Init()
        {
            //刷新Map
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j].IsBody = false;
                    Map[i, j].IsHeader = false;
                    Map[i, j].IsObstacle = false;
                }
            }

            Body = new List<Node>();

            //初始化身体
            Map[0, 0].IsBody = true;
            Map[0, 1].IsBody = true;
            Map[0, 2].IsBody = true;
            Map[0, 2].IsHeader = true;

            Body.Add(Map[0, 2]);
            Body.Add(Map[0, 1]);
            Body.Add(Map[0, 0]);

            Direction = Direction.Right;
        }

        public bool Move()
        {
            var header = Body.FirstOrDefault();

            //根据header找下一个点

            if (JudgeIsGameOver(header, this.Direction))
                return false;

            header.IsHeader = false;
            header.IsBody = true;

            var next = GetNextNode(header, this.Direction);
            next.IsHeader = true;
            next.IsBody = true;
            Body.Insert(0, next);

            //如果下一个是障碍物,长度增加1，不用移除末尾节点
            if (next.IsObstacle)
            {
                next.IsObstacle = false;

                if (LengthAdded != null)
                    LengthAdded();
                return true;
            }

            var last = Body.Last();
            last.IsBody = false;
            last.IsHeader = false;

            Body.Remove(last);
            return true;
        }

        //判断是否超出边界
        private bool JudgeIsOverMap(Node header, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    {
                        if (header.Location.Y + 1 == Map.GetLength(1))
                            return true;
                        break;
                    }
                case Direction.Down:
                    {
                        if (header.Location.X + 1 == Map.GetLength(0))
                            return true;
                        break;
                    }
                case Direction.Left:
                    {
                        if (header.Location.Y - 1 == -1)
                            return true;
                        break;
                    }
                case Direction.Up:
                    {
                        if (header.Location.X - 1 == -1)
                            return true;
                        break;
                    }
            }

            return false;
        }

        private bool JudgeIsGameOver(Node header, Direction direction)
        {
            if (!JudgeIsOverMap(header, direction))
            {
                //判断是否下一步是身体
                var next = GetNextNode(header, this.Direction);
                if (next.IsBody)
                    return true;
                return false;
            }
            else
            {
                return true;
            }


        }

        private Node GetNextNode(Node header, Direction direction)
        {
            Node next = null;
            switch (direction)
            {
                case Direction.Right:
                    {
                        //找到下一个点,使它成为头节点
                        next = Map[header.Location.X, header.Location.Y + 1];

                        break;
                    }
                case Direction.Down:
                    {
                        //找到下一个点,使它成为头节点
                        next = Map[header.Location.X + 1, header.Location.Y];

                        break;
                    }
                case Direction.Left:
                    {
                        //找到下一个点,使它成为头节点
                        next = Map[header.Location.X, header.Location.Y - 1];

                        break;
                    }
                case Direction.Up:
                    {
                        //找到下一个点,使它成为头节点
                        next = Map[header.Location.X - 1, header.Location.Y];

                        break;
                    }
            }

            return next;
        }

        public void SetDirection(Direction direction)
        {
            if (direction + 2 == this.Direction || direction - 2 == this.Direction)
                return;
            this.Direction = direction;
        }
    }
}
