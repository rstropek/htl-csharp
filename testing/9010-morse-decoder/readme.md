# "Morse Decoder" Exercise

## Introduction

Your job is to develop a library that can read and decode text written with the [Morse code](https://en.wikipedia.org/wiki/Morse_code). You have to verify the functionality of your library using unit tests.

## Specification

### Morse Code Representation

In input texts for your library, ...
   * ...dots (`.`) represent *Morse dots*,
   * ...dashes (`-`) represent *Morse dashes*,
   * ...letters are separated using single blanks (` `),
   * ...words are separated using four blanks (`    `).

Here is an example text representing *HELLO WORLD*:

```
.... . .-.. .-.. ---    .-- --- .-. .-.. -..
```

### Character Set

Your library has to understand all letters and digits mentioned [here](https://en.wikipedia.org/wiki/Morse_code#/media/File:International_Morse_Code.svg).

## Error Handling

If a text input

* contains letters/digits other than [A..Z] and [0..9] or
* contains unknown Morse code sequences or
* contains any special characters other than blanks

your library should indicate an error (e.g. throw an exception, print an error message to *stderr*).

## Size of Input Texts

You library must be able to process even very large input texts with Morse code. You *cannot* assume that the entire input and/or output text fits into memory all at once.

## Code to Write

### Programming Language

You have to write the code in C# using .NET Core and/or .NET Standard.

### Library

You have to design and implement a library that takes a text in Morse code as an input and provides the decoded text in ASCII. It is up to you to decide what data types you use for the input and output. A simple `string` is not sufficient because input and output texts can be so large that they don't fit into memory.

### Command Line Tool

You have to offer a command-line tool using the library. The command line tool takes two arguments:

* Name of the input file containing Morse code. Your program will read this file.
* Name of the output file containing ASCII text. Your program will create and write to this file.

### Tests

You have to write at least *five* unit tests that checks that your program implements the requirements in this document correctly.

## Test Data

```
ABC DEF GHI JKL MNO PQR STU VWX YZ
.- -... -.-.    -.. . ..-.    --. .... ..    .--- -.- .-..    -- -. ---    .--. --.- .-.    ... - ..-    ...- .-- -..-    -.-- --..
```

```
012 345 678 9
.---- ..---    ...-- ....- .....    -.... --... ---..    ----.
```

```
THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG
- .... .    --.- ..- .. -.-. -.-    -... .-. --- .-- -.    ..-. --- -..-    .--- ..- -- .--. ...    --- ...- . .-.    - .... .    .-.. .- --.. -.--    -.. --- --.
```
