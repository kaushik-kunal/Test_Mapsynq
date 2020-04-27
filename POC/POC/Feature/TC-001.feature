Feature: HomePage-TC-001
	Test on Incident location search

@TC-001 @Reg 
Scenario Outline: Incident Location Search based on Area Name
Given I am on MapSynq Home screen
When I input <AreaName> in Incident Location text field
And I Select Date as <Date> from the dropdown 
Then I see entries of the incidents based on the Area name provided along with Time in HH:MM format


Examples:
| AreaName | Date         |
| "Jurong" | "Today"      |
