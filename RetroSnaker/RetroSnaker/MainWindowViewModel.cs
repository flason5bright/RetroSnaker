using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroSnaker
{
    class MainWindowViewModel
    {
        public GameEngine Engine { get; set; }

        public ICommand KeyEventCommand { get; set; }

        public ICommand StartGameCommand { get; set; }

        public MainWindowViewModel()
        {
            Engine = new GameEngine();
            Engine.GameOver += () =>
            {

            };

            //处理键盘事件
            KeyEventCommand = new RelayCommand(o =>
            {
                var direction = (Direction)Enum.Parse(typeof(Direction), o.ToString());
                Engine.Snaker.SetDirection(direction);

            });

            //开始游戏
            StartGameCommand = new RelayCommand(o => { Engine.StartGame(); });
        }

    }
}
