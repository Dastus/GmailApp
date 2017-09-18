# GmailApp

Key features:
1. Google API used to RO access to user's email info
2. Parallel foreach used to call Gmail API for messages content
3. Ajax partial view used to display messages info
4. Pagination added
5. Max count of messages and messages count per page can be configured in Constants.cs class
6. Bootstrap is used for some styling
7. Inversion of Control isn't really needed there, it was added just in case.

Weak points:
1. GoogleClientId and GoogleClientSecret are stored in Constants.cs as plain text
2. Single user token exists and stored in cache with "userToken" key, so in case if few users try to use application, the messages of stored user will be shown. Additional logic should be implemented to separate user tokens storing.   
