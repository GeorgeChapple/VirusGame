------------------------
DIALOGUE SYNTAX
------------------------
To use dialogue box, write text into a ".txt" file ("New Text Document" option in Windows).
Place this text file into the "text" folder in the unity project.

Text will be read line by line, character by character, as seen in the txt file.

------------------------
MANAGERS
------------------------
A manager is required to control the dialogue box.
These provide the .txt files to be read, and where the dialogue box should store them.
They are also used to control when the dialogue box should start reading, and how its speech bubbles get used.
There is currently [1] manager(s) in this project. More can be produced with different functions at request.

Current Managers
----------------
	ChatBoxManager :
		Designed to look similar to an instant messenger service.
		Places speech bubbles into a scroll rect that the user can then scroll through to read dialogue.
		User presses next button to proceed in dialogue.

		Place desired text to be read and displayed into the serialized field list on the dialogue box UI object.
		This list stores all the text files that the dialogue box could read.
		Ideally, use a prefab with the dialogue box on it, then change all desired settings there and add all desired dialogue you want that prefab to be able to read.
		This will make scripting/sequencing game events much easier.

------------------------
LINE BREAK
------------------------ 
By default, the dialogue box will automatically break after each line in the file.
This can be disabled by writing a line that contains only : <alb:0>
This can be enabled by writing a line that contains only : <alb:1>

A line break can be added by placing the following anywhere in the text : <br>
This is a default feature of TextMeshProUGUI.
It is not recommend to use this, as it displays the first 3 characters until the last '>' is displayed. 
Using auto line break and enabling it/disabling it when needed works much better.

------------------------
SPEED
------------------------
Default speed is one character every 0.05 seconds.
Speed can be changed by writing a line that contains only the following : <sp:X>
X being the speed in seconds that you want the characters to appear in the text box. 
E.G. a line with "<sp:0.1>" will change the speed to one character every 0.1 seconds.
The value placed inside this command is parsed into a float datatype.

------------------------
SPEECH BUBBLES
------------------------
Dialogue is written into a speech bubble.
Prefabs for speech bubbles are stored in a list on the dialogue box script.
These can be used to select visually different speech bubbles, useful for differentiating between what character is talking.

Speech bubbles are instantiated by a line that contains only the following : <n>
This pauses the dialogue, requiring the manager to tell the dialogue to proceed (with something like a next button for example).
When dialogue receives the message to proceed, it will spawn a new speech bubble.

Newly spawned speech bubbles will use the prefab stored in the prefab list at the currently chosen index.
The default index is 0 (the first slot in the list).
The currently selected index/slot can be changed by a line that contains only the following : <pfb:x>
X being the integer value for which you want to set the index to.
E.G a line with "<pfb:2>" will change the prefab used for spawning new speech bubbles to the 3rd prefab stored in the list.

------------------------
PAUSE
------------------------
Dialogue can be paused by writing a line that contains only the following : <p>
This will stop the program from reading the next line until the manager gives the signal to proceed, without needing to spawn a new speech bubble.

------------------------
SYNTAX ERROR MESSAGES
------------------------
Inputting invalid values to the commands will display a syntax error in the text box.
Try not to get confused with any fake error messages we write in these text boxes. ;)

Error Messages
--------------
	General :
		***TEXT BOX SYNTAX ERROR*** - displays when any error is made.
	Speed : 
		~~~SPEED INVALID VALUE~~~ - displays when an invalid value is placed in the text speed command.
	Line Break :
		~~~AUTO LINE BREAK INVALID VALUE~~~  - displays when an invalid value is placed in the auto line break command.
	Prefabs :
		~~~PREFAB INDEX OUT OF BOUNDS~~~ - displays if the input index is out of bounds of the prefab list.
		~~~PREFAB INDEX INVALID VALUE~~~ - displays when an invalid value is placed in the prefab command.