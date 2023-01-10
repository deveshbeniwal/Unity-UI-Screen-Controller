using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDev_Devesh.UI_Screen_Controller
{
    public class USC_ScreenManager : MonoBehaviour
    {
        static USC_ScreenManager instance;
        public static USC_ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<USC_ScreenManager>();

                return instance;
            }
        }


        [SerializeField] USC_BaseScreen[] allScreens;
        [SerializeField] USC_SCREENS firstScreen;


        private void Awake()
        {
            foreach (var item in allScreens)
                item.Initialize();
        }


        public void RefreshAssets()
        {
            allScreens = GameObject.FindObjectsOfType<USC_BaseScreen>();
        }
        public void Process_FirstScreen()
        {
            Show(firstScreen);
        }



        USC_BaseScreen GetScreen(USC_SCREENS sType)
        {
            return System.Array.Find(allScreens, x => x.screen_type == sType);
        }

        public void Switch(USC_SCREENS toshow, USC_SCREENS tohide)
        {
            USC_BaseScreen toShow_Screen = GetScreen(toshow);
            if (toShow_Screen != null)
                toShow_Screen.Show();

            USC_BaseScreen toHide_Screen = GetScreen(tohide);
            if (toHide_Screen != null)
                toHide_Screen.Hide();
        }
        public void Show(USC_SCREENS toshow)
        {
            USC_BaseScreen toShow_Screen = GetScreen(toshow);
            if (toShow_Screen != null)
                toShow_Screen.Show();
        }
        public void Hide(USC_SCREENS tohide)
        {
            USC_BaseScreen toHide_Screen = GetScreen(tohide);
            if (toHide_Screen != null)
                toHide_Screen.Hide();
        }
    }
}