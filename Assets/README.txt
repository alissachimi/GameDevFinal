HOW TO SET UP THE DATABASE FEATURES FOR BEE BALLOON

1. Install xampp.
2. Open the xampp control panel and start MySQL and Apache.
3. Go to localhost/phpmyadmin in any browser.
4. Create a new database called "beeballoonbackend".
5. Open the Assets folder in Unity and find the Database folder. Open it and find the beeballoonbackend.sql file.
6. Go back to localhost/phpmyadmin and click on the newly created database. Click either import and upload this file,
or navigate to the SQL tab and copy the contents of the beeballoonbackend.sql file into this window and click Go. Running
these queries will set up the necessary tables.
7. Find the xampp folder in the File Explorer and open the htdocs folder within in. The Assets/Database folder in Unity
contains another folder called game-dev-final project. Copy this folder and paste it into the htdocs folder.
8. Run the game in Unity and start playing!