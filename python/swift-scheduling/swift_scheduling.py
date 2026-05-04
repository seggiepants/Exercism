"""
Calculate an actual date given a current date and desired date/time description
"""

import datetime

DATE_FORMAT = '%Y-%m-%dT%H:%M:%S'
MONDAY = 0
TUESDAY = 1
WEDNESDAY = 2
THURSDAY = 3
FRIDAY = 4
SATURDAY = 5
SUNDAY = 6

def date_to_string(value):
    """
    Format a date in the desired format
    :param value: The date to format
    :returns: date formatted as string in the DATE_FORMAT format
    """
    return datetime.datetime.strftime(value, DATE_FORMAT)

def forward_date_to(value, allowed, step=1):
    """
    Move the given date forward (or backward) until you read a
    day of the week that is desirable.
    :param value: The date to start with
    :param allowed: List of day of the week constants 0=Monday through 6=Sunday we should stop at when we encounter them
    :param step: How many days to more forward (or backward) for each iteration.
    :returns value offset until we reach a desirable day of the week.
    """
    while value.weekday() not in allowed:
        value = value + datetime.timedelta(days=step)
    return value

def month_to_quarter(month):
    """
    Convert a 1-12 month number into a 1-4 quarter number
    :param month: Month to convert (number)
    :returns: Matching quarter (1-4)
    """
    return ((month - 1) // 3) + 1

def delivery_date(start, description):
    """
    Given a start date and description figure out the actual deadline desired
    :param start: (string) date in YYYY-MM-DDTHH:NN:SS format
    :param description: (string) description of desired date in relation to given date. (NOW, ASAP, EOW, Q1, 4M, etc.)
    :returns calculated date converted to a string in the same format as the input start date.
    """

    start_date = datetime.datetime.strptime(start, DATE_FORMAT)
    # fixed delivery date
    match description:
        case 'NOW':
            return date_to_string(start_date + datetime.timedelta(hours=2))
        case 'ASAP':
            if start_date.hour < 13:
                return date_to_string(datetime.datetime(start_date.year, start_date.month, start_date.day, 17, 0, 0, 0))
            return date_to_string(datetime.datetime(start_date.year, start_date.month, start_date.day, 13, 0, 0, 0) + datetime.timedelta(days=1))
        case 'EOW':
            end_of_week = datetime.datetime(start_date.year, start_date.month, start_date.day, 0, 0, 0)            
            if start_date.weekday() in [MONDAY, TUESDAY, WEDNESDAY]:
                end_of_week = forward_date_to(end_of_week, [FRIDAY])
                return date_to_string(end_of_week + datetime.timedelta(hours=17))
            end_of_week = forward_date_to(end_of_week, [SUNDAY])
            return date_to_string(end_of_week + datetime.timedelta(hours=20))
    # variable delivery date
    if description.endswith('M'):
        # Monthly
        month = int(description[:-1])
        if start_date.month < month:
            start_of_month = datetime.datetime(start_date.year, month, 1, 8, 0, 0)
        else:
            start_of_month = datetime.datetime(start_date.year + 1, month, 1, 8, 0, 0)

        return date_to_string(forward_date_to(start_of_month, [MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY]))
    
    if description.startswith('Q'):
        # Quarterly
        quarter = int(description[1:])
        current_quarter = month_to_quarter(start_date.month)
        if current_quarter <= quarter:
            current_year = start_date.year
        else:
            current_year = start_date.year + 1
        
        quarter_date = datetime.datetime(current_year, 3 * quarter, 1, 8, 0, 0)
        while quarter_date.month == 3 * quarter:
            quarter_date = quarter_date + datetime.timedelta(days=1)
        quarter_date = quarter_date + datetime.timedelta(days=-1)
        return date_to_string(forward_date_to(quarter_date, [MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY], -1))
