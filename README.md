<div align="center">
<p>
    <img width="80" src="https://raw.githubusercontent.com/vpp-lang/vppi/main/vppicon.png">
</p>

# V++ Interpreter (vppi)
<a><img src="https://img.shields.io/maintenance/yes/2021"></a>
<a href="https://github.com/VMGP/vppi/releases"><img src="https://img.shields.io/github/v/release/vpp-lang/vppi?label=latest%20stable%20release"></a>
<a href="https://github.com/VMGP/vppi/releases"><img src=""></a>
<a href="https://en.wikipedia.org/wiki/Visual_Basic_.NET"><img src="https://img.shields.io/badge/language-Visual%20Basic%20.NET-blue"></a>
<a href=""><img src="https://img.shields.io/badge/Platform-Windows-blue"></a>

</div>

## Introduction
V++ is a small static-typed interpreted language made for running small programs in a console window.

This is the V++ interpreter from the V++ programming language package. In order to install the V++ Runtime Environment you must [check releases](https://github.com/vpp-lang/vppi/releases). For documentation [check the V++ wiki](https://github.com/vpp-lang/vppi/wiki).


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

# History and important events
## 2017
The first V++ interpreter was made in 2017, but it was very unstable and didn't have an official name. The syntax was strange. (```printline Hello world!```).
## 2018
Nothing interesting happened, just decided to remake V++ from scratch, which resulted in today's V++.
## 2019
Made a lot of progress, the syntax of V++ in 2019 is very similar to the one in ```1.0.0.0```. 
## 2020
Released V++ ```1.0.0.0``` on 19 Nov 2020.
## 2021
First prerelease of V++.
Dropped support for V++ 1.0.0.0.

# Bugs, feauture requests and discussions
To report bugs or request new feautures go to the Issues tab or [click here](https://github.com/vpp-lang/vppi/issues).

For discussions/requesting a library/dependecy for the V++ package manager go to the discussions tab or [click here](https://github.com/vpp-lang/vppi/discussions).
