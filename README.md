# Cloth Simulation - A Plain Mural

## Authors

-   Madhava Raveendra
-   Skylar Volden

## Description
This project allows the user to interact with a cloth simulation. We created a long banner that hangs in midair and reacts to being pushed, gravity, and wind simulation, with air drag. The camera can be moved freely, allowing the user to closely examine the movement of the cloth.
## Features
- Realistic cloth motion with air drag<br />
- User can shoot a ball to interact with the cloth <br />
- User can move the camera freely around the scene <br />
- A fan can be turned on, with multiple settings to showcase different amounts of wind. This fan moves with the camera, and therefore can be positioned in real time. <br />
## Code

We are using Unity3D Game Engine for the implementation of this project. Using spheres as particles points/positions, we are constructing a cloth by joining them using Line Renderers. Using Unity's Collider System, we are implementing collisions on the cloth by additing Sphere colliders to all spheres that make up the cloth. For the holder of the cloth we are using a free external asset found on SketchFab created by Ez. For air drag applied on the cloth, we are using the air drag equations provided in the lecture slides formulated by Lord Rayleigh.

## Execution

Use the button below to navigate to the source code.
Find the 'Build' folder and run the executable inside.

Controls : <br />
Mouse : Look Around <br />
ASWD/Arrow Keys : Move Around <br />
1 : Fan OFF <br />
2 : Fan LOW <br />
3 : Fan MID <br />
4 : Fan HIGH <br />
q : Quit Application <br />

## Difficulties

## Video


https://media.github.umn.edu/user/17151/files/9588a9c1-5035-499e-bcb2-b954867bbb77


## Images
![Cloth Simulation](https://media.github.umn.edu/user/17151/files/148731c9-f947-438b-aabd-8f6f12db5ddc)


## References/Links

-   Cloth Holder 3D Asset: https://sketchfab.com/3d-models/banner-holder-bfcc5bfd393742d595bd13770c6c16e5
