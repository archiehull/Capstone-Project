# 441101-2223-capstone-project

"Menu.cs" creates the framework used in "ProjectMenu.cs" for the menu system. The "Console Helpers" class contains "GetIngeterInRange", which is one of many measures in place that prevents the program from crashing and provides the user with feedback on their input. It also allows code to be reused instead of repeated when creating new menu items. They are both static, which prevents inheritance or instantiation and demonstrates effective use of encapsulation.

The abstract class "MenuItem" creates the methods "MenuText" and "Select", which means that in order to create a valid instance of "MenuItem", those methods must be inherited and overwritten. Polymorphism can be seen throughout the project through the overwriting for these virtual methods.

"ConsoleMenu" is an abstract class that inherits from "MenuItem", which creates a protected list of menu items and provides the navigation functionality of the menu.

"Project.cs" contains the "Project Manager" class, and within that, the "Projects" list. For the purpose of this exercise, I deemed it appropriate for "Projects" to be a singleton, as in order for all of the projects created and loaded by the user to be in one place, they would have to refer to the same list. Making "Projects" a singleton could raise potential issues in the program's functionality outside of its intended use for this exercise, for example,  the program can only be used for a single portfolio.

Comments can be seen throughout the project, detailing functionality of classes, methods and functions.

Source control was used locally and not through GitHub, due to this being an independant project and not needing to share progress. In retrospect, GitHub would have provided a distinct indicator of progress for the examining board to follow and the failure to do so was an oversite.

User is able to successfully load files from TXT and TML formats and is met with an appropriate error message if the contents of the file is inapropriate. The TML loader has room for improvement as when the TypeCode of the project is being determined, the loader will only check if the first letter is a match and not the rest of the word. This was done as an alternative to splitting the string up into an array and accessing the data using the array.

The project is able to display a list of all sales, display a list of all purchases, display whether it is a new build or a renovation, display a summary of all transactions, display the applicable tax refund amount, add a new transaction, display a list of all projects, display a summary of all projects, display an overall summary, select a specific project, add a new project and remove an existing project.


