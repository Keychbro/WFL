using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Kamen
{
    [Serializable] public class KamenText
    {
        #region Enums

        public enum Type
        {
            Text,
            TMPro3D,
            TMProUI
        }
        
        #endregion

        #region Variables

        [SerializeField] private Type _type;
        [SerializeField] private Text _legacyText;
        [SerializeField] private TextMeshPro _tmpro3DText;
        [SerializeField] private TextMeshProUGUI _tmproUIText;
        
        #endregion

        #region Properties

        public Text LegacyText
        {
            get 
            {
                if (_type != Type.Text) return null;
                else return _legacyText;
            }
        }
        public TextMeshPro TMPro3DText
        {
            get 
            {
                if (_type != Type.TMPro3D) return null;
                else return _tmpro3DText;
            }
        }
        public TextMeshProUGUI TMProUIText 
        {
            get 
            {
                if (_type != Type.TMProUI) return null;
                else return _tmproUIText;
            }
        }


        #endregion

        #region Control Methods

        public void SetText(string text)
        {
            switch (_type)
            {
                case Type.Text:
                    _legacyText.text = text;
                    break;
                case Type.TMPro3D:
                    _tmpro3DText.text = text;
                    break;
                case Type.TMProUI:
                    _tmproUIText.text = text;
                    break;
            }
        }
        public void UpdateTextMesh()
        {
            switch (_type)
            {
                case Type.Text:
                    _legacyText.GraphicUpdateComplete();
                    break;
                case Type.TMPro3D:
                    _tmpro3DText.ForceMeshUpdate();
                    break;
                case Type.TMProUI:
                    _tmproUIText.ForceMeshUpdate();
                    break;
            }
        }
        public Graphic GetGraphicObject()
        {
            return _type switch
            {
                Type.Text => _legacyText,
                Type.TMPro3D => _tmpro3DText,
                Type.TMProUI => _tmproUIText,
                _ => null,
            };
        }

        #endregion
    }
}