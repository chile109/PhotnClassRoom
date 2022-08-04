# PhotnClassRoom
![image](https://user-images.githubusercontent.com/13402112/182757650-9650969b-c353-40e5-a38d-8ebafffd12e7.png)

This is VR experience class room for voice presentation. The scenario is like ClubHouse that teacher can hold a class room that student can join it to hear lessons, and they are all in a VR place that support HMD interaction. Also teachers can pick some student to be the speaker, both support mute feature.

These are pending feature list:
- [x] XR Interaction toolkit integration
- [x] Photon PUN2 and Voice2 integration
- [x] Teacher avatar with rollCall and mute feature
- [x] Student avatar with raise hand for speak
- [x] Lobby room list with login page
- [x] Optimization for limit number rendering and voice
- [x] Decorate Environment
- [x] Export WIN/AOS and test on Quest2

## Getting started
### Preview package
This projects rely on additional Unity packages that are 2020.3.1f1 verified:
- [XR Interaction Toolkit and Examples](https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples)
- [Photon Voice2](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518)
- [pixel horror abandoned classroom](https://assetstore.unity.com/packages/3d/environments/urban/pixel-horror-abandoned-classroom-218424#description0

## Project overview

### Login and Guide
![image](https://user-images.githubusercontent.com/13402112/182193750-dea9b491-02bf-42fd-9d75-d016c44523c1.png)
1. Enter your nickname and room name in the input area as playerdata.
2. Click button to host your own classroom and become teacher.
3. Or you can check current classroom list and just click the room name you like to join as a student.
4. Refer to guide conrol page to know how to interaction.

### Classroom
![image](https://user-images.githubusercontent.com/13402112/182427916-93a3feb0-c9ef-4731-b4bc-7175ae472b5c.png)
1. The Classroom manage the speaker list of student, the max seat number is 7.
2. There is a recycle pool that stored all unused bunny model.
3. When student become speaker, the pool will assign bunny model on it.
4. Speaker bunny model will return to pool when teacher mute the student.

### BunnyStudent
![image](https://user-images.githubusercontent.com/13402112/182073693-985cc3b9-609e-4257-b6ff-7a67947323e3.png)
1. The joined room player will become student, dedault is no model for saving render cost.
2. Not have speaker permission in default.
3. Can raise hand by primary button and display request buuble icon.
4. Only get speaker permisson from the teacher.
5. Able to use joystick to teleport on the grid area.

### BearTeacher 
![image](https://user-images.githubusercontent.com/13402112/182072631-e6dc9015-797e-4b7a-81c7-ea3d3214ab7b.png)
1. The photon room creator will become teacher.
2. Permanently has speaker permission.
3. Able to use left raycast click on student bubble icon to give speaker seat.
4. Able to use left raycast click on student speaker icon to remove speaker seat and mute.
5. Able to use joystick to teleport on the grid area.
