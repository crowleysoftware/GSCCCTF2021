# Choose Wisely

### Challenge
> ae443db25650783b78c8f3bd67969062adabe6dd7390b53bbed9f47335fd8db7

The clue is a link to a long list of what look like flags:

salad-acrid-lower-clasp-exams  
alarm-chemo-chump-cases-bulla  
autos-coeds-retro-clown-burnt  
gases-brews-enter-bluff-solve  
noise-fakir-fatty-hints-study  
siege-cocci-dotes-rival-fleas  
cuter-trail-sheep-patch-bendy  
comes-fizzy-melee-truth-gross  
argot-caked-grave-quilt-field  
etc..  
etc..  

You need to figure out what relationship the seemingly random string has to the list of flags. 

Hopefully you will come to the conclusion that the value is a hash. But which type? If you recognize the values as hexadecimal you can determine this hash is 32 bytes. At 32 bytes you are almost certainly dealing with SHA256.

Going one-by-one you can hash each flag and see if it matches the challenge. The one that matches is the flag we are looking for. Better yet, write a utility to loop over each one. Here's a PowerShell script to get you started:

````$hasher = [System.Security.Cryptography.HashAlgorithm]::Create("sha256")
$hash = $hasher.ComputeHash([System.Text.Encoding]::UTF8.GetBytes("salad-acrid-lower-clasp-exams"))

$hashString = [System.BitConverter]::ToString($hash)
$final = $hashString.Replace("-", "")

$final````

Of course you can always brute force the system by trying to submit each flag until it works. This is a valid, if not tedious strategy and it demonstrates the importance of rate limits and retry restrictions.