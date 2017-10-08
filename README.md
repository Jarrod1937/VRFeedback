# VRFeedback - Summary

Have you ever been playing a game and get hurt, yet you don't care and keep pushing forward? In most games there is no consequence as long as you don't die, if you get hurt that is only a minor aspect of the game. Here, I wanted to take the next step. if you get hurt in the game you get hurt in real life. I am by far not the first person to consider this, but I was unable to find other projects that met my needs.

# How it works

The program acts as a memory scanner. Each game, when started gets loaded into memory, and there are a few base/static addresses that are based on fixed offsets from the executable file. These base addresses can hold all kinds of information, but the main one we're interested in is health. Health can be represented in multiple ways, as a 8/16/32/64 bit int, or as a single or double float. Once you know the memory address, simply point the VRFeedback application to it with the appropriate data type, and it will read the value for you. This value is then recorded and watched, and if a decrease happens it maps the decrease to a range of 1-10 (100% down to 92% is 1, 100% down to 20% is 8...etc). This information is sent via serial.

# What to do with the serial data?

The serial data is to be used by some device on the other end. The port to be referenced and the decrease amount (1-10) is encoded in a single byte (2, 4 bit ints, 0-15 each). You can read this information on your device and do with it what you please.

# What kind of feedback?

The program and accompanying Arduino code is designed to be generic. I use it for electrical shock feedback if I get hurt, however it can be used for any sort of feedback you can design. A less extreme approach would be a simple vibrational feedback, you get hurt you feel a vibration.

# How do I get the memory address?

This is the tricky part. What we're doing is essentially hacking the game. This is similar to what game cheats do, they search the memory for a value (like health), and fix it at a certain value. In this way, you could have an infinite health cheat, if that is what we were doing. Instead we're just monitoring the health value for changes, rather than using it for cheating. However, because t follows the same logic, you can use the same tools. I used Cheat Engine for finding the base address of a few games. If you use such tools and you find a non-base address, it will work only for that gaming session but the value will be mapped to a different address the next time you start the game. Finding the base address is the key and is what the VRFeedback program is intended to read, this is the address that will not change between game sessions. However, take note that is the game executable is updated, the fixed offset of the value will likely change. In the project folder is a preset file for SkyrimSE, and as you may see in the file name it only applies to version 1.53.0.

# What is the value range for?

Since we're reading raw bytes from memory, we don't know what is in them, and sometimes not even the game does. This is because memory may contain random values until it is initialized by the game. By providing a range of valid values for health we can allow the program to avoid reading random uninitialized values that are out of range until the game sets them.

# What is the bit size setting for?

The bit size is to tell the application if it is to read the memory space of a 32 bit or 64 bit game. If the game in question is 64 bit (addresses larger than 4 bytes), the 64 bit size needs to be selected, otherwise 32 should be selected.

# What is the port count for?

I setup the application with the ability to reference more than one port on the serial device end. This is so you can provide feedback to multiple areas of the body. Since most games may not expose this information via memory, currently if more than 1 port is selected the application will send feedback randomly to one of them. Thus, if you get hurt, you may get feedback on your arm, abs, leg...etc, depending on how many ports you have setup.

# Can I run my game fullscreen?

Yes, you can definitely run the game fullscreen without having to alt+tab out to start the program. I designed it so that it constantly looks for the game process in the background and when it finds it, it attaches and reads the value. All you need to have is a value COM port setup. Simply load your game preset, start serial, then start your game.
