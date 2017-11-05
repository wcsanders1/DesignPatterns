# Design Patterns
This repo contains examples of common OOP design patterns.

## Creational Patterns

### Builder

Choose a character from among several interesting options, and the program will build and describe it. You can create your own character too--simply make a class that implements `AbstractCharacterBuilder`.

### Prototype

This is a simple implementation of the prototype pattern. Choose a website from among a list of several classic sites, and the `WebPageExplorer` class, which implements .NET `HttpClient`, will return interesting information about that site! The client operates by creating a list of instances of `WebPageExplorer` by cloning an initial instance rather than constructing new instances.

### Simple Factory

The program will produce an array of random numbers after the user chooses the size of the array. Then, the program gives the user a choice of various sort methods. After the user chooses a method, the program uses the *simple factory* pattern to create an instance of a class that sorts the array according to the method that the user chose. Output shows how long it took to sort the array.

### Singleton

Did you come here for an argument? Good. Choose from a variety of topics on which to argue. If you get tired of arguing a certain topic, you can switch to a different topic. Every topic is a class registered with an IoC container which provides a only a single instance of each.

## Structural Patterns

### Adapter

After making your way through some questions about yourself, the program will render a report regarding your answers. The report can render either as a simple list of the questions and the answers you provided, or it can show you your incorrect answers along with a total of your score. The adapter pattern is allows render methods of various signatures to use the functionality that another render method provides.

### Bridge

Convert measurements from one type to another and print the output in either a simple or fancy style. The bridge pattern allows the conversion output to be formatted in various ways; i.e., any formatter that implements the `IFormatter` interface can be passed to any conversion class, which will format the output using the formatter.

### Composite

Determine the distribution of a decedent's estate according to the *per stirpes* distribution scheme. The composite pattern allows the decedent to contain a collection of descendants, and each descendant to collection of their own descendants. In addition, the information will print to the console in a neat tree-like fashion. (/../screenshots/Composite_SS_1.PNG)