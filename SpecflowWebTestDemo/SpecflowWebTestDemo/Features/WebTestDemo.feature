Feature: WebTestDemo
	In order to familiar specflow
	As a beginner
	I want to be create a specflow demo

@Chrome
@Firefox
@Edge
Scenario: Search keyword on JD
	Given I visit JD website
	And I have entered "Dell" into the search box
	When I press search
	Then the result should be "戴尔京东自营官方旗舰店" on the screen