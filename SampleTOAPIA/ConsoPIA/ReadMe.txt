ConsoPIA

This sample application demonstrates how to utilize the various
Console APIs found in the kernel32.dll library.

The Terminal class has methods that are very similar to what is
to be found in the System.Console class for reading and writing
information from and to a console

Through the TOAPI.Kernel32 interop assembly, there is full control off
all the console APIs.  The Terminal class makes accessing them a little
bit easier in a .net class supported way.

For the most part, the Terminal class mimics the Console class feature
by feature.  The main difference is that it is instance based, rather than
having static methods.  This is a bit more flexible in that you can pass
the object around to other processes that might want to write into 
the console.  The drawback is that it's a bit harder to get at the console
because you need an instance object laying around.

You can use both the Terminal class, and the standard System.Console class
at the same time as they are both interacting with the standard input/output
streams associated with the process.

