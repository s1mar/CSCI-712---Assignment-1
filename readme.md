# CSCI 712 Assignment 1

 Goal: Write a simplified key framing system that will translate and rotate a single object based on a set of key frames. 
 Made by Simarjot Khanna under the supervision of Prof. Joe Geigel, D.Sc


## What does it do or the task at hand?

Interpolation rules: 
¤ Translation may be interpolated using: Basic linear interpolation
¤ Rotation may be interpolated using: Spherical linear interpolation of quaternions 
¤ Will need to convert axis/angle -> quaternions


## A note about the camera placement

¤ Keep camera static 
¤ Place camera so object will not move off screen

## Additional Mechanic

Once the animation ends, to replay it simply press on the replay button on the top-left edge of the screen.

## Screenshot

![Alt text](screen_grab.png "Screenshot")