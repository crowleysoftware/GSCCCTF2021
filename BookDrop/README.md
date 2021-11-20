# Book Drop

### Challenge
> Delete a book

There is a link to "goto Book Club"  

That is not much to go on. You scour the site but don't see any way to delete a book. But you know that applications typically communicate with a back-end API, and APIs are commonly RESTful. The canonical pattern for deleting a resource is to use the http DELETE method at a route similar to [url]/[controller]/[resource id].  

With some educated guessing you send a DELETE request to https://gsccctf-bookclub.azurewebsites.net/book/[xxxx], where xxxx is a book id that you randomly guess. If you guess an actual book id, the response will contain the flag.  

`{
    "flag": "magnet-wind-oar-brook"
}`

This callenge is meant to be a reminder that even if your front end does not allow an operation, it does not mean that an attacker cannot go directly at the API.


