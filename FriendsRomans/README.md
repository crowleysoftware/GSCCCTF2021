# Friends, Romans, Hackers

### Challenge
> tlypa-hshyt-wypua-tpely-zlabw

## There is a [complete .NET 6 solution](./CaesarCipher) that shows an example encoding and decoding process of this challenge.

The title of this challenge is meant to be a clue that there is something about Caesar going on here. The famous quote from Shakespeare's *Julius Caesar* of course being: "*Friends, Romans, countrymen, lend me your ears....*".

As this is a hacker challenge, in your research you might come across a famous cipher known as a [Caesar Cipher](https://en.wikipedia.org/wiki/Caesar_cipher). This is a simple ciphering technique that encodes a message by replacing letters with another letter that is *n* places away in the alphabet. For example:

**A B C D E F G H I J K L M N O P Q R S T U V W X Y Z**  
"Hello-World!" encoded with an offset of 3 becomes:  
H + 3 = K  
E + 3 = H  
L + 3 = O  
etc...

Decoding is simply a matter of moving backwards 3 places (assuming you know the offset is 3):  
K - 3 = H  
H - 3 = E  
O - 3 = L  
etc...

Even if you don't know the offset this is easily brute forced. There is even [an online tool that will do that for you.](https://www.dcode.fr/caesar-cipher). 

What is left up to you is to figure out that the offset in the challenge is 7.


