# VRFeedback - Summary

Originally intended to just provide feedback when getting hurt in a game, this application allows you to read up to 4 independent variables of multiple types under multiple conditions, of values in a games' memory, and send the mapped data via serial. This allows one to create feedback mechanisms in real life based on the game state. A basic example is getting a shock if you get hurt in a game. While this is originally intended for VR games, it should work in all games where the memory is readable.

![VRFeedback Preview](https://github.com/Jarrod1937/VRFeedback/blob/master/vrpreviewnew.jpg)

# How it works

The program acts as a memory scanner. Each game, when started gets loaded into memory, and there are a few base/static addresses that are based on fixed offsets from the executable files base address. These static addresses can hold all kinds of information, such as player position, wind speed, player state...etc. For this explaination we'll look at health. Health can be represented in multiple ways, as a 8/16/32/64 bit int, or as a single or double float. Once you know the memory address, simply point the VRFeedback application to it with the appropriate data type, and it will read the value for you. This value is then recorded and watched, and if the 'when' condition is triggered, the state information is converted to a 4 bit int along with a 4 bit port number as a single byte. This information is sent via serial.

# What to do with the serial data?

The serial data is to be used by some device on the other end. The port to be referenced and state information is encoded in a single byte (2, 4 bit ints, 0-15 each). You can read this information on your device and do with it what you please.

# What kind of feedback?

The program and accompanying Arduino code is designed to be generic. I use it for electrical shock feedback if I get hurt, however it can be used for any sort of feedback you can design. A less extreme approach would be a simple vibrational feedback, you get hurt you feel a vibration. Since the application can track up to 4 indepenent variables, the port referenced can be used for different feedback variables. Using the appropriate 'when' condition, you can track health increase/decrease, temperature, wind speed, if the character is attacking...etc. The program is designed to be very generic, so you can track almost any sort of feedback you want.

# How do I get the memory address and/or offset?

This is the tricky part. What we're doing is essentially hacking the game. This is similar to what game cheats do, they search the memory for a value (like health), and fix it at a certain value. In this way, you could have an infinite health cheat, if that is what we were doing. Instead we're just monitoring the health value for changes, rather than using it for cheating. However, because it follows the same logic, you can use the same tools. I used Cheat Engine for finding the static addresses of a few games. If you use such tools and you find a non-static address, it will work only for that gaming session but the value will be mapped to a different address the next time you start the game. Finding the static address is the key and is what the VRFeedback program is intended to read, this is the address that will not change between game sessions. However, take note that if the game executable is updated, the fixed address of the value will likely change. In the project folder is a preset file for SkyrimSE, and as you may see in the file name it only applies to version 1.5.3.0.

# What is the difference between a memory address and offset?

The application gives you two options for reading a values location. The first is a memory address, the second is an offset. A offset is a distance from the games' executable memory address, it's base address. A straight memory address is equal to the base address + an offset. However, while in theory a game should be loaded into the same base address every time, this is not always the case. The application records the base address when it locates the games process and adds an offset to this address, if offset is chosen. As such, using offset is the preferred method as it is dynamically reading the base address each time, so it is more stable.

# What are the 'send when' options?

While reading a value at a particular address, we need a way to specify when an event is to be sent via serial. The 'when' options tell the application when to trigger this sending. All send values are 4 bit int's, resulting in a max range of 0-15.
The options given are:

0, On Decrease: If the value decreases from it's last read value, an event will be sent. If the value increases, it will be ignored until it decreases. The relative percentage of decrease is sent as the value. This percentage is mapped to 0-10, 0 being a 0% change and 10 being a 100% change.

1, On Increase: Very similar to the on decrease except this event ignores decreases and only sends an event if it increases. The value sent is a relative percentage of the increase from the last value. This percentage is mapped to 0-10, 0 being a 0% change and 10 being a 100% change.

2, On Change: A combination of on decrease and increase. Sends the relative percentage (0-10) of any change.

3, Continuous: Taking the values min and max range settings, the current read value is mapped to a 4 bit int. If the range is 0-100, 0 = 0, 8 = 50, 15 = 100. This mapping is sent contiuously, and is ideal for continuous variables like temperature.

4, On State: A state is a single value, if the read value is equal to this state value, an event occurs. The state list is to the right, it is a comma separated list of values, each parsed as the same data type as the value being read. You can have up to 16 state values. When a state value triggers an event, the zero based index of that state in the state list is sent. For example a state list of:
6,34,87,99,100

And the read value is 99, then a serial event is triggered and the value of '3' is sent, as the index value of 99 is '3'.
This allows you to track something that is a state in a game. For example, if the game character is sneaking, walking, running, laying down...etc, all may be the same variable in memory represented by different state values.


# What is the value range for?

Since we're reading raw bytes from memory, we don't know what is in them, and sometimes not even the game does. This is because memory may contain random values until it is initialized by the game. By providing a range of valid values for health we can allow the program to avoid reading random uninitialized values that are out of range until the game sets them.
The range is also used for different 'when' settings. If the 'when' setting is continuous, the system maps the given range to a 4 bit int range (0-15), and sends that value via serial.

# What is the bit size setting for?

The bit size is to tell the application if it is to read the memory space of a 32 bit or 64 bit game. If the game in question is 64 bit (addresses larger than 4 bytes), the 64 bit size needs to be selected, otherwise 32 should be selected.

# What values send to what ports?

In the single byte sent during an event, a port number is encoded along with the event value. The port number is zero based and is associated with the variable tracking number. Item 1 will send a port value of 0, item 2 a value of 1, item 3 a value of 2 and item 4 a value of 3. In this way you know what item triggered what event via serial.

# Can I run my game fullscreen?

Yes, you can definitely run the game fullscreen without having to alt+tab out to start the program. I designed it so that it constantly looks for the game process in the background and when it finds it, it attaches and reads the value. All you need to have is a valid COM port setup. Simply load your game preset, start serial, then start your game.

# Can I use this for online games?

I would not recommend using this for online games. First, a lot of online games have cheat protection, so you may not even be able to read the memory. Second, those that still allow you to read the memory, may still be triggered by such attempts. Thus, you may be banned for online games for using such a program, as they would think you're attempting to cheat, even though that is not the case.

# I don't know how to compile the code to run it

In this case, this software isn't for you. It is not a commercial application that is ready to go. While others can share the preset files, the actual software is only part of the equation. You also need to have a device at the other end of the serial to parse and act upon it. With that said, you can navigate to the bin/release directory and use the compiled version there. However, it will likely not do you much good as you still need the other portion.

# I have a 32 bit computer, can I run it?

You cannot run this application on a 32 bit machine. It is designed to read 32 bit and 64 bit games. The latter requires that the executable be compiled as a 64 bit application to gain access to the expanded memory space provided by 64 bit games.

# What is the preset file format?

VRFeedback looks for a preset file in a subdirectory of the executable named 'applications'. The file has the following format:
The first line must be:
```
process name|bitsize
```

Every line after must be:
```
memory address/offset|type|range min|range max|offset or address bool (0/1)|when to send|state string (if used, else an empty string works)
```

```
bit size: 0 = 32, 1 = 64
data type: 0 = Int16, 1 = Int32, 2 = Int64, 3 = double float, 4 = single float
When to send: 0 = decrease, 1 = increase, 2 = change, 3 = continuous, 4 = state
```
example:

```
SkyrimSE|1
1E06F50|4|0|1|0|0|
2F54B3C|4|0|1|0|4|1
```

# Legal

I release this software under no license, nothing. You can consider it public domain freeware. Considering the possibilities of mis-use with this software though:

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Likewise I take no responsibility for any physical harm that may take place by whatever feedback mechanism you use. By downloading and using this software you release me (the original author) of any liability whatsoever in any civil court.
