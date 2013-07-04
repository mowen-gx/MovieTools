using System;
using System.Collections.Generic;

namespace MovieLink.Model {
    
    public class Movie {
        public Movie() { }
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
        public virtual string Country
        {
            get { return _contry; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _contry = value;
                }
            }
        }
        private string _contry ="";
        public virtual string Language
        {
            get { return _language; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _language = value;
                }
            }
        }
        private string _language = "";
        public virtual string ScreenWriter
        {
            get { return _screenWriter; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _screenWriter = value;
                }
            }
        }
        private string _screenWriter = "";
        public virtual string Summary
        {
            get { return _summary; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _summary = value;
                }
            }
        }
        private string _summary = "";
        public virtual string MainPic
        {
            get { return _mainPic; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _mainPic = value;
                }
            }
        }
        private string _mainPic = "";
        public virtual string Publish
        {
            get { return _publish; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _publish = value;
                }
            }
        }
        private string _publish = "";
        public virtual string Screenwriter
        {
            get { return _screenwriter; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _screenwriter = value;
                }
            }
        }
        private string _screenwriter = "";
        public string Source
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
        public string SourceName
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
        private List<string> _otherNames = new List<string>(); 
        public List<String> OtherNames {
            get { return _otherNames; }
            set { _otherNames = value; }
        }

        private List<string> _downloadLinks = new List<string>();
        public List<String> DownloadLinks
        {
            get { return _downloadLinks; }
            set { _downloadLinks = value; }
        }

        private int _isSyn = -1;
        public int IsSyn {
            get { return _isSyn; }
            set { _isSyn = value; }
        }
    }
}
