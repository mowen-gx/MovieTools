namespace MovieLink.Model {
    
    public class MovieName {
        public MovieName() { }
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
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
            }
        }
        private string _name = "";
    }
}
