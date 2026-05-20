"""
Ledger Printer

Change Log:
* Add comments that the test suite will complain about if missing.
* Want to merge LedgerEntry.__init__, and create_entry. Can't do that, the test suite needs it. Still moved code into the constructor.
* Reduced header line in format_entries to a single line of code.
* Write date is now a one liner
* Write description is now a one liner
* Currency required the locale library and a special function but it is shorter now. The formatting doesn't match what the locale expects, it is off and that is what required a custom function.
* Both locales handled in one branch removing duplicated code.
* Sort the entries and iterate over them instead of finding the minimum value and popping each time.
* Exercism is broken and doesn't behave the same locally and on their server. Had to remove the locale library and roll my own thing even more.
"""
from datetime import datetime

# Currency code to symbol
# Could add more currencies this way
currency_lookup = {
    'USD': '$',
    'EUR': '€',
}
language_lookup = {'en_US':  {
        'Date': 'Date',
        'Description': 'Description',
        'Change': 'Change',
        'DateFormat': '%m/%d/%Y',
        'Decimal': '.',
        'Thousands': ','
    },
    'nl_NL': {
        'Date': 'Datum',
        'Description': 'Omschrijving',
        'Change': 'Verandering',
        'DateFormat': '%d-%m-%Y',
        'Decimal': ',', 
        'Thousands': '.'
    }
}


class LedgerEntry:
    """
    Ledger Entry class (describes on ledger entry)
    This one is going to throw a too few methods error.
    """
    def __init__(self, date, description, change):
        """
        Initialze Ledger Entry to default values
        :param date: Date of the entry
        :param description: Description of the entry
        :param change: monetary amount
        """
        self.date = datetime.strptime(date, '%Y-%m-%d')
        self.description = description
        self.change = change


def create_entry(date, description, change):
    """Create a LedgerEntry object with the given properties
    :param date: Date of the transaction
    :param description: Description of the transaction
    :change: Amount of the transaction (implied 2 digit decimal)
    """
    return LedgerEntry(date, description,change)

def format_currency(currency, currency_locale, amount):
    """Format Currency for the desired locale and currency
    :param currency: Currency of the transaction USD or EUR
    :param currency_locale: Locale of the transaction en_US or nl_NL
    :param amount: How much the transaction was for
    :returns: The currency amount formatted properly.
    """
    # write the number as a string for the desired locale 
    # will add () for negative numbers if en_US so give it ABS() if en_US locale. Don't want two negative indicators after all.
    symbol = currency_lookup[currency]
    symbol = (symbol if currency_locale == 'en_US' else symbol + ' ')
    value = f"{symbol}{abs(amount) if currency_locale == 'en_US' else amount:_.2f}"
    value = value.replace('.', language_lookup[currency_locale]['Decimal']).replace('_', language_lookup[currency_locale]['Thousands'])
    # Negative numbers get parenthesis in the US. If no parenthesis add an extra space so everything lines up
    if amount < 0 and currency_locale == 'en_US':
        value = '(' + value + ')'
    else:
        value += ' '
    return value


def format_entries(currency, current_locale, entries):
    """
    Write out the entries in the list as a formatted table.
    :param currency: Monetary currency 'USD' or 'EUR'
    :param current_locale: Locale of the transaction 'en_US' or 'nl_NL'
    :entries: List of LedgerEntry objects
    :returns: string with the entries nicely formatted in a table.
    """

    # Generate Header Row
    table = language_lookup[current_locale]['Date'].ljust(11) +  '| ' + \
    language_lookup[current_locale]['Description'].ljust(26) +  '| ' + \
    language_lookup[current_locale]['Change'].ljust(13)

    sorted_entries = sorted(entries, key=lambda x: (x.date, x.change, x.description))

    for entry in sorted_entries:
        table += '\n'
        # Write entry date to table
        table += entry.date.strftime(language_lookup[current_locale]['DateFormat']) 
        table += ' | '

        # Write entry description to table
        # Truncate if necessary
        table += entry.description[:22] + '...' if len(entry.description) > 25 else entry.description.ljust(25)
        table += ' | '

        # Write entry change to table
        table += format_currency(currency, current_locale, entry.change / 100).rjust(13)
    return table
