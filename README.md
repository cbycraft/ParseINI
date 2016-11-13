# ParseINI
> Parses INI file and returns a dictionary of dictionary indexed by first and second level key values as strings
> Any questions, please contact carlbycraft@gmail.com 

## Namespace: ParseIni
## Class: ParseIni
1. Constructor
    1. public Parser(string fullAbsolutePathToIniFile)
        *Description: Pass the full absolute path to INI file to parse INI file 
    2. public Parser(string[] fileAsStringArray)
        *Description: Pass the file as an array of strings to parse INI file

2. Returned Parse Type
    1. public Dictionary<string, Dictionary<string, string> IniFileDictionary
        * Description: Once the file is parsed, primary groups will be dictionary keys.

##Grammar rules
1. Whitespace
    1. White space is defined as the space character " " and tab character "\t"

2. Comments
    1. Comments are denoted by a semicolon (;) as the first character on a line.
    2. No inline comments

3. File start
    1. The first non-white space or comment character needs to be an open square brace "[".

4. Primary keys
    1. Primary keys can contain white space to seperate words, but no white space after and before "[" and "]" characters containing the key.
    2. White space before first "[" on line for primary key is ignored; however, it's considered good practice to start every primary key line with the "[" character rather than white space.
    
5. Secondary keys:
    1. Secondary keys can contain white space to seperate works, but all training white space before equals sign will be considered part of secondary key.
    2. Leading white space before secondary key will be ignored, but it's good practice to start the secondary key line with a string.
    
6. Secondary key values:
    1. All secondary key values will be read from equal sign to end of line. Any character besides end of line will be added to the string. Once end of line is hit, the buffer is loaded as the value.
    
## Example INI file (https://en.wikipedia.org/wiki/INI_file)
> ; last modified 1 April 2001 by John Doe
> [owner]
> name=John Doe
> organization=Acme Widgets Inc.
>
> [database]
> ; use IP address in case network name resolution is not working
> server=192.0.2.62     
> port=143
> file="payroll.dat"
