# compas_xr_unity
COMPAS XR: Visualizer app for collaborative robotic assembly

Firebase Installations Quickstart

### Requirements
1. [Unity 2022.3.3f1]
2. [Xcode 13.3.1](https://developer.apple.com/xcode/) or higher (when developing for iOS or tvOS).
3. Android SDK (when developing for Android) - should be installed when installing Unity.
4. [Rhinoceros 7](https://www.rhino3d.com/de/7/) / [Grasshopper](https://www.grasshopper3d.com/)

### Dependencies
1. [COMPAS](https://compas.dev)
2. [COMPAS Fab](https://gramaziokohler.github.io/compas_fab/latest/)
3. [COMPAS eve](https://github.com/gramaziokohler/compas_eve/tree/main)

## Getting started with this project

### Installing Dependencies
Enabling communication between grasshopper and your application with MQTT
1. Install compas_eve on your working environment and Rhino
```
conda activate <environment_name>
pip install git+https://github.com/gramaziokohler/compas_eve@main
```

2. If your host (broker) for messages is not localhost. DISABLE your Firewall or Ensure that the required ports for MQTT (typically 1883 or 8883 for secure connections) are open and accessible.

3. Possible public hosts for MQTT
    - [HiveMQ Cloud](https://www.hivemq.com/mqtt-cloud-broker/)
    - [CloudMQTT](https://www.cloudmqtt.com/)
    - [Eclipse Mosquitto](https://mosquitto.org/)



### Unity

1. Relink the VuforiaPackage and accept the Developer Agreement

    - Before opening the project - download, copy and paste the following Vuforia Tarball Package into the Unity Project:
      
    project path […] / Packages/com.ptc.vuforia.engine-10.18.4.tgz.

    https://drive.google.com/file/d/1nfvQRbzSj2HSMrrA4bCLXUkwQfWudjQT/view?usp=share_link

    - In Unity, go through the following steps in order to accept Vuforia’s Developer Agreement:
    Help - Vuforia Engine - Show Developer Agreement -> Accept
       
2. Import Ros# into the project

    - In case you don’t have a Unity ID yet, go to the Unity website and register an account.
      https://id.unity.com/en/conversations/5c9a9838-2b4d-4c7e-bc53-d31475d0ba8001af 
    - Following that, go to the Asset Store and add Ros# to your asset list:
https://assetstore.unity.com/packages/tools/physics/ros-107085
    - In Unity’s Package Manager Window open the Packages drop-down menu and choose My Assets.  Make sure you are loading all your assets in the list.
    - Download and import Ros# to the project 

### Android
(Unless you wish to test the project with the given credentials, please follow all steps below. Otherwise, skip to 7)
Register your Android app with [Firebase](https://firebase.google.com/docs/unity/setup).
1. Create a Unity project in the Firebase console.

2. Associate your project to an app by clicking the Add app button, and selecting the Unity icon.
    - You should use ```com.ETHZ.cdf``` as the package name while you're testing.
    - If you do not use the prescribed package name you will need to update the bundle identifier as described in the
      - *Optional: Update the Project Bundle Identifier below.*
    - change the rules in ```Realtime Database``` to :

```
{
  "rules": {
    ".read": true,
    ".write": true
  }
}
```

3. Accessing Firebase config information
    - In your Firebase console, navigate to Project Overview and click the gear icon.
    - In the drop-down window select Project Settings
    - In the project settings window under Your apps select CDF Web App
    - The Required Config Information is listed under the section SDK setup and configuration and an example is shown below

```
// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyAO_YVROUIc866BqgWgcBpPxUe6SVG5O9g",
  authDomain: "cdf-project-f570f.firebaseapp.com",
  databaseURL: "https://cdf-project-f570f-default-rtdb.europe-west1.firebasedatabase.app",
  projectId: "cdf-project-f570f",
  storageBucket: "cdf-project-f570f.appspot.com",
  messagingSenderId: "641027065982",
  appId: "1:641027065982:web:20ca92f0a2326bc3dab02f",
  measurementId: "G-RZ5BVHNGK8"
};
```

4. Android apps must be signed by a SHA1 key, and the key's signature must be registered to your project in the Firebase Console. To generate a SHA1, first you will need to set the keystore in the Unity project.
    - Go to ```Publishing Settings``` under ```Player Settings``` in the Unity editor.
    - Select an existing keystore, or create a new keystore using the toggle.
    - Select an existing key, or create a new key using ```Create a new key```.
    - Build an apk to be able to generate the SHA1 key
    - After setting the keystore and key, as well as building the app once, you can generate a SHA1 by running this command in CMD (admin):
      
    ```
    keytool -list -v -keystore <path_to_keystore> -alias <key_name>
    ```

    - Copy the SHA1 digest string into your clipboard.
    - Navigate to your Android App in your Firebase console.
    - From the main console view, click on your Android App at the top, and open the settings page.
    - Scroll down to your apps at the bottom of the page and click on Add Fingerprint.
    - Paste the SHA1 digest of your key into the form. The SHA1 box will illuminate if the string is valid. If it's not valid, check that you have copied the entire SHA1 digest string.
      
5. Download the ```google-services.json``` file associated with your Firebase project from the console. This file contains the information mentioned above that, you need to connect your Android app to the Firebase backend, and will need to be included either in the FirebaseInitialize script in the Unity project or at the start of the app, before initializing Firebase. You will need to look for the following parameters:
App id, api key, database url, storage bucket, and project id


6. Optional: Update the Project Bundle Identifier.
    - If you did not use ```com.ETHZ.cdf``` as the project package name you will need to update the sample's Bundle Identifier.
    - Select the File > Build Settings menu option.
    - Select Android in the Platform list.
    - Click Player Settings.
    - In the Player Settings panel scroll down to Bundle Identifier and update the value to the package name you provided when you registered your app with Firebase.
      
7. Build for Android.
    - Select the File > Build Settings menu option.
    - Select Android in the Platform list.
    - Click Switch Platform to select Android as the target platform.
    - Wait for the spinner (compiling) icon to stop in the bottom right corner of the Unity status bar.
    - Click Build and Run.
