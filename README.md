# SeleniumTest

Consideration

Used tube as a preference to search results in all the tests

Page object model: POM, is a design pattern in Selenium that creates an object repository for storing all web elements. It helps reduce code duplication and improves test case maintenance.

page object factory: Page class looks neat and the instance would be part of the page object factory class

Page class: all the page classes related to pages are under the page folder

Extension methods: Easier to work with web elements and which does support dynamic wait

AppConfig Manager: To read data from run settings

Common hooks: Add common or generic hooks

Webdriver hooks: All the driver-related setup functions where the setup starts during the test run

WebDriverSupport: Generic functions which related to the driver

Browser Factory: All the browser config should be part of this class

Steps: All spec flow steps are implemented under steps folder
