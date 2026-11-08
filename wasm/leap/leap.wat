(module
  ;;
  ;; Determine if a year is a leap year
  ;;
  ;; @param {i32} year - The year to check
  ;;
  ;; @returns {i32} 1 if leap year, 0 otherwise
  ;;
  (func (export "isLeap") (param $year i32) (result i32)
    ;; if $year %4 != 0 return 0        
    (i32.ne (i32.rem_s (local.get $year) (i32.const 4)) (i32.const 0))
    (if (then (return (i32.const 0))))

    ;; if $year % 100 == 0 then    
    (i32.eq (i32.rem_s (local.get $year) (i32.const 100)) (i32.const 0))
    (if (then
      ;; if $year %400 != 0 then return 0      
      (i32.ne (i32.rem_s (local.get $year) (i32.const 400)) (i32.const 0))
      (if (then (return (i32.const 0))))
    ))
    (return (i32.const 1))
  )  
)
