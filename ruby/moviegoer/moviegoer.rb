# This is a custom exception that you can use in your code
class NotMovieClubMemberError < RuntimeError
end

class Moviegoer
  def initialize(age, member: false)
    @age = age
    @member = member
  end

  def ticket_price
    @age >= 60 ? 10.00 : 15.00
  end

  def watch_scary_movie?
    # could just do the @age >= 18 part but this is a ternary operator exercise so...
    @age >= 18 ? true : false
  end

  # Popcorn is 🍿
  def claim_free_popcorn!
    # Have to put the raise in parenthesis for some reason.
    @member ? "🍿" : (raise NotMovieClubMemberError.new) 
  end
end
