module SavingsAccount
  def self.interest_rate(balance)
    if balance < 0.0
      -3.213
    elsif balance < 1000.0
      0.5
    elsif balance < 5000.0
      1.621
    else
      2.475
    end
  end

  def self.annual_balance_update(balance)
    (balance + (balance.abs * (interest_rate(balance) / 100.0))).to_f
  end

  def self.years_before_desired_balance(current_balance, desired_balance)
    balance = current_balance
    years = 0
    until balance >= desired_balance
      years += 1
      balance = annual_balance_update(balance)
    end
    years
  end
end
