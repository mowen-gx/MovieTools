namespace MovieLink.Model {
    
    public class DownloadLink {
        
        public virtual string Guid {
            get { return _guid; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    _guid = value;
                }
            }
        }
        private string _guid = "";
        public virtual string LinkAddr
        {
            get { return _linkAddr; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _linkAddr = value;
                }
            }
        }
        private string _linkAddr = "";
        public virtual string Type
        {
            get { return _type; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _type = value;
                }
            }
        }
        private string _type = "";
        public virtual string BusinessGuid
        {
            get { return _businessGuid; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _businessGuid = value;
                }
            }
        }
        private string _businessGuid = "";
        public virtual string Source
        {
            get { return _source; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _source = value;
                }
            }
        }
        private string _source = "";
        public virtual string SourceName
        {
            get { return _sourceName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _sourceName = value;
                }
            }
        }
        private string _sourceName = "";
    }
}
