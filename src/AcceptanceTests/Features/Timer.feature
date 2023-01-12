Feature: Timer Rest Api
Rest API with 2 Endpoints: To Set Timer and Get Timer Status.
Set Timer should take JSON payload with hours, minutes, seconds and a callback url.
And returns an id as JSON. End endpont sets a timer which calls the url passed in.
The Get timer status take the id and returns the status time left of the call back

Background: 
	Given The Rest Api endpoint is live

Scenario: Setting the timer
	When Set timer is called with empty Callback url
	Then Should return Bad Request
	When Set timer is called with 0 hours 1 minute and 0 seconds and callback url "https://google.com"
	Then Should return an Id
	When Get tiemer status API is called using the Id
	Then Should return JSON data with Id and the time left should be less then 1 minutes
	When Set timer is called with 0 hours 0 minute and 0 seconds and callback url "https://google.com"
	Then Should return an Id
	When Get tiemer status API is called using the Id
	Then Should return JSON data with Id and the time left should be 0 minute
	When Get timer is call with a not known Id 
	Then Should return Not Found
