namespace MovieLink.Model {
    
    public class MovieType {
        public MovieType() { }
        public virtual string Guid
        {
            get { return _guid; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _guid = value;
                }
            }
        }
        private string _guid = "";
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _typeName = value;
                }
            }
        }
        private string _typeName = "";
    }
}
