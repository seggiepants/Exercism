package erratum

func Use(opener ResourceOpener, input string) (err error) {
	// Do I need to name the output?
	// Looks like I do or it doesn't get returned properly.
	var resource Resource = nil
	var ok bool

	resource, err = opener()
	for err != nil {

		// Somehow didn't know about err.(ErrorType) which returns ErrorType, boolean
		_, ok = err.(TransientError)
		if !ok {
			return err
		}
		resource, err = opener()
	}

	defer func(resource Resource) {
		result := recover() // This is the magic line I didn't know about.
		if result != nil {
			var frobErr FrobError
			frobErr, ok = result.(FrobError)
			if ok {
				if resource != nil {
					resource.Defrob(frobErr.defrobTag)
				}
			}
			err = result.(error)
		}
		if resource != nil {
			resource.Close()
		}
	}(resource)

	resource.Frob(input)
	return err
}
