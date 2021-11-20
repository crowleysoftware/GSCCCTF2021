
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