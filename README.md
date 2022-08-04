# Immersed ClassroomVR
![image](https://user-images.githubusercontent.com/13402112/182757650-9650969b-c353-40e5-a38d-8ebafffd12e7.png)

This is the VR experience class room for voice presentation. The scenario is like ClubHouse where the teacher can hold a classroom where students can join it to hear lessons, and they are all in a VR place that supports HMD interaction. Also teachers can pick some students to be the speaker, both support mute features.

Progress:
- [x] XR Interaction toolkit integration
- [x] Photon PUN2 and Voice2 integration
- [x] Teacher avatar with rollCall and mute feature
- [x] Student avatar with raise hand for speak
- [x] Lobby room list with login page
- [x] Optimization for limit number rendering and voice
- [x] Decorate Environment
- [x] Export WIN/AOS and test on Quest2

Download link:　https://drive.google.com/file/d/1pDZYXl9zkXsfjbDacel3RC69FqJP0dXz/view?usp=sharing
> Demo support 2 plateform WIN/AOS, note that only AOS can open virtual keyboard for text input



## Getting started
### Rely package
This projects rely on additional Unity packages that are 2020.3.1f1 verified:
- [XR Interaction Toolkit and Examples](https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples)
- [Photon Voice2](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518)
- [pixel horror abandoned classroom](https://assetstore.unity.com/packages/3d/environments/urban/pixel-horror-abandoned-classroom-218424#description0)

## Project overview

### Login and Guide
![image](https://user-images.githubusercontent.com/13402112/182193750-dea9b491-02bf-42fd-9d75-d016c44523c1.png)
1. Enter your nickname and room name in the input area as playerdata.
2. Click the button to host your own classroom and become a teacher.
3. Or you can check the current classroom list and just click the room name you like to join as a student.
4. Refer to the guide control page to know how to interact.

### Classroom
![image](https://user-images.githubusercontent.com/13402112/182427916-93a3feb0-c9ef-4731-b4bc-7175ae472b5c.png)
1. The Classroom manages the speaker list of students, the max seat number is 7.
2. There is a recycle pool that stores all unused bunny models.
3. When a student becomes a speaker, the pool will assign a bunny model on it.
4. Speaker bunny model will return to the pool when the teacher mute the student.

### BunnyStudent
![image](https://user-images.githubusercontent.com/13402112/182073693-985cc3b9-609e-4257-b6ff-7a67947323e3.png)
1. The joined room player will become a student, default is no model for saving render cost.
2. Not have speaker permission by default.
3. Can raise hand by primary button and display request bubble icon.
4. Only get speaker permission from the teacher.
5. Able to use the joystick to teleport on the grid area.

### BearTeacher
![image](https://user-images.githubusercontent.com/13402112/182072631-e6dc9015-797e-4b7a-81c7-ea3d3214ab7b.png)
1. The photon room creator will become a teacher.
2. Permanently has speaker permission.
3. Able to use the left raycast click on the student bubble icon to give the speaker seat.
4. Able to use the left raycast click on the student speaker icon to remove the speaker seat and mute.
5. Able to use a joystick to teleport on the grid area.
