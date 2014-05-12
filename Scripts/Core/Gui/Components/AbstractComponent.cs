using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Animations;
using Assets.Scripts.Core.Gui.Elements;
using UnityEngine;
using Color = UnityEngine.Color;
using Content = Assets.Scripts.Core.Gui.Elements.Content;
using Style = Assets.Scripts.Core.Gui.Elements.Style;

namespace Assets.Scripts.Core.Gui.Components
{
    public abstract class AbstractComponent : MonoBehaviour
    {
        [SerializeField] private string _id = "ENTER_ID";
        [SerializeField] private GuiTransform _transform = new GuiTransform();
        [SerializeField] private Style _style = new Style();
        [SerializeField] private Content _content = new Content();
        private GuiAnimation _animation;
        private bool _autoSizeWasOn;
        private Rect _startRect;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public GuiTransform Transform
        {
            get { return _transform; }
        }

        public Style Style
        {
            get { return _style; }
        }

        public Content Content
        {
            get { return _content; }
        }

        public GuiAnimation Animate()
        {
            return _animation;
        }

        private void Awake()
        {
            _animation = new GuiAnimation(this, new List<GuiAnimation>());
        }

        private void Start()
        {
            OnStart();
            Transform.Refresh();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void FixedUpdate()
        {
            _animation.Update();
        }

        public void OnDraw()
        {
            InitStyle();
            OnGuiUpdate();
            AutoSize();
            var originalColor = Color();
            var originalMatrix = Rotate();
            DrawElement();
            GUI.color = originalColor;
            GUI.matrix = originalMatrix;
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnGuiUpdate()
        {
        }

        protected abstract void DrawElement();

        protected abstract GUIStyle DefaultGuiStyle();

        private void AutoSize()
        {
            if (_transform.AutoSize)
            {
                if (!_autoSizeWasOn) _startRect = _transform.Rect;
                var size = _style.Current.CalcSize(_content.Current);
                _transform.Rect = new Rect(_transform.Rect.x, _transform.Rect.y, size.x, size.y);
                _autoSizeWasOn = true;
            }
            else if (_autoSizeWasOn)
            {
                _transform.Rect = _startRect;
                _autoSizeWasOn = false;
            }
        }

        private Color Color()
        {
            var originalColor = GUI.color;
            GUI.color = new Color(_style.Red, _style.Green, _style.Blue, _style.Alpha);
            return originalColor;
        }

        private Matrix4x4 Rotate()
        {
            var originalMatrix = GUI.matrix;
            if (0f.Equals(_transform.Rotation)) return originalMatrix;
            GUIUtility.RotateAroundPivot(_transform.Rotation, _transform.Rect.center);
            return originalMatrix;
        }

        private void InitStyle()
        {
            if (_style.Current == GUIStyle.none || _style.Current == null)
            {
                _style.Styles = new[] { DefaultGuiStyle() };
            }
        }
    }
}