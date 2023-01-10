# Unity-UI-Screen-Controller
This plugin helps to maintain the UI and flow of the Game/App. This works in Screen-To-Screen manner, Easy to use and handle all Dialogs/Screens easily in your Game/App.

<!-- Installation-->
# Installation
You can download and install this with different methods given below:
* [Clone/Download](https://github.com/deveshbeniwal/Unity-UI-Screen-Controller/archive/refs/heads/main.zip) this repository and move the Unity_UIScreenController folder in to your project _Assets_ folder.
* (via Package Manager) add the following line to _Packages/manifest.json_ or you can direct copy paste the below url in _Package Manager/Add Packages From GIT Url_
  ```sh
    https://github.com/deveshbeniwal/Unity-UI-Screen-Controller.git
  ```

<!-- How to-->
# How to
* Step 1: Find the __USC_Screens_ scriptable object in Unity-UI-Screen-Controller folder.
* Step 2: Add Screen/Dialog name in the list named _Screen-names_.
* Step 3: Click on Update button below the screen names list
* Step 4: You can find the new generated script in _Assets/Scripts/Screens/Screen-{ScreenName}_
* Step 5: Attach this script to that screen gameobject you prepared.
* Step 6: Create a new GameObject and Add __USC_ScreenManager__[Singleton Class] script as a component.
* Step 7: Drag all the screens gameobject in __USC_ScreenManager__ in _AllScreens_ list.
* Step 8: Choose the first screen and manually call:
  ```sh
    USC_ScreenManager.Instance.Process_FirstScreen();
  ```
  to process the first screen you selected.
* Step 9: Now you can Switch, Show, Hide to your UI Screens by Simply using the methods
    * For Switching
        ```sh
            USC_ScreenManager.Instance.Switch(USC_SCREENS.MENU, USC_SCREENS.GAME);
        ```
    * To Show
        ```sh
            USC_ScreenManager.Instance.Show(USC_SCREENS.MENU);
        ```
    * To Hide
        ```sh
            USC_ScreenManager.Instance.Hide(USC_SCREENS.MENU);
        ```

<!-- Notes-->
# Notes
* You can edit the USC_BaseScreen.cs script to make animations to your UI switching.
* All the new generated screen is inherited from USC_BaseScreen class, so you can use the override methods to make UI updation like, Show(), Hide(), ShowCompleted(), HideCompleted().
* Please do not change the Generated screen scripts location!
* Please do not use the same name in screen list.
* Try to add screens in CAPITAL LETTERS only.


<!-- Licence-->
# Licence
[MIT License][license-url]


<!-- Contact-->
# Contact
Email: devbeniwal80@gmail.com
[LinkedIn][linkedin-url]


<!-- MARKDOWN LINKS -->
[license-url]: https://github.com/deveshbeniwal/Unity-UI-Screen-Controller/blob/13f4492c0834a744f038ab97c4d428d63e02756b/LICENSE
[linkedin-url]:https://in.linkedin.com/in/devesh-beniwal-ba4460143
