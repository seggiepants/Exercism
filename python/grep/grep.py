"""
Limited subset of the grep command
Search for a string in a set of files. 
Optional flags include:
-n Prepend the line number and a colon (':') to each line in the output, placing the 
   number after the filename (if present).
-l Output only the names of the files that contain at least one matching line.
-i Match using a case-insensitive comparison.
-v Invert the program -- collect all lines that fail to match.
-x Search only for lines where the search string matches the entire line.
"""

def grep(pattern, flags, files):
    """
    Search a set of files for a given pattern. Behavior varies depending on the given flags.
    -n Prepend the line number and a colon (':') to each line in the output, placing the 
       number after the filename (if present).
    -l Output only the names of the files that contain at least one matching line.
    -i Match using a case-insensitive comparison.
    -v Invert the program -- collect all lines that fail to match.
    -x Search only for lines where the search string matches the entire line.
    :param pattern: The text to search for. New line character implied for full line match.
    :param flags: space separated list of flags -l, -i, -v, or -v you can have zero, one, or many.
    :param files: List of strings of filenames to search.
    :returns: matching lines or filenames as a string lines separated by new line character.
    """
    options = parse_flags(flags)
    multiple_files = len(files) > 1
    buffer = []

    for file in files:
        result = file_search(file, pattern, options, multiple_files, buffer)
        if result and options['-l']:
            buffer.append(file + '\n')
    
    return ''.join(buffer)
    

def file_search(file_name, pattern, flags, multiple_files, buffer):
    """
    Search a file for a given text pattern.
    :param file_name: the path and filename of the file to search
    :param pattern: the string to search for (case insensitive if -i is set)
    :param flags: dictonary of flag values 
    :param multiple_files: true if we are searching multiple files. If so prepend filename.
    :param buffer: write text to this
    :returns: True if the filename matched at least one line. Unless -v is set 
    in which case it is only True if at least one line did not match.
    """
    ret = False
    with open(file_name, 'rt', encoding='utf-8') as file_handle:
        for line_number, line in enumerate(file_handle):
            if flags['-x']: # Match whole line
                if flags['-i']: # Case insensitive
                    found = line.lower() == pattern.lower() + '\n'
                else:
                    found = line == pattern + '\n'
                if found:
                    index = 0
                else:
                    index = -1
            else:
                if flags['-i']: # Case insensitive                
                    index = line.lower().find(pattern.lower())
                else:
                    index = line.find(pattern)
            
            if (flags['-v'] and index == -1) or (not flags['-v'] and index >= 0):
                ret = True
                if not flags['-l']: # filenames only
                    prefix = file_name + ':' if multiple_files else ''
                    prefix = prefix + str(line_number + 1) + ':' if flags['-n'] else prefix
                    buffer.append(prefix + line)
    return ret

def parse_flags(flags):
    """
    Parse the flags string for supported flags.
    :param flags: space separated flags
    :returns: dictionary of supported flags set to true/false if found or not.
    """
    ret = {
        '-n': False,
        '-l': False,
        '-i': False,
        '-v': False,
        '-x': False,
    }

    for flag in flags.split(' '):
        flag_normalized = flag.lower().strip()
        if len(flag_normalized) == 0:
            continue

        if flag_normalized in ret:
            ret[flag_normalized] = True
        else:
            raise ValueError(f'Unsupported flag: \'{flag}\'.')

    return ret