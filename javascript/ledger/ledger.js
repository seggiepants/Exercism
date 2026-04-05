// Change Log:
// * Modified Ledger Entry constructor to take date, descritpion, and change that way it is a one-liner
//   and not a four liner. Later date format fixing added some lines back in.
// * Wanted to remove function createEntry as it is the same amount of code to just make a new LedgerEntry.
//   Unfortunately, the test cases need it.
// * Added T00:00 to date values when needed so they would parse correctly when parsed during new Date(date)
//   how did this work without that?
// * formatEntries move header to top pulls locale specific info from a locale dictionary
// * looks like formatEntries sort function is the same either way, move that out. 
//   I put it in ledgers dictionary so that other locales may sort differently if added.
// * Change date formatting custom code to use .toLocaleDateString() with an options object to get two digt month/year
// * Change currency format options to also pull from the locales dictionary. Didn't work, moved to a currency dictionary
//   that still didn't work either so, both locale and currency for a key. Went for both locale and currency as
//   new keys, and finally got that to work. Not sure if NL-nl EUR should have parenthesized negative number or not (No test for that).
// * With all that out of the way we don't need to have code specific to locale in the write records loop so it was
//   simplified to one loop for everything. Trimming for negative numbers did get strange.


class LedgerEntry {
  constructor(date, description, change) {
    this.date = date;
    this.description = description;
    this.change = change;
  }
}

export const createEntry = (date, description, change) => {  
  let dateStr = date
  if (date.length <= 10 && date.indexOf('T') < 0) 
    dateStr += 'T00:00'
  return new LedgerEntry(new Date(dateStr), description, change)
}

let ledgerEntrySort = (a, b) => {
  return a.date - b.date ||
    a.change - b.change ||
    a.description.localeCompare(b.description)
}

const currencies = {
  'en-US_USD': {
    style: 'currency',
    currency: 'USD',
    currencyDisplay: 'narrowSymbol',
    currencySign: 'accounting',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  },
  'en-US_EUR': {
    style: 'currency',
    currency: 'EUR',
    currencyDisplay: 'narrowSymbol',
    currencySign: 'accounting',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  },
  'nl-NL_USD': {
    style: 'currency',
    currency: 'USD',
    currencyDisplay: 'narrowSymbol',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  },
  'nl-NL_EUR': {
    style: 'currency',
    currency: 'EUR',
    currencyDisplay: 'narrowSymbol',
    currencySign: 'accounting',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }
}

const locales= {
  'en-US': {
    locale: 'en-US', 
    date: 'Date', 
    description: 'Description', 
    change: 'Change', 
    sort: ledgerEntrySort, 
  },
  'nl-NL': {
    locale: 'nl-NL', 
    date: 'Datum', 
    description: 'Omschrijving', 
    change: 'Verandering', 
    sort: ledgerEntrySort,
  }
}

export function formatEntries(currency, locale, entries) {
  if (!(locale in locales))
  {
    throw new Error(`Locale: ${locale} is not supported.`)
  }

  let currencyKey = `${locale}_${currency}` 
  if (!(currencyKey in currencies))
  {
    throw new Error(`Locale: ${locale}, and Currency: ${currency} combination is not supported.`)
  }
      
  let table = '';
  // Generate Header Row
  table += `${locales[locale].date.padEnd(10, ' ')} | ${locales[locale].description.padEnd(25, ' ')} | ${locales[locale].change.padEnd(13, ' ')}\n`  

  // Sort entries
  entries.sort(locales[locale].sort)
  
  let dateFormatOptions = { year: 'numeric', day: '2-digit', month: '2-digit'}
  entries.forEach((entry) => {
    // Write entry date to table
    const dateStr = entry.date.toLocaleDateString(locale, dateFormatOptions)
    table += `${dateStr} | `;

    // Write entry description to table
    const truncatedDescription =
      entry.description.length > 25
        ? `${entry.description.substring(0, 22)}...`
        : entry.description.padEnd(25, ' ');
    table += `${truncatedDescription} | `;

    // Write entry change to table
    
    let shouldTrim = entry.change < 0 && 
          currencies[currencyKey].currencySign === 'accounting'
    let changeStr = `${(entry.change / 100).toLocaleString(
          locale,
          currencies[currencyKey],
        )} `;
    if (shouldTrim)
        changeStr = changeStr.trim()
    table += changeStr.padStart(13, ' ');
    table += '\n';
  });
  
  return table.replace(/\n$/, '');
}
