using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core.Gui.Attributes;
using Assets.Scripts.Core.Gui.Types;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [AddComponentMenu("")]
    public class GuiPanel : AbstractComponent
    {
        [SerializeField] private bool _draggable;
        [SerializeField] private bool _scrollable;
        private bool _focus;
        private bool _toBack;
        private bool _toFront;
        private float _maxHeight;
        private float _maxWidth;
        private Vector2 _scroll;
        private List<GuiComponent> _components = new List<GuiComponent>(); 

        public bool Draggable
        {
            get { return _draggable; }
            set { _draggable = value; }
        }

        public bool Scrollable
        {
            get { return _scrollable; }
            set { _scrollable = value; }
        }

        protected override void OnStart()
        {
            _components = gameObject.GetComponents<GuiComponent>().ToList();
            InjectFields();
        }

        private void OnGUI()
        {
            OnDraw();
        }

        protected override void DrawElement()
        {
            if (_draggable)
            {
                Transform.Rect = GUI.Window(Id.GetHashCode(), Transform.Rect, Draw, Content.Current, Style.Current);
            }
            else
            {
                GUI.Window(Id.GetHashCode(), Transform.Rect, Draw, Content.Current, Style.Current);   
            }
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.window);
        }

        public T AddGuiComponent<T>(string id) where T : GuiComponent
        {
            var component = gameObject.AddComponent<T>();
            component.Id = id;
            _components.Add(component);
            return component;
        }

        public T GetGuiComponent<T>(string id) where T : GuiComponent
        {
            return _components.First(component => component is T && component.Id == id) as T;
        }
        public GuiComponent GetGuiComponent(string id)
        {
            return _components.First(component => component.Id == id);
        }

        public void ToFront()
        {
            _toFront = true;
        }

        public void ToBack()
        {
            _toBack = true;
        }

        public void Focus()
        {
            _focus = true;
        }

        private void Draw(int id)
        {
            if (Input.GetKeyUp(KeyCode.Escape)) OnBack();
            SetState();
            if (!DrawWithScroll()) Draw();
            if (_draggable) GUI.DragWindow();
        }

        private bool DrawWithScroll()
        {
            if (!_scrollable) return false;
            _scroll = GUI.BeginScrollView(new Rect(0, 0, Transform.Rect.width, Transform.Rect.height),
                _scroll, new Rect(0, 0, _maxWidth + 5, _maxHeight + 5));
            Draw();
            GUI.EndScrollView();
            return true;
        }

        private void Draw()
        {
            _maxWidth = 0;
            _maxHeight = 0;
            _components
                .Where(component => component.enabled)
                .OrderBy(component => component.Transform.Position.Z)
                .ToList()
                .ForEach(component =>
                {
                    component.OnDraw();
                    var rect = component.Transform.Rect;
                    var width = rect.width + rect.x;
                    var height = rect.height + rect.y;
                    _maxWidth = Mathf.Max(_maxWidth, width);
                    _maxHeight = Mathf.Max(_maxHeight, height);
                });
        }

        private void SetState()
        {
            if (_toFront) GUI.BringWindowToFront(Id.GetHashCode());
            if (_toBack) GUI.BringWindowToBack(Id.GetHashCode());
            if (_focus) GUI.FocusWindow(Id.GetHashCode());
            _toFront = false;
            _toBack = false;
            _focus = false;
        }

        private void InjectFields()
        {
            GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttributes(typeof (InjectAttribute), false).Any())
                .ToList().ForEach(field => field.SetValue(this,
                    IsPanel(field)
                        ? FindObjectsOfType(field.FieldType)
                            .First(panel =>
                            {
                                var guiPanel = panel as GuiPanel;
                                return guiPanel != null && guiPanel.Id == field.Name;
                            })
                        : GetGuiComponent(field.Name)));
        }

        private static bool IsPanel(FieldInfo field)
        {
            return field.FieldType.IsSubclassOf(typeof(GuiPanel)) || field.FieldType == typeof(GuiPanel);
        }

        protected virtual void OnBack()
        {
        }
    }
}