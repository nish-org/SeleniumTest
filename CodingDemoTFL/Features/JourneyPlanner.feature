Feature: JourneyPlanner

Background: 

		Given User is navigate to jouney planner page


Scenario:1_Valid journey returnes when user enter the valid location
	And I enter a journey from 'Sudbury Town Underground Station'  to 'Park Royal Underground Station'
	When I plan my journey now
	Then the result page should have atleast 1 journey
	And the result page should include journey options, journey details and journey time
		

Scenario Outline:2_Inalid journey returnes when user enter either of the invalid location
	And I enter a journey from '<From>'  to '<To>'
	When I plan my journey now
	Then I should see an error message '<msg>'
Examples: 
	| No | From                                | To                                  | msg                                                                         |
	| 1  | 1234                                | Wembley Central Underground Station | Journey planner could not find any results to your search. Please try again |
	| 2  | Wembley Central Underground Station | 1234                                | Journey planner could not find any results to your search. Please try again |
	| 3  | 1234                                | 9874                                | Journey planner could not find any results to your search. Please try again |
	| 4  | "112"                               | "$$"                                | Sorry, we can't find a journey matching your criteria                       |
				 	
Scenario Outline:3_User is unable to plan a journey when no locations are entered  
	And I enter no data to journey from and to field
	When I plan my journey now
	Then the validation error triggered for From field as 'The From field is required.'
	And the validation error triggered for To field as 'The To field is required.'

Scenario:4_User is able to amend the journey on the result page by using the edit journey button  
	And I plan journey from '<From>'  to '<To>'
	When I edit the journey for destination station to '<NewDestination>'
	And  I update the journey
	Then I should be able to view journey from '<From>'  to '<NewDestination>'
Examples: 
    | From                             | To                             | NewDestination                    |
    | Sudbury Town Underground Station | Park Royal Underground Station | Ealing Common Underground Station |


Scenario:5_Recents tab on the widget displays a list of recently planned journeys  
	And I plan first journey from '<Journey1From>'  to '<Jounery1To>'
	When I view recent tab
	Then I should be able to see both my journey in recent tab
		| Journey                        |
		| <Journey1From> to <Jounery1To> |
		
Examples: 
   | Journey1From                   | Jounery1To                       | 
   | Sudbury Town Underground Station | Park Royal Underground Station | 

