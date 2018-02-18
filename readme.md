# Title - Avinode Coding Exercise

#### Short Description
A console application that takes an xml document, and a string representing an active path name, and returns a menu, with the active menu marked as such.

#### Author
Charles Ewel

## Technologies Used

* .NET
* C#

## Instructions

* Clone the repository or download it from github
* In powershell, navigate to the project directory
* Type: .\AvinodeCodeTest.exe [xml file location] [active path name] and hit enter

Where [xml file location] is the location of the xml file on your computer. There is a copy of "SchedAero Menu.txt" and "Wyvern Menu.txt" in the project directory, so relative pathing can be used to reference those or absolute pathing can be used to reference xml elsewhere on the computer. [active path name] should be the name of the path that you want to display as active in the menu. Both [xml file location] and [active path name] should be in quotes.

Example:

Running (from the project directory),

.\AvinodeCodeTest.exe ".\SchedAero Menu.txt" "/Requests/OpenQuotes.aspx"

will return a menu with the open quotes option set as active.

## Known Bugs

None

## Contacts

Charles.Ewel@gmail.com
