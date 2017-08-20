# Design Patterns
This repo contains examples of common OOP design patterns.

## Creational Patterns

### Simple Factory

The program will produce an array of random numbers after the user chooses the size of the array. Then, the program gives the user a choice of various sort methods. After the user chooses a method, the program uses the *simple factory* pattern to create an instance of a class that sorts the array according to the method that the user chose. Output shows how long it took to sort the array.

### Builder

Choose a character from among several interesting options, and the program will build and describe it. You can create your own character too--simply make a class that implements `AbstractCharacterBuilder`.

### Prototype

This is a simple implementation of the prototype pattern. Choose a website from among a list of several classic sites, and the `WebPageExplorer` class, which implements .NET `HttpClient`, will return interesting information about that site! The client operates by creating a list of instances of `WebPageExplorer` by cloning an initial instance rather than constructing new instances.