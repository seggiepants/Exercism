(module
  (global $balance (mut i32) (i32.const 0))
  (global $opened (mut i32) (i32.const 0))
  ;;
  ;; Set the state of the bank account to open.
  ;;
  ;; @return {i32} 0 on success, -1 on failure
  ;;
  (func (export "open") (result i32)
    global.get $opened
    i32.const 0
    i32.ne
    (if
      (then
        i32.const -1
        return
      )
    )
    i32.const 0
    global.set $balance
    i32.const -1
    global.set $opened
    i32.const 0
    return
  )

  ;;
  ;; Set the state of the bank account to closed.
  ;;
  ;; @return {i32} 0 on success, -1 on failure
  ;;
  (func (export "close") (result i32)
    global.get $opened  ;; if $opened == 0 /* false */ then return -1
    i32.const 0
    i32.eq
    (if
      (then
        i32.const -1
        return
      )
    )
    i32.const 0
    global.set $balance 
    i32.const 0
    global.set $opened 
    i32.const 0
    return
  )

  ;;
  ;; Deposit the given amount into the bank account.
  ;;
  ;; @param {i32} amount - The amount to deposit
  ;;
  ;; @return {i32} 0 on success, -1 if account closed, -2 if amount negative
  ;;
  (func (export "deposit") (param $amount i32) (result i32)
    global.get $opened
    i32.const 0
    i32.eq
    (if 
      (then
        i32.const -1
        return
      )
    )
    local.get $amount 
    i32.const 0
    i32.lt_s
    (if 
      (then
        i32.const -2
        return
      )
    )
    global.get $balance
    local.get $amount
    i32.add
    global.set $balance
    i32.const 0
    return
  )

  ;;
  ;; Withdraw the given amount from the bank account.
  ;;
  ;; @param {i32} amount - The amount to withdraw
  ;;
  ;; @return {i32} 0 on success, -1 if account closed, -2 if amount invalid
  ;;
  (func (export "withdraw") (param $amount i32) (result i32)
    global.get $opened
    i32.const 0
    i32.eq
    (if (then
        (return (i32.const -1)) ;; if !opened return -1
    ))

    local.get $amount
    i32.const 0
    i32.lt_s 
    (if (then   
      (return (i32.const -2)) ;; if amount < 0 return -2
    ))

    global.get $balance
    local.get $amount
    i32.lt_s
    (if (then       
      (return (i32.const -2))   ;; if balance < amount return -2
    ))

    global.get $balance
    local.get $amount
    i32.sub
    global.set $balance

    i32.const 0
    return
  )

  ;;
  ;; Get the current balance of the bank account.
  ;;
  ;; @return {i32} balance on success, -1 if account closed
  ;;
  (func (export "balance") (result i32)
    global.get $opened
    i32.const 0
    i32.eq
    (if 
      (then
        i32.const -1
        return
      )
    )
    global.get $balance
    return
  )
)
