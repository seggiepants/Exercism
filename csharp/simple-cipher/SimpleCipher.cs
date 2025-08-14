using System.Text;

/// <summary>
/// Substitution Cipher
/// NOTE: Only characters a-z are encoded all others are passed through unchanged.
/// </summary>
public class SimpleCipher
{
    const int ALPHABET_LEN = (int)'z' - (int)'a' + 1; // a-z range
    string cipherKey; // encoding key.

    /// <summary>
    /// Class intializer, uses a random key
    /// </summary>
    public SimpleCipher()
    {
        cipherKey = RandomKey();
    }

    /// <summary>
    /// Class initializer, initialize with the given key
    /// </summary>
    /// <param name="key">The cipher key to use</param>
    public SimpleCipher(string key)
    {
        cipherKey = key;
    }

    /// <summary>
    /// Return the class' cipher key.
    /// </summary>
    public string Key
    {
        get
        {
            return cipherKey;
        }
    }

    /// <summary>
    /// Generate a random key when one is not provided. Change KEY_LENGTH to get larger or smaller keys.
    /// </summary>
    /// <returns>A string of random characters 'a' to 'z' of length KEY_LENGTH.</returns>
    private static string RandomKey()
    {
        const int KEY_LENGTH = 100;
        Random rnd = new();
        StringBuilder sb = new();
        for (int i = 0; i < KEY_LENGTH; i++)
        {
            sb.Append((char)(rnd.Next(ALPHABET_LEN) + (int)'a'));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Encode the given text with the class' key
    /// </summary>
    /// <param name="plaintext">The text to encode</param>
    /// <returns>The encoded text</returns>
    public string Encode(string plaintext)
    {

        StringBuilder sb = new();
        for (int i = 0; i < plaintext.Length; i++)
        {
            if (plaintext[i] >= 'a' && plaintext[i] <= 'z')
            {
                int ch = (int)plaintext[i] - (int)'a';
                ch = (ch + (int)cipherKey[i % cipherKey.Length] - (int)'a') % ALPHABET_LEN;
                sb.Append((char)(ch + (int)'a'));
            }
            else
            {
                sb.Append(plaintext[i]); // Append normal if not in range.
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Decode a given cipher using the class' key
    /// </summary>
    /// <param name="ciphertext">The text to decode</param>
    /// <returns>The decoded text</returns>
    public string Decode(string ciphertext)
    {
        StringBuilder sb = new();
        for (int i = 0; i < ciphertext.Length; i++)
        {
            if (ciphertext[i] >= 'a' && ciphertext[i] <= 'z')
            {
                int ch = (int)ciphertext[i] - (int)'a';
                // don't want to go less than zero so add in ALPHABET_LEN to be modded of but keep the value >= 0.
                ch = (ch + ALPHABET_LEN - ((int)cipherKey[i % cipherKey.Length] - (int)'a')) % ALPHABET_LEN;
                sb.Append((char)(ch + (int)'a'));
            }
            else
            {
                sb.Append(ciphertext[i]); // Append normal if not in range.
            }
        }
        return sb.ToString();
    }
}