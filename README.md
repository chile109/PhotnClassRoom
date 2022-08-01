# PhotnClassRoom
This is VR experience class room for voice presentation. The scenario is like ClubHouse that teacher can hold a class room that student can join it to hear, but they are all in a VR place that support HMD interaction. Also teacher can pick some student to be the speaker, both support mute feature.

These are pending feature list:
- [x] XR Interaction toolkit integration
- [x] Photon PUN2 and Voice2 integration
- [x] Teacher avatar with rollCall and mute feature
- [x] Student avatar with raise hand for speak
- [ ] Lobby room list with login page
- [ ] Optimization for limit number rendering and voice

## Getting started
### Preview package
This projects rely on additional Unity packages that are 2020.3.1f1 verified:
- [XR Interaction Toolkit and Examples](https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples)
- [Photon Voice2](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518)

## Project overview

### BunnyStudent
![image](https://user-images.githubusercontent.com/13402112/182073693-985cc3b9-609e-4257-b6ff-7a67947323e3.png)
1. The joined room player will become student.
2. Not have speaker permission in default.
3. Can raise hand by primary button and display request buuble icon.
4. Only get speaker permisson from the teacher.
5. Able to use right hand joystick to teleport.


### BearTeacher 
![image](https://user-images.githubusercontent.com/13402112/182072631-e6dc9015-797e-4b7a-81c7-ea3d3214ab7b.png)
1. The photon room creator will become teacher.
2. Permanently has speaker permission.
3. Able to use left raycast click on student bubble icon to give speaker seat.
4. Able to use left raycast click on student speaker icon to remove speaker seat and mute.
5. Able to use right hand joystick to teleport.
