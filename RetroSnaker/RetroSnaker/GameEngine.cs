using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RetroSnaker
{
    public class GameEngine : INotifyPropertyChanged
    {
        public int MapWidth { get; set; } = 18;
        public int MapHeight { get; set; } = 18;

        public Snaker Snaker { get; set; }

        private Timer _timer = new Timer();

        public List<Node> Map
        {
            get { return Snaker.Map.ToList(); }
        }

        private bool _isGameOver = false;
        public bool IsGameOver
        {
            get { return _isGameOver; }
            set
            {
                _isGameOver = value;
                Notify(nameof(IsGameOver));
            }
        }

        public delegate void GameOverEventHandler();

        public event GameOverEventHandler GameOver;

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameEngine()
        {
            Snaker = new Snaker(MapWidth, MapHeight);
        }

        public void StartGame()
        {
            IsGameOver = false;

            Snaker.Init();
            //长度加1后，生成下一个障碍物
            Snaker.LengthAdded += () => { SetObstacle(); };

            //生成第一块障碍物
            SetObstacle();

            _timer.Interval = 500;
            _timer.Elapsed += (s, e) =>
            {
                if (!Snaker.Move())
                {
                    IsGameOver = true;
                    _timer.Stop();
                    if (GameOver != null)
                        GameOver();
                }

            };

            _timer.Start();
        }

        //随机生成障碍物坐标
        private Tuple<int, int> InitObstacle()
        {
            Random random = new Random();

            var x = random.Next(0, MapHeight);
            var y = random.Next(0, MapWidth);

            if (Snaker.Map[x, y].IsBody)
                InitObstacle();
            return new Tuple<int, int>(x, y);
        }

        //设置障碍物
        private void SetObstacle()
        {
            var location = InitObstacle();
            Snaker.Map[location.Item1, location.Item2].IsObstacle = true;
        }


    }
}
