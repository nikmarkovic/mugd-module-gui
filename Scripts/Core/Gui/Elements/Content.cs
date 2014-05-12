using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Elements
{
    [Serializable]
    public class Content
    {
        [SerializeField] private GUIContent[] _contents;
        private int _current;

        public GUIContent[] Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        public GUIContent Current
        {
            get { return _contents == null || !_contents.Any() ? GUIContent.none : _contents[_current]; }
        }

        public int CurrentIndex
        {
            get { return _current; }
            set
            {
                if (_contents == null || _contents.Length - 1 < Mathf.Max(0, value)) return;
                _current = Mathf.Max(0, value);
            }
        }
    }
}