1. Test Users:

testuser_1 / testuser111
testuser_2 / testuser222
testuser_3 / testuser333
testuser_4 / testuser444

2. Dummy tasks created for each users, post login the Home page will list the details per user entitlements.

3. tblUserBase will have 3 columns
	
	id 		- Primary key auto increment column
	username 	- Username/login id, for login the username does not need to be case sensitive
	password 	- Passwords will be saved in Encoded format for security, and while login will be decoded & matched exacly
	lastlogin 	- Last login timestamp for audit
	
	Enhancement Possible: 
		
		Create a new table with detailed log of each login and not only the last login
		Add actual user name, email address, phone number for a better experience and full profile, password reset etc.
		
4. Login Screen.
	
	3 Password attempts in the login Screen.
	Post 3 time user will be reset and ask to relogin again.
	Username / password fields cannot be left blank.
	User System IP is tracked and shown at the footer section for audit.
	Post login the logged-in user name will be shown in the header.
	Last login time is shown in the footer.

	Enhancement Possible: 
		
		In Ideal world post 3 attempts user should be forced to reset the password or account should be disabled for a certain time. Only possible when we have a full profile based system.
		IP should be saved in the system along with timestamp of the login attempt for audit purpose.
		
5. Logout button is provided in the header.

6. Post login the saved task lists are shown (tblUserTask).

	tblUserTask table have foreign key constraint with the tblUserBase.
	All the tasks are shown in the decending order of the last modified date.
	Tasks comes with Edit, Delete, Complete Checkbox button.
		Click on Edit wil open editable mode for the clicked task with existing information preloaded for easy editing and save.
		Click on Delete will prompt a confirm window to reconsider the selection and on confirmation deletes the task.
		Delete will soft delete the tasks and set Isactive field as 0 and wont be shown in UI.
		Complete will mark a task as completed and once completed it cannot be uncompleted.
		Completed tasks will not have a Delete and Edit button, and the Complete Checkbox is disabled.
	Add task - button with enable the add task area and once details entered, clicking on save will Save the task. Validations included.

7. In real world, we wouldn't use the inline queries for the transactions queries, we would use SPs for better performance and avoid sql Injections possibilities.

8. Sqlite DB is created under the DB folder inside the solution itself.

9. I have used Bootstarp js/css framework for UI styling for standadization.	

10. I have used Log4Net for error logging. Logs are saved in the Logs folder in the solution.

11. Random Copyright information placed in the footer for display purpose only.

12. A running clock is placed at the footer for UI richness.

13. MSTest test project added and also some very high level test cases integrated.

14. Selenium based framework for UI testing would be helpful.

15. For the interest of the time I have used asp.net 2019 for the application development but would have been easier to use MVC for better user experince and segregating the UI vs Middle Layer vs Data Layer.

16. For the current design the DataLayer is a simple folder in the application itself, but ideally it should be a another Class Library and the output dll should be referred to the web application.

17. Singleton pattern used for Data Connection to maintain only one instance at any point.

18. Ideally the User & Task should have been an Entity itself to have a cleaner approach.

19. The resuable static/extension methods are created for reusable functions like datetime formatting etc to maintain uniformity.

20. As the bootstrap framework is used and also any inline js/css used is designed to make sure to have a Responsive design, to support multibrowser.

------------------------------------------------------------------------------------------------------------------------------------------------

Note:

I would like ot highlight the current situation. We have relocated to Ireland from US just 2 weeks and currently in a temp accommodation, seeking to find housing, schools for both kids and with all the shortcomings and no access to full version of the VS. Hence with limited options at my perusal at this point was able to achieve only so much. But with my well versed career to back my capabilities I would like to take this opportunity to thank you for reviewing the code and seek an opportunity to express my knowledge in a open discussion to clear the shortcomings of the test application. Thanks for your consideration.