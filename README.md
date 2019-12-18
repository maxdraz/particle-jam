# Beautiful Generative Experience

## What is the project about?
I plan to create an application that brings the user into an expressive and hypnotic world of beautiful visuals, that can be enjoyed to their favourite music. The visuals will be heavily inspired by spirally, interesting, geometric and mathematical patterns. The goal is to provide the user with controls through which they can change and customise the visualisation in real time, jamming along with the music. I would also like there to be an 'auto pilot' mode in which the visualisation will react to the music being played, without need for user input. Users will be able to create and save their own presets or choose from pre-made ones to suit different genres / moods / speeds of music. Perfect setup for VJ-ing.

### Features planned
- Jumping to interesting geometrical patterns at the press of a button.
- Persistant custom presets - customise rotation speed, colour pallette, sensitivity to audio.
- Ability to toggle between multiple stlyles of visualisation.
- ECS for performance and huge number of particles.

### Researched topics
* [ECS](https://www.youtube.com/playlist?list=PLzDRvYVwl53s40yP5RQXitbT--IRcHqba)
* [Poisson Disc Sampling](https://www.youtube.com/watch?v=7WcmyxyFO7o)
* [Golden Ratio](https://www.youtube.com/watch?v=sj8Sg8qnjOg)
* [Audio Visualisation in Unity](https://www.youtube.com/playlist?list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo)
* [Mandelbrot Fractals](https://www.youtube.com/watch?v=6IWXkV82oyY)
* [Mathematical rose patterns](https://www.youtube.com/watch?v=f5QBExMNB1I)
* [Raymarching](https://www.youtube.com/watch?v=Cp5WWtMoeKg)
* [Prime number spirals](https://www.youtube.com/watch?v=EK32jo7i5LQ)
* [Voronoi](https://www.youtube.com/watch?v=l-07BXzNdPw)
* [Hilbert's curve](https://www.youtube.com/watch?v=3s7h2MHQtxc)
* [Flow fields](https://www.youtube.com/watch?v=rB83DpBJQsE)
* [Interesting patterns achieved with graphs](https://www.youtube.com/watch?v=pAMgUB51XZA)

### Early progress
Check out an early progress video [here](https://www.youtube.com/watch?v=GV9sL5xkrDM) :)

## What I coded myself
The majority of code used in the final project was coded by me.

**ParticlesECS.cs**
* Acts as a bootstrapper.
* Contains the Particle archetype.
* Spawns particles on a coroutine.
* Handles user input.
* Controls particles from user input.
* Handles the behaviours during Audio Reactive mode.
* Updates new particle positions.
* Gets spectrum band data from the AudioAnalyser. 
* Communicates with AudioAnalyser to control music playback.
* Handles exiting the application.

**ParticleComponentData.cs**
* Contains fields relevant to every particle.

**ParticleBehaviour.cs**
* A component system that acts on anything with a Translation and Particle component.
* Moves each particle to its designated point.

**BPM Finder**
* Calculates the BPM of a song based on how long the user holds a button.
* Works for different time signatures.

**Utilities.cs**
* A static class containing some useful functions, including the algorithm for generating the points for particles.

## What code did I use from tutorials?

**AudioAnalyser.cs**
* Used to get spectrum data form an audio clip.
* Splits the spectrum data into 8 bands, in order to be more workable.
* Sourced from this tutorial [series](https://www.youtube.com/playlist?list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo).
* I modified it to my needs, and added my own custom functions.

**Terrain1.shader**
* A shader from Bryan Duggan that colours the material based on its position.

## What am I most proud of?
I'm most proud of the fact that I implemented the particles using pure ECS. I really wanted to deepen my understanding of this efficient way of coding, and this project allowed me to do just that. I feel I now have a good grasp of the basics and am ready to take on further challenges.

I'm also happy that I was able to create a beautiful experience and that surprises me. 

## Instructions
###Start playing###  
Unity Editor - Scene found in Assets > Scenes > Particles   
Build - Launch BeautifulGenerativeExperience.exe

###Keyboard Controls###  
***Moving particles***  
Turning right/ left - right/ left arrow keys  
Faster rotation - Hold Left Shift while turning  
Slower rotation - Hold Left Ctrl while turning  
Zooming in/ out - up/ down arrow keys  
Jump to preset shape - 1,2,3 keys  

***Audio Reactivity***  
Play/ pause song - Space  
Stop song - Backspace  
Toggle audio reactivity - Return  
Find song's BPM - Hold Alt for a full bar of the song  

###XBOX Controller###    
***Moving particles***  
Turning right/ left - Left stick    
Faster rotation - RT while turning  
Slower rotation - LT while turning  
Zooming in/ out - Right stick  
Jump to preset shape - N/A  

***Audio Reactivity***  
Play/ pause song - LB + A  
Stop song - LB + B  
Toggle audio reactivity - LB + X  
Find song's BPM - RB  

###Customisation###  
To tweak variables go to Particles scene and select the ParticlesECS gameobject.

***ParticlesECS script***
* Turn Fraction - decides the intervals at which particles are spawned. Changing this will dictate the starting arrangement of particles.
* Quick Access Turn Fractions - If you find a magnificent looking arrangement of particles and want to be able to quickly jump to it during your jam, copy the Turn Fraction here to one of the 3 slots.
* Speed Slow / Normal / Fast - how quickly the particles reach their destinations. Note that numbers of 1 or below work best.
* BPM - if you already know the BPM of your song, you can input it here without using the BPM Finder.
* Beats Per Bar - how many crotchets in a bar of the song.
* Rotate After Bars - dictates how many bars of the song elapse before the particles do an automatic turn (Audio Reactive mode). 

## Youtube video

Final video can be found at this [link](https://www.youtube.com/watch?v=cGKtmeEuuGU&feature=youtu.be).
