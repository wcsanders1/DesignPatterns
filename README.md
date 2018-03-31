# Design Patterns
This repo contains examples of common OOP design patterns.

## Creational Patterns

### Builder

Choose a character from among several interesting options. The **builder pattern** is used to build a class with the attributes corresponding to the character you chose. You can create your own character making a class that implements `AbstractCharacterBuilder`.

### Prototype

Choose a website from among a list of several classic sites, and the `WebPageExplorer` class, which implements .NET `HttpClient`, will return interesting information about that site. Pursuant to the **prototype pattern**, The client operates by creating a list of instances of `WebPageExplorer` by cloning an initial instance rather than constructing new instances.

### Simple Factory

The program will produce an array of random numbers after the user chooses the size of the array. Then, the program gives the user a choice of various sort methods. After the user chooses a method, the program uses the **simple factory** pattern to create an instance of a class that sorts the array according to the method that the user chose. Output shows how long it took to sort the array.

### Singleton

Did you come here for an argument? Good. Choose from a variety of topics on which to argue. If you get tired of arguing a certain topic, you can switch to a different topic. Every topic is a class registered with an IoC container which provides a only a single instance of each, which is in accord with the **singleton pattern**.

## Structural Patterns

### Adapter

After making your way through some questions about yourself, the program will render a report regarding your answers. The report can render either as a simple list of the questions and the answers you provided, or it can show you your incorrect answers along with a total of your score. The **adapter pattern** allows render methods of various signatures to use the functionality that another render method provides.

### Bridge

Convert measurements from one type to another and print the output in either a simple or fancy style. The **bridge pattern** allows the conversion output to be formatted in various ways; i.e., any formatter that implements the `IFormatter` interface can be passed to any conversion class, which will format the output using the formatter.

### Composite

Determine the distribution of a decedent's estate according to the *per stirpes* distribution scheme. The **composite pattern** allows the decedent to contain a collection of descendants, and each descendant to collection of their own descendants. In addition, the information will print to the console in a neat tree-like fashion:

![Composite gif](/../screenshots/Composite_GIF_1.gif)

### Decorator

Tell the program what town you're in, and you'll receive information about that town. If you want a broader perspective on things, the **decorator pattern** will wrap the town object in a decorator object that provides further information about the town. And if you want an even broader perspetive, you can keep wrapping the town in decorators until your perspective is all-encompassing...

### Facade

Construct xml or json, and the program will print it to the console, convert it, then print the converted data to the console. In other words, if the user chooses to create xml, the program will take the user through the process of creating xml, print the xml to the console, convert the xml to json, then print the json to the console. The **facade pattern** simplifies the interaction with the functionality in the xml and json libraries.

### Flyweight

Provide foreground and background colors for characters if you want. Then, type a string that you want printed to the console. Any characters in the string for which you provided custom colors will be printed with the colors you chose. (Also, the string will be printed in a descending-stair fashion, which is pretty neat!) The **flyweight pattern** allows classes representing characters with custom colors to be shared. The character's colors are part of the *internal state* of the class, and the character's y-position is its *external state*.

### Proxy

Choose a drive and file extension, and the program will recursively search the drive for files with the chosen extension. The **proxy pattern** allows the client to call a proxy class rather than the class that actually provides search functionality. The proxy class displays helpful information to the user, such as the percentage of files and bytes searched, and a status bar.

![Proxy gif](/../screenshots/Proxy_GIF_1.gif)

## Behavioral Patterns

### Chain of Responsibility

The clergymen are available to answer your questions. Which member of the clergy will answer depends on the philosophical depth of the question asked. The **chain of responsibility pattern** makes it easy for the question to be passed on to the clergyman in the next highest order in the church heirarchy if the current clergyman is unable to answer it.

### Command

A variety of interesting items are available to order. Choose items from a list and add them to an order, and remove later if you want. The **command pattern** makes it easy to add functionality to this simple application; just add a class implementing `ICommand` and the functionality it provides will automatically be available to the user.

### Interpreter

Enter a mathematical expression and get the correct answer, or get an error message if your expression is malformed. The **interpreter pattern** is used to take the mathematical expression, which is provided as a `string`, and convert it into a series of mathematical operations, which are then resolved.

![Interpreter gif](/../screenshots/Interpreter_GIF_1.gif)

### Iterator

This is a simple program that allows the user to enter strings onto a custom stack object, which uses the **iterator pattern** to allow `foreach` to be called on the stack. When iterating the stack, the values are popped and written to the console.