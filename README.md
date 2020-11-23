# vppi
This is the V++ interpreter from the V++ programming language package. In order to install the V++ Runtime Environment you must [check releases](https://github.com/VMGP/vppi/releases). For documentation please [check the V++ wiki](https://github.com/VMGP/vppi/wiki).

In case you want to create V++ scripts and need syntax highlighting, you can [install the official V++ VSCode extension](https://marketplace.visualstudio.com/items?itemName=VMGPOfficial.vpp)

## Sample code
```
function test ()
	command (0x0007,"Press any key to continue...")
	exit
end function

function main ()
	command (0x0001,"I am a title!")
	command (0x0008,"I am a message box!")
	test ()
end function
```
