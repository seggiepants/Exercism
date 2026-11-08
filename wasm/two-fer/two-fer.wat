(module
  (memory (export "mem") 1)
  (data $one_for "One for ")
  (data $you "you")
  (data $one_for_me ", one for me.")

  ;;
  ;; Given a string X, return a string that says "One for X, one for me."
  ;; If the X is empty, return the string "One for you, one for me."
  ;;
  ;; @param {i32} $offset - The offset of the name in linear memory.
  ;; @param {i32} $length - The length of the name in linear memory.
  ;; 
  ;; @return {(i32,i32)} - The offset and length the resulting string in linear memory.
  ;;
  (func (export "twoFer") (param $offset i32) (param $length i32) (result i32 i32)
    (local $index i32)
    (local $bytes i32)

    (local.set $index (i32.const 0))
    (local.set $bytes (i32.const 8)) ;; 'One for ' is 8 characters

    (memory.init $one_for (local.get $index) (i32.const 0) (local.get $bytes))
    (data.drop $one_for)
    (local.set $index (i32.add (local.get $index) (local.get $bytes)))

    (if (i32.eq (i32.const 0) (local.get $length))
      (then
        (local.set $bytes (i32.const 3)) ;; 'you' is 3 characters
        (memory.init $you (local.get $index) (i32.const 0) (local.get $bytes))
        (data.drop $you)
        (local.set $index (i32.add (local.get $index) (local.get $bytes)))

      )
      (else 
        (local.set $bytes (local.get $length)) ;; length of the given name
        (memory.copy (local.get $index) (local.get $offset) (local.get $bytes))
        (local.set $index (i32.add (local.get $index) (local.get $bytes)))
      )
    )

    (local.set $bytes (i32.const 13)) ;; ', one for me.' is 13 characters
    (memory.init $one_for_me (local.get $index) (i32.const 0) (local.get $bytes))
    (data.drop $one_for_me)
    (local.set $index (i32.add (local.get $index) (local.get $bytes)))
    
    (return (i32.const 0) (local.get $index))
  )
)
