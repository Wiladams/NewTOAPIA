using System;
using System.Text;
using System.Collections;

using TOAPI.Kernel32;

class Environment
{
    const int MAX_ENV_VALUE_LENGTH = 32767;

    // GetEnvironementStrings
    public Environment()
    {
    }

    public string GetVariableValue(string aKey)
    {
        StringBuilder builder = new StringBuilder(MAX_ENV_VALUE_LENGTH);
        int aSize = MAX_ENV_VALUE_LENGTH;
        int charsRead = 0;

        // Try to get the value.
        charsRead = Kernel32.GetEnvironmentVariable(aKey, builder, aSize);

        // Throw argument out of range exception if it does not exist
        if (0 == charsRead)
            throw new ArgumentOutOfRangeException(aKey);

        return builder.ToString();
    }

    public string CommandLine
    {
        get { return Kernel32.GetCommandLine(); }
    }
}

