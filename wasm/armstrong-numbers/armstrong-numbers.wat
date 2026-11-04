(module
  ;; 
  ;; Determine if a number is an Armstrong number.
  ;;
  ;; @param {i32} candidate - The number to check.
  ;;
  ;; @return {i32} 1 if the number is an Armstrong number, 0 otherwise.
  ;;
  (func (export "isArmstrongNumber") (param $candidate i32) (result i32)
    (local $count_digits i32) 
    (local $remaining i32) 
    (local $total i32)
    (local $remainder i32)
    local.get $candidate
    call $getLength
    local.set $count_digits                 ;; count_digits = getLength(candidate)
    i32.const 0
    local.set $total                        ;; total = 0;
    local.get $candidate
    local.set $remaining                    ;; remaining = candidate

    (loop $digit_loop
      
      local.get $remaining                  ;; remainder = remaining mod 10
      i32.const 10
      i32.rem_s
      local.set $remainder
      local.get $remainder
      local.get $count_digits
      call $pow
      local.get $total
      i32.add
      local.set $total                      ;; total += pow(remainder, count_digits)


      local.get $remaining                  ;; remaining = remaininig - remainder
      local.get $remainder
      i32.sub
      local.set $remaining

      local.get $remaining
      i32.const 10                          ;; remaining /= 10
      i32.div_s
      local.set $remaining

      local.get $remaining
      i32.const 0
      i32.ne                                ;; loop if remainder != 0
      br_if $digit_loop
    )
    local.get $total
    local.get $candidate
    i32.eq
    return
  )

  (func $getLength (param $candidate i32)  (result i32)
    (local $count i32) 
    (local $remaining i32)
    (local $remainder i32)
    
    i32.const 0
    local.set $count            ;; count = 0
    local.get $candidate        ;; remaining = candidate
    local.set $remaining
    
    (loop $count_loop
      local.get $count          ;; count++
      i32.const 1
      i32.add
      local.set $count

      local.get $remaining      ;; remainder = remaining mod 10
      i32.const 10
      i32.rem_u
      local.set $remainder

      local.get $remaining      ;; remaining = remaininig - remainder
      local.get $remainder
      i32.sub
      local.set $remaining

      local.get $remaining
      i32.const 10              ;; remaining /= 10
      i32.div_s
      local.set $remaining 

      local.get $remaining
      i32.const 0
      i32.ne                    ;; loop if remainder != 0
      br_if $count_loop
    )
    local.get $count            ;; return count
    return
  )

  (func $pow (param $number i32) (param $exponent i32) (result i32) 
    (local $total i32) 
    (local $count i32)
    i32.const 1
    local.set $total            ;; total = 1
    i32.const 0
    local.set $count            ;; count = 0

    (loop $exp_loop
      local.get $number
      local.get $total
      i32.mul
      local.set $total        ;; total *= number

      local.get $count
      i32.const 1
      i32.add 
      local.set $count        ;; count++

      local.get $count
      local.get $exponent
      i32.ne 
      br_if $exp_loop         ;; continue if exponent != count
    )

    local.get $total
    return                    ;; return total

  )

)
