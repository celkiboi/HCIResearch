English:

# Title: Human computer interaction in videogames
## Made by: Tomislav Celić

### Facunlty of Electrical Engineering, Computer Science and Information Technology Osijek
### Undergraduate university study programme in Computer Engineering - Software Engineering

## Mentor: doc. dr. sc. Bruno Zorić

## WELCOME TO "HCIRESEARCH"
This videogame was made to research human computer interaction in videogames. It's a side scroller 2D infinite runner where the player has to avoid obstacles either by ducking or jumping. You can play the game using multiple methods:
- using your keyboard
- using an xbox controller
- using facial recognition, position you face (either by jumping and ducking, or by changing your sitting stance) and watch the player respond to your actions
- using color detection, choose the color of the object that you wish to move around, and see the player respond

### MULTIPLE OBSTACLES
- Rocket -> this obstacle won't do anything to you unless you choose to jump at it. It's purpose is simply to confuse you
- Candles -> this obstacle will punish you if you don't jump in time
- Big balloons -> this obstacle will punish you if you don't duck in time
- Small balloons -> this obstacle is the most flexible. You can either jump over it, or duck under it.

### MULTIPLE POWERUPS
- Blue coin -> this powerup will grant you 50 SCORE
- Gold coin -> this powerup will grant you 100 SCORE
- Fog circle -> this powerup will grant you temprorary invicibility to obstacles

## SETTINGS GUIDE

### FACIAL DETECTION

#### Face Detecion Frequency
This slider allows you to change the frequency of facial detection. Beware, that higher values result in higher CPU usage, while lower result in smaller precision and higher input lag

#### Jump threshold
This slider allows you to change the threshold for jumping. The values closer to 0 are higher on the camera while higher values are lower on the camera (I.E the top row of pixels on your camera has value 0)

#### Duck threshold
This slider allows you to change the threshold for ducking. The values closer to 0 are higher on the camera while higher values are lower on the camera (I.E the top row of pixels on your camera has value 0)

### COLOR DETECTION

#### Color Detection Pixels To Skip
This value allows you to change how many pixels are skipped. Imagine your camera like a W x H pixel matrix. 4 pixels to skip means every 4th pixel in every 4th row will be analyzed. Higher values (maximum 16) means lower input lag and better CPU performance, but will reduce precision. Use 1 for value unless you need extra performance. Available values are 1,2,4,8,16

#### Select Color
This submenu will allow you to choose which color to track for color detection gamemode. Recomended object are sticky notes, colorful pencils... any monocolor object with unique colors and enough surface

