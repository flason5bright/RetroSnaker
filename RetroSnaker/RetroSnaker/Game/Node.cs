using System.ComponentModel;

namespace RetroSnaker
{
    public class Node : INotifyPropertyChanged
    {
        private bool _isHeader = false;
        public bool IsHeader
        {
            get { return _isHeader; }
            set
            {
                _isHeader = value;
                Notify(nameof(IsHeader));
            }
        }

        private bool _isBody = false;
        public bool IsBody
        {
            get { return _isBody; }
            set
            {
                _isBody = value;
                Notify(nameof(IsBody));
            }
        }

        private bool _isObstacle = false;
        public bool IsObstacle
        {
            get { return _isObstacle; }
            set
            {
                _isObstacle = value;
                Notify(nameof(IsObstacle));
            }
        }

        public Location Location { get; set; }

        public Node(int x, int y)
        {
            Location = new Location(x, y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}