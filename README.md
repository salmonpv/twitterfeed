# twitterfeed

<h2>Instructions</h2>

Open solution in visual studio 2013 or higher
Build and run

If Visual Studio Commandline tools have been installed
Or Open Visual studio command prompt 
Navigate to the repository and run MSBuild


<h2>Changing input files</h2>
Replace the user.txt and the tweet.txt in the bin directory

or

Specify file names in commandline arguments, the user file followed by the tweet file
    
    AG_Twitter.exe useralternative.txt tweetalternative.txt

<h2>Assumptions</h2>

1. The names are case insensitive (Alan could occur later as alan)
2. User's name cannot contain > character
3. Server memory isn't a issue with the amount of tweets that has to be aggregated
4. A name can contain the word follows
5. the follows keyword will always be in lower case
6. Empty tweets should be ignored
