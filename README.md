# Plinkrupt

# Building the Plinkrupt Project for iOS


## Steps to Build the Project

### 1. Build the Project in Unity
1. Clone the repository from [https://github.com/RichardOhata/Plinkrupt](https://github.com/RichardOhata/Plinkrupt).
2. Open the project in Unity.
3. Configure the project for an iOS build:
   - Go to **File > Build Settings**.
   - Select **iOS** as the platform.
   - Click **Build** to generate the Xcode project files.
4. Note the location of the generated `.xcodeproj` file.

### 2. Open the Project in Xcode
1. Locate the generated `.xcodeproj` file.
2. Double-click to open it in Xcode.

### 3. Configure Xcode
1. In Xcode, select the project in the **Project Navigator**.
2. Under the **General** tab:
   - Ensure your **Team** is selected (linked to your Apple Developer account).
   - Verify the **Bundle Identifier** matches the settings configured in Unity.
3. Under **Signing & Capabilities**:
   - Check **Automatically manage signing**, or manually select your provisioning profile.
   - Add any required capabilities (e.g., Push Notifications, In-App Purchases).
4. Under **Build Settings**:
   - Confirm **Bitcode** is enabled (recommended for App Store submission).
   - Set the **Swift Version** if your project uses Swift plugins.

### 4. Test on Simulator or Device
1. Connect an iOS device via USB (ensure it’s registered in your Apple Developer account) or select a simulator.
2. In Xcode, select your target device from the top-left dropdown.
3. Click the **Play** button to build and run the project on the selected device or simulator.
4. Debug any issues using Xcode’s console output.

## Notes
- Ensure all dependencies and plugins in the Unity project are compatible with iOS.
- Verify that your Apple Developer account is properly set up for signing and testing.
- Refer to the Trello board for project management and task tracking.
