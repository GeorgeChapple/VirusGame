------------------------
DIALOGUE SYNTAX
------------------------
To use dialogue box, write text into a ".txt" file ("New Text Document" option in Windows).
Place this text file into the "text" folder in the unity project.
Place desired text to be read and displayed into the serialized field on the dialogue box UI object.
Text will be read line by line, character by character, as seen in the txt file.
For examples on the following commands, use "testFile.txt"

------------------------
LINE BREAK
------------------------ 
By defualt, the dialogue box will automatically break after each line in the file.
This can be disabled by writing a line that contains only : <alb:0>
This can be enabled by writing a line that contains only : <alb:1>

A line break can be added by placing the following anywhere in the text : <br>
This is a defualt feature of TextMeshProUGUI.
I would not recommend using this, as it displays the first 3 characters until the last '>' is displayed. 
Using auto line break and enabling it/disabling it when needed works much better.

------------------------
SPEED
------------------------
Default speed is one character every 0.001 seconds.
Speed can be changed by writing a line that contains only the following : <sp:X>
X being the speed in seconds that you want the characters to appear in the text box. 
E.G. a line with "<sp:0.1>" will change the speed to one character every 0.1 seconds.
The value placed inside this command is parsed into a float datatype.

------------------------
PAUSE
------------------------
Dialogue can be paused by writing a line that contains only the following : <p>
This will stop the program from reading the next line until instructed to.
For now, the boolean "forceSkip" unpauses the dialogue.

------------------------
SYNTAX ERROR MESSAGES
------------------------
Inputing invalid values to the commands will display a syntax error in the text box.
Try not to get confused with any fake error messages we write in these text boxes. ;)
These are the error messages : 

***TEXT BOX SYNTAX ERROR*** - displays when any error is made.
~~~SPEED INVALID VALUE~~~ - displays when an invalid value is placed in the text speed command.
~~~AUTO LINE BREAK INVALID VALUE~~~ displays when an invalid value is placed in the auto line break command.



More features coming soon - George