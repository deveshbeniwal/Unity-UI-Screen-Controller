using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDev_Devesh.UI_Screen_Controller
{
    public class USC_BaseScreen : MonoBehaviour
    {
        public USC_SCREENS screen_type;


        public virtual void Initialize()
        { 
        }


        public virtual void Show()
        { 
        }
        protected virtual void ShowCompleted()
        { 
        }

        public virtual void Hide()
        {
        }
        protected virtual void HideCompleted()
        {
        }
    }
}