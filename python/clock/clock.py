class Clock(object):
    def __init__(self, hour, minute):
        self.hour = 0
        self.minute = minute
        while self.minute >= 60:
            self.hour += 1
            self.minute -= 60
        
        while self.minute < 0:
            self.hour -= 1
            self.minute += 60
        
        self.hour += hour
        while self.hour >= 24:
            self.hour -= 24
        
        while self.hour < 0:
            self.hour += 24
        
    def __repr__(self):
        return "%02d" % self.hour + ":" + "%02d" % self.minute

    def __eq__(self, other):
        return (self.hour == other.hour and self.minute == other.minute)

    def __add__(self, minutes):
        return Clock(self.hour, self.minute + minutes)
        
    def __sub__(self, minutes):
        return Clock(self.hour, self.minute - minutes)
