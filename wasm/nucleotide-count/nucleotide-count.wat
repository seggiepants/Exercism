(module
  (memory (export "mem") 1)

  ;;
  ;; Count the number of each nucleotide in a DNA string.
  ;;
  ;; @param {i32} offset - The offset of the DNA string in memory.
  ;; @param {i32} length - The length of the DNA string.
  ;;
  ;; @returns {(i32,i32,i32,i32)} - The number of adenine, cytosine, guanine, 
  ;;                                and thymine nucleotides in the DNA string
  ;;                                or (-1, -1, -1, -1) if the DNA string is
  ;;                                invalid.
  ;;
  (func (export "countNucleotides") (param $offset i32) (param $length i32) (result i32 i32 i32 i32)
    (local $adenine i32)
    (local $cytosine i32)
    (local $guanine i32)
    (local $thymine i32)
    (local $index i32)
    (local $index_stop i32)
    (local $char i32)
    
    ;; return -1's if length is less than zero
    (i32.lt_s (local.get $length) (i32.const 0))
    (if (then
      (return (i32.const -1) (i32.const -1) (i32.const -1) (i32.const -1))
    ))

    (local.set $adenine (i32.const 0))
    (local.set $cytosine (i32.const 0))
    (local.set $guanine (i32.const 0))
    (local.set $thymine (i32.const 0))
    (local.set $index (local.get $offset))
    (local.set $index_stop (i32.add (local.get $offset) (local.get $length)))

    (loop $count_loop
      (block $count_body
        (i32.ge_s (local.get $index) (local.get $index_stop))
        (br_if $count_body) ;; continue count loop while index < index_stop

        (local.set $char (i32.load8_u (local.get $index)))        
        (block $found_nucleotide
          (if (i32.eq (local.get $char) (i32.const 65)) ;; 65 = 'A'
            (then 
              (local.set $adenine (i32.add (local.get $adenine) (i32.const 1)))
              (br $found_nucleotide)
            )
          )
          (if (i32.eq (local.get $char) (i32.const 67)) ;; 67 = 'C'
            (then
              (local.set $cytosine (i32.add (local.get $cytosine) (i32.const 1)))
              (br $found_nucleotide)
            )
          )
          (if (i32.eq (local.get $char) (i32.const 71)) ;; 71 = 'G'
            (then
              (local.set $guanine (i32.add (local.get $guanine) (i32.const 1)))
              (br $found_nucleotide)
            )
          )
          (if (i32.eq (local.get $char) (i32.const 84)) ;; 84 = 'T'
            (then
              (local.set $thymine (i32.add (local.get $thymine) (i32.const 1)))
              (br $found_nucleotide)
            )
          )
          
          (return (i32.const -1) (i32.const -1) (i32.const -1) (i32.const -1))
        )

        (local.set $index (i32.add (local.get $index) (i32.const 1)))   ;; index++      
        (br $count_loop)
      )
    )
    (return
      (local.get $adenine)
      (local.get $cytosine)
      (local.get $guanine)
      (local.get $thymine)
    )
    
  )
)
