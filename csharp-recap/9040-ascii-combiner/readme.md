# ASCII Combiner

## Introduction

Your job is to write an application that can combine multiple text files with ASCII art in it. Here is an example:

*hg1.txt*:
```txt
+====+
|    |
|    |
|    |
+====+
```

*hg2.txt*:
```txt
      
 (  ) 
  )(  
 (  ) 
      
```

*hg3.txt*:

```txt
      
  ::  
      
  ..  
      
```

Expected result (*hgResult.txt*):
```txt
+====+
|(::)|
| )( |
|(..)|
+====+
```

## Requirements

* Write a *.NET Core 3* Command Line tool called *AsciiArtCombiner*.

* The *logic of combining files* should be encapsulated in a separate *.NET Standard 2.1* class library. The class library *must not* contain code for I/O (e.g. reading files, printing to console).

* The program gets 2..n file names as command line arguments (e.g. `dotnet run file1.txt file2.txt file3.txt`).
  * If the user specifies less than 2 files names, display an error.
  * If a file cannot be processed (e.g. file not found, cannot be opened for reading), display an error message *including* the name of the file that cannot be processed.

* All files have to have the same number of characters per line and the same number of lines. If this is not the case, display an error message.

* Files are in UTF8 format. Line endings can be LF (`\n`) or CRLF (`\r\n`).

* Combine the files as follows:
  * Start with the content of the first file.
  * Write the content of the second file over the content of the first one. Spaces indicate transparency, i.e. they should *not* overwrite content.
  * Repeat previous step with next file.
  * Print the result on the screen (*stdout*).

## Test Data

The folder [*TestData*](TestData) contains files that you can use for testing. Here are some test cases with expected result:

|                     Command                      |                       Expected Result                        |
| ------------------------------------------------ | ------------------------------------------------------------ |
| `dotnet run hg1.txt notexistingfile.txt`         | Display error that *notexistingfile.txt* is invalid          |
| `dotnet run hg1.txt`                             | Display error because only one file specified                |
| `dotnet run`                                     | Display error because no file specified                      |
| `dotnet run`                                     | Display error because no file specified                      |
| `dotnet run hg1.txt hg2.txt hg3.txt`             | ASCII art like *hgResult.txt* is displayed on the screen     |
| `dotnet run smiley1.txt smiley2.txt smiley3.txt` | ASCII art like *smileyResult.txt* is displayed on the screen |
| `dotnet run brokenHeight1.txt brokenHeight2.txt` | Display error because files do not have equal sizes          |
| `dotnet run brokenLength1.txt brokenLength2.txt` | Display error because files do not have equal sizes          |

## Extra Points

Demonstrate the correct combining of files without error handling and you will get one extra point.

Demonstrate correct results for all the test cases above including error handling and you will get two extra points.
