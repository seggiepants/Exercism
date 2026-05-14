"""
The class Scale will generate note scales based on a given tonic
"""

NATURAL	= ['C', 'a']
SHARP	= ['G', 'D', 'A', 'E', 'B', 'F#', 'e', 'b', 'f#', 'c#', 'g#', 'd#']
FLAT	= ['F', 'Bb', 'Eb', 'Ab', 'Db', 'Gb', 'd', 'g', 'c', 'f', 'bb', 'eb']
CHROMATIC_SHARP = ['A', 'A#', 'B', 'C', 'C#', 'D', 'D#', 'E', 'F', 'F#', 'G', 'G#']
CHROMATIC_FLAT = ['A', 'Bb', 'B', 'C', 'Db', 'D', 'Eb', 'E', 'F', 'Gb', 'G', 'Ab']

class Scale:
    """
    Class to generate note scales
    """
    def __init__(self, tonic):
        """
        Initialize the class.
        :param tonic: tonic to generate a scale from
        """
        self.tonic = tonic

    def chromatic(self):
        """
        Generate a chromatic scale based on the class member tonic
        :returns: list of notes as strings
        """
        use_sharp = self.tonic in SHARP or self.tonic in NATURAL
        if use_sharp:
            notes = CHROMATIC_SHARP
        else:
            notes = CHROMATIC_FLAT        
        note_index = notes.index(self.tonic.title())
        return notes[note_index:] + notes[:note_index]


    def interval(self, intervals):
        """
        Generate a note scale starting at the class variable tonic and proceeding
        with wrap around based on the given intervals (m, M, and A supported)
        :param intervals: string of intervals m = half, M = whole, A = one and a half.
        :returns: list of notes as strings for the scale produced.
        """
        notes = self.chromatic()
        count_notes = len(notes)
        note_index = notes.index(self.tonic.title())
        ret = [self.tonic.title()]
        for interval in intervals:
            if interval == 'm':
                note_index += 1
            elif interval == 'M':
                note_index += 2
            elif interval == 'A':
                note_index += 3
            else:
                raise ValueError(f'Unsupported interval \'{interval}\'.')
            ret.append(notes[note_index % count_notes])            

        return ret
