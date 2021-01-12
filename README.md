# Introduction
This is the V++ interpreter from the V++ programming language package. In order to install the V++ Runtime Environment you must [check releases](https://github.com/VMGP/vppi/releases). For documentation [check the V++ wiki](https://github.com/VMGP/vppi/wiki).

In case you want to create V++ scripts and need syntax highlighting, you can [install the official V++ VSCode extension](https://marketplace.visualstudio.com/items?itemName=VMGPOfficial.vpp)

## Sample code
```

function test ()
	command (0x0006,null)
	exit
end function

function main ()
	command (0x0001,"I am a title!",null)
	command (0x0008,"I am a message box!",null)
	test ()
end function
```

# How does it work
## Loading a script
When you load a script a new interpreter instance that runs in parallel with the other instances. When a script is loaded by the user/another app via a command line call, there is created a master interpreter that can log data and is the main script. When a script is loaded by main script/master interpreter via the @Include keyword there is created a slave interpreter which works the same as a master interpreter but can be ordered to execute a function or get/set a value.


After the loading of the script the interpreter (either master or slave) will enter the setup phase, where setup keywords like ```@EntryPoint```, ```@Include``` and ```function```. The setup phase defines functions and sets things up before the runtime phase. After the setup finishes, the interpreter enters in the runtime phase, where the script is executed.

# History
## 2017
The first V++ interpreter was made in 2017, but it was very unstable and didn't have an official name. The syntax was strange. (```printline Hello world!```).
## 2018
Nothing interesting happened, just decided to remake V++ from scratch, which resulted in today's V++.
## 2019
Made a lot of progress, the syntax of V++ in 2019 is very similar to the one in ```1.0.0.0```. 
## 2020
Released V++ ```1.0.0.0``` on 19 Nov 2020.
