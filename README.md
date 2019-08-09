# Modular Android Robot Controller Interface (MACI) for CRPI 
This project is sponsored by the Intelligent Systems Division of NIST.

### Disclaimer
Certain commercial equipment, instruments, or materials are identified in this paper to foster understanding. Such identification does not imply recommendation or endorsement by the National Institute of Standards and Technology, nor does it imply that the materials or equipment identified are necessarily the best available for the purpose.

### Introduction 
This Unity project is a collection of files intended to run on a tablet or smartphone device in order to provide control over a robot's functionalities. This app can be installed on any device with Android device 6.0 (newer models unconfirmed).
This app can provide an robot controller for use on Android. The intent of this project is to be able to create a user interface that is modular, functional, and flexible enough for users to modify and use in combination with a real industrial 
robot. For the purposes of application, the list below showcases the setup utilized for a use-basis case with the interface. 

For more information on the project and its general purpose, please visit the [documentation repository](https://github.com/OvercodedStack/CRPI-UI-DOCUMENTATION-Summer-of-2019). 

### ASSUMPTIONS ABOUT HARDWARE AND SOFTWARE
The hardware that was utilized throughout this project remained consistent and can be summarized in the following list: 
-	Android tablet SM-T700 (Collablab2) 
-	Latitude E5570  
-	UR5 robot
-	CollablabA network structure
-	Vicon system (10 camera setup) 
The following list of IP addresses were utilized throughout the project: 
-	Vicon system:
o	169.254.152.38 (CollablabA)
o	129.6.35.213 (NistNet) 
-	Tablet IP address:
o	169.254.152.27 (CollabllabA)
-	Nithya’s laptop:
o	169.254.152.2 (CollabllabA)
o	129.6.35.81 (NistNet) 
While not strictly relevant, the following are the IP addresses of the robots used in the project: 
-	169.254.152.47 (UR5) 
-	169.254.152.46 (UR10Left) 
-	169.254.152.45 (UR10Right)
-	169.254.152.80 (ABB Left and right) 

### Installation process

As Github limits the size of uploads, this project will be limited to only including the source folder. Therefore a manual copy and overwrite of the project has to be done in order to download and install the project. This is the process to do so. 
1.	Start Unity editor
2.	Click on new, name a new project, start it as any type of project 
3.	Navigate to the “Project” tab and right click on the assets folder. Click on “Show in Explorer” 
4.	Delete all the files inside the folder and copy all repository files into the folder
5.	You’ve successfully installed the project into unity

### Usage
You can start the project through pressing the “play” button or you can compile directly onto a working android device with Android version 6.0.1. All presets are set for this version and it may not be guaranteed to work on newer Android versions. It is recommended to use a device with at least 2560x1600 pixels in the horizontal or vertical configuration as the app is optimized to use the most display available. 

### Controls

There are three possible ways of directly controlling the robot at the present moment; these are (from left to right) the XYZ position and rotation control, joystick controls, and free-move mode.  

![alt text](https://raw.githubusercontent.com/OvercodedStack/CRPI-UI-DOCUMENTATION-Summer-of-2019/master/Images/Control%20schemes.PNG)

The XYZ control is similar in action to those controls as provided by a traditional CAD or 3D modeling program and intended for ease of understanding. 

The joystick moves the end-effector and rotates it accordingly.

The free-move mode moves the endeffector according to how the user desires to move the device relative to the angle of the camera and their touch-swipe. 

### CRPI Message format
In order for this system to operate correctly, this system was designed to communicate internally with the use of TCP websockets that streams a string to CRPI for internal communciation. 
This string order is sent out at the rate given by the Unity TCP Server script in the TCP_Server_node_obj_coord script. For convenience, this is streamed at a rate every 2 seconds. 

#### By default, transmission of data is sent through the local network pointed to the CRPI client at 169.254.152.2 on port 27000

The message format for this string is stated as the following: 
__{$UR5_pos:(joint value 1),(joint value 2),(joint value 3),(joint value 4),(joint value 5),(joint value 6), Robot Utilities:(Robot ID),(Gripper),(Digital Port 1),(Digital Port 2),(Digital Port 3),(Digital Port 4),(Manual Bypass flag),(Vicon Robot changer flag)#}__

1. Joint value 1 - 6: Angles as recieved to the UR5
2. Robot ID#: the enabled robot
4. Gripper: This allows the gripper to toggle between closed and open 
5. DO(1 - 4): These are the digital port outputs as controlled by the user
6. Manual Bypass flag: This allows the user to bypass the Vicon system and also override the Vicon vision control
7. Vicon Robot changer flag: This flag determines if the user desires to switch robots either through the vicon or the override

### Log file 

The log file for the app can be found in the following locations:

For use in the Unity editor: **/Assets/Resources/**

For use in the Android App:  **Device Storage/Android/Data/com.NIST.CRPI_UI/files**

For most actions performed on the Unity App, there are various listeners and actions that are being recorded, including the time-stamp, the position of the end-effector, the position of the joints, digital ports, and many other features that are occuring at the time of use. This will be detailed in the following file list explanation. In chronological order from left to right the log file will appear as such: 

1. Timestamp: Stored as hour minutes seconds with no comma spacing 
2. Jnt(1 - 6): The joint angle value in degrees rotated towards. This is the data that has been sent to the robot for CRPI
3. Robot ID#: The enabled robot (desired robot) flag
4. Gripper: This allows the gripper to toggle between closed and open 
5. DO(1 - 4): These are the digital port outputs as controlled by the user
6. Bypass Active?: This allows the user to bypass the Vicon system and also override the Vicon vision control
7. Chng_robots: This flag determines if the user desires to switch robots either through the vicon or the override
8. X: Coordinate system based on Unity scale and location (position)
9. Y: Coordinate system based on Unity scale and location (position)
10. Z: Coordinate system based on Unity scale and location (position)
11. Q_X: Coordinate system based on Unity scale and location (rotation in quaternion)
12. Q_Y: Coordinate system based on Unity scale and location (rotation in quaternion)
13. Q_Z: Coordinate system based on Unity scale and location (rotation in quaternion)
14. Q_W: Coordinate system based on Unity scale and location (rotation in quaternion)
15. Mouse pointer X: Coordinates based on those relative to the position on the screen size
16. Mouse pointer Y: Coordinates based on those relative to the position on the screen size
17. Screen Mode: Direction of screen (Landscape or portrait) 
18. Button being used: The action being performed by the user 

### Bugs 

There are some bugs that didn't get enough time to be fixed or implemented. In specific this section will talk about the bugs that would have taken more time to fix than would have been to implement. 

- The joystick controls control direction get disoriented when the target gameobject is moved with the other types of controls (e.g: XYZ rotation). 
- Some script names were not the best choice for use in certain situations and due to the nature of Unity, when a game object is included with a different script, could result in a missing reference error. In specific, please note this is highly probable with the script *TCP_Server_node_Obj_coordinator* due to its name-specific reference in **rotator.cs**.
- The Z-axis on the XYZ axis control is emulated using XY mouse delta positions and as such may result in sometimes inaccurate readings or "floaty" control. 
- The inverse kinematic control model is not very good at avoiding collisions with itself and may result in accidental clipping with the real robot itself.
- Some scripts are legacy and are not used anymore. 

### Conclusion

Special thanks to the following people during SURF 2019:

- Shelly Bagchi
- Dr. Jeremy Marvel
- Megan Zimmerman
- Holiday Inn SURF Fellows

Thank you all for being the great people you guys are!
