# Cert-ainly Decrypted/Encrypted

This write up is for both of the "Cert-ainly" challenges:

### Challenges
> The flag is "ladder-stage-mesh-corn", but the twist is that YOU have to encrypt it.

> sqSveAKQw6YBSryUar1anWxlr/6b2MB6gq72AYvHhqB00U5M9yqYu4kTCKw6IHJRY16nnDw/6E4MdS6EhdNu9sVDWWk3VkG8t2/xlT7Zu6nVKLwl9b3UeTMfjqlYh3WOyhtw3iJKs8NnmGB6yp4X/7hFNu+MSjrV0lbxrUL8crk=

The provided link offers you a file to download: "bookclub.pfx"  

This pfx file contains a public key and a private key. Given these keys, you can encrypt and decrypt data. Here is a C# example:  

````
X509Certificate2 cert = new X509Certificate2(@"c:\deleteme\bookclub.pfx", "bookclub");

RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;

byte[] bytesToEncrypt = Encoding.UTF8.GetBytes("ladder-stage-mesh-corn");
byte[] encryptedBytes = rsa.Encrypt(bytesToEncrypt, false);

string b64val = Convert.ToBase64String(encryptedBytes);

Console.WriteLine(b64val);


RSACryptoServiceProvider rsa2 = (RSACryptoServiceProvider)cert.PrivateKey;

byte[] encryptedBytesToDecrypt = Convert.FromBase64String(b64val);
byte[] result = rsa2.Decrypt(encryptedBytesToDecrypt, false);

string final = Encoding.UTF8.GetString(result);

Console.WriteLine(final);
````

This challenge demonstrates how important it is to protect your certificates.