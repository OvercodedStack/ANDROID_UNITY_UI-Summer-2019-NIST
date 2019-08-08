# Modular Android Robot Controller Interface (MACI) for CRPI 
This project is sponsored by the Intelligent Systems Division of NIST.

### Introduction 
This Unity project is a collection of files intended to run on a tablet or smartphone device in order to provide control over a robot's functionalities. This app can be installed on any device with Android device 6.0 (newer models unconfirmed).
This app can provide an robot controller for use on Android. The intent of this project is to be able to create a user interface that is modular, functional, and flexible enough for users to modify and use in combination with a real industrial 
robot. For the purposes of application, the list below showcases the setup utilized for a use-basis case with the interface. 

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
4.	Delete all the files inside the folder and copy the repository files into the folder
5.	You’ve successfully installed the project into unity

### Usage
You can start the project through pressing the “play” button or you can compile directly onto a working android device with Android version 6.0.1. All presets are set for this version and it may not be guaranteed to work on newer Android versions. It is recommended to use a device with at least 2560x1600 pixels in the horizontal or vertical configuration as the app is optimized to use the most display available. 

### CRPI Message format
In order for this system to operate correctly, this system was designed to communicate internally with the use of TCP websockets that streams a string to CRPI for internal communciation. 
This string order is sent out at the rate given by the Unity TCP Server script in the TCP_Server_node_obj_coord script. For convenience, this is streamed at a rate every 2 seconds. 

The message format for this string is stated as the following: 
__ {$UR5_pos:(joint value 1),(joint value 2),(joint value 3),(joint value 4),(joint value 5),(joint value 6), Robot Utilities:(Robot ID),(Gripper),(Digital Port 1),(Digital Port 2),(Digital Port 3),(Digital Port 4),(Manual Bypass flag),(Vicon Robot changer flag)#} __


