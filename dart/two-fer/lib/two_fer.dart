// Return a two-fer message using the optional parameter name and having a 
// failsafe it not passed in.
// @param string name (optional): The name of the user to share with. "you" if not set.
// @returns: a two-fer statement customized with the given name/"you".
String twoFer([String name = 'you']) {
  return 'One for $name, one for me.';
}
