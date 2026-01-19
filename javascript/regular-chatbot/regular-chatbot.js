// @ts-check

/**
 * Given a certain command, help the chatbot recognize whether the command is valid or not.
 *
 * @param {string} command
 * @returns {boolean} whether or not is the command valid
 */

export function isValidCommand(command) {
  let re = /^chatbot/i;
  return re.test(command)
}

/**
 * Given a certain message, help the chatbot get rid of all the emoji's encryption through the message.
 *
 * @param {string} message
 * @returns {string} The message without the emojis encryption
 */
export function removeEmoji(message) {
  let re = new RegExp('emoji\\d+', 'gim')
  return message.replace(re, '')
}

/**
 * Given a certain phone number, help the chatbot recognize whether it is in the correct format.
 *
 * @param {string} number
 * @returns {string} the Chatbot response to the phone Validation
 */
export function checkPhoneNumber(number) {
  let re = /\(\+\d{2}\)\s\d{3}-\d{3}-\d{3}/
  let m = number.match(re)
  if (m == null || m.length == 0)
  {
    return `Oops, it seems like I can't reach out to ${number}`
  }
  return 'Thanks! You can now download me to your phone.'
}

/**
 * Given a certain response from the user, help the chatbot get only the URL.
 *
 * @param {string} userInput
 * @returns {string[] | null} all the possible URL's that the user may have answered
 */
export function getURL(userInput) {
  // Edit 1. Account for top level domains like co.uk
  let re = /((http|https):\/\/)?(\w|_)+(\.\w{1,5})+/gim
  return userInput.match(re)
}

/**
 * Greet the user using the full name data from the profile.
 *
 * @param {string} fullName
 * @returns {string} Greeting from the chatbot
 */
export function niceToMeetYou(fullName) {
  // I feel like they wanted something different.
  let reSplit = /,\s?/
  let parts = fullName.split(reSplit)
  return 'Nice to meet you, name'.replace('name', parts[1].trim() + ' ' + parts[0].trim())
}
