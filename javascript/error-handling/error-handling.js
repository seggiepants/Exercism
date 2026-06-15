// Throw a number of different errors of different types.
//

// Process as string, throwing errors as necessary.
// @param input: The input string ot be processed.
// @returns: An uppercase version of the input string
// @raises: A variety of errors. We check type, length, 
// range of length, if values are mixed alpha and 
// numeric and throw errors as appropriate.
export const processString = (input) => {
  try {
    if (!processString instanceof String) {
      throw new TypeError("input is not a string")
    }

    let inputLen = input.length
    if (inputLen === 0) {
      return null
    }

    if (inputLen < 10 || inputLen > 100) {
      throw new RangeError(`Input length out of range expected 10-100 but input was ${inputLen} characters.`)
    }

    if(/[a-zA-Z]/.test(input) && /[0-9]/.test(input)) {
      throw new SyntaxError("Input may not have mixed alphabetic and numeric characters.")
    }

    return input.toUpperCase()

  } catch(ex) {
    console.log(ex.message)
    throw ex
  }
};
