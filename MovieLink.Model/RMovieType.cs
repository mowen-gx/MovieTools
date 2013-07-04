namespace MovieLink.Model {
    
    public class RMovietype {
        public RMovietype() { }
        public virtual string MovieGuid
        {
            get { return _movieGuid; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _movieGuid = value;
                }
            }
        }
        private string _movieGuid = "";
        public virtual string MovietypeGuid
        {
            get { return _movietypeGuid; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _movietypeGuid = value;
                }
            }
        }
        private string _movietypeGuid = "";
    }
}
