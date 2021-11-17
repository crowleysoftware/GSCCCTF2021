// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, Hackers!");
Console.WriteLine("Enter a string to encrypt:");
string clearText = Console.ReadLine();
Console.WriteLine("Enter the offset to use:");
int offset = int.Parse(Console.ReadLine());

string encodedMessage = Encode(clearText, offset);

Console.BackgroundColor = ConsoleColor.Green;
Console.ForegroundColor = ConsoleColor.Black;

Console.WriteLine($"Encoded Value: {encodedMessage}");

string decoded = Decode(encodedMessage, offset);
Console.WriteLine($"Decoded value: {decoded}");

Console.ResetColor();

// Assumes positive offset
static string Encode(string clearText, int offset)
{
    string alphabet = "abcdefghijklmnopqrstuvwxyz";
    StringBuilder cipher = new StringBuilder();

    if (offset > 26)
    {
        offset = offset % 26;
    }

    for (int i = 0; i < clearText.Length; i++)
    {
        char ltr = char.ToLower(clearText[i]);

        if (alphabet.Contains(ltr)) //apply non-letters verbatim
        {
            //Maintain upper case letters even though "alphabet" is all lower
            bool isupper = char.IsUpper(clearText[i]);

            int idx = alphabet.IndexOf(ltr);

            // no wrap around the alphabet needed
            if (26 - idx > offset)
            {
                char n = isupper ? char.ToUpper(alphabet[idx + offset]) : alphabet[idx + offset];
                cipher.Append(n);
            }
            else //offset of current letter wraps back to front of alphabet
            {
                char n = isupper ? char.ToUpper(alphabet[offset - (26 - idx)]) : alphabet[offset - (26 - idx)];
                cipher.Append(n);
            }
        }
        else
        {
            cipher.Append(clearText[i]);
        }
    }
    return cipher.ToString();
}

// Assumes positive offset
static string Decode(string clearText, int offset)
{
    string alphabet = "abcdefghijklmnopqrstuvwxyz";
    StringBuilder cipher = new StringBuilder();

    if (offset > 26)
    {
        offset = offset % 26;
    }

    for (int i = 0; i < clearText.Length; i++)
    {
        char ltr = char.ToLower(clearText[i]);

        if (alphabet.Contains(ltr)) //apply non-letters verbatim
        {
            //Maintain upper case letters even though "alphabet" is all lower
            bool isupper = char.IsUpper(clearText[i]);

            int idx = alphabet.IndexOf(ltr);

            // no wrap around the alphabet needed
            if (idx > offset)
            {
                char n = isupper ? char.ToUpper(alphabet[idx - offset]) : alphabet[idx - offset];
                cipher.Append(n);
            }
            else //offset of current letter wraps back to front of alphabet
            {
                char n = isupper ? char.ToUpper(alphabet[26 - (offset - idx)]) : alphabet[26 - (offset - idx)];
                cipher.Append(n);
            }
        }
        else
        {
            cipher.Append(clearText[i]);
        }
    }
    return cipher.ToString();
}