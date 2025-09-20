I am designing the data storage

This app will store lots of data entries
For each data entries, there is more data inside

## Current Solution

+ I make any data entry into a C# class for runtime modification/creation/ease of use purpose
+ I serialize them into JSON string
+ I store them using PlayerPrefs

## Issues Now

PlayerPrefs are key-value pairs. I need to have a unique key (uuid) for every data entry (This one solved by UuidGenerator class)

For data accessing convenience, I also need to make many tables for keys.

Any solutions?

