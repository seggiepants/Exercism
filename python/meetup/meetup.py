from datetime import date, timedelta

# Constants for week day string to number returned by date.weekday()
MONDAY = 0
TUESDAY = 1
WEDNESDAY = 2
THURSDAY = 3
FRIDAY = 4
SATURDAY = 5
SUNDAY = 6

def meetup(year, month, week, day_of_week):
    """
    Calculate the date for a meetup. You pass in the month, year, week, and
    day of the week, and the get the desired meetup if possible or a 
    MeetupDayException if it cannot be fullfilled.
    Parameters:
    year: An integer number that is a valid year 1900 - 9999 should work.

    month: A month number integer value betwen 1 (January) and 12 (December)
    
    week: Valid values are first, 1st, second, 2nd, third, 3rd, fourth, 4th,
    fifth, 5th for a date on the first through fith week of the month 
    respectively. Last will give you a date during the last full week of the 
    month. Each week starts on the first, and continues for seven days. It does
    not restart on a Sunday. You also can pass in teenth to get a meeting 
    scheduled for a specific date between the 13th and 19th of the month.
    
    day_of_week: Acceptable values are 'Monday', 'Tuesday', 'Wednesday', 
    'Thursday', 'Friday', 'Saturday', or 'Sunday' (case insensitive).
    """
    one_day = timedelta(days=1)

    # Get the week formatted in lower case without trailing whitespace
    week = week.lower()    
    week = week.strip()

    # Same with the day of the week
    day_of_week = day_of_week.lower()
    day_of_week = day_of_week.strip()

    # Map string to output of weekday() 0 = Monday to Sunday == 6
    day_of_week_map = {
        'monday': MONDAY
        , 'tuesday': TUESDAY
        , 'wednesday': WEDNESDAY
        , 'thursday': THURSDAY
        , 'friday': FRIDAY
        , 'saturday': SATURDAY
        , 'sunday': SUNDAY
    }

    if week == 'first' or week == '1st':        
        day = date(year, month, 1)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day
        return day
    elif week == 'second' or week == '2nd':
        day = date(year, month, 1) + timedelta(weeks=1)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day
        return day
    elif week == 'third' or week == '3rd':
        day = date(year, month, 1) + timedelta(weeks=2)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day
        return day
    elif week == 'fourth' or week == '4th':
        day = date(year, month, 1) + timedelta(weeks=3)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day
        return day
    elif week == 'fifth' or week == '5th':
        day = date(year, month, 1) + timedelta(weeks=4)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day  
        if day.month != month:
            raise MeetupDayException('Falls outside of month')
        else:
            return day
    elif week == 'last':
        if month == 12:
            day = date(year + 1, 1, 1) - timedelta(days=1)
        else:
            day = date(year, month + 1, 1) - timedelta(days=1)

        while day.weekday() != day_of_week_map[day_of_week]:
            day -= one_day
        return day
    elif week == 'teenth':
        day = date(year, month, 13)
        while day.weekday() != day_of_week_map[day_of_week]:
            day += one_day
        return day
    else:
        raise MeetupDayException('week parameter is not an expected value.')

class MeetupDayException(Exception):
    """
    Custom exception class used when we have an error scheduling a day for a
    meetup.
    """
    def __init__(self, message):
        self.message = message
    
    def getMessage(self):
        return self.message
    
    def __str__(self):
        return 'Meetup Day Exception: ' + self.message