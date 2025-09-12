using System.Collections.Immutable;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

// I said that I have a feeling despite working I did this completely wrong.
// well, I did. Looking at the community solutions it looks like this is how
// it was supposed to be done.  Stole the important parts from ExercismsGhost's solution.
public class HangmanState
{
    public string MaskedWord { get; }
    public ImmutableHashSet<char> GuessedChars { get; }
    public int RemainingGuesses { get; }

    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }
}

public class TooManyGuessesException : Exception
{
}

public class Hangman
{
    const int MAX_GUESSES = 9;
    public IObservable<HangmanState> StateObservable { get; }
    public IObserver<char> GuessObserver { get; }

    string word;
    public Hangman(string word)
    {
        this.word = word;

        // looks like we need subject and StateObservable because you can't get to Value from StateObservable.
        BehaviorSubject<HangmanState> subject = new BehaviorSubject<HangmanState>(new HangmanState(
            MaskWord(word, []),
            new HashSet<char> { }.ToImmutableHashSet(),
            MAX_GUESSES
        ));
        StateObservable = subject;


        GuessObserver = Observer.Create<char>(ch =>
			{
                HangmanState next = NextState(subject.Value, ch);
				if (WonGame(next))
				{
					subject.OnCompleted();
				}
				else if (next.RemainingGuesses < 0) // Game over
				{
					subject.OnError(new TooManyGuessesException());
				}
				else
				{
					subject.OnNext(next);
				}
			}
		);
    }

    private string MaskWord(string word, HashSet<char> set)
    {
        return new string((from char c in word
                       select set.Contains(c) ? c : '_').ToArray<char>());
    }

    public bool IsCharInWord(char c)
    {
        return word.IndexOf(c) != -1;
    }

    public bool WonGame(HangmanState state)
    {
        return word.Equals(state.MaskedWord);
    }

    public HangmanState NextState(HangmanState currentState, char guess)
    {
        HashSet<char> guessed = currentState.GuessedChars.ToHashSet<char>();
        if (!guessed.Contains(guess))
            guessed.Add(guess);

        int remaining = currentState.RemainingGuesses;
        if (currentState.GuessedChars.Contains(guess) || !IsCharInWord(guess))
            remaining--;
        
        currentState = new HangmanState(MaskWord(word, guessed), guessed.ToImmutableHashSet<char>(), remaining);
        return currentState;
    }
}
